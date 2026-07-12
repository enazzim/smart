import { useCallback, useEffect, useMemo, useState } from 'react';
import FiscalPeriodDisplay from '../components/FiscalPeriodDisplay';
import GridExcelExportButton from '../components/GridExcelExportButton';
import { useFiscalPeriod } from '../hooks/useFiscalPeriod';
import { formatFiscalPeriodFromInstant } from '../utils/fiscalCalendar';
import { useMaterialIssueSetting } from '../context/MaterialIssueSettingContext';
import {
  cancelQualityInspection,
  completeQualityInspection,
  fetchQualityInspections,
  type QualityInspection,
  type QualityInspectionListParams,
  type QualityInspectionSourceType,
} from '../api/qualityInspection';
import { formatQty } from '../utils/numberFormat';

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

function sourceLabel(value: QualityInspectionSourceType): string {
  return value === 'OUTSOURCE' ? '외주' : '구매';
}

function formatInstantDate(value?: string | null): string {
  if (!value) return '—';
  return value.slice(0, 10);
}

export default function QualityInspectionPage() {
  const [pending, setPending] = useState<QualityInspection[]>([]);
  const [history, setHistory] = useState<QualityInspection[]>([]);
  const [pendingFilters, setPendingFilters] = useState<QualityInspectionListParams>(() => ({
    status: 'PENDING',
    receiptDateFrom: addDaysIso(todayIso(), -30),
    receiptDateTo: todayIso(),
  }));
  const [historyFilters, setHistoryFilters] = useState<QualityInspectionListParams>(() => ({
    status: 'COMPLETED',
    completedDateFrom: addDaysIso(todayIso(), -30),
    completedDateTo: todayIso(),
  }));
  const [tab, setTab] = useState<'pending' | 'history'>('pending');
  const [loading, setLoading] = useState(true);
  const [historyLoading, setHistoryLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [selected, setSelected] = useState<QualityInspection | null>(null);
  const [passedQty, setPassedQty] = useState('');
  const [failedQty, setFailedQty] = useState('');
  const [lotNo, setLotNo] = useState('');
  const [autoGenerateLot, setAutoGenerateLot] = useState(false);
  const [completedDate, setCompletedDate] = useState(todayIso());
  const { fiscalCutoverSetting } = useMaterialIssueSetting();
  const fiscalPeriod = useFiscalPeriod(completedDate, fiscalCutoverSetting);
  const [submitting, setSubmitting] = useState(false);
  const [cancellingId, setCancellingId] = useState<number | null>(null);

  const loadPending = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await fetchQualityInspections({
        ...pendingFilters,
        status: 'PENDING',
        sourceType: pendingFilters.sourceType || undefined,
      });
      setPending(data);
    } catch (e) {
      setError(e instanceof Error ? e.message : '검사 대기 목록을 불러오지 못했습니다.');
    } finally {
      setLoading(false);
    }
  }, [pendingFilters]);

  const loadHistory = useCallback(async () => {
    setHistoryLoading(true);
    try {
      const data = await fetchQualityInspections({
        ...historyFilters,
        status: 'COMPLETED',
        sourceType: historyFilters.sourceType || undefined,
      });
      setHistory(data);
    } catch (e) {
      setError(e instanceof Error ? e.message : '검사 이력을 불러오지 못했습니다.');
    } finally {
      setHistoryLoading(false);
    }
  }, [historyFilters]);

  useEffect(() => {
    if (tab === 'pending') {
      void loadPending();
    }
  }, [tab, loadPending]);

  useEffect(() => {
    if (tab === 'history') {
      void loadHistory();
    }
  }, [tab, loadHistory]);

  const historyExportRows = useMemo(
    () =>
      history.map((row) => ({
        검사완료일: formatInstantDate(row.completedAt),
        매입월: formatFiscalPeriodFromInstant(row.completedAt, fiscalCutoverSetting),
        입고일: row.receiptDate ?? '',
        구분: sourceLabel(row.sourceType),
        거래처: row.companyName,
        발주번호: row.orderNo ?? '',
        품목: `${row.itemNum} ${row.itemName}`,
        의뢰: formatQty(row.requestQty),
        합격: formatQty(row.passedQty),
        불량: formatQty(row.failedQty),
      })),
    [history, fiscalCutoverSetting],
  );

  const openComplete = (inspection: QualityInspection) => {
    setSelected(inspection);
    setPassedQty(String(inspection.requestQty));
    setFailedQty('0');
    setCompletedDate(todayIso());
    setError(null);
  };

  const closeModal = () => {
    setSelected(null);
    setPassedQty('');
    setFailedQty('');
    setLotNo('');
    setAutoGenerateLot(false);
  };

  const onComplete = async () => {
    if (!selected) return;
    const passed = Number(passedQty);
    const failed = Number(failedQty);
    if (Number.isNaN(passed) || Number.isNaN(failed)) {
      setError('합격·불량 수량을 입력해 주세요.');
      return;
    }
    if (passed + failed !== selected.requestQty) {
      setError(`합격+불량은 의뢰수량(${formatQty(selected.requestQty)})과 같아야 합니다.`);
      return;
    }
    if (
      selected.sourceType === 'PURCHASE' &&
      selected.lotTracked &&
      passed > 0 &&
      !autoGenerateLot &&
      !lotNo.trim()
    ) {
      setError('Lot 추적 품목은 Lot 번호를 입력하거나 자동생성을 선택해 주세요.');
      return;
    }
    setSubmitting(true);
    setError(null);
    setSuccess(null);
    try {
      await completeQualityInspection(selected.id, {
        passedQty: passed,
        failedQty: failed,
        completedDate,
        fiscalYear: fiscalPeriod.period.fiscalYear,
        fiscalMonth: fiscalPeriod.period.fiscalMonth,
        lotNo: lotNo.trim() || undefined,
        autoGenerateLot,
      });
      setSuccess('검사 완료 처리되었습니다. 합격 수량이 창고에 반영되었습니다.');
      closeModal();
      await loadPending();
      if (tab === 'history') {
        await loadHistory();
      }
    } catch (e) {
      setError(e instanceof Error ? e.message : '검사 완료 처리에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancelInspection = async (inspection: QualityInspection) => {
    if (!window.confirm(`검사 완료 건을 취소하시겠습니까? 합격 수량(${formatQty(inspection.passedQty)})이 창고에서 차감됩니다.`)) {
      return;
    }
    setCancellingId(inspection.id);
    setError(null);
    setSuccess(null);
    try {
      await cancelQualityInspection(inspection.id);
      setSuccess('검사 취소되었습니다. 창고 수량이 차감되었습니다.');
      await loadHistory();
      await loadPending();
    } catch (e) {
      setError(e instanceof Error ? e.message : '검사 취소에 실패했습니다.');
    } finally {
      setCancellingId(null);
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>품질검사</h1>
        <p>구매·외주 입고 검사품 대기 목록입니다. 검사 완료 시 합격분만 창고에 반영되며, 취소 시 차감됩니다.</p>
      </header>

      <div className="tab-row">
        <button type="button" className={tab === 'pending' ? 'tab-active' : undefined} onClick={() => setTab('pending')}>
          검사 대기
        </button>
        <button type="button" className={tab === 'history' ? 'tab-active' : undefined} onClick={() => setTab('history')}>
          검사 이력
        </button>
      </div>

      {error && <p className="error-banner">{error}</p>}
      {success && <p className="success-banner">{success}</p>}

      {tab === 'pending' && (
        <>
          <section className="filter-panel">
            <label>
              구분
              <select
                value={pendingFilters.sourceType ?? ''}
                onChange={(e) =>
                  setPendingFilters((f) => ({
                    ...f,
                    sourceType: (e.target.value || '') as QualityInspectionSourceType | '',
                  }))
                }
              >
                <option value="">전체</option>
                <option value="PURCHASE">구매</option>
                <option value="OUTSOURCE">외주</option>
              </select>
            </label>
            <label>
              거래처
              <input
                value={pendingFilters.partnerName ?? ''}
                onChange={(e) => setPendingFilters((f) => ({ ...f, partnerName: e.target.value }))}
              />
            </label>
            <label>
              품목번호
              <input
                value={pendingFilters.itemNo ?? ''}
                onChange={(e) => setPendingFilters((f) => ({ ...f, itemNo: e.target.value }))}
              />
            </label>
            <label>
              품목명
              <input
                value={pendingFilters.itemName ?? ''}
                onChange={(e) => setPendingFilters((f) => ({ ...f, itemName: e.target.value }))}
              />
            </label>
            <label>
              입고일(부터)
              <input
                type="date"
                value={pendingFilters.receiptDateFrom ?? ''}
                onChange={(e) => setPendingFilters((f) => ({ ...f, receiptDateFrom: e.target.value }))}
              />
            </label>
            <label>
              입고일(까지)
              <input
                type="date"
                value={pendingFilters.receiptDateTo ?? ''}
                onChange={(e) => setPendingFilters((f) => ({ ...f, receiptDateTo: e.target.value }))}
              />
            </label>
            <button type="button" onClick={() => void loadPending()}>
              조회
            </button>
          </section>

          {loading ? (
            <p>불러오는 중…</p>
          ) : (
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>입고일</th>
                    <th>구분</th>
                    <th>거래처</th>
                    <th>발주번호</th>
                    <th>품목</th>
                    <th className="num">의뢰수량</th>
                    <th />
                  </tr>
                </thead>
                <tbody>
                  {pending.length === 0 ? (
                    <tr>
                      <td colSpan={7}>대기 중인 검사가 없습니다.</td>
                    </tr>
                  ) : (
                    pending.map((row) => (
                      <tr key={row.id}>
                        <td>{row.receiptDate ?? '—'}</td>
                        <td>{sourceLabel(row.sourceType)}</td>
                        <td>{row.companyName}</td>
                        <td>{row.orderNo ?? '—'}</td>
                        <td>
                          {row.itemNum} {row.itemName}
                        </td>
                        <td className="num">{formatQty(row.requestQty)}</td>
                        <td>
                          <button type="button" onClick={() => openComplete(row)}>
                            검사완료
                          </button>
                        </td>
                      </tr>
                    ))
                  )}
                </tbody>
              </table>
            </div>
          )}
        </>
      )}

      {tab === 'history' && (
        <>
          <section className="filter-panel">
            <label>
              구분
              <select
                value={historyFilters.sourceType ?? ''}
                onChange={(e) =>
                  setHistoryFilters((f) => ({
                    ...f,
                    sourceType: (e.target.value || '') as QualityInspectionSourceType | '',
                  }))
                }
              >
                <option value="">전체</option>
                <option value="PURCHASE">구매</option>
                <option value="OUTSOURCE">외주</option>
              </select>
            </label>
            <label>
              거래처
              <input
                value={historyFilters.partnerName ?? ''}
                onChange={(e) => setHistoryFilters((f) => ({ ...f, partnerName: e.target.value }))}
              />
            </label>
            <label>
              품목번호
              <input
                value={historyFilters.itemNo ?? ''}
                onChange={(e) => setHistoryFilters((f) => ({ ...f, itemNo: e.target.value }))}
              />
            </label>
            <label>
              검사완료일(부터)
              <input
                type="date"
                value={historyFilters.completedDateFrom ?? ''}
                onChange={(e) => setHistoryFilters((f) => ({ ...f, completedDateFrom: e.target.value }))}
              />
            </label>
            <label>
              검사완료일(까지)
              <input
                type="date"
                value={historyFilters.completedDateTo ?? ''}
                onChange={(e) => setHistoryFilters((f) => ({ ...f, completedDateTo: e.target.value }))}
              />
            </label>
            <button type="button" onClick={() => void loadHistory()}>
              조회
            </button>
          </section>

          <div className="panel-header-row">
            <h2>검사 이력</h2>
            <GridExcelExportButton
              fileBaseName="품질검사이력"
              disabled={historyLoading}
              rows={historyExportRows}
            />
          </div>

          {historyLoading ? (
            <p>불러오는 중…</p>
          ) : (
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>검사완료일</th>
                    <th>매입월</th>
                    <th>입고일</th>
                    <th>구분</th>
                    <th>거래처</th>
                    <th>발주번호</th>
                    <th>품목</th>
                    <th className="num">의뢰</th>
                    <th className="num">합격</th>
                    <th className="num">불량</th>
                    <th />
                  </tr>
                </thead>
                <tbody>
                  {history.length === 0 ? (
                    <tr>
                      <td colSpan={11}>검사 이력이 없습니다.</td>
                    </tr>
                  ) : (
                    history.map((row) => (
                      <tr key={row.id}>
                        <td>{formatInstantDate(row.completedAt)}</td>
                        <td>{formatFiscalPeriodFromInstant(row.completedAt, fiscalCutoverSetting)}</td>
                        <td>{row.receiptDate ?? '—'}</td>
                        <td>{sourceLabel(row.sourceType)}</td>
                        <td>{row.companyName}</td>
                        <td>{row.orderNo ?? '—'}</td>
                        <td>
                          {row.itemNum} {row.itemName}
                        </td>
                        <td className="num">{formatQty(row.requestQty)}</td>
                        <td className="num">{formatQty(row.passedQty)}</td>
                        <td className="num">{formatQty(row.failedQty)}</td>
                        <td>
                          {row.passedQty > 0 && (
                            <button
                              type="button"
                              disabled={cancellingId === row.id}
                              onClick={() => void onCancelInspection(row)}
                            >
                              {cancellingId === row.id ? '취소 중…' : '취소'}
                            </button>
                          )}
                        </td>
                      </tr>
                    ))
                  )}
                </tbody>
              </table>
            </div>
          )}
        </>
      )}

      {selected && (
        <div className="modal-backdrop" role="presentation" onClick={closeModal}>
          <div className="modal" role="dialog" onClick={(e) => e.stopPropagation()}>
            <h2>검사 완료</h2>
            <p>
              [{sourceLabel(selected.sourceType)}] {selected.itemNum} {selected.itemName} — 의뢰{' '}
              {formatQty(selected.requestQty)}
            </p>
            <label>
              합격 수량
              <input type="number" min={0} step="any" value={passedQty} onChange={(e) => setPassedQty(e.target.value)} />
            </label>
            <label>
              불량 수량
              <input type="number" min={0} step="any" value={failedQty} onChange={(e) => setFailedQty(e.target.value)} />
            </label>
            {selected.sourceType === 'PURCHASE' && selected.lotTracked && (
              <>
                <label className="checkbox">
                  <input
                    type="checkbox"
                    checked={autoGenerateLot}
                    onChange={(e) => setAutoGenerateLot(e.target.checked)}
                  />
                  Lot 자동생성
                </label>
                <label>
                  Lot 번호
                  <input
                    value={lotNo}
                    disabled={autoGenerateLot}
                    onChange={(e) => setLotNo(e.target.value)}
                    placeholder="수동 입력 시"
                  />
                </label>
              </>
            )}
            <label>
              검사완료일
              <input type="date" value={completedDate} onChange={(e) => setCompletedDate(e.target.value)} />
            </label>
            <FiscalPeriodDisplay
              baseDate={completedDate}
              period={fiscalPeriod.period}
              onPeriodChange={fiscalPeriod.onPeriodChange}
            />
            <div className="modal-actions">
              <button type="button" className="secondary" onClick={closeModal} disabled={submitting}>
                닫기
              </button>
              <button type="button" disabled={submitting} onClick={() => void onComplete()}>
                {submitting ? '처리 중…' : '완료'}
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
