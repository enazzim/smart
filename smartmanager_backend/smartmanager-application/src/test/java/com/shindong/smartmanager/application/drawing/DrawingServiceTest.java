package com.shindong.smartmanager.application.drawing;

import com.shindong.smartmanager.application.company.CompanyCommand;
import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.company.CompanyUpdateCommand;
import com.shindong.smartmanager.application.company.CompanyView;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemCommand;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemUpdateCommand;
import com.shindong.smartmanager.application.item.ItemView;
import com.shindong.smartmanager.domain.drawing.DrawingLifecycleStage;
import com.shindong.smartmanager.domain.drawing.DrawingType;
import com.shindong.smartmanager.domain.event.DomainEvent;
import java.time.Instant;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.Set;
import java.util.UUID;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.junit.jupiter.api.Assertions.assertTrue;

class DrawingServiceTest {

    private InMemoryDrawingRepository drawingRepository;
    private DrawingService service;

    @BeforeEach
    void setUp() {
        drawingRepository = new InMemoryDrawingRepository();
        service = new DrawingService(
                drawingRepository,
                new NoOpDrawingReferenceService(),
                new NoOpDomainEventStore(),
                new FakeItemRepository(),
                new FakeCompanyRepository(),
                (partNo, major) -> { }
        );
    }

    @Test
    void promoteThenLinkItemSucceeds() {
        String masterId = registerDev("P-001");
        service.promoteToProd("P-001", "admin");

        service.linkItem(masterId, new DrawingLinkItemCommand(100L), "admin");

        DrawingMasterView master = drawingRepository.findMasterById(masterId).orElseThrow();
        assertEquals(DrawingLifecycleStage.ITEM_LINKED, master.lifecycleStage());
        assertEquals(100L, master.itemId());
        assertTrue(master.itemLinkedAt() != null);
    }

    @Test
    void linkItemRejectsWhenLatestIsDev() {
        String masterId = registerDev("P-002");

        IllegalStateException ex = assertThrows(
                IllegalStateException.class,
                () -> service.linkItem(masterId, new DrawingLinkItemCommand(100L), "admin")
        );
        assertTrue(ex.getMessage().contains("PROD"));
    }

    @Test
    void reviseRejectsWhenLatestIsProd() {
        registerDev("P-003");
        service.promoteToProd("P-003", "admin");

        IllegalStateException ex = assertThrows(
                IllegalStateException.class,
                () -> service.revise(
                        "P-003",
                        new DrawingReviseCommand("MAJOR", "변경", "/tmp/x.pdf", 1L),
                        "admin"
                )
        );
        assertTrue(ex.getMessage().contains("reopen-dev"));
    }

    @Test
    void reopenDevCreatesNewDevHistoryAndResetsLifecycle() {
        registerDev("P-004");
        service.promoteToProd("P-004", "admin");

        service.reopenDev("P-004", new DrawingReopenDevCommand("양산 변경"), "admin");

        DrawingHistoryDetailView latest = drawingRepository.findLatestActiveHistoryByPartNo("P-004").orElseThrow();
        assertEquals(DrawingType.DEV, latest.drawingType());
        assertEquals(2, latest.majorVersion());

        DrawingMasterView master = drawingRepository.findMasterById(latest.masterId()).orElseThrow();
        assertEquals(DrawingLifecycleStage.SAMPLE, master.lifecycleStage());
    }

    @Test
    void promoteAfterReopenUsesIncrementedProdMajor() {
        registerDev("P-005");
        service.promoteToProd("P-005", "admin");
        service.reopenDev("P-005", new DrawingReopenDevCommand(null), "admin");
        service.promoteToProd("P-005", "admin");

        DrawingHistoryDetailView latest = drawingRepository.findLatestActiveHistoryByPartNo("P-005").orElseThrow();
        assertEquals(DrawingType.PROD, latest.drawingType());
        assertEquals(2, latest.majorVersion());
    }

    @Test
    void updateLifecycleRejectsItemLinkedViaPut() {
        String masterId = registerDev("P-006");

        IllegalArgumentException ex = assertThrows(
                IllegalArgumentException.class,
                () -> service.updateLifecycle(
                        masterId,
                        new DrawingLifecycleUpdateCommand(DrawingLifecycleStage.ITEM_LINKED),
                        "admin"
                )
        );
        assertTrue(ex.getMessage().contains("link-item"));
    }

    @Test
    void archiveProdBlocksReopenReviseAndLink() {
        String masterId = registerDev("P-007");
        service.promoteToProd("P-007", "admin");
        service.updateLifecycle(
                masterId,
                new DrawingLifecycleUpdateCommand(DrawingLifecycleStage.ARCHIVED),
                "admin"
        );

        IllegalStateException reopenEx = assertThrows(
                IllegalStateException.class,
                () -> service.reopenDev("P-007", new DrawingReopenDevCommand("재개"), "admin")
        );
        assertTrue(reopenEx.getMessage().contains("보관"));

        IllegalStateException reviseEx = assertThrows(
                IllegalStateException.class,
                () -> service.revise(
                        "P-007",
                        new DrawingReviseCommand("MINOR", "사유", "/tmp/p7.pdf", 1L),
                        "admin"
                )
        );
        assertTrue(reviseEx.getMessage().contains("보관"));

        IllegalStateException linkEx = assertThrows(
                IllegalStateException.class,
                () -> service.linkItem(masterId, new DrawingLinkItemCommand(100L), "admin")
        );
        assertTrue(linkEx.getMessage().contains("보관"));
    }

