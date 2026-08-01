package com.shindong.smartmanager.api.web.drawing;

import com.shindong.smartmanager.api.security.BasisAuthorize;
import com.shindong.smartmanager.application.drawing.DrawingHistoryDetailView;
import com.shindong.smartmanager.application.drawing.DrawingInfoUpdateCommand;
import com.shindong.smartmanager.application.drawing.DrawingLifecycleUpdateCommand;
import com.shindong.smartmanager.application.drawing.DrawingLinkItemCommand;
import com.shindong.smartmanager.application.drawing.DrawingListFilter;
import com.shindong.smartmanager.application.drawing.DrawingRegisterCommand;
import com.shindong.smartmanager.application.drawing.DrawingReopenDevCommand;
import com.shindong.smartmanager.application.drawing.DrawingReviseCommand;
import com.shindong.smartmanager.domain.drawing.DrawingLifecycleStage;
import com.shindong.smartmanager.infrastructure.application.DrawingApplicationService;
import com.shindong.smartmanager.infrastructure.drawing.pdf.DrawingPdfStorageService;
import com.shindong.smartmanager.infrastructure.drawing.pdf.DrawingPdfUploadSupport;
import com.shindong.smartmanager.infrastructure.drawing.pdf.DrawingPdfWatermarkService;
import jakarta.validation.Valid;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.List;
import java.util.Map;
import org.springframework.core.io.ByteArrayResource;
import org.springframework.core.io.Resource;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RequestPart;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

@RestController
@RequestMapping("/api/v1/basis/drawings")
@BasisAuthorize.DrawingRead
public class DrawingController {

    private static final String DEFAULT_ACTOR = "local-dev";

    private final DrawingApplicationService drawingApplicationService;
    private final DrawingPdfStorageService drawingPdfStorageService;
    private final DrawingPdfWatermarkService drawingPdfWatermarkService;
    private final DrawingPdfUploadSupport drawingPdfUploadSupport;

    public DrawingController(
            DrawingApplicationService drawingApplicationService,
            DrawingPdfStorageService drawingPdfStorageService,
            DrawingPdfWatermarkService drawingPdfWatermarkService,
            DrawingPdfUploadSupport drawingPdfUploadSupport
    ) {
        this.drawingApplicationService = drawingApplicationService;
        this.drawingPdfStorageService = drawingPdfStorageService;
        this.drawingPdfWatermarkService = drawingPdfWatermarkService;
        this.drawingPdfUploadSupport = drawingPdfUploadSupport;
    }

    @GetMapping("/check-part-no")
    public Map<String, Boolean> checkPartNo(@RequestParam("partNo") String partNo) {
        return Map.of("exists", drawingApplicationService.existsActivePartNo(partNo));
    }

    @GetMapping
    public List<DrawingListResponse> list(
            @RequestParam(value = "lifecycleStage", required = false) DrawingLifecycleStage lifecycleStage,
            @RequestParam(value = "historyQuery", required = false) String historyQuery
    ) {
        return drawingApplicationService.listActive(
                new DrawingListFilter(lifecycleStage, historyQuery)
        ).stream()
                .map(DrawingListResponse::from)
                .toList();
    }

    @GetMapping("/deleted")
    public List<DrawingListResponse> listDeleted() {
        return drawingApplicationService.listDeleted().stream()
                .map(DrawingListResponse::from)
                .toList();
    }

    @GetMapping("/reference-integrity")
    public List<DrawingReferenceIntegrityResponse> referenceIntegrity() {
        return drawingApplicationService.scanReferenceIntegrity().stream()
                .map(DrawingReferenceIntegrityResponse::from)
                .toList();
    }

    @GetMapping("/{id}/reference-candidates")
    public List<DrawingReferenceCandidateResponse> referenceCandidates(@PathVariable("id") String id) {
        return drawingApplicationService.listBomReferenceCandidates(id).stream()
                .map(DrawingReferenceCandidateResponse::from)
                .toList();
    }

    @GetMapping("/history/{historyId}/where-used")
    public List<DrawingReferenceResponse.DrawingWhereUsedResponse> whereUsed(
            @PathVariable("historyId") String historyId
    ) {
        return drawingApplicationService.listWhereUsed(historyId).stream()
                .map(DrawingReferenceResponse::fromWhereUsed)
                .toList();
    }

