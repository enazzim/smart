package com.shindong.smartmanager.application.drawing;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.domain.drawing.DrawingType;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import java.util.List;

public class DrawingService {

    private final DrawingRepository drawingRepository;
    private final DrawingReferenceService drawingReferenceService;
    private final DomainEventStore domainEventStore;
    private final ItemRepository itemRepository;
    private final DrawingRevisionNotifier drawingRevisionNotifier;

    public DrawingService(
            DrawingRepository drawingRepository,
            DrawingReferenceService drawingReferenceService,
            DomainEventStore domainEventStore,
            ItemRepository itemRepository,
            DrawingRevisionNotifier drawingRevisionNotifier
    ) {
        this.drawingRepository = drawingRepository;
        this.drawingReferenceService = drawingReferenceService;
        this.domainEventStore = domainEventStore;
        this.itemRepository = itemRepository;
        this.drawingRevisionNotifier = drawingRevisionNotifier;
    }

    public String register(DrawingRegisterCommand command, String actorUserId) {
        validatePartInfo(command.partNo(), command.partName(), command.modelType());
        validateItemId(command.itemId());
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
                command.itemId(),
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
        return drawingRepository.findLatestActiveDrawings();
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
        validateItemId(command.itemId());

        DrawingMasterView master = drawingRepository.findMasterById(masterId)
                .orElseThrow(() -> new IllegalArgumentException("해당 도면을 찾을 수 없습니다: " + masterId));

        if (!master.partNo().equals(command.partNo()) && drawingRepository.existsActiveByPartNo(command.partNo())) {
            throw new IllegalStateException("이미 사용 중인 품번입니다.");
        }

        drawingRepository.updateMasterInfo(
                masterId,
                command.partNo(),
                command.partName(),
                command.modelType(),
                command.itemId(),
                actorUserId
        );
        appendEvent(EventTypes.DRAWING_INFO_UPDATED, masterId, actorUserId, command.partNo());
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

        if (latest.drawingType() == DrawingType.PROD) {
            throw new IllegalStateException("이미 양산품입니다.");
        }

        drawingReferenceService.assertProdChildrenForPromote(latest.id());

        drawingRepository.markHistoryAsOld(latest.id());
        String newHistoryId = drawingRepository.saveHistory(
                latest.masterId(),
                DrawingType.PROD,
                1,
                0,
                latest.filePath(),
                latest.fileSizeBytes(),
                "MAJOR",
                "개발품 -> 양산품 이관"
        );
        drawingReferenceService.copySnapshot(latest.id(), newHistoryId, actorUserId);

        drawingRevisionNotifier.notifyMajorRevision(partNo, 1);

        appendEvent(EventTypes.DRAWING_PROMOTED, latest.masterId(), actorUserId, partNo);
        return latest.masterId();
    }

    public boolean existsActivePartNo(String partNo) {
        return drawingRepository.existsActiveByPartNo(partNo);
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

    private void validateItemId(Long itemId) {
        if (itemId == null) {
            return;
        }
        itemRepository.findActiveById(itemId)
                .orElseThrow(() -> new IllegalArgumentException("연결할 품목을 찾을 수 없습니다."));
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
