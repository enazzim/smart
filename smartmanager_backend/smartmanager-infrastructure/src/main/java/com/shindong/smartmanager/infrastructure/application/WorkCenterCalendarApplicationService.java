package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.calendar.EffectiveCalendarDayView;
import com.shindong.smartmanager.application.calendar.WorkCenterCalendarOverrideCommand;
import com.shindong.smartmanager.application.calendar.WorkCenterCalendarOverrideView;
import com.shindong.smartmanager.application.calendar.WorkCenterCalendarService;
import java.time.LocalDate;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class WorkCenterCalendarApplicationService {

    private final WorkCenterCalendarService workCenterCalendarService;

    public WorkCenterCalendarApplicationService(WorkCenterCalendarService workCenterCalendarService) {
        this.workCenterCalendarService = workCenterCalendarService;
    }

    @Transactional
    public EffectiveCalendarDayView upsertOverride(
            WorkCenterCalendarOverrideCommand command,
            String actorUserId
    ) {
        return workCenterCalendarService.upsertOverride(command, actorUserId);
    }

    @Transactional
    public void deleteOverride(long workCenterId, LocalDate calendarDate, String actorUserId) {
        workCenterCalendarService.deleteOverride(workCenterId, calendarDate, actorUserId);
    }

    @Transactional(readOnly = true)
    public List<WorkCenterCalendarOverrideView> listOverrides(long workCenterId, int year, int month) {
        return workCenterCalendarService.listOverrides(workCenterId, year, month);
    }

    @Transactional(readOnly = true)
    public List<EffectiveCalendarDayView> listEffective(long workCenterId, int year, int month) {
        return workCenterCalendarService.listEffective(workCenterId, year, month);
    }
}
