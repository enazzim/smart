package com.shindong.smartmanager.application.unitprice;

import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.process.ProcessCodeLookup;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.domain.company.CompanyRoleType;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.pricing.CostType;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import java.math.BigDecimal;
import java.util.EnumSet;
import java.util.List;
import java.util.Set;

public class UnitPriceService {

    private static final BigDecimal MAX_ORDER_RATE = new BigDecimal("100");
    private static final Set<PropertyClassification> PURCHASE_ITEM_CLASSES =
            EnumSet.of(PropertyClassification.원자재, PropertyClassification.상품, PropertyClassification.부자재);
    private static final Set<PropertyClassification> SALE_ITEM_CLASSES =
            EnumSet.of(PropertyClassification.제품, PropertyClassification.상품, PropertyClassification.공정품);
    private static final Set<PropertyClassification> OUTSOURCE_ITEM_CLASSES =
            EnumSet.of(PropertyClassification.제품, PropertyClassification.공정품);

    private final UnitPriceRepository unitPriceRepository;
    private final ItemRepository itemRepository;
    private final CompanyRepository companyRepository;
    private final ProcessRepository processRepository;
    private final ProcessCodeLookup processCodeLookup;
    private final OutsourceInputBalanceProjector outsourceInputBalanceProjector;
    private final UnitPriceHistoryProjector unitPriceHistoryProjector;
    private final DomainEventStore domainEventStore;

    public UnitPriceService(
            UnitPriceRepository unitPriceRepository,
            ItemRepository itemRepository,
            CompanyRepository companyRepository,
            ProcessRepository processRepository,
            ProcessCodeLookup processCodeLookup,
            OutsourceInputBalanceProjector outsourceInputBalanceProjector,
            UnitPriceHistoryProjector unitPriceHistoryProjector,
            DomainEventStore domainEventStore
    ) {
        this.unitPriceRepository = unitPriceRepository;
        this.itemRepository = itemRepository;
        this.companyRepository = companyRepository;
        this.processRepository = processRepository;
        this.processCodeLookup = processCodeLookup;
        this.outsourceInputBalanceProjector = outsourceInputBalanceProjector;
        this.unitPriceHistoryProjector = unitPriceHistoryProjector;
        this.domainEventStore = domainEventStore;
    }

    public UnitPriceView register(UnitPriceCommand command, String actorUserId) {
        return register(command, actorUserId, UnitPriceHistoryProjector.REASON_NORMAL_REGISTER);
    }

    public UnitPriceView register(UnitPriceCommand command, String actorUserId, String historyReason) {
        validateCommand(command, null);
        ItemView item = resolveItem(command.itemId(), command.costType());
        validateCompanyRole(command.companyId(), command.costType());

        if (unitPriceRepository.existsActiveUk(
                command.costType(),
                command.itemId(),
                command.companyId(),
                command.beginDate(),
                command.beginProcessCodeId(),
                command.endProcessCodeId(),
                null)) {
            throw new IllegalArgumentException("동일 내용키의 단가가 이미 존재합니다.");
        }

        unitPriceRepository.lockActiveRowsForOrderRateValidation(command.costType(), command.itemId());
        validateOrderRate(command.costType(), command.itemId(), command.orderRate(), command, null);

        if (command.costType() == CostType.OUTSOURCE) {
            OutsourceUnitPriceContext context = toOutsourceContext(command);
            validateOutsourceProcessRange(context);
        }

        long id = unitPriceRepository.save(command, actorUserId);

        if (command.costType() == CostType.OUTSOURCE) {
            outsourceInputBalanceProjector.ensure(toOutsourceContext(command), actorUserId);
        }

        String reason = historyReason == null || historyReason.isBlank()
                ? UnitPriceHistoryProjector.REASON_NORMAL_REGISTER
                : historyReason.trim();
        unitPriceHistoryProjector.appendHistory(id, reason, actorUserId);
        appendRegisteredEvent(id, command, item, actorUserId);

        return getActive(id);
    }

    public UnitPriceView update(long id, UnitPriceUpdateCommand command, String actorUserId) {
        UnitPriceView existing = getActive(id);
        if (command.updateReason() == null || command.updateReason().isBlank()) {
            throw new IllegalArgumentException("변경 사유는 필수입니다.");
        }

        validateUpdateValues(existing.costType(), command);

        unitPriceRepository.lockActiveRowsForOrderRateValidation(existing.costType(), existing.itemId());
        validateOrderRate(
                existing.costType(),
                existing.itemId(),
                command.orderRate(),
                toCommand(existing),
                id
        );

        OutsourceUnitPriceContext oldContext = existing.costType() == CostType.OUTSOURCE
                ? toOutsourceContext(existing)
                : null;

        unitPriceRepository.update(id, command, actorUserId);

        if (existing.costType() == CostType.OUTSOURCE) {
            OutsourceUnitPriceContext newContext = toOutsourceContext(getActive(id));
            outsourceInputBalanceProjector.rebuild(oldContext, newContext, actorUserId);
        }

        unitPriceHistoryProjector.appendHistory(id, command.updateReason().trim(), actorUserId);
        appendUpdatedEvent(id, existing, command, actorUserId);

        return getActive(id);
    }

