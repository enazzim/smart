package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.production.CreateWorkReportCommand;
import com.shindong.smartmanager.application.production.MaterialIssueOnHandView;
import com.shindong.smartmanager.application.production.WorkOrderView;
import com.shindong.smartmanager.application.production.WorkReportConsumptionStatusView;
import com.shindong.smartmanager.application.production.WorkReportListCriteria;
import com.shindong.smartmanager.application.production.WorkReportService;
import com.shindong.smartmanager.application.production.WorkReportView;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class WorkReportApplicationService {

    private final WorkReportService workReportService;

    public WorkReportApplicationService(WorkReportService workReportService) {
        this.workReportService = workReportService;
    }

    @Transactional(readOnly = true)
    public List<WorkOrderView> listReportTargets() {
        return workReportService.listReportTargets();
    }

    @Transactional(readOnly = true)
    public List<WorkReportView> list(WorkReportListCriteria criteria) {
        return workReportService.list(criteria);
    }

    @Transactional(readOnly = true)
    public List<MaterialIssueOnHandView> listIssueOnHand(long workOrderId, LocalDate reportDate) {
        return workReportService.listIssueOnHand(workOrderId, reportDate);
    }

    @Transactional(readOnly = true)
    public WorkReportConsumptionStatusView getConsumptionStatus(long workOrderId, BigDecimal pendingGoodQty) {
        return workReportService.getConsumptionStatus(workOrderId, pendingGoodQty);
    }

    @Transactional
    public WorkReportView register(CreateWorkReportCommand command, String actorUserId) {
        return workReportService.register(command, actorUserId);
    }

    @Transactional
    public void cancel(long id, String actorUserId) {
        workReportService.cancel(id, actorUserId);
    }
}
