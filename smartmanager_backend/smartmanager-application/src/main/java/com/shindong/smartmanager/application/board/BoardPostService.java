package com.shindong.smartmanager.application.board;

import com.shindong.smartmanager.application.user.UserRepository;
import com.shindong.smartmanager.domain.board.BoardType;
import com.shindong.smartmanager.domain.board.PostKind;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
import java.util.Optional;
import java.util.Set;

public class BoardPostService {

    private static final List<BoardType> DASHBOARD_BOARD_TYPES = List.of(
            BoardType.NOTICE,
            BoardType.PRESIDENT_NOTICE,
            BoardType.PRODUCT,
            BoardType.LASER,
            BoardType.INSTITUTE,
            BoardType.SALES_QC
    );

    private final BoardPostRepository boardPostRepository;
    private final BoardFileStorage boardFileStorage;
    private final UserRepository userRepository;
    private final long maxFileSizeBytes;
    private final Set<String> allowedExtensions;

    public BoardPostService(
            BoardPostRepository boardPostRepository,
            BoardFileStorage boardFileStorage,
            UserRepository userRepository,
            long maxFileSizeBytes,
            Set<String> allowedExtensions
    ) {
        this.boardPostRepository = boardPostRepository;
        this.boardFileStorage = boardFileStorage;
        this.userRepository = userRepository;
        this.maxFileSizeBytes = maxFileSizeBytes;
        this.allowedExtensions = allowedExtensions;
    }

    public String defaultEditorTemplate() {
        return BoardEditorTemplates.DEFAULT_HTML_BODY;
    }

    public BoardPostPageView list(BoardPostListCriteria criteria) {
        List<BoardPostSummaryView> items = boardPostRepository.findActiveTopPosts(criteria).stream()
                .map(post -> toSummary(post, hasAttachment(post.id())))
                .toList();
        long total = boardPostRepository.countActiveTopPosts(criteria.boardType(), criteria.keyword());
        return new BoardPostPageView(items, total, criteria.page(), criteria.size());
    }

    public BoardPostDetailView getDetail(long postId, boolean incrementViewCount) {
        BoardPostRepository.BoardPostRecord post = findActivePost(postId);
        if (incrementViewCount) {
            boardPostRepository.incrementViewCount(postId);
        }
        return toDetail(post, true);
    }

    public BoardPostDetailView createTopPost(
            BoardPostCommand command,
            List<BoardUploadFile> uploadFiles,
            long authorUserId,
            String actorLoginId,
            String actorUserId
    ) {
        validateTopPost(command);
        String content = normalizeContent(command.content());
        long postId = boardPostRepository.saveTopPost(
                command.boardType(),
                command.title().trim(),
                content,
                authorUserId,
                actorLoginId,
                actorUserId
        );
        boardPostRepository.updateThreadRootId(postId, postId);
        saveUploadFiles(command.boardType(), postId, uploadFiles);
        return getDetail(postId, false);
    }

    public BoardPostDetailView createReply(
            long parentPostId,
            BoardReplyCommand command,
            List<BoardUploadFile> uploadFiles,
            long authorUserId,
            String actorLoginId,
            String actorUserId
    ) {
        BoardPostRepository.BoardPostRecord parent = findActivePost(parentPostId);
        String content = normalizeContent(command.content());
        if (content.isBlank()) {
            throw new IllegalArgumentException("답글 내용을 입력하세요.");
        }
        long replyId = boardPostRepository.saveReply(
                parentPostId,
                content,
                authorUserId,
                actorLoginId,
                actorUserId
        );
        saveUploadFiles(parent.boardType(), replyId, uploadFiles);
        return toDetail(findActivePost(replyId), false);
    }

    public BoardPostDetailView updatePost(
            long postId,
            String title,
            String content,
            long actorUserId,
            boolean moderator,
            String actorLoginId,
            String actorUserIdText
    ) {
        BoardPostRepository.BoardPostRecord post = findActivePost(postId);
        assertCanModify(post, actorUserId, moderator);
        if (post.postKind() == PostKind.TOP) {
            if (title == null || title.isBlank()) {
                throw new IllegalArgumentException("제목을 입력하세요.");
            }
            boardPostRepository.updatePost(postId, title.trim(), normalizeContent(content), actorLoginId, actorUserIdText);
        } else {
            boardPostRepository.updatePost(postId, post.title(), normalizeContent(content), actorLoginId, actorUserIdText);
        }
        return getDetail(postId, false);
    }

