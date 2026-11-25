package com.shindong.smartmanager.infrastructure.persistence.inventory;

import jakarta.persistence.LockModeType;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Lock;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataMiscMovementNoSequenceRepository extends JpaRepository<MiscMovementNoSequenceJpaEntity, String> {

    @Lock(LockModeType.PESSIMISTIC_WRITE)
    @Query("SELECT s FROM MiscMovementNoSequenceJpaEntity s WHERE s.prefix = :prefix")
    Optional<MiscMovementNoSequenceJpaEntity> findForUpdate(@Param("prefix") String prefix);
}
