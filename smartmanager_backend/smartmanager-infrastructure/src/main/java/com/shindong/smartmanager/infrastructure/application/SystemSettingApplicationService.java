package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.system.SystemSettingItemView;
import com.shindong.smartmanager.application.system.SystemSettingService;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class SystemSettingApplicationService {

    private final SystemSettingService systemSettingService;

    public SystemSettingApplicationService(SystemSettingService systemSettingService) {
        this.systemSettingService = systemSettingService;
    }

    @Transactional(readOnly = true)
    public List<SystemSettingItemView> listManagedSettings() {
        return systemSettingService.listManagedSettings();
    }

    @Transactional
    public SystemSettingItemView updateSetting(String settingKey, String value, String actorUserId) {
        return systemSettingService.updateSetting(settingKey, value, actorUserId);
    }
}
