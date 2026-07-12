package com.shindong.smartmanager.api.web.sales;

import com.shindong.smartmanager.application.sales.SalesRevenueLineView;
import com.shindong.smartmanager.application.sales.SalesRevenueView;
import com.shindong.smartmanager.domain.sales.SalesRevenueStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record SalesRevenueResponse(
        long id,
        String revenueNo,
        long partnerId,
        String partnerName,
        LocalDate revenueDate,
        Long salesShipmentId,
        SalesRevenueStatus status,
        String statusLabel,
        Instant createdAt,
        String createdBy,
        boolean cancelable,
        List<SalesRevenueLineResponse> lines
) {
    public static SalesRevenueResponse from(SalesRevenueView view) {
        return new SalesRevenueResponse(
                view.id(),
                view.revenueNo(),
                view.partnerId(),
                view.partnerName(),
                view.revenueDate(),
                view.salesShipmentId(),
                view.status(),
                statusLabel(view.status()),
                view.createdAt(),
                view.createdBy(),
                view.cancelable(),
                view.lines().stream().map(SalesRevenueLineResponse::from).toList()
        );
    }

    private static String statusLabel(SalesRevenueStatus status) {
        return switch (status) {
            case ISSUED -> "매출";
            case CANCELLED -> "취소";
        };
    }
}

record SalesRevenueLineResponse(
        long id,
        int lineNo,
        long salesShipmentLineId,
        String shipmentNo,
        String orderNo,
        long partnerId,
        String partnerName,
        String itemNo,
        String itemName,
        BigDecimal revenueQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        Long lotId
) {
    static SalesRevenueLineResponse from(SalesRevenueLineView view) {
        return new SalesRevenueLineResponse(
                view.id(),
                view.lineNo(),
                view.salesShipmentLineId(),
                view.shipmentNo(),
                view.orderNo(),
                view.partnerId(),
                view.partnerName(),
                view.itemNo(),
                view.itemName(),
                view.revenueQty(),
                view.unitPrice(),
                view.amount(),
                view.lotId()
        );
    }
}
