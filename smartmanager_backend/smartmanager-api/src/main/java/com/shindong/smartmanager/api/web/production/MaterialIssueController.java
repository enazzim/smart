package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.production.CreateMaterialIssueCommand;
import com.shindong.smartmanager.application.production.MaterialIssueLineCommand;
import com.shindong.smartmanager.application.production.MaterialIssueListCriteria;
import com.shindong.smartmanager.infrastructure.application.MaterialIssueApplicationService;
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
@RequestMapping("/api/v1/production/material-issues")
public class MaterialIssueController {

    private final MaterialIssueApplicationService materialIssueApplicationService;

    public MaterialIssueController(MaterialIssueApplicationService materialIssueApplicationService) {
        this.materialIssueApplicationService = materialIssueApplicationService;
    }

    @GetMapping("/issue-targets")
    @PreAuthorize("hasAuthority('production:material-issue:read')")
    public List<WorkOrderResponse> listIssueTargets() {
        return materialIssueApplicationService.listIssueTargets().stream()
                .map(WorkOrderResponse::from)
                .toList();
    }

    @GetMapping
    @PreAuthorize("hasAuthority('production:material-issue:read')")
    public List<MaterialIssueResponse> list(
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) String orderNum,
            @RequestParam(required = false) String issueNum,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate issueDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate issueDateTo
    ) {
        return materialIssueApplicationService.list(new MaterialIssueListCriteria(
                itemNo, itemName, orderNum, issueNum, issueDateFrom, issueDateTo
        )).stream().map(MaterialIssueResponse::from).toList();
    }

    @GetMapping("/consumption-preview")
    @PreAuthorize("hasAuthority('production:material-issue:read')")
    public MaterialIssueConsumptionPreviewResponse getConsumptionPreview(
            @RequestParam long workOrderId,
            @RequestParam(defaultValue = "0") BigDecimal goodQty
    ) {
        return MaterialIssueConsumptionPreviewResponse.from(
                materialIssueApplicationService.getConsumptionPreview(workOrderId, goodQty)
        );
    }

    @GetMapping("/issue-on-hand")
    @PreAuthorize("hasAuthority('production:material-issue:read')")
    public List<MaterialIssueOnHandResponse> listOnHand(
            @RequestParam long workOrderId,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate issueDate
    ) {
        LocalDate stockDate = issueDate != null ? issueDate : LocalDate.now();
        return materialIssueApplicationService.listOnHand(workOrderId, stockDate).stream()
                .map(MaterialIssueOnHandResponse::from)
                .toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('production:material-issue:write')")
    public MaterialIssueResponse create(@Valid @RequestBody CreateMaterialIssueRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return MaterialIssueResponse.from(materialIssueApplicationService.register(
                new CreateMaterialIssueCommand(
                        request.workOrderId(),
                        request.issueDate(),
                        request.lines().stream()
                                .map(line -> new MaterialIssueLineCommand(
                                        line.itemCompositionId(),
                                        line.issueQty()
                                ))
                                .toList()
                ),
                principal.loginId()
        ));
    }

    @PostMapping("/{id}/cancel")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('production:material-issue:write')")
    public void cancel(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        materialIssueApplicationService.cancel(id, principal.loginId());
    }

    public record CreateMaterialIssueRequest(
            @NotNull Long workOrderId,
            @NotNull LocalDate issueDate,
            @NotNull List<MaterialIssueLineRequest> lines
    ) {
    }

    public record MaterialIssueLineRequest(
            @NotNull Long itemCompositionId,
            @NotNull @PositiveOrZero BigDecimal issueQty
    ) {
    }
}
