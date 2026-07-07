package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.application.bom.ItemCompositionRepository;
import com.shindong.smartmanager.application.bom.ItemCompositionView;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.application.unitprice.OutsourceInputBalanceProjector;
import com.shindong.smartmanager.application.unitprice.OutsourceInputSlot;
import com.shindong.smartmanager.application.unitprice.OutsourceUnitPriceContext;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.function.Function;
import java.util.stream.Collectors;

public class OutsourcingShipmentConsumptionCalculator {

    private static final int QTY_SCALE = 4;
    private static final RoundingMode QTY_ROUNDING = RoundingMode.HALF_UP;
    private static final String LOCATION_RAW = "RAW";
    private static final String LOCATION_WIP = "WIP";

    private final OutsourceInputBalanceProjector outsourceInputBalanceProjector;
    private final ItemCompositionRepository itemCompositionRepository;
    private final ItemRepository itemRepository;
    private final ProcessRepository processRepository;

    public OutsourcingShipmentConsumptionCalculator(
            OutsourceInputBalanceProjector outsourceInputBalanceProjector,
            ItemCompositionRepository itemCompositionRepository,
            ItemRepository itemRepository,
            ProcessRepository processRepository
    ) {
        this.outsourceInputBalanceProjector = outsourceInputBalanceProjector;
        this.itemCompositionRepository = itemCompositionRepository;
        this.itemRepository = itemRepository;
        this.processRepository = processRepository;
    }

    public List<OutsourcingShipmentInputSaveCommand> calculateInputLines(
            OutsourcingOrderView order,
            OutsourcingOrderLineView orderLine,
            BigDecimal shipmentQty,
            String actorUserId
    ) {
        return calculateForParent(
                orderLine.itemId(),
                order.partnerId(),
                orderLine.beginProcessCodeId(),
                orderLine.endProcessCodeId(),
                shipmentQty,
                actorUserId
        );
    }

    public List<OutsourcingShipmentInputSaveCommand> calculateAdvanceInputLines(
            long parentItemId,
            long partnerId,
            long beginProcessCodeId,
            long endProcessCodeId,
            BigDecimal referenceQty,
            String actorUserId
    ) {
        return calculateForParent(
                parentItemId,
                partnerId,
                beginProcessCodeId,
                endProcessCodeId,
                referenceQty,
                actorUserId
        );
    }

    private List<OutsourcingShipmentInputSaveCommand> calculateForParent(
            long parentItemId,
            long partnerId,
            long beginProcessCodeId,
            long endProcessCodeId,
            BigDecimal shipmentQty,
            String actorUserId
    ) {
        if (shipmentQty == null || shipmentQty.compareTo(BigDecimal.ZERO) <= 0) {
            throw new IllegalArgumentException("출고수량은 0보다 커야 합니다.");
        }

        OutsourceUnitPriceContext context = new OutsourceUnitPriceContext(
                parentItemId,
                partnerId,
                beginProcessCodeId,
                endProcessCodeId
        );
        List<OutsourceInputSlot> slots = outsourceInputBalanceProjector.resolveInputSlots(context, actorUserId);
        if (slots.isEmpty()) {
            throw new IllegalArgumentException("하위 출고 공정 또는 원자재를 찾을 수 없음");
        }

        Map<Long, ItemCompositionView> bomByChildId = itemCompositionRepository
                .findActiveByParentItemId(parentItemId).stream()
                .collect(Collectors.toMap(ItemCompositionView::childItemId, Function.identity(), (left, right) -> left));

        List<OutsourcingShipmentInputSaveCommand> result = new ArrayList<>();
        for (OutsourceInputSlot slot : slots) {
            ItemView item = itemRepository.findActiveById(slot.itemId())
                    .orElseThrow(() -> new IllegalArgumentException("투입 품목을 찾을 수 없습니다: " + slot.itemId()));

            BigDecimal issueQty;
            String locationCode;
            Long sourceProcessId;
            Long itemCompositionId = null;

            if (slot.itemId() == parentItemId) {
                issueQty = shipmentQty.setScale(QTY_SCALE, QTY_ROUNDING);
                locationCode = LOCATION_WIP;
                sourceProcessId = slot.inputProcessId();
            } else {
                ItemCompositionView bomLine = bomByChildId.get(slot.itemId());
                if (bomLine == null) {
                    continue;
                }
                itemCompositionId = bomLine.id();
                BigDecimal unitRatio = bomLine.childQuantity()
                        .divide(bomLine.parentQuantity(), QTY_SCALE, QTY_ROUNDING);
                issueQty = unitRatio.multiply(shipmentQty).setScale(QTY_SCALE, QTY_ROUNDING);
                if (item.propertyClassification() == PropertyClassification.원자재) {
                    locationCode = LOCATION_RAW;
                    sourceProcessId = null;
                } else {
                    locationCode = LOCATION_WIP;
                    sourceProcessId = slot.inputProcessId();
                }
            }

            if (issueQty.compareTo(BigDecimal.ZERO) <= 0) {
                continue;
            }
            result.add(new OutsourcingShipmentInputSaveCommand(
                    item.id(),
                    itemCompositionId,
                    issueQty,
                    locationCode,
                    sourceProcessId,
                    slot.inputProcessId()
            ));
        }
        if (result.isEmpty()) {
            throw new IllegalArgumentException("출고할 투입 자재가 없습니다.");
        }
        return result;
    }
}
