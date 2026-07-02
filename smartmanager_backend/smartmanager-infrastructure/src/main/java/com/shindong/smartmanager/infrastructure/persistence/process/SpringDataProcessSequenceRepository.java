package com.shindong.smartmanager.infrastructure.persistence.process;

import com.shindong.smartmanager.domain.process.ProcessVariant;
import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataProcessSequenceRepository extends JpaRepository<ProcessSequenceJpaEntity, Long> {

    Optional<ProcessSequenceJpaEntity> findByIdAndRecordingState(Long id, int recordingState);

    List<ProcessSequenceJpaEntity> findByItemIdAndVariantAndRecordingStateOrderByProcessSequenceNumAsc(
            Long itemId,
            ProcessVariant variant,
            int recordingState
    );

    List<ProcessSequenceJpaEntity> findByVariantAndRecordingStateOrderByItemIdAscProcessSequenceNumAsc(
            ProcessVariant variant,
            int recordingState
    );

    boolean existsByItemIdAndPublicCodeIdAndProcessSequenceNumAndRecordingState(
            Long itemId,
            Long publicCodeId,
            short processSequenceNum,
            int recordingState
    );

    @Query("""
            SELECT COUNT(p) > 0 FROM ProcessSequenceJpaEntity p
            WHERE p.itemId = :itemId
              AND p.publicCodeId = :publicCodeId
              AND p.processSequenceNum = :processSequenceNum
              AND p.recordingState = 1
              AND (:excludeId IS NULL OR p.id <> :excludeId)
            """)
    boolean existsActiveDuplicate(
            @Param("itemId") Long itemId,
            @Param("publicCodeId") Long publicCodeId,
            @Param("processSequenceNum") short processSequenceNum,
            @Param("excludeId") Long excludeId
    );
}
