package com.shindong.smartmanager.infrastructure.persistence.outsource;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataOutsourcingShipmentLineRepository extends JpaRepository<OutsourcingShipmentLineJpaEntity, Long> {

    List<OutsourcingShipmentLineJpaEntity> findByOutsourcingShipmentIdAndRecordingStateOrderByLineNoAsc(
            long outsourcingShipmentId,
            int recordingState
    );
}
