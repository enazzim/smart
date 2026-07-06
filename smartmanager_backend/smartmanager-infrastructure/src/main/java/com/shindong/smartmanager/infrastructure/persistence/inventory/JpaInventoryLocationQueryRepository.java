package com.shindong.smartmanager.infrastructure.persistence.inventory;

import com.shindong.smartmanager.application.inventory.InventoryLocationQueryRepository;
import java.util.Optional;
import org.springframework.stereotype.Repository;

@Repository
public class JpaInventoryLocationQueryRepository implements InventoryLocationQueryRepository {

    private final SpringDataInventoryLocationRepository locationRepository;

    public JpaInventoryLocationQueryRepository(SpringDataInventoryLocationRepository locationRepository) {
        this.locationRepository = locationRepository;
    }

    @Override
    public Optional<Long> findActiveLocationIdByCode(String locationCode) {
        return locationRepository.findByLocationCode(locationCode)
                .map(InventoryLocationJpaEntity::getId);
    }
}
