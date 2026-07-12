package com.shindong.smartmanager.api.web.inventory;

import com.shindong.smartmanager.application.inventory.LotBalanceView;
import com.shindong.smartmanager.application.inventory.LotView;
import com.shindong.smartmanager.domain.inventory.LotOriginType;
import com.shindong.smartmanager.domain.inventory.LotStatus;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record LotResponse(
        long id,
        long itemId,
        String itemNo,
        String itemName,
        String lotNo,
        LotStatus status,
        String statusLabel,
        LotOriginType originType,
        String originTypeLabel,
        String originDocType,
        Long originDocId,
        String p1,
        String p2,
        LocalDate expiryDate,
        String certificateRef,
        String remark,
        Instant createdAt,
        Instant updatedAt,
        List<LotBalanceResponse> balances
) {
    public static LotResponse from(LotView view) {
        List<LotBalanceResponse> balances = view.balances() == null
                ? List.of()
                : view.balances().stream().map(LotBalanceResponse::from).toList();
        return new LotResponse(
                view.id(),
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.lotNo(),
                view.status(),
                statusLabel(view.status()),
                view.originType(),
                originTypeLabel(view.originType()),
                view.originDocType(),
                view.originDocId(),
                view.p1(),
                view.p2(),
                view.expiryDate(),
                view.certificateRef(),
                view.remark(),
                view.createdAt(),
                view.updatedAt(),
                balances
        );
    }

    private static String statusLabel(LotStatus status) {
        if (status == null) {
            return "";
        }
        return switch (status) {
            case ACTIVE -> "활성";
            case BLOCKED -> "차단";
            case DEPLETED -> "소진";
        };
    }

    private static String originTypeLabel(LotOriginType originType) {
        if (originType == null) {
            return "";
        }
        return switch (originType) {
            case MANUAL -> "수동";
            case PURCHASE -> "구매";
            case PRODUCTION -> "생산";
            case SPLIT -> "분할";
            case MERGE -> "병합";
            case ADJUSTMENT -> "조정";
        };
    }

    public record LotBalanceResponse(
            long id,
            long lotId,
            long inventoryBalanceId,
            String locationCode,
            String locationLabel,
            Long outputProcessId,
            Short outputProcessSequence,
            String outputProcessName,
            BigDecimal qtyOnHand
    ) {
        public static LotBalanceResponse from(LotBalanceView view) {
            return new LotBalanceResponse(
                    view.id(),
                    view.lotId(),
                    view.inventoryBalanceId(),
                    view.locationCode(),
                    view.locationLabel(),
                    view.outputProcessId(),
                    view.outputProcessSequence(),
                    view.outputProcessName(),
                    view.qtyOnHand()
            );
        }
    }
}
