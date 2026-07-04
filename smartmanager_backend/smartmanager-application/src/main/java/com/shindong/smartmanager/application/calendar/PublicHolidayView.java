package com.shindong.smartmanager.application.calendar;

import java.time.LocalDate;

public record PublicHolidayView(
        LocalDate holidayDate,
        String holidayName
) {
}
