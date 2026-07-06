package com.shindong.smartmanager.api.web.outsource;

import com.shindong.smartmanager.application.outsource.WorkPlanOutsourceCandidateView;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record WorkPlanOutsourceCandidateResponse(
        long workPlanId,
        long productionPlanId,
        String planNo,
        long itemId,
        String itemNo,
        String itemName,
        long processSequenceId,
        short processSequenceNum,
        String processCode,
        String processName,
        BigDecimal plannedQty,
        BigDecimal orderedQty,
        BigDecimal remainingQty,
        LocalDate planEndDate,
        BigDecimal orderRateTotal,
        boolean orderable,
        String orderableMessage,
        List<WorkPlanOutsourceCandidateVendorResponse> vendors
) {
    public static WorkPlanOutsourceCandidateResponse from(WorkPlanOutsourceCandidateView view) {
        return new WorkPlanOutsourceCandidateResponse(
                view.workPlanId(),
                view.productionPlanId(),
                view.planNo(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.processSequenceId(),
                view.processSequenceNum(),
                view.processCode(),
                view.processName(),
                view.plannedQty(),
                view.orderedQty(),
                view.remainingQty(),
                view.planEndDate(),
                view.orderRateTotal(),
                view.orderable(),
                view.orderableMessage(),
                view.vendors().stream().map(WorkPlanOutsourceCandidateVendorResponse::from).toList()
        );
    }
}