    public void delete(long id, String actorUserId) {
        UnitPriceView existing = getActive(id);

        if (existing.costType() == CostType.OUTSOURCE) {
            outsourceInputBalanceProjector.deactivate(toOutsourceContext(existing), actorUserId);
        }

        unitPriceHistoryProjector.appendHistory(id, UnitPriceHistoryProjector.REASON_DELETE, actorUserId);
        unitPriceRepository.softDelete(id, actorUserId);
        appendDeletedEvent(id, existing, actorUserId);
    }

    public List<UnitPriceView> listActive(CostType costType, String query) {
        return unitPriceRepository.findAllActiveByCostType(costType, query);
    }

    public UnitPriceView getActive(long id) {
        return unitPriceRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("단가를 찾을 수 없습니다: " + id));
    }

    public UnitPriceView getActiveByItemNo(String itemNo, CostType costType) {
        return unitPriceRepository.findActiveByItemNoAndCostType(itemNo, costType)
                .orElseThrow(() -> new IllegalArgumentException("품목 단가를 찾을 수 없습니다: " + itemNo));
    }

    public List<UnitPriceChangeLogView> listHistory(long id) {
        getActive(id);
        return unitPriceRepository.findChangeLogs(id);
    }

    public List<UnitPriceChangeLogView> listAllHistory(UnitPriceHistorySearchQuery query) {
        if (query.changedFrom() != null
                && query.changedTo() != null
                && query.changedTo().isBefore(query.changedFrom())) {
            throw new IllegalArgumentException("수정일 종료일은 시작일 이후여야 합니다.");
        }
        return unitPriceRepository.findChangeLogs(query);
    }

    private void validateCommand(UnitPriceCommand command, Long excludeId) {
        if (command.costType() == null) {
            throw new IllegalArgumentException("단가 구분은 필수입니다.");
        }
        if (command.standardUnitCost() == null || command.standardUnitCost().signum() < 0) {
            throw new IllegalArgumentException("기준단가는 0 이상이어야 합니다.");
        }
        if (command.beginDate() == null) {
            throw new IllegalArgumentException("적용시작일은 필수입니다.");
        }
        if (command.endDate() != null && command.endDate().isBefore(command.beginDate())) {
            throw new IllegalArgumentException("적용종료일은 적용시작일 이후여야 합니다.");
        }

        switch (command.costType()) {
            case SALE -> {
                if (command.beginProcessCodeId() != null || command.endProcessCodeId() != null) {
                    throw new IllegalArgumentException("판매단가는 공정을 지정할 수 없습니다.");
                }
            }
            case PURCHASE -> {
                if (command.beginProcessCodeId() != null || command.endProcessCodeId() != null) {
                    throw new IllegalArgumentException("구매단가는 공정을 지정할 수 없습니다.");
                }
                if (command.orderRate() == null) {
                    throw new IllegalArgumentException("발주비율은 필수입니다.");
                }
            }
            case OUTSOURCE -> {
                if (command.beginProcessCodeId() == null || command.endProcessCodeId() == null) {
                    throw new IllegalArgumentException("외주단가는 시작·종료 공정이 필수입니다.");
                }
                if (command.orderRate() == null) {
                    throw new IllegalArgumentException("발주비율은 필수입니다.");
                }
                requireProcessCode(command.beginProcessCodeId());
                requireProcessCode(command.endProcessCodeId());
            }
        }
    }

    private void validateUpdateValues(CostType costType, UnitPriceUpdateCommand command) {
        if (command.standardUnitCost() == null || command.standardUnitCost().signum() < 0) {
            throw new IllegalArgumentException("기준단가는 0 이상이어야 합니다.");
        }
        if (command.beginDate() == null) {
            throw new IllegalArgumentException("적용시작일은 필수입니다.");
        }
        if (command.endDate() != null && command.endDate().isBefore(command.beginDate())) {
            throw new IllegalArgumentException("적용종료일은 적용시작일 이후여야 합니다.");
        }
        if (costType == CostType.SALE) {
            if (command.orderRate() == null || command.orderRate().compareTo(BigDecimal.ZERO) != 0) {
                throw new IllegalArgumentException("판매단가의 발주비율은 0이어야 합니다.");
            }
        } else if (command.orderRate() == null) {
            throw new IllegalArgumentException("발주비율은 필수입니다.");
        }
    }

    private void validateOrderRate(
            CostType costType,
            long itemId,
            BigDecimal orderRate,
            UnitPriceCommand context,
            Long excludeId
    ) {
        if (costType == CostType.SALE) {
            return;
        }

        BigDecimal total = unitPriceRepository.sumActiveOrderRate(costType, itemId, excludeId)
                .add(orderRate != null ? orderRate : BigDecimal.ZERO);
        if (total.compareTo(MAX_ORDER_RATE) > 0) {
            throw new IllegalArgumentException("동일 품목의 발주비율 합계는 100%를 초과할 수 없습니다.");
        }

        if (costType == CostType.OUTSOURCE) {
            BigDecimal segmentTotal = unitPriceRepository.sumActiveOrderRateForOutsourceSegment(
                    itemId,
                    context.beginProcessCodeId(),
                    context.endProcessCodeId(),
                    excludeId
            ).add(orderRate != null ? orderRate : BigDecimal.ZERO);
            if (segmentTotal.compareTo(MAX_ORDER_RATE) > 0) {
                throw new IllegalArgumentException("동일 공정구간의 발주비율 합계는 100%를 초과할 수 없습니다.");
            }
        }
    }

    private ItemView resolveItem(long itemId, CostType costType) {
        ItemView item = itemRepository.findActiveById(itemId)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + itemId));

        Set<PropertyClassification> allowed = switch (costType) {
            case PURCHASE -> PURCHASE_ITEM_CLASSES;
            case SALE -> SALE_ITEM_CLASSES;
            case OUTSOURCE -> OUTSOURCE_ITEM_CLASSES;
        };

        if (!allowed.contains(item.propertyClassification())) {
            String allowedLabel = switch (costType) {
                case PURCHASE -> "원자재·상품·부자재";
                case SALE -> "제품·상품·공정품";
                case OUTSOURCE -> "제품·공정품";
            };
            throw new IllegalArgumentException(
                    "해당 단가 구분에 허용되지 않는 자산분류입니다. "
                            + costType.name() + "은(는) " + allowedLabel + "만 가능합니다. (현재: "
                            + item.propertyClassification().name() + ")"
            );
        }

        if (costType == CostType.OUTSOURCE) {
            // 외주단가: 작업구분이 외주(OUTSOURCE)·혼합(SPLIT)인 공정이 1건 이상 있어야 함.
            // 자가(INHOUSE)만 있는 품목은 외주단가 등록 불가.
            boolean hasOutsourceOrSplit = processRepository.findAllActiveByItemId(itemId, ProcessVariant.plan)
                    .stream()
                    .anyMatch(process -> process.workDistinction() == WorkDistinction.OUTSOURCE
                            || process.workDistinction() == WorkDistinction.SPLIT);
            if (!hasOutsourceOrSplit) {
                throw new IllegalArgumentException(
                        "외주단가는 외주·혼합(자가/외주) 공정이 1건 이상 등록된 품목만 가능합니다.");
            }
        }

        return item;
    }

    private void validateCompanyRole(long companyId, CostType costType) {
        companyRepository.findActiveById(companyId)
                .orElseThrow(() -> new IllegalArgumentException("거래처를 찾을 수 없습니다: " + companyId));

        CompanyRoleType requiredRole = switch (costType) {
            case SALE -> CompanyRoleType.SALES;
            case PURCHASE -> CompanyRoleType.PURCHASE;
            case OUTSOURCE -> CompanyRoleType.OUTSOURCE;
        };

        if (!companyRepository.findRoles(companyId).contains(requiredRole)) {
            throw new IllegalArgumentException("거래처 역할이 단가 구분과 일치하지 않습니다.");
        }
    }

    private void validateOutsourceProcessRange(OutsourceUnitPriceContext context) {
        List<ProcessView> processes = processRepository.findAllActiveByItemId(context.itemId(), ProcessVariant.plan);
        ProcessView begin = findProcess(processes, context.beginProcessCodeId(), "시작공정");
        ProcessView end = findProcess(processes, context.endProcessCodeId(), "종료공정");
        if (begin.processSequenceNum() > end.processSequenceNum()) {
            throw new IllegalArgumentException("시작공정 순번은 종료공정 순번 이하여야 합니다.");
        }
        requireOutsourceEligibleProcess(begin, "시작공정");
        requireOutsourceEligibleProcess(end, "종료공정");
    }

    private void requireOutsourceEligibleProcess(ProcessView process, String label) {
        WorkDistinction distinction = process.workDistinction();
        if (distinction != WorkDistinction.OUTSOURCE && distinction != WorkDistinction.SPLIT) {
            throw new IllegalArgumentException(
                    label + "은(는) 외주 또는 혼합(자가/외주) 공정만 지정할 수 있습니다. (자가 공정은 외주단가 불가)");
        }
    }

    private ProcessView findProcess(List<ProcessView> processes, long processCodeId, String label) {
        return processes.stream()
                .filter(process -> process.processCodeId() == processCodeId)
                .findFirst()
                .orElseThrow(() -> new IllegalArgumentException("품목 공정 계획에 " + label + "이 없습니다: " + processCodeId));
    }

    private short findSequence(List<ProcessView> processes, long processCodeId) {
        return findProcess(processes, processCodeId, "공정").processSequenceNum();
    }

    private void requireProcessCode(long processCodeId) {
        processCodeLookup.findActiveProcessCode(processCodeId)
                .orElseThrow(() -> new IllegalArgumentException("공정코드를 찾을 수 없습니다: " + processCodeId));
    }

    private OutsourceUnitPriceContext toOutsourceContext(UnitPriceCommand command) {
        return new OutsourceUnitPriceContext(
                command.itemId(),
                command.companyId(),
                command.beginProcessCodeId(),
                command.endProcessCodeId()
        );
    }

    private OutsourceUnitPriceContext toOutsourceContext(UnitPriceView view) {
        return new OutsourceUnitPriceContext(
                view.itemId(),
                view.companyId(),
                view.beginProcessCodeId(),
                view.endProcessCodeId()
        );
    }

    private UnitPriceCommand toCommand(UnitPriceView view) {
        return new UnitPriceCommand(
                view.costType(),
                view.itemId(),
                view.companyId(),
                view.beginProcessCodeId(),
                view.endProcessCodeId(),
                view.orderRate(),
                view.standardUnitCost(),
                view.discountUnitCost(),
                view.beginDate(),
                view.endDate()
        );
    }

    private void appendRegisteredEvent(long id, UnitPriceCommand command, ItemView item, String actorUserId) {
        domainEventStore.append(DomainEvent.create(
                registeredEventType(command.costType()),
                1,
                AggregateTypes.UNIT_PRICE,
                String.valueOf(id),
                actorUserId,
                buildPayload(id, command, item.itemNo())
        ));
    }

    private void appendUpdatedEvent(
            long id,
            UnitPriceView existing,
            UnitPriceUpdateCommand command,
            String actorUserId
    ) {
        domainEventStore.append(DomainEvent.create(
                updatedEventType(existing.costType()),
                1,
                AggregateTypes.UNIT_PRICE,
                String.valueOf(id),
                actorUserId,
                """
                {"unitPriceId":%d,"orderRate":%s,"standardUnitCost":%s,"updateReason":"%s"}
                """.formatted(
                        id,
                        command.orderRate(),
                        command.standardUnitCost(),
                        escape(command.updateReason().trim())
                ).trim()
        ));
    }

    private void appendDeletedEvent(long id, UnitPriceView existing, String actorUserId) {
        domainEventStore.append(DomainEvent.create(
                deletedEventType(existing.costType()),
                1,
                AggregateTypes.UNIT_PRICE,
                String.valueOf(id),
                actorUserId,
                """
                {"unitPriceId":%d,"itemId":%d,"companyId":%d,"costType":"%s"}
                """.formatted(id, existing.itemId(), existing.companyId(), existing.costType().name()).trim()
        ));
    }

    private String buildPayload(long id, UnitPriceCommand command, String itemNo) {
        return """
                {"unitPriceId":%d,"costType":"%s","itemId":%d,"itemNo":"%s","companyId":%d,"beginProcessCodeId":%s,"endProcessCodeId":%s}
                """.formatted(
                id,
                command.costType().name(),
                command.itemId(),
                escape(itemNo),
                command.companyId(),
                command.beginProcessCodeId(),
                command.endProcessCodeId()
        ).trim();
    }

    private String registeredEventType(CostType costType) {
        return switch (costType) {
            case SALE -> EventTypes.SALE_UNIT_PRICE_REGISTERED;
            case PURCHASE -> EventTypes.PURCHASE_UNIT_PRICE_REGISTERED;
            case OUTSOURCE -> EventTypes.OUTSOURCE_UNIT_PRICE_REGISTERED;
        };
    }

    private String updatedEventType(CostType costType) {
        return switch (costType) {
            case SALE -> EventTypes.SALE_UNIT_PRICE_UPDATED;
            case PURCHASE -> EventTypes.PURCHASE_UNIT_PRICE_UPDATED;
            case OUTSOURCE -> EventTypes.OUTSOURCE_UNIT_PRICE_UPDATED;
        };
    }

    private String deletedEventType(CostType costType) {
        return switch (costType) {
            case SALE -> EventTypes.SALE_UNIT_PRICE_DELETED;
            case PURCHASE -> EventTypes.PURCHASE_UNIT_PRICE_DELETED;
            case OUTSOURCE -> EventTypes.OUTSOURCE_UNIT_PRICE_DELETED;
        };
    }

    private String escape(String value) {
        return value.replace("\\", "\\\\").replace("\"", "\\\"");
    }
}
