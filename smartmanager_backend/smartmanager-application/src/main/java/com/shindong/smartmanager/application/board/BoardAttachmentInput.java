package com.shindong.smartmanager.application.board;

public record BoardAttachmentInput(
        String originalFileName,
        String storedFileName,
        String contentType,
        long fileSize
) {
}
