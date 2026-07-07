package com.shindong.smartmanager.api.web.board;

import com.shindong.smartmanager.application.board.BoardUploadFile;
import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;
import org.springframework.web.multipart.MultipartFile;

final class BoardMultipartSupport {

    private BoardMultipartSupport() {
    }

    static List<BoardUploadFile> toUploadFiles(List<MultipartFile> files) {
        if (files == null || files.isEmpty()) {
            return List.of();
        }
        List<BoardUploadFile> uploadFiles = new ArrayList<>();
        for (MultipartFile file : files) {
            if (file == null || file.isEmpty()) {
                continue;
            }
            uploadFiles.add(toUploadFile(file));
        }
        return uploadFiles;
    }

    static BoardUploadFile toUploadFile(MultipartFile file) {
        try {
            InputStream inputStream = file.getInputStream();
            return new BoardUploadFile(
                    file.getOriginalFilename() != null ? file.getOriginalFilename() : file.getName(),
                    file.getContentType(),
                    file.getSize(),
                    inputStream
            );
        } catch (IOException ex) {
            throw new IllegalStateException("첨부파일을 읽을 수 없습니다.", ex);
        }
    }
}
