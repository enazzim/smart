package com.shindong.smartmanager.infrastructure.persistence.production;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataMaterialIssueLineRepository extends JpaRepository<MaterialIssueLineJpaEntity, Long> {

    List<MaterialIssueLineJpaEntity> findByMaterialIssueIdAndRecordingStateOrderByLineNoAsc(
            long materialIssueId,
            int recordingState
    );

    @Query("""
            SELECT mil.itemCompositionId, SUM(mil.issueQty)
            FROM MaterialIssueLineJpaEntity mil
            JOIN MaterialIssueJpaEntity mi ON mi.id = mil.materialIssueId
            WHERE mi.workOrderId = :workOrderId
              AND mi.recordingState = 1
              AND mi.status = com.shindong.smartmanager.domain.production.MaterialIssueStatus.ISSUED
              AND mil.recordingState = 1
              AND mil.itemCompositionId IS NOT NULL
            GROUP BY mil.itemCompositionId
            """)
    List<Object[]> sumIssuedQtyByWorkOrderId(@Param("workOrderId") long workOrderId);
}
