/** 백엔드 FiscalCalendarService.FISCAL_CUTOVER_DAY 와 동일 */
export const FISCAL_CUTOVER_DAY = 25;

export interface FiscalPeriod {
  fiscalYear: number;
  fiscalMonth: number;
}

/**
 * 거래일 기준 회계월 (day <= 25 → 당월, day > 25 → 익월)
 * @param isoDate YYYY-MM-DD
 */
export function resolveFiscalPeriod(isoDate: string): FiscalPeriod {
  const [yearText, monthText, dayText] = isoDate.split('-');
  const year = Number(yearText);
  const month = Number(monthText);
  const day = Number(dayText);
  if (!Number.isFinite(year) || !Number.isFinite(month) || !Number.isFinite(day)) {
    const today = new Date().toISOString().slice(0, 10);
    return resolveFiscalPeriod(today);
  }
  if (day <= FISCAL_CUTOVER_DAY) {
    return { fiscalYear: year, fiscalMonth: month };
  }
  if (month === 12) {
    return { fiscalYear: year + 1, fiscalMonth: 1 };
  }
  return { fiscalYear: year, fiscalMonth: month + 1 };
}

export function formatFiscalPeriodLabel(period: FiscalPeriod): string {
  return `${period.fiscalYear}년 ${period.fiscalMonth}월`;
}

export function formatFiscalPeriodFromIso(isoDate: string | null | undefined): string {
  if (!isoDate) {
    return '—';
  }
  const datePart = isoDate.length >= 10 ? isoDate.slice(0, 10) : isoDate;
  return formatFiscalPeriodLabel(resolveFiscalPeriod(datePart));
}

/** ISO instant / date → 회계월 라벨 (품질검사 completedAt 등) */
export function formatFiscalPeriodFromInstant(value: string | null | undefined): string {
  if (!value) {
    return '—';
  }
  return formatFiscalPeriodFromIso(value.slice(0, 10));
}

/** 검색 기본값용 — 달력 기준 이번 년·월 */
export function currentCalendarYearMonth(): FiscalPeriod {
  const now = new Date();
  return { fiscalYear: now.getFullYear(), fiscalMonth: now.getMonth() + 1 };
}
