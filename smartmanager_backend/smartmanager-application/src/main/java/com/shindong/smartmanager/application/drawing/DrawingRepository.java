package com.shindong.smartmanager.application.drawing;

import com.shindong.smartmanager.domain.drawing.DrawingLifecycleStage;
import com.shindong.smartmanager.domain.drawing.DrawingType;
import java.util.List;
import java.util.Optional;
import java.util.Set;

public interface DrawingRepository {

    boolean existsActiveByPartNo(String partNo);

    String saveMaster(
            String partNo,
            String partName,
            String modelType,
            Long sourcePartnerId,
            DrawingLifecycleStage lifecycleStage,
            String actorUserId
    );

    Optional<DrawingMasterView> findMasterById(String id);

    Optional<DrawingMasterView> findActiveMasterByPartNo(String partNo);

    void softDeleteMasterByPartNo(String partNo, String actorUserId);

    void restoreMaster(String id, String actorUserId);

    void updateMasterInfo(
            String id,
            String partNo,
            String partName,
            String modelType,
            String actorUserId
    );

    void updateLifecycleStage(String id, DrawingLifecycleStage lifecycleStage, String actorUserId);

    void linkItem(String id, long itemId, String actorUserId);

    void hardDeleteMaster(String id);

    String saveHistory(
            String masterId,
            DrawingType drawingType,
            int majorVersion,
            int minorVersion,
            String filePath,
            long fileSizeBytes,
            String changeType,
            String changeReason
    );

    void markHistoryAsOld(String historyId);

    int findMaxProdMajorVersion(String masterId);

    List<DrawingListView> findLatestActiveDrawings();

    List<DrawingListView> findLatestDeletedDrawings();

    Set<String> findActiveMasterIdsMatchingHistoryQuery(String query);

    List<DrawingHistoryView> findHistoriesByMasterId(String masterId);

    Optional<DrawingHistoryDetailView> findHistoryById(String historyId);

    Optional<DrawingHistoryDetailView> findLatestActiveHistoryByPartNo(String partNo);

    List<String> findFilePathsByMasterId(String masterId);

    void deleteAllHistoriesByMasterId(String masterId);
}
