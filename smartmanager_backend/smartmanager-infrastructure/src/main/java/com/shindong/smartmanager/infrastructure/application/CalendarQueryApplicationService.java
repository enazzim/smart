package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.calendar.ProductionCalendarEffectiveDayView;
import com.shindong.smartmanager.application.calendar.ProductionCalendarEffectiveService;
import com.shindong.smartmanager.application.calendar.PublicHolidayView;
import com.shindong.smartmanager.application.calendar.PublicHolidayRepository;
import com.shindong.smartmanager.application.calendar.WorkCenterCapaService;
import com.shindong.smartmanager.application.calendar.WorkCenterCapaView;
import java.time.LocalDate;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class CalendarQueryApplicationService {

    private final ProductionCalendarEffectiveService productionCalendarEffectiveService;
    private final PublicHolidayRepository publicHolidayRepository;
    private final WorkCenterCapaService workCenterCapaService;

    public CalendarQueryApplicationService(
            ProductionCalendarEffectiveService productionCalendarEffectiveService,
            PublicHolidayRepository publicHolidayRepository,
            WorkCenterCapaService workCenterCapaService
    ) {
        this.productionCalendarEffectiveService = productionCalendarEffectiveService;
        this.publicHolidayRepository = publicHolidayRepository;
        this.workCenterCapaService = workCenterCapaService;
    }

    @Transactional(readOnly = true)
    public List<ProductionCalendarEffectiveDayView> listProductionCalendarEffective(int year, int month) {
        return productionCalendarEffectiveService.listEffectiveByYearMonth(year, month);
    }

    @Transactional(readOnly = true)
    public List<PublicHolidayView> listPublicHolidays(int year) {
        return publicHolidayRepository.findByYear(year);
    }

    @Transactional(readOnly = true)
    public WorkCenterCapaView resolveWorkCenterCapa(long workCenterId, LocalDate calendarDate) {
        return workCenterCapaService.resolveCapa(workCenterId, calendarDate);
    }
}
