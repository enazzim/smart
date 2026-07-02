package com.shindong.smartmanager.infrastructure.persistence.company;

import com.shindong.smartmanager.application.company.PartnerLedgerAccountRepository;
import com.shindong.smartmanager.domain.company.PartnerLedgerType;
import java.time.Instant;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaPartnerLedgerAccountRepository implements PartnerLedgerAccountRepository {

    private final SpringDataPartnerLedgerAccountRepository repository;

    public JpaPartnerLedgerAccountRepository(SpringDataPartnerLedgerAccountRepository repository) {
        this.repository = repository;
    }

    @Override
    @Transactional
    public void ensureAccount(long companyId, int fiscalYear, PartnerLedgerType ledgerType, String actorUserId) {
        repository.findByCompanyIdAndFiscalYearAndLedgerType(companyId, (short) fiscalYear, ledgerType)
                .ifPresentOrElse(
                        existing -> {
                            if (existing.getRecordingState() != 1) {
                                Instant now = Instant.now();
                                existing.setRecordingState(1);
                                existing.setUpdatedBy(actorUserId);
                                existing.setUpdatedById(actorUserId);
                                existing.setUpdatedAt(now);
                                repository.save(existing);
                            }
                        },
                        () -> {
                            Instant now = Instant.now();
                            PartnerLedgerAccountJpaEntity entity = new PartnerLedgerAccountJpaEntity();
                            entity.setCompanyId(companyId);
                            entity.setFiscalYear((short) fiscalYear);
                            entity.setLedgerType(ledgerType);
                            entity.setRecordingState(1);
                            entity.setCreatedBy(actorUserId);
                            entity.setCreatedById(actorUserId);
                            entity.setCreatedAt(now);
                            entity.setUpdatedBy(actorUserId);
                            entity.setUpdatedById(actorUserId);
                            entity.setUpdatedAt(now);
                            repository.save(entity);
                        }
                );
    }
}
