import { useCallback, useEffect, useMemo, useState } from 'react';
import MonthCalendarGrid, { MonthNavigator } from '../components/MonthCalendarGrid';
import GridExcelExportButton from '../components/GridExcelExportButton';
import {
  DEFAULT_WORK_TIME,
  deleteProductionCalendarByDate,
  fetchProductionCalendarEffective,
  formatWorkTimeLabel,
  upsertProductionCalendarByDate,
  type ProductionCalendarEffectiveDay,
} from '../api/productionCalendar';

export default function ProductionCalendarPage() {
  const now = new Date();
  const [year, setYear] = useState(now.getFullYear());
  const [month, setMonth] = useState(now.getMonth() + 1);
  const [days, setDays] = useState<ProductionCalendarEffectiveDay[]>([]);
  const [selectedDate, setSelectedDate] = useState<string | null>(null);
  const [workTime, setWorkTime] = useState(DEFAULT_WORK_TIME);
  const [content, setContent] = useState('');
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const dayMap = useMemo(() => new Map(days.map((d) => [d.calendarDate, d])), [days]);

  const calendarExportRows = useMemo(
    () =>
      days.map((day) => ({
        일자: day.calendarDate,
        '유효가동(분)': day.effectiveWorkTime,
        등록여부: day.registered ? 'Y' : 'N',
        자동휴무: day.autoOffDay ? 'Y' : 'N',
        '등록가동(분)': day.registeredWorkTime ?? '',
        비고: day.content ?? '',
      })),
    [days],
  );

  const load = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      setDays(await fetchProductionCalendarEffective(year, month));
    } catch (e) {
      setError(e instanceof Error ? e.message : '달력 조회 실패');
    } finally {
      setLoading(false);
    }
  }, [year, month]);

  useEffect(() => {
    void load();
  }, [load]);

  const openDay = (date: string) => {
    const existing = dayMap.get(date);
    setSelectedDate(date);
    setWorkTime(existing?.effectiveWorkTime ?? DEFAULT_WORK_TIME);
    setContent(existing?.content ?? '');
    setError(null);
  };

  const closeModal = () => {
    setSelectedDate(null);
  };

  const onSave = async () => {
    if (!selectedDate) {
      return;
    }
    setSubmitting(true);
    setError(null);
    try {
      await upsertProductionCalendarByDate(selectedDate, {
        workTime,
        content: content || undefined,
      });
      closeModal();
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '저장 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async () => {
    if (!selectedDate) {
      return;
    }
    const existing = dayMap.get(selectedDate);
    if (!existing?.registered) {
      return;
    }
    if (!window.confirm(`${selectedDate} 기본생산달력 설정을 삭제하시겠습니까?`)) {
      return;
    }
    setSubmitting(true);
    setError(null);
    try {
      await deleteProductionCalendarByDate(selectedDate);
      closeModal();
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '삭제 실패');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>기본생산달력</h1>
        <p>공장 전체 가동일·휴무일 — 주말·공휴일 자동 휴무, 일자별 upsert (평일 기본 480분)</p>
      </header>

      {error && <div className="error">{error}</div>}

      <section className="panel">
        <div className="panel-header-row">
          <h2>
            {year}년 {month}월 기본생산달력
          </h2>
          <GridExcelExportButton
            fileBaseName={`기본생산달력_${year}${String(month).padStart(2, '0')}`}
            sheetName="기본생산달력"
            disabled={loading}
            rows={calendarExportRows}
          />
        </div>
        <MonthNavigator
          year={year}
          month={month}
          onChange={(y, m) => {
            setYear(y);
            setMonth(m);
          }}
        />
        {loading ? (
          <p>로딩 중…</p>
        ) : (
          <MonthCalendarGrid
            year={year}
            month={month}
            renderCell={(cell) => {
              const entry = dayMap.get(cell.date);
              const minutes = entry?.effectiveWorkTime ?? DEFAULT_WORK_TIME;
              const autoOffDay = entry?.autoOffDay ?? false;
              const registered = entry?.registered ?? false;
              return (
                <button
                  type="button"
                  className="calendar-day-btn"
                  onClick={() => openDay(cell.date)}
                >
                  <span className="calendar-day-num">{cell.day}</span>
                  <span
                    className={`calendar-day-label${minutes === 0 ? ' calendar-holiday' : ''}`}
                  >
                    {formatWorkTimeLabel(minutes, autoOffDay && !registered)}
                  </span>
                  {registered && entry?.content && <span className="calendar-day-badge">비고</span>}
                  {!registered && autoOffDay && (
                    <span className="calendar-day-default">자동휴무</span>
                  )}
                  {!registered && !autoOffDay && (
                    <span className="calendar-day-default">기본</span>
                  )}
                </button>
              );
            }}
            cellClassName={(cell) => {
              const entry = dayMap.get(cell.date);
              const classes = [];
              if (cell.dayOfWeek === 0 || cell.dayOfWeek === 6) {
                classes.push('weekend');
              }
              if (entry?.autoOffDay && !entry.registered) {
                classes.push('auto-off');
              }
              return classes.join(' ');
            }}
          />
        )}
      </section>

      {selectedDate && (
        <div className="modal-backdrop" onClick={closeModal}>
          <div className="modal" onClick={(e) => e.stopPropagation()}>
            <h2>{selectedDate} — 기본생산달력</h2>
            <div className="form-grid">
              <label>
                가동시간(분) *
                <input
                  type="number"
                  min={0}
                  max={1440}
                  value={workTime}
                  onChange={(e) => setWorkTime(Number(e.target.value))}
                />
              </label>
              <label>
                비고
                <input value={content} onChange={(e) => setContent(e.target.value)} />
              </label>
            </div>
            <div className="form-actions">
              <button type="button" onClick={() => void onSave()} disabled={submitting}>
                {submitting ? '저장 중…' : '저장'}
              </button>
              {dayMap.get(selectedDate)?.registered && (
                <button type="button" className="danger" onClick={() => void onDelete()} disabled={submitting}>
                  삭제
                </button>
              )}
              <button type="button" className="secondary" onClick={closeModal} disabled={submitting}>
                닫기
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
