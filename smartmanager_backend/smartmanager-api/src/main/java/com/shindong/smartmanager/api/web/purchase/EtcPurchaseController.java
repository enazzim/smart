package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.purchase.CreateEtcPurchaseOrderCommand;
import com.shindong.smartmanager.application.purchase.CreateEtcPurchaseReceiptCommand;
import com.shindong.smartmanager.application.purchase.CreateEtcPurchaseReceiptLineCommand;
import com.shindong.smartmanager.application.purchase.EtcPurchaseOrderListCriteria;
import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptCandidateCriteria;
import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptListCriteria;
import com.shindong.smartmanager.application.purchase.UpdateEtcPurchaseOrderCommand;
import com.shindong.smartmanager.application.purchase.UpdateEtcPurchaseReceiptCommand;
import com.shindong.smartmanager.infrastructure.application.EtcPurchaseApplicationService;
import com.shindong.smartmanager.infrastructure.application.PublicCodeApplicationService;
import com.shindong.smartmanager.api.web.system.publiccode.PublicCodeSmallResponse;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotEmpty;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.DeleteMapping;
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
@RequestMapping("/api/v1/purchase/etc")
public class EtcPurchaseController {

    private final EtcPurchaseApplicationService etcPurchaseApplicationService;
    private final PublicCodeApplicationService publicCodeApplicationService;

    public EtcPurchaseController(
            EtcPurchaseApplicationService etcPurchaseApplicationService,
            PublicCodeApplicationService publicCodeApplicationService
    ) {
        this.etcPurchaseApplicationService = etcPurchaseApplicationService;
        this.publicCodeApplicationService = publicCodeApplicationService;
    }

    @GetMapping("/categories")
    @PreAuthorize("hasAuthority('purchase:etc-order:read')")
    public List<PublicCodeSmallResponse> listCategories() {
        return publicCodeApplicationService.listActiveSmallCodes("1800", null).stream()
                .map(PublicCodeSmallResponse::from)
                .toList();
    }

