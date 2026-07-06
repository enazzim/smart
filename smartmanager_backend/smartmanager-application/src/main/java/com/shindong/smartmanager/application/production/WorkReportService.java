package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.application.bom.ItemCompositionRepository;
import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.FiscalPeriod;
import com.shindong.smartmanager.application.closing.MonthClosingService;
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
            SystemSettingService systemSettingService
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
    }

    public List<WorkOrderView> listReportTargets() {
        return workReportRepository.findReportTargets();
    }

    public List<WorkReportView> list(WorkReportListCriteria criteria) {
        return workReportRepository.findAllActive(criteria);
    }

    public List<MaterialIssueOnHandView> listIssueOnHand(long workOrderId, LocalDate reportDate) {
        WorkOrderView order = workOrderRepository.findActiveIssuedById(workOrderId)
                .orElseThrow(() -> new IllegalArgumentException("작업지시를 찾을 수 없습니다: " + workOrderId));
        LocalDate stockDate = reportDate != null ? reportDate : LocalDate.now();

        return itemCompositionRepository.findActiveByParentItemId(order.itemId()).stream()
                .map(bomLine -> new MaterialIssueOnHandView(
                        bomLine.childItemId(),
                        workReportConsumptionInventoryService.resolveOnHandQty(
                                bomLine.childItemId(),
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
                .orElseThrow(() -> new IllegalArgumentException("작업지시를 찾을 수 없습니다: " + workOrderId));

        BigDecimal pending = pendingGoodQty != null ? pendingGoodQty : BigDecimal.ZERO;
        if (pending.compareTo(BigDecimal.ZERO) < 0) {
            throw new IllegalArgumentException("양품 수량은 0 이상이어야 합니다.");
        }

        boolean materialIssueEnabled = systemSettingService.isMaterialIssueEnabled();
        List<WorkReportConsumptionLineView> lines;
        boolean allSatisfied;
        BigDecimal cumulativeGoodQty;

        if (materialIssueEnabled) {
            cumulativeGoodQty = order.reportedQty().add(pending);
            Map<Long, BigDecimal> issuedQtyByCompositionId =
                    materialIssueRepository.sumIssuedQtyByWorkOrderId(workOrderId);
            lines = bomConsumptionCalculator.calculateLines(
                    order.itemId(),
                    order.processSequenceNum(),
                    cumulativeGoodQty,
                    issuedQtyByCompositionId
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
        if (materialIssueEnabled) {
            assertMaterialIssueSatisfied(context, command.goodQty());
        } else {
            consumptionCommands = resolveConsumptionCommands(
                    context,
                    command.goodQty(),
                    command.issueLines(),
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
        }

        workReportInventoryService.applyRegistration(
                context,
                command.goodQty(),
                scrapQty,
                saved.id(),
                actorUserId
        );

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
                .orElseThrow(() -> new IllegalArgumentException("작업일보를 찾을 수 없습니다: " + id));
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

        workReportInventoryService.applyCancellation(
                context,
                report.goodQty(),
                report.scrapQty(),
                report.id(),
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
            result.add(workReportConsumptionInventoryService.resolveSaveCommand(
                    line,
                    context.itemId(),
                    context.processSequenceNum(),
                    actorUserId
            ));
        }
        return result;
    }

    private void assertMaterialIssueSatisfied(WorkReportRegistrationContext context, BigDecimal pendingGoodQty) {
        BigDecimal cumulativeGoodQty = context.reportedQty().add(pendingGoodQty);
        Map<Long, BigDecimal> issuedQtyByCompositionId =
                materialIssueRepository.sumIssuedQtyByWorkOrderId(context.workOrderId());

        List<WorkReportConsumptionLineView> lines = bomConsumptionCalculator.calculateLines(
                context.itemId(),
                context.processSequenceNum(),
                cumulativeGoodQty,
                issuedQtyByCompositionId
        );

        for (WorkReportConsumptionLineView line : lines) {
            if (!line.satisfied()) {
                throw new IllegalArgumentException(String.format(
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
                .orElseThrow(() -> new IllegalArgumentException("작업지시를 찾을 수 없습니다: " + workOrderId));
        WorkPlanView plan = workPlanRepository.findActiveById(order.workPlanId())
                .orElseThrow(() -> new IllegalArgumentException("작업계획을 찾을 수 없습니다: " + order.workPlanId()));
        if (plan.workDistinction() == WorkDistinction.OUTSOURCE) {
            throw new IllegalArgumentException("외주 공정은 작업일보를 등록할 수 없습니다.");
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
            throw new IllegalArgumentException("수량은 0 이상이어야 합니다.");
        }
        if (good.add(scrap).compareTo(BigDecimal.ZERO) <= 0) {
            throw new IllegalArgumentException("작업수량(양품+불량)을 입력하세요.");
        }
        if (command.reportDate() == null) {
            throw new IllegalArgumentException("실적일을 입력하세요.");
        }
    }

    private static void validateQuantities(
            WorkReportRegistrationContext context,
            BigDecimal goodQty,
            BigDecimal scrapQty
    ) {
        BigDecimal total = goodQty.add(scrapQty != null ? scrapQty : BigDecimal.ZERO);
        if (total.compareTo(context.remainingQty()) > 0) {
            throw new IllegalArgumentException("작업지시 잔량을 초과할 수 없습니다. 잔량: " + context.remainingQty());
        }
    }

    private String nextReportNum(LocalDate reportDate) {
        String prefix = "WR-" + reportDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = workReportRepository.countByReportNumPrefix(prefix) + 1;
        return prefix + seq;
    }
}
