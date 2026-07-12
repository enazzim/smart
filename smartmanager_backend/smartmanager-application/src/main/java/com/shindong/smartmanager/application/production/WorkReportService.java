package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.application.bom.ItemCompositionRepository;
import com.shindong.smartmanager.application.common.AppBusinessException;
import com.shindong.smartmanager.application.common.AppErrorCode;
import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.FiscalPeriod;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.inventory.LotGenealogyParentQty;
import com.shindong.smartmanager.application.inventory.LotService;
import com.shindong.smartmanager.application.system.SystemSettingService;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import com.shindong.smartmanager.domain.production.WorkReportHistorySourceType;
import com.shindong.smartmanager.domain.production.WorkReportStatus;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class WorkReportService {

    private final WorkReportRepository workReportRepository;
    private final WorkOrderRepository workOrderRepository;
    private final WorkPlanRepository workPlanRepository;
    private final ProductionPlanRepository productionPlanRepository;
    private final WorkReportInventoryService workReportInventoryService;
    private final WorkReportConsumptionInventoryService workReportConsumptionInventoryService;
    private final MonthClosingService monthClosingService;
    private final FiscalCalendarService fiscalCalendarService;
    private final BomConsumptionCalculator bomConsumptionCalculator;
    private final MaterialIssueRepository materialIssueRepository;
    private final ItemCompositionRepository itemCompositionRepository;
    private final SystemSettingService systemSettingService;
    private final LotService lotService;

    public WorkReportService(
            WorkReportRepository workReportRepository,
            WorkOrderRepository workOrderRepository,
            WorkPlanRepository workPlanRepository,
            ProductionPlanRepository productionPlanRepository,
            WorkReportInventoryService workReportInventoryService,
            WorkReportConsumptionInventoryService workReportConsumptionInventoryService,
            MonthClosingService monthClosingService,
            FiscalCalendarService fiscalCalendarService,
            BomConsumptionCalculator bomConsumptionCalculator,
            MaterialIssueRepository materialIssueRepository,
            ItemCompositionRepository itemCompositionRepository,
            SystemSettingService systemSettingService,
            LotService lotService
    ) {
        this.workReportRepository = workReportRepository;
        this.workOrderRepository = workOrderRepository;
        this.workPlanRepository = workPlanRepository;
        this.productionPlanRepository = productionPlanRepository;
        this.workReportInventoryService = workReportInventoryService;
        this.workReportConsumptionInventoryService = workReportConsumptionInventoryService;
        this.monthClosingService = monthClosingService;
        this.fiscalCalendarService = fiscalCalendarService;
        this.bomConsumptionCalculator = bomConsumptionCalculator;
        this.materialIssueRepository = materialIssueRepository;
        this.itemCompositionRepository = itemCompositionRepository;
        this.systemSettingService = systemSettingService;
        this.lotService = lotService;
    }

    public List<WorkOrderView> listReportTargets() {
        return workReportRepository.findReportTargets();
    }

    public List<WorkReportView> list(WorkReportListCriteria criteria) {
        return workReportRepository.findAllActive(criteria);
    }

    public List<MaterialIssueOnHandView> listIssueOnHand(long workOrderId, LocalDate reportDate) {
        WorkOrderView order = workOrderRepository.findActiveIssuedById(workOrderId)
                .orElseThrow(() -> new AppBusinessException(
                        AppErrorCode.WORK_ORDER_NOT_FOUND,
                        "작업지시를 찾을 수 없습니다: " + workOrderId
                ));
        LocalDate stockDate = reportDate != null ? reportDate : LocalDate.now();

        return bomConsumptionCalculator.calculateLines(
                order.itemId(),
                order.processSequenceNum(),
                BigDecimal.ONE,
                Map.of()
        ).stream()
                .map(line -> new MaterialIssueOnHandView(
                        line.itemId(),
                        workReportConsumptionInventoryService.resolveOnHandQty(
                                line.itemId(),
                                order.itemId(),
                                order.processSequenceNum(),
                                stockDate,
                                null
                        )
                ))
                .toList();
    }

    public WorkReportConsumptionStatusView getConsumptionStatus(long workOrderId, BigDecimal pendingGoodQty) {
        WorkOrderView order = workOrderRepository.findActiveIssuedById(workOrderId)
                .orElseThrow(() -> new AppBusinessException(
                        AppErrorCode.WORK_ORDER_NOT_FOUND,
                        "작업지시를 찾을 수 없습니다: " + workOrderId
                ));

        BigDecimal pending = pendingGoodQty != null ? pendingGoodQty : BigDecimal.ZERO;
        if (pending.compareTo(BigDecimal.ZERO) < 0) {
            throw new AppBusinessException(AppErrorCode.WORK_REPORT_QUANTITY_INVALID, "양품 수량은 0 이상이어야 합니다.");
        }

        boolean materialIssueEnabled = systemSettingService.isMaterialIssueEnabled();
        List<WorkReportConsumptionLineView> lines;
        boolean allSatisfied;
        BigDecimal cumulativeGoodQty;

        if (materialIssueEnabled) {
            cumulativeGoodQty = order.reportedQty().add(pending);
            Map<Long, BigDecimal> issuedQtyByCompositionId =
                    materialIssueRepository.sumIssuedQtyByWorkOrderId(workOrderId);
            Map<Long, BigDecimal> issuedQtyByItemId =
                    materialIssueRepository.sumIssuedQtyByItemIdForWorkOrder(workOrderId);
            lines = bomConsumptionCalculator.calculateLines(
                    order.itemId(),
                    order.processSequenceNum(),
                    cumulativeGoodQty,
                    issuedQtyByCompositionId,
                    issuedQtyByItemId
            );
            allSatisfied = lines.isEmpty() || lines.stream().allMatch(WorkReportConsumptionLineView::satisfied);
        } else {
            cumulativeGoodQty = pending;
            lines = bomConsumptionCalculator.calculateLines(
                    order.itemId(),
                    order.processSequenceNum(),
                    pending,
                    Map.of()
            ).stream()
                    .map(line -> new WorkReportConsumptionLineView(
                            line.itemCompositionId(),
                            line.itemId(),
                            line.itemNo(),
                            line.itemName(),
                            line.propertyClassification(),
                            line.lotTracked(),
                            line.locationCode(),
                            line.unitRatio(),
                            line.requiredQty(),
                            BigDecimal.ZERO,
                            line.requiredQty(),
                            true,
                            line.sourceProcessId(),
                            line.sourceProcessName(),
                            line.requiredQty()
                    ))
                    .toList();
            allSatisfied = true;
        }

        return new WorkReportConsumptionStatusView(
                order.id(),
                order.itemNo(),
                order.itemName(),
                cumulativeGoodQty,
                pending,
                lines,
                allSatisfied,
                materialIssueEnabled
        );
    }

    public WorkReportView register(CreateWorkReportCommand command, String actorUserId) {
        validateCommand(command);
        monthClosingService.assertTransactionOpen(command.reportDate());

        WorkReportRegistrationContext context = loadContext(command.workOrderId(), command.reportDate());
        BigDecimal scrapQty = command.scrapQty() != null ? command.scrapQty() : BigDecimal.ZERO;
        validateQuantities(context, command.goodQty(), scrapQty);

        boolean materialIssueEnabled = systemSettingService.isMaterialIssueEnabled();
        List<WorkReportConsumptionSaveCommand> consumptionCommands = List.of();
        Long preferredWipLotId = null;
        List<LotGenealogyParentQty> genealogyParents = new ArrayList<>();
        if (materialIssueEnabled) {
            assertMaterialIssueSatisfied(context, command.goodQty());
        } else {
            SplitIssueLines split = splitIssueLines(context.itemId(), command.issueLines());
            preferredWipLotId = split.preferredWipLotId();
            consumptionCommands = resolveConsumptionCommands(
                    context,
                    command.goodQty(),
                    split.materialLines(),
                    actorUserId
            );
            if (!consumptionCommands.isEmpty()) {
                workReportConsumptionInventoryService.assertSufficientStock(command.reportDate(), consumptionCommands);
            }
        }

        String reportNum = nextReportNum(command.reportDate());
        WorkReportView saved = workReportRepository.save(
                new WorkReportSaveCommand(
                        reportNum,
                        command.workOrderId(),
                        command.reportDate(),
                        command.goodQty(),
                        scrapQty,
                        command.setupTime(),
                        command.runTime(),
                        command.workerName(),
                        WorkReportStatus.REGISTERED,
                        true
                ),
                actorUserId
        );

        if (!materialIssueEnabled && !consumptionCommands.isEmpty()) {
            List<WorkReportConsumptionRecordView> consumptionRecords = workReportRepository.saveConsumptionLines(
                    saved.id(),
                    consumptionCommands,
                    actorUserId
            );
            workReportConsumptionInventoryService.applyRegistration(
                    command.reportDate(),
                    saved.id(),
                    consumptionRecords,
                    actorUserId
            );
            genealogyParents.addAll(toGenealogyParents(consumptionRecords));
        }

        Long outputLotId = workReportInventoryService.applyRegistration(
                context,
                command.goodQty(),
                scrapQty,
                saved.id(),
                preferredWipLotId,
                null,
                true,
                actorUserId
        );
        if (outputLotId != null) {
            workReportRepository.updateOutputLotId(saved.id(), outputLotId, actorUserId);
            lotService.recordWorkReportGenealogy(saved.id(), outputLotId, genealogyParents, actorUserId);
        }

        FiscalPeriod period = fiscalCalendarService.resolvePeriod(command.reportDate());
        workReportRepository.saveHistory(
                new WorkReportHistorySaveCommand(
                        context.itemId(),
                        context.productionPlanId(),
                        context.processSequenceId(),
                        command.goodQty(),
                        scrapQty,
                        command.reportDate(),
                        WorkReportHistorySourceType.WORK_REPORT,
                        saved.id(),
                        period.fiscalYear(),
                        period.fiscalMonth()
                ),
                actorUserId
        );

        workOrderRepository.addReportedQty(command.workOrderId(), command.goodQty(), actorUserId);
        productionPlanRepository.addProducedQty(context.productionPlanId(), command.goodQty(), actorUserId);

        return workReportRepository.findActiveRegisteredById(saved.id()).orElse(saved);
    }

    public void cancel(long id, String actorUserId) {
        WorkReportView report = workReportRepository.findActiveRegisteredById(id)
                .orElseThrow(() -> new AppBusinessException(
                        AppErrorCode.WORK_REPORT_NOT_FOUND,
                        "작업일보를 찾을 수 없습니다: " + id
                ));
        monthClosingService.assertTransactionOpen(report.reportDate());

        WorkReportRegistrationContext context = loadContext(report.workOrderId(), report.reportDate());

        List<WorkReportConsumptionRecordView> consumptionLines =
                workReportRepository.findActiveConsumptionLinesByReportId(report.id());
        if (!consumptionLines.isEmpty()) {
            workReportConsumptionInventoryService.applyCancellation(
                    report.reportDate(),
                    report.id(),
                    consumptionLines,
                    actorUserId
            );
            workReportRepository.deactivateConsumptionLinesByReportId(report.id(), actorUserId);
        }

        lotService.deactivateWorkReportGenealogy(report.id());

        Long outputLotId = workReportRepository.findOutputLotId(report.id()).orElse(null);
        workReportInventoryService.applyCancellation(
                context,
                report.goodQty(),
                report.scrapQty(),
                report.id(),
                outputLotId,
                actorUserId
        );

        workReportRepository.deactivateHistory(WorkReportHistorySourceType.WORK_REPORT, report.id(), actorUserId);
        workReportRepository.cancelById(id, actorUserId);
        workOrderRepository.subtractReportedQty(report.workOrderId(), report.goodQty(), actorUserId);
        productionPlanRepository.subtractProducedQty(context.productionPlanId(), report.goodQty(), actorUserId);
    }

    private List<WorkReportConsumptionSaveCommand> resolveConsumptionCommands(
            WorkReportRegistrationContext context,
            BigDecimal goodQty,
            List<WorkReportIssueLineCommand> issueLines,
            String actorUserId
    ) {
        List<WorkReportIssueLineCommand> effectiveLines = issueLines;
        if (effectiveLines == null || effectiveLines.isEmpty()) {
            effectiveLines = bomConsumptionCalculator.calculateLines(
                    context.itemId(),
                    context.processSequenceNum(),
                    goodQty,
                    Map.of()
            ).stream()
                    .filter(line -> line.requiredQty().compareTo(BigDecimal.ZERO) > 0)
                    .map(line -> new WorkReportIssueLineCommand(
                            line.itemCompositionId(),
                            line.itemId(),
                            line.requiredQty()
                    ))
                    .toList();
        }

        List<WorkReportConsumptionSaveCommand> result = new ArrayList<>();
        for (WorkReportIssueLineCommand line : effectiveLines) {
            if (line.issueQty() == null || line.issueQty().compareTo(BigDecimal.ZERO) <= 0) {
                continue;
            }
            // 모품목 직전공정 투입은 WorkReportInventoryService가 현재 공정 WIP에서 처리한다.
            if (line.itemCompositionId() == null && line.itemId() == context.itemId()) {
                continue;
            }
            result.add(workReportConsumptionInventoryService.resolveSaveCommand(
                    line,
                    context.itemId(),
                    context.processSequenceNum(),
                    actorUserId
            ));
        }
        return result;
    }

    /**
     * 비첫 공정 모품목 라인의 lotId는 WIP 이동용으로 분리하고, BOM 자재만 백플러시 소비한다.
     */
    private SplitIssueLines splitIssueLines(long parentItemId, List<WorkReportIssueLineCommand> issueLines) {
        if (issueLines == null || issueLines.isEmpty()) {
            return new SplitIssueLines(null, List.of());
        }
        Long preferredWipLotId = null;
        List<WorkReportIssueLineCommand> materialLines = new ArrayList<>();
        for (WorkReportIssueLineCommand line : issueLines) {
            if (line.itemCompositionId() == null && line.itemId() == parentItemId) {
                preferredWipLotId = line.lotId();
            } else {
                materialLines.add(line);
            }
        }
        return new SplitIssueLines(preferredWipLotId, materialLines);
    }

    private record SplitIssueLines(Long preferredWipLotId, List<WorkReportIssueLineCommand> materialLines) {
    }

    private static List<LotGenealogyParentQty> toGenealogyParents(List<WorkReportConsumptionRecordView> records) {
        List<LotGenealogyParentQty> parents = new ArrayList<>();
        for (WorkReportConsumptionRecordView record : records) {
            if (record.lotId() == null || record.issueQty() == null
                    || record.issueQty().compareTo(BigDecimal.ZERO) <= 0) {
                continue;
            }
            parents.add(new LotGenealogyParentQty(record.lotId(), record.issueQty()));
        }
        return parents;
    }

    private void assertMaterialIssueSatisfied(WorkReportRegistrationContext context, BigDecimal pendingGoodQty) {
        BigDecimal cumulativeGoodQty = context.reportedQty().add(pendingGoodQty);
        Map<Long, BigDecimal> issuedQtyByCompositionId =
                materialIssueRepository.sumIssuedQtyByWorkOrderId(context.workOrderId());
        Map<Long, BigDecimal> issuedQtyByItemId =
                materialIssueRepository.sumIssuedQtyByItemIdForWorkOrder(context.workOrderId());

        List<WorkReportConsumptionLineView> lines = bomConsumptionCalculator.calculateLines(
                context.itemId(),
                context.processSequenceNum(),
                cumulativeGoodQty,
                issuedQtyByCompositionId,
                issuedQtyByItemId
        );

        for (WorkReportConsumptionLineView line : lines) {
            if (!line.satisfied()) {
                throw new AppBusinessException(AppErrorCode.MATERIAL_ISSUE_SHORTAGE, String.format(
                        "자재투입이 부족합니다: %s (필요 %s, 투입 %s)",
                        line.itemNo(),
                        line.requiredQty().stripTrailingZeros().toPlainString(),
                        line.issuedQty().stripTrailingZeros().toPlainString()
                ));
            }
        }
    }

    private WorkReportRegistrationContext loadContext(long workOrderId, LocalDate reportDate) {
        WorkOrderView order = workOrderRepository.findActiveIssuedById(workOrderId)
                .orElseThrow(() -> new AppBusinessException(
                        AppErrorCode.WORK_ORDER_NOT_FOUND,
                        "작업지시를 찾을 수 없습니다: " + workOrderId
                ));
        WorkPlanView plan = workPlanRepository.findActiveById(order.workPlanId())
                .orElseThrow(() -> new AppBusinessException(
                        AppErrorCode.WORK_PLAN_NOT_FOUND,
                        "작업계획을 찾을 수 없습니다: " + order.workPlanId()
                ));
        if (plan.workDistinction() == WorkDistinction.OUTSOURCE) {
            throw new AppBusinessException(
                    AppErrorCode.OUTSOURCE_WORK_REPORT_NOT_ALLOWED,
                    "외주 공정은 작업일보를 등록할 수 없습니다."
            );
        }
        return new WorkReportRegistrationContext(
                order.id(),
                order.workPlanId(),
                order.productionPlanId(),
                order.itemId(),
                order.processSequenceId(),
                order.processSequenceNum(),
                order.orderedQty(),
                order.reportedQty(),
                reportDate
        );
    }

    private static void validateCommand(CreateWorkReportCommand command) {
        BigDecimal good = command.goodQty() != null ? command.goodQty() : BigDecimal.ZERO;
        BigDecimal scrap = command.scrapQty() != null ? command.scrapQty() : BigDecimal.ZERO;
        if (good.compareTo(BigDecimal.ZERO) < 0 || scrap.compareTo(BigDecimal.ZERO) < 0) {
            throw new AppBusinessException(AppErrorCode.WORK_REPORT_QUANTITY_INVALID, "수량은 0 이상이어야 합니다.");
        }
        if (good.add(scrap).compareTo(BigDecimal.ZERO) <= 0) {
            throw new AppBusinessException(
                    AppErrorCode.WORK_REPORT_QUANTITY_INVALID,
                    "작업수량(양품+불량)을 입력하세요."
            );
        }
        if (command.reportDate() == null) {
            throw new AppBusinessException(AppErrorCode.WORK_REPORT_QUANTITY_INVALID, "실적일을 입력하세요.");
        }
    }

    private static void validateQuantities(
            WorkReportRegistrationContext context,
            BigDecimal goodQty,
            BigDecimal scrapQty
    ) {
        BigDecimal total = goodQty.add(scrapQty != null ? scrapQty : BigDecimal.ZERO);
        if (total.compareTo(context.remainingQty()) > 0) {
            throw new AppBusinessException(
                    AppErrorCode.WORK_REPORT_OVER_REMAINING_QTY,
                    "작업지시 잔량을 초과할 수 없습니다. 잔량: " + context.remainingQty()
            );
        }
    }

    private String nextReportNum(LocalDate reportDate) {
        String prefix = "WR-" + reportDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = workReportRepository.countByReportNumPrefix(prefix) + 1;
        return prefix + seq;
    }
}
