package com.shindong.smartmanager.infrastructure.persistence.purchase;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataPartnerPaymentLineRepository extends JpaRepository<PartnerPaymentLineJpaEntity, Long> {

    List<PartnerPaymentLineJpaEntity> findByPaymentIdAndRecordingStateOrderByIdAsc(long paymentId, int recordingState);

    List<PartnerPaymentLineJpaEntity> findByPaymentIdInAndRecordingState(List<Long> paymentIds, int recordingState);

    @Query("""
            SELECT COALESCE(SUM(o.amount), 0)
            FROM PartnerPrepaidOffsetJpaEntity o
            WHERE o.paymentLineId = :lineId AND o.recordingState = :active
            """)
    java.math.BigDecimal sumActiveOffsetByLineId(@Param("lineId") long lineId, @Param("active") int active);

    @Query("""
            SELECT COALESCE(SUM(o.amount), 0)
            FROM PartnerPrepaidOffsetJpaEntity o
            JOIN PartnerPaymentLineJpaEntity l ON l.id = o.paymentLineId
            WHERE l.paymentId = :paymentId
              AND o.recordingState = :active
              AND l.recordingState = :active
            """)
    java.math.BigDecimal sumActiveOffsetByPaymentId(@Param("paymentId") long paymentId, @Param("active") int active);
}
