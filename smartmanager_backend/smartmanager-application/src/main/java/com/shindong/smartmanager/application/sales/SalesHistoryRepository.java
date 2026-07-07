package com.shindong.smartmanager.application.sales;

import com.shindong.smartmanager.domain.sales.SalesHistorySourceType;

public interface SalesHistoryRepository {

    long save(SalesHistoryCommand command);

    void deactivateBySource(SalesHistorySourceType sourceType, long sourceId, String actorUserId);
}
