package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.user.UserCommand;
import com.shindong.smartmanager.application.user.UserService;
import com.shindong.smartmanager.application.user.UserUpdateCommand;
import com.shindong.smartmanager.application.user.UserView;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class UserApplicationService {

    private final UserService userService;

    public UserApplicationService(UserService userService) {
        this.userService = userService;
    }

    @Transactional
    public UserView register(UserCommand command, String actorUserId) {
        return userService.register(command, actorUserId);
    }

    @Transactional
    public UserView update(long id, UserUpdateCommand command, String actorUserId) {
        return userService.update(id, command, actorUserId);
    }

    @Transactional
    public void delete(long id, String actorUserId) {
        userService.delete(id, actorUserId);
    }

    @Transactional(readOnly = true)
    public List<UserView> listActive(String query) {
        return userService.listActive(query);
    }

    @Transactional(readOnly = true)
    public UserView getActive(long id) {
        return userService.getActive(id);
    }

    @Transactional(readOnly = true)
    public boolean isLoginIdAvailable(String loginId) {
        return userService.isLoginIdAvailable(loginId);
    }
}
