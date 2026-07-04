package com.shindong.smartmanager.application.user;

import java.util.List;
import java.util.Optional;

public interface UserRepository {

    long save(UserCommand command, String passwordHash, String actorUserId);

    void update(long id, UserUpdateCommand command, String passwordHashOrNull, String actorUserId);

    void softDelete(long id, String actorUserId);

    void replaceRoles(long userId, List<Long> roleIds);

    List<UserView> findAllActive(String query);

    Optional<UserView> findActiveById(long id);

    boolean existsActiveByLoginId(String loginId, Long excludeId);

    Optional<String> findActiveLoginId(long id);
}
