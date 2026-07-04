package com.shindong.smartmanager.application.calendar;

import java.time.LocalDate;
import java.util.List;
import java.util.Optional;

public interface WorkCenterCalendarRepository {

    WorkCenterCalendarOverrideView upsertOverride(WorkCenterCalendarOverrideCommand command, String actorUserId);

    void softDeleteOverride(long workCenterId, LocalDate calendarDate, String actorUserId);

    List<WorkCenterCalendarOverrideView> findActiveOverrides(long workCenterId, int year, int month);

    Optional<WorkCenterCalendarOverrideView> findActiveOverride(long workCenterId, LocalDate calendarDate);
}
