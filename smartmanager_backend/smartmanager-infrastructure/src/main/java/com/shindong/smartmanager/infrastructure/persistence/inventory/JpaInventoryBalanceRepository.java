package com.shindong.smartmanager.infrastructure.persistence.inventory;

import com.shindong.smartmanager.application.process.InventoryBalanceRepository;
import java.time.Instant;
import java.util.List;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaInventoryBalanceRepository implements InventoryBalanceRepository {

    private static final String WIP_LOCATION_CODE = "WIP";

    private final SpringDataInventoryBalanceRepository balanceRepository;
    private final SpringDataInventoryLocationRepository locationRepository;

    public JpaInventoryBalanceRepository(
            SpringDataInventoryBalanceRepository balanceRepository,
            SpringDataInventoryLocationRepository locationRepository
    ) {
        this.balanceRepository = balanceRepository;
        this.locationRepository = locationRepository;
    }

    @Override
    @Transactional
    public void ensureWipBalance(long itemId, int fiscalYear, long outputProcessId, String actorUserId) {
        long wipLocationId = resolveWipLocationId();
        balanceRepository
                .findByItemIdAndLocationIdAndFiscalYearAndOutputProcessIdAndPartnerId(
                        itemId, wipLocationId, (short) fiscalYear, outputProcessId, null)
                .ifPresentOrElse(
                        existing -> {
                            if (existing.getRecordingState() != 1) {
                                Instant now = Instant.now();
                                existing.setRecordingState(1);
                                existing.setUpdatedBy(actorUserId);
                                existing.setUpdatedById(actorUserId);
                                existing.setUpdatedAt(now);
                                balanceRepository.save(existing);
                            }
                        },
                        () -> {
                            Instant now = Instant.now();
                            InventoryBalanceJpaEntity entity = new InventoryBalanceJpaEntity();
                            entity.setItemId(itemId);
                            entity.setLocationId(wipLocationId);
                            entity.setFiscalYear((short) fiscalYear);
                            entity.setOutputProcessId(outputProcessId);
                            entity.setPartnerId(null);
                            entity.setRecordingState(1);
                            entity.setCreatedBy(actorUserId);
                            entity.setCreatedById(actorUserId);
                            entity.setCreatedAt(now);
                            entity.setUpdatedBy(actorUserId);
                            entity.setUpdatedById(actorUserId);
                            entity.setUpdatedAt(now);
                            balanceRepository.save(entity);
                        }
                );
    }

    @Override
    @Transactional
    public void updateWipItemId(long outputProcessId, long itemId, String actorUserId) {
        Instant now = Instant.now();
        List<InventoryBalanceJpaEntity> balances =
                balanceRepository.findByOutputProcessIdAndRecordingState(outputProcessId, 1);
        for (InventoryBalanceJpaEntity balance : balances) {
            balance.setItemId(itemId);
            balance.setUpdatedBy(actorUserId);
            balance.setUpdatedById(actorUserId);
            balance.setUpdatedAt(now);
        }
        balanceRepository.saveAll(balances);
    }

    @Override
    @Transactional
    public void deactivateWipBalance(long outputProcessId, String actorUserId) {
        Instant now = Instant.now();
        List<InventoryBalanceJpaEntity> balances =
                balanceRepository.findByOutputProcessIdAndRecordingState(outputProcessId, 1);
        for (InventoryBalanceJpaEntity balance : balances) {
            balance.setRecordingState(0);
            balance.setUpdatedBy(actorUserId);
            balance.setUpdatedById(actorUserId);
            balance.setUpdatedAt(now);
        }
        balanceRepository.saveAll(balances);
    }

    private long resolveWipLocationId() {
        return locationRepository.findByLocationCode(WIP_LOCATION_CODE)
                .map(InventoryLocationJpaEntity::getId)
                .orElseThrow(() -> new IllegalStateException("WIP 창고 위치가 시드되지 않았습니다."));
    }
}
