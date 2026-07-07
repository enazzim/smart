package com.shindong.smartmanager.infrastructure.persistence.board;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataBoardAttachmentRepository extends JpaRepository<BoardAttachmentJpaEntity, Long> {

    @Query("""
            SELECT a FROM BoardAttachmentJpaEntity a
            WHERE a.id = :id AND a.recordingState = 1
            """)
    Optional<BoardAttachmentJpaEntity> findActiveById(@Param("id") long id);

    @Query("""
            SELECT a FROM BoardAttachmentJpaEntity a
            WHERE a.postId = :postId AND a.recordingState = 1
            ORDER BY a.id ASC
            """)
    List<BoardAttachmentJpaEntity> findActiveByPostId(@Param("postId") long postId);

    @Query("""
            SELECT a FROM BoardAttachmentJpaEntity a
            WHERE a.postId IN :postIds AND a.recordingState = 1
            """)
    List<BoardAttachmentJpaEntity> findActiveByPostIds(@Param("postIds") List<Long> postIds);

    @Modifying
    @Query("""
            UPDATE BoardAttachmentJpaEntity a
            SET a.recordingState = 0
            WHERE a.postId IN :postIds AND a.recordingState = 1
            """)
    void softDeleteByPostIds(@Param("postIds") List<Long> postIds);
}
