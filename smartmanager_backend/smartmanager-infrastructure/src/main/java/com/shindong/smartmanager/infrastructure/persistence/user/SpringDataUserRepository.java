package com.shindong.smartmanager.infrastructure.persistence.user;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataUserRepository extends JpaRepository<UserJpaEntity, Long> {

    Optional<UserJpaEntity> findByIdAndRecordingState(Long id, int recordingState);

    @Query("""
            SELECT u FROM UserJpaEntity u
            WHERE u.recordingState = 1
              AND (:query IS NULL OR :query = ''
                   OR LOWER(u.loginId) LIKE LOWER(CONCAT('%', :query, '%'))
                   OR LOWER(u.name) LIKE LOWER(CONCAT('%', :query, '%')))
            ORDER BY u.loginId ASC
            """)
    List<UserJpaEntity> searchActive(@Param("query") String query);

    @Query("""
            SELECT CASE WHEN COUNT(u) > 0 THEN true ELSE false END
            FROM UserJpaEntity u
            WHERE u.loginId = :loginId
              AND u.recordingState = 1
              AND (:excludeId IS NULL OR u.id <> :excludeId)
            """)
    boolean existsActiveByLoginId(@Param("loginId") String loginId, @Param("excludeId") Long excludeId);

    boolean existsByWorkDiaryGroupIdAndRecordingState(Long workDiaryGroupId, int recordingState);

    Optional<UserJpaEntity> findByLoginIdAndRecordingState(String loginId, int recordingState);
}
