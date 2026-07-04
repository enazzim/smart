package com.shindong.smartmanager.application.auth;

import java.util.List;

public interface JwtTokenPort {

    String createToken(long userId, String loginId, List<String> authorities);

    JwtClaims parseToken(String token);

    long expirationSeconds();

    record JwtClaims(
            long userId,
            String loginId,
            List<String> authorities
    ) {
    }
}
