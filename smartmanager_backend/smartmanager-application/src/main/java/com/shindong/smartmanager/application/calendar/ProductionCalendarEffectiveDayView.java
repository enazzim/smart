package com.shindong.smartmanager.application.calendar;

import java.time.LocalDate;

public record ProductionCalendarEffectiveDayView(
        LocalDate calendarDate,
        int effectiveWorkTime,
        boolean registered,
        boolean autoOffDay,
        Integer registeredWorkTime,
        String content
) {
}
