package com.shindong.smartmanager.api.web.sales;

import com.shindong.smartmanager.application.sales.SalesOrderLineListView;
import com.shindong.smartmanager.domain.sales.SalesFulfillmentRoute;
import com.shindong.smartmanager.domain.sales.SalesLineDeliveryStatus;
import com.shindong.smartmanager.domain.sales.SalesLineFulfillmentStatus;
import com.shindong.smartmanager.domain.sales.SalesOrderStatus;
import java.math.BigDecimal;
import java.time.LocalDate;

public record SalesOrderLineListResponse(
        long orderId,
        long lineId,
        String orderNo,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        LocalDate orderDate,
        LocalDate requestedDeliveryDate,
        long itemId,
        String itemNo,
        String itemName,
        BigDecimal unitPrice,
        BigDecimal orderQty,
        BigDecimal amount,
        SalesFulfillmentRoute fulfillmentRoute,
        String fulfillmentRouteLabel,
        SalesLineFulfillmentStatus fulfillmentStatus,
        String fulfillmentStatusLabel,
        String executionStatusLabel,
        SalesLineDeliveryStatus deliveryStatus,
        String deliveryStatusLabel,
        SalesOrderStatus orderStatus,
        boolean orderEditable
) {
    public static SalesOrderLineListResponse from(SalesOrderLineListView view) {
        String routeLabel = routeLabel(view.fulfillmentRoute());
        String fulfillmentLabel = fulfillmentLabel(view.fulfillmentStatus());
        return new SalesOrderLineListResponse(
                view.orderId(),
                view.lineId(),
                view.orderNo(),
                view.partnerId(),
                view.partnerName(),
                view.partnerBusinessRegNo(),
                view.orderDate(),
                view.requestedDeliveryDate(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.unitPrice(),
                view.orderQty(),
                view.amount(),
                view.fulfillmentRoute(),
                routeLabel,
                view.fulfillmentStatus(),
                fulfillmentLabel,
                routeLabel + "·" + fulfillmentLabel,
                view.deliveryStatus(),
                deliveryLabel(view.deliveryStatus()),
                view.orderStatus(),
                view.orderEditable()
        );
    }

    private static String routeLabel(SalesFulfillmentRoute route) {
        return route == SalesFulfillmentRoute.COMMODITY ? "구매" : "생산";
    }

    private static String fulfillmentLabel(SalesLineFulfillmentStatus status) {
        return switch (status) {
            case WAITING -> "대기";
            case IN_PROGRESS -> "진행";
            case COMPLETED -> "완료";
            case FORCE_COMPLETED -> "강제완료";
        };
    }

    private static String deliveryLabel(SalesLineDeliveryStatus status) {
        return switch (status) {
            case NOT_STARTED -> "미납";
            case IN_PROGRESS -> "진행";
            case COMPLETED -> "완납";
        };
    }
}
