package com.shindong.smartmanager.application.board;

import com.shindong.smartmanager.domain.board.BoardType;
import com.shindong.smartmanager.domain.board.PostKind;
import java.util.List;
import java.util.Optional;

public interface BoardPostRepository {

    record BoardPostRecord(
            long id,
            BoardType boardType,
            PostKind postKind,
            Long parentPostId,
            Long threadRootId,
            String title,
            String content,
            Long authorUserId,
            int viewCount,
            boolean pinned,
            int recordingState,
            java.time.Instant createdAt,
            java.time.Instant updatedAt
    ) {
    }

    record BoardAttachmentRecord(
            long id,
            long postId,
            String originalFileName,
            String storedFileName,
            String contentType,
            long fileSize,
            int recordingState,
            java.time.Instant createdAt
    ) {
    }

    long saveTopPost(
            BoardType boardType,
            String title,
            String content,
            long authorUserId,
            String actorLoginId,
            String actorUserId
    );

    long saveReply(
            long parentPostId,
            String content,
            long authorUserId,
            String actorLoginId,
            String actorUserId
    );

    void updateThreadRootId(long postId, long threadRootId);

    void updatePost(long postId, String title, String content, String actorLoginId, String actorUserId);

    void setPinned(long postId, boolean pinned, String actorLoginId, String actorUserId);

    void incrementViewCount(long postId);

    void softDeletePost(long postId, String actorLoginId, String actorUserId);

    void softDeletePosts(List<Long> postIds, String actorLoginId, String actorUserId);

    Optional<BoardPostRecord> findActiveById(long id);

    List<BoardPostRecord> findActiveTopPosts(BoardPostListCriteria criteria);

    long countActiveTopPosts(BoardType boardType, String keyword);

    List<BoardPostRecord> findActiveReplies(long threadRootId);

    List<BoardPostRecord> findRecentTopPosts(BoardType boardType, int limit);

    List<Long> findActiveReplyIdsByThreadRoot(long threadRootId);

    long saveAttachment(long postId, BoardAttachmentInput attachment);

    void updateAttachment(long attachmentId, BoardAttachmentInput attachment);

    Optional<BoardAttachmentRecord> findActiveAttachmentById(long attachmentId);

    List<BoardAttachmentRecord> findActiveAttachmentsByPostId(long postId);

    List<BoardAttachmentRecord> findActiveAttachmentsByPostIds(List<Long> postIds);

    void softDeleteAttachment(long attachmentId);

    void softDeleteAttachmentsByPostIds(List<Long> postIds);
}
