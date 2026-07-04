package com.shindong.smartmanager.infrastructure.persistence.calendar;

import com.shindong.smartmanager.application.calendar.WorkCenterCalendarOverrideCommand;
import com.shindong.smartmanager.application.calendar.WorkCenterCalendarOverrideView;
import com.shindong.smartmanager.application.calendar.WorkCenterCalendarRepository;
import com.shindong.smartmanager.application.process.WorkCenterLookup;
import java.time.Instant;
import java.time.LocalDate;
import java.time.YearMonth;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
public class JpaWorkCenterCalendarRepository implements WorkCenterCalendarRepository {

    private final SpringDataWorkCenterCalendarRepository calendarRepository;
    private final WorkCenterLookup workCenterLookup;

    public JpaWorkCenterCalendarRepository(
            SpringDataWorkCenterCalendarRepository calendarRepository,
            WorkCenterLookup workCenterLookup
    ) {
        this.calendarRepository = calendarRepository;
        this.workCenterLookup = workCenterLookup;
    }

    @Override
    @Transactional
    public WorkCenterCalendarOverrideView upsertOverride(
            WorkCenterCalendarOverrideCommand command,
            String actorUserId
    ) {
        Instant now = Instant.now();
        WorkCenterCalendarJpaEntity entity = calendarRepository
                .findByWorkCenterIdAndCalendarDateAndRecordingState(
                        command.workCenterId(), command.calendarDate(), 1)
                .orElseGet(WorkCenterCalendarJpaEntity::new);

        if (entity.getId() == null) {
            entity.setWorkCenterId(command.workCenterId());
            entity.setCalendarDate(command.calendarDate());
            entity.setRecordingState(1);
            entity.setCreatedBy(actorUserId);
            entity.setCreatedById(actorUserId);
            entity.setCreatedAt(now);
        }

        entity.setWorkTime(command.workTime());
        entity.setContent(normalizeContent(command.content()));
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        return toView(calendarRepository.save(entity));
    }

    @Override
    @Transactional
    public void softDeleteOverride(long workCenterId, LocalDate calendarDate, String actorUserId) {
        WorkCenterCalendarJpaEntity entity = calendarRepository
                .findByWorkCenterIdAndCalendarDateAndRecordingState(workCenterId, calendarDate, 1)
                .orElseThrow(() -> new IllegalArgumentException("작업장 달력 Override를 찾을 수 없습니다."));
        Instant now = Instant.now();
        entity.setRecordingState(0);
        entity.setUpdatedBy(actorUserId);
        entity.setUpdatedById(actorUserId);
        entity.setUpdatedAt(now);
        calendarRepository.save(entity);
    }

    @Override
    public List<WorkCenterCalendarOverrideView> findActiveOverrides(long workCenterId, int year, int month) {
        YearMonth yearMonth = YearMonth.of(year, month);
        return calendarRepository.findActiveOverridesBetween(
                workCenterId, yearMonth.atDay(1), yearMonth.atEndOfMonth()).stream()
                .map(this::toView)
                .toList();
    }

    @Override
    public Optional<WorkCenterCalendarOverrideView> findActiveOverride(long workCenterId, LocalDate calendarDate) {
        return calendarRepository.findByWorkCenterIdAndCalendarDateAndRecordingState(workCenterId, calendarDate, 1)
                .map(this::toView);
    }

    private WorkCenterCalendarOverrideView toView(WorkCenterCalendarJpaEntity entity) {
        String wcName = workCenterLookup.findActiveName(entity.getWorkCenterId());
        return new WorkCenterCalendarOverrideView(
                entity.getId(),
                entity.getWorkCenterId(),
                wcName,
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
