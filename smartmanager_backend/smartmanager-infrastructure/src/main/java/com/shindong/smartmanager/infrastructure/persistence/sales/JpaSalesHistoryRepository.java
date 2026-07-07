package com.shindong.smartmanager.infrastructure.persistence.sales;

import com.shindong.smartmanager.application.sales.SalesHistoryCommand;
import com.shindong.smartmanager.application.sales.SalesHistoryRepository;
import com.shindong.smartmanager.domain.sales.SalesHistorySourceType;
import java.time.Instant;
import java.util.List;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaSalesHistoryRepository implements SalesHistoryRepository {

    private static final int ACTIVE = 1;
    private static final int INACTIVE = 0;

    private final SpringDataSalesHistoryRepository repository;

    public JpaSalesHistoryRepository(SpringDataSalesHistoryRepository repository) {
        this.repository = repository;
    }

    @Override
    @Transactional
    public long save(SalesHistoryCommand command) {
        Instant now = Instant.now();
        SalesHistoryJpaEntity entity = new SalesHistoryJpaEntity();
        entity.setCompanyId(command.companyId());
        entity.setItemId(command.itemId());
        entity.setSalesQty(command.salesQty());
        entity.setUnitPrice(command.unitPrice());
        entity.setAmount(command.amount());
        entity.setHistoryDate(command.historyDate());
        entity.setSourceType(command.sourceType());
        entity.setSourceId(command.sourceId());
        entity.setFiscalYear((short) command.fiscalYear());
        entity.setFiscalMonth((byte) command.fiscalMonth());
        entity.setRecordingState(ACTIVE);
        entity.setCreatedBy(command.actorUserId());
        entity.setCreatedById(command.actorUserId());
        entity.setCreatedAt(now);
        return repository.save(entity).getId();
    }

    @Override
    @Transactional
    public void deactivateBySource(SalesHistorySourceType sourceType, long sourceId, String actorUserId) {
        List<SalesHistoryJpaEntity> rows = repository.findBySourceTypeAndSourceIdAndRecordingState(
                sourceType, sourceId, ACTIVE);
        for (SalesHistoryJpaEntity row : rows) {
            row.setRecordingState(INACTIVE);
            repository.save(row);
        }
    }
}
