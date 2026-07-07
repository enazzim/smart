package com.shindong.smartmanager.application.system.imports;

import com.shindong.smartmanager.application.auth.AuthUserRepository;
import com.shindong.smartmanager.application.bom.ItemCompositionService;
import com.shindong.smartmanager.application.common.BulkFailure;
import com.shindong.smartmanager.application.common.BulkImportResult;
import com.shindong.smartmanager.application.company.CompanyCommand;
import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.company.CompanyService;
import com.shindong.smartmanager.application.company.CompanyView;
import com.shindong.smartmanager.application.equipment.EquipmentRepository;
import com.shindong.smartmanager.application.equipment.EquipmentView;
import com.shindong.smartmanager.application.item.ItemCommand;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemService;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.application.process.ProcessCommand;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessService;
import com.shindong.smartmanager.application.process.ProcessView;
import com.shindong.smartmanager.application.publiccode.PublicCodeRepository;
import com.shindong.smartmanager.application.publiccode.PublicCodeSmallView;
import com.shindong.smartmanager.application.unitprice.UnitPriceService;
import com.shindong.smartmanager.application.workcenter.WorkCenterCommand;
import com.shindong.smartmanager.application.workcenter.WorkCenterRepository;
import com.shindong.smartmanager.application.workcenter.WorkCenterService;
import com.shindong.smartmanager.application.workcenter.WorkCenterView;
import com.shindong.smartmanager.application.workstandard.WorkStandardService;
import com.shindong.smartmanager.domain.item.CheckDistinction;
import com.shindong.smartmanager.domain.process.ProcessVariant;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
import java.util.Optional;

public class MasterDataImportService {

    private static final String PROCESS_USAGE = "PROCESS";

    private final CompanyService companyService;
    private final CompanyRepository companyRepository;
    private final ItemService itemService;
    private final ItemRepository itemRepository;
    private final ItemCompositionService itemCompositionService;
    private final WorkCenterService workCenterService;
    private final WorkCenterRepository workCenterRepository;
    private final ProcessService processService;
    private final ProcessRepository processRepository;
    private final WorkStandardService workStandardService;
    private final UnitPriceService unitPriceService;
    private final PublicCodeRepository publicCodeRepository;
    private final EquipmentRepository equipmentRepository;
    private final AuthUserRepository authUserRepository;

    public MasterDataImportService(
            CompanyService companyService,
            CompanyRepository companyRepository,
            ItemService itemService,
            ItemRepository itemRepository,
            ItemCompositionService itemCompositionService,
            WorkCenterService workCenterService,
            WorkCenterRepository workCenterRepository,
            ProcessService processService,
            ProcessRepository processRepository,
            WorkStandardService workStandardService,
            UnitPriceService unitPriceService,
            PublicCodeRepository publicCodeRepository,
            EquipmentRepository equipmentRepository,
            AuthUserRepository authUserRepository
    ) {
        this.companyService = companyService;
        this.companyRepository = companyRepository;
        this.itemService = itemService;
        this.itemRepository = itemRepository;
        this.itemCompositionService = itemCompositionService;
        this.workCenterService = workCenterService;
        this.workCenterRepository = workCenterRepository;
        this.processService = processService;
        this.processRepository = processRepository;
        this.workStandardService = workStandardService;
        this.unitPriceService = unitPriceService;
        this.publicCodeRepository = publicCodeRepository;
        this.equipmentRepository = equipmentRepository;
        this.authUserRepository = authUserRepository;
    }

    public BulkImportResult importCompanies(List<CompanyImportRow> rows, String actorUserId) {
        return bulk(rows, actorUserId, row -> {
            companyService.register(
                    new CompanyCommand(
                            row.companyName(),
                            row.presidentName(),
                            normalizeBusinessRegNo(row.businessRegNo()),
                            blankToNull(row.corporationRegNo()),
                            row.businessAddress(),
                            blankToNull(row.homepageUrl()),
                            blankToNull(row.businessType()),
                            blankToNull(row.businessItem()),
                            blankToNull(row.telephone()),
                            blankToNull(row.fax()),
                            row.saleStandardDay(),
                            row.billApprovalStandard(),
                            row.fixCollectDay1(),
                            blankToNull(row.contactName()),
                            blankToNull(row.contactEmail()),
                            row.roles()
                    ),
                    actorUserId
            );
        }, row -> normalizeBusinessRegNo(row.businessRegNo()));
    }

