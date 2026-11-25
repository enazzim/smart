package com.shindong.smartmanager.infrastructure.persistence.inventory;

import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataStocktakeLineRepository extends JpaRepository<StocktakeLineJpaEntity, Long> {
}
