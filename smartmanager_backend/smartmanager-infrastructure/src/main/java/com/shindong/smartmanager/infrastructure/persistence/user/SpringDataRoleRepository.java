package com.shindong.smartmanager.infrastructure.persistence.user;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataRoleRepository extends JpaRepository<RoleJpaEntity, Long> {

    List<RoleJpaEntity> findByRecordingStateOrderByRoleCodeAsc(int recordingState);

    Optional<RoleJpaEntity> findByRoleCodeAndRecordingState(String roleCode, int recordingState);

    @Query("""
            SELECT COUNT(r) FROM RoleJpaEntity r
            WHERE r.recordingState = 1 AND r.id IN :roleIds
            """)
    long countActiveByIdIn(@Param("roleIds") List<Long> roleIds);
}
