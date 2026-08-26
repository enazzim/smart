package com.shindong.smartmanager.infrastructure.persistence.user;

import com.shindong.smartmanager.application.user.UserCommand;
import com.shindong.smartmanager.application.user.UserRepository;
import com.shindong.smartmanager.application.user.UserUpdateCommand;
import com.shindong.smartmanager.application.user.UserView;
import com.shindong.smartmanager.infrastructure.persistence.code.PublicCodeJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.code.SpringDataPublicCodeRepository;
import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;
import java.time.Instant;
import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaUserRepository implements UserRepository {

    private final SpringDataUserRepository userRepository;
    private final SpringDataUserRoleRepository userRoleRepository;
    private final SpringDataRoleRepository roleRepository;
    private final SpringDataPublicCodeRepository publicCodeRepository;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaUserRepository(
            SpringDataUserRepository userRepository,
            SpringDataUserRoleRepository userRoleRepository,
            SpringDataRoleRepository roleRepository,
            SpringDataPublicCodeRepository publicCodeRepository,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.userRepository = userRepository;
        this.userRoleRepository = userRoleRepository;
        this.roleRepository = roleRepository;
        this.publicCodeRepository = publicCodeRepository;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    @Transactional
    public long save(UserCommand command, String passwordHash, String actorUserId) {
        Instant now = Instant.now();
        UserJpaEntity entity = new UserJpaEntity();
        entity.setLoginId(command.loginId().trim());
        entity.setPasswordHash(passwordHash);
        entity.setName(command.name().trim());
        entity.setContact(normalizeOptional(command.contact()));
        entity.setEmail(normalizeOptional(command.email()));
        entity.setWorkDiaryGroupId(command.workDiaryGroupId());
        entity.setRecordingState(1);
        entity.setCreatedBy(masterAuditActorLookup.nameOf(actorUserId));
        entity.setCreatedById(actorUserId);
        entity.setCreatedAt(now);
        entity.setUpdatedBy(masterAuditActorLookup.nameOf(actorUserId));
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        return userRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void update(long id, UserUpdateCommand command, String passwordHashOrNull, String actorUserId) {
        UserJpaEntity entity = userRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("사용자를 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setName(command.name().trim());
        entity.setContact(normalizeOptional(command.contact()));
        entity.setEmail(normalizeOptional(command.email()));
        entity.setWorkDiaryGroupId(command.workDiaryGroupId());
        if (passwordHashOrNull != null) {
            entity.setPasswordHash(passwordHashOrNull);
        }
        entity.setUpdatedBy(masterAuditActorLookup.nameOf(actorUserId));
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        userRepository.save(entity);
    }

    @Override
    @Transactional
    public void softDelete(long id, String actorUserId) {
        UserJpaEntity entity = userRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("사용자를 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setRecordingState(0);
        entity.setUpdatedBy(masterAuditActorLookup.nameOf(actorUserId));
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        userRepository.save(entity);
    }

    @Override
    @Transactional
    public void replaceRoles(long userId, List<Long> roleIds) {
        userRoleRepository.deleteByUserId(userId);
        userRoleRepository.flush();
        for (Long roleId : roleIds) {
            userRoleRepository.save(new UserRoleJpaEntity(userId, roleId));
        }
    }

    @Override
    public List<UserView> findAllActive(String query) {
        String normalized = query == null || query.isBlank() ? null : query.trim();
        Map<Long, RoleJpaEntity> roleMap = loadActiveRoleMap();
        return userRepository.searchActive(normalized).stream()
                .map(entity -> toView(entity, roleMap))
                .toList();
    }

    @Override
    public Optional<UserView> findActiveById(long id) {
        Map<Long, RoleJpaEntity> roleMap = loadActiveRoleMap();
        return userRepository.findByIdAndRecordingState(id, 1).map(entity -> toView(entity, roleMap));
    }

    @Override
    public boolean existsActiveByLoginId(String loginId, Long excludeId) {
        return userRepository.existsActiveByLoginId(loginId.trim(), excludeId);
    }

    @Override
    public Optional<String> findActiveLoginId(long id) {
        return userRepository.findByIdAndRecordingState(id, 1).map(UserJpaEntity::getLoginId);
    }

    private UserView toView(UserJpaEntity entity, Map<Long, RoleJpaEntity> roleMap) {
        List<Long> roleIds = new ArrayList<>();
        List<String> roleCodes = new ArrayList<>();
        for (UserRoleJpaEntity userRole : userRoleRepository.findByUserId(entity.getId())) {
            RoleJpaEntity role = roleMap.get(userRole.getRoleId());
            if (role != null) {
                roleIds.add(role.getId());
                roleCodes.add(role.getRoleCode());
            }
        }

        String workDiaryGroupName = null;
        if (entity.getWorkDiaryGroupId() != null) {
            workDiaryGroupName = publicCodeRepository.findById(entity.getWorkDiaryGroupId())
                    .filter(code -> code.getRecordingState() == 1)
                    .map(PublicCodeJpaEntity::getSmallName)
                    .orElse(null);
        }

        return new UserView(
                entity.getId(),
                entity.getLoginId(),
                entity.getName(),
                entity.getContact(),
                entity.getEmail(),
                roleIds,
                roleCodes,
                entity.getWorkDiaryGroupId(),
                workDiaryGroupName,
                entity.getCreatedAt()
        );
    }

    private Map<Long, RoleJpaEntity> loadActiveRoleMap() {
        Map<Long, RoleJpaEntity> roleMap = new LinkedHashMap<>();
        for (RoleJpaEntity role : roleRepository.findByRecordingStateOrderByRoleCodeAsc(1)) {
            roleMap.put(role.getId(), role);
        }
        return roleMap;
    }

    private static String normalizeOptional(String value) {
        if (value == null || value.isBlank()) {
            return null;
        }
        return value.trim();
    }
}
