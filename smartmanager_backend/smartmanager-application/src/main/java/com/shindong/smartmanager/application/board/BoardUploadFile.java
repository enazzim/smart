package com.shindong.smartmanager.application.board;

import java.io.InputStream;

public record BoardUploadFile(
        String originalFileName,
        String contentType,
        long fileSize,
        InputStream inputStream
) {
}
