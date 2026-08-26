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
import com.shindong.smartmanager.application.sales.SalesOrderFulfillmentSyncService;
import com.shindong.smartmanager.application.sales.SalesOrderRepository;
import com.shindong.smartmanager.application.sales.SalesOrderService;
import com.shindong.smartmanager.application.sales.SalesRevenueInventoryService;
import com.shindong.smartmanager.application.sales.SalesRevenueRepository;
import com.shindong.smartmanager.application.sales.SalesRevenueService;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderRepository;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.WipBalanceProjector;
import com.shindong.smartmanager.application.production.ProductionPlanRepository;
import com.shindong.smartmanager.application.production.WorkOrderRepository;
import com.shindong.smartmanager.application.production.WorkPlanRepository;
import com.shindong.smartmanager.application.sales.SalesShipmentInventoryService;
import com.shindong.smartmanager.application.sales.SalesShipmentRepository;
import com.shindong.smartmanager.application.sales.SalesShipmentService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class SalesApplicationConfig {

    @Bean
    public SalesOrderFulfillmentSyncService salesOrderFulfillmentSyncService(
            SalesOrderRepository salesOrderRepository,
            ProductionPlanRepository productionPlanRepository,
            WorkPlanRepository workPlanRepository,
            WorkOrderRepository workOrderRepository,
            OutsourcingOrderRepository outsourcingOrderRepository,
            ProcessRepository processRepository
    ) {
        return new SalesOrderFulfillmentSyncService(
                salesOrderRepository,
                productionPlanRepository,
                workPlanRepository,
                workOrderRepository,
                outsourcingOrderRepository,
                processRepository
        );
    }

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
            InventoryBalanceService inventoryBalanceService,
            ProcessRepository processRepository,
            WipBalanceProjector wipBalanceProjector
    ) {
        return new SalesShipmentInventoryService(
                inventoryBalanceService,
                processRepository,
                wipBalanceProjector
        );
    }

    @Bean
    public SalesShipmentService salesShipmentService(
            SalesShipmentRepository salesShipmentRepository,
            SalesShipmentInventoryService salesShipmentInventoryService,
            ItemRepository itemRepository,
            MonthClosingService monthClosingService,
            DomainEventStore domainEventStore
    ) {
        return new SalesShipmentService(
                salesShipmentRepository,
                salesShipmentInventoryService,
                itemRepository,
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
            ItemRepository itemRepository,
            PartnerLedgerService partnerLedgerService,
            FiscalCalendarService fiscalCalendarService,
            MonthClosingService monthClosingService,
            DomainEventStore domainEventStore
    ) {
        return new SalesRevenueService(
                salesRevenueRepository,
                salesHistoryRepository,
                salesRevenueInventoryService,
                itemRepository,
                partnerLedgerService,
                fiscalCalendarService,
                monthClosingService,
                domainEventStore
        );
    }
}
