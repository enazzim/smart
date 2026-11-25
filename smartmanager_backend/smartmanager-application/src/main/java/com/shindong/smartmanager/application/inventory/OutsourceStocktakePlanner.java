package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.application.bom.ItemCompositionRepository;
import com.shindong.smartmanager.application.bom.ItemCompositionView;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.application.unitprice.UnitPriceView;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.List;
import java.util.Optional;
import java.util.Set;

/**
 * 외주단가의 투입 슬롯을 조회만 한다. 공정 마스터를 만들지 않는다.
 */
public class OutsourceStocktakePlanner {

    static final String MATERIAL_PROCESS_CODE = "14000000";

    private final ProcessRepository processRepository;
    private final ItemCompositionRepository itemCompositionRepository;
    private final ItemRepository itemRepository;

    public OutsourceStocktakePlanner(
            ProcessRepository processRepository,
            ItemCompositionRepository itemCompositionRepository,
            ItemRepository itemRepository
    ) {
        this.processRepository = processRepository;
        this.itemCompositionRepository = itemCompositionRepository;
        this.itemRepository = itemRepository;
    }

    public List<OutsourceStocktakeSlot> plan(UnitPriceView price) {
        if (price.businessRegNo() == null || price.businessRegNo().isBlank() || price.companyId() <= 0) {
            return List.of(error(price, price.itemId(), price.itemNo(), price.itemName(), "외주단가 거래처를 정할 수 없습니다."));
        }
        if (price.beginProcessCodeId() == null) {
            return List.of(error(price, price.itemId(), price.itemNo(), price.itemName(), "외주단가 시작공정을 정할 수 없습니다."));
        }
        List<ProcessView> processes = sorted(price.itemId());
        if (processes.isEmpty()) {
            return bomSlots(price, price.itemId(), new HashSet<>());
        }
        Optional<Short> begin = sequenceOf(processes, price.beginProcessCodeId());
        if (begin.isEmpty()) {
            return List.of(error(price, price.itemId(), price.itemNo(), price.itemName(), "품목 공정 계획에 시작공정이 없습니다."));
        }
        short first = processes.get(0).processSequenceNum();
        if (begin.get() == first) {
            return bomSlots(price, price.itemId(), new HashSet<>());
        }
        return processes.stream()
                .filter(process -> process.processSequenceNum() < begin.get())
                .max(Comparator.comparingInt(ProcessView::processSequenceNum))
                .map(process -> List.of(slot(price, process.itemId(), process)))
                .orElseGet(() -> bomSlots(price, price.itemId(), new HashSet<>()));
    }

    private List<OutsourceStocktakeSlot> bomSlots(UnitPriceView price, long parentItemId, Set<Long> visited) {
        if (!visited.add(parentItemId)) {
            return List.of();
        }
        List<OutsourceStocktakeSlot> slots = new ArrayList<>();
        for (ItemCompositionView line : itemCompositionRepository.findActiveByParentItemId(parentItemId)) {
            ItemView child = itemRepository.findActiveById(line.childItemId()).orElse(null);
            if (child == null) {
                slots.add(error(price, line.childItemId(), line.childItemNo(), line.childItemName(), "BOM 자품목을 찾을 수 없습니다."));
                continue;
            }
            if (child.propertyClassification() == PropertyClassification.원자재) {
                ProcessView material = materialProcess(child.id()).orElse(null);
                if (material == null) {
                    slots.add(error(price, child.id(), child.itemNo(), child.itemName(), "자재 공정이 없습니다."));
                } else {
                    slots.add(slot(price, child.id(), material));
                }
            } else if (child.propertyClassification() == PropertyClassification.공정품) {
                List<OutsourceStocktakeSlot> prior = priorInhouse(price, child);
                if (!prior.isEmpty()) {
                    slots.addAll(prior);
                } else {
                    slots.addAll(bomSlots(price, child.id(), visited));
                }
            }
        }
        return dedupe(slots);
    }

    private List<OutsourceStocktakeSlot> priorInhouse(UnitPriceView price, ItemView child) {
        List<ProcessView> processes = sorted(child.id());
        Optional<Short> begin = sequenceOf(processes, price.beginProcessCodeId());
        if (begin.isEmpty()) {
            return List.of(error(price, child.id(), child.itemNo(), child.itemName(), "하위 공정품의 시작공정을 정할 수 없습니다."));
        }
        List<OutsourceStocktakeSlot> slots = new ArrayList<>();
        for (ProcessView process : processes) {
            if (process.processSequenceNum() >= begin.get()) {
                continue;
            }
            if (process.workDistinction() == WorkDistinction.OUTSOURCE) {
                continue;
            }
            slots.add(slot(price, child.id(), process));
        }
        return slots;
    }

    private Optional<ProcessView> materialProcess(long itemId) {
        return sorted(itemId).stream()
                .filter(process -> MATERIAL_PROCESS_CODE.equals(process.processCode()))
                .findFirst();
    }

    private List<ProcessView> sorted(long itemId) {
        return processRepository.findAllActiveByItemId(itemId, ProcessVariant.plan).stream()
                .sorted(Comparator.comparingInt(ProcessView::processSequenceNum))
                .toList();
    }

    private static Optional<Short> sequenceOf(List<ProcessView> processes, Long processCodeId) {
        if (processCodeId == null) {
            return Optional.empty();
        }
        return processes.stream()
                .filter(process -> process.processCodeId() == processCodeId)
                .map(ProcessView::processSequenceNum)
                .findFirst();
    }

    private OutsourceStocktakeSlot slot(UnitPriceView price, long itemId, ProcessView process) {
        ItemView item = itemRepository.findActiveById(itemId).orElse(null);
        return new OutsourceStocktakeSlot(
                price.companyId(),
                price.businessRegNo(),
                price.companyName(),
                itemId,
                item != null ? item.itemNo() : process.itemNo(),
                item != null ? item.itemName() : process.itemName(),
                item != null && item.propertyClassification() != null ? item.propertyClassification().name() : null,
                item != null && item.lotTracked(),
                process.id(),
                process.processName(),
                null
        );
    }

    private static OutsourceStocktakeSlot error(
            UnitPriceView price,
            long itemId,
            String itemNo,
            String itemName,
            String message
    ) {
        return new OutsourceStocktakeSlot(
                price.companyId(),
                price.businessRegNo(),
                price.companyName(),
                itemId,
                itemNo,
                itemName,
                null,
                false,
                null,
                null,
                message
        );
    }

    private static List<OutsourceStocktakeSlot> dedupe(List<OutsourceStocktakeSlot> slots) {
        return new ArrayList<>(new LinkedHashSet<>(slots));
    }
}
