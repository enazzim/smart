package com.shindong.smartmanager.api.web.purchase;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.purchase.DefectClaimCommand;
import com.shindong.smartmanager.application.purchase.DefectClaimListCriteria;
import com.shindong.smartmanager.infrastructure.application.DefectClaimApplicationService;
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
@RequestMapping("/api/v1/purchase/defect-claims")
public class DefectClaimController {

    private final DefectClaimApplicationService defectClaimApplicationService;

    public DefectClaimController(DefectClaimApplicationService defectClaimApplicationService) {
        this.defectClaimApplicationService = defectClaimApplicationService;
    }

    @GetMapping
    @PreAuthorize("hasAuthority('purchase:defect-claim:read')")
    public List<DefectClaimResponse> list(
            @RequestParam(required = false) Long partnerId,
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) Long itemId,
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateTo
    ) {
        return defectClaimApplicationService.list(new DefectClaimListCriteria(
                partnerId,
                partnerName,
                itemId,
                itemNo,
                itemName,
                receiptDateFrom,
                receiptDateTo
        )).stream().map(DefectClaimResponse::from).toList();
    }

    @GetMapping("/{id}")
    @PreAuthorize("hasAuthority('purchase:defect-claim:read')")
    public DefectClaimResponse get(@PathVariable long id) {
        return DefectClaimResponse.from(defectClaimApplicationService.get(id));
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('purchase:defect-claim:write')")
    public DefectClaimResponse create(@Valid @RequestBody SaveDefectClaimRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return DefectClaimResponse.from(defectClaimApplicationService.register(
                new DefectClaimCommand(
                        request.partnerId(),
                        request.itemId(),
                        request.receiptDate(),
                        request.claimQty(),
                        request.amount(),
                        request.reason()
                ),
                principal.loginId()
        ));
    }

    @PutMapping("/{id}")
    @PreAuthorize("hasAuthority('purchase:defect-claim:write')")
    public DefectClaimResponse update(@PathVariable long id, @Valid @RequestBody SaveDefectClaimRequest request) {
        var principal = SecurityUtils.requirePrincipal();
        return DefectClaimResponse.from(defectClaimApplicationService.update(
                id,
                new DefectClaimCommand(
                        request.partnerId(),
                        request.itemId(),
                        request.receiptDate(),
                        request.claimQty(),
                        request.amount(),
                        request.reason()
                ),
                principal.loginId()
        ));
    }

    @DeleteMapping("/{id}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('purchase:defect-claim:write')")
    public void delete(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        defectClaimApplicationService.delete(id, principal.loginId());
    }

    public record SaveDefectClaimRequest(
            @NotNull Long partnerId,
            @NotNull Long itemId,
            @NotNull LocalDate receiptDate,
            @NotNull BigDecimal claimQty,
            @NotNull BigDecimal amount,
            @NotBlank String reason
    ) {
    }
}
