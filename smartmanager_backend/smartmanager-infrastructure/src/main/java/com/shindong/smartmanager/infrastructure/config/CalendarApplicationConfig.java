package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.calendar.ProductionCalendarRepository;
import com.shindong.smartmanager.application.calendar.ProductionCalendarService;
import com.shindong.smartmanager.application.calendar.WorkCenterCalendarRepository;
import com.shindong.smartmanager.application.calendar.WorkCenterCalendarService;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.process.WorkCenterLookup;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class CalendarApplicationConfig {

    @Bean
    public ProductionCalendarService productionCalendarService(
            ProductionCalendarRepository productionCalendarRepository,
            DomainEventStore domainEventStore
    ) {
        return new ProductionCalendarService(productionCalendarRepository, domainEventStore);
    }

    @Bean
    public WorkCenterCalendarService workCenterCalendarService(
            WorkCenterCalendarRepository workCenterCalendarRepository,
            ProductionCalendarRepository productionCalendarRepository,
            WorkCenterLookup workCenterLookup,
            DomainEventStore domainEventStore
    ) {
        return new WorkCenterCalendarService(
                workCenterCalendarRepository,
                productionCalendarRepository,
                workCenterLookup,
                domainEventStore
        );
    }
}
