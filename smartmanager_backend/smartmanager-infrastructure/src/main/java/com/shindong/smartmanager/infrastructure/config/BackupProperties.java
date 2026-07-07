package com.shindong.smartmanager.infrastructure.config;

import org.springframework.boot.context.properties.ConfigurationProperties;

@ConfigurationProperties(prefix = "smartmanager.backup")
public record BackupProperties(
        String dir,
        String mariadbBinDir
) {
    public BackupProperties {
        if (dir == null || dir.isBlank()) {
            dir = "../../backup";
        }
    }
}
