package com.shindong.smartmanager.infrastructure.config;

import org.springframework.boot.context.properties.ConfigurationProperties;

@ConfigurationProperties(prefix = "smartmanager.security")
public record SmartManagerSecurityProperties(
        Jwt jwt,
        AdminSeed admin
) {
    public record Jwt(
            String secret,
            long expirationMinutes
    ) {
    }

    public record AdminSeed(
            String loginId,
            String initialPassword,
            boolean enabled
    ) {
    }
}
