package com.shindong.smartmanager.api.web.drawing;

import com.shindong.smartmanager.api.security.BasisAuthorize;
import com.shindong.smartmanager.application.drawing.DrawingHistoryDetailView;
import com.shindong.smartmanager.application.drawing.DrawingInfoUpdateCommand;
import com.shindong.smartmanager.application.drawing.DrawingRegisterCommand;
import com.shindong.smartmanager.application.drawing.DrawingReviseCommand;
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
    public List<DrawingListResponse> list() {
        return drawingApplicationService.listActive().stream()
                .map(DrawingListResponse::from)
                .toList();
    }

    @GetMapping("/deleted")
    public List<DrawingListResponse> listDeleted() {
        return drawingApplicationService.listDeleted().stream()
                .map(DrawingListResponse::from)
                .toList();
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
                request.modelGroup(),
                request.itemId(),
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
                request.modelGroup(),
                request.itemId()
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
