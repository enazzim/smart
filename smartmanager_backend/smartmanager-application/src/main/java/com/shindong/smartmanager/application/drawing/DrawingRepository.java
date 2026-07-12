package com.shindong.smartmanager.application.drawing;

import com.shindong.smartmanager.domain.drawing.DrawingType;
import java.time.Instant;
import java.util.List;
import java.util.Optional;

public interface DrawingRepository {

    boolean existsActiveByPartNo(String partNo);

    String saveMaster(String partNo, String partName, String modelGroup, Long itemId, String actorUserId);

    Optional<DrawingMasterView> findMasterById(String id);

    Optional<DrawingMasterView> findActiveMasterByPartNo(String partNo);

    void softDeleteMasterByPartNo(String partNo, String actorUserId);

    void restoreMaster(String id, String actorUserId);

    void updateMasterInfo(
            String id,
            String partNo,
            String partName,
            String modelGroup,
            Long itemId,
            String actorUserId
    );

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

    List<DrawingListView> findLatestActiveDrawings();

    List<DrawingListView> findLatestDeletedDrawings();

    List<DrawingHistoryView> findHistoriesByMasterId(String masterId);

    Optional<DrawingHistoryDetailView> findHistoryById(String historyId);

    Optional<DrawingHistoryDetailView> findLatestActiveHistoryByPartNo(String partNo);

    List<String> findFilePathsByMasterId(String masterId);

    void deleteAllHistoriesByMasterId(String masterId);
}
