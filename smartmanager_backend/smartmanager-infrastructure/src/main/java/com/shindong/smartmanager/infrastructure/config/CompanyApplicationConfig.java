package com.shindong.smartmanager.infrastructure.config;

import com.shindong.smartmanager.application.company.CompanyRepository;
import com.shindong.smartmanager.application.company.CompanyService;
import com.shindong.smartmanager.application.company.PartnerLedgerAccountRepository;
import com.shindong.smartmanager.application.company.PartnerLedgerProjector;
import com.shindong.smartmanager.application.event.DomainEventStore;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class CompanyApplicationConfig {

    @Bean
    public PartnerLedgerProjector partnerLedgerProjector(PartnerLedgerAccountRepository ledgerAccountRepository) {
        return new PartnerLedgerProjector(ledgerAccountRepository);
    }

    @Bean
    public CompanyService companyService(
            CompanyRepository companyRepository,
            PartnerLedgerProjector partnerLedgerProjector,
            DomainEventStore domainEventStore
    ) {
        return new CompanyService(companyRepository, partnerLedgerProjector, domainEventStore);
    }
}
