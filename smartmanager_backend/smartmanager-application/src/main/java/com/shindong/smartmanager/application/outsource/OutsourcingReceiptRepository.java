package com.shindong.smartmanager.application.outsource;

import java.math.BigDecimal;
import java.util.List;
import java.util.Optional;

public interface OutsourcingReceiptRepository {

    List<OutsourcingReceiptCandidateView> findReceiptCandidates(OutsourcingReceiptCandidateCriteria criteria);

    long countByReceiptNoPrefix(String prefix);

    long saveReceipt(OutsourcingReceiptSaveCommand command, String actorUserId);

    Optional<OutsourcingReceiptView> findActiveById(long id);

    List<OutsourcingReceiptView> findAllActive(OutsourcingReceiptListCriteria criteria);

    void cancelReceipt(long receiptId, String actorUserId);

    OutsourcingOrderLineReceiptContext findOrderLineContext(long outsourcingOrderLineId);

    void addReceivedQty(long orderLineId, BigDecimal qty, String actorUserId);

    void addWaitingInspectionQty(long orderLineId, BigDecimal qty, String actorUserId);

    void releaseWaitingInspectionQty(long orderLineId, BigDecimal qty, String actorUserId);

    void updateReceiptLinePostedQty(long receiptLineId, BigDecimal postedQty, String actorUserId);

    void updateReceiptStatus(long receiptId, String actorUserId);

    void subtractReceivedQty(long orderLineId, BigDecimal qty, String actorUserId);
}
