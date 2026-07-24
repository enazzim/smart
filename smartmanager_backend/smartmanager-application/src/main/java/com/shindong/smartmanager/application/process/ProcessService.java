package com.shindong.smartmanager.application.process;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import com.shindong.smartmanager.application.workstandard.WorkStandardRepository;
import java.util.EnumSet;
import java.util.List;
import java.util.Set;

public class ProcessService {

    private static final Set<String> EXCLUDED_PROCESS_CODES = Set.of("14000000", "14009999");
    private static final Set<PropertyClassification> ALLOWED_ITEM_CLASSES =
            EnumSet.of(PropertyClassification.제품, PropertyClassification.공정품);

    private final ProcessRepository processRepository;
    private final ItemRepository itemRepository;
    private final ProcessCodeLookup processCodeLookup;
    private final WorkCenterLookup workCenterLookup;
    private final WipBalanceProjector wipBalanceProjector;
    private final WorkStandardRepository workStandardRepository;
    private final DomainEventStore domainEventStore;

    public ProcessService(
            ProcessRepository processRepository,
            ItemRepository itemRepository,
            ProcessCodeLookup processCodeLookup,
            WorkCenterLookup workCenterLookup,
            WipBalanceProjector wipBalanceProjector,
            WorkStandardRepository workStandardRepository,
            DomainEventStore domainEventStore
    ) {
        this.processRepository = processRepository;
        this.itemRepository = itemRepository;
        this.processCodeLookup = processCodeLookup;
        this.workCenterLookup = workCenterLookup;
        this.wipBalanceProjector = wipBalanceProjector;
        this.workStandardRepository = workStandardRepository;
        this.domainEventStore = domainEventStore;
    }

    public ProcessView register(ProcessCommand command, String actorUserId) {
        ProcessCommand normalized = normalizeOutsideOrderRate(command);
        validateCommand(normalized, null);
        ItemView item = getAllowedItem(normalized.itemId());
        ProcessCodeLookup.ProcessCodeInfo processCode = getAllowedProcessCode(normalized.processCodeId());

        if (processRepository.existsActiveDuplicate(
                normalized.itemId(), normalized.processCodeId(), normalized.processSequenceNum(), null)) {
            throw new IllegalArgumentException("동일 품목·공정·순번 조합이 이미 존재합니다.");
        }

        long processId = processRepository.save(normalized, ProcessVariant.plan, actorUserId);
        wipBalanceProjector.ensure(normalized.itemId(), processId, actorUserId);

        domainEventStore.append(DomainEvent.create(
                EventTypes.PROCESS_REGISTERED,
                1,
                AggregateTypes.PROCESS,
                String.valueOf(processId),
                actorUserId,
                buildPayload(processId, normalized, item, processCode)
        ));

        return getActive(processId);
    }

    public ProcessView update(long id, ProcessUpdateCommand command, String actorUserId) {
        ProcessView existing = getActive(id);
        ProcessUpdateCommand normalized = normalizeOutsideOrderRate(command);
        validateCommand(toCommand(normalized), id);
        ItemView item = getAllowedItem(normalized.itemId());
        ProcessCodeLookup.ProcessCodeInfo processCode = getAllowedProcessCode(normalized.processCodeId());

        if (processRepository.existsActiveDuplicate(
                normalized.itemId(), normalized.processCodeId(), normalized.processSequenceNum(), id)) {
            throw new IllegalArgumentException("동일 품목·공정·순번 조합이 이미 존재합니다.");
        }

        processRepository.update(id, normalized, actorUserId);

        if (existing.itemId() != normalized.itemId()) {
            wipBalanceProjector.updateItemId(id, normalized.itemId(), actorUserId);
        } else {
            wipBalanceProjector.reconcile(normalized.itemId(), id, actorUserId);
        }

        domainEventStore.append(DomainEvent.create(
                EventTypes.PROCESS_UPDATED,
                1,
                AggregateTypes.PROCESS,
                String.valueOf(id),
                actorUserId,
                buildPayload(id, toCommand(normalized), item, processCode)
        ));

        return getActive(id);
    }

    public void delete(long id, String actorUserId) {
        ProcessView existing = getActive(id);
        workStandardRepository.softDeleteByProcessSequenceId(id, actorUserId);
        processRepository.softDelete(id, actorUserId);
        wipBalanceProjector.deactivate(id, actorUserId);

        domainEventStore.append(DomainEvent.create(
                EventTypes.PROCESS_DELETED,
                1,
                AggregateTypes.PROCESS,
                String.valueOf(id),
                actorUserId,
                """
                {"processId":%d,"itemId":%d,"processSequenceNum":%d}
                """.formatted(id, existing.itemId(), existing.processSequenceNum()).trim()
        ));
    }

    public List<ProcessView> listActiveByItemId(long itemId) {
        return processRepository.findAllActiveByItemId(itemId, ProcessVariant.plan);
    }

    public List<ProcessView> listActiveAll() {
        return processRepository.findAllActive(ProcessVariant.plan);
    }

