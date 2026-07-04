package com.shindong.smartmanager.api.web.calendar;

import com.shindong.smartmanager.api.security.BasisAuthorize;
import com.shindong.smartmanager.application.calendar.EffectiveCalendarDayView;
import com.shindong.smartmanager.application.calendar.WorkCenterCalendarOverrideCommand;
import com.shindong.smartmanager.application.calendar.WorkCenterCalendarOverrideView;
import com.shindong.smartmanager.infrastructure.application.WorkCenterCalendarApplicationService;
import jakarta.validation.Valid;
import jakarta.validation.constraints.Max;
import jakarta.validation.constraints.Min;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/basis/work-center-calendars")
@BasisAuthorize.ProductionCalendarRead
public class WorkCenterCalendarController {

    private static final String DEFAULT_ACTOR = "local-dev";

    private final WorkCenterCalendarApplicationService workCenterCalendarApplicationService;

    public WorkCenterCalendarController(WorkCenterCalendarApplicationService workCenterCalendarApplicationService) {
        this.workCenterCalendarApplicationService = workCenterCalendarApplicationService;
    }

    @GetMapping("/overrides")
    public List<WorkCenterCalendarOverrideResponse> listOverrides(
            @RequestParam long workCenterId,
            @RequestParam int year,
            @RequestParam @Min(1) @Max(12) int month
    ) {
        return workCenterCalendarApplicationService.listOverrides(workCenterId, year, month).stream()
                .map(WorkCenterCalendarOverrideResponse::from)
                .toList();
    }

    @GetMapping("/effective")
    public List<EffectiveCalendarDayResponse> listEffective(
            @RequestParam long workCenterId,
            @RequestParam int year,
            @RequestParam @Min(1) @Max(12) int month
    ) {
        return workCenterCalendarApplicationService.listEffective(workCenterId, year, month).stream()
                .map(EffectiveCalendarDayResponse::from)
                .toList();
    }

    @PutMapping("/overrides")
    @BasisAuthorize.ProductionCalendarWrite
    public EffectiveCalendarDayResponse upsertOverride(
            @Valid @RequestBody UpsertWorkCenterCalendarOverrideRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        WorkCenterCalendarOverrideCommand command = new WorkCenterCalendarOverrideCommand(
                request.workCenterId(),
                request.calendarDate(),
                request.workTime(),
                request.content()
        );
        return EffectiveCalendarDayResponse.from(
                workCenterCalendarApplicationService.upsertOverride(command, actor)
        );
    }

    @DeleteMapping("/overrides")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @BasisAuthorize.ProductionCalendarWrite
    public void deleteOverride(
            @RequestParam long workCenterId,
            @RequestParam @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate calendarDate,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        workCenterCalendarApplicationService.deleteOverride(workCenterId, calendarDate, actor);
    }

    public record UpsertWorkCenterCalendarOverrideRequest(
            long workCenterId,
            LocalDate calendarDate,
            @Min(0) @Max(1440) int workTime,
            String content
    ) {
    }

    public record WorkCenterCalendarOverrideResponse(
            long id,
            long workCenterId,
            String wcName,
            LocalDate calendarDate,
            int workTime,
            String content
    ) {
        static WorkCenterCalendarOverrideResponse from(WorkCenterCalendarOverrideView view) {
            return new WorkCenterCalendarOverrideResponse(
                    view.id(),
                    view.workCenterId(),
                    view.wcName(),
                    view.calendarDate(),
                    view.workTime(),
                    view.content()
            );
        }
    }

    public record EffectiveCalendarDayResponse(
            LocalDate calendarDate,
            int effectiveWorkTime,
            boolean isOverride,
            Integer baseWorkTime,
            String content
    ) {
        static EffectiveCalendarDayResponse from(EffectiveCalendarDayView view) {
            return new EffectiveCalendarDayResponse(
                    view.calendarDate(),
                    view.effectiveWorkTime(),
                    view.isOverride(),
                    view.baseWorkTime(),
                    view.content()
            );
        }
    }
}
