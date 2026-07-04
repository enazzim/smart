package com.shindong.smartmanager.infrastructure.persistence.workcenter;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataWorkCenterRepository extends JpaRepository<WorkCenterJpaEntity, Long> {

    List<WorkCenterJpaEntity> findByRecordingStateOrderByWcNameAsc(int recordingState);

    Optional<WorkCenterJpaEntity> findByIdAndRecordingState(Long id, int recordingState);

    boolean existsByWcNameAndRecordingState(String wcName, int recordingState);

    @Query("""
            SELECT CASE WHEN COUNT(w) > 0 THEN true ELSE false END
            FROM WorkCenterJpaEntity w
            WHERE w.wcName = :wcName
              AND w.recordingState = 1
              AND (:excludeId IS NULL OR w.id <> :excludeId)
            """)
    boolean existsActiveByWcName(@Param("wcName") String wcName, @Param("excludeId") Long excludeId);

    @Query("""
            SELECT w FROM WorkCenterJpaEntity w
            WHERE w.recordingState = 1
              AND (:query IS NULL OR :query = '' OR LOWER(w.wcName) LIKE LOWER(CONCAT('%', :query, '%')))
            ORDER BY w.wcName ASC
            """)
    List<WorkCenterJpaEntity> searchActive(@Param("query") String query);
}
