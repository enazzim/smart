package com.shindong.smartmanager.api.web.bom;

import com.shindong.smartmanager.application.bom.BomTreeNode;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;
import java.util.List;

public record BomTreeNodeResponse(
        String itemNum,
        String itemName,
        PropertyClassification propertyClassification,
        int level,
        BigDecimal quantity,
        List<BomTreeNodeResponse> children
) {
    static BomTreeNodeResponse from(BomTreeNode node) {
        return new BomTreeNodeResponse(
                node.itemNum(),
                node.itemName(),
                node.propertyClassification(),
                node.level(),
                node.quantity(),
                node.children().stream().map(BomTreeNodeResponse::from).toList()
        );
    }
}
