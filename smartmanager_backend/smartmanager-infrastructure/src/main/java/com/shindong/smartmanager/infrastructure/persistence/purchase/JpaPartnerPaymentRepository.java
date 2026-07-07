package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.application.purchase.PartnerPaymentCandidateCriteria;
import com.shindong.smartmanager.application.purchase.PartnerPaymentCandidateView;
import com.shindong.smartmanager.application.purchase.PartnerPaymentListCriteria;
import com.shindong.smartmanager.application.purchase.PartnerPaymentRepository;
import com.shindong.smartmanager.application.purchase.PartnerPaymentSaveCommand;
import com.shindong.smartmanager.application.purchase.PartnerPaymentView;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentStatus;
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
public class JpaPartnerPaymentRepository implements PartnerPaymentRepository {

    private static final int ACTIVE = 1;

    private final EntityManager entityManager;
    private final SpringDataPartnerPaymentRepository paymentRepository;
    private final SpringDataCompanyRepository companyRepository;

    public JpaPartnerPaymentRepository(
            EntityManager entityManager,
            SpringDataPartnerPaymentRepository paymentRepository,
            SpringDataCompanyRepository companyRepository
    ) {
        this.entityManager = entityManager;
        this.paymentRepository = paymentRepository;
        this.companyRepository = companyRepository;
    }

    @Override
    public long countByPaymentNoPrefix(String prefix) {
        return paymentRepository.countByPaymentNoStartingWithAndRecordingState(prefix, ACTIVE);
    }

    @Override
    @Transactional(readOnly = true)
    public BigDecimal sumIssuedPayableAmountByPartnerId(long partnerId) {
        return sumPurchaseHistoryAmount(partnerId).add(sumOutsourceHistoryAmount(partnerId));
    }

    @Override
    @Transactional(readOnly = true)
    public BigDecimal sumIssuedPaymentAmountByPartnerId(long partnerId) {
        BigDecimal sum = paymentRepository.sumIssuedTotalAmountByPartnerId(
                partnerId, ACTIVE, PartnerPaymentStatus.ISSUED);
        return sum != null ? sum : BigDecimal.ZERO;
    }

    @Override
    @Transactional(readOnly = true)
    public List<PartnerPaymentCandidateView> findCandidates(PartnerPaymentCandidateCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT c.id, c.company_name, c.business_reg_no,
                       COALESCE(ph.purchase_amount, 0) AS purchase_amount,
                       COALESCE(oh.outsource_amount, 0) AS outsource_amount,
                       COALESCE(pp.paid_amount, 0) AS paid_amount
                FROM company c
                LEFT JOIN (
                    SELECT company_id, SUM(amount) AS purchase_amount
                    FROM purchase_history
                    WHERE recording_state = 1
                    GROUP BY company_id
                ) ph ON ph.company_id = c.id
                LEFT JOIN (
                    SELECT company_id, SUM(amount) AS outsource_amount
                    FROM outsource_history
                    WHERE recording_state = 1
                    GROUP BY company_id
                ) oh ON oh.company_id = c.id
                LEFT JOIN (
                    SELECT partner_id, SUM(total_amount) AS paid_amount
                    FROM partner_payment
                    WHERE recording_state = 1 AND status = 'ISSUED'
                    GROUP BY partner_id
                ) pp ON pp.partner_id = c.id
                WHERE c.recording_state = 1
                  AND EXISTS (
                    SELECT 1 FROM company_role cr
                    WHERE cr.company_id = c.id AND cr.role_type IN ('PURCHASE', 'OUTSOURCE')
                  )
                  AND (COALESCE(ph.purchase_amount, 0) + COALESCE(oh.outsource_amount, 0)
                       - COALESCE(pp.paid_amount, 0)) > 0
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
        List<PartnerPaymentCandidateView> result = new ArrayList<>();
        for (Object[] row : rows) {
            BigDecimal purchaseAmount = toBigDecimal(row[3]);
            BigDecimal outsourceAmount = toBigDecimal(row[4]);
            BigDecimal paidAmount = toBigDecimal(row[5]);
            BigDecimal totalPayable = purchaseAmount.add(outsourceAmount);
            BigDecimal unpaidAmount = totalPayable.subtract(paidAmount).max(BigDecimal.ZERO);
            result.add(new PartnerPaymentCandidateView(
                    ((Number) row[0]).longValue(),
                    row[1].toString(),
                    row[2] != null ? row[2].toString() : "",
                    purchaseAmount,
                    outsourceAmount,
                    totalPayable,
                    paidAmount,
                    unpaidAmount,
                    unpaidAmount.compareTo(BigDecimal.ZERO) > 0
            ));
        }
        return result;
    }

