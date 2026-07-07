package com.shindong.smartmanager.application.board;

import java.util.List;

public record BoardPostPageView(
        List<BoardPostSummaryView> items,
        long totalElements,
        int page,
        int size
) {
}
