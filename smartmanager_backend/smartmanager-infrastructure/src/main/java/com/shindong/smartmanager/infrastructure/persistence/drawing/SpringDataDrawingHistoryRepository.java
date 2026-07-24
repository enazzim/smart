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
            LEFT JOIN FETCH m.sourcePartner
            WHERE h.isLatest = 'Y' AND m.recordingState = 1
            ORDER BY m.createdAt DESC
            """)
    List<DrawingHistoryJpaEntity> findLatestActiveHistories();

    @Query("""
            SELECT h FROM DrawingHistoryJpaEntity h
            JOIN FETCH h.drawingMaster m
            LEFT JOIN FETCH m.item
            LEFT JOIN FETCH m.sourcePartner
            WHERE h.isLatest = 'Y' AND m.recordingState = 0
            ORDER BY m.updatedAt DESC
            """)
    List<DrawingHistoryJpaEntity> findLatestDeletedHistories();

    @Query("""
            SELECT h FROM DrawingHistoryJpaEntity h
            JOIN FETCH h.drawingMaster m
            LEFT JOIN FETCH m.item
            LEFT JOIN FETCH m.sourcePartner
            WHERE h.id = :historyId
            """)
    Optional<DrawingHistoryJpaEntity> findByIdWithMaster(@Param("historyId") String historyId);

    @Query("""
            SELECT h FROM DrawingHistoryJpaEntity h
            JOIN FETCH h.drawingMaster m
            LEFT JOIN FETCH m.item
            LEFT JOIN FETCH m.sourcePartner
            WHERE m.partNo = :partNo AND m.recordingState = 1 AND h.isLatest = 'Y'
            """)
    Optional<DrawingHistoryJpaEntity> findLatestActiveByPartNo(@Param("partNo") String partNo);

    void deleteByDrawingMasterId(String drawingMasterId);

    @Query("""
            SELECT COALESCE(MAX(h.majorVersion), 0) FROM DrawingHistoryJpaEntity h
            WHERE h.drawingMaster.id = :masterId AND h.drawingType = com.shindong.smartmanager.domain.drawing.DrawingType.PROD
            """)
    int findMaxProdMajorByMasterId(@Param("masterId") String masterId);

    @Query("""
            SELECT DISTINCT h.drawingMaster.id FROM DrawingHistoryJpaEntity h
            JOIN h.drawingMaster m
            WHERE m.recordingState = 1
              AND (
                LOWER(h.changeReason) LIKE LOWER(CONCAT('%', :query, '%'))
                OR LOWER(h.changeType) LIKE LOWER(CONCAT('%', :query, '%'))
                OR CONCAT(h.majorVersion, '.', h.minorVersion) LIKE CONCAT('%', :query, '%')
                OR CONCAT('V', h.majorVersion, '.', h.minorVersion) LIKE CONCAT('%', :query, '%')
              )
            """)
    List<String> findActiveMasterIdsMatchingHistoryQuery(@Param("query") String query);
}
