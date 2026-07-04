package com.shindong.smartmanager.application.calendar;

import java.time.LocalDate;

public record WorkCenterCapaView(
        long workCenterId,
        LocalDate calendarDate,
        int effectiveMinutes,
        int capaMinutes,
        String capacityDistinction,
        int retentionStaff
) {
}
