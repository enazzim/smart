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
import { useConfirm } from '../context/ConfirmContext';

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

function roundQty(value: number): number {
  return Math.round(value * 10_000) / 10_000;
}

export default function QualityInspectionPage() {
  const confirm = useConfirm();
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
  const [selectedIds, setSelectedIds] = useState<Set<number>>(new Set());
  const [batchCompletedDate, setBatchCompletedDate] = useState(todayIso());
  const { fiscalCutoverSetting } = useMaterialIssueSetting();
  const batchFiscalPeriod = useFiscalPeriod(batchCompletedDate, fiscalCutoverSetting);

  const [selected, setSelected] = useState<QualityInspection | null>(null);
  const [passedQty, setPassedQty] = useState('');
  const [failedQty, setFailedQty] = useState('');
  const [failureReason, setFailureReason] = useState('');
  const [lotNo, setLotNo] = useState('');
  const [autoGenerateLot, setAutoGenerateLot] = useState(false);
  const [completedDate, setCompletedDate] = useState(todayIso());
  const fiscalPeriod = useFiscalPeriod(completedDate, fiscalCutoverSetting);
  const [submitting, setSubmitting] = useState(false);
  const [batchSubmitting, setBatchSubmitting] = useState(false);
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
      setSelectedIds(new Set());
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
        불량사유: row.failureReason ?? '',
      })),
    [history, fiscalCutoverSetting],
  );

  const selectedRows = useMemo(
    () => pending.filter((row) => selectedIds.has(row.id)),
    [pending, selectedIds],
  );
  const allSelected = pending.length > 0 && selectedIds.size === pending.length;
  const someSelected = selectedIds.size > 0 && !allSelected;

  const toggleSelectAll = (checked: boolean) => {
    setSelectedIds(checked ? new Set(pending.map((row) => row.id)) : new Set());
  };

  const toggleRow = (id: number, checked: boolean) => {
    setSelectedIds((prev) => {
      const next = new Set(prev);
      if (checked) {
        next.add(id);
      } else {
        next.delete(id);
      }
      return next;
    });
  };

  const openRegister = (inspection: QualityInspection) => {
    setSelected(inspection);
    setPassedQty(String(inspection.requestQty));
    setFailedQty('0');
    setFailureReason('');
    setCompletedDate(todayIso());
    setLotNo('');
    setAutoGenerateLot(Boolean(inspection.sourceType === 'PURCHASE' && inspection.lotTracked));
    setError(null);
  };

  const closeModal = () => {
    setSelected(null);
    setPassedQty('');
    setFailedQty('');
    setFailureReason('');
    setLotNo('');
    setAutoGenerateLot(false);
  };

  const onPassedQtyChange = (value: string) => {
    setPassedQty(value);
    if (!selected) return;
    const passed = Number(value);
    if (!Number.isFinite(passed)) return;
    const failed = roundQty(selected.requestQty - passed);
    if (failed >= 0) {
      setFailedQty(String(failed));
      if (failed <= 0) {
        setFailureReason('');
      }
    }
  };

  const onFailedQtyChange = (value: string) => {
    setFailedQty(value);
    if (!selected) return;
    const failed = Number(value);
    if (!Number.isFinite(failed)) return;
    if (failed <= 0) {
      setFailureReason('');
    }
    const passed = roundQty(selected.requestQty - failed);
    if (passed >= 0) {
      setPassedQty(String(passed));
    }
  };

  const onRegister = async () => {
    if (!selected) return;
    const passed = Number(passedQty);
    const failed = Number(failedQty);
    if (Number.isNaN(passed) || Number.isNaN(failed)) {
      setError('합격·불량 수량을 입력해 주세요.');
      return;
    }
    const total = roundQty(passed + failed);
    const request = roundQty(selected.requestQty);
    if (total < request) {
      setError(`합격+불량은 의뢰수량(${formatQty(selected.requestQty)}) 이상이어야 합니다.`);
      return;
    }
    let allowOverQty = false;
    if (total > request) {
      const ok = await confirm(
        `합격·불량 합계(${formatQty(total)})가 의뢰수량(${formatQty(selected.requestQty)})보다 많습니다.\n그래도 등록하시겠습니까?\n(초과 합격분은 승인 시 지급금액에 반영됩니다.)`,
        { title: '의뢰수량 초과 확인', confirmLabel: '그래도 등록', cancelLabel: '닫기' },
      );
      if (!ok) {
        return;
      }
      allowOverQty = true;
    }
    if (failed > 0 && !failureReason.trim()) {
      setError('불량수량이 있으면 불량사유를 입력해 주세요.');
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
        failureReason: failureReason.trim() || undefined,
        completedDate,
        fiscalYear: fiscalPeriod.period.fiscalYear,
        fiscalMonth: fiscalPeriod.period.fiscalMonth,
        lotNo: lotNo.trim() || undefined,
        autoGenerateLot,
        allowOverQty,
      });
      setSuccess('검사 등록이 완료되었습니다. 합격 수량이 창고에 반영되었습니다.');
      closeModal();
      await loadPending();
      if (tab === 'history') {
        await loadHistory();
      }
    } catch (e) {
      setError(e instanceof Error ? e.message : '검사 등록에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const onBatchPass = async () => {
    if (selectedRows.length === 0) {
      setError('일괄 등록할 항목을 선택해 주세요.');
      return;
    }
    if (
      !(await confirm(
        `선택한 ${selectedRows.length}건을 의뢰수량 전량 합격으로 등록하시겠습니까?`,
        { title: '일괄 합격 등록', confirmLabel: '등록', cancelLabel: '닫기' },
      ))
    ) {
      return;
    }
    setBatchSubmitting(true);
    setError(null);
    setSuccess(null);
    let successCount = 0;
    const failures: string[] = [];
    for (const row of selectedRows) {
      try {
        await completeQualityInspection(row.id, {
          passedQty: row.requestQty,
          failedQty: 0,
          completedDate: batchCompletedDate,
          fiscalYear: batchFiscalPeriod.period.fiscalYear,
          fiscalMonth: batchFiscalPeriod.period.fiscalMonth,
          autoGenerateLot: row.sourceType === 'PURCHASE' && row.lotTracked,
        });
        successCount += 1;
      } catch (e) {
        failures.push(
          `${row.itemNum}: ${e instanceof Error ? e.message : '등록 실패'}`,
        );
      }
    }
    setBatchSubmitting(false);
    await loadPending();
    if (failures.length === 0) {
      setSuccess(`일괄 합격 등록 완료: ${successCount}건`);
    } else {
      setError(
        `성공 ${successCount}건 / 실패 ${failures.length}건\n${failures.slice(0, 5).join('\n')}${
          failures.length > 5 ? `\n…외 ${failures.length - 5}건` : ''
        }`,
      );
    }
  };

  const onCancelInspection = async (inspection: QualityInspection) => {
    if (
      !(await confirm(
        `완료된 검사를 취소하시겠습니까? 합격 수량(${formatQty(inspection.passedQty)})이 창고에서 차감되며, 해당 건은 검사 대기로 복귀합니다.`,
        { title: '검사대기로 복귀', confirmLabel: '예, 복귀', cancelLabel: '닫기', danger: true },
      ))
    ) {
      return;
    }
    setCancellingId(inspection.id);
    setError(null);
    setSuccess(null);
    try {
      await cancelQualityInspection(inspection.id);
      setSuccess('검사가 검사 대기로 복귀했습니다. 합격 수량은 창고에서 차감되었습니다.');
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
        <p>
          구매·외주 입고 검사품 대기 목록입니다. 의뢰수량을 합격·불량으로 나누어 검사 등록하면 합격분만
          창고에 반영됩니다. 이력에서 취소를 누르면 검사 대기로 복귀하며 합격분은 창고에서 차감됩니다.
        </p>
      </header>

      <div className="tab-row">
        <button type="button" className={tab === 'pending' ? 'tab-active' : undefined} onClick={() => setTab('pending')}>
          검사 대기
        </button>
        <button type="button" className={tab === 'history' ? 'tab-active' : undefined} onClick={() => setTab('history')}>
          검사 이력
        </button>
      </div>

      {error && <p className="error-banner" style={{ whiteSpace: 'pre-wrap' }}>{error}</p>}
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

          <section className="action-bar">
            <label>
              검사일
              <input
                type="date"
                value={batchCompletedDate}
                onChange={(e) => setBatchCompletedDate(e.target.value)}
              />
            </label>
            <FiscalPeriodDisplay
              baseDate={batchCompletedDate}
              period={batchFiscalPeriod.period}
              onPeriodChange={batchFiscalPeriod.onPeriodChange}
            />
            <button
              type="button"
              disabled={batchSubmitting || selectedRows.length === 0}
              onClick={() => void onBatchPass()}
            >
              {batchSubmitting
                ? '등록 중…'
                : `일괄 합격 등록 (${selectedRows.length}건)`}
            </button>
          </section>

          {loading ? (
            <p>불러오는 중…</p>
          ) : (
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>
                      <input
                        type="checkbox"
                        aria-label="전체 선택"
                        checked={allSelected}
                        disabled={pending.length === 0 || batchSubmitting}
                        ref={(el) => {
                          if (el) {
                            el.indeterminate = someSelected;
                          }
                        }}
                        onChange={(e) => toggleSelectAll(e.target.checked)}
                      />
                    </th>
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
                      <td colSpan={8}>대기 중인 검사가 없습니다.</td>
                    </tr>
                  ) : (
                    pending.map((row) => (
                      <tr key={row.id}>
                        <td>
                          <input
                            type="checkbox"
                            aria-label={`${row.itemNum} 선택`}
                            checked={selectedIds.has(row.id)}
                            disabled={batchSubmitting}
                            onChange={(e) => toggleRow(row.id, e.target.checked)}
                          />
                        </td>
                        <td>{row.receiptDate ?? '—'}</td>
                        <td>{sourceLabel(row.sourceType)}</td>
                        <td>{row.companyName}</td>
                        <td>{row.orderNo ?? '—'}</td>
                        <td>
                          {row.itemNum} {row.itemName}
                          {row.lotTracked ? ' · Lot' : ''}
                        </td>
                        <td className="num">{formatQty(row.requestQty)}</td>
                        <td className="row-actions">
                          <button type="button" onClick={() => openRegister(row)}>
                            검사등록
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
                    <th>불량사유</th>
                    <th />
                  </tr>
                </thead>
                <tbody>
                  {history.length === 0 ? (
                    <tr>
                      <td colSpan={12}>검사 이력이 없습니다.</td>
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
                        <td>{row.failureReason?.trim() ? row.failureReason : '—'}</td>
                        <td className="row-actions">
                          <button
                            type="button"
                            disabled={cancellingId === row.id}
                            onClick={() => void onCancelInspection(row)}
                          >
                            {cancellingId === row.id ? '복귀 중…' : '대기로 복귀'}
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

      {selected && (
        <div className="modal-backdrop" role="presentation" onClick={closeModal}>
          <div className="modal" role="dialog" onClick={(e) => e.stopPropagation()}>
            <h2>검사 등록</h2>
            <p>
              [{sourceLabel(selected.sourceType)}] {selected.itemNum} {selected.itemName}
            </p>
            <label>
              검사의뢰수량
              <input type="text" className="readonly" readOnly value={formatQty(selected.requestQty)} />
            </label>
            <label>
              합격 수량
              <input
                type="number"
                min={0}
                step="any"
                value={passedQty}
                onChange={(e) => onPassedQtyChange(e.target.value)}
              />
            </label>
            <label>
              불량 수량
              <input
                type="number"
                min={0}
                step="any"
                value={failedQty}
                onChange={(e) => onFailedQtyChange(e.target.value)}
              />
            </label>
            <label>
              불량사유 {Number(failedQty) > 0 ? '*' : ''}
              <textarea
                rows={3}
                value={failureReason}
                onChange={(e) => setFailureReason(e.target.value)}
                placeholder={Number(failedQty) > 0 ? '불량사유를 입력해 주세요' : '불량이 있을 때 입력'}
                disabled={!(Number(failedQty) > 0)}
              />
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
              검사일
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
              <button type="button" disabled={submitting} onClick={() => void onRegister()}>
                {submitting ? '등록 중…' : '등록'}
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
