package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.bom.ItemCompositionRepository;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.process.InventoryBalanceRepository;
import com.shindong.smartmanager.application.process.ProcessCodeLookup;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.unitprice.OutsourceInputBalanceProjector;
import com.shindong.smartmanager.application.unitprice.UnitPriceHistoryProjector;
import com.shindong.smartmanager.application.unitprice.UnitPriceRepository;
import com.shindong.smartmanager.application.unitprice.UnitPriceService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class UnitPriceApplicationConfig {

    @Bean
    public OutsourceInputBalanceProjector outsourceInputBalanceProjector(
            ProcessRepository processRepository,
            InventoryBalanceRepository inventoryBalanceRepository,
            ItemCompositionRepository itemCompositionRepository,
            ItemRepository itemRepository
    ) {
        return new OutsourceInputBalanceProjector(
                processRepository,
                inventoryBalanceRepository,
                itemCompositionRepository,
                itemRepository
        );
    }

    @Bean
    public UnitPriceHistoryProjector unitPriceHistoryProjector(UnitPriceRepository unitPriceRepository) {
        return new UnitPriceHistoryProjector(unitPriceRepository);
    }

    @Bean
    public UnitPriceService unitPriceService(
            UnitPriceRepository unitPriceRepository,
            ItemRepository itemRepository,
            CompanyRepository companyRepository,
            ProcessRepository processRepository,
            ProcessCodeLookup processCodeLookup,
            OutsourceInputBalanceProjector outsourceInputBalanceProjector,
            UnitPriceHistoryProjector unitPriceHistoryProjector,
            DomainEventStore domainEventStore
    ) {
        return new UnitPriceService(
                unitPriceRepository,
                itemRepository,
                companyRepository,
                processRepository,
                processCodeLookup,
                outsourceInputBalanceProjector,
                unitPriceHistoryProjector,
                domainEventStore
        );
    }
}