    public ProcessView getActive(long id) {
        return processRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("공정을 찾을 수 없습니다: " + id));
    }

    private void validateCommand(ProcessCommand command, Long excludeId) {
        if (command.processSequenceNum() == 99) {
            throw new IllegalArgumentException("공정 순번 99는 사용할 수 없습니다.");
        }
        if (command.processSequenceNum() <= 0) {
            throw new IllegalArgumentException("공정 순번은 1 이상이어야 합니다.");
        }
        if (command.progressRate() < 0 || command.progressRate() > 100) {
            throw new IllegalArgumentException("진척비율은 0~100 사이여야 합니다.");
        }
        if (command.workDistinction() == null) {
            throw new IllegalArgumentException("작업구분은 필수입니다.");
        }

        switch (command.workDistinction()) {
            case INHOUSE -> {
                requireWorkCenter(command.workCenterId());
                // 발주비율은 normalizeOutsideOrderRate에서 0으로 고정
            }
            case OUTSOURCE -> {
                if (command.workCenterId() != null) {
                    throw new IllegalArgumentException("외주 공정은 작업장을 지정할 수 없습니다.");
                }
                // 발주비율은 무시 후 0 저장 (normalizeOutsideOrderRate)
            }
            case SPLIT -> {
                requireWorkCenter(command.workCenterId());
                // 전량 자가(0)~전량 외주(100)까지 운영 중 조정 가능
                if (command.outsideOrderRate() < 0 || command.outsideOrderRate() > 100) {
                    throw new IllegalArgumentException("혼합 공정의 발주비율은 0~100 사이여야 합니다.");
                }
            }
            default -> throw new IllegalArgumentException("지원하지 않는 작업구분입니다.");
        }
    }

    /** 자가·외주 전용은 발주비율 입력을 무시하고 0으로 저장한다. */
    private ProcessCommand normalizeOutsideOrderRate(ProcessCommand command) {
        if (command.workDistinction() == WorkDistinction.INHOUSE
                || command.workDistinction() == WorkDistinction.OUTSOURCE) {
            if (command.outsideOrderRate() == 0) {
                return command;
            }
            return new ProcessCommand(
                    command.itemId(),
                    command.processSequenceNum(),
                    command.processCodeId(),
                    command.workDistinction(),
                    command.workCenterId(),
                    0,
                    command.progressRate()
            );
        }
        return command;
    }

    private ProcessUpdateCommand normalizeOutsideOrderRate(ProcessUpdateCommand command) {
        if (command.workDistinction() == WorkDistinction.INHOUSE
                || command.workDistinction() == WorkDistinction.OUTSOURCE) {
            if (command.outsideOrderRate() == 0) {
                return command;
            }
            return new ProcessUpdateCommand(
                    command.itemId(),
                    command.processSequenceNum(),
                    command.processCodeId(),
                    command.workDistinction(),
                    command.workCenterId(),
                    0,
                    command.progressRate()
            );
        }
        return command;
    }

    private void requireWorkCenter(Long workCenterId) {
        if (workCenterId == null) {
            throw new IllegalArgumentException("작업장은 필수입니다.");
        }
        if (!workCenterLookup.existsActive(workCenterId)) {
            throw new IllegalArgumentException("작업장을 찾을 수 없습니다: " + workCenterId);
        }
    }

    private ItemView getAllowedItem(long itemId) {
        ItemView item = itemRepository.findActiveById(itemId)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + itemId));
        if (!ALLOWED_ITEM_CLASSES.contains(item.propertyClassification())) {
            throw new IllegalArgumentException("제품·공정품만 공정을 등록할 수 있습니다.");
        }
        return item;
    }

    private ProcessCodeLookup.ProcessCodeInfo getAllowedProcessCode(long processCodeId) {
        ProcessCodeLookup.ProcessCodeInfo code = processCodeLookup.findActiveProcessCode(processCodeId)
                .orElseThrow(() -> new IllegalArgumentException("공정코드를 찾을 수 없습니다: " + processCodeId));
        if (EXCLUDED_PROCESS_CODES.contains(code.smallCode())) {
            throw new IllegalArgumentException("예약 공정코드는 사용할 수 없습니다: " + code.smallCode());
        }
        return code;
    }

    private ProcessCommand toCommand(ProcessUpdateCommand command) {
        return new ProcessCommand(
                command.itemId(),
                command.processSequenceNum(),
                command.processCodeId(),
                command.workDistinction(),
                command.workCenterId(),
                command.outsideOrderRate(),
                command.progressRate()
        );
    }

    private String buildPayload(
            long processId,
            ProcessCommand command,
            ItemView item,
            ProcessCodeLookup.ProcessCodeInfo processCode
    ) {
        return """
                {"processId":%d,"itemId":%d,"itemNo":"%s","processSequenceNum":%d,"processCodeId":%d,"processCode":"%s","workDistinction":"%s"}
                """.formatted(
                processId,
                command.itemId(),
                escape(item.itemNo()),
                command.processSequenceNum(),
                command.processCodeId(),
                escape(processCode.smallCode()),
                command.workDistinction().name()
        ).trim();
    }

    private String escape(String value) {
        return value.replace("\\", "\\\\").replace("\"", "\\\"");
    }
}
