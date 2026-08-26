package com.shindong.smartmanager.infrastructure.persistence.closing;

import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;

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
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaMonthClosingRepository(
            SpringDataMonthClosingRepository springDataMonthClosingRepository,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.springDataMonthClosingRepository = springDataMonthClosingRepository;
        this.masterAuditActorLookup = masterAuditActorLookup;
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
        return springDataMonthClosingRepository
                .findByFiscalYearAndFiscalMonthAndRecordingState(fiscalYear, fiscalMonth, ACTIVE)
                .map(this::toView)
                .orElseGet(() -> {
                    MonthClosingJpaEntity entity = MonthClosingJpaEntity.create(
                            fiscalYear,
                            fiscalMonth,
                            masterAuditActorLookup.idOf(closedById),
                            Instant.now()
                    );
                    return toView(springDataMonthClosingRepository.save(entity));
                });
    }

    @Override
    public Optional<MonthClosingView> findLatestClosed() {
        return springDataMonthClosingRepository
                .findFirstByRecordingStateOrderByFiscalYearDescFiscalMonthDesc(ACTIVE)
                .map(this::toView);
    }

    @Override
    public void reopen(int fiscalYear, int fiscalMonth) {
        List<MonthClosingJpaEntity> rows = springDataMonthClosingRepository
                .findByFiscalYearAndFiscalMonth(fiscalYear, fiscalMonth);
        boolean hasActive = rows.stream().anyMatch(entity -> entity.getRecordingState() == ACTIVE);
        if (!hasActive) {
            throw new IllegalArgumentException("마감 정보를 찾을 수 없습니다.");
        }
        springDataMonthClosingRepository.deleteAll(rows);
    }

    private MonthClosingView toView(MonthClosingJpaEntity entity) {
        return new MonthClosingView(
                entity.getId(),
                entity.getFiscalYear(),
                entity.getFiscalMonth(),
                entity.getClosedAt(),
                masterAuditActorLookup.nameOf(entity.getClosedById()),
                masterAuditActorLookup.loginIdOf(entity.getClosedById())
        );
    }
}
