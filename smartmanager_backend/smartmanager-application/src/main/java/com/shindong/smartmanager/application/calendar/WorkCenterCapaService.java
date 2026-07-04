package com.shindong.smartmanager.application.calendar;

import java.time.LocalDate;

public class WorkCenterCapaService {

    private final WorkCenterCalendarService workCenterCalendarService;
    private final WorkCenterCapaLookup workCenterCapaLookup;

    public WorkCenterCapaService(
            WorkCenterCalendarService workCenterCalendarService,
            WorkCenterCapaLookup workCenterCapaLookup
    ) {
        this.workCenterCalendarService = workCenterCalendarService;
        this.workCenterCapaLookup = workCenterCapaLookup;
    }

    public WorkCenterCapaView resolveCapa(long workCenterId, LocalDate calendarDate) {
        WorkCenterCapaProfile profile = workCenterCapaLookup.findActiveCapaProfile(workCenterId);
        int effectiveMinutes = workCenterCalendarService.resolveEffectiveMinutes(workCenterId, calendarDate);
        int capaMinutes = CapaCalculator.calculate(
                effectiveMinutes,
                profile.capacityDistinction(),
                profile.retentionStaff()
        );
        return new WorkCenterCapaView(
                workCenterId,
                calendarDate,
                effectiveMinutes,
                capaMinutes,
                profile.capacityDistinction(),
                profile.retentionStaff()
        );
    }
}
