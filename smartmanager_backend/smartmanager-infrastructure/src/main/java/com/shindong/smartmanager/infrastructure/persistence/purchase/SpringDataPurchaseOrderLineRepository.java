package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.domain.purchase.PurchaseOrderStatus;
import java.math.BigDecimal;
import java.util.Collection;
import java.util.List;
import java.util.Set;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataPurchaseOrderLineRepository extends JpaRepository<PurchaseOrderLineJpaEntity, Long> {

    @Query("""
            SELECT CASE WHEN COUNT(pol) > 0 THEN true ELSE false END
            FROM PurchaseOrderLineJpaEntity pol
            JOIN PurchaseOrderJpaEntity po ON po.id = pol.purchaseOrderId
            WHERE pol.requirementLineId = :requirementLineId
              AND pol.recordingState = :active
              AND po.recordingState = :active
              AND po.status <> :cancelled
            """)
    boolean existsActiveOrderReferencingRequirementLine(
            @Param("requirementLineId") long requirementLineId,
            @Param("active") int active,
            @Param("cancelled") PurchaseOrderStatus cancelled
    );

    @Query("""
            SELECT CASE WHEN COUNT(pol) > 0 THEN true ELSE false END
            FROM PurchaseOrderLineJpaEntity pol
            JOIN PurchaseOrderJpaEntity po ON po.id = pol.purchaseOrderId
            WHERE pol.requirementLineId IN :requirementLineIds
              AND pol.recordingState = :active
              AND po.recordingState = :active
              AND po.status <> :cancelled
            """)
    boolean existsActiveOrderReferencingAnyRequirementLines(
            @Param("requirementLineIds") Collection<Long> requirementLineIds,
            @Param("active") int active,
            @Param("cancelled") PurchaseOrderStatus cancelled
    );

    @Query("""
            SELECT pol.requirementLineId
            FROM PurchaseOrderLineJpaEntity pol
            JOIN PurchaseOrderJpaEntity po ON po.id = pol.purchaseOrderId
            WHERE pol.requirementLineId IN :requirementLineIds
              AND pol.recordingState = :active
              AND po.recordingState = :active
              AND po.status <> :cancelled
            """)
    Set<Long> findReferencedRequirementLineIds(
            @Param("requirementLineIds") Collection<Long> requirementLineIds,
            @Param("active") int active,
            @Param("cancelled") PurchaseOrderStatus cancelled
    );

    @Query("""
            SELECT COALESCE(SUM(pol.orderQty), 0) FROM PurchaseOrderLineJpaEntity pol
            JOIN PurchaseOrderJpaEntity po ON po.id = pol.purchaseOrderId
            WHERE pol.requirementLineId = :requirementLineId
              AND pol.recordingState = :active
              AND po.recordingState = :active
              AND po.status <> :cancelled
            """)
    BigDecimal sumOrderedQtyByRequirementLineId(
            @Param("requirementLineId") long requirementLineId,
            @Param("active") int active,
            @Param("cancelled") PurchaseOrderStatus cancelled
    );

    @Query("""
            SELECT pol.requirementLineId, COALESCE(SUM(pol.orderQty), 0)
            FROM PurchaseOrderLineJpaEntity pol
            JOIN PurchaseOrderJpaEntity po ON po.id = pol.purchaseOrderId
            WHERE pol.requirementLineId IN :requirementLineIds
              AND pol.recordingState = :active
              AND po.recordingState = :active
              AND po.status <> :cancelled
            GROUP BY pol.requirementLineId
            """)
    List<Object[]> sumOrderedQtyGroupedByRequirementLineId(
            @Param("requirementLineIds") Collection<Long> requirementLineIds,
            @Param("active") int active,
            @Param("cancelled") PurchaseOrderStatus cancelled
    );

    List<PurchaseOrderLineJpaEntity> findByPurchaseOrderIdAndRecordingStateOrderByLineNoAsc(
            long purchaseOrderId,
            int recordingState
    );

    @Query("""
            SELECT pol FROM PurchaseOrderLineJpaEntity pol
            WHERE pol.purchaseOrderId = :purchaseOrderId
              AND pol.recordingState = :active
              AND pol.requirementLineId IS NOT NULL
            """)
    List<PurchaseOrderLineJpaEntity> findActiveLinesWithRequirementReference(
            @Param("purchaseOrderId") long purchaseOrderId,
            @Param("active") int active
    );

    void deleteByPurchaseOrderId(long purchaseOrderId);
}
