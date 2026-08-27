package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.EtcClaimRecognition;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record DefectClaimView(
        long id,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        long itemId,
        String itemNo,
        String itemName,
        String drawingNo,
        LocalDate receiptDate,
        BigDecimal claimQty,
        BigDecimal amount,
        String reason,
        int fiscalYear,
        int fiscalMonth,
        EtcClaimRecognition recognition,
        String createdBy,
        Instant createdAt,
        boolean editable
) {
}
