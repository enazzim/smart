package com.shindong.smartmanager.infrastructure.persistence.production;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataWorkReportConsumptionLineRepository
        extends JpaRepository<WorkReportConsumptionLineJpaEntity, Long> {

    List<WorkReportConsumptionLineJpaEntity> findByWorkReportIdAndRecordingStateOrderByLineNoAsc(
            long workReportId,
            int recordingState
    );
}
