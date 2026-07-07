package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.purchase.CreatePartnerPaymentCommand;
import com.shindong.smartmanager.application.purchase.PartnerPaymentCandidateCriteria;
import com.shindong.smartmanager.application.purchase.PartnerPaymentListCriteria;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentStatus;
import com.shindong.smartmanager.infrastructure.application.PartnerPaymentApplicationService;
import jakarta.validation.Valid;
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
@RequestMapping("/api/v1/purchase/payments")
public class PartnerPaymentController {

    private final PartnerPaymentApplicationService partnerPaymentApplicationService;

    public PartnerPaymentController(PartnerPaymentApplicationService partnerPaymentApplicationService) {
        this.partnerPaymentApplicationService = partnerPaymentApplicationService;
    }

    @GetMapping("/candidates")
    @PreAuthorize("hasAuthority('purchase:payment:read')")
    public List<PartnerPaymentCandidateResponse> listCandidates(
            @RequestParam(required = false) String partnerName
    ) {
        return partnerPaymentApplicationService.listCandidates(new PartnerPaymentCandidateCriteria(partnerName))
                .stream()
                .map(PartnerPaymentCandidateResponse::from)
                .toList();
    }

    @GetMapping
    @PreAuthorize("hasAuthority('purchase:payment:read')")
    public List<PartnerPaymentResponse> list(
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate paymentDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate paymentDateTo,
            @RequestParam(required = false) String paymentNo,
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) PartnerPaymentStatus status,
            @RequestParam(defaultValue = "true") boolean excludeCancelled
    ) {
        return partnerPaymentApplicationService.list(new PartnerPaymentListCriteria(
                paymentDateFrom,
                paymentDateTo,
                paymentNo,
                partnerName,
                status,
                excludeCancelled
        )).stream().map(PartnerPaymentResponse::from).toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('purchase:payment:write')")
    public PartnerPaymentResponse create(@Valid @RequestBody CreatePartnerPaymentRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return PartnerPaymentResponse.from(partnerPaymentApplicationService.register(
                new CreatePartnerPaymentCommand(
                        request.partnerId(),
                        request.paymentDate(),
                        request.costCategory(),
                        request.supplyAmount(),
                        request.vatAmount(),
                        request.paymentMethod(),
                        request.remark()
                ),
                principal.loginId()
        ));
    }

    @PostMapping("/{id}/cancel")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('purchase:payment:write')")
    public void cancel(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        partnerPaymentApplicationService.cancel(id, principal.loginId());
    }

    public record CreatePartnerPaymentRequest(
            @NotNull Long partnerId,
            @NotNull LocalDate paymentDate,
            @NotNull PartnerPaymentCostCategory costCategory,
            @NotNull BigDecimal supplyAmount,
            BigDecimal vatAmount,
            String paymentMethod,
            String remark
    ) {
    }
}
