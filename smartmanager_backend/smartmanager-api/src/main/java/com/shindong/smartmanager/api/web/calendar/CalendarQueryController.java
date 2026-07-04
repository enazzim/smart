package com.shindong.smartmanager.api.web.calendar;

import com.shindong.smartmanager.api.security.BasisAuthorize;
import com.shindong.smartmanager.application.calendar.PublicHolidayView;
import com.shindong.smartmanager.application.calendar.WorkCenterCapaView;
import com.shindong.smartmanager.infrastructure.application.CalendarQueryApplicationService;
import jakarta.validation.constraints.Max;
import jakarta.validation.constraints.Min;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/basis")
@BasisAuthorize.ProductionCalendarRead
public class CalendarQueryController {

    private final CalendarQueryApplicationService calendarQueryApplicationService;

    public CalendarQueryController(CalendarQueryApplicationService calendarQueryApplicationService) {
        this.calendarQueryApplicationService = calendarQueryApplicationService;
    }

    @GetMapping("/public-holidays")
    public List<PublicHolidayResponse> listPublicHolidays(@RequestParam int year) {
        return calendarQueryApplicationService.listPublicHolidays(year).stream()
                .map(PublicHolidayResponse::from)
                .toList();
    }

    @GetMapping("/work-centers/{workCenterId}/capa")
    public WorkCenterCapaResponse resolveCapa(
            @PathVariable long workCenterId,
            @RequestParam @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate date
    ) {
        return WorkCenterCapaResponse.from(
                calendarQueryApplicationService.resolveWorkCenterCapa(workCenterId, date)
        );
    }

    public record PublicHolidayResponse(
            LocalDate holidayDate,
            String holidayName
    ) {
        static PublicHolidayResponse from(PublicHolidayView view) {
            return new PublicHolidayResponse(view.holidayDate(), view.holidayName());
        }
    }

    public record WorkCenterCapaResponse(
            long workCenterId,
            LocalDate calendarDate,
            int effectiveMinutes,
            int capaMinutes,
            String capacityDistinction,
            int retentionStaff
    ) {
        static WorkCenterCapaResponse from(WorkCenterCapaView view) {
            return new WorkCenterCapaResponse(
                    view.workCenterId(),
                    view.calendarDate(),
                    view.effectiveMinutes(),
                    view.capaMinutes(),
                    view.capacityDistinction(),
                    view.retentionStaff()
            );
        }
    }
}
