package com.shindong.smartmanager.api.web.sales;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.sales.CreateSalesRevenueCommand;
import com.shindong.smartmanager.application.sales.CreateSalesRevenueLineCommand;
import com.shindong.smartmanager.application.sales.SalesRevenueCandidateCriteria;
import com.shindong.smartmanager.application.sales.SalesRevenueListCriteria;
import com.shindong.smartmanager.domain.sales.SalesRevenueStatus;
import com.shindong.smartmanager.infrastructure.application.SalesRevenueApplicationService;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
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
@RequestMapping("/api/v1/sales/revenues")
public class SalesRevenueController {

    private final SalesRevenueApplicationService salesRevenueApplicationService;

    public SalesRevenueController(SalesRevenueApplicationService salesRevenueApplicationService) {
        this.salesRevenueApplicationService = salesRevenueApplicationService;
    }

    @GetMapping("/candidates")
    @PreAuthorize("hasAuthority('sales:revenue:read')")
    public List<SalesRevenueCandidateResponse> listCandidates(
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) String shipmentNo,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate shipmentDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate shipmentDateTo,
            @RequestParam(required = false) String orderNo,
            @RequestParam(required = false) String itemNum,
            @RequestParam(required = false) String itemName
    ) {
        return salesRevenueApplicationService.listCandidates(new SalesRevenueCandidateCriteria(
                partnerName, shipmentNo, shipmentDateFrom, shipmentDateTo, orderNo, itemNum, itemName
        )).stream().map(SalesRevenueCandidateResponse::from).toList();
    }

    @GetMapping
    @PreAuthorize("hasAuthority('sales:revenue:read')")
    public List<SalesRevenueResponse> list(
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate revenueDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate revenueDateTo,
            @RequestParam(required = false) String revenueNo,
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) SalesRevenueStatus status,
            @RequestParam(defaultValue = "true") boolean excludeCancelled
    ) {
        return salesRevenueApplicationService.list(new SalesRevenueListCriteria(
                revenueDateFrom,
                revenueDateTo,
                revenueNo,
                partnerName,
                status,
                excludeCancelled
        )).stream().map(SalesRevenueResponse::from).toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('sales:revenue:write')")
    public SalesRevenueResponse create(@Valid @RequestBody CreateSalesRevenueRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return SalesRevenueResponse.from(salesRevenueApplicationService.register(
                new CreateSalesRevenueCommand(
                        request.revenueDate(),
                        request.lines().stream()
                                .map(line -> new CreateSalesRevenueLineCommand(
                                        line.salesShipmentLineId(),
                                        line.revenueQty(),
                                        line.lotId()
                                ))
                                .toList()
                ),
                principal.loginId()
        ));
    }

    @PostMapping("/{id}/cancel")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('sales:revenue:write')")
    public void cancel(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        salesRevenueApplicationService.cancel(id, principal.loginId());
    }

    public record CreateSalesRevenueRequest(
            @NotNull LocalDate revenueDate,
            @NotEmpty List<CreateSalesRevenueLineRequest> lines
    ) {
    }

    public record CreateSalesRevenueLineRequest(
            @NotNull Long salesShipmentLineId,
            @NotNull BigDecimal revenueQty,
            Long lotId
    ) {
    }
}
