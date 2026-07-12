import { useState, type KeyboardEvent } from 'react';
import { formatFiscalCutoverHint, formatFiscalPeriodLabel, type FiscalPeriod } from '../utils/fiscalCalendar';
import { useMaterialIssueSetting } from '../context/MaterialIssueSettingContext';

interface FiscalPeriodDisplayProps {
  /** YYYY-MM-DD — 입고일·납품일·검사완료일 등 */
  baseDate: string;
  period: FiscalPeriod;
  onPeriodChange: (period: FiscalPeriod) => void;
  className?: string;
}

/**
 * 매입년도·매입월 (매입마감일 규칙 자동 계산, 더블클릭으로 수동 수정)
 */
export default function FiscalPeriodDisplay({
  baseDate,
  period,
  onPeriodChange,
  className,
}: FiscalPeriodDisplayProps) {
  const { fiscalCutoverSetting } = useMaterialIssueSetting();
  const label = formatFiscalPeriodLabel(period);
  const [editing, setEditing] = useState<'year' | 'month' | null>(null);
  const [draft, setDraft] = useState('');

  const startEdit = (field: 'year' | 'month') => {
    setEditing(field);
    setDraft(String(field === 'year' ? period.fiscalYear : period.fiscalMonth));
  };

  const commitEdit = () => {
    if (!editing) {
      return;
    }
    const value = Number(draft);
    if (!Number.isFinite(value)) {
      setEditing(null);
      return;
    }
    if (editing === 'year') {
      if (value < 2000 || value > 2100) {
        setEditing(null);
        return;
      }
      onPeriodChange({ ...period, fiscalYear: value });
    } else if (value < 1 || value > 12) {
      setEditing(null);
      return;
    } else {
      onPeriodChange({ ...period, fiscalMonth: value });
    }
    setEditing(null);
  };

  const onKeyDown = (e: KeyboardEvent<HTMLInputElement>) => {
    if (e.key === 'Enter') {
      commitEdit();
    }
    if (e.key === 'Escape') {
      setEditing(null);
    }
  };

  return (
    <div className={className ?? 'fiscal-period-display'}>
      <label>
        매입년도
        {editing === 'year' ? (
          <input
            type="number"
            className="fiscal-period-field fiscal-period-field-editing"
            min={2000}
            max={2100}
            value={draft}
            onChange={(e) => setDraft(e.target.value)}
            onBlur={commitEdit}
            onKeyDown={onKeyDown}
            autoFocus
            aria-label="매입년도 수정"
          />
        ) : (
          <input
            type="text"
            className="readonly fiscal-period-field"
            readOnly
            tabIndex={0}
            value={String(period.fiscalYear)}
            onDoubleClick={() => startEdit('year')}
            title="더블클릭하여 수정"
            aria-label={`매입년도 ${period.fiscalYear}`}
          />
        )}
      </label>
      <label>
        매입월
        {editing === 'month' ? (
          <input
            type="number"
            className="fiscal-period-field fiscal-period-field-editing"
            min={1}
            max={12}
            value={draft}
            onChange={(e) => setDraft(e.target.value)}
            onBlur={commitEdit}
            onKeyDown={onKeyDown}
            autoFocus
            aria-label="매입월 수정"
          />
        ) : (
          <input
            type="text"
            className="readonly fiscal-period-field"
            readOnly
            tabIndex={0}
            value={String(period.fiscalMonth)}
            onDoubleClick={() => startEdit('month')}
            title="더블클릭하여 수정"
            aria-label={`매입월 ${period.fiscalMonth}`}
          />
        )}
      </label>
      <p className="fiscal-period-hint" title={label}>
        {formatFiscalCutoverHint(baseDate, fiscalCutoverSetting)}
      </p>
    </div>
  );
}
