package com.shindong.smartmanager.application.inventory;

import java.math.BigDecimal;
import java.util.List;

public record LotGenealogyTreeNode(
        int depth,
        long lotId,
        String lotNo,
        long itemId,
        String itemNo,
        String itemName,
        BigDecimal qty,
        String sourceDocType,
        Long sourceDocId,
        boolean truncated,
        List<LotGenealogyTreeNode> children
) {
}
