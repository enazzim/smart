package com.shindong.smartmanager.infrastructure.persistence.production;

import com.shindong.smartmanager.domain.production.WorkOrderStatus;
import java.math.BigDecimal;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataWorkOrderRepository extends JpaRepository<WorkOrderJpaEntity, Long> {

    Optional<WorkOrderJpaEntity> findByIdAndRecordingState(long id, int recordingState);

    Optional<WorkOrderJpaEntity> findByIdAndRecordingStateAndStatus(
            long id,
            int recordingState,
            WorkOrderStatus status
    );

    @Query("""
            SELECT COALESCE(SUM(w.reportedQty), 0)
            FROM WorkOrderJpaEntity w
            WHERE w.workPlanId = :workPlanId
              AND w.recordingState = :active
            """)
    BigDecimal sumReportedQtyByWorkPlanId(
            @Param("workPlanId") long workPlanId,
            @Param("active") int active
    );

    boolean existsByWorkPlanIdAndRecordingStateAndStatus(
            long workPlanId,
            int recordingState,
            WorkOrderStatus status
    );

    long countByOrderNumStartingWithAndRecordingState(String prefix, int recordingState);

    @Modifying
    @Query("""
            DELETE FROM WorkOrderJpaEntity w
            WHERE w.workPlanId = :workPlanId
              AND w.recordingState = :inactive
            """)
    void deleteInactiveByWorkPlanId(
            @Param("workPlanId") long workPlanId,
            @Param("inactive") int inactive
    );
}
