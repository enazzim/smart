package com.shindong.smartmanager.infrastructure.persistence.user;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataAuthRepository extends JpaRepository<UserJpaEntity, Long> {

    @Query("""
            SELECT DISTINCT p.permissionCode
            FROM UserJpaEntity u
            JOIN UserRoleJpaEntity ur ON ur.userId = u.id
            JOIN RolePermissionJpaEntity rp ON rp.roleId = ur.roleId
            JOIN PermissionJpaEntity p ON p.id = rp.permissionId
            WHERE u.id = :userId
              AND u.recordingState = 1
              AND p.recordingState = 1
            ORDER BY p.permissionCode
            """)
    List<String> findPermissionCodesByUserId(@Param("userId") long userId);

    @Query("""
            SELECT r.roleCode
            FROM UserRoleJpaEntity ur
            JOIN RoleJpaEntity r ON r.id = ur.roleId
            WHERE ur.userId = :userId
              AND r.recordingState = 1
            ORDER BY r.roleCode
            """)
    List<String> findRoleCodesByUserId(@Param("userId") long userId);
}
