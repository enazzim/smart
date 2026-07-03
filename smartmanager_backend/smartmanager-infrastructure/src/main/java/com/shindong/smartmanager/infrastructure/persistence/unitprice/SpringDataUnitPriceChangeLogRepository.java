package com.shindong.smartmanager.infrastructure.persistence.unitprice;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataUnitPriceChangeLogRepository extends JpaRepository<UnitPriceChangeLogJpaEntity, Long> {

    List<UnitPriceChangeLogJpaEntity> findByUnitPriceIdOrderByChangedAtDesc(long unitPriceId);
}
