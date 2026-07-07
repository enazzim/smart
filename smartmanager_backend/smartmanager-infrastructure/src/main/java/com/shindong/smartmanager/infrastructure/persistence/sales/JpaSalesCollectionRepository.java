package com.shindong.smartmanager.infrastructure.persistence.sales;

import com.shindong.smartmanager.application.sales.SalesCollectionCandidateCriteria;
import com.shindong.smartmanager.application.sales.SalesCollectionCandidateView;
import com.shindong.smartmanager.application.sales.SalesCollectionListCriteria;
import com.shindong.smartmanager.application.sales.SalesCollectionRepository;
import com.shindong.smartmanager.application.sales.SalesCollectionSaveCommand;
import com.shindong.smartmanager.application.sales.SalesCollectionView;
import com.shindong.smartmanager.domain.sales.SalesCollectionStatus;
import com.shindong.smartmanager.infrastructure.persistence.company.CompanyJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.company.SpringDataCompanyRepository;
import jakarta.persistence.EntityManager;
import jakarta.persistence.Query;
import java.math.BigDecimal;
import java.time.Instant;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaSalesCollectionRepository implements SalesCollectionRepository {

    private static final int ACTIVE = 1;

    private final EntityManager entityManager;
    private final SpringDataSalesCollectionRepository collectionRepository;
    private final SpringDataCompanyRepository companyRepository;

    public JpaSalesCollectionRepository(
            EntityManager entityManager,
            SpringDataSalesCollectionRepository collectionRepository,
            SpringDataCompanyRepository companyRepository
    ) {
        this.entityManager = entityManager;
        this.collectionRepository = collectionRepository;
        this.companyRepository = companyRepository;
    }

    @Override
    public long countByCollectionNoPrefix(String prefix) {
        return collectionRepository.countByCollectionNoStartingWithAndRecordingState(prefix, ACTIVE);
    }

    @Override
    @Transactional(readOnly = true)
    public BigDecimal sumIssuedRevenueAmountByPartnerId(long partnerId) {
        return sumIssuedRevenueAmount(partnerId);
    }

    @Override
    @Transactional(readOnly = true)
    public BigDecimal sumIssuedCollectionAmountByPartnerId(long partnerId) {
        BigDecimal sum = collectionRepository.sumIssuedTotalAmountByPartnerId(
                partnerId, ACTIVE, SalesCollectionStatus.ISSUED);
        return sum != null ? sum : BigDecimal.ZERO;
    }

    @Override
    @Transactional(readOnly = true)
    public List<SalesCollectionCandidateView> findCandidates(SalesCollectionCandidateCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT c.id, c.company_name, c.business_reg_no,
                       COALESCE(rev.revenue_amount, 0) AS revenue_amount,
                       COALESCE(col.collected_amount, 0) AS collected_amount
                FROM company c
                JOIN company_role cr ON cr.company_id = c.id AND cr.role_type = 'SALES'
                LEFT JOIN (
                    SELECT sr.partner_id, SUM(srl.amount) AS revenue_amount
                    FROM sales_revenue sr
                    JOIN sales_revenue_line srl ON srl.sales_revenue_id = sr.id AND srl.recording_state = 1
                    WHERE sr.recording_state = 1 AND sr.status = 'ISSUED'
                    GROUP BY sr.partner_id
                ) rev ON rev.partner_id = c.id
                LEFT JOIN (
                    SELECT sc.partner_id, SUM(sc.total_amount) AS collected_amount
                    FROM sales_collection sc
                    WHERE sc.recording_state = 1 AND sc.status = 'ISSUED'
                    GROUP BY sc.partner_id
                ) col ON col.partner_id = c.id
                WHERE c.recording_state = 1
                  AND (COALESCE(rev.revenue_amount, 0) - COALESCE(col.collected_amount, 0)) > 0
                """);
        Map<String, Object> params = new HashMap<>();
        if (criteria != null && criteria.partnerName() != null && !criteria.partnerName().isBlank()) {
            sql.append(" AND c.company_name LIKE :partnerName");
            params.put("partnerName", "%" + criteria.partnerName().trim() + "%");
        }
        sql.append(" ORDER BY c.company_name");

        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);

        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<SalesCollectionCandidateView> result = new ArrayList<>();
        for (Object[] row : rows) {
            BigDecimal revenueAmount = toBigDecimal(row[3]);
            BigDecimal collectedAmount = toBigDecimal(row[4]);
            BigDecimal uncollectedAmount = revenueAmount.subtract(collectedAmount).max(BigDecimal.ZERO);
            result.add(new SalesCollectionCandidateView(
                    ((Number) row[0]).longValue(),
                    row[1].toString(),
                    row[2] != null ? row[2].toString() : "",
                    revenueAmount,
                    collectedAmount,
                    uncollectedAmount,
                    uncollectedAmount.compareTo(BigDecimal.ZERO) > 0
            ));
        }
        return result;
    }

    @Override
    @Transactional
    public SalesCollectionView save(SalesCollectionSaveCommand command, String actorUserId) {
        Instant now = Instant.now();
        SalesCollectionJpaEntity entity = new SalesCollectionJpaEntity();
        entity.setCollectionNo(command.collectionNo());
        entity.setPartnerId(command.partnerId());
        entity.setCollectionDate(command.collectionDate());
        entity.setSupplyAmount(command.supplyAmount());
        entity.setVatAmount(command.vatAmount());
        entity.setTotalAmount(command.totalAmount());
        entity.setPaymentMethod(command.paymentMethod());
        entity.setRemark(command.remark());
        entity.setStatus(SalesCollectionStatus.ISSUED);
        entity.setRecordingState(ACTIVE);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        SalesCollectionJpaEntity saved = collectionRepository.save(entity);
        return toView(saved);
    }

    @Override
    @Transactional(readOnly = true)
    public List<SalesCollectionView> findAllActive(SalesCollectionListCriteria criteria) {
        List<SalesCollectionJpaEntity> rows;
        if (criteria == null) {
            rows = collectionRepository.searchActive(
                    ACTIVE, null, null, null, null, true, SalesCollectionStatus.CANCELLED);
        } else {
            rows = collectionRepository.searchActive(
                    ACTIVE,
                    criteria.collectionDateFrom(),
                    criteria.collectionDateTo(),
                    normalize(criteria.collectionNo()),
                    criteria.status(),
                    criteria.excludeCancelled(),
                    SalesCollectionStatus.CANCELLED
            );
        }
        return rows.stream()
                .map(this::toView)
                .filter(view -> matchesPartnerName(view, criteria))
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<SalesCollectionView> findActiveIssuedById(long id) {
        return collectionRepository.findByIdAndRecordingStateAndStatus(id, ACTIVE, SalesCollectionStatus.ISSUED)
                .map(this::toView);
    }

    @Override
    @Transactional
    public void cancelById(long id, String actorUserId) {
        SalesCollectionJpaEntity entity = collectionRepository
                .findByIdAndRecordingStateAndStatus(id, ACTIVE, SalesCollectionStatus.ISSUED)
                .orElseThrow(() -> new IllegalArgumentException("수금을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setStatus(SalesCollectionStatus.CANCELLED);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        collectionRepository.save(entity);
    }

    private BigDecimal sumIssuedRevenueAmount(long partnerId) {
        String sql = """
                SELECT COALESCE(SUM(srl.amount), 0)
                FROM sales_revenue sr
                JOIN sales_revenue_line srl ON srl.sales_revenue_id = sr.id AND srl.recording_state = 1
                WHERE sr.recording_state = 1 AND sr.status = 'ISSUED' AND sr.partner_id = :partnerId
                """;
        Query query = entityManager.createNativeQuery(sql);
        query.setParameter("partnerId", partnerId);
        Object result = query.getSingleResult();
        return toBigDecimal(result);
    }

    private SalesCollectionView toView(SalesCollectionJpaEntity entity) {
        CompanyJpaEntity partner = companyRepository.findById(entity.getPartnerId()).orElse(null);
        return new SalesCollectionView(
                entity.getId(),
                entity.getCollectionNo(),
                entity.getPartnerId(),
                partner != null ? partner.getCompanyName() : "",
                partner != null ? partner.getBusinessRegNo() : "",
                entity.getCollectionDate(),
                entity.getSupplyAmount(),
                entity.getVatAmount(),
                entity.getTotalAmount(),
                entity.getPaymentMethod(),
                entity.getRemark(),
                entity.getStatus(),
                entity.getCreatedAt(),
                entity.getCreatedBy(),
                entity.getStatus() == SalesCollectionStatus.ISSUED
        );
    }

    private boolean matchesPartnerName(SalesCollectionView view, SalesCollectionListCriteria criteria) {
        if (criteria == null || criteria.partnerName() == null || criteria.partnerName().isBlank()) {
            return true;
        }
        String needle = criteria.partnerName().trim().toLowerCase();
        return view.partnerName().toLowerCase().contains(needle);
    }

    private String normalize(String value) {
        if (value == null || value.isBlank()) {
            return null;
        }
        return value.trim();
    }

    private static BigDecimal toBigDecimal(Object value) {
        if (value == null) {
            return BigDecimal.ZERO;
        }
        if (value instanceof BigDecimal decimal) {
            return decimal;
        }
        return new BigDecimal(value.toString());
    }
}
