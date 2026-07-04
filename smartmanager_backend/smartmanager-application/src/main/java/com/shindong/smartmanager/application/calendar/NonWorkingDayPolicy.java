package com.shindong.smartmanager.application.calendar;

import java.time.DayOfWeek;
import java.time.LocalDate;
import java.util.Set;

public final class NonWorkingDayPolicy {

    private NonWorkingDayPolicy() {
    }

    public static boolean isWeekend(LocalDate date) {
        DayOfWeek dayOfWeek = date.getDayOfWeek();
        return dayOfWeek == DayOfWeek.SATURDAY || dayOfWeek == DayOfWeek.SUNDAY;
    }

    public static boolean isAutoOffDay(LocalDate date, Set<LocalDate> publicHolidays) {
        return isWeekend(date) || publicHolidays.contains(date);
    }
}
