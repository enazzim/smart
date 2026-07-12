package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.domain.inventory.LotOriginType;
import com.shindong.smartmanager.domain.inventory.LotStatus;
import java.time.Instant;
import java.time.LocalDate;
import java.util.List;

public record LotView(
        long id,
        long itemId,
        String itemNo,
        String itemName,
        String lotNo,
        LotStatus status,
        LotOriginType originType,
        String originDocType,
        Long originDocId,
        String p1,
        String p2,
        LocalDate expiryDate,
        String certificateRef,
        String remark,
        Instant createdAt,
        Instant updatedAt,
        List<LotBalanceView> balances
) {
}
