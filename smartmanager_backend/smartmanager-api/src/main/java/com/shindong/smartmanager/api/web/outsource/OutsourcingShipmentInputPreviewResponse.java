package com.shindong.smartmanager.api.web.outsource;

import com.shindong.smartmanager.application.outsource.OutsourcingShipmentInputPreviewLineView;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentInputPreviewView;
import java.math.BigDecimal;
import java.util.List;

public record OutsourcingShipmentInputPreviewResponse(
        Long orderLineId,
        String orderNo,
        String itemNo,
        BigDecimal shipmentQty,
        List<OutsourcingShipmentInputPreviewLineResponse> lines
) {
    public static OutsourcingShipmentInputPreviewResponse from(OutsourcingShipmentInputPreviewView view) {
        return new OutsourcingShipmentInputPreviewResponse(
                view.orderLineId(),
                view.orderNo(),
                view.itemNo(),
                view.shipmentQty(),
                view.lines().stream().map(OutsourcingShipmentInputPreviewLineResponse::from).toList()
        );
    }
}

record OutsourcingShipmentInputPreviewLineResponse(
        long itemId,
        String itemNo,
        String itemName,
        String propertyClassification,
        Long itemCompositionId,
        BigDecimal unitRatio,
        BigDecimal issueQty,
        String sourceLocationCode,
        Long sourceProcessId,
        long inputProcessId,
        String inputProcessName,
        BigDecimal onHandQty
) {
    static OutsourcingShipmentInputPreviewLineResponse from(OutsourcingShipmentInputPreviewLineView view) {
        return new OutsourcingShipmentInputPreviewLineResponse(
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.propertyClassification(),
                view.itemCompositionId(),
                view.unitRatio(),
                view.issueQty(),
                view.sourceLocationCode(),
                view.sourceProcessId(),
                view.inputProcessId(),
                view.inputProcessName(),
                view.onHandQty()
        );
    }
}
