package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.application.production.WorkCenterLoadQuery;
import com.shindong.smartmanager.infrastructure.application.SchedulingApplicationService;
import java.time.LocalDate;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/production/scheduling")
public class SchedulingController {

    private final SchedulingApplicationService schedulingApplicationService;

    public SchedulingController(SchedulingApplicationService schedulingApplicationService) {
        this.schedulingApplicationService = schedulingApplicationService;
    }

    @GetMapping("/work-center-load")
    @PreAuthorize("hasAnyAuthority('production:scheduling:read', 'production:work-plan:read')")
    public WorkCenterLoadResponse queryWorkCenterLoad(
            @RequestParam(required = false) Long workCenterId,
            @RequestParam @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate from,
            @RequestParam @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate to
    ) {
        return WorkCenterLoadResponse.from(
                schedulingApplicationService.queryWorkCenterLoad(
                        new WorkCenterLoadQuery(workCenterId, from, to)
                )
        );
    }
}
