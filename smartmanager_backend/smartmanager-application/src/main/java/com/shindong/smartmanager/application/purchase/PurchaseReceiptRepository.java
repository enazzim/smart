package com.shindong.smartmanager.application.purchase;

import java.math.BigDecimal;
import java.util.List;
import java.util.Optional;

public interface PurchaseReceiptRepository {

    List<PurchaseReceiptCandidateView> findReceiptCandidates(PurchaseReceiptCandidateCriteria criteria);

    long countByReceiptNoPrefix(String prefix);

    long saveReceipt(PurchaseReceiptSaveCommand command, String actorUserId);

    Optional<PurchaseReceiptView> findActiveById(long id);

    List<PurchaseReceiptView> findAllActive();

    List<PurchaseReceiptView> findAllActive(PurchaseReceiptListCriteria criteria);

    void cancelReceipt(long receiptId, String actorUserId);

    PurchaseOrderLineReceiptContext findOrderLineContext(long purchaseOrderLineId);

    void addReceivedQty(long purchaseOrderLineId, BigDecimal qty, String actorUserId);

    void addWaitingInspectionQty(long purchaseOrderLineId, BigDecimal qty, String actorUserId);

    void releaseWaitingInspectionQty(long purchaseOrderLineId, BigDecimal qty, String actorUserId);

    void updateReceiptLinePostedQty(long receiptLineId, BigDecimal postedQty, String actorUserId);

    void updateReceiptStatus(long receiptId, String actorUserId);

    void subtractReceivedQty(long purchaseOrderLineId, BigDecimal qty, String actorUserId);

    void refreshPurchaseOrderReceiptStatus(long purchaseOrderId, String actorUserId);
}
