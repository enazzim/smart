package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.auth.AuthService;
import com.shindong.smartmanager.application.auth.AuthTokenView;
import com.shindong.smartmanager.application.auth.AuthenticatedUserView;
import com.shindong.smartmanager.application.auth.LoginCommand;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class AuthApplicationService {

    private final AuthService authService;

    public AuthApplicationService(AuthService authService) {
        this.authService = authService;
    }

    @Transactional(readOnly = true)
    public AuthTokenView login(LoginCommand command) {
        return authService.login(command);
    }

    @Transactional(readOnly = true)
    public AuthenticatedUserView me(long userId) {
        return authService.me(userId);
    }
}
