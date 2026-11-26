package com.shindong.smartmanager.api.web.system.menu;

import com.shindong.smartmanager.application.menu.GroupMenuCatalog;
import com.shindong.smartmanager.application.menu.GroupMenuService.GroupMenuAssignment;
import com.shindong.smartmanager.infrastructure.application.GroupMenuApplicationService;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import java.util.List;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/system/group-menus")
public class GroupMenuController {

    private final GroupMenuApplicationService groupMenuApplicationService;

    public GroupMenuController(GroupMenuApplicationService groupMenuApplicationService) {
        this.groupMenuApplicationService = groupMenuApplicationService;
    }

    @GetMapping
    @PreAuthorize("hasAuthority('system:menu-grant:write')")
    public GroupMenuSettingsResponse list() {
        List<GroupResponse> groups = groupMenuApplicationService.list().stream()
                .map(GroupResponse::from)
                .toList();
        List<MenuResponse> menus = groupMenuApplicationService.catalog().stream()
                .map(MenuResponse::from)
                .toList();
        return new GroupMenuSettingsResponse(groups, menus);
    }

    @PutMapping("/{workDiaryGroupId}")
    @PreAuthorize("hasAuthority('system:menu-grant:write')")
    public void replace(
            @PathVariable long workDiaryGroupId,
            @Valid @RequestBody ReplaceGroupMenuRequest request
    ) {
        groupMenuApplicationService.replace(workDiaryGroupId, request.menuIds());
    }

    public record GroupMenuSettingsResponse(List<GroupResponse> groups, List<MenuResponse> menus) {
    }

    public record GroupResponse(long id, String name, List<String> menuIds) {
        static GroupResponse from(GroupMenuAssignment assignment) {
            return new GroupResponse(assignment.workDiaryGroupId(), assignment.groupName(), assignment.menuIds());
        }
    }

    public record MenuResponse(String categoryLabel, String menuId, String menuLabel) {
        static MenuResponse from(GroupMenuCatalog.MenuItem item) {
            return new MenuResponse(item.categoryLabel(), item.menuId(), item.menuLabel());
        }
    }

    public record ReplaceGroupMenuRequest(@NotNull List<String> menuIds) {
    }
}
