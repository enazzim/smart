package com.shindong.smartmanager.application.menu;

import com.shindong.smartmanager.application.code.CodeGroupOptionsRepository;
import com.shindong.smartmanager.application.code.CodeOptionView;
import java.util.ArrayList;
import java.util.LinkedHashSet;
import java.util.List;

public class GroupMenuService {

    private static final String WORK_DIARY_GROUP = "WORK_DIARY_GROUP";

    private final CodeGroupOptionsRepository codeGroupOptionsRepository;
    private final WorkDiaryGroupMenuRepository workDiaryGroupMenuRepository;

    public GroupMenuService(
            CodeGroupOptionsRepository codeGroupOptionsRepository,
            WorkDiaryGroupMenuRepository workDiaryGroupMenuRepository
    ) {
        this.codeGroupOptionsRepository = codeGroupOptionsRepository;
        this.workDiaryGroupMenuRepository = workDiaryGroupMenuRepository;
    }

    public List<GroupMenuAssignment> list() {
        List<GroupMenuAssignment> assignments = new ArrayList<>();
        for (CodeOptionView group : activeGroups()) {
            assignments.add(new GroupMenuAssignment(
                    group.id(),
                    group.name(),
                    workDiaryGroupMenuRepository.findMenuIds(group.id())
            ));
        }
        return assignments;
    }

    public void replace(long workDiaryGroupId, List<String> menuIds) {
        boolean found = activeGroups().stream().anyMatch(group -> group.id() == workDiaryGroupId);
        if (!found) {
            throw new IllegalArgumentException("업무일지 그룹을 찾을 수 없습니다: " + workDiaryGroupId);
        }
        LinkedHashSet<String> distinct = new LinkedHashSet<>();
        if (menuIds != null) {
            for (String menuId : menuIds) {
                if (menuId == null || menuId.isBlank() || !GroupMenuCatalog.contains(menuId)) {
                    throw new IllegalArgumentException("허용할 수 없는 메뉴입니다: " + menuId);
                }
                distinct.add(menuId);
            }
        }
        workDiaryGroupMenuRepository.replace(workDiaryGroupId, List.copyOf(distinct));
    }

    private List<CodeOptionView> activeGroups() {
        return codeGroupOptionsRepository.findActiveOptions(WORK_DIARY_GROUP);
    }

    public record GroupMenuAssignment(long workDiaryGroupId, String groupName, List<String> menuIds) {
    }
}