    @GetMapping("/{id}/history/{historyId}/references")
    public List<DrawingReferenceResponse> references(
            @PathVariable("id") String id,
            @PathVariable("historyId") String historyId
    ) {
        return drawingApplicationService.listReferences(id, historyId).stream()
                .map(DrawingReferenceResponse::from)
                .toList();
    }

    @PutMapping("/{id}/history/{historyId}/references")
    @BasisAuthorize.DrawingWrite
    public ResponseEntity<Map<String, String>> replaceReferences(
            @PathVariable("id") String id,
            @PathVariable("historyId") String historyId,
            @RequestBody DrawingReferenceResponse.ReplaceRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        drawingApplicationService.replaceReferences(
                id,
                historyId,
                request == null ? List.of() : request.toCommands(),
                resolveActor(actorUserId)
        );
        return ResponseEntity.ok(Map.of("message", "도면 구성 참조가 저장되었습니다."));
    }

    @GetMapping("/{id}/history")
    public List<DrawingHistoryResponse> history(@PathVariable("id") String id) {
        return drawingApplicationService.listHistories(id).stream()
                .map(DrawingHistoryResponse::from)
                .toList();
    }

    @GetMapping("/pdf/{partNo}")
    public ResponseEntity<Resource> pdf(
            @PathVariable("partNo") String partNo,
            @RequestParam(value = "historyId", required = false) String historyId
    ) throws IOException {
        DrawingHistoryDetailView history = historyId != null && !historyId.isBlank()
                ? drawingApplicationService.getHistoryById(historyId)
                : drawingApplicationService.getLatestHistoryByPartNo(partNo);

        Path filePath = Path.of(history.filePath());
        if (!Files.exists(filePath) || !Files.isReadable(filePath)) {
            return ResponseEntity.notFound().build();
        }

        boolean isDeleted = history.masterRecordingState() == 0;
        boolean isLatest = "Y".equals(history.isLatest());

        String watermarkText = null;
        if (isDeleted) {
            watermarkText = "삭제된 도면 - 사용 금지";
        } else if (!isLatest) {
            watermarkText = "과거 이력 문서 - 생산 투입 금지";
        }

        if (watermarkText != null) {
            byte[] watermarkedPdf = drawingPdfWatermarkService.addWatermark(filePath.toString(), watermarkText);
            Resource resource = new ByteArrayResource(watermarkedPdf);
            return ResponseEntity.ok()
                    .contentType(MediaType.APPLICATION_PDF)
                    .header(HttpHeaders.CONTENT_DISPOSITION, "inline; filename=\"" + partNo + ".pdf\"")
                    .body(resource);
        }

        byte[] pdfBytes = Files.readAllBytes(filePath);
        Resource resource = new ByteArrayResource(pdfBytes);
        return ResponseEntity.ok()
                .contentType(MediaType.APPLICATION_PDF)
                .header(HttpHeaders.CONTENT_DISPOSITION, "inline; filename=\"" + partNo + ".pdf\"")
                .body(resource);
    }

    @PostMapping("/register")
    @BasisAuthorize.DrawingWrite
    public ResponseEntity<Map<String, String>> register(
            @Valid @RequestPart("data") DrawingRegisterRequest request,
            @RequestPart("file") MultipartFile file,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) throws IOException {
        drawingPdfUploadSupport.validate(file);
        String actor = resolveActor(actorUserId);
        String filePath = drawingPdfStorageService.savePdf(file.getBytes());
        DrawingRegisterCommand command = new DrawingRegisterCommand(
                request.partNo(),
                request.partName(),
                request.modelType(),
                request.sourcePartnerId(),
                request.drawingType(),
                filePath,
                file.getSize()
        );
        String masterId = drawingApplicationService.register(command, actor);
        return ResponseEntity.ok(Map.of(
                "message", "도면이 성공적으로 등록되었습니다.",
                "masterId", masterId
        ));
    }

    @PostMapping("/{partNo}/revise")
    @BasisAuthorize.DrawingWrite
    public ResponseEntity<Map<String, String>> revise(
            @PathVariable("partNo") String partNo,
            @Valid @RequestPart("data") DrawingReviseRequest request,
            @RequestPart("file") MultipartFile file,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) throws IOException {
        drawingPdfUploadSupport.validate(file);
        String actor = resolveActor(actorUserId);
        String filePath = drawingPdfStorageService.savePdf(file.getBytes());
        DrawingReviseCommand command = new DrawingReviseCommand(
                request.changeType(),
                request.changeReason(),
                filePath,
                file.getSize()
        );
        String masterId = drawingApplicationService.revise(partNo, command, actor);
        return ResponseEntity.ok(Map.of(
                "message", "도면 개정이 완료되었습니다.",
                "masterId", masterId
        ));
    }

