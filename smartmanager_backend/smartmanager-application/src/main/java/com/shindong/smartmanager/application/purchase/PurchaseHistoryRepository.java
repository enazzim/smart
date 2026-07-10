package com.shindong.smartmanager.application.purchase;

import com.shindong.smartmanager.domain.purchase.PurchaseHistorySourceType;
import java.util.List;

public interface PurchaseHistoryRepository {

    long save(PurchaseHistoryCommand command);

    List<PurchaseHistoryRecord> findActiveBySource(PurchaseHistorySourceType sourceType, long sourceId);

    void deactivateBySource(PurchaseHistorySourceType sourceType, long sourceId, String actorUserId);
}
