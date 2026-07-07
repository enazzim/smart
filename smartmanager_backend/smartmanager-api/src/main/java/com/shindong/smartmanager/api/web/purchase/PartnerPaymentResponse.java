package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.PartnerPaymentView;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record PartnerPaymentResponse(
        long id,
        String paymentNo,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        LocalDate paymentDate,
        PartnerPaymentCostCategory costCategory,
        String costCategoryLabel,
        BigDecimal supplyAmount,
        BigDecimal vatAmount,
        BigDecimal totalAmount,
        String paymentMethod,
        String remark,
        PartnerPaymentStatus status,
        String statusLabel,
        Instant createdAt,
        String createdBy,
        boolean cancelable
) {
    public static PartnerPaymentResponse from(PartnerPaymentView view) {
        return new PartnerPaymentResponse(
                view.id(),
                view.paymentNo(),
                view.partnerId(),
                view.partnerName(),
                view.partnerBusinessRegNo(),
                view.paymentDate(),
                view.costCategory(),
                costCategoryLabel(view.costCategory()),
                view.supplyAmount(),
                view.vatAmount(),
                view.totalAmount(),
                view.paymentMethod(),
                view.remark(),
                view.status(),
                statusLabel(view.status()),
                view.createdAt(),
                view.createdBy(),
                view.cancelable()
        );
    }

    private static String costCategoryLabel(PartnerPaymentCostCategory category) {
        return switch (category) {
            case PURCHASE -> "구매";
            case OUTSOURCE -> "외주";
        };
    }

    private static String statusLabel(PartnerPaymentStatus status) {
        return switch (status) {
            case ISSUED -> "지급";
            case CANCELLED -> "취소";
        };
    }
}
