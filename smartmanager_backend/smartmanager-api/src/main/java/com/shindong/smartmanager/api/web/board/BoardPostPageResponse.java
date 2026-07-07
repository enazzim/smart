package com.shindong.smartmanager.api.web.board;

import com.shindong.smartmanager.application.board.BoardPostPageView;
import java.util.List;

public record BoardPostPageResponse(
        List<BoardPostSummaryResponse> items,
        long totalElements,
        int page,
        int size
) {
    public static BoardPostPageResponse from(BoardPostPageView view) {
        return new BoardPostPageResponse(
                view.items().stream().map(BoardPostSummaryResponse::from).toList(),
                view.totalElements(),
                view.page(),
                view.size()
        );
    }
}
