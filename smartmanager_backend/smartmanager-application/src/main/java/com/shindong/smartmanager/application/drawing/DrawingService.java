package com.shindong.smartmanager.application.drawing;

import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.domain.drawing.DrawingLifecycleStage;
import com.shindong.smartmanager.domain.drawing.DrawingType;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import java.util.EnumSet;
import java.util.List;
import java.util.Set;

public class DrawingService {

    /** DEV 활성 업무 단계(수동). 보관(ARCHIVED)은 전용 전환으로만 설정 */
    private static final Set<DrawingLifecycleStage> DEV_ACTIVE_STAGES = EnumSet.of(
            DrawingLifecycleStage.RECEIVED,
            DrawingLifecycleStage.SAMPLE,
            DrawingLifecycleStage.PARTNER_REVIEW
    );

    private static final String ARCHIVED_MUTATION_BLOCKED =
            "보관(ARCHIVED) 도면은 개정·버전업·품목 연결·개발 재개·정보 수정을 할 수 없습니다. AS·기출고용 열람만 가능합니다.";

    private final DrawingRepository drawingRepository;
    private final DrawingReferenceService drawingReferenceService;
    private final DomainEventStore domainEventStore;
    private final ItemRepository itemRepository;
    private final CompanyRepository companyRepository;
    private final DrawingRevisionNotifier drawingRevisionNotifier;

    public DrawingService(
            DrawingRepository drawingRepository,
            DrawingReferenceService drawingReferenceService,
            DomainEventStore domainEventStore,
            ItemRepository itemRepository,
            CompanyRepository companyRepository,
            DrawingRevisionNotifier drawingRevisionNotifier
    ) {
        this.drawingRepository = drawingRepository;
        this.drawingReferenceService = drawingReferenceService;
        this.domainEventStore = domainEventStore;
        this.itemRepository = itemRepository;
        this.companyRepository = companyRepository;
        this.drawingRevisionNotifier = drawingRevisionNotifier;
    }

    public String register(DrawingRegisterCommand command, String actorUserId) {
        validatePartInfo(command.partNo(), command.partName(), command.modelType());
        validateSourcePartnerId(command.sourcePartnerId());
        if (command.drawingType() == null) {
            throw new IllegalArgumentException("도면 구분(DEV/PROD)은 필수입니다.");
        }
        if (command.filePath() == null || command.filePath().isBlank()) {
            throw new IllegalArgumentException("도면 파일 경로가 필요합니다.");
        }

        if (drawingRepository.existsActiveByPartNo(command.partNo())) {
            throw new DrawingAlreadyExistsException(
                    "이미 등록되어 사용 중인 품번입니다. 새로운 도면 파일은 '수정' 메뉴를 통해 이력을 추가해 주세요.");
        }

        String masterId = drawingRepository.saveMaster(
                command.partNo(),
                command.partName(),
                command.modelType(),
                command.sourcePartnerId(),
                DrawingLifecycleStage.RECEIVED,
                actorUserId
        );
        drawingRepository.saveHistory(
                masterId,
                command.drawingType(),
                1,
                0,
                command.filePath(),
                command.fileSizeBytes(),
                "MAJOR",
                "최초 등록"
        );

        appendEvent(EventTypes.DRAWING_REGISTERED, masterId, actorUserId, command.partNo());
        return masterId;
    }

    public List<DrawingListView> listActive() {
        return listActive(DrawingListFilter.empty());
    }

    public List<DrawingListView> listActive(DrawingListFilter filter) {
        List<DrawingListView> rows = drawingRepository.findLatestActiveDrawings();
        if (filter == null) {
            return rows;
        }
        Set<String> historyMasterIds = filter.hasHistoryQuery()
                ? drawingRepository.findActiveMasterIdsMatchingHistoryQuery(filter.historyQuery())
                : null;
        return rows.stream()
                .filter(row -> !filter.hasLifecycleStage() || filter.lifecycleStage() == row.lifecycleStage())
                .filter(row -> historyMasterIds == null || historyMasterIds.contains(row.id()))
                .toList();
    }

    public List<DrawingListView> listDeleted() {
        return drawingRepository.findLatestDeletedDrawings();
    }

    public List<DrawingHistoryView> listHistories(String masterId) {
        drawingRepository.findMasterById(masterId)
                .orElseThrow(() -> new IllegalArgumentException("도면을 찾을 수 없습니다."));
        return drawingRepository.findHistoriesByMasterId(masterId);
    }

    public DrawingHistoryDetailView getHistoryById(String historyId) {
        return drawingRepository.findHistoryById(historyId)
                .orElseThrow(() -> new IllegalArgumentException("이력을 찾을 수 없습니다."));
    }

