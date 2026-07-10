import { useState, type KeyboardEvent } from 'react';
import type { FiscalPeriod } from '../utils/fiscalCalendar';

interface FiscalPeriodTableCellsProps {
  fiscalYear: number;
  fiscalMonth: number;
  disabled?: boolean;
  onSave: (period: FiscalPeriod) => Promise<void>;
}

export default function FiscalPeriodTableCells({
  fiscalYear,
  fiscalMonth,
  disabled = false,
  onSave,
}: FiscalPeriodTableCellsProps) {
  const [editing, setEditing] = useState<'year' | 'month' | null>(null);
  const [draft, setDraft] = useState('');
  const [saving, setSaving] = useState(false);

  const startEdit = (field: 'year' | 'month') => {
    if (disabled || saving) {
      return;
    }
    setEditing(field);
    setDraft(String(field === 'year' ? fiscalYear : fiscalMonth));
  };

  const commitEdit = async () => {
    if (!editing || saving) {
      return;
    }
    const value = Number(draft);
    if (!Number.isFinite(value)) {
      setEditing(null);
      return;
    }

    let next: FiscalPeriod | null = null;
    if (editing === 'year') {
      if (value < 2000 || value > 2100 || value === fiscalYear) {
        setEditing(null);
        return;
      }
      next = { fiscalYear: value, fiscalMonth };
    } else if (value < 1 || value > 12 || value === fiscalMonth) {
      setEditing(null);
      return;
    } else {
      next = { fiscalYear, fiscalMonth: value };
    }

    setSaving(true);
    try {
      await onSave(next);
    } finally {
      setSaving(false);
      setEditing(null);
    }
  };

  const onKeyDown = (e: KeyboardEvent<HTMLInputElement>) => {
    if (e.key === 'Enter') {
      void commitEdit();
    }
    if (e.key === 'Escape') {
      setEditing(null);
    }
  };

  const renderCell = (field: 'year' | 'month', value: number, label: string) => (
    <td className="fiscal-period-cell">
      {editing === field ? (
        <input
          type="number"
          className="fiscal-period-field fiscal-period-field-editing"
          min={field === 'year' ? 2000 : 1}
          max={field === 'year' ? 2100 : 12}
          value={draft}
          onChange={(e) => setDraft(e.target.value)}
          onBlur={() => void commitEdit()}
          onKeyDown={onKeyDown}
          autoFocus
          aria-label={label}
        />
      ) : (
        <input
          type="text"
          className="readonly fiscal-period-field fiscal-period-field-table"
          readOnly
          tabIndex={disabled ? -1 : 0}
          value={String(value)}
          onDoubleClick={() => startEdit(field)}
          title={disabled ? undefined : '더블클릭하여 수정'}
          aria-label={`${label} ${value}`}
        />
      )}
    </td>
  );

  return (
    <>
      {renderCell('year', fiscalYear, '매입년도')}
      {renderCell('month', fiscalMonth, '매입월')}
    </>
  );
}
