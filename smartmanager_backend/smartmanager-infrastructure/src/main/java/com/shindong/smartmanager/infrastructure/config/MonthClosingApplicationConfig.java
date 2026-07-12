package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.MonthClosingRepository;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.system.SystemSettingService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class MonthClosingApplicationConfig {

    @Bean
    public FiscalCalendarService fiscalCalendarService(SystemSettingService systemSettingService) {
        return new FiscalCalendarService(systemSettingService);
    }

    @Bean
    public MonthClosingService monthClosingService(
            MonthClosingRepository monthClosingRepository,
            FiscalCalendarService fiscalCalendarService
    ) {
        return new MonthClosingService(monthClosingRepository, fiscalCalendarService);
    }
}
