package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.WorkCenterLookup;
import com.shindong.smartmanager.application.workstandard.WorkStandardRepository;
import com.shindong.smartmanager.application.workstandard.WorkStandardService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class WorkStandardApplicationConfig {

    @Bean
    public WorkStandardService workStandardService(
            WorkStandardRepository workStandardRepository,
            ItemRepository itemRepository,
            ProcessRepository processRepository,
            WorkCenterLookup workCenterLookup,
            DomainEventStore domainEventStore
    ) {
        return new WorkStandardService(
                workStandardRepository,
                itemRepository,
                processRepository,
                workCenterLookup,
                domainEventStore
        );
    }
}
