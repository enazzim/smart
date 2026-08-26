package com.shindong.smartmanager.infrastructure.persistence.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourcingOrderStatus;
import java.util.Collection;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataOutsourcingOrderLineRepository extends JpaRepository<OutsourcingOrderLineJpaEntity, Long> {

    List<OutsourcingOrderLineJpaEntity> findByOutsourcingOrderIdAndRecordingStateOrderByLineNoAsc(
            long outsourcingOrderId,
            int recordingState
    );

    @Query("""
            SELECT l.workPlanId, COALESCE(SUM(l.orderQty), 0)
            FROM OutsourcingOrderLineJpaEntity l
            JOIN OutsourcingOrderJpaEntity o ON o.id = l.outsourcingOrderId
            WHERE l.recordingState = :active
              AND o.recordingState = :active
              AND o.status <> :cancelled
              AND l.workPlanId IN :workPlanIds
            GROUP BY l.workPlanId
            """)
    List<Object[]> sumOrderedQtyGroupedByWorkPlanId(
            @Param("workPlanIds") Collection<Long> workPlanIds,
            @Param("active") int active,
            @Param("cancelled") OutsourcingOrderStatus cancelled
    );

    @Query("""
            SELECT l.workPlanId, COALESCE(SUM(l.receivedQty), 0)
            FROM OutsourcingOrderLineJpaEntity l
            JOIN OutsourcingOrderJpaEntity o ON o.id = l.outsourcingOrderId
            WHERE l.recordingState = :active
              AND o.recordingState = :active
              AND o.status <> :cancelled
              AND l.workPlanId IN :workPlanIds
            GROUP BY l.workPlanId
            """)
    List<Object[]> sumReceivedQtyGroupedByWorkPlanId(
            @Param("workPlanIds") Collection<Long> workPlanIds,
            @Param("active") int active,
            @Param("cancelled") OutsourcingOrderStatus cancelled
    );

    java.util.Optional<OutsourcingOrderLineJpaEntity> findByIdAndRecordingState(long id, int recordingState);
}
