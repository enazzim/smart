package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.inventory.InventoryBalanceMonthlyRepository;
import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.inventory.InventoryLedgerQueryService;
import com.shindong.smartmanager.application.inventory.InventoryLedgerRepository;
import com.shindong.smartmanager.application.inventory.InventoryLocationQueryRepository;
import com.shindong.smartmanager.application.inventory.InventoryStockBalanceRepository;
import com.shindong.smartmanager.application.inventory.StockMovementRepository;
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
}
