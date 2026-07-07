package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.ledger.PartnerLedgerService;
import com.shindong.smartmanager.application.sales.SalesCollectionRepository;
import com.shindong.smartmanager.application.sales.SalesCollectionService;
import com.shindong.smartmanager.application.sales.SalesHistoryRepository;
import com.shindong.smartmanager.application.sales.SalesOrderRepository;
import com.shindong.smartmanager.application.sales.SalesOrderService;
import com.shindong.smartmanager.application.sales.SalesRevenueInventoryService;
import com.shindong.smartmanager.application.sales.SalesRevenueRepository;
import com.shindong.smartmanager.application.sales.SalesRevenueService;
import com.shindong.smartmanager.application.sales.SalesShipmentInventoryService;
import com.shindong.smartmanager.application.sales.SalesShipmentRepository;
import com.shindong.smartmanager.application.sales.SalesShipmentService;
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

    @Bean
    public SalesShipmentInventoryService salesShipmentInventoryService(
            InventoryBalanceService inventoryBalanceService
    ) {
        return new SalesShipmentInventoryService(inventoryBalanceService);
    }

    @Bean
    public SalesShipmentService salesShipmentService(
            SalesShipmentRepository salesShipmentRepository,
            SalesShipmentInventoryService salesShipmentInventoryService,
            MonthClosingService monthClosingService,
            DomainEventStore domainEventStore
    ) {
        return new SalesShipmentService(
                salesShipmentRepository,
                salesShipmentInventoryService,
                monthClosingService,
                domainEventStore
        );
    }

    @Bean
    public SalesRevenueInventoryService salesRevenueInventoryService(
            InventoryBalanceService inventoryBalanceService
    ) {
        return new SalesRevenueInventoryService(inventoryBalanceService);
    }

    @Bean
    public SalesCollectionService salesCollectionService(
            SalesCollectionRepository salesCollectionRepository,
            CompanyRepository companyRepository,
            PartnerLedgerService partnerLedgerService,
            MonthClosingService monthClosingService,
            DomainEventStore domainEventStore
    ) {
        return new SalesCollectionService(
                salesCollectionRepository,
                companyRepository,
                partnerLedgerService,
                monthClosingService,
                domainEventStore
        );
    }

    @Bean
    public SalesRevenueService salesRevenueService(
            SalesRevenueRepository salesRevenueRepository,
            SalesHistoryRepository salesHistoryRepository,
            SalesRevenueInventoryService salesRevenueInventoryService,
            PartnerLedgerService partnerLedgerService,
            FiscalCalendarService fiscalCalendarService,
            MonthClosingService monthClosingService,
            DomainEventStore domainEventStore
    ) {
        return new SalesRevenueService(
                salesRevenueRepository,
                salesHistoryRepository,
                salesRevenueInventoryService,
                partnerLedgerService,
                fiscalCalendarService,
                monthClosingService,
                domainEventStore
        );
    }
}
