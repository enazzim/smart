package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public record OutsourcingOrderPrintView(
        String orderNos,
        LocalDate orderDate,
        String partnerName,
        String partnerBusinessRegNo,
        String partnerTelephone,
        String partnerFax,
        String issuerCompanyName,
        String issuerAddress,
        String issuerPhone,
        String issuerFax,
        String orderManagerName,
        List<OutsourcingOrderPrintLineView> lines,
        BigDecimal totalAmount
) {
}
