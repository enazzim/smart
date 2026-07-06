package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.inventory.InventoryBalanceService;
import com.shindong.smartmanager.application.ledger.PartnerLedgerService;
import com.shindong.smartmanager.application.purchase.PurchaseHistoryRepository;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptRepository;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptService;
import com.shindong.smartmanager.application.quality.QualityInspectionRepository;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class PurchaseReceiptApplicationConfig {

    @Bean
    public PurchaseReceiptService purchaseReceiptService(
            PurchaseReceiptRepository receiptRepository,
            PurchaseHistoryRepository purchaseHistoryRepository,
            QualityInspectionRepository qualityInspectionRepository,
            InventoryBalanceService inventoryBalanceService,
            PartnerLedgerService partnerLedgerService,
            MonthClosingService monthClosingService,
            FiscalCalendarService fiscalCalendarService
    ) {
        return new PurchaseReceiptService(
                receiptRepository,
                purchaseHistoryRepository,
                qualityInspectionRepository,
                inventoryBalanceService,
                partnerLedgerService,
                monthClosingService,
                fiscalCalendarService
        );
    }
}
