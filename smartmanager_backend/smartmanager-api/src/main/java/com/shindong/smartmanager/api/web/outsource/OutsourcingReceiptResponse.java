package com.shindong.smartmanager.api.web.outsource;

import com.shindong.smartmanager.application.outsource.OutsourcingReceiptView;
import com.shindong.smartmanager.domain.outsource.OutsourcingReceiptStatus;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record OutsourcingReceiptResponse(
        long id,
        String receiptNo,
        long partnerId,
        String partnerName,
        LocalDate receiptDate,
        Long outsourcingOrderId,
        OutsourcingReceiptStatus status,
        Instant createdAt,
        String createdBy,
        List<OutsourcingReceiptLineResponse> lines
) {
    public static OutsourcingReceiptResponse from(OutsourcingReceiptView view) {
        return new OutsourcingReceiptResponse(
                view.id(),
                view.receiptNo(),
                view.partnerId(),
                view.partnerName(),
                view.receiptDate(),
                view.outsourcingOrderId(),
                view.status(),
                view.createdAt(),
                view.createdBy(),
                view.lines().stream().map(OutsourcingReceiptLineResponse::from).toList()
        );
    }
}
