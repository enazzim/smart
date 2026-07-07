package com.shindong.smartmanager.infrastructure.persistence.user;

import com.shindong.smartmanager.application.auth.AuthUserRepository;
import java.time.Instant;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaAuthUserRepository implements AuthUserRepository {

    private final SpringDataUserRepository userRepository;
    private final SpringDataAuthRepository authRepository;
    private final SpringDataRoleRepository roleRepository;
    private final SpringDataUserRoleRepository userRoleRepository;

    public JpaAuthUserRepository(
            SpringDataUserRepository userRepository,
            SpringDataAuthRepository authRepository,
            SpringDataRoleRepository roleRepository,
            SpringDataUserRoleRepository userRoleRepository
    ) {
        this.userRepository = userRepository;
        this.authRepository = authRepository;
        this.roleRepository = roleRepository;
        this.userRoleRepository = userRoleRepository;
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<AuthUserRecord> findActiveByLoginId(String loginId) {
        return userRepository.findByLoginIdAndRecordingState(loginId, 1)
                .map(this::toRecord);
    }

    @Override
    @Transactional(readOnly = true)
    public boolean existsActiveAdmin() {
        return userRepository.findByLoginIdAndRecordingState("admin", 1).isPresent();
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<AuthUserRecord> findActiveById(long id) {
        return userRepository.findByIdAndRecordingState(id, 1)
                .map(this::toRecord);
    }

    @Override
    @Transactional
    public void updatePassword(long userId, String passwordHash, String actorUserId) {
        UserJpaEntity entity = userRepository.findByIdAndRecordingState(userId, 1)
                .orElseThrow(() -> new IllegalArgumentException("사용자를 찾을 수 없습니다: " + userId));
        Instant now = Instant.now();
        entity.setPasswordHash(passwordHash);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        userRepository.save(entity);
    }

    @Override
    @Transactional
    public long createAdmin(String loginId, String passwordHash, String name) {
        Instant now = Instant.now();
        UserJpaEntity entity = new UserJpaEntity();
        entity.setLoginId(loginId);
        entity.setPasswordHash(passwordHash);
        entity.setName(name);
        entity.setRecordingState(1);
        entity.setCreatedBy("seed");
        entity.setCreatedById("seed");
        entity.setCreatedAt(now);
        entity.setUpdatedBy("seed");
        entity.setUpdatedById("seed");
        entity.setUpdatedAt(now);
        long userId = userRepository.save(entity).getId();

        roleRepository.findByRoleCodeAndRecordingState("SYSTEM_ADMIN", 1)
                .ifPresent(role -> userRoleRepository.save(new UserRoleJpaEntity(userId, role.getId())));
        return userId;
    }

    private AuthUserRecord toRecord(UserJpaEntity entity) {
        long userId = entity.getId();
        return new AuthUserRecord(
                userId,
                entity.getLoginId(),
                entity.getPasswordHash(),
                entity.getName(),
                authRepository.findRoleCodesByUserId(userId),
                authRepository.findPermissionCodesByUserId(userId)
        );
    }
}
