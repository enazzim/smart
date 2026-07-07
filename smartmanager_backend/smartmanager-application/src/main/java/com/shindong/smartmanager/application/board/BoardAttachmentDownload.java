package com.shindong.smartmanager.application.board;

import java.io.InputStream;

public record BoardAttachmentDownload(
        String originalFileName,
        String contentType,
        long fileSize,
        InputStream inputStream
) {
}
