package com.shindong.smartmanager.application.closing;

import com.shindong.smartmanager.application.system.SystemSettingService;
import java.time.LocalDate;
import java.time.YearMonth;
import java.util.function.Function;

/**
 * 회계월 마감일 규칙: 거래일 day &lt;= N → 해당 달, day &gt; N → 다음 달 회계월.
 * N은 시스템 설정 {@code closing.fiscal_cutover_day}(고정일 또는 LAST)에서 조회한다.
 */
public class FiscalCalendarService {

    public static final int DEFAULT_FISCAL_CUTOVER_DAY = 25;

    private final Function<LocalDate, Integer> cutoverDayResolver;

    public FiscalCalendarService(SystemSettingService systemSettingService) {
        this(date -> FiscalCutoverPolicy.cutoverDayFor(
                date,
                systemSettingService.resolveFiscalCutoverSettingValue()
        ));
    }

    /** 테스트·기본값 고정용 */
    public FiscalCalendarService() {
        this(date -> DEFAULT_FISCAL_CUTOVER_DAY);
    }

    FiscalCalendarService(int fixedCutoverDay) {
        this(date -> fixedCutoverDay);
    }

    FiscalCalendarService(String settingValue) {
        this(date -> FiscalCutoverPolicy.cutoverDayFor(date, settingValue));
    }

    FiscalCalendarService(Function<LocalDate, Integer> cutoverDayResolver) {
        this.cutoverDayResolver = cutoverDayResolver;
    }

    public int resolveCutoverDay(LocalDate date) {
        int day = cutoverDayResolver.apply(date);
        if (day < 1 || day > 31) {
            return DEFAULT_FISCAL_CUTOVER_DAY;
        }
        return day;
    }

    public FiscalPeriod resolvePeriod(LocalDate date) {
        int cutoverDay = resolveCutoverDay(date);
        if (date.getDayOfMonth() <= cutoverDay) {
            return new FiscalPeriod(date.getYear(), date.getMonthValue());
        }
        LocalDate nextMonth = date.plusMonths(1);
        return new FiscalPeriod(nextMonth.getYear(), nextMonth.getMonthValue());
    }

    public FiscalPeriod resolvePeriod(LocalDate date, Integer fiscalYear, Integer fiscalMonth) {
        if (fiscalYear != null || fiscalMonth != null) {
            if (fiscalYear == null || fiscalMonth == null) {
                throw new IllegalArgumentException("매입년도와 매입월을 함께 지정해야 합니다.");
            }
            if (fiscalYear < 2000 || fiscalYear > 2100) {
                throw new IllegalArgumentException("매입년도가 올바르지 않습니다.");
            }
            return new FiscalPeriod(fiscalYear, fiscalMonth);
        }
        return resolvePeriod(date);
    }

    public int compare(FiscalPeriod left, FiscalPeriod right) {
        if (left.fiscalYear() != right.fiscalYear()) {
            return Integer.compare(left.fiscalYear(), right.fiscalYear());
        }
        return Integer.compare(left.fiscalMonth(), right.fiscalMonth());
    }

    /**
     * 회계연·월에 속하는 달력 일자 구간(양끝 포함).
     * 예: 마감일 25일, 2024년 7월 회계월 → 2024-06-26 ~ 2024-07-25
     */
    public FiscalPeriodDateRange toCalendarDateRange(FiscalPeriod period) {
        if (period == null) {
            throw new IllegalArgumentException("회계기간이 필요합니다.");
        }
        YearMonth endYm = YearMonth.of(period.fiscalYear(), period.fiscalMonth());
        int endCutover = Math.min(resolveCutoverDay(endYm.atEndOfMonth()), endYm.lengthOfMonth());
        LocalDate endInclusive = endYm.atDay(endCutover);

        FiscalPeriod previous = period.previous();
        YearMonth startYm = YearMonth.of(previous.fiscalYear(), previous.fiscalMonth());
        int prevCutover = Math.min(resolveCutoverDay(startYm.atEndOfMonth()), startYm.lengthOfMonth());
        LocalDate startInclusive = startYm.atDay(prevCutover).plusDays(1);

        return new FiscalPeriodDateRange(startInclusive, endInclusive);
    }
}
