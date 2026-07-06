package com.shindong.smartmanager.api.web.outsource;

import com.shindong.smartmanager.application.outsource.WorkPlanOutsourceCandidateVendorView;
import java.math.BigDecimal;
import java.time.LocalDate;

public record WorkPlanOutsourceCandidateVendorResponse(
        long partnerId,
        String partnerName,
        String businessRegNo,
        long beginProcessCodeId,
        String beginProcessCode,
        String beginProcessName,
        long endProcessCodeId,
        String endProcessCode,
        String endProcessName,
        BigDecimal orderRate,
        BigDecimal orderQty,
        BigDecimal unitPrice,
        BigDecimal amount,
        LocalDate requestedDeliveryDate
) {
    public static WorkPlanOutsourceCandidateVendorResponse from(WorkPlanOutsourceCandidateVendorView view) {
        return new WorkPlanOutsourceCandidateVendorResponse(
                view.partnerId(),
                view.partnerName(),
                view.businessRegNo(),
                view.beginProcessCodeId(),
                view.beginProcessCode(),
                view.beginProcessName(),
                view.endProcessCodeId(),
                view.endProcessCode(),
                view.endProcessName(),
                view.orderRate(),
                view.orderQty(),
                view.unitPrice(),
                view.amount(),
                view.requestedDeliveryDate()
        );
    }
}
