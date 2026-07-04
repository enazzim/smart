package com.shindong.smartmanager.infrastructure.persistence.code;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataPublicCodeRepository extends JpaRepository<PublicCodeJpaEntity, Long> {

    Optional<PublicCodeJpaEntity> findByIdAndUsageTypeAndRecordingState(Long id, String usageType, int recordingState);

    Optional<PublicCodeJpaEntity> findByIdAndRecordingState(Long id, int recordingState);

    Optional<PublicCodeJpaEntity> findByLargeCodeAndSmallCodeIsNullAndRecordingState(String largeCode, int recordingState);

    boolean existsByLargeCodeAndSmallCodeIsNullAndRecordingState(String largeCode, int recordingState);

    boolean existsByLargeCodeAndSmallCodeAndRecordingState(String largeCode, String smallCode, int recordingState);

    List<PublicCodeJpaEntity> findBySmallCodeIsNullAndRecordingStateOrderByLargeCodeAsc(int recordingState);

    List<PublicCodeJpaEntity> findByLargeCodeAndSmallCodeIsNotNullAndRecordingStateOrderBySmallCodeAsc(
            String largeCode,
            int recordingState
    );

    @Query("""
            SELECT p FROM PublicCodeJpaEntity p
            WHERE p.smallCode IS NOT NULL
              AND p.recordingState = 1
              AND (:largeCode IS NULL OR p.largeCode = :largeCode)
              AND (:usageType IS NULL OR p.usageType = :usageType)
            ORDER BY p.largeCode ASC, p.smallCode ASC
            """)
    List<PublicCodeJpaEntity> searchActiveSmallCodes(
            @Param("largeCode") String largeCode,
            @Param("usageType") String usageType
    );

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

    Optional<PublicCodeJpaEntity> findBySmallCodeAndUsageTypeAndRecordingState(
            String smallCode,
            String usageType,
            int recordingState
    );

    List<PublicCodeJpaEntity> findByLargeCodeAndRecordingState(String largeCode, int recordingState);
}
