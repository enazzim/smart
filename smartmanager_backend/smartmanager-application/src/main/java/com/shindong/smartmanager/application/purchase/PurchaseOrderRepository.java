package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PurchaseOrderStatus;
import java.math.BigDecimal;
import java.util.Collection;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.Set;

public interface PurchaseOrderRepository {

    boolean existsActiveOrderReferencingRequirementLine(long requirementLineId);

    boolean existsActiveOrderReferencingAnyRequirementLines(Collection<Long> requirementLineIds);

    Set<Long> findReferencedRequirementLineIds(Collection<Long> requirementLineIds);

    BigDecimal sumOrderedQtyByRequirementLineId(long requirementLineId);

    Map<Long, BigDecimal> sumOrderedQtyByRequirementLineIds(Collection<Long> requirementLineIds);

    long countByOrderNoPrefix(String prefix);

    boolean existsActiveByOrderNo(String orderNo);

    long save(PurchaseOrderCommand command, String orderNo, String actorUserId);

    void updateHeader(long purchaseOrderId, PurchaseOrderCommand command, String actorUserId);

    void replaceLines(long purchaseOrderId, PurchaseOrderCommand command, String actorUserId);

    void updateStatus(long purchaseOrderId, PurchaseOrderStatus status, String actorUserId);

    void clearRequirementLineReferences(long purchaseOrderId, String actorUserId);

    void markConfirmed(long purchaseOrderId, String actorUserId);

    List<PurchaseOrderView> findAllActive();

    List<PurchaseOrderView> findAllActive(PurchaseOrderListCriteria criteria);

    Optional<PurchaseOrderView> findActiveById(long id);

    PurchaseOrderStatus findStatus(long purchaseOrderId);

    boolean hasReceiptProgress(long purchaseOrderId);
}
