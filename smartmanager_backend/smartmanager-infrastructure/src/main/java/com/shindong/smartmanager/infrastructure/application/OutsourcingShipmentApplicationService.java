package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.outsource.CreateOutsourcingShipmentCommand;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentInputPreviewView;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentCandidateView;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentListCriteria;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentService;
import com.shindong.smartmanager.application.outsource.OutsourcingShipmentView;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class OutsourcingShipmentApplicationService {

    private final OutsourcingShipmentService outsourcingShipmentService;

    public OutsourcingShipmentApplicationService(OutsourcingShipmentService outsourcingShipmentService) {
        this.outsourcingShipmentService = outsourcingShipmentService;
    }

    @Transactional(readOnly = true)
    public List<OutsourcingShipmentCandidateView> listCandidates() {
        return outsourcingShipmentService.listCandidates();
    }

    @Transactional(readOnly = true)
    public OutsourcingShipmentInputPreviewView previewInput(
            long orderLineId,
            BigDecimal shipmentQty,
            LocalDate shipmentDate
    ) {
        return outsourcingShipmentService.previewInput(orderLineId, shipmentQty, shipmentDate);
    }

    @Transactional(readOnly = true)
    public List<OutsourcingShipmentView> list(OutsourcingShipmentListCriteria criteria) {
        return outsourcingShipmentService.list(criteria);
    }

    @Transactional
    public OutsourcingShipmentView register(CreateOutsourcingShipmentCommand command, String actorUserId) {
        return outsourcingShipmentService.register(command, actorUserId);
    }

    @Transactional
    public void cancel(long id, String actorUserId) {
        outsourcingShipmentService.cancel(id, actorUserId);
    }
}
