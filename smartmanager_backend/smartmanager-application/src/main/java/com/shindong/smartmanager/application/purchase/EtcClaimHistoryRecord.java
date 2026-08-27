package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.EtcClaimRecognition;
import java.math.BigDecimal;
import java.time.LocalDate;

public record EtcClaimHistoryRecord(
        long id,
        long companyId,
        BigDecimal amount,
        LocalDate receiptDate,
        int fiscalYear,
        int fiscalMonth,
        EtcClaimRecognition recognition
) {
}
