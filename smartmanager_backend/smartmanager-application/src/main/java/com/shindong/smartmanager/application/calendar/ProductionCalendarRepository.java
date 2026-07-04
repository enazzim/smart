package com.shindong.smartmanager.application.calendar;

import java.time.LocalDate;
import java.util.List;
import java.util.Optional;

public interface ProductionCalendarRepository {

    ProductionCalendarView upsertByDate(LocalDate calendarDate, ProductionCalendarUpsertCommand command, String actorUserId);

    void softDeleteByDate(LocalDate calendarDate, String actorUserId);

    List<ProductionCalendarView> findActiveByYearMonth(int year, int month);

    Optional<ProductionCalendarView> findActiveById(long id);

    Optional<ProductionCalendarView> findActiveByDate(LocalDate calendarDate);
}
