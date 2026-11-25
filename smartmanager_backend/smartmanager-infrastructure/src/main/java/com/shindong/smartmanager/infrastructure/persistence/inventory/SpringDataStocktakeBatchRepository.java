package com.shindong.smartmanager.infrastructure.persistence.inventory;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataStocktakeBatchRepository extends JpaRepository<StocktakeBatchJpaEntity, Long> {

    @Query(value = """
            SELECT COALESCE(MAX(CAST(SUBSTRING(batch_no, CHAR_LENGTH(:prefix) + 1) AS UNSIGNED)), 0)
            FROM stocktake_batch
            WHERE batch_no LIKE CONCAT(:prefix, '%')
            """, nativeQuery = true)
    int maxBatchSuffix(@Param("prefix") String prefix);
}
