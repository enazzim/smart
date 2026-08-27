package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.EtcClaimView;
import com.shindong.smartmanager.domain.purchase.EtcClaimRecognition;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record EtcClaimResponse(
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

    public static EtcClaimResponse from(EtcClaimView view) {
        return new EtcClaimResponse(
                view.id(),
                view.partnerId(),
                view.partnerName(),
                view.partnerBusinessRegNo(),
                view.receiptDate(),
                view.reason(),
                view.amount(),
                view.fiscalYear(),
                view.fiscalMonth(),
                view.recognition(),
                view.createdBy(),
                view.createdAt(),
                view.editable()
        );
    }
}
