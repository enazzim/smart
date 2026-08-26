package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;

import com.shindong.smartmanager.application.purchase.EtcClaimCommand;
import com.shindong.smartmanager.application.purchase.EtcClaimListCriteria;
import com.shindong.smartmanager.application.purchase.EtcClaimRepository;
import com.shindong.smartmanager.application.purchase.EtcClaimView;
import com.shindong.smartmanager.domain.purchase.EtcClaimRecognition;
import com.shindong.smartmanager.infrastructure.persistence.company.CompanyJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.company.SpringDataCompanyRepository;
import java.time.Instant;
import java.time.ZoneId;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaEtcClaimRepository implements EtcClaimRepository {

    private static final ZoneId SEOUL = ZoneId.of("Asia/Seoul");

    private final SpringDataEtcClaimRepository etcClaimRepository;
    private final SpringDataCompanyRepository companyRepository;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaEtcClaimRepository(
            SpringDataEtcClaimRepository etcClaimRepository,
            SpringDataCompanyRepository companyRepository,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.etcClaimRepository = etcClaimRepository;
        this.companyRepository = companyRepository;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    @Transactional
    public long save(EtcClaimCommand command, int fiscalYear, int fiscalMonth, String actorUserId) {
        Instant now = Instant.now();
        EtcClaimJpaEntity entity = new EtcClaimJpaEntity();
        entity.setPartnerId(command.partnerId());
        entity.setReceiptDate(command.receiptDate());
        entity.setReason(command.reason());
        entity.setAmount(command.amount());
        entity.setFiscalYear((short) fiscalYear);
        entity.setFiscalMonth((byte) fiscalMonth);
        entity.setRecognition(EtcClaimRecognition.PENDING);
        entity.setRecordingState(1);
        entity.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setCreatedAt(now);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        return etcClaimRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void update(long id, EtcClaimCommand command, int fiscalYear, int fiscalMonth, String actorUserId) {
        EtcClaimJpaEntity entity = etcClaimRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("기타공제를 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setPartnerId(command.partnerId());
        entity.setReceiptDate(command.receiptDate());
        entity.setReason(command.reason());
        entity.setAmount(command.amount());
        entity.setFiscalYear((short) fiscalYear);
        entity.setFiscalMonth((byte) fiscalMonth);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        etcClaimRepository.save(entity);
    }

    @Override
    @Transactional
    public void softDelete(long id, String actorUserId) {
        EtcClaimJpaEntity entity = etcClaimRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("기타공제를 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setRecordingState(0);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        etcClaimRepository.save(entity);
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<EtcClaimView> findActiveById(long id) {
        return etcClaimRepository.findByIdAndRecordingState(id, 1).map(this::toView);
    }

    @Override
    @Transactional(readOnly = true)
    public List<EtcClaimView> findActive(EtcClaimListCriteria criteria) {
        Instant registeredFrom = null;
        Instant registeredToExclusive = null;
        if (criteria != null && criteria.registeredOn() != null) {
            registeredFrom = criteria.registeredOn().atStartOfDay(SEOUL).toInstant();
            registeredToExclusive = criteria.registeredOn().plusDays(1).atStartOfDay(SEOUL).toInstant();
        }

        Long partnerId = criteria != null ? criteria.partnerId() : null;
        String partnerName = criteria != null && criteria.partnerName() != null
                ? criteria.partnerName().trim()
                : "";
        String reason = criteria != null && criteria.reason() != null ? criteria.reason().trim() : "";

        List<EtcClaimJpaEntity> rows = etcClaimRepository.search(
                partnerId,
                criteria != null ? criteria.receiptDateFrom() : null,
                criteria != null ? criteria.receiptDateTo() : null,
                reason,
                registeredFrom,
                registeredToExclusive
        );

        return rows.stream()
                .map(this::toView)
                .filter(view -> partnerName.isEmpty()
                        || view.partnerName().toLowerCase().contains(partnerName.toLowerCase())
                        || view.partnerBusinessRegNo().toLowerCase().contains(partnerName.toLowerCase()))
                .toList();
    }

    private EtcClaimView toView(EtcClaimJpaEntity entity) {
        CompanyJpaEntity company = companyRepository.findById(entity.getPartnerId()).orElse(null);
        return new EtcClaimView(
                entity.getId(),
                entity.getPartnerId(),
                company != null ? company.getCompanyName() : "",
                company != null ? company.getBusinessRegNo() : "",
                entity.getReceiptDate(),
                entity.getReason(),
                entity.getAmount(),
                entity.getFiscalYear(),
                entity.getFiscalMonth(),
                entity.getRecognition(),
                masterAuditActorLookup.nameOf(entity.getCreatedById()),
                entity.getCreatedAt(),
                entity.getRecognition() == EtcClaimRecognition.PENDING
        );
    }
}
