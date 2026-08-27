package com.shindong.smartmanager.infrastructure.persistence.board;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataBoardPostReadRepository extends JpaRepository<BoardPostReadJpaEntity, Long> {

    Optional<BoardPostReadJpaEntity> findByPostIdAndReaderUserId(long postId, long readerUserId);

    List<BoardPostReadJpaEntity> findByPostId(long postId);

    @Query("""
            SELECT r.readerUserId, u.loginId, u.name, r.readAt
            FROM BoardPostReadJpaEntity r, UserJpaEntity u
            WHERE r.postId = :postId
              AND u.id = r.readerUserId
              AND r.readerUserId <> :authorUserId
            ORDER BY r.readAt ASC
            """)
    List<Object[]> findReadersExcludingAuthor(
            @Param("postId") long postId,
            @Param("authorUserId") long authorUserId
    );
}
