package com.shindong.smartmanager.application.closing;

import static org.junit.jupiter.api.Assertions.assertEquals;

import java.time.LocalDate;
import org.junit.jupiter.api.Test;

class FiscalCalendarServiceTest {

    private final FiscalCalendarService service = new FiscalCalendarService();

    @Test
    void resolvePeriodOnCutoverDay() {
        FiscalPeriod period = service.resolvePeriod(LocalDate.of(2026, 6, 25));
        assertEquals(2026, period.fiscalYear());
        assertEquals(6, period.fiscalMonth());
    }

    @Test
    void resolvePeriodAfterCutoverDay() {
        FiscalPeriod period = service.resolvePeriod(LocalDate.of(2026, 6, 26));
        assertEquals(2026, period.fiscalYear());
        assertEquals(7, period.fiscalMonth());
    }

    @Test
    void resolvePeriodYearRollover() {
        FiscalPeriod period = service.resolvePeriod(LocalDate.of(2026, 12, 26));
        assertEquals(2027, period.fiscalYear());
        assertEquals(1, period.fiscalMonth());
    }

    @Test
    void resolvePeriodWithCustomCutoverDay() {
        FiscalCalendarService custom = new FiscalCalendarService(20);
        FiscalPeriod onCutover = custom.resolvePeriod(LocalDate.of(2026, 6, 20));
        assertEquals(2026, onCutover.fiscalYear());
        assertEquals(6, onCutover.fiscalMonth());

        FiscalPeriod afterCutover = custom.resolvePeriod(LocalDate.of(2026, 6, 21));
        assertEquals(2026, afterCutover.fiscalYear());
        assertEquals(7, afterCutover.fiscalMonth());
    }

    @Test
    void resolvePeriodWithEndOfMonthSetting() {
        FiscalCalendarService lastDayService = new FiscalCalendarService(FiscalCutoverPolicy.VALUE_LAST);

        FiscalPeriod feb28 = lastDayService.resolvePeriod(LocalDate.of(2026, 2, 28));
        assertEquals(2026, feb28.fiscalYear());
        assertEquals(2, feb28.fiscalMonth());

        FiscalPeriod mar1 = lastDayService.resolvePeriod(LocalDate.of(2026, 3, 1));
        assertEquals(2026, mar1.fiscalYear());
        assertEquals(3, mar1.fiscalMonth());

        FiscalPeriod apr30 = lastDayService.resolvePeriod(LocalDate.of(2026, 4, 30));
        assertEquals(2026, apr30.fiscalYear());
        assertEquals(4, apr30.fiscalMonth());
    }

    @Test
    void comparePeriods() {
        FiscalPeriod earlier = new FiscalPeriod(2026, 5);
        FiscalPeriod later = new FiscalPeriod(2026, 6);
        assertEquals(-1, service.compare(earlier, later));
        assertEquals(1, service.compare(later, earlier));
    }

    @Test
    void toCalendarDateRangeWithDefaultCutover() {
        FiscalPeriodDateRange range = service.toCalendarDateRange(new FiscalPeriod(2024, 7));
        assertEquals(LocalDate.of(2024, 6, 26), range.startInclusive());
        assertEquals(LocalDate.of(2024, 7, 25), range.endInclusive());
    }

    @Test
    void toCalendarDateRangeWithEndOfMonthSetting() {
        FiscalCalendarService lastDayService = new FiscalCalendarService(FiscalCutoverPolicy.VALUE_LAST);
        FiscalPeriodDateRange range = lastDayService.toCalendarDateRange(new FiscalPeriod(2024, 7));
        assertEquals(LocalDate.of(2024, 7, 1), range.startInclusive());
        assertEquals(LocalDate.of(2024, 7, 31), range.endInclusive());
    }
}
