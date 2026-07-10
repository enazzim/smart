package com.shindong.smartmanager.infrastructure.persistence.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourceHistorySourceType;
import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataOutsourceHistoryRepository extends JpaRepository<OutsourceHistoryJpaEntity, Long> {

    List<OutsourceHistoryJpaEntity> findBySourceTypeAndSourceIdAndRecordingState(
            OutsourceHistorySourceType sourceType,
            long sourceId,
            int recordingState
    );

    Optional<OutsourceHistoryJpaEntity> findByIdAndRecordingState(long id, int recordingState);
}
