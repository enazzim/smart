package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.bom.BomHistoryProjector;
import com.shindong.smartmanager.application.bom.ItemCompositionRepository;
import com.shindong.smartmanager.application.bom.ItemCompositionService;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class ItemCompositionApplicationConfig {

    @Bean
    public BomHistoryProjector bomHistoryProjector(ItemCompositionRepository itemCompositionRepository) {
        return new BomHistoryProjector(itemCompositionRepository);
    }

    @Bean
    public ItemCompositionService itemCompositionService(
            ItemCompositionRepository itemCompositionRepository,
            ItemRepository itemRepository,
            BomHistoryProjector bomHistoryProjector,
            DomainEventStore domainEventStore
    ) {
        return new ItemCompositionService(
                itemCompositionRepository,
                itemRepository,
                bomHistoryProjector,
                domainEventStore
        );
    }
}
