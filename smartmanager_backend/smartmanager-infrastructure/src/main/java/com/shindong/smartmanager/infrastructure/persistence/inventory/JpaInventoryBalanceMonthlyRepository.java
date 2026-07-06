package com.shindong.smartmanager.infrastructure.persistence.inventory;

import com.shindong.smartmanager.application.inventory.InventoryBalanceMonthlyRepository;
import java.math.BigDecimal;
import java.time.Instant;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaInventoryBalanceMonthlyRepository implements InventoryBalanceMonthlyRepository {

    private final SpringDataInventoryBalanceMonthlyRepository monthlyRepository;

    public JpaInventoryBalanceMonthlyRepository(SpringDataInventoryBalanceMonthlyRepository monthlyRepository) {
        this.monthlyRepository = monthlyRepository;
    }

    @Override
    @Transactional
    public void applyMovement(
            long inventoryBalanceId,
            int monthNum,
            BigDecimal inQty,
            BigDecimal inAmount,
            BigDecimal outQty,
            BigDecimal outAmount,
            BigDecimal stockQtyDelta
    ) {
        byte month = (byte) monthNum;
        InventoryBalanceMonthlyJpaEntity monthly = monthlyRepository
                .findByInventoryBalanceIdAndMonthNumAndRecordingState(inventoryBalanceId, month, 1)
                .orElseGet(() -> createMonthly(inventoryBalanceId, month));

        monthly.setInQty(monthly.getInQty().add(nullToZero(inQty)));
        monthly.setInAmount(monthly.getInAmount().add(nullToZero(inAmount)));
        monthly.setOutQty(monthly.getOutQty().add(nullToZero(outQty)));
        monthly.setOutAmount(monthly.getOutAmount().add(nullToZero(outAmount)));
        monthly.setStockQty(monthly.getStockQty().add(nullToZero(stockQtyDelta)));
        monthly.setUpdatedAt(Instant.now());
        monthlyRepository.save(monthly);
    }

    private InventoryBalanceMonthlyJpaEntity createMonthly(long inventoryBalanceId, byte monthNum) {
        Instant now = Instant.now();
        InventoryBalanceMonthlyJpaEntity entity = new InventoryBalanceMonthlyJpaEntity();
        entity.setInventoryBalanceId(inventoryBalanceId);
        entity.setMonthNum(monthNum);
        entity.setRecordingState(1);
        entity.setCreatedAt(now);
        entity.setUpdatedAt(now);
        return entity;
    }

    private static BigDecimal nullToZero(BigDecimal value) {
        return value != null ? value : BigDecimal.ZERO;
    }
}
