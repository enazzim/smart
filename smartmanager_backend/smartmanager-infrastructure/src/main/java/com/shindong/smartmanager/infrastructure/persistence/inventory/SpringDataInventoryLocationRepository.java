package com.shindong.smartmanager.infrastructure.persistence.inventory;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataInventoryLocationRepository extends JpaRepository<InventoryLocationJpaEntity, Long> {

    Optional<InventoryLocationJpaEntity> findByLocationCode(String locationCode);
}
