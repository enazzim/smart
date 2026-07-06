package com.shindong.smartmanager.infrastructure.persistence.purchase;

import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataPurchaseReceiptRepository extends JpaRepository<PurchaseReceiptJpaEntity, Long> {

    long countByReceiptNoStartingWith(String prefix);

    long countByReceiptNoStartingWithAndRecordingState(String prefix, int recordingState);
}
