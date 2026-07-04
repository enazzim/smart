package com.shindong.smartmanager.api.web.system.closing;

import com.shindong.smartmanager.application.closing.MonthClosingView;
import java.time.Instant;

public record MonthClosingResponse(
        long id,
        int fiscalYear,
        int fiscalMonth,
        Instant closedAt,
        String closedBy,
        String closedById
) {

    public static MonthClosingResponse from(MonthClosingView view) {
        return new MonthClosingResponse(
                view.id(),
                view.fiscalYear(),
                view.fiscalMonth(),
                view.closedAt(),
                view.closedBy(),
                view.closedById()
        );
    }
}
