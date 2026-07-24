package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.purchase.EtcClaimCommand;
import com.shindong.smartmanager.application.purchase.EtcClaimListCriteria;
import com.shindong.smartmanager.infrastructure.application.EtcClaimApplicationService;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
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
@RequestMapping("/api/v1/purchase/etc-claims")
public class EtcClaimController {

    private final EtcClaimApplicationService etcClaimApplicationService;

    public EtcClaimController(EtcClaimApplicationService etcClaimApplicationService) {
        this.etcClaimApplicationService = etcClaimApplicationService;
    }

    @GetMapping
    @PreAuthorize("hasAuthority('purchase:etc-claim:read')")
    public List<EtcClaimResponse> list(
            @RequestParam(required = false) Long partnerId,
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateTo,
            @RequestParam(required = false) String reason,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate registeredOn
    ) {
        return etcClaimApplicationService.list(new EtcClaimListCriteria(
                partnerId,
                partnerName,
                receiptDateFrom,
                receiptDateTo,
                reason,
                registeredOn
        )).stream().map(EtcClaimResponse::from).toList();
    }

    @GetMapping("/{id}")
    @PreAuthorize("hasAuthority('purchase:etc-claim:read')")
    public EtcClaimResponse get(@PathVariable long id) {
        return EtcClaimResponse.from(etcClaimApplicationService.get(id));
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('purchase:etc-claim:write')")
    public EtcClaimResponse create(@Valid @RequestBody SaveEtcClaimRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return EtcClaimResponse.from(etcClaimApplicationService.register(
                new EtcClaimCommand(
                        request.partnerId(),
                        request.receiptDate(),
                        request.reason(),
                        request.amount()
                ),
                principal.loginId()
        ));
    }

    @PutMapping("/{id}")
    @PreAuthorize("hasAuthority('purchase:etc-claim:write')")
    public EtcClaimResponse update(@PathVariable long id, @Valid @RequestBody SaveEtcClaimRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return EtcClaimResponse.from(etcClaimApplicationService.update(
                id,
                new EtcClaimCommand(
                        request.partnerId(),
                        request.receiptDate(),
                        request.reason(),
                        request.amount()
                ),
                principal.loginId()
        ));
    }

    @DeleteMapping("/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('purchase:etc-claim:write')")
    public void delete(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        etcClaimApplicationService.delete(id, principal.loginId());
    }

    public record SaveEtcClaimRequest(
            @NotNull Long partnerId,
            @NotNull LocalDate receiptDate,
            @NotBlank String reason,
            @NotNull BigDecimal amount
    ) {
    }
}
