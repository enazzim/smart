package com.shindong.smartmanager.application.unitprice;

import com.shindong.smartmanager.application.bom.ItemCompositionRepository;
import com.shindong.smartmanager.application.bom.ItemCompositionView;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.process.InventoryBalanceRepository;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import java.time.Year;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.List;
import java.util.Set;

/**
 * Ref: docs/step0/domain-event-projector-matrix.md §4.5
 */
public class OutsourceInputBalanceProjector {

    static final String NO_INPUT_MATERIALS_MESSAGE = "하위 출고 공정 또는 원자재를 찾을 수 없음";

    private final ProcessRepository processRepository;
    private final InventoryBalanceRepository inventoryBalanceRepository;
    private final ItemCompositionRepository itemCompositionRepository;
    private final ItemRepository itemRepository;

    public OutsourceInputBalanceProjector(
            ProcessRepository processRepository,
            InventoryBalanceRepository inventoryBalanceRepository,
            ItemCompositionRepository itemCompositionRepository,
            ItemRepository itemRepository
    ) {
        this.processRepository = processRepository;
        this.inventoryBalanceRepository = inventoryBalanceRepository;
        this.itemCompositionRepository = itemCompositionRepository;
        this.itemRepository = itemRepository;
    }

    public void ensure(OutsourceUnitPriceContext context, String actorUserId) {
        List<OutsourceInputSlot> slots = resolveInputSlots(context, actorUserId);
        if (slots.isEmpty()) {
            throw new IllegalArgumentException(NO_INPUT_MATERIALS_MESSAGE);
        }

        int fiscalYear = Year.now().getValue();
        for (OutsourceInputSlot slot : slots) {
            inventoryBalanceRepository.ensureOutsourceInputBalance(
                    slot.itemId(),
                    fiscalYear,
                    context.companyId(),
                    slot.inputProcessId(),
                    actorUserId
            );
        }
    }

    public void rebuild(OutsourceUnitPriceContext oldContext, OutsourceUnitPriceContext newContext, String actorUserId) {
        List<OutsourceInputSlot> newSlots = resolveInputSlots(newContext, actorUserId);
        if (newSlots.isEmpty()) {
            throw new IllegalArgumentException(NO_INPUT_MATERIALS_MESSAGE);
        }

        for (OutsourceInputSlot slot : resolveInputSlots(oldContext, actorUserId)) {
            inventoryBalanceRepository.deactivateOutsourceInputBalance(
                    slot.itemId(),
                    oldContext.companyId(),
                    slot.inputProcessId(),
                    actorUserId
            );
        }

        int fiscalYear = Year.now().getValue();
        for (OutsourceInputSlot slot : newSlots) {
            inventoryBalanceRepository.ensureOutsourceInputBalance(
                    slot.itemId(),
                    fiscalYear,
                    newContext.companyId(),
                    slot.inputProcessId(),
                    actorUserId
            );
        }
    }

    public void deactivate(OutsourceUnitPriceContext context, String actorUserId) {
        for (OutsourceInputSlot slot : resolveInputSlots(context, actorUserId)) {
            inventoryBalanceRepository.deactivateOutsourceInputBalance(
                    slot.itemId(),
                    context.companyId(),
                    slot.inputProcessId(),
                    actorUserId
            );
        }
    }

    public List<OutsourceInputSlot> resolveInputSlots(OutsourceUnitPriceContext context, String actorUserId) {
        List<ProcessView> processes = processRepository
                .findAllActiveByItemId(context.itemId(), ProcessVariant.plan)
                .stream()
                .sorted(Comparator.comparingInt(ProcessView::processSequenceNum))
                .toList();
        if (processes.isEmpty()) {
            return resolveBomSlots(context.itemId(), context, actorUserId, new HashSet<>());
        }

        short beginSequence = findSequence(processes, context.beginProcessCodeId(), "시작공정");
        short firstSequence = processes.get(0).processSequenceNum();
        if (beginSequence == firstSequence) {
            return resolveBomSlots(context.itemId(), context, actorUserId, new HashSet<>());
        }

        return processes.stream()
                .filter(process -> process.processSequenceNum() < beginSequence)
                .max(Comparator.comparingInt(ProcessView::processSequenceNum))
                .map(process -> List.of(new OutsourceInputSlot(context.itemId(), process.id())))
                .orElseGet(() -> resolveBomSlots(context.itemId(), context, actorUserId, new HashSet<>()));
    }

    private List<OutsourceInputSlot> resolvePriorInhouseProcessSlots(OutsourceUnitPriceContext context) {
        return resolvePriorInhouseProcessSlotsForItem(context.itemId(), context);
    }

    private List<OutsourceInputSlot> resolvePriorInhouseProcessSlotsForItem(
            long itemId,
            OutsourceUnitPriceContext context
    ) {
        List<ProcessView> processes = processRepository
                .findAllActiveByItemId(itemId, ProcessVariant.plan)
                .stream()
                .sorted(Comparator.comparingInt(ProcessView::processSequenceNum))
                .toList();

        short beginSequence = findSequence(processes, context.beginProcessCodeId(), "시작공정");

        List<OutsourceInputSlot> slots = new ArrayList<>();
        for (ProcessView process : processes) {
            if (process.processSequenceNum() >= beginSequence) {
                continue;
            }
            if (process.workDistinction() == WorkDistinction.OUTSOURCE) {
                continue;
            }
            slots.add(new OutsourceInputSlot(itemId, process.id()));
        }

        List<OutsourceInputSlot> reversed = new ArrayList<>(slots);
        java.util.Collections.reverse(reversed);
        return dedupe(reversed);
    }

    private List<OutsourceInputSlot> resolveBomSlots(
            long parentItemId,
            OutsourceUnitPriceContext context,
            String actorUserId,
            Set<Long> visited
    ) {
        if (!visited.add(parentItemId)) {
            return List.of();
        }

        List<OutsourceInputSlot> slots = new ArrayList<>();
        for (ItemCompositionView bomLine : itemCompositionRepository.findActiveByParentItemId(parentItemId)) {
            ItemView child = itemRepository.findActiveById(bomLine.childItemId())
                    .orElseThrow(() -> new IllegalArgumentException("BOM 자품목을 찾을 수 없습니다: " + bomLine.childItemId()));

            if (child.propertyClassification() == PropertyClassification.원자재) {
                long materialProcessId = processRepository.ensureMaterialProcess(child.id(), actorUserId);
                slots.add(new OutsourceInputSlot(child.id(), materialProcessId));
            } else if (child.propertyClassification() == PropertyClassification.공정품) {
                List<OutsourceInputSlot> childProcessSlots =
                        resolvePriorInhouseProcessSlotsForItem(child.id(), context);
                if (!childProcessSlots.isEmpty()) {
                    slots.addAll(childProcessSlots);
                } else {
                    slots.addAll(resolveBomSlots(child.id(), context, actorUserId, visited));
                }
            }
        }
        return dedupe(slots);
    }

    private List<OutsourceInputSlot> dedupe(List<OutsourceInputSlot> slots) {
        return new ArrayList<>(new LinkedHashSet<>(slots));
    }

    private short findSequence(List<ProcessView> processes, long processCodeId, String label) {
        return processes.stream()
                .filter(process -> process.processCodeId() == processCodeId)
                .map(ProcessView::processSequenceNum)
                .findFirst()
                .orElseThrow(() -> new IllegalArgumentException(
                        "품목 공정 계획에 " + label + "이 없습니다: " + processCodeId));
    }
}