    public void deletePost(long postId, long actorUserId, boolean moderator, String actorLoginId, String actorUserIdText) {
        BoardPostRepository.BoardPostRecord post = findActivePost(postId);
        assertCanModify(post, actorUserId, moderator);
        List<Long> targetPostIds = new ArrayList<>();
        targetPostIds.add(postId);
        if (post.postKind() == PostKind.TOP) {
            targetPostIds.addAll(boardPostRepository.findActiveReplyIdsByThreadRoot(post.threadRootId()));
        }
        deleteAttachmentsPhysically(post.boardType(), targetPostIds);
        boardPostRepository.softDeletePosts(targetPostIds, actorLoginId, actorUserIdText);
    }

    public List<BoardAttachmentView> addAttachments(
            long postId,
            List<BoardUploadFile> uploadFiles,
            long actorUserId,
            boolean moderator,
            String actorLoginId,
            String actorUserIdText
    ) {
        BoardPostRepository.BoardPostRecord post = findActivePost(postId);
        assertCanModify(post, actorUserId, moderator);
        saveUploadFiles(post.boardType(), postId, uploadFiles);
        return boardPostRepository.findActiveAttachmentsByPostId(postId).stream()
                .map(this::toAttachmentView)
                .toList();
    }

    public BoardAttachmentView replaceAttachment(
            long attachmentId,
            BoardUploadFile uploadFile,
            long actorUserId,
            boolean moderator
    ) {
        BoardPostRepository.BoardAttachmentRecord attachment = findActiveAttachment(attachmentId);
        BoardPostRepository.BoardPostRecord post = findActivePost(attachment.postId());
        assertCanModify(post, actorUserId, moderator);
        validateUploadFile(uploadFile);
        boardFileStorage.deletePhysical(post.boardType(), attachment.storedFileName());
        BoardFileStorage.StoredFile stored = boardFileStorage.store(
                post.boardType(),
                uploadFile.originalFileName(),
                uploadFile.contentType(),
                uploadFile.fileSize(),
                uploadFile.inputStream()
        );
        boardPostRepository.updateAttachment(
                attachmentId,
                new BoardAttachmentInput(
                        uploadFile.originalFileName(),
                        stored.storedFileName(),
                        stored.contentType(),
                        stored.fileSize()
                )
        );
        return toAttachmentView(findActiveAttachment(attachmentId));
    }

    public void deleteAttachment(long attachmentId, long actorUserId, boolean moderator) {
        BoardPostRepository.BoardAttachmentRecord attachment = findActiveAttachment(attachmentId);
        BoardPostRepository.BoardPostRecord post = findActivePost(attachment.postId());
        assertCanModify(post, actorUserId, moderator);
        boardFileStorage.deletePhysical(post.boardType(), attachment.storedFileName());
        boardPostRepository.softDeleteAttachment(attachmentId);
    }

    public BoardAttachmentDownload downloadAttachment(long attachmentId) {
        BoardPostRepository.BoardAttachmentRecord attachment = findActiveAttachment(attachmentId);
        BoardPostRepository.BoardPostRecord post = findActivePost(attachment.postId());
        InputStream inputStream = boardFileStorage.open(post.boardType(), attachment.storedFileName())
                .orElseThrow(() -> new IllegalStateException("첨부파일을 찾을 수 없습니다."));
        return new BoardAttachmentDownload(
                attachment.originalFileName(),
                attachment.contentType(),
                attachment.fileSize(),
                inputStream
        );
    }

    public List<DashboardWidgetView> dashboardWidgets(int limit) {
        return DASHBOARD_BOARD_TYPES.stream()
                .map(boardType -> new DashboardWidgetView(
                        boardType,
                        boardType.displayTitle(),
                        boardPostRepository.findRecentTopPosts(boardType, limit).stream()
                                .map(post -> toSummary(post, hasAttachment(post.id())))
                                .toList()
                ))
                .toList();
    }

    private void saveUploadFiles(BoardType boardType, long postId, List<BoardUploadFile> uploadFiles) {
        if (uploadFiles == null || uploadFiles.isEmpty()) {
            return;
        }
        for (BoardUploadFile uploadFile : uploadFiles) {
            validateUploadFile(uploadFile);
            BoardFileStorage.StoredFile stored = boardFileStorage.store(
                    boardType,
                    uploadFile.originalFileName(),
                    uploadFile.contentType(),
                    uploadFile.fileSize(),
                    uploadFile.inputStream()
            );
            boardPostRepository.saveAttachment(
                    postId,
                    new BoardAttachmentInput(
                            uploadFile.originalFileName(),
                            stored.storedFileName(),
                            stored.contentType(),
                            stored.fileSize()
                    )
            );
        }
    }

