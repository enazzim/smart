package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.production.WorkOrderStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record WorkOrderView(
        long id,
        long workPlanId,
        String orderNum,
        long productionPlanId,
        String planNo,
        long itemId,
        String itemNo,
        String itemName,
        long processSequenceId,
        short processSequenceNum,
        String processCode,
        String processName,
        Long workCenterId,
        String workCenterName,
        BigDecimal orderedQty,
        BigDecimal reportedQty,
        BigDecimal remainingQty,
        LocalDate planStartDate,
        WorkOrderStatus status,
        boolean cancellable,
        Instant createdAt,
        String createdBy
) {
}
