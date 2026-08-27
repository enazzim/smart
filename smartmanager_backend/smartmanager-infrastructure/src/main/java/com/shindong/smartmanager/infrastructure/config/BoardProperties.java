package com.shindong.smartmanager.infrastructure.config;

import org.springframework.boot.context.properties.ConfigurationProperties;

@ConfigurationProperties(prefix = "smartmanager.board")
public record BoardProperties(
        String filesDir,
        long maxFileSizeBytes
) {
}
