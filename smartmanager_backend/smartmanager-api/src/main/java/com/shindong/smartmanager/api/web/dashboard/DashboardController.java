package com.shindong.smartmanager.api.web.dashboard;

import com.shindong.smartmanager.api.security.CommunityAuthorize;
import com.shindong.smartmanager.api.web.board.BoardPostSummaryResponse;
import com.shindong.smartmanager.application.board.DashboardWidgetView;
import com.shindong.smartmanager.infrastructure.application.BoardPostApplicationService;
import java.util.List;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/dashboard")
@CommunityAuthorize.Read
public class DashboardController {

    private final BoardPostApplicationService boardPostApplicationService;

    public DashboardController(BoardPostApplicationService boardPostApplicationService) {
        this.boardPostApplicationService = boardPostApplicationService;
    }

    @GetMapping("/widgets")
    public DashboardWidgetsResponse widgets(@RequestParam(defaultValue = "5") int limit) {
        List<DashboardWidgetView> widgets = boardPostApplicationService.dashboardWidgets(limit);
        return new DashboardWidgetsResponse(
                widgets.stream()
                        .map(widget -> new DashboardWidgetResponse(
                                widget.boardType().name(),
                                widget.title(),
                                widget.items().stream().map(BoardPostSummaryResponse::from).toList()
                        ))
                        .toList()
        );
    }
}
