package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.stats.StatsReportQueryService;
import com.shindong.smartmanager.application.stats.StatsReportRepository;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class StatsApplicationConfig {

    @Bean
    public StatsReportQueryService statsReportQueryService(StatsReportRepository statsReportRepository) {
        return new StatsReportQueryService(statsReportRepository);
    }
}
