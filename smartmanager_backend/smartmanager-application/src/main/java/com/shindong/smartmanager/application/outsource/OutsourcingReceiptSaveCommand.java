package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourcingReceiptStatus;
import java.time.LocalDate;
import java.util.List;

public record OutsourcingReceiptSaveCommand(
        String receiptNo,
        long partnerId,
        LocalDate receiptDate,
        Long outsourcingOrderId,
        OutsourcingReceiptStatus status,
        List<OutsourcingReceiptLineSaveCommand> lines
) {
}
