package com.shindong.smartmanager.infrastructure.persistence.unitprice;

import com.shindong.smartmanager.domain.pricing.CostType;
import jakarta.persistence.LockModeType;
import java.time.LocalDate;
import java.util.Collection;
import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Lock;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataUnitPriceRepository extends JpaRepository<UnitPriceJpaEntity, Long> {

    Optional<UnitPriceJpaEntity> findByIdAndRecordingState(Long id, int recordingState);

    List<UnitPriceJpaEntity> findByCostTypeAndRecordingStateOrderByBeginDateDescIdDesc(
            CostType costType,
            int recordingState
    );

    @Query("""
            SELECT u FROM UnitPriceJpaEntity u
            JOIN ItemJpaEntity i ON i.id = u.itemId
            JOIN CompanyJpaEntity c ON c.id = u.companyId
            WHERE u.costType = :costType
              AND u.recordingState = 1
              AND (
                :query IS NULL OR :query = '' OR
                LOWER(i.itemNo) LIKE LOWER(CONCAT('%', :query, '%')) OR
                LOWER(i.itemName) LIKE LOWER(CONCAT('%', :query, '%')) OR
                LOWER(c.companyName) LIKE LOWER(CONCAT('%', :query, '%')) OR
                LOWER(c.businessRegNo) LIKE LOWER(CONCAT('%', :query, '%'))
              )
            ORDER BY u.beginDate DESC, u.id DESC
            """)
    List<UnitPriceJpaEntity> searchActive(
            @Param("costType") CostType costType,
            @Param("query") String query
    );

    @Query("""
            SELECT COUNT(u) > 0 FROM UnitPriceJpaEntity u
            WHERE u.costType = :costType
              AND u.itemId = :itemId
              AND u.companyId = :companyId
              AND u.beginDate = :beginDate
              AND ((:beginProcessCodeId IS NULL AND u.beginProcessCodeId IS NULL)
                   OR u.beginProcessCodeId = :beginProcessCodeId)
              AND ((:endProcessCodeId IS NULL AND u.endProcessCodeId IS NULL)
                   OR u.endProcessCodeId = :endProcessCodeId)
              AND u.recordingState = 1
              AND (:excludeId IS NULL OR u.id <> :excludeId)
            """)
    boolean existsActiveUk(
            @Param("costType") CostType costType,
            @Param("itemId") long itemId,
            @Param("companyId") long companyId,
            @Param("beginDate") LocalDate beginDate,
            @Param("beginProcessCodeId") Long beginProcessCodeId,
            @Param("endProcessCodeId") Long endProcessCodeId,
            @Param("excludeId") Long excludeId
    );

    @Lock(LockModeType.PESSIMISTIC_WRITE)
    @Query("""
            SELECT u FROM UnitPriceJpaEntity u
            WHERE u.costType = :costType
              AND u.itemId = :itemId
              AND u.recordingState = 1
            """)
    List<UnitPriceJpaEntity> lockActiveByCostTypeAndItemId(
            @Param("costType") CostType costType,
            @Param("itemId") long itemId
    );

    @Query("""
            SELECT COALESCE(SUM(u.orderRate), 0) FROM UnitPriceJpaEntity u
            WHERE u.costType = :costType
              AND u.itemId = :itemId
              AND u.recordingState = 1
              AND (:excludeId IS NULL OR u.id <> :excludeId)
            """)
    java.math.BigDecimal sumActiveOrderRate(
            @Param("costType") CostType costType,
            @Param("itemId") long itemId,
            @Param("excludeId") Long excludeId
    );

    @Query("""
            SELECT COALESCE(SUM(u.orderRate), 0) FROM UnitPriceJpaEntity u
            WHERE u.costType = com.shindong.smartmanager.domain.pricing.CostType.OUTSOURCE
              AND u.itemId = :itemId
              AND u.beginProcessCodeId = :beginProcessCodeId
              AND u.endProcessCodeId = :endProcessCodeId
              AND u.recordingState = 1
              AND (:excludeId IS NULL OR u.id <> :excludeId)
            """)
    java.math.BigDecimal sumActiveOrderRateForOutsourceSegment(
            @Param("itemId") long itemId,
            @Param("beginProcessCodeId") long beginProcessCodeId,
            @Param("endProcessCodeId") long endProcessCodeId,
            @Param("excludeId") Long excludeId
    );

    @Query("""
            SELECT u FROM UnitPriceJpaEntity u
            JOIN ItemJpaEntity i ON i.id = u.itemId
            WHERE i.itemNo = :itemNo
              AND u.costType = :costType
              AND u.recordingState = 1
            ORDER BY u.beginDate DESC, u.id DESC
            """)
    List<UnitPriceJpaEntity> findActiveByItemNoAndCostType(
            @Param("itemNo") String itemNo,
            @Param("costType") CostType costType
    );

    @Query("""
            SELECT u FROM UnitPriceJpaEntity u
            WHERE u.costType = :costType
              AND u.itemId IN :itemIds
              AND u.recordingState = 1
            ORDER BY u.itemId ASC, u.beginDate DESC, u.id DESC
            """)
    List<UnitPriceJpaEntity> findActiveByCostTypeAndItemIds(
            @Param("costType") CostType costType,
            @Param("itemIds") Collection<Long> itemIds
    );

    boolean existsByBeginProcessCodeIdAndRecordingState(Long beginProcessCodeId, int recordingState);

    boolean existsByEndProcessCodeIdAndRecordingState(Long endProcessCodeId, int recordingState);
}
