package com.shindong.smartmanager.application.workstandard;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.application.process.WorkCenterLookup;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import java.util.EnumSet;
import java.util.List;
import java.util.Set;

public class WorkStandardService {

    private static final Set<PropertyClassification> ALLOWED_ITEM_CLASSES =
            EnumSet.of(PropertyClassification.제품, PropertyClassification.공정품);
    private static final Set<WorkDistinction> ALLOWED_WORK_DISTINCTIONS =
            EnumSet.of(WorkDistinction.INHOUSE, WorkDistinction.SPLIT);

    private final WorkStandardRepository workStandardRepository;
    private final ItemRepository itemRepository;
    private final ProcessRepository processRepository;
    private final WorkCenterLookup workCenterLookup;
    private final DomainEventStore domainEventStore;

    public WorkStandardService(
            WorkStandardRepository workStandardRepository,
            ItemRepository itemRepository,
            ProcessRepository processRepository,
            WorkCenterLookup workCenterLookup,
            DomainEventStore domainEventStore
    ) {
        this.workStandardRepository = workStandardRepository;
        this.itemRepository = itemRepository;
        this.processRepository = processRepository;
        this.workCenterLookup = workCenterLookup;
        this.domainEventStore = domainEventStore;
    }

    public WorkStandardView register(WorkStandardCommand command, String actorUserId) {
        ItemView item = getAllowedItem(command.itemId());
        ProcessView process = getAllowedProcess(command.processSequenceId(), command.itemId());
        validateOptionalReferences(command.equipmentId(), command.mainWorkerId());
        validateTimes(command.setupTime(), command.standardTime());
        validatePriority(command.priorityOrder());
        requireWorkCenter(command.workCenterId());

        if (workStandardRepository.existsActiveUk(
                command.itemId(), command.processSequenceId(), command.priorityOrder(), null)) {
            throw new IllegalArgumentException("동일 품목·공정·우선순위의 작업표준이 이미 존재합니다.");
        }

        long id = workStandardRepository.save(command, actorUserId);
        appendEvent(EventTypes.WORK_STANDARD_REGISTERED, id, item, process, command, actorUserId);
        return getActive(id);
    }

    public WorkStandardView update(long id, WorkStandardUpdateCommand command, String actorUserId) {
        WorkStandardView existing = getActive(id);
        validateOptionalReferences(command.equipmentId(), command.mainWorkerId());
        validateTimes(command.setupTime(), command.standardTime());
        validatePriority(command.priorityOrder());
        requireWorkCenter(command.workCenterId());

        if (workStandardRepository.existsActiveUk(
                existing.itemId(), existing.processSequenceId(), command.priorityOrder(), id)) {
            throw new IllegalArgumentException("동일 품목·공정·우선순위의 작업표준이 이미 존재합니다.");
        }

        workStandardRepository.update(id, command, actorUserId);
        ItemView item = getAllowedItem(existing.itemId());
        ProcessView process = getAllowedProcess(existing.processSequenceId(), existing.itemId());
        appendUpdateEvent(id, item, process, command, actorUserId);
        return getActive(id);
    }

    public void delete(long id, String actorUserId) {
        WorkStandardView existing = getActive(id);
        workStandardRepository.softDelete(id, actorUserId);
        domainEventStore.append(DomainEvent.create(
                EventTypes.WORK_STANDARD_DELETED,
                1,
                AggregateTypes.WORK_STANDARD,
                String.valueOf(id),
                actorUserId,
                """
                {"workStandardId":%d,"itemId":%d,"processSequenceId":%d}
                """.formatted(id, existing.itemId(), existing.processSequenceId()).trim()
        ));
    }

    public void cascadeDeleteByProcessSequenceId(long processSequenceId, String actorUserId) {
        workStandardRepository.softDeleteByProcessSequenceId(processSequenceId, actorUserId);
    }

    public List<WorkStandardView> listActive(String itemNumQuery) {
        return workStandardRepository.findAllActive(itemNumQuery);
    }

