package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.process.InventoryBalanceRepository;
import com.shindong.smartmanager.application.process.ProcessCodeLookup;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.ProcessService;
import com.shindong.smartmanager.application.process.WipBalanceProjector;
import com.shindong.smartmanager.application.process.WorkCenterLookup;
import com.shindong.smartmanager.application.workstandard.WorkStandardRepository;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class ProcessApplicationConfig {

    @Bean
    public WipBalanceProjector wipBalanceProjector(InventoryBalanceRepository inventoryBalanceRepository) {
        return new WipBalanceProjector(inventoryBalanceRepository);
    }

    @Bean
    public ProcessService processService(
            ProcessRepository processRepository,
            ItemRepository itemRepository,
            ProcessCodeLookup processCodeLookup,
            WorkCenterLookup workCenterLookup,
            WipBalanceProjector wipBalanceProjector,
            WorkStandardRepository workStandardRepository,
            DomainEventStore domainEventStore
    ) {
        return new ProcessService(
                processRepository,
                itemRepository,
                processCodeLookup,
                workCenterLookup,
                wipBalanceProjector,
                workStandardRepository,
                domainEventStore
        );
    }
}
