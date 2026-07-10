import { useCallback, useEffect, useMemo, useState } from 'react';
import type { WorkCenter } from '../api/workCenter';
import { fetchWorkCenters } from '../api/workCenter';
import {
  fetchWorkCenterLoad,
  type WorkCenterLoadDay,
  type WorkCenterLoadDetail,
} from '../api/workCenterLoad';
import { formatInteger, formatQty } from '../utils/numberFormat';

function formatPct(rate: number | null): string {
  if (rate == null) {
    return '—';
  }
  return `${(rate * 100).toFixed(1)}%`;
}

function defaultDateRange(): { from: string; to: string } {
  const now = new Date();
  const year = now.getFullYear();
  const month = now.getMonth();
  const from = new Date(year, month, 1);
  const to = new Date(year, month + 1, 0);
  const fmt = (d: Date) => d.toISOString().slice(0, 10);
  return { from: fmt(from), to: fmt(to) };
}

export default function WorkCenterLoadPage() {
  const initialRange = useMemo(() => defaultDateRange(), []);
  const [workCenters, setWorkCenters] = useState<WorkCenter[]>([]);
  const [workCenterId, setWorkCenterId] = useState<number>(0);
  const [from, setFrom] = useState(initialRange.from);
  const [to, setTo] = useState(initialRange.to);
  const [days, setDays] = useState<WorkCenterLoadDay[]>([]);
  const [warnThreshold, setWarnThreshold] = useState(0.8);
  const [selectedDay, setSelectedDay] = useState<WorkCenterLoadDay | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

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
    if (!from || !to) {
      return;
    }
    setLoading(true);
    setError(null);
    try {
      const result = await fetchWorkCenterLoad({
        workCenterId: workCenterId > 0 ? workCenterId : undefined,
        from,
        to,
      });
      setDays(result.days);
      setWarnThreshold(result.warnLoadThreshold);
    } catch (e) {
      setError(e instanceof Error ? e.message : '부하 조회 실패');
      setDays([]);
    } finally {
      setLoading(false);
    }
  }, [workCenterId, from, to]);

  useEffect(() => {
    if (workCenterId > 0) {
      void load();
    }
  }, [load, workCenterId]);

  const openDetail = (day: WorkCenterLoadDay) => {
    if (day.workPlanCount === 0) {
      return;
    }
    setSelectedDay(day);
  };

  const closeModal = () => setSelectedDay(null);

  const thresholdLabel = `${(warnThreshold * 100).toFixed(0)}%`;

  return (
    <div className="page">
      <header className="page-header">
        <h1>작업장 부하</h1>
        <p>
          작업계획 소요시간과 작업장 Capa를 비교합니다. 부하율이 {thresholdLabel} 이상이면 경고로 표시됩니다.
        </p>
      </header>

      {error && <p className="error-banner">{error}</p>}

      <section className="filter-panel">
        <label>
          작업장
          <select
            value={workCenterId}
            onChange={(e) => setWorkCenterId(Number(e.target.value))}
            disabled={loading || workCenters.length === 0}
          >
            {workCenters.length === 0 ? (
              <option value={0}>작업장 없음</option>
            ) : (
              workCenters.map((wc) => (
                <option key={wc.id} value={wc.id}>
                  {wc.wcName}
                </option>
              ))
            )}
          </select>
        </label>
        <label>
          시작일
          <input type="date" value={from} onChange={(e) => setFrom(e.target.value)} disabled={loading} />
        </label>
        <label>
          종료일
          <input type="date" value={to} onChange={(e) => setTo(e.target.value)} disabled={loading} />
        </label>
        <button type="button" disabled={loading || workCenterId <= 0} onClick={() => void load()}>
          {loading ? '조회 중…' : '조회'}
        </button>
      </section>

      {loading ? (
        <p>불러오는 중…</p>
      ) : days.length === 0 ? (
        <p>표시할 부하 데이터가 없습니다.</p>
      ) : (
        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>일자</th>
                <th>작업장</th>
                <th className="num">소요(분)</th>
                <th className="num">Capa(분)</th>
                <th>부하율</th>
                <th className="num">계획건수</th>
                <th>상태</th>
              </tr>
            </thead>
            <tbody>
              {days.map((day) => (
                <tr
                  key={`${day.workCenterId}-${day.date}`}
                  style={day.workPlanCount > 0 ? { cursor: 'pointer' } : undefined}
                  onClick={() => openDetail(day)}
                >
                  <td>{day.date}</td>
                  <td>{day.workCenterName}</td>
                  <td className="num">{formatInteger(day.demandMinutes)}</td>
                  <td className="num">{formatInteger(day.capaMinutes)}</td>
                  <td style={day.overThreshold ? { color: '#b45309', fontWeight: 600 } : undefined}>
                    {formatPct(day.loadRate)}
                  </td>
                  <td className="num">{formatInteger(day.workPlanCount)}</td>
                  <td>
                    {day.overThreshold ? (
                      <span style={{ color: '#b45309', fontWeight: 600 }}>경고</span>
                    ) : day.workPlanCount > 0 ? (
                      '정상'
                    ) : (
                      '—'
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      {selectedDay && (
        <div className="modal-backdrop" role="presentation" onClick={closeModal}>
          <div className="modal" role="dialog" aria-labelledby="load-detail-title" onClick={(e) => e.stopPropagation()}>
            <h2 id="load-detail-title">
              {selectedDay.date} · {selectedDay.workCenterName}
            </h2>
            <p>
              소요 {formatInteger(selectedDay.demandMinutes)}분 / Capa {formatInteger(selectedDay.capaMinutes)}분 · 부하율{' '}
              {formatPct(selectedDay.loadRate)}
            </p>
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>계획번호</th>
                    <th>품목</th>
                    <th>공정</th>
                    <th className="num">계획수량</th>
                    <th className="num">소요(분)</th>
                  </tr>
                </thead>
                <tbody>
                  {selectedDay.details.map((row: WorkCenterLoadDetail) => (
                    <tr key={row.workPlanId}>
                      <td>{row.planNo}</td>
                      <td>
                        {row.itemNo} {row.itemName}
                      </td>
                      <td>{row.processName}</td>
                      <td className="num">{formatQty(row.plannedQty)}</td>
                      <td className="num">{formatInteger(row.demandMinutes)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
            <div className="modal-actions">
              <button type="button" onClick={closeModal}>
                닫기
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
