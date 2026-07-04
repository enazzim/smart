package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.ProductionPlanView;
import com.shindong.smartmanager.domain.production.ProductionPlanMrpStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanWorkPlanStatus;
import java.math.BigDecimal;
import java.time.LocalDate;

public record ProductionPlanResponse(
        long id,
        String planNo,
        long salesOrderId,
        long salesOrderLineId,
        String orderNo,
        long partnerId,
        String partnerName,
        LocalDate orderDate,
        long itemId,
        String itemNo,
        String itemName,
        BigDecimal plannedQty,
        BigDecimal producedQty,
        LocalDate requestedDeliveryDate,
        ProductionPlanStatus status,
        String statusLabel,
        ProductionPlanMrpStatus mrpStatus,
        String mrpStatusLabel,
        ProductionPlanWorkPlanStatus workPlanStatus,
        String workPlanStatusLabel,
        boolean cancellable
) {
    public static ProductionPlanResponse from(ProductionPlanView view) {
        return new ProductionPlanResponse(
                view.id(),
                view.planNo(),
                view.salesOrderId(),
                view.salesOrderLineId(),
                view.orderNo(),
                view.partnerId(),
                view.partnerName(),
                view.orderDate(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.plannedQty(),
                view.producedQty(),
                view.requestedDeliveryDate(),
                view.status(),
                planStatusLabel(view.status()),
                view.mrpStatus(),
                mrpStatusLabel(view.mrpStatus()),
                view.workPlanStatus(),
                workPlanStatusLabel(view.workPlanStatus()),
                isCancellable(view)
        );
    }

    private static boolean isCancellable(ProductionPlanView view) {
        if (view.status() == ProductionPlanStatus.CANCELLED || view.status() == ProductionPlanStatus.COMPLETED) {
            return false;
        }
        if (view.producedQty() != null && view.producedQty().compareTo(BigDecimal.ZERO) > 0) {
            return false;
        }
        if (view.mrpStatus() != ProductionPlanMrpStatus.NOT_CALCULATED) {
            return false;
        }
        return view.workPlanStatus() == ProductionPlanWorkPlanStatus.NOT_PLANNED;
    }

    private static String planStatusLabel(ProductionPlanStatus status) {
        return switch (status) {
            case PLANNED -> "계획";
            case IN_PROGRESS -> "진행";
            case COMPLETED -> "완료";
            case CANCELLED -> "취소";
        };
    }

    private static String mrpStatusLabel(ProductionPlanMrpStatus status) {
        return switch (status) {
            case NOT_CALCULATED -> "미산출";
            case CALCULATED -> "산출완료";
        };
    }

    private static String workPlanStatusLabel(ProductionPlanWorkPlanStatus status) {
        return switch (status) {
            case NOT_PLANNED -> "미수립";
            case PLANNED -> "수립";
        };
    }
}
