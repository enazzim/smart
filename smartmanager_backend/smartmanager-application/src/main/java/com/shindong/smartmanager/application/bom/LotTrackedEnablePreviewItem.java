package com.shindong.smartmanager.application.bom;

import java.util.List;

public record LotTrackedEnablePreviewItem(
        long itemId,
        String itemNo,
        String itemName,
        boolean alreadyLotTracked,
        List<String> otherParentItemNos
) {
}
