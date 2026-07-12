package com.shindong.smartmanager.infrastructure.persistence.quality;

import com.shindong.smartmanager.application.quality.QualityInspectionListCriteria;
import com.shindong.smartmanager.application.quality.QualityInspectionRepository;
import com.shindong.smartmanager.application.quality.QualityInspectionView;
import com.shindong.smartmanager.domain.quality.QualityInspectionSourceType;
import com.shindong.smartmanager.domain.quality.QualityInspectionStatus;
import com.shindong.smartmanager.infrastructure.persistence.company.CompanyJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.company.SpringDataCompanyRepository;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import com.shindong.smartmanager.infrastructure.persistence.purchase.PurchaseOrderJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.purchase.PurchaseOrderLineJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.purchase.PurchaseReceiptJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.purchase.PurchaseReceiptLineJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.purchase.SpringDataPurchaseOrderLineRepository;
import com.shindong.smartmanager.infrastructure.persistence.purchase.SpringDataPurchaseOrderRepository;
import com.shindong.smartmanager.infrastructure.persistence.purchase.SpringDataPurchaseReceiptLineRepository;
import com.shindong.smartmanager.infrastructure.persistence.purchase.SpringDataPurchaseReceiptRepository;
import com.shindong.smartmanager.infrastructure.persistence.outsource.OutsourcingOrderJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.outsource.OutsourcingOrderLineJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.outsource.OutsourcingReceiptJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.outsource.OutsourcingReceiptLineJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.outsource.SpringDataOutsourcingOrderLineRepository;
import com.shindong.smartmanager.infrastructure.persistence.outsource.SpringDataOutsourcingOrderRepository;
import com.shindong.smartmanager.infrastructure.persistence.outsource.SpringDataOutsourcingReceiptLineRepository;
import com.shindong.smartmanager.infrastructure.persistence.outsource.SpringDataOutsourcingReceiptRepository;
import jakarta.persistence.EntityManager;
import jakarta.persistence.Query;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaQualityInspectionRepository implements QualityInspectionRepository {

    private static final int ACTIVE = 1;

    private final SpringDataQualityInspectionRepository inspectionRepository;
    private final SpringDataItemRepository itemRepository;
    private final SpringDataCompanyRepository companyRepository;
    private final SpringDataPurchaseReceiptLineRepository receiptLineRepository;
    private final SpringDataPurchaseReceiptRepository receiptRepository;
    private final SpringDataPurchaseOrderLineRepository orderLineRepository;
    private final SpringDataPurchaseOrderRepository orderRepository;
    private final SpringDataOutsourcingReceiptLineRepository outsourcingReceiptLineRepository;
    private final SpringDataOutsourcingReceiptRepository outsourcingReceiptRepository;
    private final SpringDataOutsourcingOrderLineRepository outsourcingOrderLineRepository;
    private final SpringDataOutsourcingOrderRepository outsourcingOrderRepository;
    private final EntityManager entityManager;

    public JpaQualityInspectionRepository(
            SpringDataQualityInspectionRepository inspectionRepository,
            SpringDataItemRepository itemRepository,
            SpringDataCompanyRepository companyRepository,
            SpringDataPurchaseReceiptLineRepository receiptLineRepository,
            SpringDataPurchaseReceiptRepository receiptRepository,
            SpringDataPurchaseOrderLineRepository orderLineRepository,
            SpringDataPurchaseOrderRepository orderRepository,
            SpringDataOutsourcingReceiptLineRepository outsourcingReceiptLineRepository,
            SpringDataOutsourcingReceiptRepository outsourcingReceiptRepository,
            SpringDataOutsourcingOrderLineRepository outsourcingOrderLineRepository,
            SpringDataOutsourcingOrderRepository outsourcingOrderRepository,
            EntityManager entityManager
    ) {
        this.inspectionRepository = inspectionRepository;
        this.itemRepository = itemRepository;
        this.companyRepository = companyRepository;
        this.receiptLineRepository = receiptLineRepository;
        this.receiptRepository = receiptRepository;
        this.orderLineRepository = orderLineRepository;
        this.orderRepository = orderRepository;
        this.outsourcingReceiptLineRepository = outsourcingReceiptLineRepository;
        this.outsourcingReceiptRepository = outsourcingReceiptRepository;
        this.outsourcingOrderLineRepository = outsourcingOrderLineRepository;
        this.outsourcingOrderRepository = outsourcingOrderRepository;
        this.entityManager = entityManager;
    }

    @Override
    @Transactional
    public long createPending(
            QualityInspectionSourceType sourceType,
            long sourceReceiptLineId,
            long itemId,
            long companyId,
            BigDecimal requestQty,
            String actorUserId
    ) {
        Instant now = Instant.now();
        QualityInspectionJpaEntity entity = new QualityInspectionJpaEntity();
        entity.setSourceType(sourceType);
        entity.setSourceReceiptLineId(sourceReceiptLineId);
        entity.setItemId(itemId);
        entity.setCompanyId(companyId);
        entity.setRequestQty(requestQty);
        entity.setStatus(QualityInspectionStatus.PENDING);
        entity.setRecordingState(ACTIVE);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        return inspectionRepository.save(entity).getId();
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<QualityInspectionView> findActiveById(long id) {
        return inspectionRepository.findByIdAndRecordingState(id, ACTIVE).map(this::toView);
    }

    @Override
    @Transactional(readOnly = true)
    public List<QualityInspectionView> findActive(QualityInspectionListCriteria criteria) {
        QualityInspectionStatus status = criteria.status() != null
                ? criteria.status()
                : QualityInspectionStatus.PENDING;

        StringBuilder sql = new StringBuilder("""
                SELECT qi.id
                FROM quality_inspection qi
                JOIN company c ON c.id = qi.company_id
                JOIN item i ON i.id = qi.item_id
                LEFT JOIN purchase_receipt_line prl
                    ON qi.source_type = 'PURCHASE' AND prl.id = qi.source_receipt_line_id AND prl.recording_state = 1
                LEFT JOIN purchase_receipt pr ON pr.id = prl.purchase_receipt_id AND pr.recording_state = 1
                LEFT JOIN outsourcing_receipt_line orl
                    ON qi.source_type = 'OUTSOURCE' AND orl.id = qi.source_receipt_line_id AND orl.recording_state = 1
                LEFT JOIN outsourcing_receipt orc ON orc.id = orl.outsourcing_receipt_id AND orc.recording_state = 1
                WHERE qi.recording_state = 1
                  AND qi.status = :status
                """);
        Map<String, Object> params = new HashMap<>();
        params.put("status", status.name());

        if (criteria.sourceType() != null) {
            sql.append(" AND qi.source_type = :sourceType");
            params.put("sourceType", criteria.sourceType().name());
        }
        if (criteria.partnerName() != null && !criteria.partnerName().isBlank()) {
            sql.append(" AND c.company_name LIKE :partnerName");
            params.put("partnerName", "%" + criteria.partnerName().trim() + "%");
        }
        if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
            sql.append(" AND i.item_no LIKE :itemNo");
            params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
        }
        if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
            sql.append(" AND i.item_name LIKE :itemName");
            params.put("itemName", "%" + criteria.itemName().trim() + "%");
        }
        if (criteria.receiptDateFrom() != null) {
            sql.append(" AND COALESCE(pr.receipt_date, orc.receipt_date) >= :receiptDateFrom");
            params.put("receiptDateFrom", criteria.receiptDateFrom());
        }
        if (criteria.receiptDateTo() != null) {
            sql.append(" AND COALESCE(pr.receipt_date, orc.receipt_date) <= :receiptDateTo");
            params.put("receiptDateTo", criteria.receiptDateTo());
        }
        if (criteria.completedDateFrom() != null) {
            sql.append(" AND qi.completed_at IS NOT NULL AND DATE(qi.completed_at) >= :completedDateFrom");
            params.put("completedDateFrom", criteria.completedDateFrom());
        }
        if (criteria.completedDateTo() != null) {
            sql.append(" AND qi.completed_at IS NOT NULL AND DATE(qi.completed_at) <= :completedDateTo");
            params.put("completedDateTo", criteria.completedDateTo());
        }

        if (status == QualityInspectionStatus.COMPLETED) {
            sql.append(" ORDER BY qi.completed_at DESC, qi.id DESC");
        } else {
            sql.append(" ORDER BY qi.created_at DESC, qi.id DESC");
        }

        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);

        @SuppressWarnings("unchecked")
        List<Number> rows = query.getResultList();
        List<QualityInspectionView> result = new ArrayList<>();
        for (Number row : rows) {
            findActiveById(row.longValue()).ifPresent(result::add);
        }
        return result;
    }

    @Override
    @Transactional
    public void complete(
            long id,
            BigDecimal passedQty,
            BigDecimal failedQty,
            Long inspectionDecisionCodeId,
            Long unsuitabilityCauseCodeId,
            Long unsuitabilityStatusCodeId,
            Instant completedAt,
            String actorUserId
    ) {
        QualityInspectionJpaEntity entity = inspectionRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("품질검사를 찾을 수 없습니다: " + id));
        entity.setPassedQty(passedQty);
        entity.setFailedQty(failedQty);
        entity.setInspectionDecisionCodeId(inspectionDecisionCodeId);
        entity.setUnsuitabilityCauseCodeId(unsuitabilityCauseCodeId);
        entity.setUnsuitabilityStatusCodeId(unsuitabilityStatusCodeId);
        entity.setStatus(QualityInspectionStatus.COMPLETED);
        entity.setCompletedAt(completedAt);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(Instant.now());
        inspectionRepository.save(entity);
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<QualityInspectionView> findPendingByReceiptLineId(long receiptLineId) {
        return inspectionRepository
                .findBySourceReceiptLineIdAndStatusAndRecordingState(
                        receiptLineId, QualityInspectionStatus.PENDING, ACTIVE)
                .map(this::toView);
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<QualityInspectionView> findActiveByReceiptLineId(long receiptLineId) {
        return inspectionRepository.findBySourceReceiptLineIdAndRecordingState(receiptLineId, ACTIVE)
                .map(this::toView);
    }

    @Override
    @Transactional
    public void cancelById(long id, String actorUserId) {
        QualityInspectionJpaEntity entity = inspectionRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("품질검사를 찾을 수 없습니다: " + id));
        if (entity.getStatus() != QualityInspectionStatus.COMPLETED) {
            throw new IllegalArgumentException("완료된 검사만 취소할 수 있습니다.");
        }
        entity.setStatus(QualityInspectionStatus.CANCELLED);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(Instant.now());
        inspectionRepository.save(entity);
    }

    @Override
    @Transactional
    public void cancelByReceiptLineId(long receiptLineId, String actorUserId) {
        inspectionRepository.findBySourceReceiptLineIdAndRecordingState(receiptLineId, ACTIVE)
                .ifPresent(entity -> {
                    if (entity.getStatus() == QualityInspectionStatus.PENDING
                            || entity.getStatus() == QualityInspectionStatus.COMPLETED) {
                        entity.setStatus(QualityInspectionStatus.CANCELLED);
                        entity.setUpdatedBy(actorUserId);
                        entity.setUpdatedById(actorUserId);
                        entity.setUpdatedAt(Instant.now());
                        inspectionRepository.save(entity);
                    }
                });
    }

    private QualityInspectionView toView(QualityInspectionJpaEntity entity) {
        ItemJpaEntity item = itemRepository.findById(entity.getItemId()).orElse(null);
        CompanyJpaEntity company = companyRepository.findById(entity.getCompanyId()).orElse(null);
        String orderNo = null;
        LocalDate receiptDate = null;
        if (entity.getSourceType() == QualityInspectionSourceType.PURCHASE) {
            PurchaseReceiptLineJpaEntity receiptLine = receiptLineRepository.findById(entity.getSourceReceiptLineId()).orElse(null);
            if (receiptLine != null) {
                PurchaseReceiptJpaEntity receipt = receiptRepository.findById(receiptLine.getPurchaseReceiptId()).orElse(null);
                if (receipt != null) {
                    receiptDate = receipt.getReceiptDate();
                }
                PurchaseOrderLineJpaEntity orderLine = orderLineRepository.findById(receiptLine.getPurchaseOrderLineId()).orElse(null);
                if (orderLine != null) {
                    PurchaseOrderJpaEntity order = orderRepository.findById(orderLine.getPurchaseOrderId()).orElse(null);
                    if (order != null) {
                        orderNo = order.getOrderNo();
                    }
                }
            }
        } else if (entity.getSourceType() == QualityInspectionSourceType.OUTSOURCE) {
            OutsourcingReceiptLineJpaEntity receiptLine = outsourcingReceiptLineRepository
                    .findById(entity.getSourceReceiptLineId()).orElse(null);
            if (receiptLine != null) {
                OutsourcingReceiptJpaEntity receipt = outsourcingReceiptRepository
                        .findById(receiptLine.getOutsourcingReceiptId()).orElse(null);
                if (receipt != null) {
                    receiptDate = receipt.getReceiptDate();
                }
                OutsourcingOrderLineJpaEntity orderLine = outsourcingOrderLineRepository
                        .findById(receiptLine.getOutsourcingOrderLineId()).orElse(null);
                if (orderLine != null) {
                    OutsourcingOrderJpaEntity order = outsourcingOrderRepository
                            .findById(orderLine.getOutsourcingOrderId()).orElse(null);
                    if (order != null) {
                        orderNo = order.getOrderNo();
                    }
                }
            }
        }
        return new QualityInspectionView(
                entity.getId(),
                entity.getInspectionNo(),
                entity.getSourceType(),
                entity.getSourceReceiptLineId(),
                entity.getItemId(),
                item != null ? item.getItemNo() : "",
                item != null ? item.getItemName() : "",
                entity.getCompanyId(),
                company != null ? company.getCompanyName() : "",
                entity.getRequestQty(),
                entity.getPassedQty(),
                entity.getFailedQty(),
                entity.getStatus(),
                orderNo,
                receiptDate,
                entity.getCreatedAt(),
                entity.getCompletedAt(),
                item != null && item.isLotTracked()
        );
    }
}
