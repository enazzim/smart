package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.domain.purchase.PartnerPaymentStatus;
import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataPartnerPaymentRepository extends JpaRepository<PartnerPaymentJpaEntity, Long> {

    long countByPaymentNoStartingWithAndRecordingState(String prefix, int recordingState);

    Optional<PartnerPaymentJpaEntity> findByIdAndRecordingStateAndStatus(
            long id,
            int recordingState,
            PartnerPaymentStatus status
    );

    @Query("""
            SELECT COALESCE(SUM(p.supplyAmount), 0)
            FROM PartnerPaymentJpaEntity p
            WHERE p.partnerId = :partnerId
              AND p.recordingState = :active
              AND p.status = :issued
            """)
    java.math.BigDecimal sumIssuedSupplyAmountByPartnerId(
            @Param("partnerId") long partnerId,
            @Param("active") int active,
            @Param("issued") PartnerPaymentStatus issued
    );

    @Query("""
            SELECT p FROM PartnerPaymentJpaEntity p
            WHERE p.recordingState = :active
              AND (:paymentDateFrom IS NULL OR p.paymentDate >= :paymentDateFrom)
              AND (:paymentDateTo IS NULL OR p.paymentDate <= :paymentDateTo)
              AND (:paymentNo IS NULL OR p.paymentNo LIKE CONCAT('%', :paymentNo, '%'))
              AND (:status IS NULL OR p.status = :status)
              AND (:excludeCancelled = false OR p.status <> :cancelled)
            ORDER BY p.paymentDate DESC, p.id DESC
            """)
    List<PartnerPaymentJpaEntity> searchActive(
            @Param("active") int active,
            @Param("paymentDateFrom") java.time.LocalDate paymentDateFrom,
            @Param("paymentDateTo") java.time.LocalDate paymentDateTo,
            @Param("paymentNo") String paymentNo,
            @Param("status") PartnerPaymentStatus status,
            @Param("excludeCancelled") boolean excludeCancelled,
            @Param("cancelled") PartnerPaymentStatus cancelled
    );
}
