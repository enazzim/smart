package com.shindong.smartmanager.api.web.sales;

import com.shindong.smartmanager.application.sales.SalesOrderLineView;
import com.shindong.smartmanager.domain.sales.SalesFulfillmentRoute;
import com.shindong.smartmanager.domain.sales.SalesLineDeliveryStatus;
import com.shindong.smartmanager.domain.sales.SalesLineFulfillmentStatus;
import java.math.BigDecimal;
import java.time.LocalDate;

public record SalesOrderLineResponse(
        long id,
        int lineNo,
        long itemId,
        String itemNo,
        String itemName,
        String propertyClassification,
        BigDecimal orderQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        LocalDate deliveryDate,
        SalesFulfillmentRoute fulfillmentRoute,
        String fulfillmentRouteLabel,
        SalesLineFulfillmentStatus fulfillmentStatus,
        String fulfillmentStatusLabel,
        SalesLineDeliveryStatus deliveryStatus,
        String deliveryStatusLabel
) {

    public static SalesOrderLineResponse from(SalesOrderLineView view) {
        return new SalesOrderLineResponse(
                view.id(),
                view.lineNo(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.propertyClassification(),
                view.orderQty(),
                view.unitPrice(),
                view.amount(),
                view.deliveryDate(),
                view.fulfillmentRoute(),
                routeLabel(view.fulfillmentRoute()),
                view.fulfillmentStatus(),
                fulfillmentLabel(view.fulfillmentStatus()),
                view.deliveryStatus(),
                deliveryLabel(view.deliveryStatus())
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
