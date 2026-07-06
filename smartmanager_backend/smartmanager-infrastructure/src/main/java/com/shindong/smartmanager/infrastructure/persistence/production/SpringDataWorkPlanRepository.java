package com.shindong.smartmanager.infrastructure.persistence.production;

import com.shindong.smartmanager.domain.process.WorkDistinction;
import com.shindong.smartmanager.domain.production.WorkPlanStatus;
import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataWorkPlanRepository extends JpaRepository<WorkPlanJpaEntity, Long> {

    List<WorkPlanJpaEntity> findByRecordingStateOrderByPlanStartDateDescIdDesc(int recordingState);

    Optional<WorkPlanJpaEntity> findByIdAndRecordingStateAndStatus(
            long id,
            int recordingState,
            WorkPlanStatus status
    );

    List<WorkPlanJpaEntity> findByProductionPlanIdAndRecordingStateAndStatusOrderByProcessSequenceIdAscIdAsc(
            long productionPlanId,
            int recordingState,
            WorkPlanStatus status
    );

    long countByProductionPlanIdAndRecordingStateAndStatus(
            long productionPlanId,
            int recordingState,
            WorkPlanStatus status
    );

    @Modifying
    @Query("""
            DELETE FROM WorkPlanJpaEntity w
            WHERE w.productionPlanId = :productionPlanId
              AND w.processSequenceId = :processSequenceId
              AND w.workDistinction = :workDistinction
              AND w.recordingState = :inactive
            """)
    void deleteInactiveByKey(
            @Param("productionPlanId") long productionPlanId,
            @Param("processSequenceId") long processSequenceId,
            @Param("workDistinction") WorkDistinction workDistinction,
            @Param("inactive") int inactive
    );
}
