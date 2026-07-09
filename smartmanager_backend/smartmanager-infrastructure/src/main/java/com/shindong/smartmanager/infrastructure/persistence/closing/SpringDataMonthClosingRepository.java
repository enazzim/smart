package com.shindong.smartmanager.infrastructure.persistence.closing;

import java.util.List;
import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SpringDataMonthClosingRepository extends JpaRepository<MonthClosingJpaEntity, Long> {

    List<MonthClosingJpaEntity> findByRecordingStateOrderByFiscalYearDescFiscalMonthDesc(int recordingState);

    Optional<MonthClosingJpaEntity> findByFiscalYearAndFiscalMonthAndRecordingState(
            int fiscalYear,
            int fiscalMonth,
            int recordingState
    );

    boolean existsByRecordingState(int recordingState);

    Optional<MonthClosingJpaEntity> findFirstByRecordingStateOrderByFiscalYearDescFiscalMonthDesc(int recordingState);

    List<MonthClosingJpaEntity> findByFiscalYearAndFiscalMonth(int fiscalYear, int fiscalMonth);
}
