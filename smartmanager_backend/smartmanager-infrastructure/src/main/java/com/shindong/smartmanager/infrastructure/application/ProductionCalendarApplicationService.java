package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.calendar.ProductionCalendarService;
import com.shindong.smartmanager.application.calendar.ProductionCalendarUpsertCommand;
import com.shindong.smartmanager.application.calendar.ProductionCalendarView;
import java.time.LocalDate;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class ProductionCalendarApplicationService {

    private final ProductionCalendarService productionCalendarService;

    public ProductionCalendarApplicationService(ProductionCalendarService productionCalendarService) {
        this.productionCalendarService = productionCalendarService;
    }

    @Transactional
    public ProductionCalendarView upsertByDate(
            LocalDate calendarDate,
            ProductionCalendarUpsertCommand command,
            String actorUserId
    ) {
        return productionCalendarService.upsertByDate(calendarDate, command, actorUserId);
    }

    @Transactional
    public void deleteByDate(LocalDate calendarDate, String actorUserId) {
        productionCalendarService.deleteByDate(calendarDate, actorUserId);
    }

    @Transactional(readOnly = true)
    public List<ProductionCalendarView> listByYearMonth(int year, int month) {
        return productionCalendarService.listByYearMonth(year, month);
    }

    @Transactional(readOnly = true)
    public ProductionCalendarView getById(long id) {
        return productionCalendarService.getById(id);
    }
}