    @Override
    @Transactional
    public PartnerPaymentView save(PartnerPaymentSaveCommand command, String actorUserId) {
        Instant now = Instant.now();
        PartnerPaymentJpaEntity entity = new PartnerPaymentJpaEntity();
        entity.setPaymentNo(command.paymentNo());
        entity.setPartnerId(command.partnerId());
        entity.setPaymentDate(command.paymentDate());
        entity.setCostCategory(command.costCategory());
        entity.setSupplyAmount(command.supplyAmount());
        entity.setVatAmount(command.vatAmount());
        entity.setTotalAmount(command.totalAmount());
        entity.setPaymentMethod(command.paymentMethod());
        entity.setRemark(command.remark());
        entity.setStatus(PartnerPaymentStatus.ISSUED);
        entity.setRecordingState(ACTIVE);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        return toView(paymentRepository.save(entity));
    }

    @Override
    @Transactional(readOnly = true)
    public List<PartnerPaymentView> findAllActive(PartnerPaymentListCriteria criteria) {
        List<PartnerPaymentJpaEntity> rows;
        if (criteria == null) {
            rows = paymentRepository.searchActive(
                    ACTIVE, null, null, null, null, true, PartnerPaymentStatus.CANCELLED);
        } else {
            rows = paymentRepository.searchActive(
                    ACTIVE,
                    criteria.paymentDateFrom(),
                    criteria.paymentDateTo(),
                    normalize(criteria.paymentNo()),
                    criteria.status(),
                    criteria.excludeCancelled(),
                    PartnerPaymentStatus.CANCELLED
            );
        }
        return rows.stream()
                .map(this::toView)
                .filter(view -> matchesPartnerName(view, criteria))
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<PartnerPaymentView> findActiveIssuedById(long id) {
        return paymentRepository.findByIdAndRecordingStateAndStatus(id, ACTIVE, PartnerPaymentStatus.ISSUED)
                .map(this::toView);
    }

    @Override
    @Transactional
    public void cancelById(long id, String actorUserId) {
        PartnerPaymentJpaEntity entity = paymentRepository
                .findByIdAndRecordingStateAndStatus(id, ACTIVE, PartnerPaymentStatus.ISSUED)
                .orElseThrow(() -> new IllegalArgumentException("지급을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setStatus(PartnerPaymentStatus.CANCELLED);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        paymentRepository.save(entity);
    }

    private BigDecimal sumPurchaseHistoryAmount(long partnerId) {
        String sql = """
                SELECT COALESCE(SUM(amount), 0)
                FROM purchase_history
                WHERE recording_state = 1 AND company_id = :partnerId
                """;
        Query query = entityManager.createNativeQuery(sql);
        query.setParameter("partnerId", partnerId);
        return toBigDecimal(query.getSingleResult());
    }

    private BigDecimal sumOutsourceHistoryAmount(long partnerId) {
        String sql = """
                SELECT COALESCE(SUM(amount), 0)
                FROM outsource_history
                WHERE recording_state = 1 AND company_id = :partnerId
                """;
        Query query = entityManager.createNativeQuery(sql);
        query.setParameter("partnerId", partnerId);
        return toBigDecimal(query.getSingleResult());
    }

    private PartnerPaymentView toView(PartnerPaymentJpaEntity entity) {
        CompanyJpaEntity partner = companyRepository.findById(entity.getPartnerId()).orElse(null);
        return new PartnerPaymentView(
                entity.getId(),
                entity.getPaymentNo(),
                entity.getPartnerId(),
                partner != null ? partner.getCompanyName() : "",
                partner != null ? partner.getBusinessRegNo() : "",
                entity.getPaymentDate(),
                entity.getCostCategory(),
                entity.getSupplyAmount(),
                entity.getVatAmount(),
                entity.getTotalAmount(),
                entity.getPaymentMethod(),
                entity.getRemark(),
                entity.getStatus(),
                entity.getCreatedAt(),
                entity.getCreatedBy(),
                entity.getStatus() == PartnerPaymentStatus.ISSUED
        );
    }

    private boolean matchesPartnerName(PartnerPaymentView view, PartnerPaymentListCriteria criteria) {
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
