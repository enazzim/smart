package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.production.CreateWorkReportCommand;
import com.shindong.smartmanager.application.production.WorkReportIssueLineCommand;
import com.shindong.smartmanager.application.production.WorkReportListCriteria;
import com.shindong.smartmanager.infrastructure.application.WorkReportApplicationService;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.PositiveOrZero;
import java.math.BigDecimal;
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
@RequestMapping("/api/v1/production/work-reports")
public class WorkReportController {

    private final WorkReportApplicationService workReportApplicationService;

    public WorkReportController(WorkReportApplicationService workReportApplicationService) {
        this.workReportApplicationService = workReportApplicationService;
    }

    @GetMapping("/report-targets")
    @PreAuthorize("hasAuthority('production:work-report:read')")
    public List<WorkOrderResponse> listReportTargets() {
        return workReportApplicationService.listReportTargets().stream()
                .map(WorkOrderResponse::from)
                .toList();
    }

    @GetMapping
    @PreAuthorize("hasAuthority('production:work-report:read')")
    public List<WorkReportResponse> list(
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) String processName,
            @RequestParam(required = false) String orderNum,
            @RequestParam(required = false) String reportNum,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate reportDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate reportDateTo
    ) {
        return workReportApplicationService.list(new WorkReportListCriteria(
                itemNo, itemName, processName, orderNum, reportNum, reportDateFrom, reportDateTo
        )).stream().map(WorkReportResponse::from).toList();
    }

    @GetMapping("/issue-on-hand")
    @PreAuthorize("hasAuthority('production:work-report:read')")
    public List<MaterialIssueOnHandResponse> listIssueOnHand(
            @RequestParam long workOrderId,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate reportDate
    ) {
        return workReportApplicationService.listIssueOnHand(workOrderId, reportDate).stream()
                .map(MaterialIssueOnHandResponse::from)
                .toList();
    }

    @GetMapping("/consumption-status")
    @PreAuthorize("hasAuthority('production:work-report:read')")
    public WorkReportConsumptionStatusResponse getConsumptionStatus(
            @RequestParam long workOrderId,
            @RequestParam(defaultValue = "0") BigDecimal pendingGoodQty
    ) {
        return WorkReportConsumptionStatusResponse.from(
                workReportApplicationService.getConsumptionStatus(workOrderId, pendingGoodQty)
        );
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('production:work-report:write')")
    public WorkReportResponse create(@Valid @RequestBody CreateWorkReportRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return WorkReportResponse.from(workReportApplicationService.register(
                new CreateWorkReportCommand(
                        request.workOrderId(),
                        request.reportDate(),
                        request.goodQty(),
                        request.scrapQty() != null ? request.scrapQty() : BigDecimal.ZERO,
                        request.setupTime(),
                        request.runTime(),
                        request.workerName(),
                        request.issueLines() != null
                                ? request.issueLines().stream()
                                        .map(line -> new WorkReportIssueLineCommand(
                                                line.itemCompositionId(),
                                                line.itemId(),
                                                line.issueQty()
                                        ))
                                        .toList()
                                : List.of()
                ),
                principal.loginId()
        ));
    }

    @PostMapping("/{id}/cancel")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('production:work-report:write')")
    public void cancel(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        workReportApplicationService.cancel(id, principal.loginId());
    }

    public record WorkReportIssueLineRequest(
            Long itemCompositionId,
            @NotNull Long itemId,
            @NotNull @PositiveOrZero BigDecimal issueQty
    ) {
    }

    public record CreateWorkReportRequest(
            @NotNull Long workOrderId,
            @NotNull LocalDate reportDate,
            @NotNull @PositiveOrZero BigDecimal goodQty,
            @PositiveOrZero BigDecimal scrapQty,
            BigDecimal setupTime,
            BigDecimal runTime,
            String workerName,
            List<WorkReportIssueLineRequest> issueLines
    ) {
    }
}
