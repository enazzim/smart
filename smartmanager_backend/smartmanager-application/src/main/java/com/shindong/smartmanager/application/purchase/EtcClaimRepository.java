package com.shindong.smartmanager.application.purchase;

import java.util.List;
import java.util.Optional;

public interface EtcClaimRepository {

    long save(EtcClaimCommand command, int fiscalYear, int fiscalMonth, String actorUserId);

    void update(long id, EtcClaimCommand command, int fiscalYear, int fiscalMonth, String actorUserId);

    void softDelete(long id, String actorUserId);

    Optional<EtcClaimView> findActiveById(long id);

    List<EtcClaimView> findActive(EtcClaimListCriteria criteria);
}
