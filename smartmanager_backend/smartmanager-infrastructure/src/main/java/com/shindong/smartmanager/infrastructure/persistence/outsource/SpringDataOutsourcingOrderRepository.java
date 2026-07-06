package com.shindong.smartmanager.infrastructure.persistence.outsource;

import com.shindong.smartmanager.domain.outsource.OutsourcingOrderStatus;
import java.time.LocalDate;
import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataOutsourcingOrderRepository extends JpaRepository<OutsourcingOrderJpaEntity, Long> {

    long countByOrderNoStartingWithAndRecordingState(String prefix, int recordingState);

    boolean existsByOrderNoAndRecordingState(String orderNo, int recordingState);

    List<OutsourcingOrderJpaEntity> findByRecordingStateOrderByOrderDateDescIdDesc(int recordingState);

    Optional<OutsourcingOrderJpaEntity> findByIdAndRecordingState(long id, int recordingState);

    @Query("""
            SELECT oo FROM OutsourcingOrderJpaEntity oo
            JOIN CompanyJpaEntity c ON c.id = oo.partnerId
            WHERE oo.recordingState = :active
              AND (:orderDateFrom IS NULL OR oo.orderDate >= :orderDateFrom)
              AND (:orderDateTo IS NULL OR oo.orderDate <= :orderDateTo)
              AND (:status IS NULL OR oo.status = :status)
              AND (:excludeCancelled = false OR oo.status <> :cancelledStatus)
              AND (:orderNo IS NULL OR :orderNo = '' OR LOWER(oo.orderNo) LIKE LOWER(CONCAT('%', :orderNo, '%')))
              AND (:partnerName IS NULL OR :partnerName = '' OR LOWER(c.companyName) LIKE LOWER(CONCAT('%', :partnerName, '%')))
            ORDER BY oo.orderDate DESC, oo.id DESC
            """)
    List<OutsourcingOrderJpaEntity> searchActive(
            @Param("active") int active,
            @Param("orderDateFrom") LocalDate orderDateFrom,
            @Param("orderDateTo") LocalDate orderDateTo,
            @Param("partnerName") String partnerName,
            @Param("orderNo") String orderNo,
            @Param("status") OutsourcingOrderStatus status,
            @Param("excludeCancelled") boolean excludeCancelled,
            @Param("cancelledStatus") OutsourcingOrderStatus cancelledStatus
    );
}
