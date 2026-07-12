package com.shindong.smartmanager.api.web.outsource;

import com.shindong.smartmanager.application.outsource.OutsourcingShipmentInputLineView;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentLineView;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentView;
import com.shindong.smartmanager.domain.outsource.OutsourcingShipmentStatus;
import com.shindong.smartmanager.domain.outsource.OutsourcingShipmentType;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record OutsourcingShipmentResponse(
        long id,
        String shipmentNo,
        LocalDate shipmentDate,
        OutsourcingShipmentType shipmentType,
        String shipmentTypeLabel,
        Long partnerId,
        String partnerName,
        OutsourcingShipmentStatus status,
        String statusLabel,
        Instant createdAt,
        String createdBy,
        boolean cancelable,
        List<OutsourcingShipmentLineResponse> lines
) {
    public static OutsourcingShipmentResponse from(OutsourcingShipmentView view) {
        return new OutsourcingShipmentResponse(
                view.id(),
                view.shipmentNo(),
                view.shipmentDate(),
                view.shipmentType(),
                shipmentTypeLabel(view.shipmentType()),
                view.partnerId(),
                view.partnerName(),
                view.status(),
                statusLabel(view.status()),
                view.createdAt(),
                view.createdBy(),
                view.cancelable(),
                view.lines().stream().map(OutsourcingShipmentLineResponse::from).toList()
        );
    }

    private static String shipmentTypeLabel(OutsourcingShipmentType type) {
        return switch (type) {
            case ORDER -> "발주출고";
            case ADVANCE -> "선출고";
        };
    }

    private static String statusLabel(OutsourcingShipmentStatus status) {
        return switch (status) {
            case ISSUED -> "출고";
            case CANCELLED -> "취소";
        };
    }
}

record OutsourcingShipmentLineResponse(
        long id,
        int lineNo,
        Long orderLineId,
        String orderNo,
        Long parentItemId,
        String parentItemNo,
        String parentItemName,
        String itemNo,
        String itemName,
        String processName,
        BigDecimal shipmentQty,
        long partnerId,
        String partnerName,
        List<OutsourcingShipmentInputLineResponse> inputLines
) {
    static OutsourcingShipmentLineResponse from(OutsourcingShipmentLineView view) {
        return new OutsourcingShipmentLineResponse(
                view.id(),
                view.lineNo(),
                view.orderLineId(),
                view.orderNo(),
                view.parentItemId(),
                view.parentItemNo(),
                view.parentItemName(),
                view.itemNo(),
                view.itemName(),
                view.processName(),
                view.shipmentQty(),
                view.partnerId(),
                view.partnerName(),
                view.inputLines().stream().map(OutsourcingShipmentInputLineResponse::from).toList()
        );
    }
}

record OutsourcingShipmentInputLineResponse(
        long itemId,
        String itemNo,
        String itemName,
        Long itemCompositionId,
        BigDecimal issueQty,
        String sourceLocationCode,
        Long sourceProcessId,
        long inputProcessId,
        Long lotId
) {
    static OutsourcingShipmentInputLineResponse from(OutsourcingShipmentInputLineView view) {
        return new OutsourcingShipmentInputLineResponse(
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.itemCompositionId(),
                view.issueQty(),
                view.sourceLocationCode(),
                view.sourceProcessId(),
                view.inputProcessId(),
                view.lotId()
        );
    }
}