    @DeleteMapping("/{partNo}")
    @BasisAuthorize.DrawingWrite
    public ResponseEntity<Map<String, String>> softDelete(
            @PathVariable("partNo") String partNo,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        drawingApplicationService.softDelete(partNo, resolveActor(actorUserId));
        return ResponseEntity.ok(Map.of("message", "도면이 삭제 처리되었습니다."));
    }

    @PostMapping("/{partNo}/promote")
    @BasisAuthorize.DrawingWrite
    public ResponseEntity<Map<String, String>> promote(
            @PathVariable("partNo") String partNo,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        drawingApplicationService.promoteToProd(partNo, resolveActor(actorUserId));
        return ResponseEntity.ok(Map.of("message", "성공적으로 양산품으로 이관되었습니다."));
    }

    @PostMapping("/{id}/link-item")
    @BasisAuthorize.DrawingWrite
    public ResponseEntity<Map<String, String>> linkItem(
            @PathVariable("id") String id,
            @Valid @RequestBody DrawingLinkItemRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        drawingApplicationService.linkItem(
                id,
                new DrawingLinkItemCommand(request.itemId()),
                resolveActor(actorUserId)
        );
        return ResponseEntity.ok(Map.of("message", "품목이 연결되었습니다."));
    }

    @PutMapping("/{id}/lifecycle")
    @BasisAuthorize.DrawingWrite
    public ResponseEntity<Map<String, String>> updateLifecycle(
            @PathVariable("id") String id,
            @Valid @RequestBody DrawingLifecycleUpdateRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        drawingApplicationService.updateLifecycle(
                id,
                new DrawingLifecycleUpdateCommand(request.lifecycleStage()),
                resolveActor(actorUserId)
        );
        return ResponseEntity.ok(Map.of("message", "도면 lifecycle이 변경되었습니다."));
    }

    @PostMapping("/{partNo}/reopen-dev")
    @BasisAuthorize.DrawingWrite
    public ResponseEntity<Map<String, String>> reopenDev(
            @PathVariable("partNo") String partNo,
            @RequestBody(required = false) DrawingReopenDevRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        String reason = request != null ? request.reason() : null;
        drawingApplicationService.reopenDev(
                partNo,
                new DrawingReopenDevCommand(reason),
                resolveActor(actorUserId)
        );
        return ResponseEntity.ok(Map.of("message", "양산 도면이 개발 단계로 재개되었습니다."));
    }

    @PostMapping("/{id}/restore")
    @BasisAuthorize.DrawingWrite
    public ResponseEntity<Map<String, String>> restore(
            @PathVariable("id") String id,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        drawingApplicationService.restore(id, resolveActor(actorUserId));
        return ResponseEntity.ok(Map.of("message", "도면이 복구되었습니다."));
    }

    @PutMapping("/{id}/info")
    @BasisAuthorize.DrawingWrite
    public ResponseEntity<Map<String, String>> updateInfo(
            @PathVariable("id") String id,
            @Valid @RequestBody DrawingInfoUpdateRequest request,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        DrawingInfoUpdateCommand command = new DrawingInfoUpdateCommand(
                request.partNo(),
                request.partName(),
                request.modelType()
        );
        drawingApplicationService.updateInfo(id, command, resolveActor(actorUserId));
        return ResponseEntity.ok(Map.of("message", "도면 정보가 성공적으로 수정되었습니다."));
    }

    @DeleteMapping("/{id}/hard")
    @BasisAuthorize.DrawingHardDelete
    public ResponseEntity<Map<String, String>> hardDelete(
            @PathVariable("id") String id,
            @RequestHeader(value = "X-Actor-User-Id", required = false) String actorUserId
    ) {
        drawingApplicationService.hardDelete(id, resolveActor(actorUserId));
        return ResponseEntity.ok(Map.of("message", "도면이 물리적으로 영구 삭제되었습니다."));
    }

    private String resolveActor(String actorUserId) {
        return actorUserId != null && !actorUserId.isBlank() ? actorUserId : DEFAULT_ACTOR;
    }
}
