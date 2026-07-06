package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.production.CreateMaterialIssueCommand;
import com.shindong.smartmanager.application.production.MaterialIssueConsumptionPreviewView;
import com.shindong.smartmanager.application.production.MaterialIssueListCriteria;
import com.shindong.smartmanager.application.production.MaterialIssueOnHandView;
import com.shindong.smartmanager.application.production.MaterialIssueService;
import com.shindong.smartmanager.application.production.MaterialIssueView;
import com.shindong.smartmanager.application.production.WorkOrderView;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class MaterialIssueApplicationService {

    private final MaterialIssueService materialIssueService;

    public MaterialIssueApplicationService(MaterialIssueService materialIssueService) {
        this.materialIssueService = materialIssueService;
    }

    @Transactional(readOnly = true)
    public List<WorkOrderView> listIssueTargets() {
        return materialIssueService.listIssueTargets();
    }

    @Transactional(readOnly = true)
    public List<MaterialIssueView> list(MaterialIssueListCriteria criteria) {
        return materialIssueService.list(criteria);
    }

    @Transactional(readOnly = true)
    public MaterialIssueConsumptionPreviewView getConsumptionPreview(long workOrderId, BigDecimal goodQty) {
        return materialIssueService.getConsumptionPreview(workOrderId, goodQty);
    }

    @Transactional(readOnly = true)
    public List<MaterialIssueOnHandView> listOnHand(long workOrderId, LocalDate issueDate) {
        return materialIssueService.listOnHand(workOrderId, issueDate);
    }

    @Transactional
    public MaterialIssueView register(CreateMaterialIssueCommand command, String actorUserId) {
        return materialIssueService.register(command, actorUserId);
    }

    @Transactional
    public void cancel(long id, String actorUserId) {
        materialIssueService.cancel(id, actorUserId);
    }
}
