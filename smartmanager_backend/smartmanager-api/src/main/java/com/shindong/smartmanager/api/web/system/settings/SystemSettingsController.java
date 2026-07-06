package com.shindong.smartmanager.api.web.system.settings;

import com.shindong.smartmanager.api.security.SecurityUtils;
import com.shindong.smartmanager.infrastructure.application.SystemSettingApplicationService;
import jakarta.validation.Valid;
import java.util.List;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/v1/system/settings")
public class SystemSettingsController {

    private final SystemSettingApplicationService systemSettingApplicationService;

    public SystemSettingsController(SystemSettingApplicationService systemSettingApplicationService) {
        this.systemSettingApplicationService = systemSettingApplicationService;
    }

    @GetMapping
    @PreAuthorize("hasAuthority('system:settings:read')")
    public List<SystemSettingResponse> list() {
        return systemSettingApplicationService.listManagedSettings().stream()
                .map(SystemSettingResponse::from)
                .toList();
    }

    @PutMapping("/{settingKey:.+}")
    @PreAuthorize("hasAuthority('system:settings:write')")
    public SystemSettingResponse update(
            @PathVariable("settingKey") String settingKey,
            @Valid @RequestBody UpdateSystemSettingRequest request
    ) {
        var principal = SecurityUtils.requirePrincipal();
        return SystemSettingResponse.from(systemSettingApplicationService.updateSetting(
                settingKey,
                request.value(),
                principal.loginId()
        ));
    }
}
