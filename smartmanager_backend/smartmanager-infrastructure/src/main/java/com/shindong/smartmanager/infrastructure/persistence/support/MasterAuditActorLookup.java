package com.shindong.smartmanager.infrastructure.persistence.support;

import com.shindong.smartmanager.infrastructure.persistence.user.SpringDataUserRepository;
import com.shindong.smartmanager.infrastructure.persistence.user.UserJpaEntity;
import org.springframework.stereotype.Component;

@Component
public class MasterAuditActorLookup {

    private final SpringDataUserRepository userRepository;

    public MasterAuditActorLookup(SpringDataUserRepository userRepository) {
        this.userRepository = userRepository;
    }

    public String nameOf(String loginId) {
        if (loginId == null || loginId.isBlank()) {
            return loginId;
        }
        return userRepository.findByLoginIdAndRecordingState(loginId, 1)
                .map(UserJpaEntity::getName)
                .map(String::trim)
                .filter(name -> !name.isEmpty())
                .orElse(loginId);
    }
}
