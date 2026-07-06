package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.production.WorkOrderListCriteria;
import com.shindong.smartmanager.infrastructure.application.WorkOrderApplicationService;
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
@RequestMapping("/api/v1/production/work-orders")
public class WorkOrderController {

    private final WorkOrderApplicationService workOrderApplicationService;

    public WorkOrderController(WorkOrderApplicationService workOrderApplicationService) {
        this.workOrderApplicationService = workOrderApplicationService;
    }

    @GetMapping("/order-targets")
    @PreAuthorize("hasAuthority('production:work-order:read')")
    public List<WorkOrderResponse> listOrderTargets() {
        return workOrderApplicationService.listOrderTargets().stream()
                .map(WorkOrderResponse::from)
                .toList();
    }

    @GetMapping
    @PreAuthorize("hasAuthority('production:work-order:read')")
    public List<WorkOrderResponse> list(
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) String processName,
            @RequestParam(required = false) String workCenterName,
            @RequestParam(required = false) String orderNum,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate planStartDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate planStartDateTo
    ) {
        return workOrderApplicationService.list(new WorkOrderListCriteria(
                itemNo, itemName, processName, workCenterName, orderNum, planStartDateFrom, planStartDateTo
        )).stream().map(WorkOrderResponse::from).toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('production:work-order:write')")
    public List<WorkOrderResponse> create(@Valid @RequestBody CreateWorkOrderRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return workOrderApplicationService.createOrders(request.workPlanIds(), principal.loginId()).stream()
                .map(WorkOrderResponse::from)
                .toList();
    }

    @PostMapping("/{id}/cancel")
    @PreAuthorize("hasAuthority('production:work-order:write')")
    public WorkOrderResponse cancel(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        return WorkOrderResponse.from(workOrderApplicationService.cancel(id, principal.loginId()));
    }

    public record CreateWorkOrderRequest(@NotEmpty List<Long> workPlanIds) {
    }
}
