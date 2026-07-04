package com.shindong.smartmanager.application.calendar;

import java.time.LocalDate;

public record EffectiveCalendarDayView(
        LocalDate calendarDate,
        int effectiveWorkTime,
        boolean isOverride,
        Integer baseWorkTime,
        String content
) {
}
