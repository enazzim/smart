package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.time.LocalDate;

public record UpdateEtcPurchaseReceiptCommand(
        LocalDate receiptDate,
        BigDecimal receiptQty,
        Integer fiscalYear,
        Integer fiscalMonth
) {
}
