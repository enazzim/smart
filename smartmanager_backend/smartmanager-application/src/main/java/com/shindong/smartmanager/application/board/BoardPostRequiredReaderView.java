package com.shindong.smartmanager.application.board;

import java.time.Instant;

public record BoardPostRequiredReaderView(
        long userId,
        String loginId,
        String name,
        Instant readAt,
        boolean read
) {
}
