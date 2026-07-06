package com.shindong.smartmanager.infrastructure.persistence.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourceHistorySourceType;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataOutsourceHistoryRepository extends JpaRepository<OutsourceHistoryJpaEntity, Long> {

    List<OutsourceHistoryJpaEntity> findBySourceTypeAndSourceIdAndRecordingState(
            OutsourceHistorySourceType sourceType,
            long sourceId,
            int recordingState
    );
}
