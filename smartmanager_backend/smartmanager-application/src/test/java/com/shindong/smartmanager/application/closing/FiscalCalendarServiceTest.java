package com.shindong.smartmanager.application.closing;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;

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
    void comparePeriods() {
        FiscalPeriod earlier = new FiscalPeriod(2026, 5);
        FiscalPeriod later = new FiscalPeriod(2026, 6);
        assertEquals(-1, service.compare(earlier, later));
        assertEquals(1, service.compare(later, earlier));
    }
}
