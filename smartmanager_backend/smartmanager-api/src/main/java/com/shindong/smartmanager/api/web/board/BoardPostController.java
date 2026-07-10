package com.shindong.smartmanager.api.web.board;

import com.shindong.smartmanager.api.security.CommunityAuthorize;
import com.shindong.smartmanager.api.security.JwtUserPrincipal;
import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.board.BoardPostCommand;
import com.shindong.smartmanager.application.board.BoardPostListCriteria;
import com.shindong.smartmanager.application.board.BoardReplyCommand;
import com.shindong.smartmanager.domain.board.BoardType;
import com.shindong.smartmanager.infrastructure.application.BoardPostApplicationService;
import java.nio.charset.StandardCharsets;
import java.util.List;
import org.springframework.core.io.InputStreamResource;
import org.springframework.http.ContentDisposition;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RequestPart;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

@RestController
@RequestMapping("/api/v1/boards")
@CommunityAuthorize.Read
public class BoardPostController {

    private final BoardPostApplicationService boardPostApplicationService;

    public BoardPostController(BoardPostApplicationService boardPostApplicationService) {
        this.boardPostApplicationService = boardPostApplicationService;
    }

    @GetMapping("/editor-template")
    public BoardEditorTemplateResponse editorTemplate() {
        return new BoardEditorTemplateResponse(boardPostApplicationService.editorTemplate());
    }

    @GetMapping("/{boardType}/posts")
    public BoardPostPageResponse list(
            @PathVariable String boardType,
            @RequestParam(required = false) String keyword,
            @RequestParam(defaultValue = "0") int page,
            @RequestParam(defaultValue = "20") int size
    ) {
        BoardType type = BoardPostApplicationService.parseBoardType(boardType);
        return BoardPostPageResponse.from(boardPostApplicationService.list(
                new BoardPostListCriteria(type, keyword, page, size)
        ));
    }

    @GetMapping("/{boardType}/posts/{postId}")
    public BoardPostDetailResponse get(@PathVariable String boardType, @PathVariable long postId) {
        BoardPostApplicationService.parseBoardType(boardType);
        var principal = SecurityUtils.requirePrincipal();
        return BoardPostDetailResponse.from(boardPostApplicationService.getDetail(postId, principal.userId()));
    }

