package com.shindong.smartmanager.application.calendar;

import java.time.LocalDate;
import java.time.YearMonth;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.stream.Collectors;

public class ProductionCalendarEffectiveService {

    private final ProductionCalendarRepository productionCalendarRepository;
    private final PublicHolidayRepository publicHolidayRepository;

    public ProductionCalendarEffectiveService(
            ProductionCalendarRepository productionCalendarRepository,
            PublicHolidayRepository publicHolidayRepository
    ) {
        this.productionCalendarRepository = productionCalendarRepository;
        this.publicHolidayRepository = publicHolidayRepository;
    }

    public List<ProductionCalendarEffectiveDayView> listEffectiveByYearMonth(int year, int month) {
        validateYearMonth(year, month);
        Set<LocalDate> holidays = publicHolidayRepository.findHolidayDatesByYear(year);
        Map<LocalDate, ProductionCalendarView> registeredByDate =
                productionCalendarRepository.findActiveByYearMonth(year, month).stream()
                        .collect(Collectors.toMap(ProductionCalendarView::calendarDate, view -> view));

        YearMonth yearMonth = YearMonth.of(year, month);
        List<ProductionCalendarEffectiveDayView> result = new ArrayList<>();
        for (int day = 1; day <= yearMonth.lengthOfMonth(); day++) {
            LocalDate date = yearMonth.atDay(day);
            ProductionCalendarView registered = registeredByDate.get(date);
            boolean autoOffDay = NonWorkingDayPolicy.isAutoOffDay(date, holidays);
            Integer registeredWorkTime = registered != null ? registered.workTime() : null;
            int effective = EffectiveMinutesResolver.resolve(
                    null,
                    registeredWorkTime,
                    EffectiveMinutesResolver.DEFAULT_WORK_TIME,
                    autoOffDay
            );
            result.add(new ProductionCalendarEffectiveDayView(
                    date,
                    effective,
                    registered != null,
                    autoOffDay,
                    registeredWorkTime,
                    registered != null ? registered.content() : null
            ));
        }
        return result;
    }

    public int resolveDefaultWorkTime(LocalDate calendarDate) {
        Set<LocalDate> holidays = publicHolidayRepository.findHolidayDatesByYear(calendarDate.getYear());
        boolean autoOffDay = NonWorkingDayPolicy.isAutoOffDay(calendarDate, holidays);
        Integer registeredWorkTime = productionCalendarRepository.findActiveByDate(calendarDate)
                .map(ProductionCalendarView::workTime)
                .orElse(null);
        return EffectiveMinutesResolver.resolve(
                null,
                registeredWorkTime,
                EffectiveMinutesResolver.DEFAULT_WORK_TIME,
                autoOffDay
        );
    }

    private void validateYearMonth(int year, int month) {
        if (year < 1900 || year > 9999) {
            throw new IllegalArgumentException("연도가 올바르지 않습니다.");
        }
        if (month < 1 || month > 12) {
            throw new IllegalArgumentException("월은 1~12 사이여야 합니다.");
        }
    }
}
