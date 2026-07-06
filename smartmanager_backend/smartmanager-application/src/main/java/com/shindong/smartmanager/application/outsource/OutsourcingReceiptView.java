package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourcingReceiptStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record OutsourcingReceiptView(
        long id,
        String receiptNo,
        long partnerId,
        String partnerName,
        LocalDate receiptDate,
        Long outsourcingOrderId,
        OutsourcingReceiptStatus status,
        Instant createdAt,
        String createdBy,
        List<OutsourcingReceiptLineView> lines
) {
}