    @Test
    void archiveDevBlocksPromote() {
        String masterId = registerDev("P-008");
        service.updateLifecycle(
                masterId,
                new DrawingLifecycleUpdateCommand(DrawingLifecycleStage.ARCHIVED),
                "admin"
        );

        IllegalStateException ex = assertThrows(
                IllegalStateException.class,
                () -> service.promoteToProd("P-008", "admin")
        );
        assertTrue(ex.getMessage().contains("보관"));
    }

    @Test
    void unarchiveIgnoresClientStageAndRestoresByRule() {
        String masterId = registerDev("P-009");
        service.promoteToProd("P-009", "admin");
        service.updateLifecycle(
                masterId,
                new DrawingLifecycleUpdateCommand(DrawingLifecycleStage.ARCHIVED),
                "admin"
        );

        // 클라이언트가 SAMPLE을 보내도 PROD 보관 해제는 MASS_PROD_READY로 복귀
        service.updateLifecycle(
                masterId,
                new DrawingLifecycleUpdateCommand(DrawingLifecycleStage.SAMPLE),
                "admin"
        );

        assertEquals(
                DrawingLifecycleStage.MASS_PROD_READY,
                drawingRepository.findMasterById(masterId).orElseThrow().lifecycleStage()
        );
    }

    @Test
    void unarchiveProdRestoresMassProdReady() {
        String masterId = registerDev("P-010");
        service.promoteToProd("P-010", "admin");
        service.updateLifecycle(
                masterId,
                new DrawingLifecycleUpdateCommand(DrawingLifecycleStage.ARCHIVED),
                "admin"
        );

        service.updateLifecycle(
                masterId,
                new DrawingLifecycleUpdateCommand(DrawingLifecycleStage.MASS_PROD_READY),
                "admin"
        );

        assertEquals(
                DrawingLifecycleStage.MASS_PROD_READY,
                drawingRepository.findMasterById(masterId).orElseThrow().lifecycleStage()
        );
    }

    @Test
    void unarchiveDevRestoresSample() {
        String masterId = registerDev("P-011");
        service.updateLifecycle(
                masterId,
                new DrawingLifecycleUpdateCommand(DrawingLifecycleStage.ARCHIVED),
                "admin"
        );

        service.updateLifecycle(
                masterId,
                new DrawingLifecycleUpdateCommand(DrawingLifecycleStage.SAMPLE),
                "admin"
        );

        assertEquals(
                DrawingLifecycleStage.SAMPLE,
                drawingRepository.findMasterById(masterId).orElseThrow().lifecycleStage()
        );
    }

    private String registerDev(String partNo) {
        return service.register(
                new DrawingRegisterCommand(
                        partNo,
                        partNo,
                        "G",
                        null,
                        DrawingType.DEV,
                        "/tmp/" + partNo + ".pdf",
                        10L
                ),
                "admin"
        );
    }

    private static final class InMemoryDrawingRepository implements DrawingRepository {
        private final Map<String, DrawingMasterView> masters = new HashMap<>();
        private final Map<String, DrawingHistoryDetailView> histories = new HashMap<>();

        @Override
        public boolean existsActiveByPartNo(String partNo) {
            return masters.values().stream()
                    .anyMatch(m -> m.partNo().equals(partNo) && m.recordingState() == 1);
        }

        @Override
        public String saveMaster(
                String partNo,
                String partName,
                String modelType,
                Long sourcePartnerId,
                DrawingLifecycleStage lifecycleStage,
                String actorUserId
        ) {
            String id = UUID.randomUUID().toString();
            masters.put(id, new DrawingMasterView(
                    id, partNo, partName, modelType, null,
                    lifecycleStage, sourcePartnerId, null, 1));
            return id;
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
                String id, String partNo, String partName, String modelType, String actorUserId
        ) {
            throw new UnsupportedOperationException();
        }

        @Override
        public void updateLifecycleStage(String id, DrawingLifecycleStage lifecycleStage, String actorUserId) {
            DrawingMasterView current = masters.get(id);
            if (current == null) {
                throw new IllegalArgumentException();
            }
            masters.put(id, new DrawingMasterView(
                    current.id(), current.partNo(), current.partName(), current.modelType(),
                    current.itemId(), lifecycleStage, current.sourcePartnerId(),
                    current.itemLinkedAt(), current.recordingState()));
        }

