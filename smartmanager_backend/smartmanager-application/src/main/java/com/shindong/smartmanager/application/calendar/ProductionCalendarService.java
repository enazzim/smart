package com.shindong.smartmanager.application.calendar;

import com.shindong.smartmanager.application.event.DomainEventStore;
import com.shindong.smartmanager.domain.event.AggregateTypes;
import com.shindong.smartmanager.domain.event.DomainEvent;
import com.shindong.smartmanager.domain.event.EventTypes;
import java.time.LocalDate;
import java.util.List;

public class ProductionCalendarService {

    private final ProductionCalendarRepository productionCalendarRepository;
    private final DomainEventStore domainEventStore;

    public ProductionCalendarService(
            ProductionCalendarRepository productionCalendarRepository,
            DomainEventStore domainEventStore
    ) {
        this.productionCalendarRepository = productionCalendarRepository;
        this.domainEventStore = domainEventStore;
    }

    public ProductionCalendarView upsertByDate(
            LocalDate calendarDate,
            ProductionCalendarUpsertCommand command,
            String actorUserId
    ) {
        validateWorkTime(command.workTime());
        boolean existed = productionCalendarRepository.findActiveByDate(calendarDate).isPresent();
        ProductionCalendarView view = productionCalendarRepository.upsertByDate(calendarDate, command, actorUserId);
        appendEvent(
                existed ? EventTypes.STANDARD_CALENDAR_DAY_UPDATED : EventTypes.STANDARD_CALENDAR_DAY_REGISTERED,
                view.id(),
                actorUserId,
                calendarDate
        );
        return view;
    }

    public void deleteByDate(LocalDate calendarDate, String actorUserId) {
        ProductionCalendarView existing = productionCalendarRepository.findActiveByDate(calendarDate)
                .orElseThrow(() -> new IllegalArgumentException("기본생산달력을 찾을 수 없습니다: " + calendarDate));
        productionCalendarRepository.softDeleteByDate(calendarDate, actorUserId);
        domainEventStore.append(DomainEvent.create(
                EventTypes.STANDARD_CALENDAR_DAY_DELETED,
                1,
                AggregateTypes.PRODUCTION_CALENDAR,
                String.valueOf(existing.id()),
                actorUserId,
                """
                {"calendarId":%d,"calendarDate":"%s"}
                """.formatted(existing.id(), calendarDate).trim()
        ));
    }

    public List<ProductionCalendarView> listByYearMonth(int year, int month) {
        validateYearMonth(year, month);
        return productionCalendarRepository.findActiveByYearMonth(year, month);
    }

    public ProductionCalendarView getById(long id) {
        return productionCalendarRepository.findActiveById(id)
                .orElseThrow(() -> new IllegalArgumentException("기본생산달력을 찾을 수 없습니다: " + id));
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

    private void appendEvent(String eventType, long id, String actorUserId, LocalDate calendarDate) {
        domainEventStore.append(DomainEvent.create(
                eventType,
                1,
                AggregateTypes.PRODUCTION_CALENDAR,
                String.valueOf(id),
                actorUserId,
                """
                {"calendarId":%d,"calendarDate":"%s"}
                """.formatted(id, calendarDate).trim()
        ));
    }
}
