package com.shindong.smartmanager.infrastructure.config;

import org.springframework.boot.context.properties.ConfigurationProperties;

@ConfigurationProperties(prefix = "smartmanager.drawing")
public record DrawingProperties(
        String storageDir,
        long maxFileSizeBytes
) {
    private static final long DEFAULT_MAX_FILE_SIZE_BYTES = 104_857_600L;

    public DrawingProperties {
        if (storageDir == null || storageDir.isBlank()) {
            storageDir = "../../drawing-storage/pdf";
        }
        if (maxFileSizeBytes <= 0) {
            maxFileSizeBytes = DEFAULT_MAX_FILE_SIZE_BYTES;
        }
    }
}
