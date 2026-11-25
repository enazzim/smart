package com.shindong.smartmanager.application.inventory;

import java.time.LocalDate;

public interface StocktakeHistoryQuery {

    boolean hasHistory(
            long itemId,
            long locationId,
            Long outputProcessId,
            Long inputProcessId,
            Long partnerId,
            int fiscalYear
    );

    boolean hasLaterMovementInSummedYears(
            long itemId,
            long locationId,
            Long outputProcessId,
            Long inputProcessId,
            Long partnerId,
            int fiscalYear,
            LocalDate stocktakeDate
    );
}
