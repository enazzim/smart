package com.shindong.smartmanager.infrastructure.persistence.production;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataProductionPlanRepository extends JpaRepository<ProductionPlanJpaEntity, Long> {

    long countByPlanNoStartingWithAndRecordingState(String prefix, int recordingState);

    List<ProductionPlanJpaEntity> findByPlanNoStartingWithAndRecordingState(String prefix, int recordingState);

    boolean existsBySalesOrderLineIdAndRecordingState(long salesOrderLineId, int recordingState);

    List<ProductionPlanJpaEntity> findByRecordingStateOrderByIdDesc(int recordingState);

    List<ProductionPlanJpaEntity> findBySalesOrderLineIdAndRecordingState(long salesOrderLineId, int recordingState);

    Optional<ProductionPlanJpaEntity> findByIdAndRecordingState(long id, int recordingState);
}
