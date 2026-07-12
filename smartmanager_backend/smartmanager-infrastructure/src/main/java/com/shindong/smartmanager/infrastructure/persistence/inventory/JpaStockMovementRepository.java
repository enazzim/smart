package com.shindong.smartmanager.infrastructure.persistence.inventory;

import com.shindong.smartmanager.application.inventory.StockMovementRepository;
import com.shindong.smartmanager.application.inventory.StockMovementView;
import java.math.BigDecimal;
import java.time.Instant;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaStockMovementRepository implements StockMovementRepository {

    private final SpringDataStockMovementRepository movementRepository;

    public JpaStockMovementRepository(SpringDataStockMovementRepository movementRepository) {
        this.movementRepository = movementRepository;
    }

    @Override
    @Transactional
    public StockMovementView save(StockMovementView movement) {
        StockMovementJpaEntity entity = new StockMovementJpaEntity();
        entity.setInventoryBalanceId(movement.inventoryBalanceId());
        entity.setItemId(movement.itemId());
        entity.setLocationId(movement.locationId());
        entity.setFiscalYear((short) movement.fiscalYear());
        entity.setFiscalMonth((byte) movement.fiscalMonth());
        entity.setMovementType(movement.movementType());
        entity.setQty(movement.qty());
        entity.setAmount(movement.amount() != null ? movement.amount() : BigDecimal.ZERO);
        entity.setReferenceType(movement.referenceType());
        entity.setReferenceId(movement.referenceId());
        entity.setMovementDate(movement.movementDate());
        entity.setLotId(movement.lotId());
        entity.setRecordingState(1);
        entity.setCreatedAt(Instant.now());
        return toView(movementRepository.save(entity));
    }

    @Override
    @Transactional(readOnly = true)
    public java.util.Optional<Long> findActiveLotIdByReference(String referenceType, long referenceId) {
        return movementRepository.findFirstByReferenceTypeAndReferenceIdAndRecordingStateOrderByIdDesc(
                        referenceType, referenceId, 1)
                .map(StockMovementJpaEntity::getLotId)
                .filter(id -> id != null);
    }

    private StockMovementView toView(StockMovementJpaEntity entity) {
        return new StockMovementView(
                entity.getId(),
                entity.getInventoryBalanceId(),
                entity.getItemId(),
                entity.getLocationId(),
                entity.getFiscalYear(),
                entity.getFiscalMonth(),
                entity.getMovementType(),
                entity.getQty(),
                entity.getAmount(),
                entity.getReferenceType(),
                entity.getReferenceId(),
                entity.getMovementDate(),
                entity.getLotId()
        );
    }
}
