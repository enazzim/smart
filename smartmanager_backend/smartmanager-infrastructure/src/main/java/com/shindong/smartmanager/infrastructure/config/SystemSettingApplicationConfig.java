package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.system.SystemSettingRepository;
import com.shindong.smartmanager.application.system.SystemSettingService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class SystemSettingApplicationConfig {

    @Bean
    public SystemSettingService systemSettingService(SystemSettingRepository systemSettingRepository) {
        return new SystemSettingService(systemSettingRepository);
    }
}
