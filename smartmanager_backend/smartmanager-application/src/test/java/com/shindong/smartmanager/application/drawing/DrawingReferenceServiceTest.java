package com.shindong.smartmanager.application.drawing;

import com.shindong.smartmanager.domain.drawing.DrawingType;
import java.time.Instant;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.assertDoesNotThrow;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.junit.jupiter.api.Assertions.assertTrue;

class DrawingReferenceServiceTest {

    private FakeDrawingRepository drawingRepository;
    private FakeDrawingReferenceRepository referenceRepository;
    private DrawingReferenceService service;

    @BeforeEach
    void setUp() {
        drawingRepository = new FakeDrawingRepository();
        referenceRepository = new FakeDrawingReferenceRepository(drawingRepository);
        service = new DrawingReferenceService(drawingRepository, referenceRepository, null, null);
    }

    @Test
    void replaceRejectsDevChildUnderProdParent() {
        drawingRepository.putMaster("m-parent", "ASM", 1);
        drawingRepository.putMaster("m-child", "PART", 1);
        DrawingHistoryDetailView parent = drawingRepository.putHistory(
                "h-parent", "m-parent", "ASM", DrawingType.PROD, true);
        drawingRepository.putHistory("h-child", "m-child", "PART", DrawingType.DEV, true);

        IllegalArgumentException ex = assertThrows(
                IllegalArgumentException.class,
                () -> service.replaceReferences(
                        parent.masterId(),
                        parent.id(),
                        List.of(new DrawingReferenceChildCommand("h-child", "COMPONENT", 1, null)),
                        "admin"
                )
        );
        assertTrue(ex.getMessage().contains("PROD"));
    }

    @Test
    void replaceRejectsCycle() {
        drawingRepository.putMaster("m-a", "A", 1);
        drawingRepository.putMaster("m-b", "B", 1);
        DrawingHistoryDetailView ha = drawingRepository.putHistory("h-a", "m-a", "A", DrawingType.DEV, true);
        DrawingHistoryDetailView hb = drawingRepository.putHistory("h-b", "m-b", "B", DrawingType.DEV, true);
        referenceRepository.insertReference(hb.id(), ha.id(), "COMPONENT", 1, null, "admin");

        IllegalArgumentException ex = assertThrows(
                IllegalArgumentException.class,
                () -> service.replaceReferences(
                        ha.masterId(),
                        ha.id(),
                        List.of(new DrawingReferenceChildCommand(hb.id(), "COMPONENT", 1, null)),
                        "admin"
                )
        );
        assertTrue(ex.getMessage().contains("순환"));
    }

    @Test
    void replaceAllowsValidDevGraph() {
        drawingRepository.putMaster("m-a", "A", 1);
        drawingRepository.putMaster("m-b", "B", 1);
        DrawingHistoryDetailView ha = drawingRepository.putHistory("h-a", "m-a", "A", DrawingType.DEV, true);
        drawingRepository.putHistory("h-b", "m-b", "B", DrawingType.DEV, true);

        assertDoesNotThrow(() -> service.replaceReferences(
                ha.masterId(),
                ha.id(),
                List.of(new DrawingReferenceChildCommand("h-b", "COMPONENT", 1, null)),
                "admin"
        ));
        assertEquals(1, referenceRepository.findActiveByParentHistoryId(ha.id()).size());
    }

    @Test
    void assertProdChildrenForPromoteRejectsDevChild() {
        drawingRepository.putMaster("m-a", "A", 1);
        drawingRepository.putMaster("m-b", "B", 1);
        DrawingHistoryDetailView ha = drawingRepository.putHistory("h-a", "m-a", "A", DrawingType.DEV, true);
        drawingRepository.putHistory("h-b", "m-b", "B", DrawingType.DEV, true);
        referenceRepository.insertReference(ha.id(), "h-b", "COMPONENT", 1, null, "admin");

        IllegalStateException ex = assertThrows(
                IllegalStateException.class,
                () -> service.assertProdChildrenForPromote(ha.id())
        );
        assertTrue(ex.getMessage().contains("PROD"));
    }

    @Test
    void scanIntegrityReportsProdChildViolation() {
        drawingRepository.putMaster("m-a", "A", 1);
        drawingRepository.putMaster("m-b", "B", 1);
        DrawingHistoryDetailView ha = drawingRepository.putHistory("h-a", "m-a", "A", DrawingType.PROD, true);
        drawingRepository.putHistory("h-b", "m-b", "B", DrawingType.DEV, true);
        // bypass replace validation to simulate legacy/bad data
        referenceRepository.insertReference(ha.id(), "h-b", "COMPONENT", 1, null, "admin");

        List<DrawingReferenceIntegrityIssue> issues = service.scanIntegrity();
        assertEquals(1, issues.size());
        assertEquals("PROD_CHILD_NOT_PROD", issues.get(0).code());
    }

    private static final class FakeDrawingRepository implements DrawingRepository {
        private final Map<String, DrawingMasterView> masters = new HashMap<>();
        private final Map<String, DrawingHistoryDetailView> histories = new HashMap<>();

        void putMaster(String id, String partNo, int recordingState) {
            masters.put(id, new DrawingMasterView(id, partNo, partNo, "G", null, recordingState));
        }

        DrawingHistoryDetailView putHistory(
                String id,
                String masterId,
                String partNo,
                DrawingType type,
                boolean latest
        ) {
            DrawingHistoryDetailView view = new DrawingHistoryDetailView(
                    id,
                    masterId,
                    partNo,
                    partNo,
                    "G",
                    type,
                    1,
                    0,
                    "/tmp/" + id + ".pdf",
                    10L,
                    latest ? "Y" : "N",
                    1,
                    Instant.now()
            );
            histories.put(id, view);
            return view;
        }