        @Override
        public void linkItem(String id, long itemId, String actorUserId) {
            DrawingMasterView current = masters.get(id);
            if (current == null) {
                throw new IllegalArgumentException();
            }
            Instant now = Instant.now();
            masters.put(id, new DrawingMasterView(
                    current.id(), current.partNo(), current.partName(), current.modelType(),
                    itemId, DrawingLifecycleStage.ITEM_LINKED, current.sourcePartnerId(),
                    now, current.recordingState()));
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
            DrawingMasterView master = masters.get(masterId);
            String id = UUID.randomUUID().toString();
            DrawingHistoryDetailView view = new DrawingHistoryDetailView(
                    id,
                    masterId,
                    master.partNo(),
                    master.partName(),
                    master.modelType(),
                    drawingType,
                    majorVersion,
                    minorVersion,
                    filePath,
                    fileSizeBytes,
                    "Y",
                    1,
                    Instant.now()
            );
            histories.put(id, view);
            return id;
        }

        @Override
        public void markHistoryAsOld(String historyId) {
            DrawingHistoryDetailView current = histories.get(historyId);
            histories.put(historyId, new DrawingHistoryDetailView(
                    current.id(), current.masterId(), current.partNo(), current.partName(),
                    current.modelType(), current.drawingType(), current.majorVersion(),
                    current.minorVersion(), current.filePath(), current.fileSizeBytes(),
                    "N", current.masterRecordingState(), current.createdAt()));
        }

        @Override
        public int findMaxProdMajorVersion(String masterId) {
            return histories.values().stream()
                    .filter(h -> h.masterId().equals(masterId) && h.drawingType() == DrawingType.PROD)
                    .mapToInt(DrawingHistoryDetailView::majorVersion)
                    .max()
                    .orElse(0);
        }

        @Override
        public List<DrawingListView> findLatestActiveDrawings() {
            return List.of();
        }

        @Override
        public List<DrawingListView> findLatestDeletedDrawings() {
            return List.of();
        }

        @Override
        public Set<String> findActiveMasterIdsMatchingHistoryQuery(String query) {
            return Set.of();
        }

        @Override
        public List<DrawingHistoryView> findHistoriesByMasterId(String masterId) {
            return List.of();
        }

        @Override
        public Optional<DrawingHistoryDetailView> findHistoryById(String historyId) {
            return Optional.ofNullable(histories.get(historyId));
        }

        @Override
        public Optional<DrawingHistoryDetailView> findLatestActiveHistoryByPartNo(String partNo) {
            return histories.values().stream()
                    .filter(h -> h.partNo().equals(partNo) && "Y".equals(h.isLatest()))
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

    private static final class NoOpDrawingReferenceService extends DrawingReferenceService {
        NoOpDrawingReferenceService() {
            super(null, null, null, null);
        }

        @Override
        public void copySnapshot(String fromHistoryId, String toHistoryId, String actorUserId) {
            // no-op
        }

        @Override
        public void assertProdChildrenForPromote(String parentHistoryId) {
            // no-op
        }
    }

    private static final class NoOpDomainEventStore implements DomainEventStore {
        @Override
        public void append(DomainEvent event) {
            // no-op
        }
    }

    private static final class FakeItemRepository implements ItemRepository {
        @Override
        public boolean existsActiveByItemNo(String itemNo) {
            return false;
        }

        @Override
        public long save(ItemCommand command, String actorUserId) {
            throw new UnsupportedOperationException();
        }

        @Override
        public void update(long id, ItemUpdateCommand command, String actorUserId) {
            throw new UnsupportedOperationException();
        }

        @Override
        public void updateLotTracked(long id, boolean lotTracked, String actorUserId) {
            throw new UnsupportedOperationException();
        }

        @Override
        public void softDelete(long id, String actorUserId) {
            throw new UnsupportedOperationException();
        }

        @Override
        public List<ItemView> findAllActive(String itemNoQuery, String itemNameQuery) {
            return List.of();
        }

        @Override
        public Optional<ItemView> findActiveById(long id) {
            if (id == 100L) {
                return Optional.of(new ItemView(
                        100L,
                        "ITEM-100",
                        "품목",
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        false,
                        Instant.now()
                ));
            }
            return Optional.empty();
        }

        @Override
        public Optional<ItemView> findActiveByItemNo(String itemNo) {
            return Optional.empty();
        }
    }

    private static final class FakeCompanyRepository implements CompanyRepository {
        @Override
        public boolean existsActiveByBusinessRegNo(String businessRegNo) {
            return false;
        }

        @Override
        public long save(CompanyCommand command, String actorUserId) {
            throw new UnsupportedOperationException();
        }

        @Override
        public void replaceRoles(long companyId, List<com.shindong.smartmanager.domain.company.CompanyRoleType> roles) {
            throw new UnsupportedOperationException();
        }

        @Override
        public List<com.shindong.smartmanager.domain.company.CompanyRoleType> findRoles(long companyId) {
            return List.of();
        }

        @Override
        public List<CompanyView> findAllActive() {
            return List.of();
        }

        @Override
        public Optional<CompanyView> findActiveById(long id) {
            return Optional.empty();
        }

        @Override
        public Optional<CompanyView> findActiveByBusinessRegNo(String businessRegNo) {
            return Optional.empty();
        }

        @Override
        public void update(long id, CompanyUpdateCommand command, String actorUserId) {
            throw new UnsupportedOperationException();
        }

        @Override
        public void softDelete(long id, String actorUserId) {
            throw new UnsupportedOperationException();
        }
    }
}
