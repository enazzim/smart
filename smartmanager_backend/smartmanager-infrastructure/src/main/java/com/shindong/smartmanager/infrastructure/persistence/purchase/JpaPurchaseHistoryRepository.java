package com.shindong.smartmanager.infrastructure.persistence.purchase;

import com.shindong.smartmanager.application.purchase.PurchaseHistoryCommand;
import com.shindong.smartmanager.application.purchase.PurchaseHistoryRecord;
import com.shindong.smartmanager.application.purchase.PurchaseHistoryRepository;
import com.shindong.smartmanager.domain.purchase.PayableApprovalStatus;
import com.shindong.smartmanager.domain.purchase.PurchaseHistorySourceType;
import java.time.Instant;
import java.util.List;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaPurchaseHistoryRepository implements PurchaseHistoryRepository {

    private final SpringDataPurchaseHistoryRepository repository;

    public JpaPurchaseHistoryRepository(SpringDataPurchaseHistoryRepository repository) {
        this.repository = repository;
    }

    @Override
    @Transactional
    public long save(PurchaseHistoryCommand command) {
        PurchaseHistoryJpaEntity entity = new PurchaseHistoryJpaEntity();
        entity.setCompanyId(command.companyId());
        entity.setItemId(command.itemId());
        entity.setItemName(command.itemName());
        entity.setPurchaseQty(command.purchaseQty());
        entity.setUnitPrice(command.unitPrice());
        entity.setAmount(command.amount());
        entity.setHistoryDate(command.historyDate());
        entity.setSourceType(command.sourceType());
        entity.setSourceId(command.sourceId());
        entity.setFiscalYear((short) command.fiscalYear());
        entity.setFiscalMonth((byte) command.fiscalMonth());
        entity.setApprovalStatus(PayableApprovalStatus.PENDING);
        entity.setRecordingState(1);
        entity.setCreatedBy(command.actorUserId());
        entity.setCreatedById(command.actorUserId());
        entity.setCreatedAt(Instant.now());
        return repository.save(entity).getId();
    }

    @Override
    @Transactional(readOnly = true)
    public List<PurchaseHistoryRecord> findActiveBySource(PurchaseHistorySourceType sourceType, long sourceId) {
        return repository.findBySourceTypeAndSourceIdAndRecordingState(sourceType, sourceId, 1).stream()
                .map(entity -> new PurchaseHistoryRecord(
                        entity.getId(),
                        entity.getCompanyId(),
                        entity.getAmount(),
                        entity.getHistoryDate(),
                        entity.getFiscalYear(),
                        entity.getFiscalMonth(),
                        entity.getApprovalStatus()
                ))
                .toList();
    }

    @Override
    @Transactional
    public void deactivateBySource(PurchaseHistorySourceType sourceType, long sourceId, String actorUserId) {
        List<PurchaseHistoryJpaEntity> histories = repository.findBySourceTypeAndSourceIdAndRecordingState(
                sourceType, sourceId, 1);
        Instant now = Instant.now();
        for (PurchaseHistoryJpaEntity history : histories) {
            history.setRecordingState(0);
            repository.save(history);
        }
    }
}
