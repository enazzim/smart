package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.domain.inventory.LotOriginType;
import java.time.LocalDate;

public record CreateLotCommand(
        long itemId,
        String lotNo,
        boolean autoGenerate,
        LotOriginType originType,
        String originDocType,
        Long originDocId,
        String p1,
        String p2,
        LocalDate expiryDate,
        String certificateRef,
        String remark
) {
}
