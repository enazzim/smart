package com.shindong.smartmanager.infrastructure.persistence.user;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataUserRoleRepository extends JpaRepository<UserRoleJpaEntity, UserRoleJpaEntity.Pk> {

    void deleteByUserId(Long userId);

    List<UserRoleJpaEntity> findByUserId(Long userId);
}
