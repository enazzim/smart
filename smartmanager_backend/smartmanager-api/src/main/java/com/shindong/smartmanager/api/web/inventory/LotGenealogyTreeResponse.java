package com.shindong.smartmanager.api.web.inventory;

import com.shindong.smartmanager.application.inventory.LotGenealogyTreeNode;
import java.math.BigDecimal;
import java.util.List;

public record LotGenealogyTreeResponse(
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
        List<LotGenealogyTreeResponse> children
) {
    public static LotGenealogyTreeResponse from(LotGenealogyTreeNode node) {
        return new LotGenealogyTreeResponse(
                node.depth(),
                node.lotId(),
                node.lotNo(),
                node.itemId(),
                node.itemNo(),
                node.itemName(),
                node.qty(),
                node.sourceDocType(),
                node.sourceDocId(),
                node.truncated(),
                node.children().stream().map(LotGenealogyTreeResponse::from).toList()
        );
    }
}
