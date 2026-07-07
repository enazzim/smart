package com.shindong.smartmanager.application.sales;

import java.math.BigDecimal;
import java.util.List;
import java.util.Optional;

public interface SalesRevenueRepository {

    long countByRevenueNoPrefix(String prefix);

    List<SalesRevenueCandidateView> findCandidates(SalesRevenueCandidateCriteria criteria);

    SalesShipmentLineRevenueContext findShipmentLineContext(long salesShipmentLineId);

    SalesRevenueView save(SalesRevenueSaveCommand command, String actorUserId);

    void cancelById(long id, String actorUserId);

    List<SalesRevenueView> findAllActive(SalesRevenueListCriteria criteria);

    Optional<SalesRevenueView> findActiveIssuedById(long id);

    void addInvoicedQty(long salesShipmentLineId, BigDecimal qty, String actorUserId);

    void subtractInvoicedQty(long salesShipmentLineId, BigDecimal qty, String actorUserId);
}
