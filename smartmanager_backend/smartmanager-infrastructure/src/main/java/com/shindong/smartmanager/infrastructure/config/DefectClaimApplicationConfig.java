package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.item.ItemRepository;
import com.shindong.smartmanager.application.purchase.DefectClaimRepository;
import com.shindong.smartmanager.application.purchase.DefectClaimService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class DefectClaimApplicationConfig {

    @Bean
    public DefectClaimService defectClaimService(
            DefectClaimRepository defectClaimRepository,
            CompanyRepository companyRepository,
            ItemRepository itemRepository,
            MonthClosingService monthClosingService,
            FiscalCalendarService fiscalCalendarService
    ) {
        return new DefectClaimService(
                defectClaimRepository,
                companyRepository,
                itemRepository,
                monthClosingService,
                fiscalCalendarService
        );
    }
}
