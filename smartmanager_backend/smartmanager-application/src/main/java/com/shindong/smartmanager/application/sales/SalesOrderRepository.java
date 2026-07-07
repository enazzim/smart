package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesLineFulfillmentStatus;
import com.shindong.smartmanager.domain.sales.SalesOrderStatus;
import java.util.List;
import java.util.Optional;

public interface SalesOrderRepository {

    long countByOrderNoPrefix(String prefix);

    boolean existsActiveByOrderNo(String orderNo);

    long save(SalesOrderCommand command, String orderNo, String actorUserId);

    void replaceLines(long salesOrderId, SalesOrderCommand command, String actorUserId);

    void updateHeader(long salesOrderId, SalesOrderCommand command, String actorUserId);

    void updateStatus(long salesOrderId, SalesOrderStatus status, String actorUserId);

    void markConfirmed(long salesOrderId, String actorUserId);

    void revertToDraft(long salesOrderId, String actorUserId);

    List<SalesOrderView> findAllActive();

    Optional<SalesOrderView> findActiveById(long id);

    SalesOrderStatus findStatus(long salesOrderId);

    List<SalesOrderLineListView> findLineList(SalesOrderLineListCriteria criteria);

    void updateLineFulfillmentStatus(
            long lineId,
            SalesLineFulfillmentStatus status,
            String actorUserId
    );

    Optional<SalesOrderLineListView> findLineListItem(long lineId);
}
