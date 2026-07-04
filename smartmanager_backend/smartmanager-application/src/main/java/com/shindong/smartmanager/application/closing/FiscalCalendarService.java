package com.shindong.smartmanager.application.closing;

import java.time.LocalDate;

/**
 * 회계월 25일 규칙: 거래일 day &lt;= 25 → 해당 달, day &gt; 25 → 다음 달 회계월.
 */
public class FiscalCalendarService {

    public static final int FISCAL_CUTOVER_DAY = 25;

    public FiscalPeriod resolvePeriod(LocalDate date) {
        if (date.getDayOfMonth() <= FISCAL_CUTOVER_DAY) {
            return new FiscalPeriod(date.getYear(), date.getMonthValue());
        }
        LocalDate nextMonth = date.plusMonths(1);
        return new FiscalPeriod(nextMonth.getYear(), nextMonth.getMonthValue());
    }

    public int compare(FiscalPeriod left, FiscalPeriod right) {
        if (left.fiscalYear() != right.fiscalYear()) {
            return Integer.compare(left.fiscalYear(), right.fiscalYear());
        }
        return Integer.compare(left.fiscalMonth(), right.fiscalMonth());
    }
}
