package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.application.bom.ItemCompositionRepository;
import com.shindong.smartmanager.application.bom.ItemCompositionView;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.system.SystemSettingService;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import com.shindong.smartmanager.domain.production.MaterialIssueStatus;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class MaterialIssueService {

    private final MaterialIssueRepository materialIssueRepository;
    private final WorkOrderRepository workOrderRepository;
    private final WorkPlanRepository workPlanRepository;
    private final MonthClosingService monthClosingService;
    private final BomConsumptionCalculator bomConsumptionCalculator;
    private final MaterialIssueInventoryService materialIssueInventoryService;
    private final ItemCompositionRepository itemCompositionRepository;
    private final SystemSettingService systemSettingService;

    public MaterialIssueService(
            MaterialIssueRepository materialIssueRepository,
            WorkOrderRepository workOrderRepository,
            WorkPlanRepository workPlanRepository,
            MonthClosingService monthClosingService,
            BomConsumptionCalculator bomConsumptionCalculator,
            MaterialIssueInventoryService materialIssueInventoryService,
            ItemCompositionRepository itemCompositionRepository,
            SystemSettingService systemSettingService
    ) {
        this.materialIssueRepository = materialIssueRepository;
        this.workOrderRepository = workOrderRepository;
        this.workPlanRepository = workPlanRepository;
        this.monthClosingService = monthClosingService;
        this.bomConsumptionCalculator = bomConsumptionCalculator;
        this.materialIssueInventoryService = materialIssueInventoryService;
        this.itemCompositionRepository = itemCompositionRepository;
        this.systemSettingService = systemSettingService;
    }

    public List<WorkOrderView> listIssueTargets() {
        return materialIssueRepository.findIssueTargets();
    }

    public List<MaterialIssueView> list(MaterialIssueListCriteria criteria) {
        return materialIssueRepository.findAllActive(criteria);
    }

    public MaterialIssueConsumptionPreviewView getConsumptionPreview(long workOrderId, BigDecimal goodQty) {
        WorkOrderView order = workOrderRepository.findActiveIssuedById(workOrderId)
                .orElseThrow(() -> new IllegalArgumentException("작업지시를 찾을 수 없습니다: " + workOrderId));

        BigDecimal qty = goodQty != null ? goodQty : BigDecimal.ZERO;
        if (qty.compareTo(BigDecimal.ZERO) < 0) {
            throw new IllegalArgumentException("양품 수량은 0 이상이어야 합니다.");
        }

        List<MaterialIssuePreviewLineView> lines = bomConsumptionCalculator.calculateLines(
                order.itemId(),
                order.processSequenceNum(),
                qty,
                Map.of()
        ).stream()
                .map(line -> new MaterialIssuePreviewLineView(
                        line.itemCompositionId(),
                        line.itemId(),
                        line.itemNo(),
                        line.itemName(),
                        line.propertyClassification(),
                        line.unitRatio(),
                        line.requiredQty(),
                        line.sourceProcessId(),
                        line.sourceProcessName()
                ))
                .toList();

        return new MaterialIssueConsumptionPreviewView(
                order.id(),
                order.itemNo(),
                qty,
                lines
        );
    }

    public List<MaterialIssueOnHandView> listOnHand(long workOrderId, LocalDate issueDate) {
        WorkOrderView order = workOrderRepository.findActiveIssuedById(workOrderId)
                .orElseThrow(() -> new IllegalArgumentException("작업지시를 찾을 수 없습니다: " + workOrderId));
        LocalDate stockDate = issueDate != null ? issueDate : LocalDate.now();

        return bomConsumptionCalculator.calculateLines(
                order.itemId(),
                order.processSequenceNum(),
                BigDecimal.ONE,
                Map.of()
        ).stream()
                .map(line -> new MaterialIssueOnHandView(
                        line.itemId(),
                        materialIssueInventoryService.resolveOnHandQty(
                                line.itemId(),
                                order.itemId(),
                                order.processSequenceNum(),
                                stockDate
                        )
                ))
                .toList();
    }

    public MaterialIssueView register(CreateMaterialIssueCommand command, String actorUserId) {
        if (!systemSettingService.isMaterialIssueEnabled()) {
            throw new IllegalArgumentException(
                    "자재투입 기능이 사용하지 않도록 설정되어 있습니다. 작업일보 등록 시 자재가 자동 반영됩니다."
            );
        }
        validateCommand(command);
        monthClosingService.assertTransactionOpen(command.issueDate());

        WorkOrderView order = workOrderRepository.findActiveIssuedById(command.workOrderId())
                .orElseThrow(() -> new IllegalArgumentException("작업지시를 찾을 수 없습니다: " + command.workOrderId()));
        WorkPlanView plan = workPlanRepository.findActiveById(order.workPlanId())
                .orElseThrow(() -> new IllegalArgumentException("작업계획을 찾을 수 없습니다: " + order.workPlanId()));
        if (plan.workDistinction() == WorkDistinction.OUTSOURCE) {
            throw new IllegalArgumentException("외주 공정은 자재투입을 등록할 수 없습니다.");
        }

        List<MaterialIssueLineSaveCommand> lineCommands = resolveLines(
                order.itemId(),
                order.processSequenceNum(),
                command.lines()
        );
        if (lineCommands.isEmpty()) {
            throw new IllegalArgumentException("투입 라인을 1건 이상 입력하세요.");
        }
        materialIssueInventoryService.assertSufficientStock(command.issueDate(), lineCommands);

        String issueNum = nextIssueNum(command.issueDate());
        MaterialIssueView saved = materialIssueRepository.save(
                new MaterialIssueSaveCommand(
                        issueNum,
                        command.workOrderId(),
                        command.issueDate(),
                        MaterialIssueStatus.ISSUED
                ),
                actorUserId
        );

        List<MaterialIssueLineRecordView> records = materialIssueRepository.saveLines(
                saved.id(),
                lineCommands,
                actorUserId
        );
        materialIssueInventoryService.applyRegistration(command.issueDate(), saved.id(), records, actorUserId);

        return materialIssueRepository.findActiveIssuedById(saved.id()).orElse(saved);
    }

    public void cancel(long id, String actorUserId) {
        MaterialIssueView issue = materialIssueRepository.findActiveIssuedById(id)
                .orElseThrow(() -> new IllegalArgumentException("자재투입을 찾을 수 없습니다: " + id));
        monthClosingService.assertTransactionOpen(issue.issueDate());

        List<MaterialIssueLineRecordView> lines = materialIssueRepository.findActiveLinesByIssueId(issue.id());
        materialIssueInventoryService.applyCancellation(issue.issueDate(), issue.id(), lines, actorUserId);
        materialIssueRepository.cancelById(id, actorUserId);
    }

    private List<MaterialIssueLineSaveCommand> resolveLines(
            long parentItemId,
            short processSequenceNum,
            List<MaterialIssueLineCommand> lines
    ) {
        if (lines == null || lines.isEmpty()) {
            return List.of();
        }
        Map<Long, ItemCompositionView> bomByCompositionId = itemCompositionRepository
                .findActiveByParentItemId(parentItemId).stream()
                .collect(java.util.stream.Collectors.toMap(ItemCompositionView::id, line -> line));

        List<MaterialIssueLineSaveCommand> result = new ArrayList<>();
        for (MaterialIssueLineCommand line : lines) {
            if (line.issueQty() == null || line.issueQty().compareTo(BigDecimal.ZERO) <= 0) {
                continue;
            }
            long itemId;
            if (line.itemCompositionId() != null) {
                ItemCompositionView bomLine = bomByCompositionId.get(line.itemCompositionId());
                if (bomLine == null) {
                    throw new IllegalArgumentException("유효하지 않은 BOM 투입 라인입니다.");
                }
                itemId = bomLine.childItemId();
            } else if (line.itemId() != null) {
                itemId = line.itemId();
            } else {
                throw new IllegalArgumentException("투입 품목을 지정해 주세요.");
            }
            result.add(materialIssueInventoryService.resolveSaveCommand(
                    line,
                    itemId,
                    parentItemId,
                    processSequenceNum
            ));
        }
        return result;
    }

    private static void validateCommand(CreateMaterialIssueCommand command) {
        if (command.issueDate() == null) {
            throw new IllegalArgumentException("투입일을 입력하세요.");
        }
        if (command.lines() == null || command.lines().isEmpty()) {
            throw new IllegalArgumentException("투입 라인을 입력하세요.");
        }
    }

    private String nextIssueNum(LocalDate issueDate) {
        String prefix = "MI-" + issueDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = materialIssueRepository.countByIssueNumPrefix(prefix) + 1;
        return prefix + seq;
    }
}
