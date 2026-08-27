package com.shindong.smartmanager.api.web.board;

import com.shindong.smartmanager.application.board.BoardPostSummaryView;
import java.time.Instant;

public record BoardPostSummaryResponse(
        long id,
        String boardType,
        String postKind,
        String title,
        String authorName,
        int viewCount,
        boolean pinned,
        boolean hasAttachment,
        boolean myRequiredUnread,
        Instant createdAt
) {
    public static BoardPostSummaryResponse from(BoardPostSummaryView view) {
        return new BoardPostSummaryResponse(
                view.id(),
                view.boardType().name(),
                view.postKind().name(),
                view.title(),
                view.authorName(),
                view.viewCount(),
                view.pinned(),
                view.hasAttachment(),
                view.myRequiredUnread(),
                view.createdAt()
        );
    }
}
