package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.sales.SalesOrderRepository;
import com.shindong.smartmanager.application.sales.SalesOrderService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class SalesApplicationConfig {

    @Bean
    public SalesOrderService salesOrderService(
            SalesOrderRepository salesOrderRepository,
            CompanyRepository companyRepository,
            ItemRepository itemRepository,
            DomainEventStore domainEventStore
    ) {
        return new SalesOrderService(
                salesOrderRepository,
                companyRepository,
                itemRepository,
                domainEventStore
        );
    }
}
