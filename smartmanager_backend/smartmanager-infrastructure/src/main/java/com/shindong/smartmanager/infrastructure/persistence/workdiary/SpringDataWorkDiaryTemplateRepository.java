package com.shindong.smartmanager.infrastructure.persistence.workdiary;

import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataWorkDiaryTemplateRepository extends JpaRepository<WorkDiaryTemplateJpaEntity, Long> {

    @Query("""
            SELECT t FROM WorkDiaryTemplateJpaEntity t
            WHERE t.workDiaryGroupId = :groupId
              AND t.recordingState = 1
            ORDER BY t.id DESC
            """)
    Optional<WorkDiaryTemplateJpaEntity> findActiveByGroupId(@Param("groupId") long groupId);
}

