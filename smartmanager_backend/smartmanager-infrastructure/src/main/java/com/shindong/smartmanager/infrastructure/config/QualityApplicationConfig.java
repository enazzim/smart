package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.outsource.OutsourcingOrderRepository;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptRepository;
import com.shindong.smartmanager.application.outsource.OutsourcingReceiptService;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptRepository;
import com.shindong.smartmanager.application.purchase.PurchaseReceiptService;
import com.shindong.smartmanager.application.quality.QualityInspectionRepository;
import com.shindong.smartmanager.application.quality.QualityInspectionService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class QualityApplicationConfig {

    @Bean
    public QualityInspectionService qualityInspectionService(
            QualityInspectionRepository inspectionRepository,
            PurchaseReceiptRepository purchaseReceiptRepository,
            PurchaseReceiptService purchaseReceiptService,
            OutsourcingReceiptRepository outsourcingReceiptRepository,
            OutsourcingReceiptService outsourcingReceiptService,
            OutsourcingOrderRepository outsourcingOrderRepository,
            MonthClosingService monthClosingService,
            FiscalCalendarService fiscalCalendarService
    ) {
        return new QualityInspectionService(
                inspectionRepository,
                purchaseReceiptRepository,
                purchaseReceiptService,
                outsourcingReceiptRepository,
                outsourcingReceiptService,
                outsourcingOrderRepository,
                monthClosingService,
                fiscalCalendarService
        );
    }
}
