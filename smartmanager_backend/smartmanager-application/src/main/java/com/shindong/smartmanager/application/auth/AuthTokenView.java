package com.shindong.smartmanager.application.auth;

public record AuthTokenView(
        String accessToken,
        String tokenType,
        long expiresInSeconds,
        AuthenticatedUserView user
) {
}
