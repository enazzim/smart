package com.shindong.smartmanager.infrastructure.persistence.workstandard;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataWorkStandardRepository extends JpaRepository<WorkStandardJpaEntity, Long> {

    Optional<WorkStandardJpaEntity> findByIdAndRecordingState(Long id, int recordingState);

    List<WorkStandardJpaEntity> findByItemIdAndRecordingStateOrderByProcessSequenceIdAscPriorityOrderAsc(
            Long itemId,
            int recordingState
    );

    List<WorkStandardJpaEntity> findByProcessSequenceIdAndRecordingState(
            Long processSequenceId,
            int recordingState
    );

    @Query("""
            SELECT ws FROM WorkStandardJpaEntity ws
            JOIN com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity i ON i.id = ws.itemId
            WHERE ws.recordingState = 1
              AND (:itemNum IS NULL OR :itemNum = '' OR LOWER(i.itemNo) LIKE LOWER(CONCAT('%', :itemNum, '%')))
            ORDER BY i.itemNo ASC, ws.processSequenceId ASC, ws.priorityOrder ASC
            """)
    List<WorkStandardJpaEntity> searchActive(@Param("itemNum") String itemNum);

    @Query("""
            SELECT CASE WHEN COUNT(ws) > 0 THEN true ELSE false END
            FROM WorkStandardJpaEntity ws
            WHERE ws.itemId = :itemId
              AND ws.processSequenceId = :processSequenceId
              AND ws.priorityOrder = :priorityOrder
              AND ws.recordingState = 1
              AND (:excludeId IS NULL OR ws.id <> :excludeId)
            """)
    boolean existsActiveUk(
            @Param("itemId") Long itemId,
            @Param("processSequenceId") Long processSequenceId,
            @Param("priorityOrder") int priorityOrder,
            @Param("excludeId") Long excludeId
    );
}
