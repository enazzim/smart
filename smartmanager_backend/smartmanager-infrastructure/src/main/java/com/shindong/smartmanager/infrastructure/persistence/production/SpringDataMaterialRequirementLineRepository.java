package com.shindong.smartmanager.infrastructure.persistence.production;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataMaterialRequirementLineRepository extends JpaRepository<MaterialRequirementLineJpaEntity, Long> {

    Optional<MaterialRequirementLineJpaEntity> findByIdAndRecordingState(long id, int recordingState);

    List<MaterialRequirementLineJpaEntity> findByMrpRunIdAndRecordingStateOrderByIdAsc(long mrpRunId, int recordingState);

    List<MaterialRequirementLineJpaEntity> findByProductionPlanIdAndRecordingStateOrderByIdAsc(
            long productionPlanId,
            int recordingState
    );

    List<MaterialRequirementLineJpaEntity> findByRecordingStateOrderByIdDesc(int recordingState);

    long countByProductionPlanIdAndRecordingState(long productionPlanId, int recordingState);

    long countByMrpRunIdAndRecordingState(long mrpRunId, int recordingState);
}
