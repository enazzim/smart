package com.shindong.smartmanager.api.web.inventory;

import com.shindong.smartmanager.application.inventory.MiscStockMovementView;
import com.shindong.smartmanager.domain.inventory.MiscStockMovementDirection;
import com.shindong.smartmanager.domain.inventory.MiscStockMovementStatus;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

public record MiscStockMovementResponse(
        long id,
        String movementNo,
        LocalDate movementDate,
        MiscStockMovementDirection movementDirection,
        String movementDirectionLabel,
        long itemId,
        String itemNo,
        String itemName,
        PropertyClassification propertyClassification,
        String locationCode,
        String locationLabel,
        Long outputProcessId,
        Short outputProcessSequence,
        String outputProcessName,
        BigDecimal qty,
        Long reasonCodeId,
        String reasonLabel,
        String note,
        MiscStockMovementStatus status,
        String statusLabel,
        Instant createdAt,
        Long lotId
) {
    public static MiscStockMovementResponse from(MiscStockMovementView view) {
        return new MiscStockMovementResponse(
                view.id(),
                view.movementNo(),
                view.movementDate(),
                view.movementDirection(),
                view.movementDirection() == MiscStockMovementDirection.IN ? "입고" : "출고",
                view.itemId(),
                view.itemNo(),
                view.itemName(),
                view.propertyClassification(),
                view.locationCode(),
                view.locationLabel(),
                view.outputProcessId(),
                view.outputProcessSequence(),
                view.outputProcessName(),
                view.qty(),
                view.reasonCodeId(),
                view.reasonLabel(),
                view.note(),
                view.status(),
                view.status() == MiscStockMovementStatus.REGISTERED ? "등록" : "취소",
                view.createdAt(),
                view.lotId()
        );
    }
}
