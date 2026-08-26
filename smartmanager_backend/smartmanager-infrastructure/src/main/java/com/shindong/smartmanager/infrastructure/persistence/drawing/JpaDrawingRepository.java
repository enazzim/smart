package com.shindong.smartmanager.infrastructure.persistence.drawing;

import com.shindong.smartmanager.application.drawing.DrawingAlreadyExistsException;
import com.shindong.smartmanager.application.drawing.DrawingHistoryDetailView;
import com.shindong.smartmanager.application.drawing.DrawingHistoryView;
import com.shindong.smartmanager.application.drawing.DrawingListView;
import com.shindong.smartmanager.application.drawing.DrawingMasterView;
import com.shindong.smartmanager.application.drawing.DrawingRepository;
import com.shindong.smartmanager.domain.drawing.DrawingLifecycleStage;
import com.shindong.smartmanager.domain.drawing.DrawingType;
import com.shindong.smartmanager.infrastructure.persistence.company.CompanyJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;
import jakarta.persistence.EntityManager;
import java.time.Instant;
import java.util.HashSet;
import java.util.List;
import java.util.Optional;
import java.util.Set;
import java.util.UUID;
import org.springframework.dao.DataIntegrityViolationException;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaDrawingRepository implements DrawingRepository {

    private static final int ACTIVE = 1;
    private static final int DELETED = 0;

    private final SpringDataDrawingMasterRepository masterRepository;
    private final SpringDataDrawingHistoryRepository historyRepository;
    private final EntityManager entityManager;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaDrawingRepository(
            SpringDataDrawingMasterRepository masterRepository,
            SpringDataDrawingHistoryRepository historyRepository,
            EntityManager entityManager,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.masterRepository = masterRepository;
        this.historyRepository = historyRepository;
        this.entityManager = entityManager;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    public boolean existsActiveByPartNo(String partNo) {
        return masterRepository.existsByPartNoAndRecordingState(partNo, ACTIVE);
    }

    @Override
    @Transactional
    public String saveMaster(
            String partNo,
            String partName,
            String modelType,
            Long sourcePartnerId,
            DrawingLifecycleStage lifecycleStage,
            String actorUserId
    ) {
        Instant now = Instant.now();
        DrawingMasterJpaEntity entity = new DrawingMasterJpaEntity();
        entity.setId(UUID.randomUUID().toString());
        entity.setPartNo(partNo);
        entity.setPartName(partName);
        entity.setModelType(modelType);
        entity.setLifecycleStage(lifecycleStage != null ? lifecycleStage : DrawingLifecycleStage.RECEIVED);
        entity.setSourcePartner(resolveCompanyReference(sourcePartnerId));
        entity.setRecordingState(ACTIVE);
        entity.setCreatedBy(masterAuditActorLookup.nameOf(actorUserId));
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(masterAuditActorLookup.nameOf(actorUserId));
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        try {
            return masterRepository.save(entity).getId();
        } catch (DataIntegrityViolationException ex) {
            throw new DrawingAlreadyExistsException("이미 등록되어 사용 중인 품번입니다. (동시성 방어)");
        }
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<DrawingMasterView> findMasterById(String id) {
        return masterRepository.findById(id).map(this::toMasterView);
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<DrawingMasterView> findActiveMasterByPartNo(String partNo) {
        return masterRepository.findByPartNoAndRecordingState(partNo, ACTIVE).map(this::toMasterView);
    }

    @Override
    @Transactional
    public void softDeleteMasterByPartNo(String partNo, String actorUserId) {
        DrawingMasterJpaEntity entity = masterRepository.findByPartNoAndRecordingState(partNo, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("해당 품번의 도면을 찾을 수 없습니다: " + partNo));
        Instant now = Instant.now();
        entity.setRecordingState(DELETED);
        entity.setUpdatedBy(masterAuditActorLookup.nameOf(actorUserId));
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        masterRepository.save(entity);
    }

    @Override
    @Transactional
    public void restoreMaster(String id, String actorUserId) {
        DrawingMasterJpaEntity entity = masterRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("해당 도면을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setRecordingState(ACTIVE);
        entity.setUpdatedBy(masterAuditActorLookup.nameOf(actorUserId));
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        masterRepository.save(entity);
    }

    @Override
    @Transactional
    public void updateMasterInfo(
            String id,
            String partNo,
            String partName,
            String modelType,
            String actorUserId
    ) {
        DrawingMasterJpaEntity entity = masterRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("해당 도면을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setPartNo(partNo);
        entity.setPartName(partName);
        entity.setModelType(modelType);
        entity.setUpdatedBy(masterAuditActorLookup.nameOf(actorUserId));
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        try {
            masterRepository.save(entity);
        } catch (DataIntegrityViolationException ex) {
            throw new IllegalStateException("이미 사용 중인 품번입니다.");
        }
    }

    @Override
    @Transactional
    public void updateLifecycleStage(String id, DrawingLifecycleStage lifecycleStage, String actorUserId) {
        DrawingMasterJpaEntity entity = masterRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("해당 도면을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setLifecycleStage(lifecycleStage);
        entity.setUpdatedBy(masterAuditActorLookup.nameOf(actorUserId));
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        masterRepository.save(entity);
    }

    @Override
    @Transactional
    public void linkItem(String id, long itemId, String actorUserId) {
        DrawingMasterJpaEntity entity = masterRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("해당 도면을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setItem(resolveItemReference(itemId));
        entity.setItemLinkedAt(now);
        entity.setLifecycleStage(DrawingLifecycleStage.ITEM_LINKED);
        entity.setUpdatedBy(masterAuditActorLookup.nameOf(actorUserId));
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        masterRepository.save(entity);
    }

    @Override
    @Transactional
    public void hardDeleteMaster(String id) {
        masterRepository.deleteById(id);
    }

    @Override
    @Transactional
    public String saveHistory(
            String masterId,
            DrawingType drawingType,
            int majorVersion,
            int minorVersion,
            String filePath,
            long fileSizeBytes,
            String changeType,
            String changeReason
    ) {
        DrawingMasterJpaEntity master = masterRepository.findById(masterId)
                .orElseThrow(() -> new IllegalArgumentException("도면 마스터를 찾을 수 없습니다: " + masterId));

        DrawingHistoryJpaEntity entity = new DrawingHistoryJpaEntity();
        entity.setId(UUID.randomUUID().toString());
        entity.setDrawingMaster(master);
        entity.setDrawingType(drawingType);
        entity.setMajorVersion(majorVersion);
        entity.setMinorVersion(minorVersion);
        entity.setFilePath(filePath);
        entity.setFileSizeBytes(fileSizeBytes);
        entity.setIsLatest("Y");
        entity.setChangeType(changeType);
        entity.setChangeReason(changeReason);
        entity.setCreatedAt(Instant.now());
        return historyRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void markHistoryAsOld(String historyId) {
        DrawingHistoryJpaEntity entity = historyRepository.findById(historyId)
                .orElseThrow(() -> new IllegalArgumentException("이력을 찾을 수 없습니다: " + historyId));
        entity.setIsLatest("N");
        historyRepository.save(entity);
    }

    @Override
    @Transactional(readOnly = true)
    public int findMaxProdMajorVersion(String masterId) {
        return historyRepository.findMaxProdMajorByMasterId(masterId);
    }

    @Override
    @Transactional(readOnly = true)
    public List<DrawingListView> findLatestActiveDrawings() {
        return historyRepository.findLatestActiveHistories().stream()
                .map(this::toListView)
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public List<DrawingListView> findLatestDeletedDrawings() {
        return historyRepository.findLatestDeletedHistories().stream()
                .map(this::toListView)
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public Set<String> findActiveMasterIdsMatchingHistoryQuery(String query) {
        if (query == null || query.isBlank()) {
            return Set.of();
        }
        return new HashSet<>(historyRepository.findActiveMasterIdsMatchingHistoryQuery(query.trim()));
    }

    @Override
    @Transactional(readOnly = true)
    public List<DrawingHistoryView> findHistoriesByMasterId(String masterId) {
        return historyRepository.findByDrawingMasterIdOrderByCreatedAtDesc(masterId).stream()
                .map(this::toHistoryView)
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<DrawingHistoryDetailView> findHistoryById(String historyId) {
        return historyRepository.findByIdWithMaster(historyId).map(this::toHistoryDetailView);
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<DrawingHistoryDetailView> findLatestActiveHistoryByPartNo(String partNo) {
        return historyRepository.findLatestActiveByPartNo(partNo).map(this::toHistoryDetailView);
    }

    @Override
    @Transactional(readOnly = true)
    public List<String> findFilePathsByMasterId(String masterId) {
        return historyRepository.findByDrawingMasterIdOrderByCreatedAtDesc(masterId).stream()
                .map(DrawingHistoryJpaEntity::getFilePath)
                .toList();
    }

    @Override
    @Transactional
    public void deleteAllHistoriesByMasterId(String masterId) {
        historyRepository.deleteByDrawingMasterId(masterId);
    }

    private DrawingMasterView toMasterView(DrawingMasterJpaEntity entity) {
        ItemJpaEntity item = entity.getItem();
        CompanyJpaEntity partner = entity.getSourcePartner();
        return new DrawingMasterView(
                entity.getId(),
                entity.getPartNo(),
                entity.getPartName(),
                entity.getModelType(),
                item != null ? item.getId() : null,
                entity.getLifecycleStage(),
                partner != null ? partner.getId() : null,
                entity.getItemLinkedAt(),
                entity.getRecordingState()
        );
    }

    private DrawingListView toListView(DrawingHistoryJpaEntity history) {
        DrawingMasterJpaEntity master = history.getDrawingMaster();
        ItemJpaEntity item = master.getItem();
        CompanyJpaEntity partner = master.getSourcePartner();
        return new DrawingListView(
                master.getId(),
                master.getPartNo(),
                master.getPartName(),
                master.getModelType(),
                item != null ? item.getId() : null,
                item != null ? item.getItemNo() : null,
                master.getLifecycleStage(),
                partner != null ? partner.getId() : null,
                partner != null ? partner.getCompanyName() : null,
                master.getItemLinkedAt(),
                history.getMajorVersion(),
                history.getMinorVersion(),
                history.getCreatedAt(),
                history.getDrawingType()
        );
    }

    private CompanyJpaEntity resolveCompanyReference(Long companyId) {
        if (companyId == null) {
            return null;
        }
        return entityManager.getReference(CompanyJpaEntity.class, companyId);
    }

    private ItemJpaEntity resolveItemReference(Long itemId) {
        if (itemId == null) {
            return null;
        }
        return entityManager.getReference(ItemJpaEntity.class, itemId);
    }

    private DrawingHistoryView toHistoryView(DrawingHistoryJpaEntity history) {
        return new DrawingHistoryView(
                history.getId(),
                history.getMajorVersion(),
                history.getMinorVersion(),
                history.getIsLatest(),
                history.getChangeType(),
                history.getChangeReason(),
                history.getCreatedAt()
        );
    }

    private DrawingHistoryDetailView toHistoryDetailView(DrawingHistoryJpaEntity history) {
        DrawingMasterJpaEntity master = history.getDrawingMaster();
        return new DrawingHistoryDetailView(
                history.getId(),
                master.getId(),
                master.getPartNo(),
                master.getPartName(),
                master.getModelType(),
                history.getDrawingType(),
                history.getMajorVersion(),
                history.getMinorVersion(),
                history.getFilePath(),
                history.getFileSizeBytes(),
                history.getIsLatest(),
                master.getRecordingState(),
                history.getCreatedAt()
        );
    }
}
