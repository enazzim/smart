package com.shindong.smartmanager.application.closing;

import java.time.LocalDate;
import java.time.YearMonth;

/**
 * 매입마감일 설정: 고정 N일(1~31) 또는 매월 말일({@value #VALUE_LAST}).
 */
public final class FiscalCutoverPolicy {

    public static final String VALUE_LAST = "LAST";
    public static final String DEFAULT_SETTING_VALUE = "25";

    private FiscalCutoverPolicy() {
    }

    public static String normalizeSettingValue(String value) {
        if (value == null || value.isBlank()) {
            throw new IllegalArgumentException("매입마감일을 입력하세요.");
        }
        String trimmed = value.trim();
        if (VALUE_LAST.equalsIgnoreCase(trimmed) || "END_OF_MONTH".equalsIgnoreCase(trimmed) || "말일".equals(trimmed)) {
            return VALUE_LAST;
        }
        int day = parseFixedDay(trimmed);
        return String.valueOf(day);
    }

    public static int cutoverDayFor(LocalDate date, String settingValue) {
        if (VALUE_LAST.equalsIgnoreCase(settingValue != null ? settingValue.trim() : "")) {
            return date.lengthOfMonth();
        }
        return parseFixedDay(settingValue);
    }

    public static int cutoverDayFor(YearMonth yearMonth, String settingValue) {
        return cutoverDayFor(yearMonth.atEndOfMonth(), settingValue);
    }

    private static int parseFixedDay(String value) {
        if (value == null || value.isBlank()) {
            throw new IllegalArgumentException("매입마감일을 입력하세요.");
        }
        int day;
        try {
            day = Integer.parseInt(value.trim());
        } catch (NumberFormatException ex) {
            throw new IllegalArgumentException("매입마감일은 1~31 사이 숫자이거나 LAST(매월 말일)여야 합니다.");
        }
        if (day < 1 || day > 31) {
            throw new IllegalArgumentException("매입마감일은 1~31 사이여야 합니다.");
        }
        return day;
    }
}
