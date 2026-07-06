package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.domain.inventory.StockMovementType;
import java.math.BigDecimal;
import java.time.LocalDate;

public record RecordStockMovementCommand(
        long itemId,
        String locationCode,
        LocalDate movementDate,
        StockMovementType movementType,
        BigDecimal qty,
        BigDecimal amount,
        String referenceType,
        long referenceId,
        Long outputProcessId,
        Long inputProcessId,
        Long partnerId,
        String actorUserId
) {
    public RecordStockMovementCommand {
        if (qty == null || qty.compareTo(BigDecimal.ZERO) == 0) {
            throw new IllegalArgumentException("수량은 0이 아니어야 합니다.");
        }
        if (movementType != StockMovementType.ADJUST && qty.compareTo(BigDecimal.ZERO) < 0) {
            throw new IllegalArgumentException("IN/OUT 수량은 양수여야 합니다.");
        }
        if (amount == null) {
            amount = BigDecimal.ZERO;
        }
        if (movementDate == null) {
            throw new IllegalArgumentException("이동일은 필수입니다.");
        }
        if (locationCode == null || locationCode.isBlank()) {
            throw new IllegalArgumentException("창고 코드는 필수입니다.");
        }
        if (referenceType == null || referenceType.isBlank()) {
            throw new IllegalArgumentException("참조 유형은 필수입니다.");
        }
    }
}