    public BulkImportResult importItems(List<ItemImportRow> rows, String actorUserId) {
        return bulk(rows, actorUserId, row -> {
            itemService.register(
                    new ItemCommand(
                            row.itemNo().trim(),
                            row.itemName().trim(),
                            row.propertyClassification(),
                            row.unit().trim(),
                            blankToNull(row.standard()),
                            row.standardUnitCost(),
                            row.checkDistinction() != null ? row.checkDistinction() : CheckDistinction.NONE,
                            row.leadTime(),
                            row.safetyStockQuantity(),
                            row.orderIntervalQuantity(),
                            row.minOrderQuantity()
                    ),
                    actorUserId
            );
        }, row -> row.itemNo());
    }

    public BulkImportResult importItemCompositions(List<ItemCompositionImportRow> rows, String actorUserId) {
        return bulk(rows, actorUserId, row -> {
            ItemView parent = itemRepository.findActiveByItemNo(row.parentItemNum().trim())
                    .orElseThrow(() -> new IllegalArgumentException("모품목을 찾을 수 없습니다: " + row.parentItemNum()));
            ItemView child = itemRepository.findActiveByItemNo(row.childItemNum().trim())
                    .orElseThrow(() -> new IllegalArgumentException("자품목을 찾을 수 없습니다: " + row.childItemNum()));
            itemCompositionService.register(
                    new com.shindong.smartmanager.application.bom.ItemCompositionCommand(
                            parent.id(),
                            child.id(),
                            row.parentQuantity(),
                            row.childQuantity()
                    ),
                    actorUserId
            );
        }, row -> row.parentItemNum() + "->" + row.childItemNum());
    }

    public BulkImportResult importWorkCenters(List<WorkCenterImportRow> rows, String actorUserId) {
        return bulk(rows, actorUserId, row -> {
            long processCodeId = resolveProcessCodeId(row.mainProcessSmallCode());
            workCenterService.register(
                    new WorkCenterCommand(row.wcName().trim(), processCodeId, row.operationTime()),
                    actorUserId
            );
        }, WorkCenterImportRow::wcName);
    }

