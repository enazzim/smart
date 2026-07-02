package com.shindong.smartmanager.infrastructure.persistence.code;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataPublicCodeRepository extends JpaRepository<PublicCodeJpaEntity, Long> {

    Optional<PublicCodeJpaEntity> findByIdAndUsageTypeAndRecordingState(Long id, String usageType, int recordingState);

    @Query("""
            SELECT p FROM PublicCodeJpaEntity p
            WHERE p.largeCode = :largeCode
              AND p.smallCode IS NOT NULL
              AND p.recordingState = 1
              AND p.usageType = :usageType
            ORDER BY p.smallCode
            """)
    List<PublicCodeJpaEntity> findActiveSmallCodes(
            @Param("largeCode") String largeCode,
            @Param("usageType") String usageType
    );
}
