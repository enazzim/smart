package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.domain.inventory.LotStatus;

public record LotListCriteria(
        Long itemId,
        String itemNo,
        String lotNo,
        LotStatus status,
        String locationCode
) {
}
