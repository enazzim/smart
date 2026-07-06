package com.shindong.smartmanager.infrastructure.persistence.production;

import com.shindong.smartmanager.domain.production.WorkReportHistorySourceType;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataWorkReportHistoryRepository extends JpaRepository<WorkReportHistoryJpaEntity, Long> {

    List<WorkReportHistoryJpaEntity> findBySourceTypeAndSourceIdAndRecordingState(
            WorkReportHistorySourceType sourceType,
            long sourceId,
            int recordingState
    );
}
