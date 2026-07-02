package com.shindong.smartmanager.infrastructure.persistence.code;

import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataCodeGroupRepository extends JpaRepository<CodeGroupJpaEntity, String> {
}
