package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.menu.GroupMenuCatalog;
import com.shindong.smartmanager.application.menu.GroupMenuService;
import com.shindong.smartmanager.application.menu.GroupMenuService.GroupMenuAssignment;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class GroupMenuApplicationService {

    private final GroupMenuService groupMenuService;

    public GroupMenuApplicationService(GroupMenuService groupMenuService) {
        this.groupMenuService = groupMenuService;
    }

    @Transactional(readOnly = true)
    public List<GroupMenuAssignment> list() {
        return groupMenuService.list();
    }

    public List<GroupMenuCatalog.MenuItem> catalog() {
        return GroupMenuCatalog.MENUS;
    }

    @Transactional
    public void replace(long workDiaryGroupId, List<String> menuIds) {
        groupMenuService.replace(workDiaryGroupId, menuIds);
    }
}
