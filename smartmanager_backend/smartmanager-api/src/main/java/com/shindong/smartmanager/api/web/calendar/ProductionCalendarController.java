package com.shindong.smartmanager.api.web.calendar;

import com.shindong.smartmanager.api.security.BasisAuthorize;
import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.calendar.ProductionCalendarEffectiveDayView;
import com.shindong.smartmanager.application.calendar.ProductionCalendarUpsertCommand;
import com.shindong.smartmanager.application.calendar.ProductionCalendarView;
import com.shindong.smartmanager.infrastructure.application.CalendarQueryApplicationService;
import com.shindong.smartmanager.infrastructure.application.ProductionCalendarApplicationService;
import jakarta.validation.Valid;
import jakarta.validation.constraints.Max;
import jakarta.validation.constraints.Min;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/basis/production-calendars")
@BasisAuthorize.ProductionCalendarRead
public class ProductionCalendarController {

    private final ProductionCalendarApplicationService productionCalendarApplicationService;
    private final CalendarQueryApplicationService calendarQueryApplicationService;

    public ProductionCalendarController(
            ProductionCalendarApplicationService productionCalendarApplicationService,
            CalendarQueryApplicationService calendarQueryApplicationService
    ) {
        this.productionCalendarApplicationService = productionCalendarApplicationService;
        this.calendarQueryApplicationService = calendarQueryApplicationService;
    }

    @GetMapping
    public List<ProductionCalendarResponse> list(
            @RequestParam int year,
            @RequestParam @Min(1) @Max(12) int month
    ) {
        return productionCalendarApplicationService.listByYearMonth(year, month).stream()
                .map(ProductionCalendarResponse::from)
                .toList();
    }

    @GetMapping("/effective")
    public List<ProductionCalendarEffectiveDayResponse> listEffective(
            @RequestParam int year,
            @RequestParam @Min(1) @Max(12) int month
    ) {
        return calendarQueryApplicationService.listProductionCalendarEffective(year, month).stream()
                .map(ProductionCalendarEffectiveDayResponse::from)
                .toList();
    }

    @GetMapping("/{id:\\d+}")
    public ProductionCalendarResponse get(@PathVariable long id) {
        return ProductionCalendarResponse.from(productionCalendarApplicationService.getById(id));
    }

    @PutMapping("/by-date/{calendarDate}")
    @BasisAuthorize.ProductionCalendarWrite
    public ProductionCalendarResponse upsertByDate(
            @PathVariable @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate calendarDate,
            @Valid @RequestBody UpsertProductionCalendarRequest request
    ) {
        String actor = SecurityUtils.requireLoginId();
        ProductionCalendarUpsertCommand command = new ProductionCalendarUpsertCommand(
                request.workTime(),
                request.content()
        );
        return ProductionCalendarResponse.from(
                productionCalendarApplicationService.upsertByDate(calendarDate, command, actor)
        );
    }

    @DeleteMapping("/by-date/{calendarDate}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @BasisAuthorize.ProductionCalendarWrite
    public void deleteByDate(
            @PathVariable @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate calendarDate
    ) {
        productionCalendarApplicationService.deleteByDate(calendarDate, SecurityUtils.requireLoginId());
    }

    public record UpsertProductionCalendarRequest(
            @Min(0) @Max(1440) int workTime,
            String content
    ) {
    }

    public record ProductionCalendarResponse(
            long id,
            LocalDate calendarDate,
            int workTime,
            String content
    ) {
        static ProductionCalendarResponse from(ProductionCalendarView view) {
            return new ProductionCalendarResponse(
                    view.id(),
                    view.calendarDate(),
                    view.workTime(),
                    view.content()
            );
        }
    }

    public record ProductionCalendarEffectiveDayResponse(
            LocalDate calendarDate,
            int effectiveWorkTime,
            boolean registered,
            boolean autoOffDay,
            Integer registeredWorkTime,
            String content
    ) {
        static ProductionCalendarEffectiveDayResponse from(ProductionCalendarEffectiveDayView view) {
            return new ProductionCalendarEffectiveDayResponse(
                    view.calendarDate(),
                    view.effectiveWorkTime(),
                    view.registered(),
                    view.autoOffDay(),
                    view.registeredWorkTime(),
                    view.content()
            );
        }
    }
}
