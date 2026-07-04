package com.shindong.smartmanager.application.calendar;

import java.time.LocalDate;

public record WorkCenterCalendarOverrideView(
        long id,
        long workCenterId,
        String wcName,
        LocalDate calendarDate,
        int workTime,
        String content
) {
}
