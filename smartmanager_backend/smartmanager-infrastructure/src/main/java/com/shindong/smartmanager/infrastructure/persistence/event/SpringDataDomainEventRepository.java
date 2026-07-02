package com.shindong.smartmanager.infrastructure.persistence.event;

import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataDomainEventRepository extends JpaRepository<DomainEventJpaEntity, String> {
}
