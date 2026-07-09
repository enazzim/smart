package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.time.LocalDate;

public record CreateOutsourcingReceiptCommand(
        LocalDate receiptDate,
        Integer fiscalYear,
        Integer fiscalMonth,
        java.util.List<CreateOutsourcingReceiptLineCommand> lines
) {
}
