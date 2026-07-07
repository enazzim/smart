package com.shindong.smartmanager.infrastructure.persistence.board;

import com.shindong.smartmanager.domain.board.BoardType;
import com.shindong.smartmanager.domain.board.PostKind;
import java.util.List;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataBoardPostRepository extends JpaRepository<BoardPostJpaEntity, Long> {

    @Query("""
            SELECT p FROM BoardPostJpaEntity p
            WHERE p.id = :id AND p.recordingState = 1
            """)
    java.util.Optional<BoardPostJpaEntity> findActiveById(@Param("id") long id);

    @Query("""
            SELECT p FROM BoardPostJpaEntity p
            WHERE p.boardType = :boardType
              AND p.postKind = com.shindong.smartmanager.domain.board.PostKind.TOP
              AND p.recordingState = 1
              AND (:keyword IS NULL OR :keyword = '' OR LOWER(p.title) LIKE LOWER(CONCAT('%', :keyword, '%')))
            ORDER BY p.pinned DESC, p.createdAt DESC
            """)
    List<BoardPostJpaEntity> findActiveTopPosts(
            @Param("boardType") BoardType boardType,
            @Param("keyword") String keyword,
            Pageable pageable
    );

    @Query("""
            SELECT COUNT(p) FROM BoardPostJpaEntity p
            WHERE p.boardType = :boardType
              AND p.postKind = com.shindong.smartmanager.domain.board.PostKind.TOP
              AND p.recordingState = 1
              AND (:keyword IS NULL OR :keyword = '' OR LOWER(p.title) LIKE LOWER(CONCAT('%', :keyword, '%')))
            """)
    long countActiveTopPosts(@Param("boardType") BoardType boardType, @Param("keyword") String keyword);

    @Query("""
            SELECT p FROM BoardPostJpaEntity p
            WHERE p.threadRootId = :threadRootId
              AND p.postKind = com.shindong.smartmanager.domain.board.PostKind.REPLY
              AND p.recordingState = 1
            ORDER BY p.createdAt ASC
            """)
    List<BoardPostJpaEntity> findActiveReplies(@Param("threadRootId") long threadRootId);

    @Query("""
            SELECT p.id FROM BoardPostJpaEntity p
            WHERE p.threadRootId = :threadRootId
              AND p.postKind = com.shindong.smartmanager.domain.board.PostKind.REPLY
              AND p.recordingState = 1
            """)
    List<Long> findActiveReplyIdsByThreadRoot(@Param("threadRootId") long threadRootId);

    @Query("""
            SELECT p FROM BoardPostJpaEntity p
            WHERE p.boardType = :boardType
              AND p.postKind = com.shindong.smartmanager.domain.board.PostKind.TOP
              AND p.recordingState = 1
            ORDER BY p.createdAt DESC
            """)
    List<BoardPostJpaEntity> findRecentTopPosts(@Param("boardType") BoardType boardType, Pageable pageable);

    @Modifying
    @Query("""
            UPDATE BoardPostJpaEntity p
            SET p.viewCount = p.viewCount + 1
            WHERE p.id = :id AND p.recordingState = 1
            """)
    void incrementViewCount(@Param("id") long id);
}
