package com.shindong.smartmanager.infrastructure.persistence.board;

import java.util.Collection;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataBoardPostRequiredReaderRepository
        extends JpaRepository<BoardPostRequiredReaderJpaEntity, Long> {

    @Modifying(clearAutomatically = true, flushAutomatically = true)
    @Query("DELETE FROM BoardPostRequiredReaderJpaEntity r WHERE r.postId = :postId")
    void deleteByPostId(@Param("postId") long postId);

    @Query("""
            SELECT u.id, u.loginId, u.name
            FROM BoardPostRequiredReaderJpaEntity rr, UserJpaEntity u
            WHERE rr.postId = :postId
              AND u.id = rr.userId
              AND u.recordingState = 1
            ORDER BY u.name ASC
            """)
    List<Object[]> findRequiredActiveUsers(@Param("postId") long postId);

    @Query("""
            SELECT rr.postId
            FROM BoardPostRequiredReaderJpaEntity rr, BoardPostJpaEntity p
            WHERE rr.userId = :userId
              AND p.id = rr.postId
              AND p.recordingState = 1
              AND p.boardType = com.shindong.smartmanager.domain.board.BoardType.NOTICE
              AND p.postKind = com.shindong.smartmanager.domain.board.PostKind.TOP
              AND rr.postId IN :postIds
              AND NOT EXISTS (
                SELECT 1 FROM BoardPostReadJpaEntity r
                WHERE r.postId = rr.postId AND r.readerUserId = :userId
              )
            """)
    List<Long> findUnreadRequiredPostIds(
            @Param("userId") long userId,
            @Param("postIds") Collection<Long> postIds
    );
}
