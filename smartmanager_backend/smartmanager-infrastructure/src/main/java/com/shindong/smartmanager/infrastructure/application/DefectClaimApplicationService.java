package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.purchase.DefectClaimCommand;
import com.shindong.smartmanager.application.purchase.DefectClaimListCriteria;
import com.shindong.smartmanager.application.purchase.DefectClaimService;
import com.shindong.smartmanager.application.purchase.DefectClaimView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class DefectClaimApplicationService {

    private final DefectClaimService defectClaimService;

    public DefectClaimApplicationService(DefectClaimService defectClaimService) {
        this.defectClaimService = defectClaimService;
    }

    @Transactional(readOnly = true)
    public List<DefectClaimView> list(DefectClaimListCriteria criteria) {
        return defectClaimService.list(criteria);
    }

    @Transactional(readOnly = true)
    public DefectClaimView get(long id) {
        return defectClaimService.get(id);
    }

    @Transactional
    public DefectClaimView register(DefectClaimCommand command, String actorUserId) {
        return defectClaimService.register(command, actorUserId);
    }

    @Transactional
    public DefectClaimView update(long id, DefectClaimCommand command, String actorUserId) {
        return defectClaimService.update(id, command, actorUserId);
    }

    @Transactional
    public void delete(long id, String actorUserId) {
        defectClaimService.delete(id, actorUserId);
    }
}
