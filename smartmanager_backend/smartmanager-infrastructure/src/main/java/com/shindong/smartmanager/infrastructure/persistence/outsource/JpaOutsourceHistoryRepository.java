package com.shindong.smartmanager.infrastructure.persistence.outsource;

import com.shindong.smartmanager.application.outsource.OutsourceHistoryCommand;
import com.shindong.smartmanager.application.outsource.OutsourceHistoryRecord;
import com.shindong.smartmanager.application.outsource.OutsourceHistoryRepository;
import com.shindong.smartmanager.domain.outsource.OutsourceHistorySourceType;
import com.shindong.smartmanager.domain.purchase.PayableApprovalStatus;
import java.time.Instant;
import java.util.List;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaOutsourceHistoryRepository implements OutsourceHistoryRepository {

    private final SpringDataOutsourceHistoryRepository repository;

    public JpaOutsourceHistoryRepository(SpringDataOutsourceHistoryRepository repository) {
        this.repository = repository;
    }

    @Override
    @Transactional
    public long save(OutsourceHistoryCommand command) {
        OutsourceHistoryJpaEntity entity = new OutsourceHistoryJpaEntity();
        entity.setCompanyId(command.companyId());
        entity.setItemId(command.itemId());
        entity.setOutsourceQty(command.outsourceQty());
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
    public List<OutsourceHistoryRecord> findActiveBySource(OutsourceHistorySourceType sourceType, long sourceId) {
        return repository.findBySourceTypeAndSourceIdAndRecordingState(sourceType, sourceId, 1).stream()
                .map(entity -> new OutsourceHistoryRecord(
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
    public void deactivateBySource(OutsourceHistorySourceType sourceType, long sourceId, String actorUserId) {
        List<OutsourceHistoryJpaEntity> histories = repository.findBySourceTypeAndSourceIdAndRecordingState(
                sourceType, sourceId, 1);
        for (OutsourceHistoryJpaEntity history : histories) {
            history.setRecordingState(0);
            repository.save(history);
        }
    }
}
