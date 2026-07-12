package com.shindong.smartmanager.api.web.sales;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.sales.CreateSalesShipmentCommand;
import com.shindong.smartmanager.application.sales.CreateSalesShipmentLineCommand;
import com.shindong.smartmanager.application.sales.SalesShipmentCandidateCriteria;
import com.shindong.smartmanager.application.sales.SalesShipmentListCriteria;
import com.shindong.smartmanager.domain.sales.SalesShipmentStatus;
import com.shindong.smartmanager.infrastructure.application.SalesShipmentApplicationService;
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
@RequestMapping("/api/v1/sales/shipments")
public class SalesShipmentController {

    private final SalesShipmentApplicationService salesShipmentApplicationService;

    public SalesShipmentController(SalesShipmentApplicationService salesShipmentApplicationService) {
        this.salesShipmentApplicationService = salesShipmentApplicationService;
    }

    @GetMapping("/candidates")
    @PreAuthorize("hasAuthority('sales:shipment:read')")
    public List<SalesShipmentCandidateResponse> listCandidates(
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) String orderNo,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDateTo,
            @RequestParam(required = false) String itemNum,
            @RequestParam(required = false) String itemName
    ) {
        return salesShipmentApplicationService.listCandidates(new SalesShipmentCandidateCriteria(
                partnerName, orderNo, orderDateFrom, orderDateTo, itemNum, itemName
        )).stream().map(SalesShipmentCandidateResponse::from).toList();
    }

    @GetMapping
    @PreAuthorize("hasAuthority('sales:shipment:read')")
    public List<SalesShipmentResponse> list(
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate shipmentDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate shipmentDateTo,
            @RequestParam(required = false) String shipmentNo,
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) SalesShipmentStatus status,
            @RequestParam(defaultValue = "true") boolean excludeCancelled
    ) {
        return salesShipmentApplicationService.list(new SalesShipmentListCriteria(
                shipmentDateFrom,
                shipmentDateTo,
                shipmentNo,
                partnerName,
                status,
                excludeCancelled
        )).stream().map(SalesShipmentResponse::from).toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('sales:shipment:write')")
    public SalesShipmentResponse create(@Valid @RequestBody CreateSalesShipmentRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return SalesShipmentResponse.from(salesShipmentApplicationService.register(
                new CreateSalesShipmentCommand(
                        request.shipmentDate(),
                        request.lines().stream()
                                .map(line -> new CreateSalesShipmentLineCommand(
                                        line.salesOrderLineId(),
                                        line.shipmentQty(),
                                        line.lotId()
                                ))
                                .toList()
                ),
                principal.loginId()
        ));
    }

    @PostMapping("/{id}/cancel")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('sales:shipment:write')")
    public void cancel(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        salesShipmentApplicationService.cancel(id, principal.loginId());
    }

    public record CreateSalesShipmentRequest(
            @NotNull LocalDate shipmentDate,
            @NotEmpty List<CreateSalesShipmentLineRequest> lines
    ) {
    }

    public record CreateSalesShipmentLineRequest(
            @NotNull Long salesOrderLineId,
            @NotNull BigDecimal shipmentQty,
            Long lotId
    ) {
    }
}
