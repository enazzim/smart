package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record WorkPlanOutsourceCandidateView(
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
        List<WorkPlanOutsourceCandidateVendorView> vendors
) {
}
