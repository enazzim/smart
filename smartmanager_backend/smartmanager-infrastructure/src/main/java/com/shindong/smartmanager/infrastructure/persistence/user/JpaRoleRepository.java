package com.shindong.smartmanager.infrastructure.persistence.user;

import com.shindong.smartmanager.application.role.RoleRepository;
import com.shindong.smartmanager.application.role.RoleView;
import java.util.List;
import org.springframework.stereotype.Repository;

@Repository
public class JpaRoleRepository implements RoleRepository {

    private final SpringDataRoleRepository roleRepository;

    public JpaRoleRepository(SpringDataRoleRepository roleRepository) {
        this.roleRepository = roleRepository;
    }

    @Override
    public List<RoleView> findAllActive() {
        return roleRepository.findByRecordingStateOrderByRoleCodeAsc(1).stream()
                .map(role -> new RoleView(role.getId(), role.getRoleCode(), role.getRoleName()))
                .toList();
    }

    @Override
    public boolean allActiveIdsExist(List<Long> roleIds) {
        if (roleIds == null || roleIds.isEmpty()) {
            return false;
        }
        return roleRepository.countActiveByIdIn(roleIds) == roleIds.size();
    }
}
