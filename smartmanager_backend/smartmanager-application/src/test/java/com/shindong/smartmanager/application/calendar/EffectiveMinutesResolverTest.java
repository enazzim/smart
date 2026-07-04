package com.shindong.smartmanager.application.calendar;

import static org.junit.jupiter.api.Assertions.assertEquals;

import java.time.LocalDate;
import org.junit.jupiter.api.Test;

class EffectiveMinutesResolverTest {

    @Test
    void overrideTakesPrecedence() {
        int result = EffectiveMinutesResolver.resolve(720, 480, 600, false);
        assertEquals(720, result);
    }

    @Test
    void baseCalendarBeforeFallback() {
        int result = EffectiveMinutesResolver.resolve(null, 0, 600, false);
        assertEquals(0, result);
    }

    @Test
    void autoOffDayWhenUnregistered() {
        int result = EffectiveMinutesResolver.resolve(null, null, 480, true);
        assertEquals(0, result);
    }

    @Test
    void operationTimeFallbackOnWeekday() {
        int result = EffectiveMinutesResolver.resolve(null, null, 600, false);
        assertEquals(600, result);
    }

    @Test
    void capaTimeWorkers() {
        assertEquals(960, CapaCalculator.calculate(480, "TIME_WORKERS", 2));
    }

    @Test
    void capaTimeDefault() {
        assertEquals(480, CapaCalculator.calculate(480, "TIME", 3));
    }

    @Test
    void weekendIsAutoOffDay() {
        assertEquals(true, NonWorkingDayPolicy.isWeekend(LocalDate.of(2026, 7, 4)));
        assertEquals(false, NonWorkingDayPolicy.isWeekend(LocalDate.of(2026, 7, 6)));
    }
}
