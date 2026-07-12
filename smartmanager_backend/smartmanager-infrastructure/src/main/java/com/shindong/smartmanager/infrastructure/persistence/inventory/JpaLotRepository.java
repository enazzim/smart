package com.shindong.smartmanager.infrastructure.persistence.inventory;

import com.shindong.smartmanager.application.inventory.LotBalanceView;
import com.shindong.smartmanager.application.inventory.LotGenealogyLinkView;
import com.shindong.smartmanager.application.inventory.LotListCriteria;
import com.shindong.smartmanager.application.inventory.LotRepository;
import com.shindong.smartmanager.application.inventory.LotView;
import com.shindong.smartmanager.application.inventory.StockMovementListItemView;
import com.shindong.smartmanager.domain.inventory.InventoryLocationLabels;
import com.shindong.smartmanager.domain.inventory.LotGenealogyDirection;
import com.shindong.smartmanager.domain.inventory.LotGenealogyLinkType;
import com.shindong.smartmanager.domain.inventory.LotOriginType;
import com.shindong.smartmanager.domain.inventory.LotStatus;
import com.shindong.smartmanager.domain.inventory.StockMovementType;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.persistence.Query;
import java.math.BigDecimal;
import java.sql.Date;
import java.sql.Timestamp;
import java.time.Instant;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import org.springframework.dao.DataIntegrityViolationException;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaLotRepository implements LotRepository {

    private static final int ACTIVE = 1;
    private static final DateTimeFormatter LOT_DATE = DateTimeFormatter.ofPattern("yyyyMMdd");

    private static final String LOT_SELECT = """
            SELECT l.id,
                   l.item_id,
                   i.item_no,
                   i.item_name,
                   l.lot_no,
                   l.status,
                   l.origin_type,
                   l.origin_doc_type,
                   l.origin_doc_id,
                   l.p1,
                   l.p2,
                   l.expiry_date,
                   l.certificate_ref,
                   l.remark,
                   l.created_at,
                   l.updated_at
            FROM inventory_lot l
            JOIN item i ON i.id = l.item_id AND i.recording_state = 1
            WHERE l.recording_state = 1
            """;

    private static final String BALANCE_SELECT = """
            SELECT lb.id,
                   lb.lot_id,
                   lb.inventory_balance_id,
                   il.location_code,
                   ib.output_process_id,
                   ps.process_sequence,
                   COALESCE(CONCAT(pc.small_code, ' ', pc.small_name), '') AS process_name,
                   lb.qty_on_hand
            FROM inventory_lot_balance lb
            JOIN inventory_balance ib ON ib.id = lb.inventory_balance_id AND ib.recording_state = 1
            JOIN inventory_location il ON il.id = ib.location_id
            LEFT JOIN process_sequence ps ON ps.id = ib.output_process_id AND ps.recording_state = 1
            LEFT JOIN public_code pc ON pc.id = ps.public_code_id AND pc.recording_state = 1
            WHERE lb.recording_state = 1
              AND lb.lot_id = :lotId
            ORDER BY il.location_code, ps.process_sequence, lb.id
            """;

    @PersistenceContext
    private EntityManager entityManager;

    private final SpringDataInventoryLotRepository lotRepository;
    private final SpringDataInventoryLotBalanceRepository lotBalanceRepository;
    private final SpringDataLotNumberSequenceRepository sequenceRepository;
    private final SpringDataLotGenealogyRepository genealogyRepository;
    private final SpringDataItemRepository itemRepository;

    public JpaLotRepository(
            SpringDataInventoryLotRepository lotRepository,
            SpringDataInventoryLotBalanceRepository lotBalanceRepository,
            SpringDataLotNumberSequenceRepository sequenceRepository,
            SpringDataLotGenealogyRepository genealogyRepository,
            SpringDataItemRepository itemRepository
    ) {
        this.lotRepository = lotRepository;
        this.lotBalanceRepository = lotBalanceRepository;
        this.sequenceRepository = sequenceRepository;
        this.genealogyRepository = genealogyRepository;
        this.itemRepository = itemRepository;
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<LotView> findActiveById(long id) {
        Query query = entityManager.createNativeQuery(LOT_SELECT + " AND l.id = :id");
        query.setParameter("id", id);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        if (rows.isEmpty()) {
            return Optional.empty();
        }
        LotView base = mapLotRow(rows.get(0), List.of());
        return Optional.of(withBalances(base));
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<LotView> findActiveByItemIdAndLotNo(long itemId, String lotNo) {
        return lotRepository.findByItemIdAndLotNoAndRecordingState(itemId, lotNo, ACTIVE)
                .flatMap(entity -> findActiveById(entity.getId()));
    }

    @Override
    @Transactional(readOnly = true)
    public List<LotView> findAllActive(LotListCriteria criteria) {
        StringBuilder sql = new StringBuilder(LOT_SELECT);
        if (criteria != null) {
            if (criteria.itemId() != null) {
                sql.append(" AND l.item_id = :itemId");
            }
            if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
                sql.append(" AND i.item_no LIKE :itemNo");
            }
            if (criteria.lotNo() != null && !criteria.lotNo().isBlank()) {
                sql.append(" AND l.lot_no LIKE :lotNo");
            }
            if (criteria.status() != null) {
                sql.append(" AND l.status = :status");
            }
            if (criteria.locationCode() != null && !criteria.locationCode().isBlank()) {
                sql.append("""
                         AND EXISTS (
                          SELECT 1 FROM inventory_lot_balance lb
                          JOIN inventory_balance ib ON ib.id = lb.inventory_balance_id AND ib.recording_state = 1
                          JOIN inventory_location il ON il.id = ib.location_id
                          WHERE lb.lot_id = l.id AND lb.recording_state = 1
                            AND il.location_code = :locationCode
                        )
                        """);
            }
        }
        sql.append(" ORDER BY i.item_no, l.lot_no, l.id");

        Query query = entityManager.createNativeQuery(sql.toString());
        if (criteria != null) {
            if (criteria.itemId() != null) {
                query.setParameter("itemId", criteria.itemId());
            }
            if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
                query.setParameter("itemNo", "%" + criteria.itemNo().trim() + "%");
            }
            if (criteria.lotNo() != null && !criteria.lotNo().isBlank()) {
                query.setParameter("lotNo", "%" + criteria.lotNo().trim() + "%");
            }
            if (criteria.status() != null) {
                query.setParameter("status", criteria.status().name());
            }
            if (criteria.locationCode() != null && !criteria.locationCode().isBlank()) {
                query.setParameter("locationCode", criteria.locationCode().trim().toUpperCase());
            }
        }

        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<LotView> result = new ArrayList<>(rows.size());
        for (Object[] row : rows) {
            result.add(mapLotRow(row, List.of()));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public List<LotBalanceView> findBalancesByLotId(long lotId) {
        Query query = entityManager.createNativeQuery(BALANCE_SELECT);
        query.setParameter("lotId", lotId);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        List<LotBalanceView> balances = new ArrayList<>(rows.size());
        for (Object[] row : rows) {
            balances.add(mapBalanceRow(row));
        }
        return balances;
    }

    @Override
    @Transactional(readOnly = true)
    public List<LotView> findAvailableLots(long itemId, String locationCode, Long outputProcessId) {
        StringBuilder sql = new StringBuilder("""
                SELECT l.id,
                       l.item_id,
                       i.item_no,
                       i.item_name,
                       l.lot_no,
                       l.status,
                       l.origin_type,
                       l.origin_doc_type,
                       l.origin_doc_id,
                       l.p1,
                       l.p2,
                       l.expiry_date,
                       l.certificate_ref,
                       l.remark,
                       l.created_at,
                       l.updated_at,
                       lb.id AS balance_row_id,
                       lb.inventory_balance_id,
                       il.location_code,
                       ib.output_process_id,
                       ps.process_sequence,
                       COALESCE(CONCAT(pc.small_code, ' ', pc.small_name), '') AS process_name,
                       lb.qty_on_hand
                FROM inventory_lot l
                JOIN item i ON i.id = l.item_id AND i.recording_state = 1
                JOIN inventory_lot_balance lb ON lb.lot_id = l.id AND lb.recording_state = 1
                JOIN inventory_balance ib ON ib.id = lb.inventory_balance_id AND ib.recording_state = 1
                JOIN inventory_location il ON il.id = ib.location_id
                LEFT JOIN process_sequence ps ON ps.id = ib.output_process_id AND ps.recording_state = 1
                LEFT JOIN public_code pc ON pc.id = ps.public_code_id AND pc.recording_state = 1
                WHERE l.recording_state = 1
                  AND l.item_id = :itemId
                  AND l.status = 'ACTIVE'
                  AND lb.qty_on_hand > 0
                  AND il.location_code = :locationCode
                """);
        if (outputProcessId != null) {
            sql.append(" AND ib.output_process_id = :outputProcessId");
        } else {
            sql.append(" AND ib.output_process_id IS NULL");
        }
        sql.append(" ORDER BY l.lot_no, l.id, lb.id");

        Query query = entityManager.createNativeQuery(sql.toString());
        query.setParameter("itemId", itemId);
        query.setParameter("locationCode", locationCode);
        if (outputProcessId != null) {
            query.setParameter("outputProcessId", outputProcessId);
        }

        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();
        Map<Long, LotView> byLotId = new LinkedHashMap<>();
        for (Object[] row : rows) {
            long lotId = ((Number) row[0]).longValue();
            LotBalanceView balance = new LotBalanceView(
                    ((Number) row[16]).longValue(),
                    lotId,
                    ((Number) row[17]).longValue(),
                    (String) row[18],
                    InventoryLocationLabels.labelWithProcess(
                            (String) row[18],
                            row[20] != null ? ((Number) row[20]).intValue() : null,
                            (String) row[21]
                    ),
                    row[19] != null ? ((Number) row[19]).longValue() : null,
                    row[20] != null ? ((Number) row[20]).shortValue() : null,
                    (String) row[21],
                    toBigDecimal(row[22])
            );
            LotView existing = byLotId.get(lotId);
            if (existing == null) {
                Object[] lotCols = new Object[16];
                System.arraycopy(row, 0, lotCols, 0, 16);
                byLotId.put(lotId, mapLotRow(lotCols, List.of(balance)));
            } else {
                List<LotBalanceView> merged = new ArrayList<>(existing.balances());
                merged.add(balance);
                byLotId.put(lotId, new LotView(
                        existing.id(),
                        existing.itemId(),
                        existing.itemNo(),
                        existing.itemName(),
                        existing.lotNo(),
                        existing.status(),
                        existing.originType(),
                        existing.originDocType(),
                        existing.originDocId(),
                        existing.p1(),
                        existing.p2(),
                        existing.expiryDate(),
                        existing.certificateRef(),
                        existing.remark(),
                        existing.createdAt(),
                        existing.updatedAt(),
                        List.copyOf(merged)
                ));
            }
        }
        return List.copyOf(byLotId.values());
    }

    @Override
    @Transactional
    public LotView saveNew(
            long itemId,
            String lotNo,
            LotStatus status,
            LotOriginType originType,
            String originDocType,
            Long originDocId,
            String p1,
            String p2,
            LocalDate expiryDate,
            String certificateRef,
            String remark,
            String actorUserId
    ) {
        itemRepository.findByIdAndRecordingState(itemId, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + itemId));

        Instant now = Instant.now();
        InventoryLotJpaEntity entity = new InventoryLotJpaEntity();
        entity.setItemId(itemId);
        entity.setLotNo(lotNo);
        entity.setStatus(status != null ? status : LotStatus.ACTIVE);
        entity.setOriginType(originType != null ? originType : LotOriginType.MANUAL);
        entity.setOriginDocType(originDocType);
        entity.setOriginDocId(originDocId);
        entity.setP1(p1);
        entity.setP2(p2);
        entity.setExpiryDate(expiryDate);
        entity.setCertificateRef(certificateRef);
        entity.setRemark(remark);
        entity.setRecordingState(ACTIVE);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        lotRepository.save(entity);
        return findActiveById(entity.getId())
                .orElseThrow(() -> new IllegalStateException("저장한 Lot를 조회할 수 없습니다."));
    }

    @Override
    @Transactional
    public LotView update(
            long id,
            LotStatus status,
            String p1,
            String p2,
            LocalDate expiryDate,
            String certificateRef,
            String remark,
            String actorUserId
    ) {
        InventoryLotJpaEntity entity = lotRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("Lot를 찾을 수 없습니다: " + id));
        entity.setStatus(status);
        entity.setP1(p1);
        entity.setP2(p2);
        entity.setExpiryDate(expiryDate);
        entity.setCertificateRef(certificateRef);
        entity.setRemark(remark);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(Instant.now());
        lotRepository.save(entity);
        return findActiveById(id)
                .orElseThrow(() -> new IllegalStateException("수정한 Lot를 조회할 수 없습니다."));
    }

    @Override
    @Transactional
    public void softDelete(long id, String actorUserId) {
        InventoryLotJpaEntity entity = lotRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("Lot를 찾을 수 없습니다: " + id));
        entity.setRecordingState(0);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(Instant.now());
        lotRepository.save(entity);
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<BigDecimal> findLotBalanceQty(long lotId, long inventoryBalanceId) {
        return lotBalanceRepository
                .findByLotIdAndInventoryBalanceIdAndRecordingState(lotId, inventoryBalanceId, ACTIVE)
                .map(InventoryLotBalanceJpaEntity::getQtyOnHand);
    }

    @Override
    @Transactional
    public String allocateLotNo(long itemId, String itemNo, LocalDate sequenceDate) {
        LocalDate date = sequenceDate != null ? sequenceDate : LocalDate.now();
        int seq = nextSequence(itemId, date);
        String safeItemNo = itemNo == null ? "ITEM" : itemNo.trim().replace(' ', '_');
        return safeItemNo + "-" + date.format(LOT_DATE) + "-" + String.format("%03d", seq);
    }

    private int nextSequence(long itemId, LocalDate sequenceDate) {
        Optional<LotNumberSequenceJpaEntity> locked = sequenceRepository.findForUpdate(itemId, sequenceDate);
        if (locked.isEmpty()) {
            LotNumberSequenceJpaEntity created = new LotNumberSequenceJpaEntity();
            created.setItemId(itemId);
            created.setSequenceDate(sequenceDate);
            created.setLastSeq(1);
            try {
                sequenceRepository.saveAndFlush(created);
                return 1;
            } catch (DataIntegrityViolationException ex) {
                locked = sequenceRepository.findForUpdate(itemId, sequenceDate);
            }
        }
        LotNumberSequenceJpaEntity entity = locked.orElseThrow(
                () -> new IllegalStateException("Lot 채번 시퀀스를 확보할 수 없습니다.")
        );
        int next = entity.getLastSeq() + 1;
        entity.setLastSeq(next);
        sequenceRepository.save(entity);
        return next;
    }

    @Override
    @Transactional
    public void applyLotBalanceDelta(
            long lotId,
            long inventoryBalanceId,
            BigDecimal signedQty,
            String actorUserId
    ) {
        Instant now = Instant.now();
        InventoryLotBalanceJpaEntity balance = lotBalanceRepository
                .findByLotIdAndInventoryBalanceIdAndRecordingState(lotId, inventoryBalanceId, ACTIVE)
                .orElseGet(() -> {
                    InventoryLotBalanceJpaEntity created = new InventoryLotBalanceJpaEntity();
                    created.setLotId(lotId);
                    created.setInventoryBalanceId(inventoryBalanceId);
                    created.setQtyOnHand(BigDecimal.ZERO);
                    created.setRecordingState(ACTIVE);
                    created.setCreatedBy(actorUserId);
                    created.setCreatedById(actorUserId);
                    created.setCreatedAt(now);
                    return created;
                });
        BigDecimal nextQty = balance.getQtyOnHand().add(signedQty);
        balance.setQtyOnHand(nextQty);
        balance.setUpdatedBy(actorUserId);
        balance.setUpdatedById(actorUserId);
        balance.setUpdatedAt(now);
        lotBalanceRepository.save(balance);
    }

    @Override
    @Transactional
    public void refreshLotStatusFromBalances(long lotId, String actorUserId) {
        InventoryLotJpaEntity lot = lotRepository.findByIdAndRecordingState(lotId, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("Lot를 찾을 수 없습니다: " + lotId));
        if (lot.getStatus() == LotStatus.BLOCKED) {
            return;
        }
        List<InventoryLotBalanceJpaEntity> balances =
                lotBalanceRepository.findByLotIdAndRecordingState(lotId, ACTIVE);
        boolean hasQty = balances.stream()
                .anyMatch(b -> b.getQtyOnHand() != null && b.getQtyOnHand().compareTo(BigDecimal.ZERO) > 0);
        // 잔량 행이 없으면 ACTIVE 유지. 행이 있고 전부 0이면 DEPLETED.
        LotStatus next = (!balances.isEmpty() && !hasQty) ? LotStatus.DEPLETED : LotStatus.ACTIVE;
        if (lot.getStatus() != next) {
            lot.setStatus(next);
            lot.setUpdatedBy(actorUserId);
            lot.setUpdatedById(actorUserId);
            lot.setUpdatedAt(Instant.now());
            lotRepository.save(lot);
        }
    }

    private LotView withBalances(LotView base) {
        return new LotView(
                base.id(),
                base.itemId(),
                base.itemNo(),
                base.itemName(),
                base.lotNo(),
                base.status(),
                base.originType(),
                base.originDocType(),
                base.originDocId(),
                base.p1(),
                base.p2(),
                base.expiryDate(),
                base.certificateRef(),
                base.remark(),
                base.createdAt(),
                base.updatedAt(),
                findBalancesByLotId(base.id())
        );
    }

    private static LotView mapLotRow(Object[] row, List<LotBalanceView> balances) {
        return new LotView(
                ((Number) row[0]).longValue(),
                ((Number) row[1]).longValue(),
                (String) row[2],
                (String) row[3],
                (String) row[4],
                LotStatus.valueOf((String) row[5]),
                LotOriginType.valueOf((String) row[6]),
                (String) row[7],
                row[8] != null ? ((Number) row[8]).longValue() : null,
                (String) row[9],
                (String) row[10],
                toLocalDate(row[11]),
                (String) row[12],
                (String) row[13],
                toInstant(row[14]),
                toInstant(row[15]),
                balances != null ? balances : List.of()
        );
    }

    private static LotBalanceView mapBalanceRow(Object[] row) {
        // row: id, lot_id, inventory_balance_id, location_code, output_process_id, process_sequence, process_name, qty
        String locationCode = (String) row[3];
        Integer processSequence = row[5] != null ? ((Number) row[5]).intValue() : null;
        String processName = (String) row[6];
        return new LotBalanceView(
                ((Number) row[0]).longValue(),
                ((Number) row[1]).longValue(),
                ((Number) row[2]).longValue(),
                locationCode,
                InventoryLocationLabels.labelWithProcess(locationCode, processSequence, processName),
                row[4] != null ? ((Number) row[4]).longValue() : null,
                row[5] != null ? ((Number) row[5]).shortValue() : null,
                processName,
                toBigDecimal(row[7])
        );
    }

    @Override
    @Transactional
    public void saveGenealogyLink(
            long parentLotId,
            long childLotId,
            LotGenealogyLinkType linkType,
            BigDecimal qty,
            Long stockMovementId,
            String sourceDocType,
            Long sourceDocId,
            String actorUserId
    ) {
        LotGenealogyJpaEntity entity = new LotGenealogyJpaEntity();
        entity.setParentLotId(parentLotId);
        entity.setChildLotId(childLotId);
        entity.setLinkType(linkType);
        entity.setQty(qty);
        entity.setStockMovementId(stockMovementId);
        entity.setSourceDocType(sourceDocType);
        entity.setSourceDocId(sourceDocId);
        entity.setRecordingState(ACTIVE);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(Instant.now());
        genealogyRepository.save(entity);
    }

    @Override
    @Transactional
    public void softDeactivateGenealogyBySourceDoc(String sourceDocType, long sourceDocId) {
        genealogyRepository.softDeactivateBySourceDoc(sourceDocType, sourceDocId);
    }

    @Override
    @Transactional(readOnly = true)
    public List<LotGenealogyLinkView> findGenealogy(long lotId, LotGenealogyDirection direction) {
        List<LotGenealogyJpaEntity> links = direction == LotGenealogyDirection.UP
                ? genealogyRepository.findByChildLotIdAndRecordingStateOrderByIdAsc(lotId, ACTIVE)
                : genealogyRepository.findByParentLotIdAndRecordingStateOrderByIdAsc(lotId, ACTIVE);

        List<LotGenealogyLinkView> result = new ArrayList<>();
        for (LotGenealogyJpaEntity link : links) {
            LotView parent = findActiveById(link.getParentLotId()).orElse(null);
            LotView child = findActiveById(link.getChildLotId()).orElse(null);
            if (parent == null || child == null) {
                continue;
            }
            result.add(new LotGenealogyLinkView(
                    link.getId(),
                    parent.id(),
                    parent.lotNo(),
                    parent.itemId(),
                    parent.itemNo(),
                    parent.itemName(),
                    child.id(),
                    child.lotNo(),
                    child.itemId(),
                    child.itemNo(),
                    child.itemName(),
                    link.getLinkType(),
                    link.getQty(),
                    link.getStockMovementId(),
                    link.getSourceDocType(),
                    link.getSourceDocId(),
                    link.getCreatedAt()
            ));
        }
        return result;
    }

    @Override
    @Transactional(readOnly = true)
    public List<StockMovementListItemView> findMovementsByLotId(long lotId) {
        Query query = entityManager.createNativeQuery("""
                SELECT sm.id, sm.item_id, i.item_no, i.item_name,
                       il.location_code, il.location_name,
                       ps.process_sequence, pc.small_name,
                       sm.movement_type, sm.qty, sm.amount,
                       sm.reference_type, sm.reference_id,
                       sm.movement_date, sm.fiscal_year, sm.fiscal_month
                FROM stock_movement sm
                JOIN item i ON i.id = sm.item_id
                JOIN inventory_location il ON il.id = sm.location_id
                JOIN inventory_balance ib ON ib.id = sm.inventory_balance_id
                LEFT JOIN process_sequence ps ON ps.id = COALESCE(sm.output_process_id, ib.output_process_id)
                        AND ps.recording_state = 1
                LEFT JOIN public_code pc ON pc.id = ps.public_code_id
                WHERE sm.recording_state = 1
                  AND sm.lot_id = :lotId
                ORDER BY sm.movement_date DESC, sm.id DESC
                LIMIT 500
                """);
        query.setParameter("lotId", lotId);
        @SuppressWarnings("unchecked")
        List<Object[]> rows = query.getResultList();

        List<StockMovementListItemView> result = new ArrayList<>();
        for (Object[] row : rows) {
            result.add(new StockMovementListItemView(
                    ((Number) row[0]).longValue(),
                    ((Number) row[1]).longValue(),
                    row[2].toString(),
                    row[3].toString(),
                    row[4].toString(),
                    row[5].toString(),
                    toInteger(row[6]),
                    row[7] != null ? row[7].toString() : null,
                    StockMovementType.valueOf(row[8].toString()),
                    toBigDecimal(row[9]),
                    toBigDecimal(row[10]),
                    row[11].toString(),
                    ((Number) row[12]).longValue(),
                    toLocalDate(row[13]),
                    ((Number) row[14]).intValue(),
                    ((Number) row[15]).intValue()
            ));
        }
        return result;
    }

    private static Integer toInteger(Object value) {
        if (value == null) {
            return null;
        }
        if (value instanceof Number number) {
            return number.intValue();
        }
        return Integer.valueOf(value.toString());
    }

    private static LocalDate toLocalDate(Object value) {
        if (value == null) {
            return null;
        }
        if (value instanceof LocalDate localDate) {
            return localDate;
        }
        if (value instanceof Date sqlDate) {
            return sqlDate.toLocalDate();
        }
        return LocalDate.parse(value.toString());
    }

    private static Instant toInstant(Object value) {
        if (value == null) {
            return null;
        }
        if (value instanceof Instant instant) {
            return instant;
        }
        if (value instanceof Timestamp timestamp) {
            return timestamp.toInstant();
        }
        return Instant.parse(value.toString());
    }

    private static BigDecimal toBigDecimal(Object value) {
        if (value == null) {
            return BigDecimal.ZERO;
        }
        if (value instanceof BigDecimal bigDecimal) {
            return bigDecimal;
        }
        return new BigDecimal(value.toString());
    }
}
