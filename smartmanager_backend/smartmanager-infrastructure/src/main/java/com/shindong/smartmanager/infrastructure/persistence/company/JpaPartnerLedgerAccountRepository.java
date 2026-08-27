package com.shindong.smartmanager.infrastructure.persistence.company;

import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;

import com.shindong.smartmanager.application.company.PartnerLedgerAccountRepository;
import com.shindong.smartmanager.domain.company.PartnerLedgerType;
import java.time.Instant;
import java.util.List;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaPartnerLedgerAccountRepository implements PartnerLedgerAccountRepository {

    private final SpringDataPartnerLedgerAccountRepository repository;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaPartnerLedgerAccountRepository(
            SpringDataPartnerLedgerAccountRepository repository,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.repository = repository;
        this.masterAuditActorLookup = masterAuditActorLookup;
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
                                existing.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
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
                            entity.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
                            entity.setCreatedAt(now);
                            entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
                            entity.setUpdatedAt(now);
                            repository.save(entity);
                        }
                );
    }

    @Override
    @Transactional
    public void deactivateByCompanyId(long companyId, String actorUserId) {
        Instant now = Instant.now();
        List<PartnerLedgerAccountJpaEntity> accounts =
                repository.findByCompanyIdAndRecordingState(companyId, 1);
        for (PartnerLedgerAccountJpaEntity account : accounts) {
            account.setRecordingState(0);
            account.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
            account.setUpdatedAt(now);
        }
        repository.saveAll(accounts);
    }
}
