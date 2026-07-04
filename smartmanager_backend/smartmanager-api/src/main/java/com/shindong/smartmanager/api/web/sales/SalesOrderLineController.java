package com.shindong.smartmanager.api.web.sales;

import com.shindong.smartmanager.application.sales.SalesOrderLineListCriteria;
import com.shindong.smartmanager.domain.sales.SalesLineDeliveryStatus;
import com.shindong.smartmanager.domain.sales.SalesLineFulfillmentStatus;
import com.shindong.smartmanager.infrastructure.application.SalesOrderApplicationService;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import com.shindong.smartmanager.api.security.SecurityUtils;

@RestController
@RequestMapping("/api/v1/sales/order-lines")
public class SalesOrderLineController {

    private final SalesOrderApplicationService salesOrderApplicationService;

    public SalesOrderLineController(SalesOrderApplicationService salesOrderApplicationService) {
        this.salesOrderApplicationService = salesOrderApplicationService;
    }

    @GetMapping
    @PreAuthorize("hasAuthority('sales:order:read')")
    public List<SalesOrderLineListResponse> list(
            @RequestParam(required = false) Long partnerId,
            @RequestParam(required = false) Long itemId,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate requestedDeliveryDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate requestedDeliveryDateTo,
            @RequestParam(required = false) SalesLineFulfillmentStatus fulfillmentStatus,
            @RequestParam(required = false) SalesLineDeliveryStatus deliveryStatus
    ) {
        SalesOrderLineListCriteria criteria = new SalesOrderLineListCriteria(
                partnerId,
                itemId,
                requestedDeliveryDateFrom,
                requestedDeliveryDateTo,
                fulfillmentStatus,
                deliveryStatus
        );
        return salesOrderApplicationService.listLines(criteria).stream()
                .map(SalesOrderLineListResponse::from)
                .toList();
    }

    @PostMapping("/{lineId}/start-progress")
    @PreAuthorize("hasAuthority('sales:order:progress')")
    public SalesOrderLineListResponse startProgress(@PathVariable long lineId) {
        var principal = SecurityUtils.requirePrincipal();
        return SalesOrderLineListResponse.from(
                salesOrderApplicationService.startLineProgress(lineId, principal.loginId())
        );
    }

    @PostMapping("/{lineId}/complete")
    @PreAuthorize("hasAuthority('sales:order:progress')")
    public SalesOrderLineListResponse complete(@PathVariable long lineId) {
        var principal = SecurityUtils.requirePrincipal();
        return SalesOrderLineListResponse.from(
                salesOrderApplicationService.completeLine(lineId, principal.loginId())
        );
    }

    @PostMapping("/{lineId}/force-complete")
    @PreAuthorize("hasAuthority('sales:order:force-complete')")
    public SalesOrderLineListResponse forceComplete(@PathVariable long lineId) {
        var principal = SecurityUtils.requirePrincipal();
        return SalesOrderLineListResponse.from(
                salesOrderApplicationService.forceCompleteLine(lineId, principal.loginId())
        );
    }
}
