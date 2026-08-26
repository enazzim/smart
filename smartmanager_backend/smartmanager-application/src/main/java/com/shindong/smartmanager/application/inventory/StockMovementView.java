package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.domain.inventory.StockMovementType;
import java.math.BigDecimal;
import java.time.LocalDate;

public record StockMovementView(
        long id,
        long inventoryBalanceId,
        long itemId,
        long locationId,
        int fiscalYear,
        int fiscalMonth,
        StockMovementType movementType,
        BigDecimal qty,
        BigDecimal amount,
        String referenceType,
        long referenceId,
        LocalDate movementDate,
        Long lotId,
        Long outputProcessId,
        Long inputProcessId,
        Long partnerId
) {
    /** Lot·공정·거래처 미지정 (기존 호출 호환) */
    public StockMovementView(
            long id,
            long inventoryBalanceId,
            long itemId,
            long locationId,
            int fiscalYear,
            int fiscalMonth,
            StockMovementType movementType,
            BigDecimal qty,
            BigDecimal amount,
            String referenceType,
            long referenceId,
            LocalDate movementDate
    ) {
        this(
                id,
                inventoryBalanceId,
                itemId,
                locationId,
                fiscalYear,
                fiscalMonth,
                movementType,
                qty,
                amount,
                referenceType,
                referenceId,
                movementDate,
                null,
                null,
                null,
                null
        );
    }

    /** 공정·거래처 미지정 (Lot만 있는 기존 호출 호환) */
    public StockMovementView(
            long id,
            long inventoryBalanceId,
            long itemId,
            long locationId,
            int fiscalYear,
            int fiscalMonth,
            StockMovementType movementType,
            BigDecimal qty,
            BigDecimal amount,
            String referenceType,
            long referenceId,
            LocalDate movementDate,
            Long lotId
    ) {
        this(
                id,
                inventoryBalanceId,
                itemId,
                locationId,
                fiscalYear,
                fiscalMonth,
                movementType,
                qty,
                amount,
                referenceType,
                referenceId,
                movementDate,
                lotId,
                null,
                null,
                null
        );
    }
}
