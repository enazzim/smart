package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.EtcClaimRecognition;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record EtcClaimView(
        long id,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        LocalDate receiptDate,
        String reason,
        BigDecimal amount,
        int fiscalYear,
        int fiscalMonth,
        EtcClaimRecognition recognition,
        String createdBy,
        Instant createdAt,
        boolean editable
) {
}
