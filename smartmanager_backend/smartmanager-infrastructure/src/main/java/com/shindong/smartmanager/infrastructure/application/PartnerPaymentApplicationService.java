package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.purchase.CreatePartnerPaymentCommand;
import com.shindong.smartmanager.application.purchase.PartnerPaymentCandidateCriteria;
import com.shindong.smartmanager.application.purchase.PartnerPaymentCandidateView;
import com.shindong.smartmanager.application.purchase.PartnerPaymentListCriteria;
import com.shindong.smartmanager.application.purchase.PartnerPaymentService;
import com.shindong.smartmanager.application.purchase.PartnerPaymentView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class PartnerPaymentApplicationService {

    private final PartnerPaymentService partnerPaymentService;

    public PartnerPaymentApplicationService(PartnerPaymentService partnerPaymentService) {
        this.partnerPaymentService = partnerPaymentService;
    }

    @Transactional(readOnly = true)
    public List<PartnerPaymentCandidateView> listCandidates(PartnerPaymentCandidateCriteria criteria) {
        return partnerPaymentService.listCandidates(criteria);
    }

    @Transactional(readOnly = true)
    public List<PartnerPaymentView> list(PartnerPaymentListCriteria criteria) {
        return partnerPaymentService.list(criteria);
    }

    @Transactional
    public PartnerPaymentView register(CreatePartnerPaymentCommand command, String actorUserId) {
        return partnerPaymentService.register(command, actorUserId);
    }

    @Transactional
    public void cancel(long id, String actorUserId) {
        partnerPaymentService.cancel(id, actorUserId);
    }
}
