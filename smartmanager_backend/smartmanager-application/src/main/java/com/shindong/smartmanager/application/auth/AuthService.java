package com.shindong.smartmanager.application.auth;

import com.shindong.smartmanager.application.security.PasswordHasher;

public class AuthService {

    private final AuthUserRepository authUserRepository;
    private final PasswordHasher passwordHasher;
    private final JwtTokenPort jwtTokenPort;

    public AuthService(
            AuthUserRepository authUserRepository,
            PasswordHasher passwordHasher,
            JwtTokenPort jwtTokenPort
    ) {
        this.authUserRepository = authUserRepository;
        this.passwordHasher = passwordHasher;
        this.jwtTokenPort = jwtTokenPort;
    }

    public AuthTokenView login(LoginCommand command) {
        if (command.loginId() == null || command.loginId().isBlank()) {
            throw new IllegalArgumentException("아이디는 필수입니다.");
        }
        if (command.password() == null || command.password().isBlank()) {
            throw new IllegalArgumentException("비밀번호는 필수입니다.");
        }

        AuthUserRepository.AuthUserRecord user = authUserRepository.findActiveByLoginId(command.loginId().trim())
                .orElseThrow(() -> new IllegalArgumentException("아이디 또는 비밀번호가 올바르지 않습니다."));

        if (!passwordHasher.matches(command.password(), user.passwordHash())) {
            throw new IllegalArgumentException("아이디 또는 비밀번호가 올바르지 않습니다.");
        }

        return issueToken(user);
    }

    public AuthenticatedUserView me(long userId) {
        return authUserRepository.findActiveById(userId)
                .map(this::toView)
                .orElseThrow(() -> new IllegalArgumentException("사용자를 찾을 수 없습니다: " + userId));
    }

    public void changePassword(long userId, ChangePasswordCommand command) {
        if (command.currentPassword() == null || command.currentPassword().isBlank()) {
            throw new IllegalArgumentException("현재 비밀번호는 필수입니다.");
        }
        validateNewPassword(command.newPassword());

        AuthUserRepository.AuthUserRecord user = authUserRepository.findActiveById(userId)
                .orElseThrow(() -> new IllegalArgumentException("사용자를 찾을 수 없습니다: " + userId));

        if (!passwordHasher.matches(command.currentPassword(), user.passwordHash())) {
            throw new IllegalArgumentException("현재 비밀번호가 올바르지 않습니다.");
        }

        String passwordHash = passwordHasher.hash(command.newPassword());
        authUserRepository.updatePassword(userId, passwordHash, String.valueOf(userId));
    }

    private void validateNewPassword(String password) {
        if (password == null || password.isBlank()) {
            throw new IllegalArgumentException("새 비밀번호는 필수입니다.");
        }
        if (password.length() < 8) {
            throw new IllegalArgumentException("비밀번호는 8자 이상이어야 합니다.");
        }
    }

    public AuthenticatedUserView authenticateToken(String token) {
        JwtTokenPort.JwtClaims claims = jwtTokenPort.parseToken(token);
        return authUserRepository.findActiveById(claims.userId())
                .map(this::toView)
                .orElseThrow(() -> new IllegalArgumentException("사용자를 찾을 수 없습니다: " + claims.userId()));
    }

    private AuthTokenView issueToken(AuthUserRepository.AuthUserRecord user) {
        String token = jwtTokenPort.createToken(user.id(), user.loginId(), user.authorities());
        return new AuthTokenView(
                token,
                "Bearer",
                jwtTokenPort.expirationSeconds(),
                toView(user)
        );
    }

    private AuthenticatedUserView toView(AuthUserRepository.AuthUserRecord user) {
        return new AuthenticatedUserView(
                user.id(),
                user.loginId(),
                user.name(),
                user.roleCodes(),
                user.authorities()
        );
    }
}
