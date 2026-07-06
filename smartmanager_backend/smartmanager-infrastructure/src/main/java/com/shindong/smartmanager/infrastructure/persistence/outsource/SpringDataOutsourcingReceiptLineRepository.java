package com.shindong.smartmanager.infrastructure.persistence.outsource;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataOutsourcingReceiptLineRepository extends JpaRepository<OutsourcingReceiptLineJpaEntity, Long> {

    List<OutsourcingReceiptLineJpaEntity> findByOutsourcingReceiptIdAndRecordingStateOrderByLineNoAsc(
            long outsourcingReceiptId,
            int recordingState
    );

    Optional<OutsourcingReceiptLineJpaEntity> findByIdAndRecordingState(long id, int recordingState);
}
