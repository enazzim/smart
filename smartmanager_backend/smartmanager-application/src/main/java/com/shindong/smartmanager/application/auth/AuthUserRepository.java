package com.shindong.smartmanager.application.auth;

import java.util.List;
import java.util.Optional;

public interface AuthUserRepository {

    Optional<AuthUserRecord> findActiveByLoginId(String loginId);

    Optional<AuthUserRecord> findActiveById(long id);

    boolean existsActiveAdmin();

    long createAdmin(String loginId, String passwordHash, String name);

    record AuthUserRecord(
            long id,
            String loginId,
            String passwordHash,
            String name,
            List<String> roleCodes,
            List<String> authorities
    ) {
    }
}
