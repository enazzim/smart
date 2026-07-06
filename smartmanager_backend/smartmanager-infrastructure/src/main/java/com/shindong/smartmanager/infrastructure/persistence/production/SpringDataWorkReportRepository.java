package com.shindong.smartmanager.infrastructure.persistence.production;

import com.shindong.smartmanager.domain.production.WorkReportStatus;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataWorkReportRepository extends JpaRepository<WorkReportJpaEntity, Long> {

    Optional<WorkReportJpaEntity> findByIdAndRecordingStateAndStatus(
            long id,
            int recordingState,
            WorkReportStatus status
    );

    long countByReportNumStartingWithAndRecordingState(String prefix, int recordingState);
}
