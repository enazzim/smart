package com.shindong.smartmanager.application.purchase;

import java.time.LocalDate;
import java.util.List;

public record PurchaseOrderPrintView(
        String orderNo,
        LocalDate orderDate,
        String partnerName,
        String partnerBusinessRegNo,
        String issuerCompanyName,
        String issuerAddress,
        String issuerPhone,
        String issuerFax,
        String orderManagerName,
        List<PurchaseOrderPrintLineView> lines,
        java.math.BigDecimal totalAmount
) {
}
