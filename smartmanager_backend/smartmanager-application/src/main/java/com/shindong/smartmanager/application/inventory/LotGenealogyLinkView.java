package com.shindong.smartmanager.application.inventory;

import com.shindong.smartmanager.domain.inventory.LotGenealogyLinkType;
import java.math.BigDecimal;
import java.time.Instant;

public record LotGenealogyLinkView(
        long id,
        long parentLotId,
        String parentLotNo,
        long parentItemId,
        String parentItemNo,
        String parentItemName,
        long childLotId,
        String childLotNo,
        long childItemId,
        String childItemNo,
        String childItemName,
        LotGenealogyLinkType linkType,
        BigDecimal qty,
        Long stockMovementId,
        String sourceDocType,
        Long sourceDocId,
        Instant createdAt
) {
}
