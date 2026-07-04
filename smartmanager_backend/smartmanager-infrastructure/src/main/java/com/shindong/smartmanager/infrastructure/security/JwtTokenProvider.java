package com.shindong.smartmanager.infrastructure.security;

import com.shindong.smartmanager.application.auth.JwtTokenPort;
import com.shindong.smartmanager.infrastructure.config.SmartManagerSecurityProperties;
import io.jsonwebtoken.Claims;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.security.Keys;
import java.nio.charset.StandardCharsets;
import java.time.Instant;
import java.util.Date;
import java.util.List;
import javax.crypto.SecretKey;
import org.springframework.stereotype.Component;

@Component
public class JwtTokenProvider implements JwtTokenPort {

    private final SecretKey secretKey;
    private final long expirationSeconds;

    public JwtTokenProvider(SmartManagerSecurityProperties properties) {
        String secret = properties.jwt().secret();
        if (secret == null || secret.length() < 32) {
            throw new IllegalStateException("smartmanager.security.jwt.secret must be at least 32 characters");
        }
        this.secretKey = Keys.hmacShaKeyFor(secret.getBytes(StandardCharsets.UTF_8));
        this.expirationSeconds = properties.jwt().expirationMinutes() * 60;
    }

    @Override
    public String createToken(long userId, String loginId, List<String> authorities) {
        Instant now = Instant.now();
        return Jwts.builder()
                .subject(loginId)
                .claim("uid", userId)
                .claim("authorities", authorities)
                .issuedAt(Date.from(now))
                .expiration(Date.from(now.plusSeconds(expirationSeconds)))
                .signWith(secretKey)
                .compact();
    }

    @Override
    public JwtClaims parseToken(String token) {
        Claims claims = Jwts.parser()
                .verifyWith(secretKey)
                .build()
                .parseSignedClaims(token)
                .getPayload();

        Number userId = claims.get("uid", Number.class);
        if (userId == null) {
            throw new IllegalArgumentException("유효하지 않은 토큰입니다.");
        }

        @SuppressWarnings("unchecked")
        List<String> authorities = claims.get("authorities", List.class);
        return new JwtClaims(
                userId.longValue(),
                claims.getSubject(),
                authorities == null ? List.of() : authorities
        );
    }

    @Override
    public long expirationSeconds() {
        return expirationSeconds;
    }
}
