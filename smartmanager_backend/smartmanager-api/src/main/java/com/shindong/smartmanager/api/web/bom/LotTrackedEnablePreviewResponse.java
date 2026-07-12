package com.shindong.smartmanager.api.web.bom;

import com.shindong.smartmanager.application.bom.LotTrackedEnablePreviewItem;
import java.util.List;

public record LotTrackedEnablePreviewResponse(
        long itemId,
        String itemNo,
        String itemName,
        boolean alreadyLotTracked,
        List<String> otherParentItemNos
) {
    static LotTrackedEnablePreviewResponse from(LotTrackedEnablePreviewItem item) {
        return new LotTrackedEnablePreviewResponse(
                item.itemId(),
                item.itemNo(),
                item.itemName(),
                item.alreadyLotTracked(),
                item.otherParentItemNos()
        );
    }
}
