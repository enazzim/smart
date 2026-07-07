package com.shindong.smartmanager.api.web.board;

import com.shindong.smartmanager.application.board.BoardAttachmentView;
import java.time.Instant;

public record BoardAttachmentResponse(
        long id,
        long postId,
        String originalFileName,
        String contentType,
        long fileSize,
        Instant createdAt
) {
    public static BoardAttachmentResponse from(BoardAttachmentView view) {
        return new BoardAttachmentResponse(
                view.id(),
                view.postId(),
                view.originalFileName(),
                view.contentType(),
                view.fileSize(),
                view.createdAt()
        );
    }
}
