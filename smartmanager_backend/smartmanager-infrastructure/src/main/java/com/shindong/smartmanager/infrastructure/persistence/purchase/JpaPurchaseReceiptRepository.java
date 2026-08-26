package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;

import com.shindong.smartmanager.application.purchase.PurchaseOrderLineReceiptContext;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptCandidateCriteria;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptListCriteria;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptCandidateView;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptLineSaveCommand;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptLineView;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptRepository;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptSaveCommand;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptView;
import com.shindong.smartmanager.domain.item.CheckDistinction;
import com.shindong.smartmanager.domain.purchase.PurchaseOrderStatus;
import com.shindong.smartmanager.domain.purchase.PurchaseReceiptStatus;
import com.shindong.smartmanager.infrastructure.persistence.company.CompanyJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.company.SpringDataCompanyRepository;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import jakarta.persistence.EntityManager;
import jakarta.persistence.Query;
import java.math.BigDecimal;
import java.sql.Date;
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
public class JpaPurchaseReceiptRepository implements PurchaseReceiptRepository {

    private static final int ACTIVE = 1;

    private final EntityManager entityManager;
    private final SpringDataPurchaseReceiptRepository receiptRepository;
    private final SpringDataPurchaseReceiptLineRepository receiptLineRepository;
    private final SpringDataPurchaseOrderLineRepository orderLineRepository;
    private final SpringDataPurchaseOrderRepository orderRepository;
    private final SpringDataCompanyRepository companyRepository;
    private final SpringDataItemRepository itemRepository;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaPurchaseReceiptRepository(
            EntityManager entityManager,
            SpringDataPurchaseReceiptRepository receiptRepository,
            SpringDataPurchaseReceiptLineRepository receiptLineRepository,
            SpringDataPurchaseOrderLineRepository orderLineRepository,
            SpringDataPurchaseOrderRepository orderRepository,
            SpringDataCompanyRepository companyRepository,
            SpringDataItemRepository itemRepository,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.entityManager = entityManager;
        this.receiptRepository = receiptRepository;
        this.receiptLineRepository = receiptLineRepository;
        this.orderLineRepository = orderLineRepository;
        this.orderRepository = orderRepository;
        this.companyRepository = companyRepository;
        this.itemRepository = itemRepository;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    @Transactional(readOnly = true)
    public List<PurchaseReceiptCandidateView> findReceiptCandidates(PurchaseReceiptCandidateCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT po.id, pol.id, po.order_no, po.order_date, po.partner_id, c.company_name,
                       pol.item_id, i.item_no, i.item_name, i.check_distinction, i.lot_tracked,
                       pol.order_qty, pol.received_qty, pol.waiting_inspection_qty,
                       pol.unit_price, pol.requested_delivery_date
                FROM purchase_order_line pol
                JOIN purchase_order po ON po.id = pol.purchase_order_id
                JOIN company c ON c.id = po.partner_id
                JOIN item i ON i.id = pol.item_id
                WHERE pol.recording_state = 1 AND po.recording_state = 1
                  AND po.status <> 'CANCELLED'
                  AND (pol.order_qty - pol.received_qty - pol.waiting_inspection_qty) > 0
                """);
        Map<String, Object> params = new HashMap<>();
        if (criteria != null) {
            if (criteria.partnerName() != null && !criteria.partnerName().isBlank()) {
                sql.append(" AND c.company_name LIKE :partnerName");
                params.put("partnerName", "%" + criteria.partnerName().trim() + "%");
            }
            if (criteria.orderNo() != null && !criteria.orderNo().isBlank()) {
                sql.append(" AND po.order_no LIKE :orderNo");
                params.put("orderNo", "%" + criteria.orderNo().trim() + "%");
            }
            if (criteria.orderDateFrom() != null) {
                sql.append(" AND po.order_date >= :orderDateFrom");
                params.put("orderDateFrom", criteria.orderDateFrom());
            }
            if (criteria.orderDateTo() != null) {
                sql.append(" AND po.order_date <= :orderDateTo");
                params.put("orderDateTo", criteria.orderDateTo());
            }
            if (criteria.itemNum() != null && !criteria.itemNum().isBlank()) {
                sql.append(" AND i.item_no LIKE :itemNum");
                params.put("itemNum", "%" + criteria.itemNum().trim() + "%");
            }
            if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
                sql.append(" AND i.item_name LIKE :itemName");
                params.put("itemName", "%" + criteria.itemName().trim() + "%");
            }
            appendItemPropertyScopeFilter(sql, params, criteria.itemPropertyScope());
        }
        sql.append(" ORDER BY po.order_date DESC, po.order_no, pol.line_no");

        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);

        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<PurchaseReceiptCandidateView> result = new ArrayList<>();
        for (Object[] row : rows) {
            BigDecimal orderQty = toBigDecimal(row[11]);
            BigDecimal receivedQty = toBigDecimal(row[12]);
            BigDecimal waitingQty = toBigDecimal(row[13]);
            BigDecimal remainQty = orderQty.subtract(receivedQty).subtract(waitingQty);
            CheckDistinction checkDistinction = row[9] != null
                    ? CheckDistinction.valueOf(row[9].toString())
                    : CheckDistinction.NONE;
            boolean lotTracked = toBooleanFlag(row[10]);
            result.add(new PurchaseReceiptCandidateView(
                    ((Number) row[0]).longValue(),
                    ((Number) row[1]).longValue(),
                    row[2].toString(),
                    toLocalDate(row[3]),
                    ((Number) row[4]).longValue(),
                    row[5].toString(),
                    ((Number) row[6]).longValue(),
                    row[7].toString(),
                    row[8].toString(),
                    checkDistinction,
                    lotTracked,
                    orderQty,
                    receivedQty,
                    remainQty,
                    waitingQty,
                    toBigDecimal(row[14]),
                    row[15] != null ? toLocalDate(row[15]) : null
            ));
        }
        return result;
    }

    @Override
    public long countByReceiptNoPrefix(String prefix) {
        return receiptRepository.countByReceiptNoStartingWith(prefix);
    }

    @Override
    @Transactional
    public long saveReceipt(PurchaseReceiptSaveCommand command, String actorUserId) {
        Instant now = Instant.now();
        PurchaseReceiptJpaEntity header = new PurchaseReceiptJpaEntity();
        header.setReceiptNo(command.receiptNo());
        header.setPartnerId(command.partnerId());
        header.setReceiptDate(command.receiptDate());
        header.setPurchaseOrderId(command.purchaseOrderId());
        header.setStatus(command.status());
        header.setRecordingState(ACTIVE);
        header.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
        header.setCreatedAt(now);
        header.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        header.setUpdatedAt(now);
        PurchaseReceiptJpaEntity savedHeader = receiptRepository.save(header);

        short lineNo = 1;
        for (PurchaseReceiptLineSaveCommand line : command.lines()) {
            PurchaseReceiptLineJpaEntity entity = new PurchaseReceiptLineJpaEntity();
            entity.setPurchaseReceiptId(savedHeader.getId());
            entity.setLineNo(lineNo++);
            entity.setPurchaseOrderLineId(line.purchaseOrderLineId());
            entity.setItemId(line.itemId());
            entity.setReceiptQty(line.receiptQty());
            entity.setPostedQty(line.postedQty());
            entity.setUnitPrice(line.unitPrice());
            entity.setAmount(line.amount());
            entity.setRecordingState(ACTIVE);
            entity.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
            entity.setCreatedAt(now);
            entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
            entity.setUpdatedAt(now);
            receiptLineRepository.save(entity);
        }
        return savedHeader.getId();
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<PurchaseReceiptView> findActiveById(long id) {
        return receiptRepository.findById(id)
                .filter(entity -> entity.getRecordingState() == ACTIVE)
                .map(this::toView);
    }

    @Override
    @Transactional(readOnly = true)
    public List<PurchaseReceiptView> findAllActive() {
        return receiptRepository.findAll().stream()
                .filter(entity -> entity.getRecordingState() == ACTIVE
                        && entity.getStatus() != PurchaseReceiptStatus.CANCELLED)
                .sorted((a, b) -> b.getReceiptDate().compareTo(a.getReceiptDate()))
                .map(this::toView)
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public List<PurchaseReceiptView> findAllActive(PurchaseReceiptListCriteria criteria) {
        if (criteria == null || isListCriteriaEmpty(criteria)) {
            return findAllActive();
        }

        StringBuilder sql = new StringBuilder("""
                SELECT DISTINCT pr.id
                FROM purchase_receipt pr
                JOIN company c ON c.id = pr.partner_id
                WHERE pr.recording_state = 1
                  AND pr.status <> 'CANCELLED'
                """);
        Map<String, Object> params = new HashMap<>();
        if (criteria.partnerName() != null && !criteria.partnerName().isBlank()) {
            sql.append(" AND c.company_name LIKE :partnerName");
            params.put("partnerName", "%" + criteria.partnerName().trim() + "%");
        }
        if (criteria.receiptDateFrom() != null) {
            sql.append(" AND pr.receipt_date >= :receiptDateFrom");
            params.put("receiptDateFrom", criteria.receiptDateFrom());
        }
        if (criteria.receiptDateTo() != null) {
            sql.append(" AND pr.receipt_date <= :receiptDateTo");
            params.put("receiptDateTo", criteria.receiptDateTo());
        }

        boolean hasItemFilter = (criteria.itemNum() != null && !criteria.itemNum().isBlank())
                || (criteria.itemName() != null && !criteria.itemName().isBlank());
        if (hasItemFilter) {
            sql.append("""
                     AND EXISTS (
                        SELECT 1 FROM purchase_receipt_line prl
                        JOIN item i ON i.id = prl.item_id
                        WHERE prl.purchase_receipt_id = pr.id AND prl.recording_state = 1
                    """);
            if (criteria.itemNum() != null && !criteria.itemNum().isBlank()) {
                sql.append(" AND i.item_no LIKE :itemNum");
                params.put("itemNum", "%" + criteria.itemNum().trim() + "%");
            }
            if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
                sql.append(" AND i.item_name LIKE :itemName");
                params.put("itemName", "%" + criteria.itemName().trim() + "%");
            }
            sql.append(")");
        }
        appendReceiptItemPropertyScopeFilter(sql, params, criteria.itemPropertyScope());
        sql.append(" ORDER BY pr.receipt_date DESC, pr.receipt_no DESC");

        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);

        @SuppressWarnings("unchecked")
        List<Number> rows = query.getResultList();
        List<PurchaseReceiptView> result = new ArrayList<>();
        for (Number row : rows) {
            findActiveById(row.longValue()).ifPresent(result::add);
        }
        return result;
    }

    private static boolean isListCriteriaEmpty(PurchaseReceiptListCriteria criteria) {
        return (criteria.partnerName() == null || criteria.partnerName().isBlank())
                && criteria.receiptDateFrom() == null
                && criteria.receiptDateTo() == null
                && (criteria.itemNum() == null || criteria.itemNum().isBlank())
                && (criteria.itemName() == null || criteria.itemName().isBlank())
                && (criteria.itemPropertyScope() == null || criteria.itemPropertyScope().isBlank());
    }

    private static void appendItemPropertyScopeFilter(
            StringBuilder sql,
            Map<String, Object> params,
            String itemPropertyScope
    ) {
        if (itemPropertyScope == null || itemPropertyScope.isBlank()) {
            return;
        }
        String scope = itemPropertyScope.trim();
        if ("SUB_MATERIAL".equals(scope)) {
            sql.append(" AND i.property_classification = :itemPropertyClassification");
            params.put("itemPropertyClassification", "부자재");
        } else if ("GENERAL".equals(scope)) {
            sql.append(" AND i.property_classification IN (:generalPropertyClassifications)");
            params.put("generalPropertyClassifications", List.of("원자재", "상품"));
        }
    }

    private static void appendReceiptItemPropertyScopeFilter(
            StringBuilder sql,
            Map<String, Object> params,
            String itemPropertyScope
    ) {
        if (itemPropertyScope == null || itemPropertyScope.isBlank()) {
            return;
        }
        String scope = itemPropertyScope.trim();
        if ("SUB_MATERIAL".equals(scope)) {
            sql.append("""
                     AND EXISTS (
                        SELECT 1 FROM purchase_receipt_line prl
                        JOIN item i ON i.id = prl.item_id
                        WHERE prl.purchase_receipt_id = pr.id AND prl.recording_state = 1
                          AND i.property_classification = :scopePropertyClassification
                    )
                    """);
            params.put("scopePropertyClassification", "부자재");
        } else if ("GENERAL".equals(scope)) {
            sql.append("""
                     AND EXISTS (
                        SELECT 1 FROM purchase_receipt_line prl
                        JOIN item i ON i.id = prl.item_id
                        WHERE prl.purchase_receipt_id = pr.id AND prl.recording_state = 1
                          AND i.property_classification IN (:scopeGeneralPropertyClassifications)
                    )
                     AND NOT EXISTS (
                        SELECT 1 FROM purchase_receipt_line prl
                        JOIN item i ON i.id = prl.item_id
                        WHERE prl.purchase_receipt_id = pr.id AND prl.recording_state = 1
                          AND i.property_classification = :scopeSubMaterialClassification
                    )
                    """);
            params.put("scopeGeneralPropertyClassifications", List.of("원자재", "상품"));
            params.put("scopeSubMaterialClassification", "부자재");
        }
    }

    @Override
    @Transactional
    public void cancelReceipt(long receiptId, String actorUserId) {
        PurchaseReceiptJpaEntity entity = receiptRepository.findById(receiptId)
                .orElseThrow(() -> new IllegalArgumentException("구매입고를 찾을 수 없습니다: " + receiptId));
        Instant now = Instant.now();
        entity.setStatus(PurchaseReceiptStatus.CANCELLED);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        receiptRepository.save(entity);
    }

    @Override
    @Transactional(readOnly = true)
    public PurchaseOrderLineReceiptContext findOrderLineContext(long purchaseOrderLineId) {
        PurchaseOrderLineJpaEntity line = orderLineRepository.findById(purchaseOrderLineId)
                .filter(entity -> entity.getRecordingState() == ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("발주 라인을 찾을 수 없습니다: " + purchaseOrderLineId));
        PurchaseOrderJpaEntity order = orderRepository.findById(line.getPurchaseOrderId())
                .orElseThrow(() -> new IllegalStateException("발주를 찾을 수 없습니다."));
        CompanyJpaEntity company = companyRepository.findById(order.getPartnerId()).orElse(null);
        ItemJpaEntity item = itemRepository.findById(line.getItemId()).orElse(null);
        return new PurchaseOrderLineReceiptContext(
                line.getId(),
                order.getId(),
                order.getPartnerId(),
                company != null ? company.getCompanyName() : "",
                order.getOrderNo(),
                order.getStatus(),
                line.getItemId(),
                item != null ? item.getItemNo() : "",
                item != null ? item.getItemName() : "",
                item != null && item.getPropertyClassification() != null
                        ? item.getPropertyClassification().name()
                        : "",
                item != null && item.getCheckDistinction() != null
                        ? item.getCheckDistinction().name()
                        : CheckDistinction.NONE.name(),
                item != null && item.isLotTracked(),
                line.getOrderQty(),
                line.getReceivedQty() != null ? line.getReceivedQty() : BigDecimal.ZERO,
                line.getWaitingInspectionQty() != null ? line.getWaitingInspectionQty() : BigDecimal.ZERO,
                line.getUnitPrice()
        );
    }

    @Override
    @Transactional
    public void addReceivedQty(long purchaseOrderLineId, BigDecimal qty, String actorUserId) {
        updateOrderLineQty(purchaseOrderLineId, qty, true, false, actorUserId);
    }

    @Override
    @Transactional
    public void addWaitingInspectionQty(long purchaseOrderLineId, BigDecimal qty, String actorUserId) {
        updateOrderLineQty(purchaseOrderLineId, qty, false, true, actorUserId);
    }

    @Override
    @Transactional
    public void releaseWaitingInspectionQty(long purchaseOrderLineId, BigDecimal qty, String actorUserId) {
        updateOrderLineQty(purchaseOrderLineId, qty.negate(), false, true, actorUserId);
    }

    @Override
    @Transactional
    public void updateReceiptLinePostedQty(long receiptLineId, BigDecimal postedQty, String actorUserId) {
        PurchaseReceiptLineJpaEntity line = receiptLineRepository.findByIdAndRecordingState(receiptLineId, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("입고 라인을 찾을 수 없습니다: " + receiptLineId));
        line.setPostedQty(line.getPostedQty().add(postedQty));
        line.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        line.setUpdatedAt(Instant.now());
        receiptLineRepository.save(line);
    }

    @Override
    @Transactional
    public void updateReceiptStatus(long receiptId, String actorUserId) {
        PurchaseReceiptJpaEntity receipt = receiptRepository.findById(receiptId)
                .orElseThrow(() -> new IllegalArgumentException("구매입고를 찾을 수 없습니다: " + receiptId));
        List<PurchaseReceiptLineJpaEntity> lines =
                receiptLineRepository.findByPurchaseReceiptIdAndRecordingStateOrderByLineNoAsc(receiptId, ACTIVE);
        boolean anyPosted = false;
        boolean allPosted = !lines.isEmpty();
        for (PurchaseReceiptLineJpaEntity line : lines) {
            if (line.getPostedQty().compareTo(BigDecimal.ZERO) > 0) {
                anyPosted = true;
            }
            if (line.getPostedQty().compareTo(line.getReceiptQty()) < 0) {
                allPosted = false;
            }
        }
        PurchaseReceiptStatus status = PurchaseReceiptStatus.REGISTERED;
        if (allPosted && anyPosted) {
            status = PurchaseReceiptStatus.POSTED;
        } else if (anyPosted) {
            status = PurchaseReceiptStatus.PARTIALLY_POSTED;
        }
        receipt.setStatus(status);
        receipt.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        receipt.setUpdatedAt(Instant.now());
        receiptRepository.save(receipt);
    }

    @Override
    @Transactional
    public void subtractReceivedQty(long purchaseOrderLineId, BigDecimal qty, String actorUserId) {
        updateOrderLineQty(purchaseOrderLineId, qty.negate(), true, false, actorUserId);
    }

    @Override
    @Transactional
    public void refreshPurchaseOrderReceiptStatus(long purchaseOrderId, String actorUserId) {
        PurchaseOrderJpaEntity order = orderRepository.findById(purchaseOrderId)
                .orElseThrow(() -> new IllegalArgumentException("구매발주를 찾을 수 없습니다: " + purchaseOrderId));
        if (order.getStatus() == PurchaseOrderStatus.CANCELLED) {
            return;
        }
        List<PurchaseOrderLineJpaEntity> lines =
                orderLineRepository.findByPurchaseOrderIdAndRecordingStateOrderByLineNoAsc(purchaseOrderId, ACTIVE);
        if (lines.isEmpty()) {
            return;
        }
        boolean allReceived = true;
        boolean anyProgress = false;
        for (PurchaseOrderLineJpaEntity line : lines) {
            BigDecimal received = line.getReceivedQty() != null ? line.getReceivedQty() : BigDecimal.ZERO;
            BigDecimal waiting = line.getWaitingInspectionQty() != null ? line.getWaitingInspectionQty() : BigDecimal.ZERO;
            if (received.compareTo(line.getOrderQty()) < 0) {
                allReceived = false;
            }
            if (received.compareTo(BigDecimal.ZERO) > 0 || waiting.compareTo(BigDecimal.ZERO) > 0) {
                anyProgress = true;
            }
        }
        PurchaseOrderStatus newStatus;
        if (allReceived) {
            newStatus = PurchaseOrderStatus.RECEIVED;
        } else if (anyProgress) {
            newStatus = PurchaseOrderStatus.IN_PROGRESS;
        } else {
            newStatus = order.getStatus() == PurchaseOrderStatus.DRAFT
                    ? PurchaseOrderStatus.DRAFT
                    : PurchaseOrderStatus.CONFIRMED;
        }
        if (order.getStatus() != newStatus) {
            Instant now = Instant.now();
            order.setStatus(newStatus);
            order.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
            order.setUpdatedAt(now);
            orderRepository.save(order);
        }
    }

    private void updateOrderLineQty(
            long purchaseOrderLineId,
            BigDecimal qty,
            boolean received,
            boolean waiting,
            String actorUserId
    ) {
        PurchaseOrderLineJpaEntity line = orderLineRepository.findById(purchaseOrderLineId)
                .orElseThrow(() -> new IllegalArgumentException("발주 라인을 찾을 수 없습니다: " + purchaseOrderLineId));
        if (received) {
            line.setReceivedQty(line.getReceivedQty().add(qty));
        }
        if (waiting) {
            line.setWaitingInspectionQty(line.getWaitingInspectionQty().add(qty));
        }
        line.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        line.setUpdatedAt(Instant.now());
        orderLineRepository.save(line);
    }

    private PurchaseReceiptView toView(PurchaseReceiptJpaEntity entity) {
        CompanyJpaEntity company = companyRepository.findById(entity.getPartnerId()).orElse(null);
        List<PurchaseReceiptLineJpaEntity> lines =
                receiptLineRepository.findByPurchaseReceiptIdAndRecordingStateOrderByLineNoAsc(entity.getId(), ACTIVE);
        List<PurchaseReceiptLineView> lineViews = new ArrayList<>();
        for (PurchaseReceiptLineJpaEntity line : lines) {
            ItemJpaEntity item = itemRepository.findById(line.getItemId()).orElse(null);
            boolean postedImmediately = line.getPostedQty().compareTo(line.getReceiptQty()) >= 0
                    && line.getReceiptQty().compareTo(BigDecimal.ZERO) > 0;
            lineViews.add(new PurchaseReceiptLineView(
                    line.getId(),
                    line.getLineNo(),
                    line.getPurchaseOrderLineId(),
                    line.getItemId(),
                    item != null ? item.getItemNo() : "",
                    item != null ? item.getItemName() : "",
                    line.getReceiptQty(),
                    line.getPostedQty(),
                    line.getUnitPrice(),
                    line.getAmount(),
                    null,
                    postedImmediately
            ));
        }
        return new PurchaseReceiptView(
                entity.getId(),
                entity.getReceiptNo(),
                entity.getPartnerId(),
                company != null ? company.getCompanyName() : "",
                entity.getReceiptDate(),
                entity.getPurchaseOrderId(),
                entity.getStatus(),
                entity.getCreatedAt(),
                masterAuditActorLookup.nameOf(entity.getCreatedById()),
                lineViews
        );
    }

    private static boolean toBooleanFlag(Object value) {
        if (value == null) {
            return false;
        }
        if (value instanceof Boolean bool) {
            return bool;
        }
        if (value instanceof Number number) {
            return number.intValue() != 0;
        }
        String text = value.toString().trim();
        return "1".equals(text) || "true".equalsIgnoreCase(text) || "Y".equalsIgnoreCase(text);
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

    private static LocalDate toLocalDate(Object value) {
        if (value instanceof LocalDate date) {
            return date;
        }
        if (value instanceof Date date) {
            return date.toLocalDate();
        }
        return LocalDate.parse(value.toString());
    }
}
