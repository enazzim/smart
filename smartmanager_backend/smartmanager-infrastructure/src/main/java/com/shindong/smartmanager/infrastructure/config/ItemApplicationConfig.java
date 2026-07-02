package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.item.ItemService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class ItemApplicationConfig {

    @Bean
    public ItemService itemService(ItemRepository itemRepository, DomainEventStore domainEventStore) {
        return new ItemService(itemRepository, domainEventStore);
    }
}
