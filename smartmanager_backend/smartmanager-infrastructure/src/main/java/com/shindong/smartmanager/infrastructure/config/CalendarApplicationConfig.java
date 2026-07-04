package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.calendar.ProductionCalendarEffectiveService;
import com.shindong.smartmanager.application.calendar.ProductionCalendarRepository;
import com.shindong.smartmanager.application.calendar.ProductionCalendarService;
import com.shindong.smartmanager.application.calendar.PublicHolidayRepository;
import com.shindong.smartmanager.application.calendar.WorkCenterCalendarRepository;
import com.shindong.smartmanager.application.calendar.WorkCenterCalendarService;
import com.shindong.smartmanager.application.calendar.WorkCenterCapaLookup;
import com.shindong.smartmanager.application.calendar.WorkCenterCapaService;
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
    public ProductionCalendarEffectiveService productionCalendarEffectiveService(
            ProductionCalendarRepository productionCalendarRepository,
            PublicHolidayRepository publicHolidayRepository
    ) {
        return new ProductionCalendarEffectiveService(productionCalendarRepository, publicHolidayRepository);
    }

    @Bean
    public WorkCenterCalendarService workCenterCalendarService(
            WorkCenterCalendarRepository workCenterCalendarRepository,
            ProductionCalendarRepository productionCalendarRepository,
            WorkCenterLookup workCenterLookup,
            PublicHolidayRepository publicHolidayRepository,
            DomainEventStore domainEventStore
    ) {
        return new WorkCenterCalendarService(
                workCenterCalendarRepository,
                productionCalendarRepository,
                workCenterLookup,
                publicHolidayRepository,
                domainEventStore
        );
    }

    @Bean
    public WorkCenterCapaService workCenterCapaService(
            WorkCenterCalendarService workCenterCalendarService,
            WorkCenterCapaLookup workCenterCapaLookup
    ) {
        return new WorkCenterCapaService(workCenterCalendarService, workCenterCapaLookup);
    }
}
