package com.shindong.smartmanager.api.web.sales;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.sales.CreateSalesCollectionCommand;
import com.shindong.smartmanager.application.sales.SalesCollectionCandidateCriteria;
import com.shindong.smartmanager.application.sales.SalesCollectionListCriteria;
import com.shindong.smartmanager.domain.sales.SalesCollectionStatus;
import com.shindong.smartmanager.infrastructure.application.SalesCollectionApplicationService;
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
@RequestMapping("/api/v1/sales/collections")
public class SalesCollectionController {

    private final SalesCollectionApplicationService salesCollectionApplicationService;

    public SalesCollectionController(SalesCollectionApplicationService salesCollectionApplicationService) {
        this.salesCollectionApplicationService = salesCollectionApplicationService;
    }

    @GetMapping("/candidates")
    @PreAuthorize("hasAuthority('sales:collection:read')")
    public List<SalesCollectionCandidateResponse> listCandidates(
            @RequestParam(required = false) String partnerName
    ) {
        return salesCollectionApplicationService.listCandidates(new SalesCollectionCandidateCriteria(partnerName))
                .stream()
                .map(SalesCollectionCandidateResponse::from)
                .toList();
    }

    @GetMapping
    @PreAuthorize("hasAuthority('sales:collection:read')")
    public List<SalesCollectionResponse> list(
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate collectionDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate collectionDateTo,
            @RequestParam(required = false) String collectionNo,
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) SalesCollectionStatus status,
            @RequestParam(defaultValue = "true") boolean excludeCancelled
    ) {
        return salesCollectionApplicationService.list(new SalesCollectionListCriteria(
                collectionDateFrom,
                collectionDateTo,
                collectionNo,
                partnerName,
                status,
                excludeCancelled
        )).stream().map(SalesCollectionResponse::from).toList();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('sales:collection:write')")
    public SalesCollectionResponse create(@Valid @RequestBody CreateSalesCollectionRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return SalesCollectionResponse.from(salesCollectionApplicationService.register(
                new CreateSalesCollectionCommand(
                        request.partnerId(),
                        request.collectionDate(),
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
    @PreAuthorize("hasAuthority('sales:collection:write')")
    public void cancel(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        salesCollectionApplicationService.cancel(id, principal.loginId());
    }

    public record CreateSalesCollectionRequest(
            @NotNull Long partnerId,
            @NotNull LocalDate collectionDate,
            @NotNull BigDecimal supplyAmount,
            BigDecimal vatAmount,
            String paymentMethod,
            String remark
    ) {
    }
}
