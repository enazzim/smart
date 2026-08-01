package com.shindong.smartmanager.infrastructure.config;



import com.shindong.smartmanager.application.bom.ItemCompositionRepository;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.ledger.PartnerLedgerService;
import com.shindong.smartmanager.application.outsource.OutsourceHistoryRepository;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptInventoryService;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptRepository;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptService;
import com.shindong.smartmanager.application.purchase.PartnerPaymentRepository;
import com.shindong.smartmanager.application.quality.QualityInspectionRepository;
import com.shindong.smartmanager.application.system.SystemSettingService;
import com.shindong.smartmanager.application.closing.MonthClosingService;

import com.shindong.smartmanager.application.company.CompanyRepository;

import com.shindong.smartmanager.application.event.DomainEventStore;

import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.inventory.LotService;
import com.shindong.smartmanager.application.item.ItemRepository;

import com.shindong.smartmanager.application.outsource.OutsourcingOrderRepository;

import com.shindong.smartmanager.application.outsource.OutsourcingOrderService;

import com.shindong.smartmanager.application.outsource.OutsourcingShipmentConsumptionCalculator;

import com.shindong.smartmanager.application.outsource.OutsourcingShipmentInventoryService;

import com.shindong.smartmanager.application.outsource.OutsourcingShipmentRepository;

import com.shindong.smartmanager.application.outsource.OutsourcingShipmentService;

import com.shindong.smartmanager.application.process.InventoryBalanceRepository;

import com.shindong.smartmanager.application.process.ProcessRepository;

import com.shindong.smartmanager.application.process.WipBalanceProjector;

import com.shindong.smartmanager.application.production.WorkPlanRepository;

import com.shindong.smartmanager.application.unitprice.OutsourceInputBalanceProjector;

import com.shindong.smartmanager.application.unitprice.UnitPriceRepository;

import org.springframework.context.annotation.Bean;

import org.springframework.context.annotation.Configuration;



@Configuration

public class OutsourceApplicationConfig {



    @Bean

    public OutsourcingOrderService outsourcingOrderService(

            OutsourcingOrderRepository outsourcingOrderRepository,

            WorkPlanRepository workPlanRepository,

            CompanyRepository companyRepository,

            ItemRepository itemRepository,

            UnitPriceRepository unitPriceRepository,

            DomainEventStore domainEventStore

    ) {

        return new OutsourcingOrderService(

                outsourcingOrderRepository,

                workPlanRepository,

                companyRepository,

                itemRepository,

                unitPriceRepository,

                domainEventStore

        );

    }



    @Bean

    public OutsourcingShipmentConsumptionCalculator outsourcingShipmentConsumptionCalculator(

            OutsourceInputBalanceProjector outsourceInputBalanceProjector,

            ItemCompositionRepository itemCompositionRepository,

            ItemRepository itemRepository,

            ProcessRepository processRepository

    ) {

        return new OutsourcingShipmentConsumptionCalculator(

                outsourceInputBalanceProjector,

                itemCompositionRepository,

                itemRepository,

                processRepository

        );

    }



    @Bean

    public OutsourcingShipmentInventoryService outsourcingShipmentInventoryService(

            InventoryBalanceService inventoryBalanceService,

            InventoryBalanceRepository inventoryBalanceRepository,

            WipBalanceProjector wipBalanceProjector

    ) {

        return new OutsourcingShipmentInventoryService(

                inventoryBalanceService,

                inventoryBalanceRepository,

                wipBalanceProjector

        );

    }



    @Bean

    public OutsourcingShipmentService outsourcingShipmentService(

            OutsourcingShipmentRepository outsourcingShipmentRepository,

            OutsourcingOrderRepository outsourcingOrderRepository,

            OutsourcingShipmentConsumptionCalculator outsourcingShipmentConsumptionCalculator,

            OutsourcingShipmentInventoryService outsourcingShipmentInventoryService,

            InventoryBalanceService inventoryBalanceService,

            ItemRepository itemRepository,

            ProcessRepository processRepository,

            CompanyRepository companyRepository,

            UnitPriceRepository unitPriceRepository,

            MonthClosingService monthClosingService,

            DomainEventStore domainEventStore

    ) {

        return new OutsourcingShipmentService(

                outsourcingShipmentRepository,

                outsourcingOrderRepository,

                outsourcingShipmentConsumptionCalculator,

                outsourcingShipmentInventoryService,

                inventoryBalanceService,

                itemRepository,

                processRepository,

                companyRepository,

                unitPriceRepository,

                monthClosingService,

                domainEventStore

        );

    }

    @Bean
    public OutsourcingReceiptInventoryService outsourcingReceiptInventoryService(
            InventoryBalanceService inventoryBalanceService,
            InventoryBalanceRepository inventoryBalanceRepository,
            WipBalanceProjector wipBalanceProjector,
            ProcessRepository processRepository,
            ItemRepository itemRepository,
            OutsourcingShipmentConsumptionCalculator outsourcingShipmentConsumptionCalculator,
            SystemSettingService systemSettingService
    ) {
        return new OutsourcingReceiptInventoryService(
                inventoryBalanceService,
                inventoryBalanceRepository,
                wipBalanceProjector,
                processRepository,
                itemRepository,
                outsourcingShipmentConsumptionCalculator,
                systemSettingService
        );
    }

    @Bean
    public OutsourcingReceiptService outsourcingReceiptService(
            OutsourcingReceiptRepository outsourcingReceiptRepository,
            OutsourcingOrderRepository outsourcingOrderRepository,
            OutsourceHistoryRepository outsourceHistoryRepository,
            QualityInspectionRepository qualityInspectionRepository,
            OutsourcingReceiptInventoryService outsourcingReceiptInventoryService,
            ItemRepository itemRepository,
            LotService lotService,
            PartnerLedgerService partnerLedgerService,
            PartnerPaymentRepository partnerPaymentRepository,
            MonthClosingService monthClosingService,
            FiscalCalendarService fiscalCalendarService,
            DomainEventStore domainEventStore
    ) {
        return new OutsourcingReceiptService(
                outsourcingReceiptRepository,
                outsourcingOrderRepository,
                outsourceHistoryRepository,
                qualityInspectionRepository,
                outsourcingReceiptInventoryService,
                itemRepository,
                lotService,
                partnerLedgerService,
                partnerPaymentRepository,
                monthClosingService,
                fiscalCalendarService,
                domainEventStore
        );
    }

}

