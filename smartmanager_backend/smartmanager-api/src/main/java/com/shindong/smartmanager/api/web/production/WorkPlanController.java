package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.production.WorkPlanListCriteria;
import com.shindong.smartmanager.infrastructure.application.WorkPlanApplicationService;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotEmpty;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/production/work-plans")
public class WorkPlanController {

    private final WorkPlanApplicationService workPlanApplicationService;

    public WorkPlanController(WorkPlanApplicationService workPlanApplicationService) {
        this.workPlanApplicationService = workPlanApplicationService;
    }

    @GetMapping("/planning-targets")
    @PreAuthorize("hasAuthority('production:work-plan:read')")
    public List<ProductionPlanResponse> listPlanningTargets() {
        return workPlanApplicationService.listPlanningTargets().stream()
                .map(ProductionPlanResponse::from)
                .toList();
    }

    @GetMapping
    @PreAuthorize("hasAuthority('production:work-plan:read')")
    public List<WorkPlanResponse> list(
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) String processName,
            @RequestParam(required = false) String workCenterName,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate planStartDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate planStartDateTo
    ) {
        WorkPlanListCriteria criteria = new WorkPlanListCriteria(
                itemNo,
                itemName,
                processName,
                workCenterName,
                planStartDateFrom,
                planStartDateTo
        );
        return workPlanApplicationService.list(criteria).stream()
                .map(WorkPlanResponse::from)
                .toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('production:work-plan:write')")
    public List<WorkPlanResponse> create(@Valid @RequestBody CreateWorkPlanRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return workPlanApplicationService.createPlans(request.productionPlanIds(), principal.loginId()).stream()
                .map(WorkPlanResponse::from)
                .toList();
    }

    @PostMapping("/production-plans/{productionPlanId}/cancel")
    @PreAuthorize("hasAuthority('production:work-plan:write')")
    public WorkPlanCancelPlanResponse cancelPlan(@PathVariable long productionPlanId) {
        var principal = SecurityUtils.requirePrincipal();
        return WorkPlanCancelPlanResponse.from(
                workPlanApplicationService.cancelPlan(productionPlanId, principal.loginId())
        );
    }

    @PostMapping("/{id}/cancel")
    @PreAuthorize("hasAuthority('production:work-plan:write')")
    public WorkPlanResponse cancelLine(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        return WorkPlanResponse.from(workPlanApplicationService.cancelLine(id, principal.loginId()));
    }

    public record CreateWorkPlanRequest(
            @NotEmpty List<Long> productionPlanIds
    ) {
    }
}
