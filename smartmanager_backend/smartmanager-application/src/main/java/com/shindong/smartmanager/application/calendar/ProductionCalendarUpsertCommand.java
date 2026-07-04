package com.shindong.smartmanager.application.calendar;

public record ProductionCalendarUpsertCommand(
        int workTime,
        String content
) {
}
