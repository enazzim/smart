package com.shindong.smartmanager.infrastructure.persistence.drawing;

import java.util.Collection;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataDrawingReferenceRepository extends JpaRepository<DrawingReferenceJpaEntity, String> {

    @Query("""
            SELECT r FROM DrawingReferenceJpaEntity r
            JOIN FETCH r.childHistory ch
            JOIN FETCH ch.drawingMaster cm
            LEFT JOIN FETCH cm.item
            WHERE r.parentHistory.id = :parentHistoryId
              AND r.recordingState = 1
            ORDER BY r.sortOrder ASC, r.createdAt ASC
            """)
    List<DrawingReferenceJpaEntity> findActiveByParentHistoryId(@Param("parentHistoryId") String parentHistoryId);

    @Query("""
            SELECT r FROM DrawingReferenceJpaEntity r
            JOIN FETCH r.parentHistory ph
            JOIN FETCH ph.drawingMaster pm
            LEFT JOIN FETCH pm.item
            WHERE r.childHistory.id = :childHistoryId
              AND r.recordingState = 1
            ORDER BY r.sortOrder ASC, r.createdAt ASC
            """)
    List<DrawingReferenceJpaEntity> findActiveByChildHistoryId(@Param("childHistoryId") String childHistoryId);

    @Query("""
            SELECT r.childHistory.id FROM DrawingReferenceJpaEntity r
            WHERE r.parentHistory.id = :parentHistoryId
              AND r.recordingState = 1
            """)
    List<String> findActiveChildHistoryIds(@Param("parentHistoryId") String parentHistoryId);

    @Modifying(clearAutomatically = true)
    @Query("""
            DELETE FROM DrawingReferenceJpaEntity r
            WHERE r.parentHistory.id = :parentHistoryId
            """)
    void deleteAllByParentHistoryId(@Param("parentHistoryId") String parentHistoryId);

    @Modifying(clearAutomatically = true)
    @Query("""
            DELETE FROM DrawingReferenceJpaEntity r
            WHERE r.parentHistory.id IN :historyIds
               OR r.childHistory.id IN :historyIds
            """)
    void deleteByHistoryIds(@Param("historyIds") Collection<String> historyIds);
}