    private void deleteAttachmentsPhysically(BoardType boardType, List<Long> postIds) {
        List<BoardPostRepository.BoardAttachmentRecord> attachments =
                boardPostRepository.findActiveAttachmentsByPostIds(postIds);
        for (BoardPostRepository.BoardAttachmentRecord attachment : attachments) {
            boardFileStorage.deletePhysical(boardType, attachment.storedFileName());
        }
        boardPostRepository.softDeleteAttachmentsByPostIds(postIds);
    }

    private BoardPostDetailView toDetail(BoardPostRepository.BoardPostRecord post, boolean includeReplies) {
        List<BoardAttachmentView> attachments = boardPostRepository.findActiveAttachmentsByPostId(post.id()).stream()
                .map(this::toAttachmentView)
                .toList();
        List<BoardPostDetailView> replies = List.of();
        if (includeReplies && post.postKind() == PostKind.TOP) {
            replies = boardPostRepository.findActiveReplies(post.threadRootId()).stream()
                    .map(reply -> toDetail(reply, false))
                    .toList();
        }
        return new BoardPostDetailView(
                post.id(),
                post.boardType(),
                post.postKind(),
                post.parentPostId(),
                post.threadRootId(),
                post.title(),
                post.content(),
                post.authorUserId() != null ? post.authorUserId() : 0L,
                resolveAuthorName(post.authorUserId()),
                post.viewCount(),
                post.pinned(),
                post.createdAt(),
                post.updatedAt(),
                attachments,
                replies
        );
    }

    private BoardPostSummaryView toSummary(BoardPostRepository.BoardPostRecord post, boolean hasAttachment) {
        return new BoardPostSummaryView(
                post.id(),
                post.boardType(),
                post.postKind(),
                post.title(),
                resolveAuthorName(post.authorUserId()),
                post.viewCount(),
                post.pinned(),
                hasAttachment,
                post.createdAt()
        );
    }

    private boolean hasAttachment(long postId) {
        return !boardPostRepository.findActiveAttachmentsByPostId(postId).isEmpty();
    }

    private BoardAttachmentView toAttachmentView(BoardPostRepository.BoardAttachmentRecord attachment) {
        return new BoardAttachmentView(
                attachment.id(),
                attachment.postId(),
                attachment.originalFileName(),
                attachment.contentType(),
                attachment.fileSize(),
                attachment.createdAt()
        );
    }

    private String resolveAuthorName(Long authorUserId) {
        if (authorUserId == null) {
            return "";
        }
        return userRepository.findActiveById(authorUserId)
                .map(user -> user.name())
                .orElse("");
    }

    private BoardPostRepository.BoardPostRecord findActivePost(long postId) {
        return boardPostRepository.findActiveById(postId)
                .orElseThrow(() -> new IllegalArgumentException("게시글을 찾을 수 없습니다: " + postId));
    }

    private BoardPostRepository.BoardAttachmentRecord findActiveAttachment(long attachmentId) {
        return boardPostRepository.findActiveAttachmentById(attachmentId)
                .orElseThrow(() -> new IllegalArgumentException("첨부파일을 찾을 수 없습니다: " + attachmentId));
    }

    private void validateTopPost(BoardPostCommand command) {
        if (command.title() == null || command.title().isBlank()) {
            throw new IllegalArgumentException("제목을 입력하세요.");
        }
    }

    private String normalizeContent(String content) {
        if (content == null || content.isBlank()) {
            return BoardEditorTemplates.DEFAULT_HTML_BODY;
        }
        return content;
    }

    private void validateUploadFile(BoardUploadFile uploadFile) {
        if (uploadFile == null) {
            throw new IllegalArgumentException("첨부파일이 없습니다.");
        }
        if (uploadFile.fileSize() <= 0) {
            throw new IllegalArgumentException("첨부파일 크기가 올바르지 않습니다.");
        }
        if (uploadFile.fileSize() > maxFileSizeBytes) {
            throw new IllegalArgumentException("첨부파일은 100MB 이하여야 합니다.");
        }
        String extension = extractExtension(uploadFile.originalFileName());
        if (extension.isEmpty() || !allowedExtensions.contains(extension)) {
            throw new IllegalArgumentException("허용되지 않은 첨부파일 형식입니다: " + extension);
        }
    }

    private String extractExtension(String fileName) {
        if (fileName == null) {
            return "";
        }
        int dot = fileName.lastIndexOf('.');
        if (dot < 0 || dot == fileName.length() - 1) {
            return "";
        }
        return fileName.substring(dot + 1).toLowerCase(Locale.ROOT);
    }

    private void assertCanModify(BoardPostRepository.BoardPostRecord post, long actorUserId, boolean moderator) {
        if (moderator) {
            return;
        }
        if (post.authorUserId() == null || post.authorUserId() != actorUserId) {
            throw new IllegalStateException("게시글을 수정할 권한이 없습니다.");
        }
    }
}
