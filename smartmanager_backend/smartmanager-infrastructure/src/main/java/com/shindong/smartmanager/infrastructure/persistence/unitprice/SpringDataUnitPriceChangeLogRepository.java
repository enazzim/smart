package com.shindong.smartmanager.infrastructure.persistence.unitprice;

import com.shindong.smartmanager.domain.pricing.CostType;
import java.time.Instant;
import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataUnitPriceChangeLogRepository extends JpaRepository<UnitPriceChangeLogJpaEntity, Long> {

    List<UnitPriceChangeLogJpaEntity> findByUnitPriceIdOrderByChangedAtDesc(long unitPriceId);

    @Query("""
            SELECT l FROM UnitPriceChangeLogJpaEntity l
            WHERE (:costType IS NULL OR l.costType = :costType)
              AND (:companyId IS NULL OR l.companyId = :companyId)
              AND (:itemId IS NULL OR l.itemId = :itemId)
              AND (:changedFromInstant IS NULL OR l.changedAt >= :changedFromInstant)
              AND (:changedToExclusiveInstant IS NULL OR l.changedAt < :changedToExclusiveInstant)
              AND (
                :changedBy IS NULL OR :changedBy = '' OR
                EXISTS (
                  SELECT 1 FROM UserJpaEntity u
                  WHERE u.id = l.changedById
                    AND (
                      LOWER(u.name) LIKE LOWER(CONCAT('%', :changedBy, '%'))
                      OR LOWER(u.loginId) LIKE LOWER(CONCAT('%', :changedBy, '%'))
                    )
                )
              )
            ORDER BY l.changedAt DESC, l.id DESC
            """)
    List<UnitPriceChangeLogJpaEntity> search(
            @Param("costType") CostType costType,
            @Param("companyId") Long companyId,
            @Param("itemId") Long itemId,
            @Param("changedFromInstant") Instant changedFromInstant,
            @Param("changedToExclusiveInstant") Instant changedToExclusiveInstant,
            @Param("changedBy") String changedBy
    );
}
