package com.shindong.smartmanager.application.outsource;

import java.time.LocalDate;

public record OutsourcingReceiptListCriteria(
        String partnerName,
        LocalDate receiptDateFrom,
        LocalDate receiptDateTo,
        String itemNo,
        String itemName
) {
}
