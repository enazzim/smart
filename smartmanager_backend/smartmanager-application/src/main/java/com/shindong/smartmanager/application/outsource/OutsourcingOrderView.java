package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourcingOrderSourceType;
import com.shindong.smartmanager.domain.outsource.OutsourcingOrderStatus;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record OutsourcingOrderView(
        long id,
        String orderNo,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        LocalDate orderDate,
        OutsourcingOrderSourceType sourceType,
        OutsourcingOrderStatus status,
        Instant createdAt,
        String createdBy,
        List<OutsourcingOrderLineView> lines
) {
    public boolean cancelable() {
        if (status == OutsourcingOrderStatus.CANCELLED) {
            return false;
        }
        return lines.stream().noneMatch(line ->
                greaterThanZero(line.shippedQty()) || greaterThanZero(line.receivedQty()));
    }

    private static boolean greaterThanZero(java.math.BigDecimal value) {
        return value != null && value.compareTo(java.math.BigDecimal.ZERO) > 0;
    }
}
