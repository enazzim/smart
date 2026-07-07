package com.shindong.smartmanager.application.board;

import com.shindong.smartmanager.domain.board.BoardType;
import java.io.InputStream;
import java.util.Optional;

public interface BoardFileStorage {

    record StoredFile(
            String storedFileName,
            String contentType,
            long fileSize
    ) {
    }

    StoredFile store(BoardType boardType, String originalFileName, String contentType, long fileSize, InputStream inputStream);

    Optional<InputStream> open(BoardType boardType, String storedFileName);

    void deletePhysical(BoardType boardType, String storedFileName);
}
