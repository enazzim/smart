package com.shindong.smartmanager.api.web.sales;

import com.shindong.smartmanager.application.sales.SalesShipmentLineView;
import com.shindong.smartmanager.application.sales.SalesShipmentView;
import com.shindong.smartmanager.domain.sales.SalesShipmentStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record SalesShipmentResponse(
        long id,
        String shipmentNo,
        long partnerId,
        String partnerName,
        LocalDate shipmentDate,
        Long salesOrderId,
        SalesShipmentStatus status,
        String statusLabel,
        Instant createdAt,
        String createdBy,
        boolean cancelable,
        List<SalesShipmentLineResponse> lines
) {
    public static SalesShipmentResponse from(SalesShipmentView view) {
        return new SalesShipmentResponse(
                view.id(),
                view.shipmentNo(),
                view.partnerId(),
                view.partnerName(),
                view.shipmentDate(),
                view.salesOrderId(),
                view.status(),
                statusLabel(view.status()),
                view.createdAt(),
                view.createdBy(),
                view.cancelable(),
                view.lines().stream().map(SalesShipmentLineResponse::from).toList()
        );
    }

    private static String statusLabel(SalesShipmentStatus status) {
        return switch (status) {
            case ISSUED -> "출고";
            case CANCELLED -> "취소";
        };
    }
}

record SalesShipmentLineResponse(
        long id,
        int lineNo,
        long salesOrderLineId,
        String orderNo,
        long partnerId,
        String partnerName,
        String itemNo,
        String itemName,
        BigDecimal shipmentQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        Long lotId
) {
    static SalesShipmentLineResponse from(SalesShipmentLineView view) {
        return new SalesShipmentLineResponse(
                view.id(),
                view.lineNo(),
                view.salesOrderLineId(),
                view.orderNo(),
                view.partnerId(),
                view.partnerName(),
                view.itemNo(),
                view.itemName(),
                view.shipmentQty(),
                view.unitPrice(),
                view.amount(),
                view.lotId()
        );
    }
}
