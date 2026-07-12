package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.domain.inventory.LotStatus;
import java.time.LocalDate;

public record UpdateLotCommand(
        LotStatus status,
        String p1,
        String p2,
        LocalDate expiryDate,
        String certificateRef,
        String remark
) {
}
