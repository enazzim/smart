package com.shindong.smartmanager.application.production;

import java.util.List;
import java.util.Optional;

public interface WorkOrderRepository {

    List<WorkOrderView> findOrderTargets();

    List<WorkOrderView> saveAll(List<WorkOrderSaveCommand> commands, String actorUserId);

    List<WorkOrderView> findAllActive(WorkOrderListCriteria criteria);

    Optional<WorkOrderView> findActiveIssuedById(long id);

    Optional<WorkOrderView> findActiveById(long id);

    long countByOrderNumPrefix(String prefix);

    void cancelById(long id, String actorUserId);

    void addReportedQty(long id, java.math.BigDecimal goodQty, String actorUserId);

    void subtractReportedQty(long id, java.math.BigDecimal goodQty, String actorUserId);

    void purgeInactiveByWorkPlanId(long workPlanId);
}