    public BulkImportResult importProcesses(List<ProcessImportRow> rows, String actorUserId) {
        return bulk(rows, actorUserId, row -> {
            ItemView item = itemRepository.findActiveByItemNo(row.itemNo().trim())
                    .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + row.itemNo()));
            long processCodeId = resolveProcessCodeId(row.processSmallCode());
            Long workCenterId = null;
            if (row.workDistinction() != WorkDistinction.OUTSOURCE) {
                if (row.workCenterName() == null || row.workCenterName().isBlank()) {
                    throw new IllegalArgumentException("사내·분할 공정은 작업장명이 필요합니다.");
                }
                workCenterId = resolveWorkCenterId(row.workCenterName());
            }
            int outsideOrderRate = row.outsideOrderRate() != null ? row.outsideOrderRate() : 0;
            short progressRate = row.progressRate() != null ? row.progressRate() : 100;
            processService.register(
                    new ProcessCommand(
                            item.id(),
                            row.processSequenceNum(),
                            processCodeId,
                            row.workDistinction(),
                            workCenterId,
                            outsideOrderRate,
                            progressRate
                    ),
                    actorUserId
            );
        }, row -> row.itemNo() + ":" + row.processSequenceNum());
    }

    public BulkImportResult importWorkStandards(List<WorkStandardImportRow> rows, String actorUserId) {
        return bulk(rows, actorUserId, row -> {
            ItemView item = itemRepository.findActiveByItemNo(row.itemNum().trim())
                    .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + row.itemNum()));
            long processCodeId = resolveProcessCodeId(row.processSmallCode());
            ProcessView process = processRepository.findActiveByItemIdAndPublicCodeIdAndSequence(
                    item.id(),
                    processCodeId,
                    row.processSequenceNum(),
                    ProcessVariant.plan
            ).orElseThrow(() -> new IllegalArgumentException(
                    "공정순서를 찾을 수 없습니다: " + row.itemNum() + " / " + row.processSequenceNum()
            ));
            long workCenterId = resolveWorkCenterId(row.workCenterName());
            Long equipmentId = resolveEquipmentId(row.equipmentNum());
            Long mainWorkerId = resolveUserId(row.mainWorkerLoginId());
            workStandardService.register(
                    new com.shindong.smartmanager.application.workstandard.WorkStandardCommand(
                            item.id(),
                            process.id(),
                            workCenterId,
                            equipmentId,
                            row.priorityOrder(),
                            mainWorkerId,
                            blankToNull(row.toolName()),
                            row.setupTime(),
                            row.standardTime()
                    ),
                    actorUserId
            );
        }, row -> row.itemNum() + ":" + row.processSequenceNum());
    }

    public BulkImportResult importUnitPrices(List<UnitPriceImportRow> rows, String actorUserId) {
        return bulk(rows, actorUserId, row -> {
            CompanyView company = companyRepository.findActiveByBusinessRegNo(normalizeBusinessRegNo(row.businessRegNo()))
                    .orElseThrow(() -> new IllegalArgumentException("거래처를 찾을 수 없습니다: " + row.businessRegNo()));
            ItemView item = itemRepository.findActiveByItemNo(row.itemNum().trim())
                    .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + row.itemNum()));
            Long beginProcessCodeId = resolveOptionalProcessCodeId(row.beginProcessSmallCode());
            Long endProcessCodeId = resolveOptionalProcessCodeId(row.endProcessSmallCode());
            unitPriceService.register(
                    new com.shindong.smartmanager.application.unitprice.UnitPriceCommand(
                            row.costType(),
                            item.id(),
                            company.id(),
                            beginProcessCodeId,
                            endProcessCodeId,
                            row.orderRate(),
                            row.standardUnitCost(),
                            row.discountUnitCost(),
                            row.beginDate(),
                            row.endDate()
                    ),
                    actorUserId
            );
        }, row -> row.itemNum() + ":" + row.businessRegNo());
    }

    private long resolveProcessCodeId(String smallCode) {
        return publicCodeRepository.findActiveSmallBySmallCode(smallCode.trim(), PROCESS_USAGE)
                .map(PublicCodeSmallView::id)
                .orElseThrow(() -> new IllegalArgumentException("공정코드를 찾을 수 없습니다: " + smallCode));
    }

    private Long resolveOptionalProcessCodeId(String smallCode) {
        if (smallCode == null || smallCode.isBlank()) {
            return null;
        }
        return resolveProcessCodeId(smallCode);
    }

    private long resolveWorkCenterId(String wcName) {
        return workCenterRepository.findAllActive(wcName.trim()).stream()
                .filter(wc -> wc.wcName().equals(wcName.trim()))
                .map(WorkCenterView::id)
                .findFirst()
                .orElseThrow(() -> new IllegalArgumentException("작업장을 찾을 수 없습니다: " + wcName));
    }

    private Long resolveEquipmentId(String equipmentNum) {
        if (equipmentNum == null || equipmentNum.isBlank()) {
            return null;
        }
        Optional<EquipmentView> found = equipmentRepository.findAllActive(equipmentNum.trim()).stream()
                .filter(eq -> eq.equipmentNum().equals(equipmentNum.trim()))
                .findFirst();
        return found.map(EquipmentView::id).orElse(null);
    }

    private Long resolveUserId(String loginId) {
        if (loginId == null || loginId.isBlank()) {
            return null;
        }
        return authUserRepository.findActiveByLoginId(loginId.trim())
                .map(AuthUserRepository.AuthUserRecord::id)
                .orElse(null);
    }

    private static String normalizeBusinessRegNo(String value) {
        return value.replaceAll("\\D", "");
    }

    private static String blankToNull(String value) {
        if (value == null || value.isBlank()) {
            return null;
        }
        return value.trim();
    }

    @FunctionalInterface
    private interface ImportAction<T> {
        void apply(T row);
    }

    @FunctionalInterface
    private interface KeyExtractor<T> {
        String key(T row);
    }

    private <T> BulkImportResult bulk(
            List<T> rows,
            String actorUserId,
            ImportAction<T> action,
            KeyExtractor<T> keyExtractor
    ) {
        if (rows == null || rows.isEmpty()) {
            throw new IllegalArgumentException("일괄 등록할 데이터가 없습니다.");
        }
        List<BulkFailure> failures = new ArrayList<>();
        int success = 0;
        for (int i = 0; i < rows.size(); i++) {
            T row = rows.get(i);
            try {
                action.apply(row);
                success++;
            } catch (RuntimeException ex) {
                failures.add(new BulkFailure(
                        i,
                        keyExtractor.key(row),
                        ex.getMessage() != null ? ex.getMessage() : "등록 실패"
                ));
            }
        }
        return new BulkImportResult(success, failures.size(), failures);
    }
}
