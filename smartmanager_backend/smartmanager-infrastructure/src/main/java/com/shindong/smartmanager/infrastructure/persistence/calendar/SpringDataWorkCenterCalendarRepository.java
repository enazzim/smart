package com.shindong.smartmanager.infrastructure.persistence.calendar;

import java.time.LocalDate;
import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface SpringDataWorkCenterCalendarRepository extends JpaRepository<WorkCenterCalendarJpaEntity, Long> {

    Optional<WorkCenterCalendarJpaEntity> findByWorkCenterIdAndCalendarDateAndRecordingState(
            Long workCenterId,
            LocalDate calendarDate,
            int recordingState
    );

    @Query("""
            SELECT w FROM WorkCenterCalendarJpaEntity w
            WHERE w.recordingState = 1
              AND w.workCenterId = :workCenterId
              AND w.calendarDate >= :startDate
              AND w.calendarDate <= :endDate
            ORDER BY w.calendarDate ASC
            """)
    List<WorkCenterCalendarJpaEntity> findActiveOverridesBetween(
            @Param("workCenterId") Long workCenterId,
            @Param("startDate") LocalDate startDate,
            @Param("endDate") LocalDate endDate
    );
}
