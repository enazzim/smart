package com.shindong.smartmanager.api.web.sales;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.sales.SalesOrderCommand;
import com.shindong.smartmanager.application.sales.SalesOrderLineCommand;
import com.shindong.smartmanager.infrastructure.application.SalesOrderApplicationService;
import jakarta.validation.Valid;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/sales/orders")
public class SalesOrderController {

    private final SalesOrderApplicationService salesOrderApplicationService;

    public SalesOrderController(SalesOrderApplicationService salesOrderApplicationService) {
        this.salesOrderApplicationService = salesOrderApplicationService;
    }

    @GetMapping
    @PreAuthorize("hasAuthority('sales:order:read')")
    public List<SalesOrderResponse> list() {
        return salesOrderApplicationService.list().stream()
                .map(SalesOrderResponse::from)
                .toList();
    }

    @GetMapping("/next-order-no")
    @PreAuthorize("hasAuthority('sales:order:read') or hasAuthority('sales:order:write')")
    public NextOrderNoResponse nextOrderNo(
            @RequestParam @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDate
    ) {
        return new NextOrderNoResponse(salesOrderApplicationService.previewNextOrderNo(orderDate));
    }

    @GetMapping("/{id}")
    @PreAuthorize("hasAuthority('sales:order:read')")
    public SalesOrderResponse get(@PathVariable long id) {
        return SalesOrderResponse.from(salesOrderApplicationService.get(id));
    }

    @PostMapping("/bulk")
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('sales:order:write')")
    public SalesOrderBulkResponse bulkCreate(@Valid @RequestBody SalesOrderBulkRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return SalesOrderBulkResponse.from(salesOrderApplicationService.registerBulk(
                request.orders().stream().map(this::toCommand).toList(),
                principal.loginId()
        ));
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('sales:order:write')")
    public SalesOrderResponse create(@Valid @RequestBody SalesOrderRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return SalesOrderResponse.from(salesOrderApplicationService.register(
                toCommand(request),
                principal.loginId()
        ));
    }

    @PutMapping("/{id}")
    @PreAuthorize("hasAuthority('sales:order:write')")
    public SalesOrderResponse update(@PathVariable long id, @Valid @RequestBody SalesOrderRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return SalesOrderResponse.from(salesOrderApplicationService.update(
                id,
                toCommand(request),
                principal.loginId()
        ));
    }

    @PostMapping("/{id}/confirm")
    @PreAuthorize("hasAuthority('sales:order:confirm')")
    public SalesOrderResponse confirm(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        return SalesOrderResponse.from(salesOrderApplicationService.confirm(id, principal.loginId()));
    }

    @PostMapping("/{id}/cancel")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('sales:order:write')")
    public void cancel(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        salesOrderApplicationService.cancel(id, principal.loginId());
    }

    private SalesOrderCommand toCommand(SalesOrderRequest request) {
        List<SalesOrderLineCommand> lines = request.lines().stream()
                .map(line -> new SalesOrderLineCommand(
                        line.itemId(),
                        line.orderQty(),
                        line.unitPrice(),
                        line.deliveryDate()
                ))
                .toList();
        return new SalesOrderCommand(
                request.orderNo(),
                request.partnerId(),
                request.orderDate(),
                request.requestedDeliveryDate(),
                request.remark(),
                lines
        );
    }
}
