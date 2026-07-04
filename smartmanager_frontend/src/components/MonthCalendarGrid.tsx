import { useMemo } from 'react';

export interface CalendarCell {
  date: string;
  day: number;
  inMonth: boolean;
}

const WEEKDAYS = ['일', '월', '화', '수', '목', '금', '토'];

export function buildMonthCells(year: number, month: number): CalendarCell[] {
  const first = new Date(year, month - 1, 1);
  const lastDay = new Date(year, month, 0).getDate();
  const startOffset = first.getDay();
  const cells: CalendarCell[] = [];

  for (let i = 0; i < startOffset; i++) {
    cells.push({ date: '', day: 0, inMonth: false });
  }
  for (let day = 1; day <= lastDay; day++) {
    const mm = String(month).padStart(2, '0');
    const dd = String(day).padStart(2, '0');
    cells.push({ date: `${year}-${mm}-${dd}`, day, inMonth: true });
  }
  return cells;
}

interface MonthCalendarGridProps {
  year: number;
  month: number;
  renderCell: (cell: CalendarCell) => React.ReactNode;
}

export default function MonthCalendarGrid({ year, month, renderCell }: MonthCalendarGridProps) {
  const cells = useMemo(() => buildMonthCells(year, month), [year, month]);

  return (
    <div className="month-calendar">
      <div className="month-calendar-header">
        {WEEKDAYS.map((wd) => (
          <div key={wd} className="month-calendar-weekday">
            {wd}
          </div>
        ))}
      </div>
      <div className="month-calendar-grid">
        {cells.map((cell, idx) => (
          <div
            key={`${cell.date || 'blank'}-${idx}`}
            className={`month-calendar-cell${cell.inMonth ? '' : ' month-calendar-cell-blank'}`}
          >
            {cell.inMonth ? renderCell(cell) : null}
          </div>
        ))}
      </div>
    </div>
  );
}

interface MonthNavigatorProps {
  year: number;
  month: number;
  onChange: (year: number, month: number) => void;
}

export function MonthNavigator({ year, month, onChange }: MonthNavigatorProps) {
  const prev = () => {
    if (month === 1) {
      onChange(year - 1, 12);
    } else {
      onChange(year, month - 1);
    }
  };
  const next = () => {
    if (month === 12) {
      onChange(year + 1, 1);
    } else {
      onChange(year, month + 1);
    }
  };

  return (
    <div className="month-navigator">
      <button type="button" className="secondary" onClick={prev}>
        ◀
      </button>
      <strong>
        {year}년 {month}월
      </strong>
      <button type="button" className="secondary" onClick={next}>
        ▶
      </button>
    </div>
  );
}
