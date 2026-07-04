package com.shindong.smartmanager.application.calendar;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.application.process.WorkCenterLookup;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import java.time.LocalDate;
import java.time.YearMonth;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

public class WorkCenterCalendarService {

    private final WorkCenterCalendarRepository workCenterCalendarRepository;
    private final ProductionCalendarRepository productionCalendarRepository;
    private final WorkCenterLookup workCenterLookup;
    private final DomainEventStore domainEventStore;

    public WorkCenterCalendarService(
            WorkCenterCalendarRepository workCenterCalendarRepository,
            ProductionCalendarRepository productionCalendarRepository,
            WorkCenterLookup workCenterLookup,
            DomainEventStore domainEventStore
    ) {
        this.workCenterCalendarRepository = workCenterCalendarRepository;
        this.productionCalendarRepository = productionCalendarRepository;
        this.workCenterLookup = workCenterLookup;
        this.domainEventStore = domainEventStore;
    }

    public EffectiveCalendarDayView upsertOverride(
            WorkCenterCalendarOverrideCommand command,
            String actorUserId
    ) {
        validateWorkTime(command.workTime());
        requireWorkCenter(command.workCenterId());

        int baseEffective = resolveBaseEffective(command.workCenterId(), command.calendarDate());
        if (command.workTime() == baseEffective && isBlank(command.content())) {
            workCenterCalendarRepository.findActiveOverride(command.workCenterId(), command.calendarDate())
                    .ifPresent(existing -> {
                        workCenterCalendarRepository.softDeleteOverride(
                                command.workCenterId(),
                                command.calendarDate(),
                                actorUserId
                        );
                        domainEventStore.append(DomainEvent.create(
                                EventTypes.WORK_CENTER_CALENDAR_DAY_DELETED,
                                1,
                                AggregateTypes.WORK_CENTER_CALENDAR,
                                String.valueOf(existing.id()),
                                actorUserId,
                                """
                                {"overrideId":%d,"workCenterId":%d,"calendarDate":"%s"}
                                """.formatted(existing.id(), command.workCenterId(), command.calendarDate()).trim()
                        ));
                    });
            return buildEffectiveDay(command.workCenterId(), command.calendarDate());
        }

        boolean existed = workCenterCalendarRepository
                .findActiveOverride(command.workCenterId(), command.calendarDate())
                .isPresent();
        WorkCenterCalendarOverrideView view =
                workCenterCalendarRepository.upsertOverride(command, actorUserId);
        appendEvent(
                existed ? EventTypes.WORK_CENTER_CALENDAR_DAY_UPDATED : EventTypes.WORK_CENTER_CALENDAR_DAY_REGISTERED,
                view.id(),
                actorUserId,
                command.workCenterId(),
                command.calendarDate()
        );
        return buildEffectiveDay(command.workCenterId(), command.calendarDate());
    }

    public void deleteOverride(long workCenterId, LocalDate calendarDate, String actorUserId) {
        requireWorkCenter(workCenterId);
        WorkCenterCalendarOverrideView existing = workCenterCalendarRepository
                .findActiveOverride(workCenterId, calendarDate)
                .orElseThrow(() -> new IllegalArgumentException("작업장 달력 Override를 찾을 수 없습니다."));
        workCenterCalendarRepository.softDeleteOverride(workCenterId, calendarDate, actorUserId);
        domainEventStore.append(DomainEvent.create(
                EventTypes.WORK_CENTER_CALENDAR_DAY_DELETED,
                1,
                AggregateTypes.WORK_CENTER_CALENDAR,
                String.valueOf(existing.id()),
                actorUserId,
                """
                {"overrideId":%d,"workCenterId":%d,"calendarDate":"%s"}
                """.formatted(existing.id(), workCenterId, calendarDate).trim()
        ));
    }

    public List<WorkCenterCalendarOverrideView> listOverrides(long workCenterId, int year, int month) {
        validateYearMonth(year, month);
        requireWorkCenter(workCenterId);
        return workCenterCalendarRepository.findActiveOverrides(workCenterId, year, month);
    }

