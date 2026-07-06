package com.shindong.smartmanager.api.web.outsource;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.outsource.CreateOutsourcingReceiptCommand;
import com.shindong.smartmanager.application.outsource.CreateOutsourcingReceiptLineCommand;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptCandidateCriteria;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptListCriteria;
import com.shindong.smartmanager.infrastructure.application.OutsourcingReceiptApplicationService;
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
@RequestMapping("/api/v1/outsource/receipts")
public class OutsourcingReceiptController {

    private final OutsourcingReceiptApplicationService outsourcingReceiptApplicationService;

    public OutsourcingReceiptController(OutsourcingReceiptApplicationService outsourcingReceiptApplicationService) {
        this.outsourcingReceiptApplicationService = outsourcingReceiptApplicationService;
    }

    @GetMapping("/candidates")
    @PreAuthorize("hasAuthority('outsource:receipt:read')")
    public List<OutsourcingReceiptCandidateResponse> listCandidates(
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) String orderNo,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate orderDateTo,
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String itemName
    ) {
        return outsourcingReceiptApplicationService.listCandidates(new OutsourcingReceiptCandidateCriteria(
                partnerName, orderNo, orderDateFrom, orderDateTo, itemNo, itemName
        )).stream().map(OutsourcingReceiptCandidateResponse::from).toList();
    }

    @GetMapping
    @PreAuthorize("hasAuthority('outsource:receipt:read')")
    public List<OutsourcingReceiptResponse> list(
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateTo,
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String itemName
    ) {
        return outsourcingReceiptApplicationService.list(new OutsourcingReceiptListCriteria(
                partnerName, receiptDateFrom, receiptDateTo, itemNo, itemName
        )).stream().map(OutsourcingReceiptResponse::from).toList();
    }

    @GetMapping("/{id}")
    @PreAuthorize("hasAuthority('outsource:receipt:read')")
    public OutsourcingReceiptResponse get(@PathVariable long id) {
        return OutsourcingReceiptResponse.from(outsourcingReceiptApplicationService.get(id));
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('outsource:receipt:write')")
    public OutsourcingReceiptResponse create(@Valid @RequestBody CreateOutsourcingReceiptRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return OutsourcingReceiptResponse.from(outsourcingReceiptApplicationService.register(
                new CreateOutsourcingReceiptCommand(
                        request.receiptDate(),
                        request.lines().stream()
                                .map(line -> new CreateOutsourcingReceiptLineCommand(
                                        line.outsourcingOrderLineId(),
                                        line.receiptQty()
                                ))
                                .toList()
                ),
                principal.loginId()
        ));
    }

    @PostMapping("/{id}/cancel")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('outsource:receipt:write')")
    public void cancel(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        outsourcingReceiptApplicationService.cancel(id, principal.loginId());
    }

    public record CreateOutsourcingReceiptRequest(
            @NotNull LocalDate receiptDate,
            @NotEmpty List<CreateOutsourcingReceiptLineRequest> lines
    ) {
    }

    public record CreateOutsourcingReceiptLineRequest(
            @NotNull Long outsourcingOrderLineId,
            @NotNull BigDecimal receiptQty
    ) {
    }
}
