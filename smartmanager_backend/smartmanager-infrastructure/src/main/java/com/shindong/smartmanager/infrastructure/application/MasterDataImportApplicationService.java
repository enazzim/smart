package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.common.BulkImportResult;
import com.shindong.smartmanager.application.system.imports.CompanyImportRow;
import com.shindong.smartmanager.application.system.imports.ItemCompositionImportRow;
import com.shindong.smartmanager.application.system.imports.ItemImportRow;
import com.shindong.smartmanager.application.system.imports.MasterDataImportService;
import com.shindong.smartmanager.application.system.imports.ProcessImportRow;
import com.shindong.smartmanager.application.system.imports.UnitPriceImportRow;
import com.shindong.smartmanager.application.system.imports.WorkCenterImportRow;
import com.shindong.smartmanager.application.system.imports.WorkStandardImportRow;
import java.util.List;
import org.springframework.stereotype.Service;

@Service
public class MasterDataImportApplicationService {

    private final MasterDataImportService masterDataImportService;

    public MasterDataImportApplicationService(MasterDataImportService masterDataImportService) {
        this.masterDataImportService = masterDataImportService;
    }

    public BulkImportResult importCompanies(List<CompanyImportRow> rows, String actorUserId) {
        return masterDataImportService.importCompanies(rows, actorUserId);
    }

    public BulkImportResult importItems(List<ItemImportRow> rows, String actorUserId) {
        return masterDataImportService.importItems(rows, actorUserId);
    }

    public BulkImportResult importItemCompositions(List<ItemCompositionImportRow> rows, String actorUserId) {
        return masterDataImportService.importItemCompositions(rows, actorUserId);
    }

    public BulkImportResult importWorkCenters(List<WorkCenterImportRow> rows, String actorUserId) {
        return masterDataImportService.importWorkCenters(rows, actorUserId);
    }

    public BulkImportResult importProcesses(List<ProcessImportRow> rows, String actorUserId) {
        return masterDataImportService.importProcesses(rows, actorUserId);
    }

    public BulkImportResult importWorkStandards(List<WorkStandardImportRow> rows, String actorUserId) {
        return masterDataImportService.importWorkStandards(rows, actorUserId);
    }

    public BulkImportResult importUnitPrices(List<UnitPriceImportRow> rows, String actorUserId) {
        return masterDataImportService.importUnitPrices(rows, actorUserId);
    }
}