    public DrawingHistoryDetailView getLatestHistoryByPartNo(String partNo) {
        return drawingRepository.findLatestActiveHistoryByPartNo(partNo)
                .orElseThrow(() -> new IllegalArgumentException("해당 품번의 도면을 찾을 수 없습니다: " + partNo));
    }

    public String revise(String partNo, DrawingReviseCommand command, String actorUserId) {
        DrawingHistoryDetailView latest = drawingRepository.findLatestActiveHistoryByPartNo(partNo)
                .orElseThrow(() -> new IllegalArgumentException("해당 품번의 도면을 찾을 수 없습니다: " + partNo));
        assertNotArchived(latest.masterId());

        if (latest.drawingType() == DrawingType.PROD) {
            throw new IllegalStateException(
                    "양산(PROD) 도면은 개정할 수 없습니다. 양산 변경은 reopen-dev 후 개발 단계에서 진행하세요.");
        }
        if ("MAJOR".equalsIgnoreCase(command.changeType()) && latest.drawingType() == DrawingType.PROD) {
            throw new IllegalStateException(
                    "양산(PROD) 도면의 메이저 개정은 reopen-dev를 사용하세요.");
        }

        drawingRepository.markHistoryAsOld(latest.id());

        int newMajor = latest.majorVersion();
        int newMinor = latest.minorVersion();
        if ("MAJOR".equalsIgnoreCase(command.changeType())) {
            newMajor++;
            newMinor = 0;
        } else {
            newMinor++;
        }

        String newHistoryId = drawingRepository.saveHistory(
                latest.masterId(),
                latest.drawingType(),
                newMajor,
                newMinor,
                command.filePath(),
                command.fileSizeBytes(),
                command.changeType(),
                command.changeReason()
        );
        drawingReferenceService.copySnapshot(latest.id(), newHistoryId, actorUserId);

        if ("MAJOR".equalsIgnoreCase(command.changeType())) {
            drawingRevisionNotifier.notifyMajorRevision(partNo, newMajor);
        }

        appendEvent(EventTypes.DRAWING_REVISED, latest.masterId(), actorUserId, partNo);
        return latest.masterId();
    }

    public void softDelete(String partNo, String actorUserId) {
        DrawingHistoryDetailView latest = drawingRepository.findLatestActiveHistoryByPartNo(partNo)
                .orElseThrow(() -> new IllegalArgumentException("해당 품번의 도면을 찾을 수 없습니다: " + partNo));
        drawingRepository.softDeleteMasterByPartNo(partNo, actorUserId);
        appendEvent(EventTypes.DRAWING_DELETED, latest.masterId(), actorUserId, partNo);
    }

    public void restore(String masterId, String actorUserId) {
        DrawingMasterView master = drawingRepository.findMasterById(masterId)
                .orElseThrow(() -> new IllegalArgumentException("해당 도면을 찾을 수 없습니다: " + masterId));

        if (master.recordingState() == 1) {
            throw new IllegalStateException("이미 활성 상태인 도면입니다.");
        }
        if (drawingRepository.existsActiveByPartNo(master.partNo())) {
            throw new IllegalStateException(
                    "이미 동일한 품번이 활성 상태로 존재하여 복구할 수 없습니다. 기존 활성 도면을 먼저 삭제해 주세요.");
        }

        drawingRepository.restoreMaster(masterId, actorUserId);
        appendEvent(EventTypes.DRAWING_RESTORED, masterId, actorUserId, master.partNo());
    }

    public void updateInfo(String masterId, DrawingInfoUpdateCommand command, String actorUserId) {
        validatePartInfo(command.partNo(), command.partName(), command.modelType());

        DrawingMasterView master = drawingRepository.findMasterById(masterId)
                .orElseThrow(() -> new IllegalArgumentException("해당 도면을 찾을 수 없습니다: " + masterId));
        assertNotArchived(master);

        if (!master.partNo().equals(command.partNo()) && drawingRepository.existsActiveByPartNo(command.partNo())) {
            throw new IllegalStateException("이미 사용 중인 품번입니다.");
        }

        drawingRepository.updateMasterInfo(
                masterId,
                command.partNo(),
                command.partName(),
                command.modelType(),
                actorUserId
        );
        appendEvent(EventTypes.DRAWING_INFO_UPDATED, masterId, actorUserId, command.partNo());
    }

