package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PurchaseHistorySourceType;

public interface PurchaseHistoryRepository {

    long save(PurchaseHistoryCommand command);

    void deactivateBySource(PurchaseHistorySourceType sourceType, long sourceId, String actorUserId);
}
