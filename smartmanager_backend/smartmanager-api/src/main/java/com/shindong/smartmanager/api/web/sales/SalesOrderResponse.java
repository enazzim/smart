package com.shindong.smartmanager.api.web.sales;

import com.shindong.smartmanager.application.sales.SalesOrderView;
import com.shindong.smartmanager.domain.sales.SalesOrderStatus;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record SalesOrderResponse(
        long id,
        String orderNo,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        LocalDate orderDate,
        LocalDate requestedDeliveryDate,
        SalesOrderStatus status,
        String remark,
        Instant confirmedAt,
        String confirmedBy,
        List<SalesOrderLineResponse> lines
) {

    public static SalesOrderResponse from(SalesOrderView view) {
        return new SalesOrderResponse(
                view.id(),
                view.orderNo(),
                view.partnerId(),
                view.partnerName(),
                view.partnerBusinessRegNo(),
                view.orderDate(),
                view.requestedDeliveryDate(),
                view.status(),
                view.remark(),
                view.confirmedAt(),
                view.confirmedBy(),
                view.lines().stream().map(SalesOrderLineResponse::from).toList()
        );
    }
}
