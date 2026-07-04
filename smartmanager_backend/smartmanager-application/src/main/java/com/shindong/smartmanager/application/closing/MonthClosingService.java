package com.shindong.smartmanager.application.closing;

import java.time.LocalDate;
import java.util.List;

public class MonthClosingService {

    private final MonthClosingRepository monthClosingRepository;
    private final FiscalCalendarService fiscalCalendarService;

    public MonthClosingService(
            MonthClosingRepository monthClosingRepository,
            FiscalCalendarService fiscalCalendarService
    ) {
        this.monthClosingRepository = monthClosingRepository;
        this.fiscalCalendarService = fiscalCalendarService;
    }

    public List<MonthClosingView> listClosed() {
        return monthClosingRepository.findAllClosed();
    }

    public FiscalPeriodStatusView resolveStatus(LocalDate referenceDate) {
        FiscalPeriod period = fiscalCalendarService.resolvePeriod(referenceDate);
        return new FiscalPeriodStatusView(
                referenceDate,
                period.fiscalYear(),
                period.fiscalMonth(),
                monthClosingRepository.isClosed(period.fiscalYear(), period.fiscalMonth())
        );
    }

    public MonthClosingView close(CloseMonthCommand command, String closedBy, String closedById) {
        validateCloseCommand(command);

        FiscalPeriod target = new FiscalPeriod(command.fiscalYear(), command.fiscalMonth());
        FiscalPeriod current = fiscalCalendarService.resolvePeriod(LocalDate.now());

        if (fiscalCalendarService.compare(target, current) >= 0) {
            throw new IllegalArgumentException("현재 회계월 이후는 마감할 수 없습니다.");
        }

        if (monthClosingRepository.isClosed(target.fiscalYear(), target.fiscalMonth())) {
            return monthClosingRepository.findClosed(target.fiscalYear(), target.fiscalMonth())
                    .orElseThrow(() -> new IllegalStateException("마감 정보를 찾을 수 없습니다."));
        }

        if (monthClosingRepository.hasAnyClosed()) {
            FiscalPeriod previous = target.previous();
            if (!monthClosingRepository.isClosed(previous.fiscalYear(), previous.fiscalMonth())) {
                throw new IllegalArgumentException(
                        "이전 회계월(%d-%02d)을 먼저 마감해야 합니다."
                                .formatted(previous.fiscalYear(), previous.fiscalMonth())
                );
            }
        }

        return monthClosingRepository.saveClose(
                target.fiscalYear(),
                target.fiscalMonth(),
                closedBy,
                closedById
        );
    }

    public void reopen(CloseMonthCommand command) {
        validateCloseCommand(command);

        FiscalPeriod target = new FiscalPeriod(command.fiscalYear(), command.fiscalMonth());
        if (!monthClosingRepository.isClosed(target.fiscalYear(), target.fiscalMonth())) {
            throw new IllegalArgumentException(
                    "회계월 %d-%02d은(는) 마감 상태가 아닙니다."
                            .formatted(target.fiscalYear(), target.fiscalMonth())
            );
        }

        MonthClosingView latest = monthClosingRepository.findLatestClosed()
                .orElseThrow(() -> new IllegalStateException("마감 이력이 없습니다."));
        if (latest.fiscalYear() != target.fiscalYear() || latest.fiscalMonth() != target.fiscalMonth()) {
            throw new IllegalArgumentException(
                    "가장 최근 마감 회계월(%d-%02d)만 해제할 수 있습니다."
                            .formatted(latest.fiscalYear(), latest.fiscalMonth())
            );
        }

        monthClosingRepository.reopen(target.fiscalYear(), target.fiscalMonth());
    }

    public void assertTransactionOpen(LocalDate transactionDate) {
        FiscalPeriod period = fiscalCalendarService.resolvePeriod(transactionDate);
        assertPeriodOpen(period.fiscalYear(), period.fiscalMonth());
    }

    public void assertPeriodOpen(int fiscalYear, int fiscalMonth) {
        if (monthClosingRepository.isClosed(fiscalYear, fiscalMonth)) {
            throw new IllegalStateException(
                    "회계월 %d-%02d은(는) 마감되어 트랜잭션을 처리할 수 없습니다."
                            .formatted(fiscalYear, fiscalMonth)
            );
        }
    }

    public FiscalCalendarService fiscalCalendarService() {
        return fiscalCalendarService;
    }

    private void validateCloseCommand(CloseMonthCommand command) {
        if (command.fiscalYear() < 2000 || command.fiscalYear() > 2100) {
            throw new IllegalArgumentException("회계연도가 올바르지 않습니다.");
        }
        if (command.fiscalMonth() < 1 || command.fiscalMonth() > 12) {
            throw new IllegalArgumentException("회계월은 1~12 사이여야 합니다.");
        }
    }
}
