package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourceHistorySourceType;

public interface OutsourceHistoryRepository {

    long save(OutsourceHistoryCommand command);

    void deactivateBySource(OutsourceHistorySourceType sourceType, long sourceId, String actorUserId);
}
