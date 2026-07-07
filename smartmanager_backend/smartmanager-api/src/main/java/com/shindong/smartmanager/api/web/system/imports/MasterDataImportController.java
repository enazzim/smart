package com.shindong.smartmanager.api.web.system.imports;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.infrastructure.application.MasterDataImportApplicationService;
import jakarta.validation.Valid;
import org.springframework.http.HttpStatus;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/system/imports/master-data")
public class MasterDataImportController {

    private final MasterDataImportApplicationService masterDataImportApplicationService;

    public MasterDataImportController(MasterDataImportApplicationService masterDataImportApplicationService) {
        this.masterDataImportApplicationService = masterDataImportApplicationService;
    }

    @PostMapping("/company")
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('system:import:execute')")
    public BulkImportResponse importCompanies(@Valid @RequestBody CompanyImportBulkRequest request) {
        return BulkImportResponse.from(masterDataImportApplicationService.importCompanies(
                request.toRows(),
                SecurityUtils.requirePrincipal().loginId()
        ));
    }

    @PostMapping("/item")
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('system:import:execute')")
    public BulkImportResponse importItems(@Valid @RequestBody ItemImportBulkRequest request) {
        return BulkImportResponse.from(masterDataImportApplicationService.importItems(
                request.toRows(),
                SecurityUtils.requirePrincipal().loginId()
        ));
    }

    @PostMapping("/item-composition")
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('system:import:execute')")
    public BulkImportResponse importItemCompositions(@Valid @RequestBody ItemCompositionImportBulkRequest request) {
        return BulkImportResponse.from(masterDataImportApplicationService.importItemCompositions(
                request.toRows(),
                SecurityUtils.requirePrincipal().loginId()
        ));
    }

    @PostMapping("/work-center")
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('system:import:execute')")
    public BulkImportResponse importWorkCenters(@Valid @RequestBody WorkCenterImportBulkRequest request) {
        return BulkImportResponse.from(masterDataImportApplicationService.importWorkCenters(
                request.toRows(),
                SecurityUtils.requirePrincipal().loginId()
        ));
    }

    @PostMapping("/process")
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('system:import:execute')")
    public BulkImportResponse importProcesses(@Valid @RequestBody ProcessImportBulkRequest request) {
        return BulkImportResponse.from(masterDataImportApplicationService.importProcesses(
                request.toRows(),
                SecurityUtils.requirePrincipal().loginId()
        ));
    }

    @PostMapping("/work-standard")
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('system:import:execute')")
    public BulkImportResponse importWorkStandards(@Valid @RequestBody WorkStandardImportBulkRequest request) {
        return BulkImportResponse.from(masterDataImportApplicationService.importWorkStandards(
                request.toRows(),
                SecurityUtils.requirePrincipal().loginId()
        ));
    }

    @PostMapping("/unit-price")
    @ResponseStatus(HttpStatus.CREATED)
    @PreAuthorize("hasAuthority('system:import:execute')")
    public BulkImportResponse importUnitPrices(@Valid @RequestBody UnitPriceImportBulkRequest request) {
        return BulkImportResponse.from(masterDataImportApplicationService.importUnitPrices(
                request.toRows(),
                SecurityUtils.requirePrincipal().loginId()
        ));
    }
}
