package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.purchase.CreatePurchaseOrderFromMrpCommand;
import com.shindong.smartmanager.application.purchase.CreatePurchaseOrderFromMrpLineCommand;
import com.shindong.smartmanager.application.purchase.PurchaseOrderCommand;
import com.shindong.smartmanager.application.purchase.PurchaseOrderLineCommand;
import com.shindong.smartmanager.application.purchase.PurchaseOrderListCriteria;
import com.shindong.smartmanager.domain.purchase.PurchaseOrderSourceType;
import com.shindong.smartmanager.domain.purchase.PurchaseOrderStatus;
import com.shindong.smartmanager.infrastructure.application.PurchaseOrderApplicationService;
import jakarta.validation.Valid;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
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
@RequestMapping("/api/v1/purchase/orders")
public class PurchaseOrderController {

    private final PurchaseOrderApplicationService purchaseOrderApplicationService;

    public PurchaseOrderController(PurchaseOrderApplicationService purchaseOrderApplicationService) {
        this.purchaseOrderApplicationService = purchaseOrderApplicationService;
    }

    @GetMapping
    @PreAuthorize("hasAuthority('purchase:order:read')")
    public List<PurchaseOrderResponse> list(
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDateTo,
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) String orderNo,
            @RequestParam(required = false) PurchaseOrderStatus status,
            @RequestParam(defaultValue = "true") boolean excludeCancelled
    ) {
        PurchaseOrderListCriteria criteria = new PurchaseOrderListCriteria(
                orderDateFrom,
                orderDateTo,
                partnerName,
                orderNo,
                status,
                excludeCancelled
        );
        return purchaseOrderApplicationService.list(criteria).stream()
                .map(PurchaseOrderResponse::from)
                .toList();
    }

    @GetMapping("/mrp-candidates")
    @PreAuthorize("hasAuthority('purchase:order:read')")
    public List<MrpPurchaseCandidateResponse> listMrpCandidates(
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDate
    ) {
        return purchaseOrderApplicationService.listMrpCandidates(orderDate).stream()
                .map(MrpPurchaseCandidateResponse::from)
                .toList();
    }

    @GetMapping("/next-order-no")
    @PreAuthorize("hasAuthority('purchase:order:read') or hasAuthority('purchase:order:write')")
    public NextPurchaseOrderNoResponse nextOrderNo(
            @RequestParam @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDate
    ) {
        return new NextPurchaseOrderNoResponse(purchaseOrderApplicationService.previewNextOrderNo(orderDate));
    }

    @GetMapping("/{id}/print")
    @PreAuthorize("hasAuthority('purchase:order:read')")
    public ResponseEntity<String> print(@PathVariable long id) {
        String html = PurchaseOrderPrintHtmlRenderer.render(purchaseOrderApplicationService.getPrintView(id));
        return ResponseEntity.ok()
                .contentType(MediaType.TEXT_HTML)
                .body(html);
    }

    @GetMapping("/{id}")
    @PreAuthorize("hasAuthority('purchase:order:read')")
    public PurchaseOrderResponse get(@PathVariable long id) {
        return PurchaseOrderResponse.from(purchaseOrderApplicationService.get(id));
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('purchase:order:write')")
    public PurchaseOrderResponse create(@Valid @RequestBody PurchaseOrderRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return PurchaseOrderResponse.from(purchaseOrderApplicationService.register(
                toCommand(request),
                principal.loginId()
        ));
    }

    @PostMapping("/from-mrp")
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('purchase:order:write')")
    public PurchaseOrderResponse createFromMrp(@Valid @RequestBody CreatePurchaseOrderFromMrpRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return PurchaseOrderResponse.from(purchaseOrderApplicationService.registerFromMrp(
                new CreatePurchaseOrderFromMrpCommand(
                        request.partnerId(),
                        request.orderDate(),
                        request.lines().stream()
                                .map(line -> new CreatePurchaseOrderFromMrpLineCommand(
                                        line.requirementLineId(),
                                        line.orderQty(),
                                        line.unitPrice(),
                                        line.requestedDeliveryDate()
                                ))
                                .toList()
                ),
                principal.loginId()
        ));
    }

    @PutMapping("/{id}")
    @PreAuthorize("hasAuthority('purchase:order:write')")
    public PurchaseOrderResponse update(@PathVariable long id, @Valid @RequestBody PurchaseOrderRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return PurchaseOrderResponse.from(purchaseOrderApplicationService.update(
                id,
                toCommand(request),
                principal.loginId()
        ));
    }

    @PostMapping("/{id}/confirm")
    @PreAuthorize("hasAuthority('purchase:order:confirm')")
    public PurchaseOrderResponse confirm(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        return PurchaseOrderResponse.from(purchaseOrderApplicationService.confirm(id, principal.loginId()));
    }

    @PostMapping("/{id}/cancel")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('purchase:order:write')")
    public void cancel(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        purchaseOrderApplicationService.cancel(id, principal.loginId());
    }

    private PurchaseOrderCommand toCommand(PurchaseOrderRequest request) {
        List<PurchaseOrderLineCommand> lines = request.lines().stream()
                .map(line -> new PurchaseOrderLineCommand(
                        line.itemId(),
                        line.orderQty(),
                        line.unitPrice(),
                        line.requirementLineId()
                ))
                .toList();
        PurchaseOrderSourceType sourceType = request.sourceType() != null
                ? request.sourceType()
                : PurchaseOrderSourceType.MANUAL;
        return new PurchaseOrderCommand(
                request.orderNo(),
                request.partnerId(),
                request.orderDate(),
                sourceType,
                lines
        );
    }
}
