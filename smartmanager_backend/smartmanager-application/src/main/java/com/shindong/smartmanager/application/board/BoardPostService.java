package com.shindong.smartmanager.application.board;

import com.shindong.smartmanager.application.user.UserRepository;
import com.shindong.smartmanager.domain.board.BoardType;
import com.shindong.smartmanager.domain.board.PostKind;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class BoardPostService {

    private static final String SYSTEM_ADMIN_ROLE = "SYSTEM_ADMIN";

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

    public BoardPostService(
            BoardPostRepository boardPostRepository,
            BoardFileStorage boardFileStorage,
            UserRepository userRepository,
            long maxFileSizeBytes
    ) {
        this.boardPostRepository = boardPostRepository;
        this.boardFileStorage = boardFileStorage;
        this.userRepository = userRepository;
        this.maxFileSizeBytes = maxFileSizeBytes;
    }

    public String defaultEditorTemplate() {
        return BoardEditorTemplates.DEFAULT_HTML_BODY;
    }

    public BoardPostPageView list(BoardPostListCriteria criteria, long actorUserId) {
        List<BoardPostRepository.BoardPostRecord> posts = boardPostRepository.findActiveTopPosts(criteria);
        Set<Long> unreadRequiredIds = resolveUnreadRequiredPostIds(actorUserId, posts);
        List<BoardPostSummaryView> items = posts.stream()
                .map(post -> toSummary(post, hasAttachment(post.id()), unreadRequiredIds.contains(post.id())))
                .toList();
        long total = boardPostRepository.countActiveTopPosts(criteria.boardType(), criteria.keyword());
        return new BoardPostPageView(items, total, criteria.page(), criteria.size());
    }

    public BoardPostDetailView getDetail(long postId, long actorUserId, boolean incrementViewCount) {
        BoardPostRepository.BoardPostRecord post = findActivePost(postId);
        boolean shouldIncrement = incrementViewCount && !isAuthor(post, actorUserId);
        if (shouldIncrement) {
            boardPostRepository.incrementViewCount(postId);
            post = findActivePost(postId);
        }
        if (shouldRecordNoticeRead(post, actorUserId, incrementViewCount)) {
            boardPostRepository.recordPostRead(postId, actorUserId);
        }
        return toDetail(post, actorUserId, true);
    }

    public BoardPostDetailView createTopPost(
            BoardPostCommand command,
            List<BoardUploadFile> uploadFiles,
            long authorUserId,
            String actorLoginId,
            String actorUserId
    ) {
        validateTopPost(command);
        assertNoticeWriteAllowed(command.boardType(), authorUserId);
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
        if (command.boardType() == BoardType.NOTICE) {
            boardPostRepository.replaceRequiredReaders(
                    postId,
                    normalizeRequiredReaderIds(command.requiredReaderUserIds(), authorUserId)
            );
        }
        return getDetail(postId, authorUserId, false);
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
        assertNoticeWriteAllowed(parent.boardType(), authorUserId);
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
        return getDetail(replyId, authorUserId, false);
    }

    public BoardPostDetailView updatePost(
            long postId,
            String title,
            String content,
            List<Long> requiredReaderUserIds,
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
            if (post.boardType() == BoardType.NOTICE && requiredReaderUserIds != null) {
                boardPostRepository.replaceRequiredReaders(
                        postId,
                        normalizeRequiredReaderIds(requiredReaderUserIds, actorUserId)
                );
            }
        } else {
            boardPostRepository.updatePost(postId, post.title(), normalizeContent(content), actorLoginId, actorUserIdText);
        }
        return getDetail(postId, actorUserId, false);
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

    public List<DashboardWidgetView> dashboardWidgets(int limit, long actorUserId) {
        return DASHBOARD_BOARD_TYPES.stream()
                .map(boardType -> {
                    List<BoardPostRepository.BoardPostRecord> posts =
                            boardPostRepository.findRecentTopPosts(boardType, limit);
                    Set<Long> unreadRequiredIds = resolveUnreadRequiredPostIds(actorUserId, posts);
                    return new DashboardWidgetView(
                            boardType,
                            boardType.displayTitle(),
                            posts.stream()
                                    .map(post -> toSummary(
                                            post,
                                            hasAttachment(post.id()),
                                            unreadRequiredIds.contains(post.id())
                                    ))
                                    .toList()
                    );
                })
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

    private BoardPostDetailView toDetail(BoardPostRepository.BoardPostRecord post, long actorUserId, boolean includeReplies) {
        List<BoardAttachmentView> attachments = boardPostRepository.findActiveAttachmentsByPostId(post.id()).stream()
                .map(this::toAttachmentView)
                .toList();
        List<BoardPostDetailView> replies = List.of();
        if (includeReplies && post.postKind() == PostKind.TOP) {
            replies = boardPostRepository.findActiveReplies(post.threadRootId()).stream()
                    .map(reply -> toDetail(reply, actorUserId, false))
                    .toList();
        }
        List<BoardPostRequiredReaderView> requiredReaders = resolveRequiredReaders(post);
        List<BoardPostReaderView> readers = requiredReaders.isEmpty() ? resolveReaders(post) : List.of();
        boolean canModify = isAuthor(post, actorUserId);
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
                replies,
                readers,
                requiredReaders,
                canModify,
                canModify
        );
    }

    private boolean shouldRecordNoticeRead(
            BoardPostRepository.BoardPostRecord post,
            long actorUserId,
            boolean incrementViewCount
    ) {
        return incrementViewCount
                && post.boardType() == BoardType.NOTICE
                && post.postKind() == PostKind.TOP
                && !isAuthor(post, actorUserId);
    }

    private List<BoardPostReaderView> resolveReaders(BoardPostRepository.BoardPostRecord post) {
        if (post.boardType() != BoardType.NOTICE || post.postKind() != PostKind.TOP) {
            return List.of();
        }
        long authorUserId = post.authorUserId() != null ? post.authorUserId() : -1L;
        return boardPostRepository.findPostReadersExcludingAuthor(post.id(), authorUserId).stream()
                .map(reader -> new BoardPostReaderView(
                        reader.userId(),
                        reader.loginId() != null ? reader.loginId() : "",
                        reader.name() != null ? reader.name() : "",
                        reader.readAt()
                ))
                .toList();
    }

    private List<BoardPostRequiredReaderView> resolveRequiredReaders(BoardPostRepository.BoardPostRecord post) {
        if (post.boardType() != BoardType.NOTICE || post.postKind() != PostKind.TOP) {
            return List.of();
        }
        return boardPostRepository.findRequiredReaders(post.id()).stream()
                .map(reader -> new BoardPostRequiredReaderView(
                        reader.userId(),
                        reader.loginId() != null ? reader.loginId() : "",
                        reader.name() != null ? reader.name() : "",
                        reader.readAt(),
                        reader.read()
                ))
                .toList();
    }

    private Set<Long> resolveUnreadRequiredPostIds(
            long actorUserId,
            List<BoardPostRepository.BoardPostRecord> posts
    ) {
        List<Long> noticeIds = posts.stream()
                .filter(post -> post.boardType() == BoardType.NOTICE && post.postKind() == PostKind.TOP)
                .map(BoardPostRepository.BoardPostRecord::id)
                .toList();
        if (noticeIds.isEmpty()) {
            return Set.of();
        }
        return new HashSet<>(boardPostRepository.findUnreadRequiredPostIds(actorUserId, noticeIds));
    }

    private List<Long> normalizeRequiredReaderIds(List<Long> rawIds, long authorUserId) {
        if (rawIds == null || rawIds.isEmpty()) {
            return List.of();
        }
        Set<Long> unique = new HashSet<>();
        for (Long userId : rawIds) {
            if (userId == null || userId == authorUserId) {
                continue;
            }
            if (userRepository.findActiveById(userId).isEmpty()) {
                throw new IllegalArgumentException("필수 열람 대상 사용자를 찾을 수 없습니다: " + userId);
            }
            unique.add(userId);
        }
        return List.copyOf(unique);
    }

    private void assertNoticeWriteAllowed(BoardType boardType, long authorUserId) {
        if (boardType != BoardType.NOTICE) {
            return;
        }
        boolean systemAdmin = userRepository.findActiveById(authorUserId)
                .map(user -> user.roleCodes().contains(SYSTEM_ADMIN_ROLE))
                .orElse(false);
        if (!systemAdmin) {
            throw new IllegalStateException("공지사항은 시스템 관리자만 작성할 수 있습니다.");
        }
    }

    private boolean isAuthor(BoardPostRepository.BoardPostRecord post, long actorUserId) {
        return post.authorUserId() != null && post.authorUserId() == actorUserId;
    }

    private BoardPostSummaryView toSummary(
            BoardPostRepository.BoardPostRecord post,
            boolean hasAttachment,
            boolean myRequiredUnread
    ) {
        return new BoardPostSummaryView(
                post.id(),
                post.boardType(),
                post.postKind(),
                post.title(),
                resolveAuthorName(post.authorUserId()),
                post.viewCount(),
                post.pinned(),
                hasAttachment,
                myRequiredUnread,
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
    }

    private void assertCanModify(BoardPostRepository.BoardPostRecord post, long actorUserId, boolean moderator) {
        if (!isAuthor(post, actorUserId)) {
            throw new IllegalStateException("게시글을 수정할 권한이 없습니다.");
        }
    }
}
