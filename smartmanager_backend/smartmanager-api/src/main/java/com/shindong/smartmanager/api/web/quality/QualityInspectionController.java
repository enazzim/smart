package com.shindong.smartmanager.api.web.quality;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.quality.CompleteQualityInspectionCommand;
import com.shindong.smartmanager.application.quality.QualityInspectionListCriteria;
import com.shindong.smartmanager.domain.quality.QualityInspectionSourceType;
import com.shindong.smartmanager.domain.quality.QualityInspectionStatus;
import com.shindong.smartmanager.infrastructure.application.QualityInspectionApplicationService;
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
@RequestMapping("/api/v1/quality/inspections")
public class QualityInspectionController {

    private final QualityInspectionApplicationService qualityInspectionApplicationService;

    public QualityInspectionController(QualityInspectionApplicationService qualityInspectionApplicationService) {
        this.qualityInspectionApplicationService = qualityInspectionApplicationService;
    }

    @GetMapping
    @PreAuthorize("hasAuthority('quality:inspection:read')")
    public List<QualityInspectionResponse> list(
            @RequestParam(defaultValue = "PENDING") QualityInspectionStatus status,
            @RequestParam(required = false) QualityInspectionSourceType sourceType,
            @RequestParam(required = false) String partnerName,
            @RequestParam(required = false) String itemNo,
            @RequestParam(required = false) String itemName,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate receiptDateTo,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate completedDateFrom,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate completedDateTo
    ) {
        return qualityInspectionApplicationService.list(new QualityInspectionListCriteria(
                status,
                sourceType,
                partnerName,
                itemNo,
                itemName,
                receiptDateFrom,
                receiptDateTo,
                completedDateFrom,
                completedDateTo
        )).stream().map(QualityInspectionResponse::from).toList();
    }

    @GetMapping("/{id}")
    @PreAuthorize("hasAuthority('quality:inspection:read')")
    public QualityInspectionResponse get(@PathVariable long id) {
        return QualityInspectionResponse.from(qualityInspectionApplicationService.get(id));
    }

    @PostMapping("/{id}/complete")
    @PreAuthorize("hasAuthority('quality:inspection:complete')")
    public QualityInspectionResponse complete(
            @PathVariable long id,
            @Valid @RequestBody CompleteQualityInspectionRequest request
    ) {
        var principal = SecurityUtils.requirePrincipal();
        return QualityInspectionResponse.from(qualityInspectionApplicationService.complete(
                id,
                new CompleteQualityInspectionCommand(
                        request.passedQty(),
                        request.failedQty(),
                        request.inspectionDecisionCodeId(),
                        request.unsuitabilityCauseCodeId(),
                        request.unsuitabilityStatusCodeId(),
                        request.completedDate(),
                        request.fiscalYear(),
                        request.fiscalMonth(),
                        request.lotNo(),
                        Boolean.TRUE.equals(request.autoGenerateLot())
                ),
                principal.loginId()
        ));
    }

    @PostMapping("/{id}/cancel")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @PreAuthorize("hasAuthority('quality:inspection:complete')")
    public void cancel(@PathVariable long id) {
        var principal = SecurityUtils.requirePrincipal();
        qualityInspectionApplicationService.cancel(id, principal.loginId());
    }
}
