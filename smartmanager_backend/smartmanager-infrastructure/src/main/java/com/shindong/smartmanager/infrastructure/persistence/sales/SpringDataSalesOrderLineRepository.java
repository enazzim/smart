package com.shindong.smartmanager.infrastructure.persistence.sales;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataSalesOrderLineRepository extends JpaRepository<SalesOrderLineJpaEntity, Long> {

    List<SalesOrderLineJpaEntity> findBySalesOrderIdAndRecordingStateOrderByLineNoAsc(
            long salesOrderId,
            int recordingState
    );

    void deleteBySalesOrderId(long salesOrderId);

    Optional<SalesOrderLineJpaEntity> findByIdAndRecordingState(long id, int recordingState);

    List<SalesOrderLineJpaEntity> findByRecordingStateOrderByIdDesc(int recordingState);
}
