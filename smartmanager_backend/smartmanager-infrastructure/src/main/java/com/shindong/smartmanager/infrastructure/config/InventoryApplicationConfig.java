package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.inventory.InventoryBalanceMonthlyRepository;
import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.inventory.InventoryLedgerQueryService;
import com.shindong.smartmanager.application.inventory.InventoryLedgerRepository;
import com.shindong.smartmanager.application.inventory.InventoryLocationQueryRepository;
import com.shindong.smartmanager.application.inventory.InventoryStockBalanceRepository;
import com.shindong.smartmanager.application.inventory.MiscStockMovementInventoryService;
import com.shindong.smartmanager.application.inventory.MiscStockMovementRepository;
import com.shindong.smartmanager.application.inventory.MiscStockMovementResolver;
import com.shindong.smartmanager.application.inventory.MiscStockMovementService;
import com.shindong.smartmanager.application.inventory.StockMovementRepository;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.process.ProcessRepository;
import com.shindong.smartmanager.application.process.WipBalanceProjector;
import com.shindong.smartmanager.application.system.SystemSettingService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class InventoryApplicationConfig {

    @Bean
    public InventoryBalanceService inventoryBalanceService(
            InventoryLocationQueryRepository locationRepository,
            InventoryStockBalanceRepository balanceRepository,
            StockMovementRepository movementRepository,
            InventoryBalanceMonthlyRepository monthlyRepository,
            FiscalCalendarService fiscalCalendarService,
            MonthClosingService monthClosingService,
            SystemSettingService systemSettingService
    ) {
        return new InventoryBalanceService(
                locationRepository,
                balanceRepository,
                movementRepository,
                monthlyRepository,
                fiscalCalendarService,
                monthClosingService,
                systemSettingService
        );
    }

    @Bean
    public InventoryLedgerQueryService inventoryLedgerQueryService(
            InventoryLedgerRepository ledgerRepository
    ) {
        return new InventoryLedgerQueryService(ledgerRepository);
    }

    @Bean
    public MiscStockMovementResolver miscStockMovementResolver(ProcessRepository processRepository) {
        return new MiscStockMovementResolver(processRepository);
    }

    @Bean
    public MiscStockMovementInventoryService miscStockMovementInventoryService(
            InventoryBalanceService inventoryBalanceService,
            WipBalanceProjector wipBalanceProjector
    ) {
        return new MiscStockMovementInventoryService(inventoryBalanceService, wipBalanceProjector);
    }

    @Bean
    public MiscStockMovementService miscStockMovementService(
            MiscStockMovementRepository miscStockMovementRepository,
            ItemRepository itemRepository,
            MiscStockMovementResolver miscStockMovementResolver,
            MiscStockMovementInventoryService miscStockMovementInventoryService,
            MonthClosingService monthClosingService
    ) {
        return new MiscStockMovementService(
                miscStockMovementRepository,
                itemRepository,
                miscStockMovementResolver,
                miscStockMovementInventoryService,
                monthClosingService
        );
    }
}
