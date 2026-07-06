package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PurchaseOrderSourceType;
import com.shindong.smartmanager.domain.purchase.PurchaseOrderStatus;
import java.time.Instant;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record PurchaseOrderView(
        long id,
        String orderNo,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        LocalDate orderDate,
        PurchaseOrderSourceType sourceType,
        PurchaseOrderStatus status,
        Instant createdAt,
        String createdBy,
        List<PurchaseOrderLineView> lines
) {
    public boolean cancelable() {
        if (status == PurchaseOrderStatus.CANCELLED) {
            return false;
        }
        if (status == PurchaseOrderStatus.DRAFT) {
            return true;
        }
        if (status == PurchaseOrderStatus.CONFIRMED) {
            return !hasReceiptProgress();
        }
        return false;
    }

    public boolean hasReceiptProgress() {
        return lines.stream().anyMatch(line ->
                greaterThanZero(line.receivedQty()) || greaterThanZero(line.waitingInspectionQty()));
    }

    private static boolean greaterThanZero(BigDecimal value) {
        return value != null && value.compareTo(BigDecimal.ZERO) > 0;
    }
}
