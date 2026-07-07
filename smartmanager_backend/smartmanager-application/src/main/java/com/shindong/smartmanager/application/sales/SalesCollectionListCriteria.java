package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesCollectionStatus;
import java.time.LocalDate;

public record SalesCollectionListCriteria(
        LocalDate collectionDateFrom,
        LocalDate collectionDateTo,
        String collectionNo,
        String partnerName,
        SalesCollectionStatus status,
        boolean excludeCancelled
) {
}
