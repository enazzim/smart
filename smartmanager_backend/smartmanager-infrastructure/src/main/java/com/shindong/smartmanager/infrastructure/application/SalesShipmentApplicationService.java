package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.sales.CreateSalesShipmentCommand;
import com.shindong.smartmanager.application.sales.SalesShipmentCandidateCriteria;
import com.shindong.smartmanager.application.sales.SalesShipmentCandidateView;
import com.shindong.smartmanager.application.sales.SalesShipmentListCriteria;
import com.shindong.smartmanager.application.sales.SalesShipmentService;
import com.shindong.smartmanager.application.sales.SalesShipmentView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class SalesShipmentApplicationService {

    private final SalesShipmentService salesShipmentService;

    public SalesShipmentApplicationService(SalesShipmentService salesShipmentService) {
        this.salesShipmentService = salesShipmentService;
    }

    @Transactional(readOnly = true)
    public List<SalesShipmentCandidateView> listCandidates(SalesShipmentCandidateCriteria criteria) {
        return salesShipmentService.listCandidates(criteria);
    }

    @Transactional(readOnly = true)
    public List<SalesShipmentView> list(SalesShipmentListCriteria criteria) {
        return salesShipmentService.list(criteria);
    }

    @Transactional
    public SalesShipmentView register(CreateSalesShipmentCommand command, String actorUserId) {
        return salesShipmentService.register(command, actorUserId);
    }

    @Transactional
    public void cancel(long id, String actorUserId) {
        salesShipmentService.cancel(id, actorUserId);
    }
}
