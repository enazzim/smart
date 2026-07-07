package com.shindong.smartmanager.infrastructure.persistence.sales;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataSalesRevenueLineRepository extends JpaRepository<SalesRevenueLineJpaEntity, Long> {

    List<SalesRevenueLineJpaEntity> findBySalesRevenueIdAndRecordingStateOrderByLineNoAsc(
            long salesRevenueId,
            int recordingState
    );
}
