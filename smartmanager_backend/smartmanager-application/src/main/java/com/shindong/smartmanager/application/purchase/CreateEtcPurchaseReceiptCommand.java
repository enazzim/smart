package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record CreateEtcPurchaseReceiptCommand(
        LocalDate receiptDate,
        Integer fiscalYear,
        Integer fiscalMonth,
        List<CreateEtcPurchaseReceiptLineCommand> lines
) {
}
