package com.shindong.smartmanager.infrastructure.persistence.user;

import com.shindong.smartmanager.application.user.UserLookup;
import org.springframework.stereotype.Repository;

@Repository
public class JpaUserLookup implements UserLookup {

    private final SpringDataUserRepository userRepository;

    public JpaUserLookup(SpringDataUserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @Override
    public boolean existsActive(long userId) {
        return userRepository.findByIdAndRecordingState(userId, 1).isPresent();
    }

    @Override
    public String findActiveName(long userId) {
        return userRepository.findByIdAndRecordingState(userId, 1)
                .map(UserJpaEntity::getName)
                .orElse(null);
    }
}
