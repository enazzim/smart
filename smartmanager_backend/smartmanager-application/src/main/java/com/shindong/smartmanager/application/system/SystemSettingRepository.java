package com.shindong.smartmanager.application.system;

import java.util.List;
import java.util.Optional;

public interface SystemSettingRepository {

    List<SystemSettingView> findAllActive();

    Optional<SystemSettingView> findActiveByKey(String settingKey);

    void upsert(String settingKey, String valueJson, String actorUserId);
}
