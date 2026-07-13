package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.application.common.AppBusinessException;
import com.shindong.smartmanager.application.common.AppErrorCode;
import com.shindong.smartmanager.domain.process.WorkDistinction;
import com.shindong.smartmanager.domain.production.WorkOrderStatus;
import com.shindong.smartmanager.domain.production.WorkPlanStatus;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;

public class WorkOrderService {

    private final WorkOrderRepository workOrderRepository;
    private final WorkPlanRepository workPlanRepository;

    public WorkOrderService(WorkOrderRepository workOrderRepository, WorkPlanRepository workPlanRepository) {
        this.workOrderRepository = workOrderRepository;
        this.workPlanRepository = workPlanRepository;
    }

    public List<WorkOrderView> listOrderTargets() {
        return workOrderRepository.findOrderTargets();
    }

    public List<WorkOrderView> list(WorkOrderListCriteria criteria) {
        return workOrderRepository.findAllActive(criteria);
    }

    public List<WorkOrderView> createOrders(List<Long> workPlanIds, String actorUserId) {
        if (workPlanIds == null || workPlanIds.isEmpty()) {
            throw new IllegalArgumentException("작업계획을 선택하세요.");
        }

        List<WorkOrderSaveCommand> commands = new ArrayList<>();
        LocalDate issueDate = LocalDate.now();
        String prefix = "WO-" + issueDate.format(DateTimeFormatter.BASIC_ISO_DATE) + "-";
        long seq = workOrderRepository.countByOrderNumPrefix(prefix);

        for (long workPlanId : workPlanIds) {
            WorkPlanView plan = workPlanRepository.findActivePlannedById(workPlanId)
                    .orElseThrow(() -> new IllegalArgumentException("작업계획을 찾을 수 없습니다: " + workPlanId));
            validateOrderTarget(plan);
            workOrderRepository.purgeInactiveByWorkPlanId(workPlanId);
            seq++;
            commands.add(new WorkOrderSaveCommand(
                    workPlanId,
                    prefix + seq,
                    plan.plannedQty()
            ));
        }

        return workOrderRepository.saveAll(commands, actorUserId);
    }

    public WorkOrderView cancel(long id, String actorUserId) {
        WorkOrderView order = workOrderRepository.findActiveIssuedById(id)
                .orElseThrow(() -> new AppBusinessException(
                        AppErrorCode.WORK_ORDER_NOT_FOUND,
                        "작업지시를 찾을 수 없습니다: " + id
                ));
        if (!order.cancellable()) {
            throw new AppBusinessException(
                    AppErrorCode.WORK_ORDER_CANCEL_BLOCKED,
                    "실적·작업일보·자재투입이 있는 작업지시는 취소할 수 없습니다: " + order.orderNum()
            );
        }
        if (workOrderRepository.hasActiveDownstream(id)) {
            throw new AppBusinessException(
                    AppErrorCode.WORK_ORDER_CANCEL_BLOCKED,
                    "등록된 작업일보 또는 자재투입이 있어 작업지시를 취소할 수 없습니다. 하위 전표를 먼저 취소해 주세요: "
                            + order.orderNum()
            );
        }
        // 취소된 작업일보·자재투입은 FK로 남아 물리 DELETE를 막으므로 정리 후 삭제한다.
        workOrderRepository.cancelById(id, actorUserId);
        return new WorkOrderView(
                order.id(),
                order.workPlanId(),
                order.orderNum(),
                order.productionPlanId(),
                order.planNo(),
                order.itemId(),
                order.itemNo(),
                order.itemName(),
                order.processSequenceId(),
                order.processSequenceNum(),
                order.processCode(),
                order.processName(),
                order.workCenterId(),
                order.workCenterName(),
                order.orderedQty(),
                order.reportedQty(),
                order.remainingQty(),
                order.planStartDate(),
                WorkOrderStatus.CANCELLED,
                false,
                order.createdAt(),
                order.createdBy()
        );
    }

    private static void validateOrderTarget(WorkPlanView plan) {
        if (plan.status() != WorkPlanStatus.PLANNED) {
            throw new IllegalArgumentException("수립 상태의 작업계획만 지시할 수 있습니다: " + plan.planNo());
        }
        if (plan.workDistinction() != WorkDistinction.INHOUSE) {
            throw new IllegalArgumentException("사내공정 작업계획만 지시할 수 있습니다: " + plan.processName());
        }
    }
}
