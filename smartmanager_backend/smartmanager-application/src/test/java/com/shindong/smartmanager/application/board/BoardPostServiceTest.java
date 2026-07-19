package com.shindong.smartmanager.application.board;

import static org.junit.jupiter.api.Assertions.assertThrows;

import com.shindong.smartmanager.application.user.UserRepository;
import com.shindong.smartmanager.domain.board.BoardType;
import java.io.ByteArrayInputStream;
import java.nio.charset.StandardCharsets;
import java.util.List;
import java.util.Optional;
import org.junit.jupiter.api.Test;

class BoardPostServiceTest {

    @Test
    void rejectsAttachmentLargerThan100Mb() {
        BoardPostService service = new BoardPostService(
                new NoopBoardPostRepository(),
                new NoopBoardFileStorage(),
                new NoopUserRepository(),
                104857600L
        );

        BoardUploadFile oversized = new BoardUploadFile(
                "large.pdf",
                "application/pdf",
                104857601L,
                new ByteArrayInputStream(new byte[] {1})
        );

        IllegalArgumentException ex = assertThrows(
                IllegalArgumentException.class,
                () -> service.createTopPost(
                        new BoardPostCommand(BoardType.NOTICE, "제목", "<p>본문</p>", List.of()),
                        List.of(oversized),
                        1L,
                        "admin",
                        "1"
                )
        );
        assert ex.getMessage().contains("100MB");
    }

    private static final class NoopBoardPostRepository implements BoardPostRepository {
        @Override
        public long saveTopPost(BoardType boardType, String title, String content, long authorUserId, String actorLoginId, String actorUserId) {
            return 1L;
        }

        @Override
        public long saveReply(long parentPostId, String content, long authorUserId, String actorLoginId, String actorUserId) {
            return 2L;
        }

        @Override
        public void updateThreadRootId(long postId, long threadRootId) {
        }

        @Override
        public void updatePost(long postId, String title, String content, String actorLoginId, String actorUserId) {
        }

        @Override
        public void setPinned(long postId, boolean pinned, String actorLoginId, String actorUserId) {
        }

        @Override
        public void incrementViewCount(long postId) {
        }

        @Override
        public void recordPostRead(long postId, long readerUserId) {
        }

        @Override
        public List<BoardPostReaderRecord> findPostReadersExcludingAuthor(long postId, long authorUserId) {
            return List.of();
        }

        @Override
        public void softDeletePost(long postId, String actorLoginId, String actorUserId) {
        }

        @Override
        public void softDeletePosts(List<Long> postIds, String actorLoginId, String actorUserId) {
        }

        @Override
        public Optional<BoardPostRecord> findActiveById(long id) {
            return Optional.empty();
        }

        @Override
        public List<BoardPostRecord> findActiveTopPosts(BoardPostListCriteria criteria) {
            return List.of();
        }

        @Override
        public long countActiveTopPosts(BoardType boardType, String keyword) {
            return 0L;
        }

        @Override
        public List<BoardPostRecord> findActiveReplies(long threadRootId) {
            return List.of();
        }

        @Override
        public List<BoardPostRecord> findRecentTopPosts(BoardType boardType, int limit) {
            return List.of();
        }

        @Override
        public List<Long> findActiveReplyIdsByThreadRoot(long threadRootId) {
            return List.of();
        }

        @Override
        public long saveAttachment(long postId, BoardAttachmentInput attachment) {
            return 1L;
        }

        @Override
        public void updateAttachment(long attachmentId, BoardAttachmentInput attachment) {
        }

        @Override
        public Optional<BoardAttachmentRecord> findActiveAttachmentById(long attachmentId) {
            return Optional.empty();
        }

        @Override
        public List<BoardAttachmentRecord> findActiveAttachmentsByPostId(long postId) {
            return List.of();
        }

        @Override
        public List<BoardAttachmentRecord> findActiveAttachmentsByPostIds(List<Long> postIds) {
            return List.of();
        }

        @Override
        public void softDeleteAttachment(long attachmentId) {
        }

        @Override
        public void softDeleteAttachmentsByPostIds(List<Long> postIds) {
        }
    }

    private static final class NoopBoardFileStorage implements BoardFileStorage {
        @Override
        public StoredFile store(BoardType boardType, String originalFileName, String contentType, long fileSize, java.io.InputStream inputStream) {
            return new StoredFile("notice/2026/07/test.pdf", contentType, fileSize);
        }

        @Override
        public Optional<java.io.InputStream> open(BoardType boardType, String storedFileName) {
            return Optional.empty();
        }

        @Override
        public void deletePhysical(BoardType boardType, String storedFileName) {
        }
    }

    private static final class NoopUserRepository implements UserRepository {
        @Override
        public long save(com.shindong.smartmanager.application.user.UserCommand command, String passwordHash, String actorUserId) {
            return 0L;
        }

        @Override
        public void update(long id, com.shindong.smartmanager.application.user.UserUpdateCommand command, String passwordHashOrNull, String actorUserId) {
        }

        @Override
        public void softDelete(long id, String actorUserId) {
        }

        @Override
        public void replaceRoles(long userId, List<Long> roleIds) {
        }

        @Override
        public List<com.shindong.smartmanager.application.user.UserView> findAllActive(String query) {
            return List.of();
        }

        @Override
        public Optional<com.shindong.smartmanager.application.user.UserView> findActiveById(long id) {
            return Optional.empty();
        }

        @Override
        public boolean existsActiveByLoginId(String loginId, Long excludeId) {
            return false;
        }

        @Override
        public Optional<String> findActiveLoginId(long id) {
            return Optional.empty();
        }
    }
}
