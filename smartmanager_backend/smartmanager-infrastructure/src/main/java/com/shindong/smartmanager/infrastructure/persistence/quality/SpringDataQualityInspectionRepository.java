package com.shindong.smartmanager.infrastructure.persistence.quality;

import com.shindong.smartmanager.domain.quality.QualityInspectionSourceType;
import com.shindong.smartmanager.domain.quality.QualityInspectionStatus;
import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataQualityInspectionRepository extends JpaRepository<QualityInspectionJpaEntity, Long> {

    List<QualityInspectionJpaEntity> findByStatusAndSourceTypeAndRecordingStateOrderByCreatedAtDesc(
            QualityInspectionStatus status,
            QualityInspectionSourceType sourceType,
            int recordingState
    );

    Optional<QualityInspectionJpaEntity> findByIdAndRecordingState(long id, int recordingState);

    Optional<QualityInspectionJpaEntity> findBySourceReceiptLineIdAndStatusAndRecordingState(
            long sourceReceiptLineId,
            QualityInspectionStatus status,
            int recordingState
    );

    Optional<QualityInspectionJpaEntity> findBySourceReceiptLineIdAndRecordingState(
            long sourceReceiptLineId,
            int recordingState
    );
}
