package com.shindong.smartmanager.application.menu;

import java.util.List;

public interface WorkDiaryGroupMenuRepository {

    List<String> findMenuIds(long workDiaryGroupId);

    void replace(long workDiaryGroupId, List<String> menuIds);

    void deleteByGroupId(long workDiaryGroupId);
}
