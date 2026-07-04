package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.process.ProcessCodeLookup;
import com.shindong.smartmanager.application.workcenter.WorkCenterRepository;
import com.shindong.smartmanager.application.workcenter.WorkCenterService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class WorkCenterApplicationConfig {

    @Bean
    public WorkCenterService workCenterService(
            WorkCenterRepository workCenterRepository,
            ProcessCodeLookup processCodeLookup,
            DomainEventStore domainEventStore
    ) {
        return new WorkCenterService(workCenterRepository, processCodeLookup, domainEventStore);
    }
}
