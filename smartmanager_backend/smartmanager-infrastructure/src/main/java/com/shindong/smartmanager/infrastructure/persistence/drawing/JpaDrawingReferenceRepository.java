package com.shindong.smartmanager.infrastructure.persistence.drawing;

import com.shindong.smartmanager.application.drawing.DrawingReferencePeerView;
import com.shindong.smartmanager.application.drawing.DrawingReferenceRepository;
import com.shindong.smartmanager.application.drawing.DrawingReferenceView;
import com.shindong.smartmanager.application.drawing.DrawingWhereUsedView;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import java.time.Instant;
import java.util.Collection;
import java.util.List;
import java.util.UUID;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaDrawingReferenceRepository implements DrawingReferenceRepository {

    private static final int ACTIVE = 1;

    private final SpringDataDrawingReferenceRepository referenceRepository;
    private final SpringDataDrawingHistoryRepository historyRepository;

    public JpaDrawingReferenceRepository(
            SpringDataDrawingReferenceRepository referenceRepository,
            SpringDataDrawingHistoryRepository historyRepository
    ) {
        this.referenceRepository = referenceRepository;
        this.historyRepository = historyRepository;
    }

    @Override
    @Transactional(readOnly = true)
    public List<DrawingReferenceView> findActiveByParentHistoryId(String parentHistoryId) {
        return referenceRepository.findActiveByParentHistoryId(parentHistoryId).stream()
                .map(this::toContainsView)
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public List<DrawingWhereUsedView> findActiveByChildHistoryId(String childHistoryId) {
        return referenceRepository.findActiveByChildHistoryId(childHistoryId).stream()
                .map(this::toWhereUsedView)
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public List<String> findActiveChildHistoryIds(String parentHistoryId) {
        return referenceRepository.findActiveChildHistoryIds(parentHistoryId);
    }

    @Override
    @Transactional
    public void deleteAllByParentHistoryId(String parentHistoryId) {
        referenceRepository.deleteAllByParentHistoryId(parentHistoryId);
    }

    @Override
    @Transactional
    public void insertReference(
            String parentHistoryId,
            String childHistoryId,
            String refRole,
            int sortOrder,
            String remark,
            String actorUserId
    ) {
        DrawingHistoryJpaEntity parent = historyRepository.findById(parentHistoryId)
                .orElseThrow(() -> new IllegalArgumentException("부모 이력을 찾을 수 없습니다."));
        DrawingHistoryJpaEntity child = historyRepository.findById(childHistoryId)
                .orElseThrow(() -> new IllegalArgumentException("자식 이력을 찾을 수 없습니다."));

        Instant now = Instant.now();
        DrawingReferenceJpaEntity entity = new DrawingReferenceJpaEntity();
        entity.setId(UUID.randomUUID().toString());
        entity.setParentHistory(parent);
        entity.setChildHistory(child);
        entity.setRefRole(refRole);
        entity.setSortOrder(sortOrder);
        entity.setRemark(remark);
        entity.setRecordingState(ACTIVE);
        entity.setCreatedBy(actorUserId);
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        referenceRepository.save(entity);
    }

    @Override
    @Transactional
    public void copyActiveReferences(String fromParentHistoryId, String toParentHistoryId, String actorUserId) {
        List<DrawingReferenceJpaEntity> source = referenceRepository.findActiveByParentHistoryId(fromParentHistoryId);
        DrawingHistoryJpaEntity targetParent = historyRepository.findById(toParentHistoryId)
                .orElseThrow(() -> new IllegalArgumentException("대상 이력을 찾을 수 없습니다."));
        Instant now = Instant.now();
        for (DrawingReferenceJpaEntity row : source) {
            DrawingReferenceJpaEntity copy = new DrawingReferenceJpaEntity();
            copy.setId(UUID.randomUUID().toString());
            copy.setParentHistory(targetParent);
            copy.setChildHistory(row.getChildHistory());
            copy.setRefRole(row.getRefRole());
            copy.setSortOrder(row.getSortOrder());
            copy.setRemark(row.getRemark());
            copy.setRecordingState(ACTIVE);
            copy.setCreatedBy(actorUserId);
            copy.setCreatedById(actorUserId);
            copy.setCreatedAt(now);
            copy.setUpdatedBy(actorUserId);
            copy.setUpdatedById(actorUserId);
            copy.setUpdatedAt(now);
            referenceRepository.save(copy);
        }
    }

    @Override
    @Transactional
    public void deleteByHistoryIds(Collection<String> historyIds) {
        if (historyIds == null || historyIds.isEmpty()) {
            return;
        }
        referenceRepository.deleteByHistoryIds(historyIds);
    }

    private DrawingReferenceView toContainsView(DrawingReferenceJpaEntity entity) {
        return new DrawingReferenceView(
                entity.getId(),
                entity.getParentHistory().getId(),
                entity.getChildHistory().getId(),
                entity.getRefRole(),
                entity.getSortOrder(),
                entity.getRemark(),
                toPeer(entity.getChildHistory())
        );
    }

    private DrawingWhereUsedView toWhereUsedView(DrawingReferenceJpaEntity entity) {
        return new DrawingWhereUsedView(
                entity.getId(),
                entity.getParentHistory().getId(),
                entity.getChildHistory().getId(),
                entity.getRefRole(),
                entity.getSortOrder(),
                entity.getRemark(),
                toPeer(entity.getParentHistory())
        );
    }

    private DrawingReferencePeerView toPeer(DrawingHistoryJpaEntity history) {
        DrawingMasterJpaEntity master = history.getDrawingMaster();
        ItemJpaEntity item = master.getItem();
        return new DrawingReferencePeerView(
                history.getId(),
                master.getId(),
                master.getPartNo(),
                master.getPartName(),
                history.getDrawingType(),
                history.getMajorVersion(),
                history.getMinorVersion(),
                item != null ? item.getId() : null,
                item != null ? item.getItemNo() : null
        );
    }
}