        @Override
        public boolean existsActiveByPartNo(String partNo) {
            return masters.values().stream().anyMatch(m -> m.partNo().equals(partNo) && m.recordingState() == 1);
        }

        @Override
        public String saveMaster(String partNo, String partName, String modelGroup, Long itemId, String actorUserId) {
            throw new UnsupportedOperationException();
        }

        @Override
        public Optional<DrawingMasterView> findMasterById(String id) {
            return Optional.ofNullable(masters.get(id));
        }

        @Override
        public Optional<DrawingMasterView> findActiveMasterByPartNo(String partNo) {
            return masters.values().stream()
                    .filter(m -> m.partNo().equals(partNo) && m.recordingState() == 1)
                    .findFirst();
        }

        @Override
        public void softDeleteMasterByPartNo(String partNo, String actorUserId) {
            throw new UnsupportedOperationException();
        }

        @Override
        public void restoreMaster(String id, String actorUserId) {
            throw new UnsupportedOperationException();
        }

        @Override
        public void updateMasterInfo(
                String id, String partNo, String partName, String modelGroup, Long itemId, String actorUserId
        ) {
            throw new UnsupportedOperationException();
        }

        @Override
        public void hardDeleteMaster(String id) {
            throw new UnsupportedOperationException();
        }

        @Override
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
            throw new UnsupportedOperationException();
        }

        @Override
        public void markHistoryAsOld(String historyId) {
            throw new UnsupportedOperationException();
        }

        @Override
        public List<DrawingListView> findLatestActiveDrawings() {
            List<DrawingListView> list = new ArrayList<>();
            for (DrawingHistoryDetailView h : histories.values()) {
                if (!"Y".equals(h.isLatest())) {
                    continue;
                }
                DrawingMasterView m = masters.get(h.masterId());
                if (m == null || m.recordingState() != 1) {
                    continue;
                }
                list.add(new DrawingListView(
                        m.id(), m.partNo(), m.partName(), m.modelGroup(), m.itemId(), null,
                        h.majorVersion(), h.minorVersion(), h.createdAt(), h.drawingType()
                ));
            }
            return list;
        }

        @Override
        public List<DrawingListView> findLatestDeletedDrawings() {
            return List.of();
        }

        @Override
        public List<DrawingHistoryView> findHistoriesByMasterId(String masterId) {
            return histories.values().stream()
                    .filter(h -> h.masterId().equals(masterId))
                    .map(h -> new DrawingHistoryView(
                            h.id(), h.majorVersion(), h.minorVersion(), h.isLatest(),
                            "MAJOR", "", h.createdAt()))
                    .toList();
        }

        @Override
        public Optional<DrawingHistoryDetailView> findHistoryById(String historyId) {
            return Optional.ofNullable(histories.get(historyId));
        }

        @Override
        public Optional<DrawingHistoryDetailView> findLatestActiveHistoryByPartNo(String partNo) {
            return histories.values().stream()
                    .filter(h -> h.partNo().equals(partNo) && "Y".equals(h.isLatest()) && h.masterRecordingState() == 1)
                    .findFirst();
        }

        @Override
        public List<String> findFilePathsByMasterId(String masterId) {
            return List.of();
        }

        @Override
        public void deleteAllHistoriesByMasterId(String masterId) {
            throw new UnsupportedOperationException();
        }
    }

    private static final class FakeDrawingReferenceRepository implements DrawingReferenceRepository {
        private final FakeDrawingRepository drawings;
        private final List<DrawingReferenceView> rows = new ArrayList<>();

        FakeDrawingReferenceRepository(FakeDrawingRepository drawings) {
            this.drawings = drawings;
        }

        @Override
        public List<DrawingReferenceView> findActiveByParentHistoryId(String parentHistoryId) {
            return rows.stream().filter(r -> r.parentHistoryId().equals(parentHistoryId)).toList();
        }

        @Override
        public List<DrawingWhereUsedView> findActiveByChildHistoryId(String childHistoryId) {
            return List.of();
        }

        @Override
        public List<String> findActiveChildHistoryIds(String parentHistoryId) {
            return rows.stream()
                    .filter(r -> r.parentHistoryId().equals(parentHistoryId))
                    .map(DrawingReferenceView::childHistoryId)
                    .toList();
        }

        @Override
        public void deleteAllByParentHistoryId(String parentHistoryId) {
            rows.removeIf(r -> r.parentHistoryId().equals(parentHistoryId));
        }

        @Override
        public void insertReference(
                String parentHistoryId,
                String childHistoryId,
                String refRole,
                int sortOrder,
                String remark,
                String actorUserId
        ) {
            DrawingHistoryDetailView child = drawings.findHistoryById(childHistoryId).orElseThrow();
            rows.add(new DrawingReferenceView(
                    "ref-" + rows.size(),
                    parentHistoryId,
                    childHistoryId,
                    refRole,
                    sortOrder,
                    remark,
                    new DrawingReferencePeerView(
                            child.id(),
                            child.masterId(),
                            child.partNo(),
                            child.partName(),
                            child.drawingType(),
                            child.majorVersion(),
                            child.minorVersion(),
                            null,
                            null
                    )
            ));
        }

        @Override
        public void copyActiveReferences(String fromParentHistoryId, String toParentHistoryId, String actorUserId) {
            throw new UnsupportedOperationException();
        }

        @Override
        public void deleteByHistoryIds(Collection<String> historyIds) {
            rows.removeIf(r -> historyIds.contains(r.parentHistoryId()) || historyIds.contains(r.childHistoryId()));
        }
    }
}
