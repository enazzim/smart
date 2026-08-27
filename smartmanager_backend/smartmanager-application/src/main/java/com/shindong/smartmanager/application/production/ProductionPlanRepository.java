package com.shindong.smartmanager.application.production;

import com.shindong.smartmanager.domain.production.ProductionPlanMrpStatus;
import com.shindong.smartmanager.domain.production.ProductionPlanWorkPlanStatus;
import java.util.List;
import java.util.Optional;
import java.util.Set;

public interface ProductionPlanRepository {

    long nextSequenceByPlanNoPrefix(String prefix);

    boolean existsActiveBySalesOrderLineId(long salesOrderLineId);

    boolean existsActiveBySalesOrderId(long salesOrderId);

    long save(ProductionPlanSaveCommand command, String planNo, String actorUserId);

    List<ProductionPlanView> findAllActive(ProductionPlanListCriteria criteria);

    Optional<ProductionPlanView> findActiveById(long id);

    Optional<ProductionPlanView> findActiveBySalesOrderLineId(long salesOrderLineId);

    Set<Long> findActiveSalesOrderLineIds();

    void deleteById(long id);

    void updateMrpStatus(long id, ProductionPlanMrpStatus mrpStatus, String actorUserId);

    void updateWorkPlanStatus(long id, ProductionPlanWorkPlanStatus workPlanStatus, String actorUserId);

    void addProducedQty(long id, java.math.BigDecimal qty, String actorUserId);

    void subtractProducedQty(long id, java.math.BigDecimal qty, String actorUserId);
}
