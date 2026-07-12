package com.shindong.smartmanager.infrastructure.persistence.inventory;

import jakarta.persistence.LockModeType;
import java.time.LocalDate;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Lock;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataLotNumberSequenceRepository extends JpaRepository<LotNumberSequenceJpaEntity, Long> {

    @Lock(LockModeType.PESSIMISTIC_WRITE)
    @Query("""
            SELECT s FROM LotNumberSequenceJpaEntity s
            WHERE s.itemId = :itemId AND s.sequenceDate = :sequenceDate
            """)
    Optional<LotNumberSequenceJpaEntity> findForUpdate(
            @Param("itemId") long itemId,
            @Param("sequenceDate") LocalDate sequenceDate
    );
}
