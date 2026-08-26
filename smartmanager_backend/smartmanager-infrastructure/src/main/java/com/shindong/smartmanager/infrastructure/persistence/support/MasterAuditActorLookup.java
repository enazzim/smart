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

    public Long idOf(String actor) {
        if (actor == null || actor.isBlank()) {
            return null;
        }
        return userRepository.findByLoginIdAndRecordingState(actor, 1)
                .map(UserJpaEntity::getId)
                .orElseGet(() -> parseUserId(actor));
    }

    public String nameOf(Long userId) {
        if (userId == null) {
            return null;
        }
        return userRepository.findByIdAndRecordingState(userId, 1)
                .map(UserJpaEntity::getName)
                .orElse(null);
    }

    public String loginIdOf(Long userId) {
        if (userId == null) {
            return null;
        }
        return userRepository.findByIdAndRecordingState(userId, 1)
                .map(UserJpaEntity::getLoginId)
                .orElse(null);
    }

    private Long parseUserId(String actor) {
        for (int i = 0; i < actor.length(); i++) {
            if (!Character.isDigit(actor.charAt(i))) {
                return null;
            }
        }
        try {
            long id = Long.parseLong(actor);
            return userRepository.findByIdAndRecordingState(id, 1)
                    .map(UserJpaEntity::getId)
                    .orElse(null);
        } catch (NumberFormatException ex) {
            return null;
        }
    }
}
