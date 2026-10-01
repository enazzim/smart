package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.system.SystemSettingRepository;
import com.shindong.smartmanager.application.system.SystemSettingService;
import com.shindong.smartmanager.application.workdiary.WorkDiaryApproverPolicy;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class SystemSettingApplicationConfig {

    @Bean
    public SystemSettingService systemSettingService(
            SystemSettingRepository systemSettingRepository,
            WorkDiaryApproverPolicy workDiaryApproverPolicy
    ) {
        return new SystemSettingService(systemSettingRepository, workDiaryApproverPolicy::approverOptions);
    }
}
