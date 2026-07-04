package com.shindong.smartmanager.api.web.system.closing;

import com.shindong.smartmanager.application.closing.FiscalPeriodStatusView;
import java.time.LocalDate;

public record FiscalPeriodStatusResponse(
        LocalDate referenceDate,
        int fiscalYear,
        int fiscalMonth,
        boolean closed
) {

    public static FiscalPeriodStatusResponse from(FiscalPeriodStatusView view) {
        return new FiscalPeriodStatusResponse(
                view.referenceDate(),
                view.fiscalYear(),
                view.fiscalMonth(),
                view.closed()
        );
    }
}
