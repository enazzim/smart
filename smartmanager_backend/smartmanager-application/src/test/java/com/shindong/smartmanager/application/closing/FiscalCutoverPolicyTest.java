package com.shindong.smartmanager.application.closing;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;

import java.time.LocalDate;
import org.junit.jupiter.api.Test;

class FiscalCutoverPolicyTest {

    @Test
    void lastDayOfFebruaryInLeapYear() {
        assertEquals(29, FiscalCutoverPolicy.cutoverDayFor(LocalDate.of(2024, 2, 15), FiscalCutoverPolicy.VALUE_LAST));
    }

    @Test
    void lastDayOfFebruaryInCommonYear() {
        assertEquals(28, FiscalCutoverPolicy.cutoverDayFor(LocalDate.of(2026, 2, 10), FiscalCutoverPolicy.VALUE_LAST));
    }

    @Test
    void lastDayOfApril() {
        assertEquals(30, FiscalCutoverPolicy.cutoverDayFor(LocalDate.of(2026, 4, 1), FiscalCutoverPolicy.VALUE_LAST));
    }

    @Test
    void normalizeLastAliases() {
        assertEquals(FiscalCutoverPolicy.VALUE_LAST, FiscalCutoverPolicy.normalizeSettingValue("LAST"));
        assertEquals(FiscalCutoverPolicy.VALUE_LAST, FiscalCutoverPolicy.normalizeSettingValue("말일"));
    }

    @Test
    void rejectInvalidFixedDay() {
        assertThrows(IllegalArgumentException.class, () -> FiscalCutoverPolicy.normalizeSettingValue("32"));
    }
}
