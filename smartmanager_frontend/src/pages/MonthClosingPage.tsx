import { useEffect, useState } from 'react';
import type { CloseMonthClosingRequest, FiscalPeriodStatus, MonthClosing } from '../api/monthClosing';
import {
  closeMonthClosing,
  fetchFiscalPeriodStatus,
  fetchMonthClosings,
  reopenMonthClosing,
} from '../api/monthClosing';

const MONTH_OPTIONS = Array.from({ length: 12 }, (_, index) => index + 1);

function formatPeriod(year: number, month: number): string {
  return `${year}-${String(month).padStart(2, '0')}`;
}

function formatDateTime(value: string): string {
  return new Date(value).toLocaleString('ko-KR');
}

function isLatestClosing(row: MonthClosing, rows: MonthClosing[]): boolean {
  if (rows.length === 0) {
    return false;
  }
  const latest = rows[0];
  return row.fiscalYear === latest.fiscalYear && row.fiscalMonth === latest.fiscalMonth;
}

export default function MonthClosingPage() {
  const currentYear = new Date().getFullYear();
  const [rows, setRows] = useState<MonthClosing[]>([]);
  const [status, setStatus] = useState<FiscalPeriodStatus | null>(null);
  const [form, setForm] = useState<CloseMonthClosingRequest>({
    fiscalYear: currentYear,
    fiscalMonth: new Date().getMonth() + 1,
  });
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [reopeningKey, setReopeningKey] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const load = async () => {
    setLoading(true);
    setError(null);
    try {
      const [closingRows, periodStatus] = await Promise.all([
        fetchMonthClosings(),
        fetchFiscalPeriodStatus(),
      ]);
      setRows(closingRows);
      setStatus(periodStatus);
    } catch (e) {
      setError(e instanceof Error ? e.message : '월마감 정보 조회 실패');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    void load();
  }, []);

  const onSubmit = async () => {
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      await closeMonthClosing(form);
      setMessage(`${formatPeriod(form.fiscalYear, form.fiscalMonth)} 회계월 마감이 완료되었습니다.`);
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '월마감 처리 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onReopen = async (row: MonthClosing) => {
    const key = `${row.fiscalYear}-${row.fiscalMonth}`;
    if (!window.confirm(`${formatPeriod(row.fiscalYear, row.fiscalMonth)} 회계월 마감을 해제하시겠습니까?`)) {
      return;
    }
    setReopeningKey(key);
    setError(null);
    setMessage(null);
    try {
      await reopenMonthClosing(row.fiscalYear, row.fiscalMonth);
      setMessage(`${formatPeriod(row.fiscalYear, row.fiscalMonth)} 회계월 마감이 해제되었습니다.`);
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '마감해제 처리 실패');
    } finally {
      setReopeningKey(null);
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>월마감</h1>
        <p>
          회계월 25일 규칙(1~25일 → 해당 월, 26일~ → 다음 월)으로 거래일을 판정하고, 마감된 회계월에는
          트랜잭션 등록·수정을 차단합니다. 마감해제는 가장 최근 마감 회계월만 가능합니다.
        </p>
      </header>

      {status && (
        <section className="panel">
          <h2>오늘 기준 회계월</h2>
          <p>
            {status.referenceDate} →{' '}
            <strong>{formatPeriod(status.fiscalYear, status.fiscalMonth)}</strong>
            {status.closed ? ' (마감됨)' : ' (미마감)'}
          </p>
        </section>
      )}

      <section className="panel">
        <h2>회계월 마감</h2>
        <div className="action-bar">
          <label>
            회계연도
            <input
              type="number"
              min={2000}
              max={2100}
              value={form.fiscalYear}
              onChange={(e) => setForm({ ...form, fiscalYear: Number(e.target.value) })}
            />
          </label>
          <label>
            회계월
            <select
              value={form.fiscalMonth}
              onChange={(e) => setForm({ ...form, fiscalMonth: Number(e.target.value) })}
            >
              {MONTH_OPTIONS.map((month) => (
                <option key={month} value={month}>
                  {month}월
                </option>
              ))}
            </select>
          </label>
          <button type="button" onClick={() => void onSubmit()} disabled={submitting}>
            {submitting ? '처리 중…' : '마감 실행'}
          </button>
        </div>
        {message && <p>{message}</p>}
        {error && <div className="error">{error}</div>}
      </section>

      <section className="panel">
        <h2>마감 이력</h2>
        {loading ? (
          <p>불러오는 중…</p>
        ) : rows.length === 0 ? (
          <p>등록된 마감 이력이 없습니다.</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>회계연월</th>
                <th>마감일시</th>
                <th>마감자</th>
                <th>관리</th>
              </tr>
            </thead>
            <tbody>
              {rows.map((row) => {
                const rowKey = `${row.fiscalYear}-${row.fiscalMonth}`;
                const canReopen = isLatestClosing(row, rows);
                return (
                  <tr key={row.id}>
                    <td>{formatPeriod(row.fiscalYear, row.fiscalMonth)}</td>
                    <td>{formatDateTime(row.closedAt)}</td>
                    <td>{row.closedBy}</td>
                    <td>
                      {canReopen ? (
                        <button
                          type="button"
                          className="btn-action danger"
                          disabled={reopeningKey === rowKey}
                          onClick={() => void onReopen(row)}
                        >
                          {reopeningKey === rowKey ? '처리 중…' : '마감해제'}
                        </button>
                      ) : (
                        '—'
                      )}
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        )}
      </section>
    </div>
  );
}
