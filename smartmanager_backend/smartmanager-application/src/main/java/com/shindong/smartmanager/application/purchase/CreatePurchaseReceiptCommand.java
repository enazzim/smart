package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record CreatePurchaseReceiptCommand(
        LocalDate receiptDate,
        List<CreatePurchaseReceiptLineCommand> lines
) {
    public CreatePurchaseReceiptCommand {
        if (receiptDate == null) {
            throw new IllegalArgumentException("입고일은 필수입니다.");
        }
        if (lines == null || lines.isEmpty()) {
            throw new IllegalArgumentException("입고 라인은 1건 이상 필요합니다.");
        }
    }
}
