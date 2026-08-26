package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;

import com.shindong.smartmanager.application.purchase.DefectClaimCommand;
import com.shindong.smartmanager.application.purchase.DefectClaimListCriteria;
import com.shindong.smartmanager.application.purchase.DefectClaimRepository;
import com.shindong.smartmanager.application.purchase.DefectClaimView;
import com.shindong.smartmanager.domain.purchase.EtcClaimRecognition;
import com.shindong.smartmanager.infrastructure.persistence.company.CompanyJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.company.SpringDataCompanyRepository;
import com.shindong.smartmanager.infrastructure.persistence.item.ItemJpaEntity;
import com.shindong.smartmanager.infrastructure.persistence.item.SpringDataItemRepository;
import java.time.Instant;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaDefectClaimRepository implements DefectClaimRepository {

    private final SpringDataDefectClaimRepository defectClaimRepository;
    private final SpringDataCompanyRepository companyRepository;
    private final SpringDataItemRepository itemRepository;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaDefectClaimRepository(
            SpringDataDefectClaimRepository defectClaimRepository,
            SpringDataCompanyRepository companyRepository,
            SpringDataItemRepository itemRepository,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.defectClaimRepository = defectClaimRepository;
        this.companyRepository = companyRepository;
        this.itemRepository = itemRepository;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    @Transactional
    public long save(DefectClaimCommand command, int fiscalYear, int fiscalMonth, String actorUserId) {
        Instant now = Instant.now();
        DefectClaimJpaEntity entity = new DefectClaimJpaEntity();
        entity.setPartnerId(command.partnerId());
        entity.setItemId(command.itemId());
        entity.setReceiptDate(command.receiptDate());
        entity.setClaimQty(command.claimQty());
        entity.setAmount(command.amount());
        entity.setReason(command.reason());
        entity.setFiscalYear((short) fiscalYear);
        entity.setFiscalMonth((byte) fiscalMonth);
        entity.setRecognition(EtcClaimRecognition.PENDING);
        entity.setRecordingState(1);
        entity.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setCreatedAt(now);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        return defectClaimRepository.save(entity).getId();
    }

    @Override
    @Transactional
    public void update(long id, DefectClaimCommand command, int fiscalYear, int fiscalMonth, String actorUserId) {
        DefectClaimJpaEntity entity = defectClaimRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("불량변상을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setPartnerId(command.partnerId());
        entity.setItemId(command.itemId());
        entity.setReceiptDate(command.receiptDate());
        entity.setClaimQty(command.claimQty());
        entity.setAmount(command.amount());
        entity.setReason(command.reason());
        entity.setFiscalYear((short) fiscalYear);
        entity.setFiscalMonth((byte) fiscalMonth);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        defectClaimRepository.save(entity);
    }

    @Override
    @Transactional
    public void softDelete(long id, String actorUserId) {
        DefectClaimJpaEntity entity = defectClaimRepository.findByIdAndRecordingState(id, 1)
                .orElseThrow(() -> new IllegalArgumentException("불량변상을 찾을 수 없습니다: " + id));
        Instant now = Instant.now();
        entity.setRecordingState(0);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        defectClaimRepository.save(entity);
    }

    @Override
    @Transactional(readOnly = true)
    public Optional<DefectClaimView> findActiveById(long id) {
        return defectClaimRepository.findByIdAndRecordingState(id, 1).map(this::toView);
    }

    @Override
    @Transactional(readOnly = true)
    public List<DefectClaimView> findActive(DefectClaimListCriteria criteria) {
        Long partnerId = criteria != null ? criteria.partnerId() : null;
        Long itemId = criteria != null ? criteria.itemId() : null;
        String partnerName = criteria != null && criteria.partnerName() != null
                ? criteria.partnerName().trim()
                : "";
        String itemNo = criteria != null && criteria.itemNo() != null
                ? criteria.itemNo().trim()
                : "";
        String itemName = criteria != null && criteria.itemName() != null
                ? criteria.itemName().trim()
                : "";

        List<DefectClaimJpaEntity> rows = defectClaimRepository.search(
                partnerId,
                itemId,
                criteria != null ? criteria.receiptDateFrom() : null,
                criteria != null ? criteria.receiptDateTo() : null
        );

        return rows.stream()
                .map(this::toView)
                .filter(view -> partnerName.isEmpty()
                        || view.partnerName().toLowerCase().contains(partnerName.toLowerCase())
                        || view.partnerBusinessRegNo().toLowerCase().contains(partnerName.toLowerCase()))
                .filter(view -> itemNo.isEmpty()
                        || view.itemNo().toLowerCase().contains(itemNo.toLowerCase()))
                .filter(view -> itemName.isEmpty()
                        || view.itemName().toLowerCase().contains(itemName.toLowerCase()))
                .toList();
    }

    private DefectClaimView toView(DefectClaimJpaEntity entity) {
        CompanyJpaEntity company = companyRepository.findById(entity.getPartnerId()).orElse(null);
        ItemJpaEntity item = itemRepository.findById(entity.getItemId()).orElse(null);
        String standard = item != null && item.getStandard() != null ? item.getStandard() : "";
        return new DefectClaimView(
                entity.getId(),
                entity.getPartnerId(),
                company != null ? company.getCompanyName() : "",
                company != null ? company.getBusinessRegNo() : "",
                entity.getItemId(),
                item != null ? item.getItemNo() : "",
                item != null ? item.getItemName() : "",
                standard,
                entity.getReceiptDate(),
                entity.getClaimQty(),
                entity.getAmount(),
                entity.getReason(),
                entity.getFiscalYear(),
                entity.getFiscalMonth(),
                entity.getRecognition(),
                masterAuditActorLookup.nameOf(entity.getCreatedById()),
                entity.getCreatedAt(),
                entity.getRecognition() == EtcClaimRecognition.PENDING
        );
    }
}
