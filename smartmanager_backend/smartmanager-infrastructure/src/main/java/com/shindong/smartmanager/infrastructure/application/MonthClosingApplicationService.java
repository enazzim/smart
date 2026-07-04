package com.shindong.smartmanager.infrastructure.application;

import com.shindong.smartmanager.application.closing.CloseMonthCommand;
import com.shindong.smartmanager.application.closing.FiscalPeriodStatusView;
import com.shindong.smartmanager.application.closing.MonthClosingService;
import com.shindong.smartmanager.application.closing.MonthClosingView;
import java.time.LocalDate;
import java.util.List;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class MonthClosingApplicationService {

    private final MonthClosingService monthClosingService;

    public MonthClosingApplicationService(MonthClosingService monthClosingService) {
        this.monthClosingService = monthClosingService;
    }

    @Transactional(readOnly = true)
    public List<MonthClosingView> listClosed() {
        return monthClosingService.listClosed();
    }

    @Transactional(readOnly = true)
    public FiscalPeriodStatusView resolveStatus(LocalDate referenceDate) {
        return monthClosingService.resolveStatus(referenceDate);
    }

    @Transactional
    public MonthClosingView close(CloseMonthCommand command, String closedBy, String closedById) {
        return monthClosingService.close(command, closedBy, closedById);
    }

    @Transactional
    public void reopen(CloseMonthCommand command) {
        monthClosingService.reopen(command);
    }

    public MonthClosingService monthClosingService() {
        return monthClosingService;
    }
}
