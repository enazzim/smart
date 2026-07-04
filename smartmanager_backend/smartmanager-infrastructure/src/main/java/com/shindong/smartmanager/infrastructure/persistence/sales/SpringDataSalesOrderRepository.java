package com.shindong.smartmanager.infrastructure.persistence.sales;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataSalesOrderRepository extends JpaRepository<SalesOrderJpaEntity, Long> {

    long countByOrderNoStartingWithAndRecordingState(String prefix, int recordingState);

    boolean existsByOrderNoAndRecordingState(String orderNo, int recordingState);

    List<SalesOrderJpaEntity> findByRecordingStateOrderByOrderDateDescIdDesc(int recordingState);

    Optional<SalesOrderJpaEntity> findByIdAndRecordingState(long id, int recordingState);
}
