package com.shindong.smartmanager.infrastructure.persistence.company;

import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataPartnerLedgerMonthlyRepository extends JpaRepository<PartnerLedgerMonthlyJpaEntity, Long> {

    Optional<PartnerLedgerMonthlyJpaEntity> findByLedgerAccountIdAndMonthNumAndRecordingState(
            long ledgerAccountId,
            byte monthNum,
            int recordingState
    );
}
