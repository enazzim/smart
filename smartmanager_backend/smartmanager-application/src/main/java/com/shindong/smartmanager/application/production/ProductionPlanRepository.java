package com.shindong.smartmanager.application.production;

import java.util.List;
import java.util.Optional;
import java.util.Set;

public interface ProductionPlanRepository {

    long nextSequenceByPlanNoPrefix(String prefix);

    boolean existsActiveBySalesOrderLineId(long salesOrderLineId);

    long save(ProductionPlanSaveCommand command, String planNo, String actorUserId);

    List<ProductionPlanView> findAllActive(ProductionPlanListCriteria criteria);

    Optional<ProductionPlanView> findActiveById(long id);

    Set<Long> findActiveSalesOrderLineIds();

    void deleteById(long id);
}
