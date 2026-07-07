package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;
import java.util.List;
import java.util.Optional;

public interface SalesShipmentRepository {

    long countByShipmentNoPrefix(String prefix);

    List<SalesShipmentCandidateView> findCandidates(SalesShipmentCandidateCriteria criteria);

    SalesOrderLineShipmentContext findOrderLineContext(long salesOrderLineId);

    SalesShipmentView save(SalesShipmentSaveCommand command, String actorUserId);

    void cancelById(long id, String actorUserId);

    List<SalesShipmentView> findAllActive(SalesShipmentListCriteria criteria);

    Optional<SalesShipmentView> findActiveIssuedById(long id);

    void addShippedQty(long salesOrderLineId, BigDecimal qty, String actorUserId);

    void subtractShippedQty(long salesOrderLineId, BigDecimal qty, String actorUserId);

    void refreshLineDeliveryStatus(long salesOrderLineId, String actorUserId);
}
