package com.shindong.smartmanager.application.outsource;

import java.time.LocalDate;

public record OutsourcingReceiptCandidateCriteria(
        String partnerName,
        String orderNo,
        LocalDate orderDateFrom,
        LocalDate orderDateTo,
        String itemNo,
        String itemName
) {
}
