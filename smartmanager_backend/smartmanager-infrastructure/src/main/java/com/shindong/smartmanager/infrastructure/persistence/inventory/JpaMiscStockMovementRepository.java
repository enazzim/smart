package com.shindong.smartmanager.infrastructure.persistence.inventory;

import com.shindong.smartmanager.application.inventory.MiscStockMovementListCriteria;
import com.shindong.smartmanager.application.inventory.MiscStockMovementRepository;
import com.shindong.smartmanager.application.inventory.MiscStockMovementSaveCommand;
import com.shindong.smartmanager.application.inventory.MiscStockMovementView;
import com.shindong.smartmanager.domain.inventory.InventoryLocationLabels;
import com.shindong.smartmanager.domain.inventory.MiscStockMovementStatus;
import com.shindong.smartmanager.domain.item.PropertyClassification;
import com.shindong.smartmanager.infrastructure.persistence.code.PublicCodeJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.code.SpringDataPublicCodeRepository;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import com.shindong.smartmanager.infrastructure.persistence.process.ProcessSequenceJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.process.SpringDataProcessSequenceRepository;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.persistence.Query;
import java.math.BigDecimal;
import java.sql.Timestamp;
import java.time.Instant;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaMiscStockMovementRepository implements MiscStockMovementRepository {

    private static final int ACTIVE = 1;

    private static final String LIST_SELECT = """
            SELECT m.id,
                   m.movement_no,
                   m.movement_date,
                   m.movement_direction,
                   m.item_id,
                   i.item_no,
                   i.item_name,
                   i.property_classification,
                   m.location_code,
                   m.output_process_id,
                   ps.process_sequence,
                   COALESCE(CONCAT(pc.small_code, ' ', pc.small_name), '') AS process_name,
                   m.qty,
                   m.reason_code_id,
                   COALESCE(rc.small_name, '') AS reason_label,
                   m.note,
                   m.status,
                   m.created_at
            FROM misc_stock_movement m
            JOIN item i ON i.id = m.item_id AND i.recording_state = 1
            LEFT JOIN process_sequence ps ON ps.id = m.output_process_id AND ps.recording_state = 1
            LEFT JOIN public_code pc ON pc.id = ps.public_code_id AND pc.recording_state = 1
            LEFT JOIN public_code rc ON rc.id = m.reason_code_id AND rc.recording_state = 1
            WHERE m.recording_state = 1
            """;

    @PersistenceContext
    private EntityManager entityManager;

    private final SpringDataMiscStockMovementRepository movementRepository;
    private final SpringDataItemRepository itemRepository;
    private final SpringDataProcessSequenceRepository processRepository;
    private final SpringDataPublicCodeRepository publicCodeRepository;

    public JpaMiscStockMovementRepository(
            SpringDataMiscStockMovementRepository movementRepository,
            SpringDataItemRepository itemRepository,
            SpringDataProcessSequenceRepository processRepository,
            SpringDataPublicCodeRepository publicCodeRepository
    ) {
        this.movementRepository = movementRepository;
        this.itemRepository = itemRepository;
        this.processRepository = processRepository;
        this.publicCodeRepository = publicCodeRepository;
    }

    @Override
    @Transactional(readOnly = true)
    public long countByMovementNoPrefix(String prefix) {
        return movementRepository.countByMovementNoStartingWithAndRecordingState(prefix, ACTIVE);
    }

    @Override
    @Transactional
    public MiscStockMovementView save(MiscStockMovementSaveCommand command, String movementNo, String actorUserId) {
        ItemJpaEntity item = itemRepository.findByIdAndRecordingState(command.itemId(), ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + command.itemId()));
        if (command.reasonCodeId() != null) {
            publicCodeRepository.findByIdAndRecordingState(command.reasonCodeId(), ACTIVE)
                    .orElseThrow(() -> new IllegalArgumentException("입출고 사유를 찾을 수 없습니다."));
        }
        if (command.outputProcessId() != null) {
            processRepository.findByIdAndRecordingState(command.outputProcessId(), ACTIVE)
                    .orElseThrow(() -> new IllegalArgumentException("공정을 찾을 수 없습니다: " + command.outputProcessId()));
        }

        Instant now = Instant.now();
        MiscStockMovementJpaEntity entity = new MiscStockMovementJpaEntity();
        entity.setMovementNo(movementNo);
        entity.setMovementDate(command.movementDate());
        entity.setMovementDirection(command.movementDirection());
        entity.setItemId(command.itemId());
        entity.setLocationCode(command.locationCode());
        entity.setOutputProcessId(command.outputProcessId());
        entity.setQty(command.qty());
        entity.setReasonCodeId(command.reasonCodeId());
        entity.setNote(command.note());
        entity.setStatus(MiscStockMovementStatus.REGISTERED);
        entity.setRecordingState(ACTIVE);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        movementRepository.save(entity);

        return toView(entity, item);
    }

    @Override
    @Transactional
    public MiscStockMovementView update(long id, MiscStockMovementSaveCommand command, String actorUserId) {
        MiscStockMovementJpaEntity entity = movementRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("기타 입출고를 찾을 수 없습니다: " + id));
        if (entity.getStatus() == MiscStockMovementStatus.CANCELLED) {
            throw new IllegalArgumentException("취소된 입출고는 수정할 수 없습니다.");
        }

        ItemJpaEntity item = itemRepository.findByIdAndRecordingState(command.itemId(), ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("품목을 찾을 수 없습니다: " + command.itemId()));
        if (command.reasonCodeId() != null) {
            publicCodeRepository.findByIdAndRecordingState(command.reasonCodeId(), ACTIVE)
                    .orElseThrow(() -> new IllegalArgumentException("입출고 사유를 찾을 수 없습니다."));
        }
        if (command.outputProcessId() != null) {
            processRepository.findByIdAndRecordingState(command.outputProcessId(), ACTIVE)
                    .orElseThrow(() -> new IllegalArgumentException("공정을 찾을 수 없습니다: " + command.outputProcessId()));
        }

        entity.setMovementDate(command.movementDate());
        entity.setMovementDirection(command.movementDirection());
        entity.setItemId(command.itemId());
        entity.setLocationCode(command.locationCode());
        entity.setOutputProcessId(command.outputProcessId());
        entity.setQty(command.qty());
        entity.setReasonCodeId(command.reasonCodeId());
        entity.setNote(command.note());
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(Instant.now());
        movementRepository.save(entity);

        return toView(entity, item);
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<MiscStockMovementView> findActiveById(long id) {
        return movementRepository.findByIdAndRecordingState(id, ACTIVE)
                .flatMap(entity -> itemRepository.findByIdAndRecordingState(entity.getItemId(), ACTIVE)
                        .map(item -> toView(entity, item)));
    }

    @Override
    @Transactional(readOnly = true)
    public List<MiscStockMovementView> findAllActive(MiscStockMovementListCriteria criteria) {
        StringBuilder sql = new StringBuilder(LIST_SELECT);
        if (criteria != null) {
            if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
                sql.append(" AND i.item_no LIKE :itemNo");
            }
            if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
                sql.append(" AND i.item_name LIKE :itemName");
            }
            if (criteria.movementDateFrom() != null) {
                sql.append(" AND m.movement_date >= :movementDateFrom");
            }
            if (criteria.movementDateTo() != null) {
                sql.append(" AND m.movement_date <= :movementDateTo");
            }
        }
        sql.append(" ORDER BY m.movement_date DESC, m.id DESC");

        Query query = entityManager.createNativeQuery(sql.toString());
        bindListParams(query, criteria);
        return mapRows(query.getResultList());
    }

    @Override
    @Transactional
    public void delete(long id) {
        MiscStockMovementJpaEntity entity = movementRepository.findByIdAndRecordingState(id, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("기타 입출고를 찾을 수 없습니다: " + id));
        movementRepository.delete(entity);
    }

    private void bindListParams(Query query, MiscStockMovementListCriteria criteria) {
        if (criteria == null) {
            return;
        }
        if (criteria.itemNo() != null && !criteria.itemNo().isBlank()) {
            query.setParameter("itemNo", "%" + criteria.itemNo().trim() + "%");
        }
        if (criteria.itemName() != null && !criteria.itemName().isBlank()) {
            query.setParameter("itemName", "%" + criteria.itemName().trim() + "%");
        }
        if (criteria.movementDateFrom() != null) {
            query.setParameter("movementDateFrom", criteria.movementDateFrom());
        }
        if (criteria.movementDateTo() != null) {
            query.setParameter("movementDateTo", criteria.movementDateTo());
        }
    }

    @SuppressWarnings("unchecked")
    private List<MiscStockMovementView> mapRows(List<?> rows) {
        List<MiscStockMovementView> result = new ArrayList<>();
        for (Object rowObj : rows) {
            result.add(toView((Object[]) rowObj));
        }
        return result;
    }

    private MiscStockMovementView toView(Object[] row) {
        String locationCode = row[8] != null ? row[8].toString() : "";
        Short processSequence = row[10] != null ? ((Number) row[10]).shortValue() : null;
        String processName = row[11] != null ? row[11].toString() : null;
        return new MiscStockMovementView(
                ((Number) row[0]).longValue(),
                row[1].toString(),
                toLocalDate(row[2]),
                com.shindong.smartmanager.domain.inventory.MiscStockMovementDirection.valueOf(row[3].toString()),
                ((Number) row[4]).longValue(),
                row[5].toString(),
                row[6].toString(),
                PropertyClassification.valueOf(row[7].toString()),
                locationCode,
                InventoryLocationLabels.labelWithProcess(
                        locationCode,
                        processSequence != null ? processSequence.intValue() : null,
                        processName
                ),
                row[9] != null ? ((Number) row[9]).longValue() : null,
                processSequence,
                processName != null && !processName.isBlank() ? processName : null,
                toBigDecimal(row[12]),
                row[13] != null ? ((Number) row[13]).longValue() : null,
                row[14] != null ? row[14].toString() : null,
                row[15] != null ? row[15].toString() : null,
                MiscStockMovementStatus.valueOf(row[16].toString()),
                toInstant(row[17])
        );
    }

    private MiscStockMovementView toView(MiscStockMovementJpaEntity entity, ItemJpaEntity item) {
        ProcessSequenceJpaEntity process = entity.getOutputProcessId() != null
                ? processRepository.findByIdAndRecordingState(entity.getOutputProcessId(), ACTIVE).orElse(null)
                : null;
        PublicCodeJpaEntity processCode = process != null
                ? publicCodeRepository.findByIdAndRecordingState(process.getPublicCodeId(), ACTIVE).orElse(null)
                : null;
        PublicCodeJpaEntity reason = entity.getReasonCodeId() != null
                ? publicCodeRepository.findByIdAndRecordingState(entity.getReasonCodeId(), ACTIVE).orElse(null)
                : null;
        String processName = processCode != null
                ? processCode.getSmallCode() + " " + processCode.getSmallName()
                : null;
        Short processSequence = process != null ? process.getProcessSequenceNum() : null;
        String locationCode = entity.getLocationCode();
        return new MiscStockMovementView(
                entity.getId(),
                entity.getMovementNo(),
                entity.getMovementDate(),
                entity.getMovementDirection(),
                entity.getItemId(),
                item.getItemNo(),
                item.getItemName(),
                item.getPropertyClassification(),
                locationCode,
                InventoryLocationLabels.labelWithProcess(
                        locationCode,
                        processSequence != null ? processSequence.intValue() : null,
                        processName
                ),
                entity.getOutputProcessId(),
                processSequence,
                processName,
                entity.getQty(),
                entity.getReasonCodeId(),
                reason != null ? reason.getSmallName() : null,
                entity.getNote(),
                entity.getStatus(),
                entity.getCreatedAt()
        );
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
        if (value instanceof java.sql.Date date) {
            return date.toLocalDate();
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
}
