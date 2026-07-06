package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.production.MrpRepository;
import com.shindong.smartmanager.application.purchase.PurchaseOrderRepository;
import com.shindong.smartmanager.application.purchase.PurchaseOrderService;
import com.shindong.smartmanager.application.unitprice.UnitPriceRepository;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class PurchaseApplicationConfig {

    @Bean
    public PurchaseOrderService purchaseOrderService(
            PurchaseOrderRepository purchaseOrderRepository,
            CompanyRepository companyRepository,
            ItemRepository itemRepository,
            MrpRepository mrpRepository,
            UnitPriceRepository unitPriceRepository,
            DomainEventStore domainEventStore
    ) {
        return new PurchaseOrderService(
                purchaseOrderRepository,
                companyRepository,
                itemRepository,
                mrpRepository,
                unitPriceRepository,
                domainEventStore
        );
    }
}
