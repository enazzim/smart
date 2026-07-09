package com.shindong.smartmanager.application.purchase;

import java.util.List;
import java.util.Optional;

public interface EtcPurchaseReceiptRepository {

    List<EtcPurchaseReceiptView> findActive(EtcPurchaseReceiptListCriteria criteria);

    Optional<EtcPurchaseReceiptView> findActiveById(long id);

    long countByReceiptNoPrefix(String prefix);

    long save(EtcPurchaseReceiptSaveCommand command, String receiptNo, String actorUserId);

    void update(long id, EtcPurchaseReceiptSaveCommand command, String actorUserId);

    void softDelete(long id, String actorUserId);
}
