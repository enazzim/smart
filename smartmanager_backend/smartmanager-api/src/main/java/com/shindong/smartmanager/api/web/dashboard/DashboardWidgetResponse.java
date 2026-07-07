package com.shindong.smartmanager.api.web.dashboard;

import com.shindong.smartmanager.api.web.board.BoardPostSummaryResponse;
import java.util.List;

public record DashboardWidgetResponse(
        String boardType,
        String title,
        List<BoardPostSummaryResponse> items
) {
}
