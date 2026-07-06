package com.shindong.smartmanager.infrastructure.persistence.outsource;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataOutsourcingShipmentInputLineRepository
        extends JpaRepository<OutsourcingShipmentInputLineJpaEntity, Long> {

    List<OutsourcingShipmentInputLineJpaEntity> findByOutsourcingShipmentLineIdAndRecordingStateOrderByLineNoAsc(
            long outsourcingShipmentLineId,
            int recordingState
    );
}
