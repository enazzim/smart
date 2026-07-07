package com.shindong.smartmanager.api.web.dashboard;

import com.shindong.smartmanager.api.web.board.BoardPostSummaryResponse;
import java.util.List;

public record DashboardWidgetsResponse(
        List<DashboardWidgetResponse> widgets
) {
}