    public List<EffectiveCalendarDayView> listEffective(long workCenterId, int year, int month) {
        validateYearMonth(year, month);
        requireWorkCenter(workCenterId);
        int operationTime = workCenterLookup.findActiveOperationTime(workCenterId);

        Map<LocalDate, ProductionCalendarView> baseByDate =
                productionCalendarRepository.findActiveByYearMonth(year, month).stream()
                        .collect(Collectors.toMap(ProductionCalendarView::calendarDate, v -> v));

        Map<LocalDate, WorkCenterCalendarOverrideView> overrideByDate =
                workCenterCalendarRepository.findActiveOverrides(workCenterId, year, month).stream()
                        .collect(Collectors.toMap(WorkCenterCalendarOverrideView::calendarDate, v -> v));

        YearMonth yearMonth = YearMonth.of(year, month);
        List<EffectiveCalendarDayView> result = new ArrayList<>();
        for (int day = 1; day <= yearMonth.lengthOfMonth(); day++) {
            LocalDate date = yearMonth.atDay(day);
            ProductionCalendarView base = baseByDate.get(date);
            WorkCenterCalendarOverrideView override = overrideByDate.get(date);

            Integer baseWorkTime = base != null ? base.workTime() : null;
            Integer overrideWorkTime = override != null ? override.workTime() : null;
            int effective = EffectiveMinutesResolver.resolve(overrideWorkTime, baseWorkTime, operationTime);
            String content = override != null && override.content() != null && !override.content().isBlank()
                    ? override.content()
                    : (base != null ? base.content() : null);

            result.add(new EffectiveCalendarDayView(
                    date,
                    effective,
                    override != null,
                    baseWorkTime,
                    content
            ));
        }
        return result;
    }

    public int resolveEffectiveMinutes(long workCenterId, LocalDate calendarDate) {
        requireWorkCenter(workCenterId);
        return buildEffectiveDay(workCenterId, calendarDate).effectiveWorkTime();
    }

    private EffectiveCalendarDayView buildEffectiveDay(long workCenterId, LocalDate calendarDate) {
        int operationTime = workCenterLookup.findActiveOperationTime(workCenterId);
        ProductionCalendarView base = productionCalendarRepository.findActiveByDate(calendarDate).orElse(null);
        WorkCenterCalendarOverrideView override =
                workCenterCalendarRepository.findActiveOverride(workCenterId, calendarDate).orElse(null);

        Integer baseWorkTime = base != null ? base.workTime() : null;
        Integer overrideWorkTime = override != null ? override.workTime() : null;
        int effective = EffectiveMinutesResolver.resolve(overrideWorkTime, baseWorkTime, operationTime);
        String content = override != null && override.content() != null && !override.content().isBlank()
                ? override.content()
                : (base != null ? base.content() : null);

        return new EffectiveCalendarDayView(calendarDate, effective, override != null, baseWorkTime, content);
    }

    private int resolveBaseEffective(long workCenterId, LocalDate calendarDate) {
        int operationTime = workCenterLookup.findActiveOperationTime(workCenterId);
        Integer baseWorkTime = productionCalendarRepository.findActiveByDate(calendarDate)
                .map(ProductionCalendarView::workTime)
                .orElse(null);
        return EffectiveMinutesResolver.resolveBaseWithoutOverride(baseWorkTime, operationTime);
    }

    private void requireWorkCenter(long workCenterId) {
        if (!workCenterLookup.existsActive(workCenterId)) {
            throw new IllegalArgumentException("작업장을 찾을 수 없습니다: " + workCenterId);
        }
    }

    private void validateWorkTime(int workTime) {
        if (workTime < 0 || workTime > 1440) {
            throw new IllegalArgumentException("가동시간은 0~1440분 사이여야 합니다.");
        }
    }

    private void validateYearMonth(int year, int month) {
        if (year < 1900 || year > 9999) {
            throw new IllegalArgumentException("연도가 올바르지 않습니다.");
        }
        if (month < 1 || month > 12) {
            throw new IllegalArgumentException("월은 1~12 사이여야 합니다.");
        }
    }

    private static boolean isBlank(String value) {
        return value == null || value.isBlank();
    }

    private void appendEvent(
            String eventType,
            long id,
            String actorUserId,
            long workCenterId,
            LocalDate calendarDate
    ) {
        domainEventStore.append(DomainEvent.create(
                eventType,
                1,
                AggregateTypes.WORK_CENTER_CALENDAR,
                String.valueOf(id),
                actorUserId,
                """
                {"overrideId":%d,"workCenterId":%d,"calendarDate":"%s"}
                """.formatted(id, workCenterId, calendarDate).trim()
        ));
    }
}
