package com.shindong.smartmanager.application.board;

import java.time.Instant;

public record BoardPostReaderView(
        long userId,
        String loginId,
        String name,
        Instant readAt
) {
}
