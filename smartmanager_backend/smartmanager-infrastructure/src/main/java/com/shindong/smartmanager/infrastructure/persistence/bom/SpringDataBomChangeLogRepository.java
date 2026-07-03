package com.shindong.smartmanager.infrastructure.persistence.bom;

import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataBomChangeLogRepository extends JpaRepository<BomChangeLogJpaEntity, Long> {
}
