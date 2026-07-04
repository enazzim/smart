package com.shindong.smartmanager.infrastructure.persistence.closing;

import com.shindong.smartmanager.application.closing.MonthClosingRepository;
import com.shindong.smartmanager.application.closing.MonthClosingView;
import java.time.Instant;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;

@Repository
public class JpaMonthClosingRepository implements MonthClosingRepository {

    private static final int ACTIVE = 1;

    private final SpringDataMonthClosingRepository springDataMonthClosingRepository;

    public JpaMonthClosingRepository(SpringDataMonthClosingRepository springDataMonthClosingRepository) {
        this.springDataMonthClosingRepository = springDataMonthClosingRepository;
    }

    @Override
    public List<MonthClosingView> findAllClosed() {
        return springDataMonthClosingRepository.findByRecordingStateOrderByFiscalYearDescFiscalMonthDesc(ACTIVE)
                .stream()
                .map(this::toView)
                .toList();
    }

    @Override
    public Optional<MonthClosingView> findClosed(int fiscalYear, int fiscalMonth) {
        return springDataMonthClosingRepository
                .findByFiscalYearAndFiscalMonthAndRecordingState(fiscalYear, fiscalMonth, ACTIVE)
                .map(this::toView);
    }

    @Override
    public boolean isClosed(int fiscalYear, int fiscalMonth) {
        return springDataMonthClosingRepository
                .findByFiscalYearAndFiscalMonthAndRecordingState(fiscalYear, fiscalMonth, ACTIVE)
                .isPresent();
    }

    @Override
    public boolean hasAnyClosed() {
        return springDataMonthClosingRepository.existsByRecordingState(ACTIVE);
    }

    @Override
    public MonthClosingView saveClose(int fiscalYear, int fiscalMonth, String closedBy, String closedById) {
        MonthClosingJpaEntity entity = MonthClosingJpaEntity.create(
                fiscalYear,
                fiscalMonth,
                closedBy,
                closedById,
                Instant.now()
        );
        return toView(springDataMonthClosingRepository.save(entity));
    }

    @Override
    public Optional<MonthClosingView> findLatestClosed() {
        return springDataMonthClosingRepository
                .findFirstByRecordingStateOrderByFiscalYearDescFiscalMonthDesc(ACTIVE)
                .map(this::toView);
    }

    @Override
    public void reopen(int fiscalYear, int fiscalMonth) {
        MonthClosingJpaEntity entity = springDataMonthClosingRepository
                .findByFiscalYearAndFiscalMonthAndRecordingState(fiscalYear, fiscalMonth, ACTIVE)
                .orElseThrow(() -> new IllegalArgumentException("마감 정보를 찾을 수 없습니다."));
        entity.deactivate();
        springDataMonthClosingRepository.save(entity);
    }

    private MonthClosingView toView(MonthClosingJpaEntity entity) {
        return new MonthClosingView(
                entity.getId(),
                entity.getFiscalYear(),
                entity.getFiscalMonth(),
                entity.getClosedAt(),
                entity.getClosedBy(),
                entity.getClosedById()
        );
    }
}
