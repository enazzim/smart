package com.shindong.smartmanager.api.web.system.closing;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.closing.CloseMonthCommand;
import com.shindong.smartmanager.infrastructure.application.MonthClosingApplicationService;
import jakarta.validation.Valid;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/system/month-closings")
public class MonthClosingController {

    private final MonthClosingApplicationService monthClosingApplicationService;

    public MonthClosingController(MonthClosingApplicationService monthClosingApplicationService) {
        this.monthClosingApplicationService = monthClosingApplicationService;
    }

    @GetMapping
    @PreAuthorize("hasAnyAuthority('system:month-closing:read', 'system:month-closing:execute')")
    public List<MonthClosingResponse> list() {
        return monthClosingApplicationService.listClosed().stream()
                .map(MonthClosingResponse::from)
                .toList();
    }

    @GetMapping("/period")
    @PreAuthorize("hasAnyAuthority('system:month-closing:read', 'system:month-closing:execute')")
    public FiscalPeriodStatusResponse resolvePeriod(
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate date
    ) {
        LocalDate referenceDate = date != null ? date : LocalDate.now();
        return FiscalPeriodStatusResponse.from(monthClosingApplicationService.resolveStatus(referenceDate));
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('system:month-closing:execute')")
    public MonthClosingResponse close(@Valid @RequestBody CloseMonthClosingRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return MonthClosingResponse.from(monthClosingApplicationService.close(
                new CloseMonthCommand(request.fiscalYear(), request.fiscalMonth()),
                principal.loginId(),
                principal.loginId()
        ));
    }

    @DeleteMapping("/{fiscalYear}/{fiscalMonth}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('system:month-closing:execute')")
    public void reopen(
            @PathVariable int fiscalYear,
            @PathVariable int fiscalMonth
    ) {
        monthClosingApplicationService.reopen(new CloseMonthCommand(fiscalYear, fiscalMonth));
    }
}
