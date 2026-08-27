package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.closing.FiscalCalendarService;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.purchase.EtcClaimRepository;
import com.shindong.smartmanager.application.purchase.EtcClaimService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class EtcClaimApplicationConfig {

    @Bean
    public EtcClaimService etcClaimService(
            EtcClaimRepository etcClaimRepository,
            CompanyRepository companyRepository,
            MonthClosingService monthClosingService,
            FiscalCalendarService fiscalCalendarService
    ) {
        return new EtcClaimService(
                etcClaimRepository, companyRepository, monthClosingService, fiscalCalendarService);
    }
}
