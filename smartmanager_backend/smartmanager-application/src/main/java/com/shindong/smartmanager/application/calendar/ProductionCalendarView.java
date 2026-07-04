package com.shindong.smartmanager.application.calendar;

import java.time.LocalDate;

public record ProductionCalendarView(
        long id,
        LocalDate calendarDate,
        int workTime,
        String content
) {
}
