package com.shindong.smartmanager.api.web.production;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.api.web.sales.SalesOrderLineListResponse;
import com.shindong.smartmanager.application.production.ProductionPlanCreateLineCommand;
import com.shindong.smartmanager.application.production.ProductionPlanListCriteria;
import com.shindong.smartmanager.domain.production.ProductionPlanStatus;
import com.shindong.smartmanager.infrastructure.application.ProductionPlanApplicationService;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/production")
public class ProductionPlanController {

    private final ProductionPlanApplicationService productionPlanApplicationService;

    public ProductionPlanController(ProductionPlanApplicationService productionPlanApplicationService) {
        this.productionPlanApplicationService = productionPlanApplicationService;
    }

    @GetMapping("/plan-candidates")
    @PreAuthorize("hasAuthority('production:plan:read')")
    public List<SalesOrderLineListResponse> listCandidates() {
        return productionPlanApplicationService.listCandidates().stream()
                .map(SalesOrderLineListResponse::from)
                .toList();
    }

    @GetMapping("/plans")
    @PreAuthorize("hasAuthority('production:plan:read')")
    public List<ProductionPlanResponse> list(
            @RequestParam(required = false) Long partnerId,
            @RequestParam(required = false) Long itemId,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate requestedDeliveryDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate requestedDeliveryDateTo,
            @RequestParam(required = false) ProductionPlanStatus status
    ) {
        ProductionPlanListCriteria criteria = new ProductionPlanListCriteria(
                partnerId,
                itemId,
                requestedDeliveryDateFrom,
                requestedDeliveryDateTo,
                status
        );
        return productionPlanApplicationService.list(criteria).stream()
                .map(ProductionPlanResponse::from)
                .toList();
    }

    @GetMapping("/plans/{id}")
    @PreAuthorize("hasAuthority('production:plan:read')")
    public ProductionPlanResponse get(@PathVariable long id) {
        return ProductionPlanResponse.from(productionPlanApplicationService.get(id));
    }

    @PostMapping("/plans")
    @PreAuthorize("hasAuthority('production:plan:write')")
    public List<ProductionPlanResponse> create(@RequestBody ProductionPlanCreateRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        List<ProductionPlanCreateLineCommand> lines = toCreateCommands(request);
        return productionPlanApplicationService.createPlans(lines, principal.loginId()).stream()
                .map(ProductionPlanResponse::from)
                .toList();
    }

    private List<ProductionPlanCreateLineCommand> toCreateCommands(ProductionPlanCreateRequest request) {
        if (request != null && request.lines() != null && !request.lines().isEmpty()) {
            return request.lines().stream()
                    .map(line -> new ProductionPlanCreateLineCommand(line.salesOrderLineId(), line.plannedQty()))
                    .toList();
        }
        if (request != null && request.salesOrderLineIds() != null && !request.salesOrderLineIds().isEmpty()) {
            return request.salesOrderLineIds().stream()
                    .map(lineId -> new ProductionPlanCreateLineCommand(lineId, null))
                    .toList();
        }
        throw new IllegalArgumentException("수립할 수주 라인을 1건 이상 선택하세요.");
    }

    @PostMapping("/plans/{id}/cancel")
    @PreAuthorize("hasAuthority('production:plan:write')")
    public ProductionPlanResponse cancel(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        return ProductionPlanResponse.from(productionPlanApplicationService.cancelPlan(id, principal.loginId()));
    }
}
