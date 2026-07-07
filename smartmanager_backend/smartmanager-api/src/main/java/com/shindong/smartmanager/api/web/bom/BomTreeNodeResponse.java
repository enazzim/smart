package com.shindong.smartmanager.api.web.bom;

import com.shindong.smartmanager.application.bom.BomTreeNode;
import com.shindong.smartmanager.application.bom.BomVendorPriceView;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import java.math.BigDecimal;
import java.util.List;

public record BomTreeNodeResponse(
        String itemNum,
        String itemName,
        PropertyClassification propertyClassification,
        int level,
        BigDecimal quantity,
        List<BomVendorPriceResponse> outsourcePrices,
        List<BomVendorPriceResponse> purchasePrices,
        List<BomTreeNodeResponse> children
) {
    static BomTreeNodeResponse from(BomTreeNode node) {
        return new BomTreeNodeResponse(
                node.itemNum(),
                node.itemName(),
                node.propertyClassification(),
                node.level(),
                node.quantity(),
                node.outsourcePrices().stream().map(BomVendorPriceResponse::from).toList(),
                node.purchasePrices().stream().map(BomVendorPriceResponse::from).toList(),
                node.children().stream().map(BomTreeNodeResponse::from).toList()
        );
    }

    public record BomVendorPriceResponse(
            String partnerName,
            BigDecimal unitPrice,
            String detail
    ) {
        static BomVendorPriceResponse from(BomVendorPriceView view) {
            return new BomVendorPriceResponse(view.partnerName(), view.unitPrice(), view.detail());
        }
    }
}
