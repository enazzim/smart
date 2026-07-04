package com.shindong.smartmanager.api.web.calendar;

import com.shindong.smartmanager.application.calendar.ProductionCalendarUpsertCommand;
import com.shindong.smartmanager.application.calendar.ProductionCalendarView;
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
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/basis/production-calendars")
public class ProductionCalendarController {

    private static final String DEFAULT_ACTOR = "local-dev";

    private final ProductionCalendarApplicationService productionCalendarApplicationService;

    public ProductionCalendarController(ProductionCalendarApplicationService productionCalendarApplicationService) {
        this.productionCalendarApplicationService = productionCalendarApplicationService;
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

    @GetMapping("/{id}")
    public ProductionCalendarResponse get(@PathVariable long id) {
        return ProductionCalendarResponse.from(productionCalendarApplicationService.getById(id));
    }

    @PutMapping("/by-date/{calendarDate}")
    public ProductionCalendarResponse upsertByDate(
            @PathVariable @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate calendarDate,
            @Valid @RequestBody UpsertProductionCalendarRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
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
    public void deleteByDate(
            @PathVariable @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate calendarDate,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String actor = actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
        productionCalendarApplicationService.deleteByDate(calendarDate, actor);
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
}
