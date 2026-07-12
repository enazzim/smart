package com.shindong.smartmanager.application.drawing;

import java.util.Collection;
import java.util.List;

public interface DrawingReferenceRepository {

    List<DrawingReferenceView> findActiveByParentHistoryId(String parentHistoryId);

    List<DrawingWhereUsedView> findActiveByChildHistoryId(String childHistoryId);

    List<String> findActiveChildHistoryIds(String parentHistoryId);

    /** latest 구성 교체용 — 해당 parent history의 참조 행을 모두 제거한다. */
    void deleteAllByParentHistoryId(String parentHistoryId);

    void insertReference(
            String parentHistoryId,
            String childHistoryId,
            String refRole,
            int sortOrder,
            String remark,
            String actorUserId
    );

    void copyActiveReferences(String fromParentHistoryId, String toParentHistoryId, String actorUserId);

    void deleteByHistoryIds(Collection<String> historyIds);
}
