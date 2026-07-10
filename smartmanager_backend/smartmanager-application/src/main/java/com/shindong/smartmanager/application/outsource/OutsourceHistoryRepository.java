package com.shindong.smartmanager.application.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourceHistorySourceType;
import java.util.List;

public interface OutsourceHistoryRepository {

    long save(OutsourceHistoryCommand command);

    List<OutsourceHistoryRecord> findActiveBySource(OutsourceHistorySourceType sourceType, long sourceId);

    void deactivateBySource(OutsourceHistorySourceType sourceType, long sourceId, String actorUserId);
}
