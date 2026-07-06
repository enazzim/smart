package com.shindong.smartmanager.infrastructure.persistence.outsource;

import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataOutsourcingReceiptRepository extends JpaRepository<OutsourcingReceiptJpaEntity, Long> {

    long countByReceiptNoStartingWithAndRecordingState(String prefix, int recordingState);
}
