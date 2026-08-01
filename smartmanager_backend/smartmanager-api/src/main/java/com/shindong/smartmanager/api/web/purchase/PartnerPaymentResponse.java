package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.application.purchase.PartnerPaymentLineView;
import com.shindong.smartmanager.application.purchase.PartnerPaymentView;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentKind;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record PartnerPaymentResponse(
        long id,
        String paymentNo,
        long partnerId,
        String partnerName,
        String partnerBusinessRegNo,
        LocalDate paymentDate,
        PartnerPaymentCostCategory costCategory,
        String costCategoryLabel,
        PartnerPaymentKind paymentKind,
        String paymentKindLabel,
        BigDecimal supplyAmount,
        BigDecimal vatAmount,
        BigDecimal totalAmount,
        String paymentMethod,
        String remark,
        PartnerPaymentStatus status,
        String statusLabel,
        Instant createdAt,
        String createdBy,
        boolean cancelable,
        List<PartnerPaymentLineResponse> lines,
        String lineSummary
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
                view.paymentKind(),
                paymentKindLabel(view.paymentKind()),
                view.supplyAmount(),
                view.vatAmount(),
                view.totalAmount(),
                view.paymentMethod(),
                view.remark(),
                view.status(),
                statusLabel(view.status()),
                view.createdAt(),
                view.createdBy(),
                view.cancelable(),
                view.lines() == null ? List.of() : view.lines().stream().map(PartnerPaymentLineResponse::from).toList(),
                view.lineSummary()
        );
    }

    private static String costCategoryLabel(PartnerPaymentCostCategory category) {
        return switch (category) {
            case PURCHASE -> "구매";
            case OUTSOURCE -> "외주";
        };
    }

    private static String paymentKindLabel(PartnerPaymentKind kind) {
        if (kind == null) {
            return "일반";
        }
        return switch (kind) {
            case NORMAL -> "일반";
            case PREPAID -> "선지급";
        };
    }

    private static String statusLabel(PartnerPaymentStatus status) {
        return switch (status) {
            case ISSUED -> "지급";
            case CANCELLED -> "취소";
        };
    }

    public record PartnerPaymentLineResponse(
            long id,
            long itemId,
            String itemNo,
            String itemName,
            Long purchaseOrderLineId,
            Long outsourcingOrderLineId,
            String orderNo,
            BigDecimal supplyAmount,
            BigDecimal vatAmount,
            BigDecimal totalAmount,
            BigDecimal offsetAmount,
            BigDecimal remainingAmount
    ) {
        public static PartnerPaymentLineResponse from(PartnerPaymentLineView view) {
            return new PartnerPaymentLineResponse(
                    view.id(),
                    view.itemId(),
                    view.itemNo(),
                    view.itemName(),
                    view.purchaseOrderLineId(),
                    view.outsourcingOrderLineId(),
                    view.orderNo(),
                    view.supplyAmount(),
                    view.vatAmount(),
                    view.totalAmount(),
                    view.offsetAmount(),
                    view.remainingAmount()
            );
        }
    }
}
