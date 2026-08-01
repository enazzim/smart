package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.domain.purchase.PurchaseOrderSourceType;
import com.shindong.smartmanager.domain.purchase.PurchaseOrderStatus;
import java.time.LocalDate;
import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataPurchaseOrderRepository extends JpaRepository<PurchaseOrderJpaEntity, Long> {

    long countByOrderNoStartingWithAndRecordingState(String prefix, int recordingState);

    boolean existsByOrderNoAndRecordingState(String orderNo, int recordingState);

    List<PurchaseOrderJpaEntity> findByRecordingStateOrderByOrderDateDescIdDesc(int recordingState);

    Optional<PurchaseOrderJpaEntity> findByIdAndRecordingState(long id, int recordingState);

    @Query("""
            SELECT po FROM PurchaseOrderJpaEntity po
            JOIN CompanyJpaEntity c ON c.id = po.partnerId
            WHERE po.recordingState = :active
              AND (:orderDateFrom IS NULL OR po.orderDate >= :orderDateFrom)
              AND (:orderDateTo IS NULL OR po.orderDate <= :orderDateTo)
              AND (:status IS NULL OR po.status = :status)
              AND (:excludeCancelled = false OR po.status <> :cancelledStatus)
              AND (:orderNo IS NULL OR :orderNo = '' OR LOWER(po.orderNo) LIKE LOWER(CONCAT('%', :orderNo, '%')))
              AND (:partnerName IS NULL OR :partnerName = '' OR LOWER(c.companyName) LIKE LOWER(CONCAT('%', :partnerName, '%')))
              AND (
                    :itemPropertyScope IS NULL OR :itemPropertyScope = ''
                    OR (
                        :itemPropertyScope = 'SUB_MATERIAL'
                        AND EXISTS (
                            SELECT 1 FROM PurchaseOrderLineJpaEntity pol
                            JOIN ItemJpaEntity i ON i.id = pol.itemId
                            WHERE pol.purchaseOrderId = po.id
                              AND pol.recordingState = 1
                              AND i.propertyClassification = com.shindong.smartmanager.domain.item.PropertyClassification.부자재
                        )
                        AND NOT EXISTS (
                            SELECT 1 FROM PurchaseOrderLineJpaEntity pol2
                            JOIN ItemJpaEntity i2 ON i2.id = pol2.itemId
                            WHERE pol2.purchaseOrderId = po.id
                              AND pol2.recordingState = 1
                              AND i2.propertyClassification <> com.shindong.smartmanager.domain.item.PropertyClassification.부자재
                        )
                    )
                    OR (
                        :itemPropertyScope = 'GENERAL'
                        AND EXISTS (
                            SELECT 1 FROM PurchaseOrderLineJpaEntity pol
                            JOIN ItemJpaEntity i ON i.id = pol.itemId
                            WHERE pol.purchaseOrderId = po.id
                              AND pol.recordingState = 1
                              AND i.propertyClassification IN (
                                  com.shindong.smartmanager.domain.item.PropertyClassification.원자재,
                                  com.shindong.smartmanager.domain.item.PropertyClassification.상품
                              )
                        )
                        AND NOT EXISTS (
                            SELECT 1 FROM PurchaseOrderLineJpaEntity pol2
                            JOIN ItemJpaEntity i2 ON i2.id = pol2.itemId
                            WHERE pol2.purchaseOrderId = po.id
                              AND pol2.recordingState = 1
                              AND i2.propertyClassification = com.shindong.smartmanager.domain.item.PropertyClassification.부자재
                        )
                    )
              )
            ORDER BY po.orderDate DESC, po.id DESC
            """)
    List<PurchaseOrderJpaEntity> searchActive(
            @Param("active") int active,
            @Param("orderDateFrom") LocalDate orderDateFrom,
            @Param("orderDateTo") LocalDate orderDateTo,
            @Param("partnerName") String partnerName,
            @Param("orderNo") String orderNo,
            @Param("status") PurchaseOrderStatus status,
            @Param("excludeCancelled") boolean excludeCancelled,
            @Param("cancelledStatus") PurchaseOrderStatus cancelledStatus,
            @Param("itemPropertyScope") String itemPropertyScope
    );
}
