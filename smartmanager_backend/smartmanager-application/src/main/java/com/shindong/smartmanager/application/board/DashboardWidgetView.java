package com.shindong.smartmanager.application.board;

import com.shindong.smartmanager.domain.board.BoardType;
import java.util.List;

public record DashboardWidgetView(
        BoardType boardType,
        String title,
        List<BoardPostSummaryView> items
) {
}
