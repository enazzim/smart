package com.shindong.smartmanager.infrastructure.persistence.sales;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataSalesShipmentLineRepository extends JpaRepository<SalesShipmentLineJpaEntity, Long> {

    List<SalesShipmentLineJpaEntity> findBySalesShipmentIdAndRecordingStateOrderByLineNoAsc(
            long salesShipmentId,
            int recordingState
    );

    Optional<SalesShipmentLineJpaEntity> findByIdAndRecordingState(long id, int recordingState);
}
