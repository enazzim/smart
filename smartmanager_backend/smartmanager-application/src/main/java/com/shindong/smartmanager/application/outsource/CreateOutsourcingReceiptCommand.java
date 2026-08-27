package com.shindong.smartmanager.application.outsource;

import java.time.LocalDate;
import java.util.List;

public record CreateOutsourcingReceiptCommand(
        LocalDate receiptDate,
        Integer fiscalYear,
        Integer fiscalMonth,
        List<CreateOutsourcingReceiptLineCommand> lines,
        boolean allowOverQty
) {
    public CreateOutsourcingReceiptCommand(
            LocalDate receiptDate,
            Integer fiscalYear,
            Integer fiscalMonth,
            List<CreateOutsourcingReceiptLineCommand> lines
    ) {
        this(receiptDate, fiscalYear, fiscalMonth, lines, false);
    }
}
