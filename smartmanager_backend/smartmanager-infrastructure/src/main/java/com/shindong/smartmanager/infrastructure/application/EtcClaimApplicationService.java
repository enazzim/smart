package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.purchase.EtcClaimCommand;
import com.shindong.smartmanager.application.purchase.EtcClaimListCriteria;
import com.shindong.smartmanager.application.purchase.EtcClaimService;
import com.shindong.smartmanager.application.purchase.EtcClaimView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class EtcClaimApplicationService {

    private final EtcClaimService etcClaimService;

    public EtcClaimApplicationService(EtcClaimService etcClaimService) {
        this.etcClaimService = etcClaimService;
    }

    @Transactional(readOnly = true)
    public List<EtcClaimView> list(EtcClaimListCriteria criteria) {
        return etcClaimService.list(criteria);
    }

    @Transactional(readOnly = true)
    public EtcClaimView get(long id) {
        return etcClaimService.get(id);
    }

    @Transactional
    public EtcClaimView register(EtcClaimCommand command, String actorUserId) {
        return etcClaimService.register(command, actorUserId);
    }

    @Transactional
    public EtcClaimView update(long id, EtcClaimCommand command, String actorUserId) {
        return etcClaimService.update(id, command, actorUserId);
    }

    @Transactional
    public void delete(long id, String actorUserId) {
        etcClaimService.delete(id, actorUserId);
    }
}
