package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.application.purchase.FifoPrepaidLineView;
import com.shindong.smartmanager.application.purchase.PartnerPaymentCandidateCriteria;
import com.shindong.smartmanager.application.purchase.PartnerPaymentCandidateView;
import com.shindong.smartmanager.application.purchase.PartnerPaymentLineCommand;
import com.shindong.smartmanager.application.purchase.PartnerPaymentLineView;
import com.shindong.smartmanager.application.purchase.PartnerPaymentListCriteria;
import com.shindong.smartmanager.application.purchase.PartnerPaymentRepository;
import com.shindong.smartmanager.application.purchase.PartnerPaymentSaveCommand;
import com.shindong.smartmanager.application.purchase.PartnerPaymentView;
import com.shindong.smartmanager.application.purchase.PartnerPrepaidOffsetSaveCommand;
import com.shindong.smartmanager.application.purchase.PrepaidBalanceView;
import com.shindong.smartmanager.application.purchase.PrepaidOrderLineCandidateCriteria;
import com.shindong.smartmanager.application.purchase.PrepaidOrderLineCandidateView;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentCostCategory;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentKind;
import com.shindong.smartmanager.domain.purchase.PartnerPaymentStatus;
import com.shindong.smartmanager.domain.purchase.PartnerPrepaidOffsetLedgerKind;
import com.shindong.smartmanager.infrastructure.persistence.company.CompanyJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.company.SpringDataCompanyRepository;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import jakarta.persistence.EntityManager;
import jakarta.persistence.Query;
import java.math.BigDecimal;
import java.time.Instant;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.stream.Collectors;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaPartnerPaymentRepository implements PartnerPaymentRepository {

    private static final int ACTIVE = 1;

    private final EntityManager entityManager;
    private final SpringDataPartnerPaymentRepository paymentRepository;
    private final SpringDataPartnerPaymentLineRepository paymentLineRepository;
    private final SpringDataPartnerPrepaidOffsetRepository prepaidOffsetRepository;
    private final SpringDataCompanyRepository companyRepository;
    private final SpringDataItemRepository itemRepository;

    public JpaPartnerPaymentRepository(
            EntityManager entityManager,
            SpringDataPartnerPaymentRepository paymentRepository,
            SpringDataPartnerPaymentLineRepository paymentLineRepository,
            SpringDataPartnerPrepaidOffsetRepository prepaidOffsetRepository,
            SpringDataCompanyRepository companyRepository,
            SpringDataItemRepository itemRepository
    ) {
        this.entityManager = entityManager;
        this.paymentRepository = paymentRepository;
        this.paymentLineRepository = paymentLineRepository;
        this.prepaidOffsetRepository = prepaidOffsetRepository;
        this.companyRepository = companyRepository;
        this.itemRepository = itemRepository;
    }

    @Override
    public long countByPaymentNoPrefix(String prefix) {
        return paymentRepository.countByPaymentNoStartingWithAndRecordingState(prefix, ACTIVE);
    }

    @Override
    @Transactional(readOnly = true)
    public BigDecimal sumIssuedPayableAmountByPartnerId(long partnerId) {
        return sumPurchaseHistoryAmount(partnerId)
                .add(sumOutsourceHistoryAmount(partnerId))
                .subtract(sumApprovedEtcClaimAmount(partnerId));
    }

    @Override
    @Transactional(readOnly = true)
    public BigDecimal sumIssuedPaymentAmountByPartnerId(long partnerId) {
        BigDecimal sum = paymentRepository.sumIssuedSupplyAmountByPartnerId(
                partnerId, ACTIVE, PartnerPaymentStatus.ISSUED);
        return sum != null ? sum : BigDecimal.ZERO;
    }

    @Override
    @Transactional(readOnly = true)
    public BigDecimal sumPrepaidRemainingByPartnerId(long partnerId) {
        String sql = """
                SELECT COALESCE(SUM(line_remaining), 0)
                FROM (
                    SELECT ppl.supply_amount - COALESCE(off.offset_amount, 0) AS line_remaining
                    FROM partner_payment_line ppl
                    JOIN partner_payment pp ON pp.id = ppl.payment_id
                    LEFT JOIN (
                        SELECT payment_line_id, SUM(amount) AS offset_amount
                        FROM partner_prepaid_offset
                        WHERE recording_state = 1
                        GROUP BY payment_line_id
                    ) off ON off.payment_line_id = ppl.id
                    WHERE ppl.recording_state = 1
                      AND pp.recording_state = 1
                      AND pp.status = 'ISSUED'
                      AND pp.payment_kind = 'PREPAID'
                      AND pp.partner_id = :partnerId
                ) t
                WHERE line_remaining > 0
                """;
        Query query = entityManager.createNativeQuery(sql);
        query.setParameter("partnerId", partnerId);
        return toBigDecimal(query.getSingleResult());
    }

    @Override
    @Transactional(readOnly = true)
    public BigDecimal unpaidPartnerAmount(long partnerId) {
        BigDecimal payable = sumIssuedPayableAmountByPartnerId(partnerId);
        BigDecimal paid = sumIssuedPaymentAmountByPartnerId(partnerId);
        BigDecimal prepaidRemaining = sumPrepaidRemainingByPartnerId(partnerId);
        return payable.subtract(paid).add(prepaidRemaining).max(BigDecimal.ZERO);
    }

    @Override
    @Transactional(readOnly = true)
    public BigDecimal sumActiveOffsetByHistory(PartnerPrepaidOffsetLedgerKind ledgerKind, long historyId) {
        BigDecimal sum = prepaidOffsetRepository.sumActiveOffsetByHistory(ledgerKind, historyId, ACTIVE);
        return sum != null ? sum : BigDecimal.ZERO;
    }

    @Override
    @Transactional(readOnly = true)
    public boolean hasActiveOffsetByPaymentId(long paymentId) {
        BigDecimal sum = paymentLineRepository.sumActiveOffsetByPaymentId(paymentId, ACTIVE);
        return sum != null && sum.compareTo(BigDecimal.ZERO) > 0;
    }

    @Override
    @Transactional(readOnly = true)
    public List<PartnerPaymentCandidateView> findCandidates(PartnerPaymentCandidateCriteria criteria) {
        boolean includeZero = criteria != null && criteria.includeZeroUnpaid();
        StringBuilder sql = new StringBuilder("""
                SELECT c.id, c.company_name, c.business_reg_no,
                       COALESCE(ph.purchase_amount, 0) AS purchase_amount,
                       COALESCE(oh.outsource_amount, 0) AS outsource_amount,
                       COALESCE(ec.claim_amount, 0) AS claim_amount,
                       COALESCE(pp.paid_amount, 0) AS paid_amount,
                       COALESCE(pr.prepaid_remaining, 0) AS prepaid_remaining
                FROM company c
                LEFT JOIN (
                    SELECT company_id, SUM(amount) AS purchase_amount
                    FROM purchase_history
                    WHERE recording_state = 1 AND approval_status = 'APPROVED'
                    GROUP BY company_id
                ) ph ON ph.company_id = c.id
                LEFT JOIN (
                    SELECT company_id, SUM(amount) AS outsource_amount
                    FROM outsource_history
                    WHERE recording_state = 1 AND approval_status = 'APPROVED'
                    GROUP BY company_id
                ) oh ON oh.company_id = c.id
                LEFT JOIN (
                    SELECT partner_id, SUM(amount) AS claim_amount
                    FROM (
                        SELECT partner_id, amount
                        FROM etc_claim
                        WHERE recording_state = 1 AND recognition = 'APPROVED'
                        UNION ALL
                        SELECT partner_id, amount
                        FROM defect_claim
                        WHERE recording_state = 1 AND recognition = 'APPROVED'
                    ) claims
                    GROUP BY partner_id
                ) ec ON ec.partner_id = c.id
                LEFT JOIN (
                    SELECT partner_id, SUM(supply_amount) AS paid_amount
                    FROM partner_payment
                    WHERE recording_state = 1 AND status = 'ISSUED'
                    GROUP BY partner_id
                ) pp ON pp.partner_id = c.id
                LEFT JOIN (
                    SELECT partner_id, SUM(line_remaining) AS prepaid_remaining
                    FROM (
                        SELECT pp.partner_id,
                               ppl.supply_amount - COALESCE(off.offset_amount, 0) AS line_remaining
                        FROM partner_payment_line ppl
                        JOIN partner_payment pp ON pp.id = ppl.payment_id
                        LEFT JOIN (
                            SELECT payment_line_id, SUM(amount) AS offset_amount
                            FROM partner_prepaid_offset
                            WHERE recording_state = 1
                            GROUP BY payment_line_id
                        ) off ON off.payment_line_id = ppl.id
                        WHERE ppl.recording_state = 1
                          AND pp.recording_state = 1
                          AND pp.status = 'ISSUED'
                          AND pp.payment_kind = 'PREPAID'
                    ) t
                    WHERE line_remaining > 0
                    GROUP BY partner_id
                ) pr ON pr.partner_id = c.id
                WHERE c.recording_state = 1
                  AND EXISTS (
                    SELECT 1 FROM company_role cr
                    WHERE cr.company_id = c.id AND cr.role_type IN ('PURCHASE', 'OUTSOURCE')
                  )
                """);
        if (!includeZero) {
            sql.append("""
                      AND (COALESCE(ph.purchase_amount, 0) + COALESCE(oh.outsource_amount, 0)
                           - COALESCE(ec.claim_amount, 0) - COALESCE(pp.paid_amount, 0)
                           + COALESCE(pr.prepaid_remaining, 0)) > 0
                    """);
        }
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
            BigDecimal claimAmount = toBigDecimal(row[5]);
            BigDecimal paidAmount = toBigDecimal(row[6]);
            BigDecimal prepaidRemaining = toBigDecimal(row[7]);
            BigDecimal totalPayable = purchaseAmount.add(outsourceAmount).subtract(claimAmount);
            BigDecimal unpaidAmount = totalPayable.subtract(paidAmount).add(prepaidRemaining).max(BigDecimal.ZERO);
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
    @Transactional(readOnly = true)
    public List<PrepaidBalanceView> findPrepaidBalances(Long partnerId, PartnerPaymentCostCategory costCategory) {
        StringBuilder sql = new StringBuilder("""
                SELECT pp.partner_id, c.company_name, ppl.item_id, i.item_no, i.item_name,
                       pp.cost_category,
                       SUM(ppl.supply_amount) AS prepaid_in,
                       SUM(COALESCE(off.offset_amount, 0)) AS prepaid_out,
                       SUM(ppl.supply_amount - COALESCE(off.offset_amount, 0)) AS prepaid_remaining
                FROM partner_payment_line ppl
                JOIN partner_payment pp ON pp.id = ppl.payment_id
                JOIN company c ON c.id = pp.partner_id
                JOIN item i ON i.id = ppl.item_id
                LEFT JOIN (
                    SELECT payment_line_id, SUM(amount) AS offset_amount
                    FROM partner_prepaid_offset
                    WHERE recording_state = 1
                    GROUP BY payment_line_id
                ) off ON off.payment_line_id = ppl.id
                WHERE ppl.recording_state = 1
                  AND pp.recording_state = 1
                  AND pp.status = 'ISSUED'
                  AND pp.payment_kind = 'PREPAID'
                """);
        Map<String, Object> params = new HashMap<>();
        if (partnerId != null) {
            sql.append(" AND pp.partner_id = :partnerId");
            params.put("partnerId", partnerId);
        }
        if (costCategory != null) {
            sql.append(" AND pp.cost_category = :costCategory");
            params.put("costCategory", costCategory.name());
        }
        sql.append("""
                GROUP BY pp.partner_id, c.company_name, ppl.item_id, i.item_no, i.item_name, pp.cost_category
                HAVING prepaid_remaining > 0
                ORDER BY c.company_name, i.item_no, pp.cost_category
                """);
        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<PrepaidBalanceView> result = new ArrayList<>();
        for (Object[] row : rows) {
            result.add(new PrepaidBalanceView(
                    ((Number) row[0]).longValue(),
                    row[1].toString(),
                    ((Number) row[2]).longValue(),
                    row[3] != null ? row[3].toString() : "",
                    row[4] != null ? row[4].toString() : "",
                    PartnerPaymentCostCategory.valueOf(row[5].toString()),
                    toBigDecimal(row[6]),
                    toBigDecimal(row[7]),
                    toBigDecimal(row[8])
            ));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public List<PrepaidOrderLineCandidateView> findOrderLineCandidates(PrepaidOrderLineCandidateCriteria criteria) {
        List<PrepaidOrderLineCandidateView> result = new ArrayList<>();
        PartnerPaymentCostCategory category = criteria != null ? criteria.costCategory() : null;
        if (category == null || category == PartnerPaymentCostCategory.PURCHASE) {
            result.addAll(findPurchaseOrderLineCandidates(criteria));
        }
        if (category == null || category == PartnerPaymentCostCategory.OUTSOURCE) {
            result.addAll(findOutsourceOrderLineCandidates(criteria));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public BigDecimal remainingOrderLineAmount(PartnerPaymentCostCategory costCategory, long orderLineId) {
        if (costCategory == PartnerPaymentCostCategory.PURCHASE) {
            return remainingPurchaseOrderLineAmount(orderLineId);
        }
        return remainingOutsourceOrderLineAmount(orderLineId);
    }

    @Override
    @Transactional(readOnly = true)
    public List<FifoPrepaidLineView> findFifoPrepaidLines(
            long partnerId,
            long itemId,
            PartnerPaymentCostCategory costCategory
    ) {
        String sql = """
                SELECT ppl.id, pp.id, pp.payment_no, pp.payment_date, pp.partner_id, ppl.item_id,
                       pp.cost_category, ppl.supply_amount, COALESCE(off.offset_amount, 0) AS offset_amount
                FROM partner_payment_line ppl
                JOIN partner_payment pp ON pp.id = ppl.payment_id
                LEFT JOIN (
                    SELECT payment_line_id, SUM(amount) AS offset_amount
                    FROM partner_prepaid_offset
                    WHERE recording_state = 1
                    GROUP BY payment_line_id
                ) off ON off.payment_line_id = ppl.id
                WHERE ppl.recording_state = 1
                  AND pp.recording_state = 1
                  AND pp.status = 'ISSUED'
                  AND pp.payment_kind = 'PREPAID'
                  AND pp.partner_id = :partnerId
                  AND ppl.item_id = :itemId
                  AND pp.cost_category = :costCategory
                  AND (ppl.supply_amount - COALESCE(off.offset_amount, 0)) > 0
                ORDER BY pp.payment_date ASC, pp.payment_no ASC, ppl.id ASC
                """;
        Query query = entityManager.createNativeQuery(sql);
        query.setParameter("partnerId", partnerId);
        query.setParameter("itemId", itemId);
        query.setParameter("costCategory", costCategory.name());
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<FifoPrepaidLineView> result = new ArrayList<>();
        for (Object[] row : rows) {
            BigDecimal total = toBigDecimal(row[7]);
            BigDecimal offset = toBigDecimal(row[8]);
            result.add(new FifoPrepaidLineView(
                    ((Number) row[0]).longValue(),
                    ((Number) row[1]).longValue(),
                    row[2].toString(),
                    toLocalDate(row[3]),
                    ((Number) row[4]).longValue(),
                    ((Number) row[5]).longValue(),
                    PartnerPaymentCostCategory.valueOf(row[6].toString()),
                    total,
                    offset,
                    total.subtract(offset)
            ));
        }
        return result;
    }

    @Override
    @Transactional
    public void saveOffsets(List<PartnerPrepaidOffsetSaveCommand> offsets, String actorUserId) {
        if (offsets == null || offsets.isEmpty()) {
            return;
        }
        Instant now = Instant.now();
        for (PartnerPrepaidOffsetSaveCommand command : offsets) {
            PartnerPrepaidOffsetJpaEntity entity = new PartnerPrepaidOffsetJpaEntity();
            entity.setPaymentLineId(command.paymentLineId());
            entity.setLedgerKind(command.ledgerKind());
            entity.setHistoryId(command.historyId());
            entity.setAmount(command.amount());
            entity.setRecordingState(ACTIVE);
            entity.setCreatedBy(actorUserId);
            entity.setCreatedById(actorUserId);
            entity.setCreatedAt(now);
            entity.setUpdatedBy(actorUserId);
            entity.setUpdatedById(actorUserId);
            entity.setUpdatedAt(now);
            prepaidOffsetRepository.save(entity);
        }
    }

    @Override
    @Transactional
    public void deactivateOffsetsByHistory(
            PartnerPrepaidOffsetLedgerKind ledgerKind,
            long historyId,
            String actorUserId
    ) {
        Instant now = Instant.now();
        List<PartnerPrepaidOffsetJpaEntity> rows = prepaidOffsetRepository
                .findByLedgerKindAndHistoryIdAndRecordingState(ledgerKind, historyId, ACTIVE);
        for (PartnerPrepaidOffsetJpaEntity row : rows) {
            row.setRecordingState(0);
            row.setUpdatedBy(actorUserId);
            row.setUpdatedById(actorUserId);
            row.setUpdatedAt(now);
            prepaidOffsetRepository.save(row);
        }
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
        entity.setPaymentKind(command.paymentKind() != null ? command.paymentKind() : PartnerPaymentKind.NORMAL);
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
        PartnerPaymentJpaEntity saved = paymentRepository.save(entity);

        if (command.lines() != null) {
            for (PartnerPaymentLineCommand line : command.lines()) {
                BigDecimal supply = line.supplyAmount();
                BigDecimal vat = line.vatAmount() != null ? line.vatAmount() : BigDecimal.ZERO;
                PartnerPaymentLineJpaEntity lineEntity = new PartnerPaymentLineJpaEntity();
                lineEntity.setPaymentId(saved.getId());
                lineEntity.setItemId(line.itemId());
                lineEntity.setPurchaseOrderLineId(line.purchaseOrderLineId());
                lineEntity.setOutsourcingOrderLineId(line.outsourcingOrderLineId());
                lineEntity.setSupplyAmount(supply);
                lineEntity.setVatAmount(vat);
                lineEntity.setTotalAmount(supply.add(vat));
                lineEntity.setRecordingState(ACTIVE);
                lineEntity.setCreatedBy(actorUserId);
                lineEntity.setCreatedById(actorUserId);
                lineEntity.setCreatedAt(now);
                lineEntity.setUpdatedBy(actorUserId);
                lineEntity.setUpdatedById(actorUserId);
                lineEntity.setUpdatedAt(now);
                paymentLineRepository.save(lineEntity);
            }
        }
        return toView(saved);
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

    private List<PrepaidOrderLineCandidateView> findPurchaseOrderLineCandidates(
            PrepaidOrderLineCandidateCriteria criteria
    ) {
        StringBuilder sql = new StringBuilder("""
                SELECT pol.id, po.id, po.order_no, pol.line_no, po.partner_id, c.company_name,
                       pol.item_id, i.item_no, i.item_name, po.order_date, pol.amount,
                       COALESCE(linked.prepaid_amount, 0) AS prepaid_amount
                FROM purchase_order_line pol
                JOIN purchase_order po ON po.id = pol.purchase_order_id
                JOIN company c ON c.id = po.partner_id
                JOIN item i ON i.id = pol.item_id
                LEFT JOIN (
                    SELECT ppl.purchase_order_line_id, SUM(ppl.supply_amount) AS prepaid_amount
                    FROM partner_payment_line ppl
                    JOIN partner_payment pp ON pp.id = ppl.payment_id
                    WHERE ppl.recording_state = 1
                      AND pp.recording_state = 1
                      AND pp.status = 'ISSUED'
                      AND ppl.purchase_order_line_id IS NOT NULL
                    GROUP BY ppl.purchase_order_line_id
                ) linked ON linked.purchase_order_line_id = pol.id
                WHERE pol.recording_state = 1
                  AND po.recording_state = 1
                  AND po.status = 'CONFIRMED'
                  AND (pol.amount - COALESCE(linked.prepaid_amount, 0)) > 0
                """);
        Map<String, Object> params = new HashMap<>();
        appendOrderLineFilters(sql, params, criteria, true);
        sql.append(" ORDER BY po.order_date DESC, po.order_no, pol.line_no");
        return mapOrderLineRows(sql.toString(), params, PartnerPaymentCostCategory.PURCHASE);
    }

    private List<PrepaidOrderLineCandidateView> findOutsourceOrderLineCandidates(
            PrepaidOrderLineCandidateCriteria criteria
    ) {
        StringBuilder sql = new StringBuilder("""
                SELECT ool.id, oo.id, oo.order_no, ool.line_no, oo.partner_id, c.company_name,
                       ool.item_id, i.item_no, i.item_name, oo.order_date, ool.amount,
                       COALESCE(linked.prepaid_amount, 0) AS prepaid_amount
                FROM outsourcing_order_line ool
                JOIN outsourcing_order oo ON oo.id = ool.outsourcing_order_id
                JOIN company c ON c.id = oo.partner_id
                JOIN item i ON i.id = ool.item_id
                LEFT JOIN (
                    SELECT ppl.outsourcing_order_line_id, SUM(ppl.supply_amount) AS prepaid_amount
                    FROM partner_payment_line ppl
                    JOIN partner_payment pp ON pp.id = ppl.payment_id
                    WHERE ppl.recording_state = 1
                      AND pp.recording_state = 1
                      AND pp.status = 'ISSUED'
                      AND ppl.outsourcing_order_line_id IS NOT NULL
                    GROUP BY ppl.outsourcing_order_line_id
                ) linked ON linked.outsourcing_order_line_id = ool.id
                WHERE ool.recording_state = 1
                  AND oo.recording_state = 1
                  AND oo.status <> 'CANCELLED'
                  AND (ool.amount - COALESCE(linked.prepaid_amount, 0)) > 0
                """);
        Map<String, Object> params = new HashMap<>();
        appendOrderLineFilters(sql, params, criteria, false);
        sql.append(" ORDER BY oo.order_date DESC, oo.order_no, ool.line_no");
        return mapOrderLineRows(sql.toString(), params, PartnerPaymentCostCategory.OUTSOURCE);
    }

    private void appendOrderLineFilters(
            StringBuilder sql,
            Map<String, Object> params,
            PrepaidOrderLineCandidateCriteria criteria,
            boolean purchase
    ) {
        if (criteria == null) {
            return;
        }
        if (criteria.partnerId() != null) {
            sql.append(purchase ? " AND po.partner_id = :partnerId" : " AND oo.partner_id = :partnerId");
            params.put("partnerId", criteria.partnerId());
        }
        if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
            sql.append(" AND i.item_no LIKE :itemNo");
            params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
        }
        if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
            sql.append(" AND i.item_name LIKE :itemName");
            params.put("itemName", "%" + criteria.itemName().trim() + "%");
        }
        if (criteria.orderNo() != null && !criteria.orderNo().isBlank()) {
            sql.append(purchase ? " AND po.order_no LIKE :orderNo" : " AND oo.order_no LIKE :orderNo");
            params.put("orderNo", "%" + criteria.orderNo().trim() + "%");
        }
    }

    private List<PrepaidOrderLineCandidateView> mapOrderLineRows(
            String sql,
            Map<String, Object> params,
            PartnerPaymentCostCategory costCategory
    ) {
        Query query = entityManager.createNativeQuery(sql);
        params.forEach(query::setParameter);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<PrepaidOrderLineCandidateView> result = new ArrayList<>();
        for (Object[] row : rows) {
            BigDecimal orderAmount = toBigDecimal(row[10]);
            BigDecimal prepaid = toBigDecimal(row[11]);
            result.add(new PrepaidOrderLineCandidateView(
                    costCategory,
                    ((Number) row[0]).longValue(),
                    ((Number) row[1]).longValue(),
                    row[2].toString(),
                    ((Number) row[3]).shortValue(),
                    ((Number) row[4]).longValue(),
                    row[5].toString(),
                    ((Number) row[6]).longValue(),
                    row[7] != null ? row[7].toString() : "",
                    row[8] != null ? row[8].toString() : "",
                    toLocalDate(row[9]),
                    orderAmount,
                    prepaid,
                    orderAmount.subtract(prepaid).max(BigDecimal.ZERO)
            ));
        }
        return result;
    }

    private BigDecimal remainingPurchaseOrderLineAmount(long orderLineId) {
        String sql = """
                SELECT pol.amount - COALESCE((
                    SELECT SUM(ppl.supply_amount)
                    FROM partner_payment_line ppl
                    JOIN partner_payment pp ON pp.id = ppl.payment_id
                    WHERE ppl.recording_state = 1
                      AND pp.recording_state = 1
                      AND pp.status = 'ISSUED'
                      AND ppl.purchase_order_line_id = pol.id
                ), 0)
                FROM purchase_order_line pol
                JOIN purchase_order po ON po.id = pol.purchase_order_id
                WHERE pol.id = :lineId AND pol.recording_state = 1 AND po.recording_state = 1
                """;
        Query query = entityManager.createNativeQuery(sql);
        query.setParameter("lineId", orderLineId);
        Object result = query.getSingleResult();
        if (result == null) {
            throw new IllegalArgumentException("구매 발주 라인을 찾을 수 없습니다: " + orderLineId);
        }
        return toBigDecimal(result).max(BigDecimal.ZERO);
    }

    private BigDecimal remainingOutsourceOrderLineAmount(long orderLineId) {
        String sql = """
                SELECT ool.amount - COALESCE((
                    SELECT SUM(ppl.supply_amount)
                    FROM partner_payment_line ppl
                    JOIN partner_payment pp ON pp.id = ppl.payment_id
                    WHERE ppl.recording_state = 1
                      AND pp.recording_state = 1
                      AND pp.status = 'ISSUED'
                      AND ppl.outsourcing_order_line_id = ool.id
                ), 0)
                FROM outsourcing_order_line ool
                JOIN outsourcing_order oo ON oo.id = ool.outsourcing_order_id
                WHERE ool.id = :lineId AND ool.recording_state = 1 AND oo.recording_state = 1
                """;
        Query query = entityManager.createNativeQuery(sql);
        query.setParameter("lineId", orderLineId);
        Object result = query.getSingleResult();
        if (result == null) {
            throw new IllegalArgumentException("외주 발주 라인을 찾을 수 없습니다: " + orderLineId);
        }
        return toBigDecimal(result).max(BigDecimal.ZERO);
    }

    private BigDecimal sumPurchaseHistoryAmount(long partnerId) {
        String sql = """
                SELECT COALESCE(SUM(amount), 0)
                FROM purchase_history
                WHERE recording_state = 1 AND approval_status = 'APPROVED' AND company_id = :partnerId
                """;
        Query query = entityManager.createNativeQuery(sql);
        query.setParameter("partnerId", partnerId);
        return toBigDecimal(query.getSingleResult());
    }

    private BigDecimal sumOutsourceHistoryAmount(long partnerId) {
        String sql = """
                SELECT COALESCE(SUM(amount), 0)
                FROM outsource_history
                WHERE recording_state = 1 AND approval_status = 'APPROVED' AND company_id = :partnerId
                """;
        Query query = entityManager.createNativeQuery(sql);
        query.setParameter("partnerId", partnerId);
        return toBigDecimal(query.getSingleResult());
    }

    private BigDecimal sumApprovedEtcClaimAmount(long partnerId) {
        String sql = """
                SELECT COALESCE(SUM(amount), 0)
                FROM (
                    SELECT amount
                    FROM etc_claim
                    WHERE recording_state = 1 AND recognition = 'APPROVED' AND partner_id = :partnerId
                    UNION ALL
                    SELECT amount
                    FROM defect_claim
                    WHERE recording_state = 1 AND recognition = 'APPROVED' AND partner_id = :partnerId
                ) claims
                """;
        Query query = entityManager.createNativeQuery(sql);
        query.setParameter("partnerId", partnerId);
        return toBigDecimal(query.getSingleResult());
    }

    private PartnerPaymentView toView(PartnerPaymentJpaEntity entity) {
        CompanyJpaEntity partner = companyRepository.findById(entity.getPartnerId()).orElse(null);
        List<PartnerPaymentLineJpaEntity> lineEntities =
                paymentLineRepository.findByPaymentIdAndRecordingStateOrderByIdAsc(entity.getId(), ACTIVE);
        List<PartnerPaymentLineView> lines = toLineViews(lineEntities);
        String lineSummary = lines.stream()
                .map(line -> line.itemNo() + (line.orderNo() != null ? "(" + line.orderNo() + ")" : ""))
                .collect(Collectors.joining(", "));
        boolean cancelable = entity.getStatus() == PartnerPaymentStatus.ISSUED
                && !hasActiveOffsetByPaymentId(entity.getId());
        PartnerPaymentKind kind = entity.getPaymentKind() != null
                ? entity.getPaymentKind()
                : PartnerPaymentKind.NORMAL;
        return new PartnerPaymentView(
                entity.getId(),
                entity.getPaymentNo(),
                entity.getPartnerId(),
                partner != null ? partner.getCompanyName() : "",
                partner != null ? partner.getBusinessRegNo() : "",
                entity.getPaymentDate(),
                entity.getCostCategory(),
                kind,
                entity.getSupplyAmount(),
                entity.getVatAmount(),
                entity.getTotalAmount(),
                entity.getPaymentMethod(),
                entity.getRemark(),
                entity.getStatus(),
                entity.getCreatedAt(),
                entity.getCreatedBy(),
                cancelable,
                lines,
                lineSummary.isBlank() ? null : lineSummary
        );
    }

    private List<PartnerPaymentLineView> toLineViews(List<PartnerPaymentLineJpaEntity> lineEntities) {
        if (lineEntities.isEmpty()) {
            return List.of();
        }
        List<PartnerPaymentLineView> lines = new ArrayList<>();
        for (PartnerPaymentLineJpaEntity line : lineEntities) {
            ItemJpaEntity item = itemRepository.findById(line.getItemId()).orElse(null);
            BigDecimal offset = paymentLineRepository.sumActiveOffsetByLineId(line.getId(), ACTIVE);
            if (offset == null) {
                offset = BigDecimal.ZERO;
            }
            String orderNo = resolveOrderNo(line);
            lines.add(new PartnerPaymentLineView(
                    line.getId(),
                    line.getItemId(),
                    item != null ? item.getItemNo() : "",
                    item != null ? item.getItemName() : "",
                    line.getPurchaseOrderLineId(),
                    line.getOutsourcingOrderLineId(),
                    orderNo,
                    line.getSupplyAmount(),
                    line.getVatAmount(),
                    line.getTotalAmount(),
                    offset,
                    line.getTotalAmount().subtract(offset).max(BigDecimal.ZERO)
            ));
        }
        return lines;
    }

    private String resolveOrderNo(PartnerPaymentLineJpaEntity line) {
        if (line.getPurchaseOrderLineId() != null) {
            String sql = """
                    SELECT po.order_no
                    FROM purchase_order_line pol
                    JOIN purchase_order po ON po.id = pol.purchase_order_id
                    WHERE pol.id = :lineId
                    """;
            Query query = entityManager.createNativeQuery(sql);
            query.setParameter("lineId", line.getPurchaseOrderLineId());
            @SuppressWarnings("unchecked")
            List<Object> rows = query.getResultList();
            return rows.isEmpty() || rows.get(0) == null ? null : rows.get(0).toString();
        }
        if (line.getOutsourcingOrderLineId() != null) {
            String sql = """
                    SELECT oo.order_no
                    FROM outsourcing_order_line ool
                    JOIN outsourcing_order oo ON oo.id = ool.outsourcing_order_id
                    WHERE ool.id = :lineId
                    """;
            Query query = entityManager.createNativeQuery(sql);
            query.setParameter("lineId", line.getOutsourcingOrderLineId());
            @SuppressWarnings("unchecked")
            List<Object> rows = query.getResultList();
            return rows.isEmpty() || rows.get(0) == null ? null : rows.get(0).toString();
        }
        return null;
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

    private static java.time.LocalDate toLocalDate(Object value) {
        if (value == null) {
            return null;
        }
        if (value instanceof java.time.LocalDate localDate) {
            return localDate;
        }
        if (value instanceof java.sql.Date sqlDate) {
            return sqlDate.toLocalDate();
        }
        if (value instanceof java.util.Date utilDate) {
            return new java.sql.Date(utilDate.getTime()).toLocalDate();
        }
        return java.time.LocalDate.parse(value.toString());
    }
}
