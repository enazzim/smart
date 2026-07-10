import { useCallback, useEffect, useMemo, useState } from 'react';
import MonthCalendarGrid, { MonthNavigator } from '../components/MonthCalendarGrid';
import GridExcelExportButton from '../components/GridExcelExportButton';
import type { WorkCenter } from '../api/workCenter';
import { fetchWorkCenters } from '../api/workCenter';
import type { EffectiveCalendarDay } from '../api/workCenterCalendar';
import {
  deleteWorkCenterCalendarOverride,
  fetchEffectiveCalendar,
  formatWorkTimeLabel,
  upsertWorkCenterCalendarOverride,
} from '../api/workCenterCalendar';

export default function WorkCenterCalendarPage() {
  const now = new Date();
  const [workCenters, setWorkCenters] = useState<WorkCenter[]>([]);
  const [workCenterId, setWorkCenterId] = useState<number>(0);
  const [year, setYear] = useState(now.getFullYear());
  const [month, setMonth] = useState(now.getMonth() + 1);
  const [days, setDays] = useState<EffectiveCalendarDay[]>([]);
  const [selectedDay, setSelectedDay] = useState<EffectiveCalendarDay | null>(null);
  const [workTime, setWorkTime] = useState(480);
  const [content, setContent] = useState('');
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const dayMap = useMemo(() => new Map(days.map((d) => [d.calendarDate, d])), [days]);

  const selectedWorkCenterName = workCenters.find((wc) => wc.id === workCenterId)?.wcName ?? '';

  const calendarExportRows = useMemo(
    () =>
      days.map((day) => ({
        일자: day.calendarDate,
        '유효가동(분)': day.effectiveWorkTime,
        Override: day.isOverride ? 'Y' : 'N',
        '기본달력(분)': day.baseWorkTime ?? '',
        비고: day.content ?? '',
      })),
    [days],
  );

  useEffect(() => {
    void (async () => {
      try {
        const centers = await fetchWorkCenters();
        setWorkCenters(centers);
        if (centers.length > 0) {
          setWorkCenterId(centers[0].id);
        }
      } catch (e) {
        setError(e instanceof Error ? e.message : '작업장 조회 실패');
      }
    })();
  }, []);

  const load = useCallback(async () => {
    if (workCenterId <= 0) {
      return;
    }
    setLoading(true);
    setError(null);
    try {
      setDays(await fetchEffectiveCalendar(workCenterId, year, month));
    } catch (e) {
      setError(e instanceof Error ? e.message : '달력 조회 실패');
    } finally {
      setLoading(false);
    }
  }, [workCenterId, year, month]);

  useEffect(() => {
    void load();
  }, [load]);

  const openDay = (date: string) => {
    const day = dayMap.get(date);
    if (!day) {
      return;
    }
    setSelectedDay(day);
    setWorkTime(day.effectiveWorkTime);
    setContent(day.content ?? '');
    setError(null);
  };

  const closeModal = () => setSelectedDay(null);

  const onSave = async () => {
    if (!selectedDay || workCenterId <= 0) {
      return;
    }
    setSubmitting(true);
    setError(null);
    try {
      await upsertWorkCenterCalendarOverride({
        workCenterId,
        calendarDate: selectedDay.calendarDate,
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

  const onClearOverride = async () => {
    if (!selectedDay?.isOverride || workCenterId <= 0) {
      return;
    }
    if (!window.confirm(`${selectedDay.calendarDate} Override를 해제하시겠습니까?`)) {
      return;
    }
    setSubmitting(true);
    setError(null);
    try {
      await deleteWorkCenterCalendarOverride(workCenterId, selectedDay.calendarDate);
      closeModal();
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '해제 실패');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>작업장별 생산달력</h1>
        <p>Override sparse — 기본달력 상속 + 예외만 저장</p>
      </header>

      {error && <div className="error">{error}</div>}

      <section className="panel calendar-split">
        <aside className="calendar-sidebar">
          <h2>작업장</h2>
          {workCenters.length === 0 ? (
            <p>등록된 작업장이 없습니다.</p>
          ) : (
            <ul className="wc-select-list">
              {workCenters.map((wc) => (
                <li key={wc.id}>
                  <button
                    type="button"
                    className={workCenterId === wc.id ? 'nav-active' : undefined}
                    onClick={() => setWorkCenterId(wc.id)}
                  >
                    {wc.wcName}
                  </button>
                </li>
              ))}
            </ul>
          )}
        </aside>
        <div className="calendar-main">
          <div className="panel-header-row">
            <h2>
              {selectedWorkCenterName || '작업장'} — {year}년 {month}월
            </h2>
            <GridExcelExportButton
              fileBaseName={`WC달력_${selectedWorkCenterName || workCenterId}_${year}${String(month).padStart(2, '0')}`}
              sheetName="WC달력"
              disabled={loading || workCenterId <= 0}
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
                const day = dayMap.get(cell.date);
                if (!day) {
                  return null;
                }
                return (
                  <button type="button" className="calendar-day-btn" onClick={() => openDay(cell.date)}>
                    <span className="calendar-day-num">{cell.day}</span>
                    <span
                      className={`calendar-day-label${day.effectiveWorkTime === 0 ? ' calendar-holiday' : ''}${day.isOverride ? ' calendar-override' : ''}`}
                    >
                      {formatWorkTimeLabel(day.effectiveWorkTime)}
                    </span>
                    {day.isOverride && <span className="calendar-day-badge">Override</span>}
                  </button>
                );
              }}
            />
          )}
        </div>
      </section>

      {selectedDay && (
        <div className="modal-backdrop" onClick={closeModal}>
          <div className="modal" onClick={(e) => e.stopPropagation()}>
            <h2>
              {selectedDay.calendarDate} — 작업장 Override
              {selectedDay.isOverride && <span className="calendar-day-badge"> Override</span>}
            </h2>
            <p className="hint">
              기본 effective: {formatWorkTimeLabel(selectedDay.effectiveWorkTime)}
              {selectedDay.baseWorkTime != null && ` (기본달력 ${selectedDay.baseWorkTime}분)`}
            </p>
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
              {selectedDay.isOverride && (
                <button type="button" className="danger" onClick={() => void onClearOverride()} disabled={submitting}>
                  예외 해제
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
