package com.shindong.smartmanager.infrastructure.persistence.outsource;

import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;

import com.shindong.smartmanager.application.outsource.OutsourcingOrderRepository;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderLineReceiptContext;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptCandidateCriteria;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptCandidateView;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptLineSaveCommand;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptLineView;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptListCriteria;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptRepository;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptSaveCommand;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptView;
import com.shindong.smartmanager.domain.item.CheckDistinction;
import com.shindong.smartmanager.domain.outsource.OutsourcingOrderStatus;
import com.shindong.smartmanager.domain.outsource.OutsourcingReceiptStatus;
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
public class JpaOutsourcingReceiptRepository implements OutsourcingReceiptRepository {

    private static final int ACTIVE = 1;

    private final EntityManager entityManager;
    private final SpringDataOutsourcingReceiptRepository receiptRepository;
    private final SpringDataOutsourcingReceiptLineRepository receiptLineRepository;
    private final SpringDataOutsourcingOrderLineRepository orderLineRepository;
    private final SpringDataOutsourcingOrderRepository orderRepository;
    private final SpringDataCompanyRepository companyRepository;
    private final SpringDataItemRepository itemRepository;
    private final OutsourcingOrderRepository outsourcingOrderRepository;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaOutsourcingReceiptRepository(
            EntityManager entityManager,
            SpringDataOutsourcingReceiptRepository receiptRepository,
            SpringDataOutsourcingReceiptLineRepository receiptLineRepository,
            SpringDataOutsourcingOrderLineRepository orderLineRepository,
            SpringDataOutsourcingOrderRepository orderRepository,
            SpringDataCompanyRepository companyRepository,
            SpringDataItemRepository itemRepository,
            OutsourcingOrderRepository outsourcingOrderRepository,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.entityManager = entityManager;
        this.receiptRepository = receiptRepository;
        this.receiptLineRepository = receiptLineRepository;
        this.orderLineRepository = orderLineRepository;
        this.orderRepository = orderRepository;
        this.companyRepository = companyRepository;
        this.itemRepository = itemRepository;
        this.outsourcingOrderRepository = outsourcingOrderRepository;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    @Transactional(readOnly = true)
    public List<OutsourcingReceiptCandidateView> findReceiptCandidates(OutsourcingReceiptCandidateCriteria criteria) {
        StringBuilder sql = new StringBuilder("""
                SELECT oo.id, ool.id, oo.order_no, oo.order_date, oo.partner_id, c.company_name,
                       ool.item_id, i.item_no, i.item_name, ps.id, pc.small_name,
                       i.check_distinction,
                       ool.order_qty, ool.shipped_qty, ool.received_qty, ool.waiting_inspection_qty,
                       ool.unit_price, ool.requested_delivery_date, i.lot_tracked
                FROM outsourcing_order_line ool
                JOIN outsourcing_order oo ON oo.id = ool.outsourcing_order_id
                JOIN company c ON c.id = oo.partner_id
                JOIN item i ON i.id = ool.item_id
                JOIN process_sequence ps ON ps.id = ool.process_sequence_id
                JOIN public_code pc ON pc.id = ps.public_code_id
                WHERE ool.recording_state = 1 AND oo.recording_state = 1
                  AND oo.status <> 'CANCELLED'
                  AND ool.shipped_qty > 0
                  AND (ool.shipped_qty - ool.received_qty - ool.waiting_inspection_qty) > 0
                """);
        Map<String, Object> params = new HashMap<>();
        if (criteria != null) {
            if (criteria.partnerName() != null && !criteria.partnerName().isBlank()) {
                sql.append(" AND c.company_name LIKE :partnerName");
                params.put("partnerName", "%" + criteria.partnerName().trim() + "%");
            }
            if (criteria.orderNo() != null && !criteria.orderNo().isBlank()) {
                sql.append(" AND oo.order_no LIKE :orderNo");
                params.put("orderNo", "%" + criteria.orderNo().trim() + "%");
            }
            if (criteria.orderDateFrom() != null) {
                sql.append(" AND oo.order_date >= :orderDateFrom");
                params.put("orderDateFrom", criteria.orderDateFrom());
            }
            if (criteria.orderDateTo() != null) {
                sql.append(" AND oo.order_date <= :orderDateTo");
                params.put("orderDateTo", criteria.orderDateTo());
            }
            if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
                sql.append(" AND i.item_no LIKE :itemNo");
                params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
            }
            if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
                sql.append(" AND i.item_name LIKE :itemName");
                params.put("itemName", "%" + criteria.itemName().trim() + "%");
            }
        }
        sql.append(" ORDER BY oo.order_date DESC, oo.order_no DESC, ool.line_no ASC");

        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);

        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<OutsourcingReceiptCandidateView> result = new ArrayList<>();
        for (Object[] row : rows) {
            BigDecimal orderQty = toBigDecimal(row[12]);
            BigDecimal shippedQty = toBigDecimal(row[13]);
            BigDecimal receivedQty = toBigDecimal(row[14]);
            BigDecimal waitingQty = toBigDecimal(row[15]);
            BigDecimal remainQty = shippedQty.subtract(receivedQty).subtract(waitingQty);
            result.add(new OutsourcingReceiptCandidateView(
                    ((Number) row[1]).longValue(),
                    ((Number) row[0]).longValue(),
                    (String) row[2],
                    ((Date) row[3]).toLocalDate(),
                    ((Number) row[4]).longValue(),
                    (String) row[5],
                    ((Number) row[6]).longValue(),
                    (String) row[7],
                    (String) row[8],
                    (String) row[10],
                    row[11] != null ? row[11].toString() : CheckDistinction.NONE.name(),
                    orderQty,
                    shippedQty,
                    receivedQty,
                    waitingQty,
                    remainQty,
                    toBigDecimal(row[16]),
                    row[17] != null ? ((Date) row[17]).toLocalDate() : null,
                    toBooleanFlag(row[18])
            ));
        }
        return result;
    }

    @Override
    public long countByReceiptNoPrefix(String prefix) {
        return receiptRepository.countByReceiptNoStartingWithAndRecordingState(prefix, ACTIVE);
    }

    @Override
    @Transactional
    public long saveReceipt(OutsourcingReceiptSaveCommand command, String actorUserId) {
        Instant now = Instant.now();
        OutsourcingReceiptJpaEntity header = new OutsourcingReceiptJpaEntity();
        header.setReceiptNo(command.receiptNo());
        header.setPartnerId(command.partnerId());
        header.setReceiptDate(command.receiptDate());
        header.setOutsourcingOrderId(command.outsourcingOrderId());
        header.setStatus(command.status());
        header.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
        header.setCreatedAt(now);
        header.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        header.setUpdatedAt(now);
        OutsourcingReceiptJpaEntity saved = receiptRepository.save(header);

        short lineNo = 1;
        for (OutsourcingReceiptLineSaveCommand line : command.lines()) {
            OutsourcingReceiptLineJpaEntity entity = new OutsourcingReceiptLineJpaEntity();
            entity.setOutsourcingReceiptId(saved.getId());
            entity.setLineNo(lineNo++);
            entity.setOutsourcingOrderLineId(line.outsourcingOrderLineId());
            entity.setItemId(line.itemId());
            entity.setReceiptQty(line.receiptQty());
            entity.setPostedQty(line.postedQty());
            entity.setUnitPrice(line.unitPrice());
            entity.setAmount(line.amount());
            entity.setLotId(line.lotId());
            entity.setRecordingState(ACTIVE);
            entity.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
            entity.setCreatedAt(now);
            entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
            entity.setUpdatedAt(now);
            receiptLineRepository.save(entity);
        }
        return saved.getId();
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<OutsourcingReceiptView> findActiveById(long id) {
        return receiptRepository.findById(id)
                .filter(entity -> entity.getRecordingState() == ACTIVE
                        && entity.getStatus() != OutsourcingReceiptStatus.CANCELLED)
                .map(this::toView);
    }

    @Override
    @Transactional(readOnly = true)
    public List<OutsourcingReceiptView> findAllActive(OutsourcingReceiptListCriteria criteria) {
        if (criteria == null || isListCriteriaEmpty(criteria)) {
            return receiptRepository.findAll().stream()
                    .filter(entity -> entity.getRecordingState() == ACTIVE
                            && entity.getStatus() != OutsourcingReceiptStatus.CANCELLED)
                    .sorted((a, b) -> b.getReceiptDate().compareTo(a.getReceiptDate()))
                    .map(this::toView)
                    .toList();
        }

        StringBuilder sql = new StringBuilder("""
                SELECT DISTINCT orc.id
                FROM outsourcing_receipt orc
                JOIN company c ON c.id = orc.partner_id
                WHERE orc.recording_state = 1
                  AND orc.status <> 'CANCELLED'
                """);
        Map<String, Object> params = new HashMap<>();
        if (criteria.partnerName() != null && !criteria.partnerName().isBlank()) {
            sql.append(" AND c.company_name LIKE :partnerName");
            params.put("partnerName", "%" + criteria.partnerName().trim() + "%");
        }
        if (criteria.receiptDateFrom() != null) {
            sql.append(" AND orc.receipt_date >= :receiptDateFrom");
            params.put("receiptDateFrom", criteria.receiptDateFrom());
        }
        if (criteria.receiptDateTo() != null) {
            sql.append(" AND orc.receipt_date <= :receiptDateTo");
            params.put("receiptDateTo", criteria.receiptDateTo());
        }
        boolean hasItemFilter = (criteria.itemNo() != null && !criteria.itemNo().isBlank())
                || (criteria.itemName() != null && !criteria.itemName().isBlank());
        if (hasItemFilter) {
            sql.append("""
                     AND EXISTS (
                        SELECT 1 FROM outsourcing_receipt_line orl
                        JOIN item i ON i.id = orl.item_id
                        WHERE orl.outsourcing_receipt_id = orc.id AND orl.recording_state = 1
                    """);
            if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
                sql.append(" AND i.item_no LIKE :itemNo");
                params.put("itemNo", "%" + criteria.itemNo().trim() + "%");
            }
            if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
                sql.append(" AND i.item_name LIKE :itemName");
                params.put("itemName", "%" + criteria.itemName().trim() + "%");
            }
            sql.append(")");
        }
        sql.append(" ORDER BY orc.receipt_date DESC, orc.receipt_no DESC");

        Query query = entityManager.createNativeQuery(sql.toString());
        params.forEach(query::setParameter);

        @SuppressWarnings("unchecked")
        List<Number> rows = query.getResultList();
        List<OutsourcingReceiptView> result = new ArrayList<>();
        for (Number row : rows) {
            findActiveById(row.longValue()).ifPresent(result::add);
        }
        return result;
    }

    @Override
    @Transactional
    public void cancelReceipt(long receiptId, String actorUserId) {
        OutsourcingReceiptJpaEntity entity = receiptRepository.findById(receiptId)
                .orElseThrow(() -> new IllegalArgumentException("외주입고를 찾을 수 없습니다: " + receiptId));
        Instant now = Instant.now();
        entity.setStatus(OutsourcingReceiptStatus.CANCELLED);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        receiptRepository.save(entity);
    }

    @Override
    @Transactional(readOnly = true)
    public OutsourcingOrderLineReceiptContext findOrderLineContext(long outsourcingOrderLineId) {
        OutsourcingOrderLineJpaEntity line = orderLineRepository.findByIdAndRecordingState(outsourcingOrderLineId, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("외주발주 라인을 찾을 수 없습니다: " + outsourcingOrderLineId));
        OutsourcingOrderJpaEntity order = orderRepository.findById(line.getOutsourcingOrderId())
                .orElseThrow(() -> new IllegalStateException("외주발주를 찾을 수 없습니다."));
        ItemJpaEntity item = itemRepository.findById(line.getItemId()).orElse(null);
        BigDecimal shippedQty = line.getShippedQty() != null ? line.getShippedQty() : BigDecimal.ZERO;
        BigDecimal receivedQty = line.getReceivedQty() != null ? line.getReceivedQty() : BigDecimal.ZERO;
        BigDecimal waitingQty = line.getWaitingInspectionQty() != null ? line.getWaitingInspectionQty() : BigDecimal.ZERO;
        BigDecimal remainQty = shippedQty.subtract(receivedQty).subtract(waitingQty);
        return new OutsourcingOrderLineReceiptContext(
                line.getId(),
                order.getId(),
                order.getPartnerId(),
                line.getItemId(),
                item != null ? item.getItemNo() : "",
                item != null && item.getCheckDistinction() != null
                        ? item.getCheckDistinction().name()
                        : CheckDistinction.NONE.name(),
                line.getEndProcessCodeId(),
                order.getStatus(),
                shippedQty,
                receivedQty,
                waitingQty,
                remainQty,
                line.getUnitPrice()
        );
    }

    @Override
    @Transactional
    public void addReceivedQty(long orderLineId, BigDecimal qty, String actorUserId) {
        outsourcingOrderRepository.addReceivedQty(orderLineId, qty, actorUserId);
    }

    @Override
    @Transactional
    public void addWaitingInspectionQty(long orderLineId, BigDecimal qty, String actorUserId) {
        outsourcingOrderRepository.addWaitingInspectionQty(orderLineId, qty, actorUserId);
    }

    @Override
    @Transactional
    public void releaseWaitingInspectionQty(long orderLineId, BigDecimal qty, String actorUserId) {
        outsourcingOrderRepository.releaseWaitingInspectionQty(orderLineId, qty, actorUserId);
    }

    @Override
    @Transactional
    public void updateReceiptLinePostedQty(long receiptLineId, BigDecimal postedQty, String actorUserId) {
        OutsourcingReceiptLineJpaEntity line = receiptLineRepository.findByIdAndRecordingState(receiptLineId, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("입고 라인을 찾을 수 없습니다: " + receiptLineId));
        line.setPostedQty(line.getPostedQty().add(postedQty));
        line.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        line.setUpdatedAt(Instant.now());
        receiptLineRepository.save(line);
    }

    @Override
    @Transactional
    public void updateReceiptLineLotId(long receiptLineId, Long lotId, String actorUserId) {
        OutsourcingReceiptLineJpaEntity line = receiptLineRepository.findByIdAndRecordingState(receiptLineId, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("입고 라인을 찾을 수 없습니다: " + receiptLineId));
        line.setLotId(lotId);
        line.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        line.setUpdatedAt(Instant.now());
        receiptLineRepository.save(line);
    }

    @Override
    @Transactional
    public void updateReceiptStatus(long receiptId, String actorUserId) {
        OutsourcingReceiptJpaEntity receipt = receiptRepository.findById(receiptId)
                .orElseThrow(() -> new IllegalArgumentException("외주입고를 찾을 수 없습니다: " + receiptId));
        List<OutsourcingReceiptLineJpaEntity> lines =
                receiptLineRepository.findByOutsourcingReceiptIdAndRecordingStateOrderByLineNoAsc(receiptId, ACTIVE);
        boolean anyPosted = false;
        boolean allPosted = !lines.isEmpty();
        for (OutsourcingReceiptLineJpaEntity line : lines) {
            if (line.getPostedQty().compareTo(BigDecimal.ZERO) > 0) {
                anyPosted = true;
            }
            if (line.getPostedQty().compareTo(line.getReceiptQty()) < 0) {
                allPosted = false;
            }
        }
        OutsourcingReceiptStatus status = OutsourcingReceiptStatus.REGISTERED;
        if (allPosted && anyPosted) {
            status = OutsourcingReceiptStatus.POSTED;
        } else if (anyPosted) {
            status = OutsourcingReceiptStatus.PARTIALLY_POSTED;
        }
        receipt.setStatus(status);
        receipt.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        receipt.setUpdatedAt(Instant.now());
        receiptRepository.save(receipt);
    }

    @Override
    @Transactional
    public void subtractReceivedQty(long orderLineId, BigDecimal qty, String actorUserId) {
        outsourcingOrderRepository.subtractReceivedQty(orderLineId, qty, actorUserId);
    }

    private OutsourcingReceiptView toView(OutsourcingReceiptJpaEntity entity) {
        CompanyJpaEntity partner = companyRepository.findById(entity.getPartnerId()).orElse(null);
        List<OutsourcingReceiptLineJpaEntity> lineEntities =
                receiptLineRepository.findByOutsourcingReceiptIdAndRecordingStateOrderByLineNoAsc(
                        entity.getId(), ACTIVE);
        List<OutsourcingReceiptLineView> lines = new ArrayList<>();
        for (OutsourcingReceiptLineJpaEntity lineEntity : lineEntities) {
            ItemJpaEntity item = itemRepository.findById(lineEntity.getItemId()).orElse(null);
            lines.add(new OutsourcingReceiptLineView(
                    lineEntity.getId(),
                    lineEntity.getLineNo(),
                    lineEntity.getOutsourcingOrderLineId(),
                    lineEntity.getItemId(),
                    item != null ? item.getItemNo() : "",
                    item != null ? item.getItemName() : "",
                    lineEntity.getReceiptQty(),
                    lineEntity.getPostedQty(),
                    lineEntity.getUnitPrice(),
                    lineEntity.getAmount(),
                    null,
                    lineEntity.getPostedQty().compareTo(BigDecimal.ZERO) > 0,
                    lineEntity.getLotId()
            ));
        }
        return new OutsourcingReceiptView(
                entity.getId(),
                entity.getReceiptNo(),
                entity.getPartnerId(),
                partner != null ? partner.getCompanyName() : "",
                entity.getReceiptDate(),
                entity.getOutsourcingOrderId(),
                entity.getStatus(),
                entity.getCreatedAt(),
                masterAuditActorLookup.nameOf(entity.getCreatedById()),
                lines
        );
    }

    private static boolean isListCriteriaEmpty(OutsourcingReceiptListCriteria criteria) {
        return (criteria.partnerName() == null || criteria.partnerName().isBlank())
                && criteria.receiptDateFrom() == null
                && criteria.receiptDateTo() == null
                && (criteria.itemNo() == null || criteria.itemNo().isBlank())
                && (criteria.itemName() == null || criteria.itemName().isBlank());
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
}