    public WorkStandardView getActive(long id) {
        return workStandardRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("작업표준을 찾을 수 없습니다: " + id));
    }

    public int copyStandards(String sourceItemNum, String targetItemNum, String actorUserId) {
        if (sourceItemNum.equals(targetItemNum)) {
            throw new IllegalArgumentException("원본과 대상 품목이 같을 수 없습니다.");
        }

        ItemView source = getAllowedItem(
                itemRepository.findActiveByItemNo(sourceItemNum)
                        .orElseThrow(() -> new IllegalArgumentException("원본 품목을 찾을 수 없습니다: " + sourceItemNum))
                        .id()
        );
        ItemView target = getAllowedItem(
                itemRepository.findActiveByItemNo(targetItemNum)
                        .orElseThrow(() -> new IllegalArgumentException("대상 품목을 찾을 수 없습니다: " + targetItemNum))
                        .id()
        );

        int copied = 0;
        for (WorkStandardView standard : workStandardRepository.findActiveByItemId(source.id())) {
            ProcessView sourceProcess = processRepository.findActiveById(standard.processSequenceId())
                    .orElseThrow(() -> new IllegalArgumentException("공정을 찾을 수 없습니다: " + standard.processSequenceId()));

            ProcessView targetProcess = processRepository.findActiveByItemIdAndPublicCodeIdAndSequence(
                            target.id(),
                            sourceProcess.processCodeId(),
                            sourceProcess.processSequenceNum(),
                            ProcessVariant.plan
                    )
                    .orElse(null);
            if (targetProcess == null || !ALLOWED_WORK_DISTINCTIONS.contains(targetProcess.workDistinction())) {
                continue;
            }

            if (workStandardRepository.existsActiveUk(
                    target.id(), targetProcess.id(), standard.priorityOrder(), null)) {
                continue;
            }

            register(
                    new WorkStandardCommand(
                            target.id(),
                            targetProcess.id(),
                            standard.workCenterId(),
                            standard.equipmentId(),
                            standard.priorityOrder(),
                            standard.mainWorkerId(),
                            standard.toolName(),
                            standard.setupTime(),
                            standard.standardTime()
                    ),
                    actorUserId
            );
            copied++;
        }
        return copied;
    }

    private ItemView getAllowedItem(long itemId) {
        ItemView item = itemRepository.findActiveById(itemId)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + itemId));
        if (!ALLOWED_ITEM_CLASSES.contains(item.propertyClassification())) {
            throw new IllegalArgumentException("제품·공정품만 작업표준을 등록할 수 있습니다.");
        }
        return item;
    }

    private ProcessView getAllowedProcess(long processSequenceId, long itemId) {
        ProcessView process = processRepository.findActiveById(processSequenceId)
                .orElseThrow(() -> new IllegalArgumentException("공정을 찾을 수 없습니다: " + processSequenceId));
        if (process.itemId() != itemId) {
            throw new IllegalArgumentException("선택한 공정이 품목과 일치하지 않습니다.");
        }
        if (!ALLOWED_WORK_DISTINCTIONS.contains(process.workDistinction())) {
            throw new IllegalArgumentException("외주 공정에는 작업표준을 등록할 수 없습니다.");
        }
        return process;
    }

    private void requireWorkCenter(long workCenterId) {
        if (!workCenterLookup.existsActive(workCenterId)) {
            throw new IllegalArgumentException("작업장을 찾을 수 없습니다: " + workCenterId);
        }
    }

    private void validateOptionalReferences(Long equipmentId, Long mainWorkerId) {
        if (equipmentId != null) {
            throw new IllegalArgumentException("설비 마스터는 아직 지원되지 않습니다. equipmentId는 비워 주세요.");
        }
        if (mainWorkerId != null) {
            throw new IllegalArgumentException("사용자 마스터는 아직 지원되지 않습니다. mainWorkerId는 비워 주세요.");
        }
    }

    private void validateTimes(int setupTime, int standardTime) {
        if (setupTime < 0) {
            throw new IllegalArgumentException("셋업시간은 0 이상이어야 합니다.");
        }
        if (standardTime < 0) {
            throw new IllegalArgumentException("표준시간은 0 이상이어야 합니다.");
        }
    }

    private void validatePriority(int priorityOrder) {
        if (priorityOrder < 1) {
            throw new IllegalArgumentException("우선순위는 1 이상이어야 합니다.");
        }
    }

    private void appendEvent(
            String eventType,
            long id,
            ItemView item,
            ProcessView process,
            WorkStandardCommand command,
            String actorUserId
    ) {
        domainEventStore.append(DomainEvent.create(
                eventType,
                1,
                AggregateTypes.WORK_STANDARD,
                String.valueOf(id),
                actorUserId,
                """
                {"workStandardId":%d,"itemId":%d,"itemNo":"%s","processSequenceId":%d,"priorityOrder":%d}
                """.formatted(
                        id,
                        item.id(),
                        escape(item.itemNo()),
                        process.id(),
                        command.priorityOrder()
                ).trim()
        ));
    }

    private void appendUpdateEvent(
            long id,
            ItemView item,
            ProcessView process,
            WorkStandardUpdateCommand command,
            String actorUserId
    ) {
        domainEventStore.append(DomainEvent.create(
                EventTypes.WORK_STANDARD_UPDATED,
                1,
                AggregateTypes.WORK_STANDARD,
                String.valueOf(id),
                actorUserId,
                """
                {"workStandardId":%d,"itemId":%d,"itemNo":"%s","processSequenceId":%d,"priorityOrder":%d}
                """.formatted(
                        id,
                        item.id(),
                        escape(item.itemNo()),
                        process.id(),
                        command.priorityOrder()
                ).trim()
        ));
    }

    private static String escape(String value) {
        return value.replace("\\", "\\\\").replace("\"", "\\\"");
    }
}
