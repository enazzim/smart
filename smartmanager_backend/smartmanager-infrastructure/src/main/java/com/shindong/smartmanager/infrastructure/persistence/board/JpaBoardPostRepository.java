package com.shindong.smartmanager.infrastructure.persistence.board;

import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;

import com.shindong.smartmanager.application.board.BoardAttachmentInput;
import com.shindong.smartmanager.application.board.BoardPostListCriteria;
import com.shindong.smartmanager.application.board.BoardPostRepository;
import com.shindong.smartmanager.domain.board.BoardType;
import com.shindong.smartmanager.domain.board.PostKind;
import java.time.Instant;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.Set;
import org.springframework.data.domain.PageRequest;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaBoardPostRepository implements BoardPostRepository {

    private final SpringDataBoardPostRepository boardPostRepository;
    private final SpringDataBoardAttachmentRepository boardAttachmentRepository;
    private final SpringDataBoardPostReadRepository boardPostReadRepository;
    private final SpringDataBoardPostRequiredReaderRepository boardPostRequiredReaderRepository;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaBoardPostRepository(
            SpringDataBoardPostRepository boardPostRepository,
            SpringDataBoardAttachmentRepository boardAttachmentRepository,
            SpringDataBoardPostReadRepository boardPostReadRepository,
            SpringDataBoardPostRequiredReaderRepository boardPostRequiredReaderRepository,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.boardPostRepository = boardPostRepository;
        this.boardAttachmentRepository = boardAttachmentRepository;
        this.boardPostReadRepository = boardPostReadRepository;
        this.boardPostRequiredReaderRepository = boardPostRequiredReaderRepository;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    @Transactional
    public long saveTopPost(
            BoardType boardType,
            String title,
            String content,
            long authorUserId,
            String actorLoginId,
            String actorUserId
    ) {
        Instant now = Instant.now();
        BoardPostJpaEntity entity = new BoardPostJpaEntity();
        entity.setBoardType(boardType);
        entity.setPostKind(PostKind.TOP);
        entity.setTitle(title);
        entity.setContent(content);
        entity.setAuthorUserId(authorUserId);
        entity.setViewCount(0);
        entity.setPinned(false);
        entity.setRecordingState(1);
        applyAuditOnCreate(entity, actorLoginId, actorUserId, now);
        return boardPostRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public long saveReply(
            long parentPostId,
            String content,
            long authorUserId,
            String actorLoginId,
            String actorUserId
    ) {
        BoardPostJpaEntity parent = boardPostRepository.findActiveById(parentPostId)
                .orElseThrow(() -> new IllegalArgumentException("부모 게시글을 찾을 수 없습니다: " + parentPostId));
        long threadRootId = parent.getThreadRootId() != null ? parent.getThreadRootId() : parent.getId();
        Instant now = Instant.now();
        BoardPostJpaEntity entity = new BoardPostJpaEntity();
        entity.setBoardType(parent.getBoardType());
        entity.setPostKind(PostKind.REPLY);
        entity.setParentPostId(parentPostId);
        entity.setThreadRootId(threadRootId);
        entity.setTitle(null);
        entity.setContent(content);
        entity.setAuthorUserId(authorUserId);
        entity.setViewCount(0);
        entity.setPinned(false);
        entity.setRecordingState(1);
        applyAuditOnCreate(entity, actorLoginId, actorUserId, now);
        return boardPostRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void updateThreadRootId(long postId, long threadRootId) {
        BoardPostJpaEntity entity = boardPostRepository.findById(postId)
                .orElseThrow(() -> new IllegalArgumentException("게시글을 찾을 수 없습니다: " + postId));
        entity.setThreadRootId(threadRootId);
        boardPostRepository.save(entity);
    }

    @Override
    @Transactional
    public void updatePost(long postId, String title, String content, String actorLoginId, String actorUserId) {
        BoardPostJpaEntity entity = boardPostRepository.findActiveById(postId)
                .orElseThrow(() -> new IllegalArgumentException("게시글을 찾을 수 없습니다: " + postId));
        entity.setTitle(title);
        entity.setContent(content);
        applyAuditOnUpdate(entity, actorLoginId, actorUserId, Instant.now());
        boardPostRepository.save(entity);
    }

    @Override
    @Transactional
    public void setPinned(long postId, boolean pinned, String actorLoginId, String actorUserId) {
        BoardPostJpaEntity entity = boardPostRepository.findActiveById(postId)
                .orElseThrow(() -> new IllegalArgumentException("게시글을 찾을 수 없습니다: " + postId));
        entity.setPinned(pinned);
        applyAuditOnUpdate(entity, actorLoginId, actorUserId, Instant.now());
        boardPostRepository.save(entity);
    }

    @Override
    @Transactional
    public void incrementViewCount(long postId) {
        boardPostRepository.incrementViewCount(postId);
    }

    @Override
    @Transactional
    public void recordPostRead(long postId, long readerUserId) {
        if (boardPostReadRepository.findByPostIdAndReaderUserId(postId, readerUserId).isPresent()) {
            return;
        }
        BoardPostReadJpaEntity entity = new BoardPostReadJpaEntity();
        entity.setPostId(postId);
        entity.setReaderUserId(readerUserId);
        entity.setReadAt(Instant.now());
        boardPostReadRepository.save(entity);
    }

    @Override
    @Transactional(readOnly = true)
    public List<BoardPostReaderRecord> findPostReadersExcludingAuthor(long postId, long authorUserId) {
        return boardPostReadRepository.findReadersExcludingAuthor(postId, authorUserId).stream()
                .map(row -> new BoardPostReaderRecord(
                        ((Number) row[0]).longValue(),
                        (String) row[1],
                        (String) row[2],
                        (Instant) row[3]
                ))
                .toList();
    }

    @Override
    @Transactional
    public void replaceRequiredReaders(long postId, List<Long> userIds) {
        boardPostRequiredReaderRepository.deleteByPostId(postId);
        if (userIds == null || userIds.isEmpty()) {
            return;
        }
        Set<Long> unique = new HashSet<>(userIds);
        for (Long userId : unique) {
            if (userId == null) {
                continue;
            }
            boardPostRequiredReaderRepository.save(new BoardPostRequiredReaderJpaEntity(postId, userId));
        }
    }

    @Override
    @Transactional(readOnly = true)
    public List<BoardPostRequiredReaderRecord> findRequiredReaders(long postId) {
        Map<Long, Instant> readAtByUser = new HashMap<>();
        for (BoardPostReadJpaEntity read : boardPostReadRepository.findByPostId(postId)) {
            readAtByUser.put(read.getReaderUserId(), read.getReadAt());
        }
        return boardPostRequiredReaderRepository.findRequiredActiveUsers(postId).stream()
                .map(row -> {
                    long userId = ((Number) row[0]).longValue();
                    Instant readAt = readAtByUser.get(userId);
                    return new BoardPostRequiredReaderRecord(
                            userId,
                            (String) row[1],
                            (String) row[2],
                            readAt,
                            readAt != null
                    );
                })
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public List<Long> findUnreadRequiredPostIds(long userId, List<Long> postIds) {
        if (postIds == null || postIds.isEmpty()) {
            return List.of();
        }
        return boardPostRequiredReaderRepository.findUnreadRequiredPostIds(userId, postIds);
    }

    @Override
    @Transactional
    public void softDeletePost(long postId, String actorLoginId, String actorUserId) {
        softDeletePosts(List.of(postId), actorLoginId, actorUserId);
    }

    @Override
    @Transactional
    public void softDeletePosts(List<Long> postIds, String actorLoginId, String actorUserId) {
        Instant now = Instant.now();
        for (Long postId : postIds) {
            boardPostRepository.findActiveById(postId).ifPresent(entity -> {
                entity.setRecordingState(0);
                applyAuditOnUpdate(entity, actorLoginId, actorUserId, now);
                boardPostRepository.save(entity);
            });
        }
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<BoardPostRecord> findActiveById(long id) {
        return boardPostRepository.findActiveById(id).map(this::toRecord);
    }

    @Override
    @Transactional(readOnly = true)
    public List<BoardPostRecord> findActiveTopPosts(BoardPostListCriteria criteria) {
        int size = Math.max(criteria.size(), 1);
        return boardPostRepository.findActiveTopPosts(
                        criteria.boardType(),
                        criteria.keyword(),
                        PageRequest.of(Math.max(criteria.page(), 0), size)
                )
                .stream()
                .map(this::toRecord)
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public long countActiveTopPosts(BoardType boardType, String keyword) {
        return boardPostRepository.countActiveTopPosts(boardType, keyword);
    }

    @Override
    @Transactional(readOnly = true)
    public List<BoardPostRecord> findActiveReplies(long threadRootId) {
        return boardPostRepository.findActiveReplies(threadRootId).stream()
                .map(this::toRecord)
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public List<BoardPostRecord> findRecentTopPosts(BoardType boardType, int limit) {
        int size = Math.max(limit, 1);
        return boardPostRepository.findRecentTopPosts(boardType, PageRequest.of(0, size)).stream()
                .map(this::toRecord)
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public List<Long> findActiveReplyIdsByThreadRoot(long threadRootId) {
        return boardPostRepository.findActiveReplyIdsByThreadRoot(threadRootId);
    }

    @Override
    @Transactional
    public long saveAttachment(long postId, BoardAttachmentInput attachment) {
        BoardAttachmentJpaEntity entity = new BoardAttachmentJpaEntity();
        entity.setPostId(postId);
        entity.setOriginalFileName(attachment.originalFileName());
        entity.setStoredFileName(attachment.storedFileName());
        entity.setContentType(attachment.contentType());
        entity.setFileSize(attachment.fileSize());
        entity.setRecordingState(1);
        entity.setCreatedAt(Instant.now());
        return boardAttachmentRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void updateAttachment(long attachmentId, BoardAttachmentInput attachment) {
        BoardAttachmentJpaEntity entity = boardAttachmentRepository.findActiveById(attachmentId)
                .orElseThrow(() -> new IllegalArgumentException("첨부파일을 찾을 수 없습니다: " + attachmentId));
        entity.setOriginalFileName(attachment.originalFileName());
        entity.setStoredFileName(attachment.storedFileName());
        entity.setContentType(attachment.contentType());
        entity.setFileSize(attachment.fileSize());
        boardAttachmentRepository.save(entity);
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<BoardAttachmentRecord> findActiveAttachmentById(long attachmentId) {
        return boardAttachmentRepository.findActiveById(attachmentId).map(this::toAttachmentRecord);
    }

    @Override
    @Transactional(readOnly = true)
    public List<BoardAttachmentRecord> findActiveAttachmentsByPostId(long postId) {
        return boardAttachmentRepository.findActiveByPostId(postId).stream()
                .map(this::toAttachmentRecord)
                .toList();
    }

    @Override
    @Transactional(readOnly = true)
    public List<BoardAttachmentRecord> findActiveAttachmentsByPostIds(List<Long> postIds) {
        if (postIds == null || postIds.isEmpty()) {
            return List.of();
        }
        return boardAttachmentRepository.findActiveByPostIds(postIds).stream()
                .map(this::toAttachmentRecord)
                .toList();
    }

    @Override
    @Transactional
    public void softDeleteAttachment(long attachmentId) {
        boardAttachmentRepository.findActiveById(attachmentId).ifPresent(entity -> {
            entity.setRecordingState(0);
            boardAttachmentRepository.save(entity);
        });
    }

    @Override
    @Transactional
    public void softDeleteAttachmentsByPostIds(List<Long> postIds) {
        if (postIds == null || postIds.isEmpty()) {
            return;
        }
        boardAttachmentRepository.softDeleteByPostIds(postIds);
    }

    private BoardPostRecord toRecord(BoardPostJpaEntity entity) {
        return new BoardPostRecord(
                entity.getId(),
                entity.getBoardType(),
                entity.getPostKind(),
                entity.getParentPostId(),
                entity.getThreadRootId(),
                entity.getTitle(),
                entity.getContent(),
                entity.getAuthorUserId(),
                entity.getViewCount(),
                entity.isPinned(),
                entity.getRecordingState(),
                entity.getCreatedAt(),
                entity.getUpdatedAt()
        );
    }

    private BoardAttachmentRecord toAttachmentRecord(BoardAttachmentJpaEntity entity) {
        return new BoardAttachmentRecord(
                entity.getId(),
                entity.getPostId(),
                entity.getOriginalFileName(),
                entity.getStoredFileName(),
                entity.getContentType(),
                entity.getFileSize(),
                entity.getRecordingState(),
                entity.getCreatedAt()
        );
    }

    private void applyAuditOnCreate(BoardPostJpaEntity entity, String actorLoginId, String actorUserId, Instant now) {
        entity.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setCreatedAt(now);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
    }

    private void applyAuditOnUpdate(BoardPostJpaEntity entity, String actorLoginId, String actorUserId, Instant now) {
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
    }
}
