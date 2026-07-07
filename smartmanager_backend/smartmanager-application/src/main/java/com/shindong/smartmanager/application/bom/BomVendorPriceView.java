package com.shindong.smartmanager.application.bom;

import java.math.BigDecimal;

public record BomVendorPriceView(
        String partnerName,
        BigDecimal unitPrice,
        String detail
) {
}
