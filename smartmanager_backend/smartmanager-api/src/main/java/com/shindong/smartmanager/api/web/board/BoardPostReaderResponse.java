package com.shindong.smartmanager.api.web.board;

import com.shindong.smartmanager.application.board.BoardPostReaderView;
import java.time.Instant;

public record BoardPostReaderResponse(
        long userId,
        String loginId,
        String name,
        Instant readAt
) {
    public static BoardPostReaderResponse from(BoardPostReaderView view) {
        return new BoardPostReaderResponse(
                view.userId(),
                view.loginId(),
                view.name(),
                view.readAt()
        );
    }
}
