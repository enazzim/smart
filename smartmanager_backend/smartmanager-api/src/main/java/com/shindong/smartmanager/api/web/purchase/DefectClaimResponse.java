package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.DefectClaimView;
import com.shindong.smartmanager.domain.purchase.EtcClaimRecognition;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record DefectClaimResponse(
        long id,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        long itemId,
        String itemNo,
        String itemName,
        String drawingNo,
        LocalDate receiptDate,
        BigDecimal claimQty,
        BigDecimal amount,
        String reason,
        int fiscalYear,
        int fiscalMonth,
        EtcClaimRecognition recognition,
        String createdBy,
        Instant createdAt,
        boolean editable
) {

    public static DefectClaimResponse from(DefectClaimView view) {
        return new DefectClaimResponse(
                view.id(),
                view.partnerId(),
                view.partnerName(),
                view.partnerBusinessRegNo(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.drawingNo(),
                view.receiptDate(),
                view.claimQty(),
                view.amount(),
                view.reason(),
                view.fiscalYear(),
                view.fiscalMonth(),
                view.recognition(),
                view.createdBy(),
                view.createdAt(),
                view.editable()
        );
    }
}
