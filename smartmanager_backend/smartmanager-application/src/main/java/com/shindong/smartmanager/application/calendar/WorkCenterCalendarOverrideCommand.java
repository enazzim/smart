package com.shindong.smartmanager.application.calendar;

import java.time.LocalDate;

public record WorkCenterCalendarOverrideCommand(
        long workCenterId,
        LocalDate calendarDate,
        int workTime,
        String content
) {
}
