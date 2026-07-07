package com.shindong.smartmanager.application.board;

import java.time.Instant;

public record BoardAttachmentView(
        long id,
        long postId,
        String originalFileName,
        String contentType,
        long fileSize,
        Instant createdAt
) {
}
