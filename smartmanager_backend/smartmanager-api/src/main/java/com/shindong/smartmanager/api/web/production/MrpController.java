package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.infrastructure.application.MrpApplicationService;
import java.util.List;
import java.util.Optional;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/production/mrp")
public class MrpController {

    private final MrpApplicationService mrpApplicationService;

    public MrpController(MrpApplicationService mrpApplicationService) {
        this.mrpApplicationService = mrpApplicationService;
    }

    @GetMapping("/targets")
    @PreAuthorize("hasAuthority('production:mrp:read')")
    public List<ProductionPlanResponse> listTargets() {
        return mrpApplicationService.listCalculationTargets().stream()
                .map(ProductionPlanResponse::from)
                .toList();
    }

    @GetMapping("/runs")
    @PreAuthorize("hasAuthority('production:mrp:read')")
    public List<MrpRunResponse> listRuns() {
        return mrpApplicationService.listRuns().stream()
                .map(MrpRunResponse::from)
                .toList();
    }

    @GetMapping("/runs/{id}")
    @PreAuthorize("hasAuthority('production:mrp:read')")
    public MrpRunResponse getRun(@PathVariable long id) {
        return MrpRunResponse.from(mrpApplicationService.getRun(id));
    }

    @GetMapping("/runs/{id}/lines")
    @PreAuthorize("hasAuthority('production:mrp:read')")
    public List<MaterialRequirementLineResponse> listLinesByRun(@PathVariable long id) {
        return mrpApplicationService.listLinesByRun(id).stream()
                .map(MaterialRequirementLineResponse::from)
                .toList();
    }

    @GetMapping("/lines")
    @PreAuthorize("hasAuthority('production:mrp:read')")
    public List<MaterialRequirementLineResponse> listAllLines() {
        return mrpApplicationService.listAllLines().stream()
                .map(MaterialRequirementLineResponse::from)
                .toList();
    }

    @GetMapping("/lines/grouped")
    @PreAuthorize("hasAuthority('production:mrp:read')")
    public MaterialRequirementGroupedResponse listGroupedLines(
            @RequestParam(required = false) Long runId
    ) {
        return MaterialRequirementGroupedResponse.from(
                mrpApplicationService.listGroupedLines(Optional.ofNullable(runId))
        );
    }

    @PostMapping("/calculate")
    @PreAuthorize("hasAuthority('production:mrp:write')")
    public MrpRunResponse calculate(@RequestBody MrpCalculateRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        List<Long> planIds = request != null && request.productionPlanIds() != null
                ? request.productionPlanIds()
                : List.of();
        return MrpRunResponse.from(mrpApplicationService.calculate(planIds, principal.loginId()));
    }

    @PostMapping("/runs/{id}/cancel")
    @PreAuthorize("hasAuthority('production:mrp:write')")
    public MrpRunResponse cancelRun(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        return MrpRunResponse.from(mrpApplicationService.cancelRun(id, principal.loginId()));
    }

    @PostMapping("/plans/{productionPlanId}/cancel")
    @PreAuthorize("hasAuthority('production:mrp:write')")
    public MrpCancelPlanResponse cancelPlan(@PathVariable long productionPlanId) {
        var principal = SecurityUtils.requirePrincipal();
        return MrpCancelPlanResponse.from(
                mrpApplicationService.cancelPlan(productionPlanId, principal.loginId())
        );
    }

    @PostMapping("/lines/{lineId}/cancel")
    @PreAuthorize("hasAuthority('production:mrp:write')")
    public MaterialRequirementLineResponse cancelLine(@PathVariable long lineId) {
        var principal = SecurityUtils.requirePrincipal();
        return MaterialRequirementLineResponse.from(
                mrpApplicationService.cancelLine(lineId, principal.loginId())
        );
    }
}