    public void linkItem(String masterId, DrawingLinkItemCommand command, String actorUserId) {
        DrawingMasterView master = drawingRepository.findMasterById(masterId)
                .orElseThrow(() -> new IllegalArgumentException("해당 도면을 찾을 수 없습니다: " + masterId));
        assertNotArchived(master);

        DrawingHistoryDetailView latest = drawingRepository.findLatestActiveHistoryByPartNo(master.partNo())
                .orElseThrow(() -> new IllegalArgumentException("도면 이력을 찾을 수 없습니다."));

        if (latest.drawingType() != DrawingType.PROD) {
            throw new IllegalStateException("품목 연결은 양산(PROD) 최신 이력이 있을 때만 가능합니다.");
        }
        if (master.lifecycleStage() != DrawingLifecycleStage.MASS_PROD_READY
                && master.lifecycleStage() != DrawingLifecycleStage.ITEM_LINKED) {
            throw new IllegalStateException(
                    "품목 연결은 lifecycle이 MASS_PROD_READY 또는 ITEM_LINKED 상태일 때만 가능합니다.");
        }

        itemRepository.findActiveById(command.itemId())
                .orElseThrow(() -> new IllegalArgumentException("연결할 품목을 찾을 수 없습니다."));

        drawingRepository.linkItem(masterId, command.itemId(), actorUserId);
        appendEvent(EventTypes.DRAWING_ITEM_LINKED, masterId, actorUserId, master.partNo());
    }

    public void updateLifecycle(String masterId, DrawingLifecycleUpdateCommand command, String actorUserId) {
        DrawingMasterView master = drawingRepository.findMasterById(masterId)
                .orElseThrow(() -> new IllegalArgumentException("해당 도면을 찾을 수 없습니다: " + masterId));

        DrawingHistoryDetailView latest = drawingRepository.findLatestActiveHistoryByPartNo(master.partNo())
                .orElseThrow(() -> new IllegalArgumentException("도면 이력을 찾을 수 없습니다."));

        DrawingLifecycleStage target = command.lifecycleStage();
        if (master.lifecycleStage() == DrawingLifecycleStage.ARCHIVED) {
            if (target == DrawingLifecycleStage.ARCHIVED) {
                return;
            }
            // 보관 해제: 클라이언트가 보낸 단계와 무관하게 서버가 복귀 단계를 확정한다.
            target = resolveUnarchiveStage(master, latest);
        } else {
            validateManualLifecycleUpdate(master, latest, target);
        }

        drawingRepository.updateLifecycleStage(masterId, target, actorUserId);
        appendEvent(EventTypes.DRAWING_LIFECYCLE_UPDATED, masterId, actorUserId, master.partNo());
    }

    public String reopenDev(String partNo, DrawingReopenDevCommand command, String actorUserId) {
        DrawingHistoryDetailView latest = drawingRepository.findLatestActiveHistoryByPartNo(partNo)
                .orElseThrow(() -> new IllegalArgumentException("해당 품번의 도면을 찾을 수 없습니다: " + partNo));
        assertNotArchived(latest.masterId());

        if (latest.drawingType() != DrawingType.PROD) {
            throw new IllegalStateException("개발 재개는 양산(PROD) 최신 이력에서만 가능합니다.");
        }

        int maxProdMajor = drawingRepository.findMaxProdMajorVersion(latest.masterId());
        int newDevMajor = maxProdMajor + 1;

        drawingRepository.markHistoryAsOld(latest.id());
        String reason = command != null && command.reason() != null && !command.reason().isBlank()
                ? command.reason()
                : "양산 변경 재개";
        String newHistoryId = drawingRepository.saveHistory(
                latest.masterId(),
                DrawingType.DEV,
                newDevMajor,
                0,
                latest.filePath(),
                latest.fileSizeBytes(),
                "MAJOR",
                reason
        );
        drawingReferenceService.copySnapshot(latest.id(), newHistoryId, actorUserId);
        drawingRepository.updateLifecycleStage(
                latest.masterId(),
                DrawingLifecycleStage.SAMPLE,
                actorUserId
        );

        appendEvent(EventTypes.DRAWING_REOPENED_DEV, latest.masterId(), actorUserId, partNo);
        return latest.masterId();
    }

    public void hardDelete(String masterId, String actorUserId) {
        DrawingMasterView master = drawingRepository.findMasterById(masterId)
                .orElseThrow(() -> new IllegalArgumentException("해당 도면을 찾을 수 없습니다: " + masterId));

        List<String> filePaths = drawingRepository.findFilePathsByMasterId(masterId);
        for (String filePath : filePaths) {
            try {
                java.nio.file.Files.deleteIfExists(java.nio.file.Paths.get(filePath));
            } catch (java.io.IOException ignored) {
                // 물리 파일 삭제 실패해도 DB 정리는 진행
            }
        }

        List<String> historyIds = drawingRepository.findHistoriesByMasterId(masterId).stream()
                .map(DrawingHistoryView::id)
                .toList();
        drawingReferenceService.deleteReferencesForHistories(historyIds);

        drawingRepository.deleteAllHistoriesByMasterId(masterId);
        drawingRepository.hardDeleteMaster(masterId);
        appendEvent(EventTypes.DRAWING_HARD_DELETED, masterId, actorUserId, master.partNo());
    }

