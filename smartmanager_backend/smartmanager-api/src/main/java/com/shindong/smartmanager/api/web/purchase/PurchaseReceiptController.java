package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.purchase.CreatePurchaseReceiptCommand;
import com.shindong.smartmanager.application.purchase.CreatePurchaseReceiptLineCommand;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptCandidateCriteria;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptListCriteria;
import com.shindong.smartmanager.infrastructure.application.PurchaseReceiptApplicationService;
import jakarta.validation.Valid;
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
@RequestMapping("/api/v1/purchase")
public class PurchaseReceiptController {

    private final PurchaseReceiptApplicationService purchaseReceiptApplicationService;

    public PurchaseReceiptController(PurchaseReceiptApplicationService purchaseReceiptApplicationService) {
        this.purchaseReceiptApplicationService = purchaseReceiptApplicationService;
    }

    @GetMapping("/receipt-candidates")
    @PreAuthorize("hasAuthority('purchase:receipt:read')")
    public List<PurchaseReceiptCandidateResponse> listCandidates(
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) String orderNo,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDateTo,
            @RequestParam(required = false) String itemNum,
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) String itemPropertyScope
    ) {
        PurchaseReceiptCandidateCriteria criteria = new PurchaseReceiptCandidateCriteria(
                partnerName, orderNo, orderDateFrom, orderDateTo, itemNum, itemName, itemPropertyScope
        );
        return purchaseReceiptApplicationService.listCandidates(criteria).stream()
                .map(PurchaseReceiptCandidateResponse::from)
                .toList();
    }

    @GetMapping("/receipts")
    @PreAuthorize("hasAuthority('purchase:receipt:read')")
    public List<PurchaseReceiptResponse> list(
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateTo,
            @RequestParam(required = false) String itemNum,
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) String itemPropertyScope
    ) {
        PurchaseReceiptListCriteria criteria = new PurchaseReceiptListCriteria(
                partnerName, receiptDateFrom, receiptDateTo, itemNum, itemName, itemPropertyScope
        );
        return purchaseReceiptApplicationService.list(criteria).stream()
                .map(PurchaseReceiptResponse::from)
                .toList();
    }

    @GetMapping("/receipts/{id}")
    @PreAuthorize("hasAuthority('purchase:receipt:read')")
    public PurchaseReceiptResponse get(@PathVariable long id) {
        return PurchaseReceiptResponse.from(purchaseReceiptApplicationService.get(id));
    }

    @PostMapping("/receipts")
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('purchase:receipt:write')")
    public PurchaseReceiptResponse create(@Valid @RequestBody CreatePurchaseReceiptRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return PurchaseReceiptResponse.from(purchaseReceiptApplicationService.register(
                new CreatePurchaseReceiptCommand(
                        request.receiptDate(),
                        request.fiscalYear(),
                        request.fiscalMonth(),
                        request.lines().stream()
                                .map(line -> new CreatePurchaseReceiptLineCommand(
                                        line.purchaseOrderLineId(),
                                        line.receiptQty(),
                                        line.lotNo(),
                                        Boolean.TRUE.equals(line.autoGenerateLot())
                                ))
                                .toList(),
                        Boolean.TRUE.equals(request.allowOverQty())
                ),
                principal.loginId()
        ));
    }

    @PostMapping("/receipts/{id}/cancel")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('purchase:receipt:write')")
    public void cancel(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        purchaseReceiptApplicationService.cancel(id, principal.loginId());
    }
}
