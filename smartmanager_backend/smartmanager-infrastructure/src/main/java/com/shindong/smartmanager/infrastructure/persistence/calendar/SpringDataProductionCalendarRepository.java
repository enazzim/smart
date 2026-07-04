package com.shindong.smartmanager.infrastructure.persistence.calendar;

import java.time.LocalDate;
import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataProductionCalendarRepository extends JpaRepository<ProductionCalendarJpaEntity, Long> {

    Optional<ProductionCalendarJpaEntity> findByIdAndRecordingState(Long id, int recordingState);

    Optional<ProductionCalendarJpaEntity> findByCalendarDateAndRecordingState(LocalDate calendarDate, int recordingState);

    @Query("""
            SELECT p FROM ProductionCalendarJpaEntity p
            WHERE p.recordingState = 1
              AND p.calendarDate >= :startDate
              AND p.calendarDate <= :endDate
            ORDER BY p.calendarDate ASC
            """)
    List<ProductionCalendarJpaEntity> findActiveBetween(
            @Param("startDate") LocalDate startDate,
            @Param("endDate") LocalDate endDate
    );
}
