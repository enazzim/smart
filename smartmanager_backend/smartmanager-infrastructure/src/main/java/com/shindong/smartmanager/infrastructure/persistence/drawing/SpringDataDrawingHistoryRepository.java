package com.shindong.smartmanager.infrastructure.persistence.drawing;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataDrawingHistoryRepository extends JpaRepository<DrawingHistoryJpaEntity, String> {

    List<DrawingHistoryJpaEntity> findByDrawingMasterIdOrderByCreatedAtDesc(String drawingMasterId);

    @Query("""
            SELECT h FROM DrawingHistoryJpaEntity h
            JOIN FETCH h.drawingMaster m
            LEFT JOIN FETCH m.item
            WHERE h.isLatest = 'Y' AND m.recordingState = 1
            ORDER BY m.createdAt DESC
            """)
    List<DrawingHistoryJpaEntity> findLatestActiveHistories();

    @Query("""
            SELECT h FROM DrawingHistoryJpaEntity h
            JOIN FETCH h.drawingMaster m
            LEFT JOIN FETCH m.item
            WHERE h.isLatest = 'Y' AND m.recordingState = 0
            ORDER BY m.updatedAt DESC
            """)
    List<DrawingHistoryJpaEntity> findLatestDeletedHistories();

    @Query("""
            SELECT h FROM DrawingHistoryJpaEntity h
            JOIN FETCH h.drawingMaster m
            LEFT JOIN FETCH m.item
            WHERE h.id = :historyId
            """)
    Optional<DrawingHistoryJpaEntity> findByIdWithMaster(@Param("historyId") String historyId);

    @Query("""
            SELECT h FROM DrawingHistoryJpaEntity h
            JOIN FETCH h.drawingMaster m
            LEFT JOIN FETCH m.item
            WHERE m.partNo = :partNo AND m.recordingState = 1 AND h.isLatest = 'Y'
            """)
    Optional<DrawingHistoryJpaEntity> findLatestActiveByPartNo(@Param("partNo") String partNo);

    void deleteByDrawingMasterId(String drawingMasterId);
}
