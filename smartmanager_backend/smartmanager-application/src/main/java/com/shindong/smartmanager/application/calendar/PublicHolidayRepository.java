package com.shindong.smartmanager.application.calendar;

import java.time.LocalDate;
import java.util.List;
import java.util.Set;

public interface PublicHolidayRepository {

    Set<LocalDate> findHolidayDatesByYear(int year);

    List<PublicHolidayView> findByYear(int year);
}
