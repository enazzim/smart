package com.shindong.smartmanager.infrastructure.persistence.calendar;

import com.shindong.smartmanager.application.calendar.ProductionCalendarRepository;
import com.shindong.smartmanager.application.calendar.ProductionCalendarUpsertCommand;
import com.shindong.smartmanager.application.calendar.ProductionCalendarView;
import com.shindong.smartmanager.infrastructure.persistence.support.MasterAuditActorLookup;
import java.time.Instant;
import java.time.LocalDate;
import java.time.YearMonth;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaProductionCalendarRepository implements ProductionCalendarRepository {

    private final SpringDataProductionCalendarRepository calendarRepository;
    private final MasterAuditActorLookup masterAuditActorLookup;

    public JpaProductionCalendarRepository(
            SpringDataProductionCalendarRepository calendarRepository,
            MasterAuditActorLookup masterAuditActorLookup
    ) {
        this.calendarRepository = calendarRepository;
        this.masterAuditActorLookup = masterAuditActorLookup;
    }

    @Override
    @Transactional
    public ProductionCalendarView upsertByDate(
            LocalDate calendarDate,
            ProductionCalendarUpsertCommand command,
            String actorUserId
    ) {
        Instant now = Instant.now();
        Optional<ProductionCalendarJpaEntity> active = calendarRepository
                .findByCalendarDateAndRecordingState(calendarDate, 1);
        Optional<ProductionCalendarJpaEntity> inactive = calendarRepository
                .findByCalendarDateAndRecordingState(calendarDate, 0);

        if (active.isPresent() && inactive.isPresent()) {
            calendarRepository.delete(inactive.get());
            calendarRepository.flush();
        }

        ProductionCalendarJpaEntity entity = active
                .or(() -> inactive)
                .orElseGet(ProductionCalendarJpaEntity::new);

        if (entity.getId() == null) {
            entity.setCalendarDate(calendarDate);
            entity.setCreatedById(masterAuditActorLookup.idOf(actorUserId));
            entity.setCreatedAt(now);
        }

        entity.setRecordingState(1);
        entity.setWorkTime(command.workTime());
        entity.setContent(normalizeContent(command.content()));
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        return toView(calendarRepository.save(entity));
    }

    @Override
    @Transactional
    public void softDeleteByDate(LocalDate calendarDate, String actorUserId) {
        ProductionCalendarJpaEntity entity = calendarRepository
                .findByCalendarDateAndRecordingState(calendarDate, 1)
                .orElseThrow(() -> new IllegalArgumentException("기본생산달력을 찾을 수 없습니다: " + calendarDate));
        calendarRepository.findByCalendarDateAndRecordingState(calendarDate, 0)
                .ifPresent(inactive -> {
                    calendarRepository.delete(inactive);
                    calendarRepository.flush();
                });
        Instant now = Instant.now();
        entity.setRecordingState(0);
        entity.setUpdatedById(masterAuditActorLookup.idOf(actorUserId));
        entity.setUpdatedAt(now);
        calendarRepository.save(entity);
    }

    @Override
    public List<ProductionCalendarView> findActiveByYearMonth(int year, int month) {
        YearMonth yearMonth = YearMonth.of(year, month);
        return calendarRepository.findActiveBetween(yearMonth.atDay(1), yearMonth.atEndOfMonth()).stream()
                .map(this::toView)
                .toList();
    }

    @Override
    public Optional<ProductionCalendarView> findActiveById(long id) {
        return calendarRepository.findByIdAndRecordingState(id, 1).map(this::toView);
    }

    @Override
    public Optional<ProductionCalendarView> findActiveByDate(LocalDate calendarDate) {
        return calendarRepository.findByCalendarDateAndRecordingState(calendarDate, 1).map(this::toView);
    }

    private ProductionCalendarView toView(ProductionCalendarJpaEntity entity) {
        return new ProductionCalendarView(
                entity.getId(),
                entity.getCalendarDate(),
                entity.getWorkTime(),
                entity.getContent()
        );
    }

    private static String normalizeContent(String content) {
        if (content == null || content.isBlank()) {
            return null;
        }
        return content.trim();
    }
}
