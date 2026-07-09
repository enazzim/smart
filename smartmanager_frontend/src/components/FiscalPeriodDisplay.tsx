import { useMemo } from 'react';
import { FISCAL_CUTOVER_DAY, formatFiscalPeriodLabel, resolveFiscalPeriod } from '../utils/fiscalCalendar';

interface FiscalPeriodDisplayProps {
  /** YYYY-MM-DD — 입고일·납품일·검사완료일 등 */
  baseDate: string;
  className?: string;
}

/**
 * 매입년도·매입월 (25일 규칙 자동 계산, 읽기 전용)
 */
export default function FiscalPeriodDisplay({ baseDate, className }: FiscalPeriodDisplayProps) {
  const period = useMemo(() => resolveFiscalPeriod(baseDate), [baseDate]);
  const label = formatFiscalPeriodLabel(period);

  return (
    <div className={className ?? 'fiscal-period-display'}>
      <label>
        매입년도
        <input
          type="text"
          className="readonly"
          readOnly
          tabIndex={-1}
          value={String(period.fiscalYear)}
          aria-label={`매입년도 ${period.fiscalYear}`}
        />
      </label>
      <label>
        매입월
        <input
          type="text"
          className="readonly"
          readOnly
          tabIndex={-1}
          value={String(period.fiscalMonth)}
          aria-label={`매입월 ${period.fiscalMonth}`}
        />
      </label>
      <p className="fiscal-period-hint" title={label}>
        {baseDate.slice(8, 10)}일 기준 — {FISCAL_CUTOVER_DAY}일 초과 시 익월 매입
      </p>
    </div>
  );
}
