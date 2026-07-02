package com.shindong.smartmanager.infrastructure.persistence.company;

import com.shindong.smartmanager.domain.company.PartnerLedgerType;
import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataPartnerLedgerAccountRepository extends JpaRepository<PartnerLedgerAccountJpaEntity, Long> {

    Optional<PartnerLedgerAccountJpaEntity> findByCompanyIdAndFiscalYearAndLedgerType(
            Long companyId,
            short fiscalYear,
            PartnerLedgerType ledgerType
    );

    List<PartnerLedgerAccountJpaEntity> findByCompanyIdAndRecordingState(Long companyId, int recordingState);
}
