package com.shindong.smartmanager.application.purchase;

import java.util.List;
import java.util.Optional;

public interface EtcPurchaseOrderRepository {

    List<EtcPurchaseOrderView> findActive(EtcPurchaseOrderListCriteria criteria);

    List<EtcPurchaseReceiptCandidateView> findReceiptCandidates(EtcPurchaseReceiptCandidateCriteria criteria);

    Optional<EtcPurchaseOrderView> findActiveById(long id);

    long countByOrderNoPrefix(String prefix);

    long save(CreateEtcPurchaseOrderCommand command, String orderNo, String actorUserId);

    void update(long id, UpdateEtcPurchaseOrderCommand command, String actorUserId);

    void softDelete(long id, String actorUserId);

    void updateRemainQtyAndStatus(long id, java.math.BigDecimal remainQty, String actorUserId);
}
