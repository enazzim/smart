/** 백엔드 FiscalCutoverPolicy.DEFAULT_SETTING_VALUE 와 동일 */
export const DEFAULT_FISCAL_CUTOVER_SETTING = '25';

/** 매월 말일(28~31일 자동) */
export const FISCAL_CUTOVER_LAST = 'LAST';

/** @deprecated DEFAULT_FISCAL_CUTOVER_SETTING 사용 */
export const DEFAULT_FISCAL_CUTOVER_DAY = 25;

/** @deprecated DEFAULT_FISCAL_CUTOVER_DAY 사용 */
export const FISCAL_CUTOVER_DAY = DEFAULT_FISCAL_CUTOVER_DAY;

export interface FiscalPeriod {
  fiscalYear: number;
  fiscalMonth: number;
}

export function isFiscalCutoverLast(setting: string): boolean {
  return setting.trim().toUpperCase() === FISCAL_CUTOVER_LAST;
}

export function normalizeFiscalCutoverSetting(value: string | null | undefined): string {
  if (!value || !value.trim()) {
    return DEFAULT_FISCAL_CUTOVER_SETTING;
  }
  const trimmed = value.trim();
  if (isFiscalCutoverLast(trimmed) || trimmed === '말일' || trimmed.toUpperCase() === 'END_OF_MONTH') {
    return FISCAL_CUTOVER_LAST;
  }
  const day = Number(trimmed);
  if (!Number.isFinite(day) || day < 1 || day > 31) {
    return DEFAULT_FISCAL_CUTOVER_SETTING;
  }
  return String(Math.trunc(day));
}

/** 거래일이 속한 달의 마감 기준일 (LAST면 해당 월 말일) */
export function resolveCutoverDayForDate(isoDate: string, setting: string = DEFAULT_FISCAL_CUTOVER_SETTING): number {
  const [yearText, monthText] = isoDate.split('-');
  const year = Number(yearText);
  const month = Number(monthText);
  if (!Number.isFinite(year) || !Number.isFinite(month) || month < 1 || month > 12) {
    return DEFAULT_FISCAL_CUTOVER_DAY;
  }
  const normalized = normalizeFiscalCutoverSetting(setting);
  if (isFiscalCutoverLast(normalized)) {
    return new Date(year, month, 0).getDate();
  }
  return Number(normalized);
}

/**
 * 거래일 기준 회계월 (day <= cutoverDay → 당월, day > cutoverDay → 익월)
 * @param isoDate YYYY-MM-DD
 */
export function resolveFiscalPeriod(
  isoDate: string,
  setting: string = DEFAULT_FISCAL_CUTOVER_SETTING,
): FiscalPeriod {
  const [yearText, monthText, dayText] = isoDate.split('-');
  const year = Number(yearText);
  const month = Number(monthText);
  const day = Number(dayText);
  if (!Number.isFinite(year) || !Number.isFinite(month) || !Number.isFinite(day)) {
    const today = new Date().toISOString().slice(0, 10);
    return resolveFiscalPeriod(today, setting);
  }
  const cutoverDay = resolveCutoverDayForDate(isoDate, setting);
  if (day <= cutoverDay) {
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

export function formatFiscalPeriodFromIso(
  isoDate: string | null | undefined,
  setting: string = DEFAULT_FISCAL_CUTOVER_SETTING,
): string {
  if (!isoDate) {
    return '—';
  }
  const datePart = isoDate.length >= 10 ? isoDate.slice(0, 10) : isoDate;
  return formatFiscalPeriodLabel(resolveFiscalPeriod(datePart, setting));
}

/** ISO instant / date → 회계월 라벨 (품질검사 completedAt 등) */
export function formatFiscalPeriodFromInstant(
  value: string | null | undefined,
  setting: string = DEFAULT_FISCAL_CUTOVER_SETTING,
): string {
  if (!value) {
    return '—';
  }
  return formatFiscalPeriodFromIso(value.slice(0, 10), setting);
}

/** 오늘 거래일 기준 현재 회계월 (검색 기본값·월마감 폼용) */
export function currentFiscalYearMonth(setting: string = DEFAULT_FISCAL_CUTOVER_SETTING): FiscalPeriod {
  return resolveFiscalPeriod(new Date().toISOString().slice(0, 10), setting);
}

/** @deprecated currentFiscalYearMonth 사용 */
export function currentCalendarYearMonth(setting: string = DEFAULT_FISCAL_CUTOVER_SETTING): FiscalPeriod {
  return currentFiscalYearMonth(setting);
}

export function formatFiscalCutoverSettingLabel(setting: string): string {
  const normalized = normalizeFiscalCutoverSetting(setting);
  if (isFiscalCutoverLast(normalized)) {
    return '매월 말일';
  }
  return `매월 ${normalized}일`;
}

export function formatFiscalCutoverHint(isoDate: string, setting: string): string {
  const normalized = normalizeFiscalCutoverSetting(setting);
  if (isFiscalCutoverLast(normalized)) {
    const lastDay = resolveCutoverDayForDate(isoDate, normalized);
    return `${isoDate.slice(8, 10)}일 기준 — 매월 말일(${lastDay}일) 초과 시 익월 매입`;
  }
  return `${isoDate.slice(8, 10)}일 기준 — ${normalized}일 초과 시 익월 매입 (더블클릭 수정)`;
}

/** @deprecated normalizeFiscalCutoverSetting 사용 */
export function parseFiscalCutoverDay(value: string | null | undefined): number {
  return resolveCutoverDayForDate(new Date().toISOString().slice(0, 10), normalizeFiscalCutoverSetting(value));
}
