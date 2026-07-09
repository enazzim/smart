package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.ledger.PartnerLedgerService;
import com.shindong.smartmanager.application.purchase.EtcPurchaseOrderRepository;
import com.shindong.smartmanager.application.purchase.EtcPurchaseOrderService;
import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptRepository;
import com.shindong.smartmanager.application.purchase.EtcPurchaseReceiptService;
import com.shindong.smartmanager.application.purchase.PurchaseHistoryRepository;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class EtcPurchaseApplicationConfig {

    @Bean
    public EtcPurchaseOrderService etcPurchaseOrderService(
            EtcPurchaseOrderRepository orderRepository,
            CompanyRepository companyRepository,
            MonthClosingService monthClosingService
    ) {
        return new EtcPurchaseOrderService(orderRepository, companyRepository, monthClosingService);
    }

    @Bean
    public EtcPurchaseReceiptService etcPurchaseReceiptService(
            EtcPurchaseOrderRepository orderRepository,
            EtcPurchaseReceiptRepository receiptRepository,
            PurchaseHistoryRepository purchaseHistoryRepository,
            PartnerLedgerService partnerLedgerService,
            MonthClosingService monthClosingService,
            FiscalCalendarService fiscalCalendarService
    ) {
        return new EtcPurchaseReceiptService(
                orderRepository,
                receiptRepository,
                purchaseHistoryRepository,
                partnerLedgerService,
                monthClosingService,
                fiscalCalendarService
        );
    }
}
