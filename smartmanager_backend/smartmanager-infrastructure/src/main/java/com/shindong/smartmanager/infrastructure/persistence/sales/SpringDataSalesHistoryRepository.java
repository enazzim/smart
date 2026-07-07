package com.shindong.smartmanager.infrastructure.persistence.sales;

import com.shindong.smartmanager.domain.sales.SalesHistorySourceType;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataSalesHistoryRepository extends JpaRepository<SalesHistoryJpaEntity, Long> {

    List<SalesHistoryJpaEntity> findBySourceTypeAndSourceIdAndRecordingState(
            SalesHistorySourceType sourceType,
            long sourceId,
            int recordingState
    );
}