    @PostMapping(path = "/{boardType}/posts", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    @ResponseStatus(HttpStatus.CREATED)
    @CommunityAuthorize.Write
    public BoardPostDetailResponse create(
            @PathVariable String boardType,
            @RequestPart("title") String title,
            @RequestPart(value = "content", required = false) String content,
            @RequestPart(value = "files", required = false) List<MultipartFile> files
    ) {
        JwtUserPrincipal principal = SecurityUtils.requirePrincipal();
        BoardType type = BoardPostApplicationService.parseBoardType(boardType);
        return BoardPostDetailResponse.from(boardPostApplicationService.createTopPost(
                new BoardPostCommand(type, title, content, List.of()),
                BoardMultipartSupport.toUploadFiles(files),
                principal.userId(),
                principal.loginId(),
                String.valueOf(principal.userId())
        ));
    }

    @PostMapping(path = "/{boardType}/posts/{postId}/replies", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    @ResponseStatus(HttpStatus.CREATED)
    @CommunityAuthorize.Write
    public BoardPostDetailResponse reply(
            @PathVariable String boardType,
            @PathVariable long postId,
            @RequestPart("content") String content,
            @RequestPart(value = "files", required = false) List<MultipartFile> files
    ) {
        JwtUserPrincipal principal = SecurityUtils.requirePrincipal();
        BoardPostApplicationService.parseBoardType(boardType);
        return BoardPostDetailResponse.from(boardPostApplicationService.createReply(
                postId,
                new BoardReplyCommand(content, List.of()),
                BoardMultipartSupport.toUploadFiles(files),
                principal.userId(),
                principal.loginId(),
                String.valueOf(principal.userId())
        ));
    }

    @PutMapping(path = "/{boardType}/posts/{postId}", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    @CommunityAuthorize.Write
    public BoardPostDetailResponse update(
            @PathVariable String boardType,
            @PathVariable long postId,
            @RequestPart(value = "title", required = false) String title,
            @RequestPart(value = "content", required = false) String content
    ) {
        JwtUserPrincipal principal = SecurityUtils.requirePrincipal();
        BoardPostApplicationService.parseBoardType(boardType);
        return BoardPostDetailResponse.from(boardPostApplicationService.updatePost(
                postId,
                title,
                content,
                principal.userId(),
                hasModerateAuthority(principal),
                principal.loginId(),
                String.valueOf(principal.userId())
        ));
    }

    @DeleteMapping("/{boardType}/posts/{postId}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @CommunityAuthorize.Write
    public void delete(@PathVariable String boardType, @PathVariable long postId) {
        JwtUserPrincipal principal = SecurityUtils.requirePrincipal();
        BoardPostApplicationService.parseBoardType(boardType);
        boardPostApplicationService.deletePost(
                postId,
                principal.userId(),
                hasModerateAuthority(principal),
                principal.loginId(),
                String.valueOf(principal.userId())
        );
    }

    @PostMapping(path = "/{boardType}/posts/{postId}/attachments", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    @ResponseStatus(HttpStatus.CREATED)
    @CommunityAuthorize.Write
    public List<BoardAttachmentResponse> addAttachments(
            @PathVariable String boardType,
            @PathVariable long postId,
            @RequestPart("files") List<MultipartFile> files
    ) {
        JwtUserPrincipal principal = SecurityUtils.requirePrincipal();
        BoardPostApplicationService.parseBoardType(boardType);
        return boardPostApplicationService.addAttachments(
                postId,
                BoardMultipartSupport.toUploadFiles(files),
                principal.userId(),
                hasModerateAuthority(principal),
                principal.loginId(),
                String.valueOf(principal.userId())
        ).stream().map(BoardAttachmentResponse::from).toList();
    }

    @PutMapping(path = "/{boardType}/posts/{postId}/attachments/{attachmentId}", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    @CommunityAuthorize.Write
    public BoardAttachmentResponse replaceAttachment(
            @PathVariable String boardType,
            @PathVariable long postId,
            @PathVariable long attachmentId,
            @RequestPart("file") MultipartFile file
    ) {
        JwtUserPrincipal principal = SecurityUtils.requirePrincipal();
        BoardPostApplicationService.parseBoardType(boardType);
        return BoardAttachmentResponse.from(boardPostApplicationService.replaceAttachment(
                attachmentId,
                BoardMultipartSupport.toUploadFile(file),
                principal.userId(),
                hasModerateAuthority(principal)
        ));
    }

    @DeleteMapping("/{boardType}/posts/{postId}/attachments/{attachmentId}")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    @CommunityAuthorize.Write
    public void deleteAttachment(
            @PathVariable String boardType,
            @PathVariable long postId,
            @PathVariable long attachmentId
    ) {
        JwtUserPrincipal principal = SecurityUtils.requirePrincipal();
        BoardPostApplicationService.parseBoardType(boardType);
        boardPostApplicationService.deleteAttachment(
                attachmentId,
                principal.userId(),
                hasModerateAuthority(principal)
        );
    }

    @GetMapping("/{boardType}/posts/{postId}/attachments/{attachmentId}/download")
    public ResponseEntity<InputStreamResource> download(
            @PathVariable String boardType,
            @PathVariable long postId,
            @PathVariable long attachmentId
    ) {
        BoardPostApplicationService.parseBoardType(boardType);
        var download = boardPostApplicationService.downloadAttachment(attachmentId);
        String contentType = download.contentType() != null && !download.contentType().isBlank()
                ? download.contentType()
                : MediaType.APPLICATION_OCTET_STREAM_VALUE;
        ContentDisposition disposition = ContentDisposition.attachment()
                .filename(download.originalFileName(), StandardCharsets.UTF_8)
                .build();
        return ResponseEntity.ok()
                .header(HttpHeaders.CONTENT_DISPOSITION, disposition.toString())
                .contentType(MediaType.parseMediaType(contentType))
                .contentLength(download.fileSize())
                .body(new InputStreamResource(download.inputStream()));
    }

    private boolean hasModerateAuthority(JwtUserPrincipal principal) {
        return principal.authorities().contains("community:board:moderate");
    }
}
