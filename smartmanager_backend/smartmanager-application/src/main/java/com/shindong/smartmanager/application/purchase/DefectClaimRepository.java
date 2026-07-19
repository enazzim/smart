package com.shindong.smartmanager.application.purchase;

import java.util.List;
import java.util.Optional;

public interface DefectClaimRepository {

    long save(DefectClaimCommand command, int fiscalYear, int fiscalMonth, String actorUserId);

    void update(long id, DefectClaimCommand command, int fiscalYear, int fiscalMonth, String actorUserId);

    void softDelete(long id, String actorUserId);

    Optional<DefectClaimView> findActiveById(long id);

    List<DefectClaimView> findActive(DefectClaimListCriteria criteria);
}