    public String promoteToProd(String partNo, String actorUserId) {
        DrawingHistoryDetailView latest = drawingRepository.findLatestActiveHistoryByPartNo(partNo)
                .orElseThrow(() -> new IllegalArgumentException("도면을 찾을 수 없습니다."));
        assertNotArchived(latest.masterId());

        if (latest.drawingType() == DrawingType.PROD) {
            throw new IllegalStateException("이미 양산품입니다.");
        }

        drawingReferenceService.assertProdChildrenForPromote(latest.id());

        int maxProdMajor = drawingRepository.findMaxProdMajorVersion(latest.masterId());
        int newProdMajor = maxProdMajor + 1;

        drawingRepository.markHistoryAsOld(latest.id());
        String newHistoryId = drawingRepository.saveHistory(
                latest.masterId(),
                DrawingType.PROD,
                newProdMajor,
                0,
                latest.filePath(),
                latest.fileSizeBytes(),
                "MAJOR",
                "개발품 -> 양산품 이관"
        );
        drawingReferenceService.copySnapshot(latest.id(), newHistoryId, actorUserId);

        drawingRepository.updateLifecycleStage(
                latest.masterId(),
                DrawingLifecycleStage.MASS_PROD_READY,
                actorUserId
        );

        drawingRevisionNotifier.notifyMajorRevision(partNo, newProdMajor);

        appendEvent(EventTypes.DRAWING_PROMOTED, latest.masterId(), actorUserId, partNo);
        return latest.masterId();
    }

    public boolean existsActivePartNo(String partNo) {
        return drawingRepository.existsActiveByPartNo(partNo);
    }

    private void validateManualLifecycleUpdate(
            DrawingMasterView master,
            DrawingHistoryDetailView latest,
            DrawingLifecycleStage target
    ) {
        if (target == DrawingLifecycleStage.ITEM_LINKED) {
            throw new IllegalArgumentException("품목 연결은 link-item API를 사용하세요.");
        }
        if (target == DrawingLifecycleStage.MASS_PROD_READY) {
            throw new IllegalArgumentException("양산 준비 상태는 promote API를 사용하세요.");
        }
        if (target == DrawingLifecycleStage.ARCHIVED) {
            return;
        }
        if (latest.drawingType() == DrawingType.PROD) {
            throw new IllegalStateException(
                    "양산 도면의 단계 변경은 보관·reopen-dev·link-item만 가능합니다.");
        }
        if (!DEV_ACTIVE_STAGES.contains(target)) {
            throw new IllegalArgumentException("허용되지 않는 lifecycle 전환입니다: " + target);
        }
    }

    /** 보관 해제 시 복귀 단계 — PROD는 품목 연결 여부, DEV는 샘플 */
    private DrawingLifecycleStage resolveUnarchiveStage(
            DrawingMasterView master,
            DrawingHistoryDetailView latest
    ) {
        if (latest.drawingType() == DrawingType.PROD) {
            return master.itemId() != null
                    ? DrawingLifecycleStage.ITEM_LINKED
                    : DrawingLifecycleStage.MASS_PROD_READY;
        }
        return DrawingLifecycleStage.SAMPLE;
    }

    private void assertNotArchived(String masterId) {
        DrawingMasterView master = drawingRepository.findMasterById(masterId)
                .orElseThrow(() -> new IllegalArgumentException("해당 도면을 찾을 수 없습니다: " + masterId));
        assertNotArchived(master);
    }

    private void assertNotArchived(DrawingMasterView master) {
        if (master.lifecycleStage() == DrawingLifecycleStage.ARCHIVED) {
            throw new IllegalStateException(ARCHIVED_MUTATION_BLOCKED);
        }
    }

    private void validatePartInfo(String partNo, String partName, String modelType) {
        if (partNo == null || partNo.isBlank()) {
            throw new IllegalArgumentException("품번은 필수입니다.");
        }
        if (partName == null || partName.isBlank()) {
            throw new IllegalArgumentException("품명은 필수입니다.");
        }
        if (modelType == null || modelType.isBlank()) {
            throw new IllegalArgumentException("기종은 필수입니다.");
        }
    }

    private void validateSourcePartnerId(Long sourcePartnerId) {
        if (sourcePartnerId == null) {
            return;
        }
        companyRepository.findActiveById(sourcePartnerId)
                .orElseThrow(() -> new IllegalArgumentException("선수신 거래처를 찾을 수 없습니다."));
    }

    private void appendEvent(String eventType, String masterId, String actorUserId, String partNo) {
        domainEventStore.append(DomainEvent.create(
                eventType,
                1,
                AggregateTypes.DRAWING,
                masterId,
                actorUserId,
                """
                {"drawingId":"%s","partNo":"%s"}
                """.formatted(escape(masterId), escape(partNo)).trim()
        ));
    }

    private String escape(String value) {
        return value.replace("\\", "\\\\").replace("\"", "\\\"");
    }
}
