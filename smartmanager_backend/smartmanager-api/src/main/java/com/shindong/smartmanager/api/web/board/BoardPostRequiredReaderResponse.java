package com.shindong.smartmanager.api.web.board;

import com.shindong.smartmanager.application.board.BoardPostRequiredReaderView;
import java.time.Instant;

public record BoardPostRequiredReaderResponse(
        long userId,
        String loginId,
        String name,
        Instant readAt,
        boolean read
) {
    public static BoardPostRequiredReaderResponse from(BoardPostRequiredReaderView view) {
        return new BoardPostRequiredReaderResponse(
                view.userId(),
                view.loginId(),
                view.name(),
                view.readAt(),
                view.read()
        );
    }
}
