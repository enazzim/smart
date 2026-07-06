package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.application.outsource.OutsourcingOrderView;
import com.shindong.smartmanager.domain.outsource.OutsourcingOrderStatus;
import java.math.BigDecimal;
import java.util.Collection;
import java.util.List;
import java.util.Map;
import java.util.Optional;

public interface OutsourcingOrderRepository {

    long save(OutsourcingOrderCommand command, String orderNo, String actorUserId);

    void updateStatus(long outsourcingOrderId, OutsourcingOrderStatus status, String actorUserId);

    List<OutsourcingOrderView> findAllActive();

    List<OutsourcingOrderView> findAllActive(OutsourcingOrderListCriteria criteria);

    Optional<OutsourcingOrderView> findActiveById(long id);

    long countByOrderNoPrefix(String prefix);

    boolean existsActiveByOrderNo(String orderNo);

    Map<Long, BigDecimal> sumOrderedQtyByWorkPlanIds(Collection<Long> workPlanIds);

    Optional<OutsourcingOrderLineView> findActiveLineById(long orderLineId);

    Optional<OutsourcingOrderView> findActiveByOrderLineId(long orderLineId);

    void addShippedQty(long orderLineId, BigDecimal qty, String actorUserId);

    void subtractShippedQty(long orderLineId, BigDecimal qty, String actorUserId);

    void refreshOrderStatus(long orderId, String actorUserId);

    void addReceivedQty(long orderLineId, BigDecimal qty, String actorUserId);

    void subtractReceivedQty(long orderLineId, BigDecimal qty, String actorUserId);

    void addWaitingInspectionQty(long orderLineId, BigDecimal qty, String actorUserId);

    void releaseWaitingInspectionQty(long orderLineId, BigDecimal qty, String actorUserId);
}
