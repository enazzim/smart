package com.shindong.smartmanager.application.bom;

import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;
import java.util.List;

public record BomTreeNode(
        String itemNum,
        String itemName,
        PropertyClassification propertyClassification,
        int level,
        BigDecimal quantity,
        List<BomVendorPriceView> outsourcePrices,
        List<BomVendorPriceView> purchasePrices,
        List<BomTreeNode> children
) {
}
