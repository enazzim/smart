package com.shindong.smartmanager.api.web.auth;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.application.auth.AuthTokenView;
import com.shindong.smartmanager.application.auth.AuthenticatedUserView;
import com.shindong.smartmanager.application.auth.ChangePasswordCommand;
import com.shindong.smartmanager.application.auth.LoginCommand;
import com.shindong.smartmanager.infrastructure.application.AuthApplicationService;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/auth")
public class AuthController {

    private final AuthApplicationService authApplicationService;

    public AuthController(AuthApplicationService authApplicationService) {
        this.authApplicationService = authApplicationService;
    }

    @PostMapping("/login")
    public AuthTokenResponse login(@Valid @RequestBody LoginRequest request) {
        AuthTokenView token = authApplicationService.login(
                new LoginCommand(request.loginId(), request.password())
        );
        return AuthTokenResponse.from(token);
    }

    @GetMapping("/me")
    public AuthenticatedUserResponse me() {
        return AuthenticatedUserResponse.from(
                authApplicationService.me(SecurityUtils.requireUserId())
        );
    }

    @PutMapping("/me/password")
    public void changePassword(@Valid @RequestBody ChangePasswordRequest request) {
        authApplicationService.changePassword(
                SecurityUtils.requireUserId(),
                new ChangePasswordCommand(request.currentPassword(), request.newPassword())
        );
    }

    public record LoginRequest(
            @NotBlank String loginId,
            @NotBlank String password
    ) {
    }

    public record ChangePasswordRequest(
            @NotBlank String currentPassword,
            @NotBlank String newPassword
    ) {
    }

    public record AuthTokenResponse(
            String accessToken,
            String tokenType,
            long expiresInSeconds,
            AuthenticatedUserResponse user
    ) {
        static AuthTokenResponse from(AuthTokenView view) {
            return new AuthTokenResponse(
                    view.accessToken(),
                    view.tokenType(),
                    view.expiresInSeconds(),
                    AuthenticatedUserResponse.from(view.user())
            );
        }
    }

    public record AuthenticatedUserResponse(
            long id,
            String loginId,
            String name,
            java.util.List<String> roleCodes,
            java.util.List<String> authorities
    ) {
        static AuthenticatedUserResponse from(AuthenticatedUserView view) {
            return new AuthenticatedUserResponse(
                    view.id(),
                    view.loginId(),
                    view.name(),
                    view.roleCodes(),
                    view.authorities()
            );
        }
    }
}
