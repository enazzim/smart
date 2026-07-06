package com.shindong.smartmanager.infrastructure.persistence.inventory;

import com.shindong.smartmanager.application.inventory.InventoryBalanceKey;
import com.shindong.smartmanager.application.inventory.InventoryBalanceSlotView;
import com.shindong.smartmanager.application.inventory.InventoryStockBalanceRepository;
import java.math.BigDecimal;
import java.time.Instant;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaInventoryStockBalanceRepository implements InventoryStockBalanceRepository {

    private final SpringDataInventoryBalanceRepository balanceRepository;

    public JpaInventoryStockBalanceRepository(SpringDataInventoryBalanceRepository balanceRepository) {
        this.balanceRepository = balanceRepository;
    }

    @Override
    @Transactional
    public InventoryBalanceSlotView ensureBalance(InventoryBalanceKey key) {
        return balanceRepository
                .findActiveSlot(
                        key.itemId(),
                        key.locationId(),
                        (short) key.fiscalYear(),
                        key.outputProcessId(),
                        key.inputProcessId(),
                        key.partnerId()
                )
                .map(this::toView)
                .orElseGet(() -> createBalance(key));
    }

    @Override
    @Transactional
    public InventoryBalanceSlotView saveBalance(InventoryBalanceSlotView balance) {
        InventoryBalanceJpaEntity entity = balanceRepository.findById(balance.id())
                .orElseThrow(() -> new IllegalStateException("재고 잔고를 찾을 수 없습니다: id=" + balance.id()));
        entity.setStockQty(balance.stockQty());
        entity.setStockAmount(balance.stockAmount());
        entity.setUpdatedAt(Instant.now());
        return toView(balanceRepository.save(entity));
    }

    private InventoryBalanceSlotView createBalance(InventoryBalanceKey key) {
        Instant now = Instant.now();
        InventoryBalanceJpaEntity entity = new InventoryBalanceJpaEntity();
        entity.setItemId(key.itemId());
        entity.setLocationId(key.locationId());
        entity.setFiscalYear((short) key.fiscalYear());
        entity.setOutputProcessId(key.outputProcessId());
        entity.setInputProcessId(key.inputProcessId());
        entity.setPartnerId(key.partnerId());
        entity.setStockQty(BigDecimal.ZERO);
        entity.setStockAmount(BigDecimal.ZERO);
        entity.setRecordingState(1);
        entity.setCreatedAt(now);
        entity.setUpdatedAt(now);
        return toView(balanceRepository.save(entity));
    }

    private InventoryBalanceSlotView toView(InventoryBalanceJpaEntity entity) {
        return new InventoryBalanceSlotView(
                entity.getId(),
                entity.getItemId(),
                entity.getLocationId(),
                entity.getFiscalYear(),
                entity.getOutputProcessId(),
                entity.getInputProcessId(),
                entity.getPartnerId(),
                entity.getStockQty() != null ? entity.getStockQty() : BigDecimal.ZERO,
                entity.getStockAmount() != null ? entity.getStockAmount() : BigDecimal.ZERO
        );
    }
}