    @GetMapping("/orders")
    @PreAuthorize("hasAuthority('purchase:etc-order:read')")
    public List<EtcPurchaseOrderResponse> listOrders(
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) String orderNo,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDateTo,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate deliveryFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate deliveryTo
    ) {
        return etcPurchaseApplicationService.listOrders(
                new EtcPurchaseOrderListCriteria(
                        itemName, partnerName, orderNo, orderDateFrom, orderDateTo, deliveryFrom, deliveryTo, null
                )
        ).stream().map(EtcPurchaseOrderResponse::from).toList();
    }

    @GetMapping("/orders/{id}/print")
    @PreAuthorize("hasAuthority('purchase:etc-order:read') or hasAuthority('purchase:etc-receipt:read')")
    public ResponseEntity<String> printOrder(@PathVariable long id) {
        String html = PurchaseOrderPrintHtmlRenderer.render(etcPurchaseApplicationService.getOrderPrintView(id));
        return ResponseEntity.ok().contentType(MediaType.TEXT_HTML).body(html);
    }

    @PostMapping("/orders/print")
    @PreAuthorize("hasAuthority('purchase:etc-order:read') or hasAuthority('purchase:etc-receipt:read')")
    public ResponseEntity<String> printOrders(@Valid @RequestBody EtcPurchaseOrderPrintRequest request) {
        String html = PurchaseOrderPrintHtmlRenderer.renderBatch(
                etcPurchaseApplicationService.getOrderBatchPrintViews(request.orderIds())
        );
        return ResponseEntity.ok().contentType(MediaType.TEXT_HTML).body(html);
    }

    @GetMapping("/orders/{id}")
    @PreAuthorize("hasAuthority('purchase:etc-order:read')")
    public EtcPurchaseOrderResponse getOrder(@PathVariable long id) {
        return EtcPurchaseOrderResponse.from(etcPurchaseApplicationService.getOrder(id));
    }

    @PostMapping("/orders")
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('purchase:etc-order:write')")
    public EtcPurchaseOrderResponse createOrder(@Valid @RequestBody CreateEtcPurchaseOrderRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return EtcPurchaseOrderResponse.from(etcPurchaseApplicationService.createOrder(
                new CreateEtcPurchaseOrderCommand(
                        request.itemName(),
                        request.partnerId(),
                        request.unitPrice(),
                        request.orderQty(),
                        request.requestedDeliveryDate(),
                        request.categoryCodeId(),
                        request.orderDate()
                ),
                principal.loginId()
        ));
    }

    @PutMapping("/orders/{id}")
    @PreAuthorize("hasAuthority('purchase:etc-order:write')")
    public EtcPurchaseOrderResponse updateOrder(
            @PathVariable long id,
            @Valid @RequestBody UpdateEtcPurchaseOrderRequest request
    ) {
        var principal = SecurityUtils.requirePrincipal();
        return EtcPurchaseOrderResponse.from(etcPurchaseApplicationService.updateOrder(
                id,
                new UpdateEtcPurchaseOrderCommand(
                        request.itemName(),
                        request.partnerId(),
                        request.unitPrice(),
                        request.orderQty(),
                        request.requestedDeliveryDate(),
                        request.categoryCodeId()
                ),
                principal.loginId()
        ));
    }

    @DeleteMapping("/orders/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('purchase:etc-order:write')")
    public void deleteOrder(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        etcPurchaseApplicationService.deleteOrder(id, principal.loginId());
    }

    @GetMapping("/receipt-candidates")
    @PreAuthorize("hasAuthority('purchase:etc-receipt:read')")
    public List<EtcPurchaseReceiptCandidateResponse> listReceiptCandidates(
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate deliveryFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate deliveryTo
    ) {
        return etcPurchaseApplicationService.listReceiptCandidates(
                new EtcPurchaseReceiptCandidateCriteria(itemName, partnerName, deliveryFrom, deliveryTo)
        ).stream().map(EtcPurchaseReceiptCandidateResponse::from).toList();
    }

    @GetMapping("/receipts")
    @PreAuthorize("hasAuthority('purchase:etc-receipt:read')")
    public List<EtcPurchaseReceiptResponse> listReceipts(
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptTo,
            @RequestParam(required = false) Integer fiscalYear,
            @RequestParam(required = false) Integer fiscalMonth
    ) {
        return etcPurchaseApplicationService.listReceipts(
                new EtcPurchaseReceiptListCriteria(itemName, partnerName, receiptFrom, receiptTo, fiscalYear, fiscalMonth)
        ).stream().map(EtcPurchaseReceiptResponse::from).toList();
    }

    @PostMapping("/receipts")
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('purchase:etc-receipt:write')")
    public List<EtcPurchaseReceiptResponse> createReceipts(@Valid @RequestBody CreateEtcPurchaseReceiptRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return etcPurchaseApplicationService.registerReceipts(
                new CreateEtcPurchaseReceiptCommand(
                        request.receiptDate(),
                        request.fiscalYear(),
                        request.fiscalMonth(),
                        request.lines().stream()
                                .map(line -> new CreateEtcPurchaseReceiptLineCommand(
                                        line.etcPurchaseOrderId(),
                                        line.receiptQty()
                                ))
                                .toList()
                ),
                principal.loginId()
        ).stream().map(EtcPurchaseReceiptResponse::from).toList();
    }

    @PutMapping("/receipts/{id}")
    @PreAuthorize("hasAuthority('purchase:etc-receipt:write')")
    public EtcPurchaseReceiptResponse updateReceipt(
            @PathVariable long id,
            @Valid @RequestBody UpdateEtcPurchaseReceiptRequest request
    ) {
        var principal = SecurityUtils.requirePrincipal();
        return EtcPurchaseReceiptResponse.from(etcPurchaseApplicationService.updateReceipt(
                id,
                new UpdateEtcPurchaseReceiptCommand(
                        request.receiptDate(),
                        request.receiptQty(),
                        request.fiscalYear(),
                        request.fiscalMonth()
                ),
                principal.loginId()
        ));
    }

    @DeleteMapping("/receipts/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('purchase:etc-receipt:write')")
    public void cancelReceipt(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        etcPurchaseApplicationService.cancelReceipt(id, principal.loginId());
    }

    public record EtcPurchaseOrderPrintRequest(@NotEmpty List<Long> orderIds) {
    }
}
