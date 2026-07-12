import { useCallback, useEffect, useMemo, useState } from 'react';
import FiscalPeriodDisplay from '../components/FiscalPeriodDisplay';
import GridExcelExportButton from '../components/GridExcelExportButton';
import { useFiscalPeriod } from '../hooks/useFiscalPeriod';
import { formatFiscalPeriodFromIso } from '../utils/fiscalCalendar';
import { useMaterialIssueSetting } from '../context/MaterialIssueSettingContext';
import {
  cancelOutsourcingReceipt,
  createOutsourcingReceipt,
  fetchOutsourcingReceiptCandidates,
  fetchOutsourcingReceipts,
  type OutsourcingReceipt,
  type OutsourcingReceiptCandidate,
  type OutsourcingReceiptCandidateParams,
  type OutsourcingReceiptListParams,
} from '../api/outsourcingReceipt';
import { formatAmount, formatQty } from '../utils/numberFormat';

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

function checkLabel(value: string): string {
  return value === 'INSPECTION' ? '검사' : '무검사';
}

function receiptStatusLabel(status: string): string {
  switch (status) {
    case 'REGISTERED':
      return '등록';
    case 'PARTIALLY_POSTED':
      return '부분반영';
    case 'POSTED':
      return '반영완료';
    case 'CANCELLED':
      return '취소';
    default:
      return status;
  }
}

export default function OutsourcingReceiptPage() {
  const [candidates, setCandidates] = useState<OutsourcingReceiptCandidate[]>([]);
  const [receipts, setReceipts] = useState<OutsourcingReceipt[]>([]);
  const [receiptQtyByLineId, setReceiptQtyByLineId] = useState<Record<number, string>>({});
  const [selectedLineIds, setSelectedLineIds] = useState<Set<number>>(new Set());
  const [receiptDate, setReceiptDate] = useState(todayIso());
  const { fiscalCutoverSetting } = useMaterialIssueSetting();
  const fiscalPeriod = useFiscalPeriod(receiptDate, fiscalCutoverSetting);
  const [filters, setFilters] = useState<OutsourcingReceiptCandidateParams>(() => ({
    orderDateFrom: addDaysIso(todayIso(), -30),
    orderDateTo: todayIso(),
  }));
  const [historyFilters, setHistoryFilters] = useState<OutsourcingReceiptListParams>(() => ({
    receiptDateFrom: addDaysIso(todayIso(), -30),
    receiptDateTo: todayIso(),
  }));
  const [loading, setLoading] = useState(true);
  const [historyLoading, setHistoryLoading] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [cancellingId, setCancellingId] = useState<number | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [tab, setTab] = useState<'candidates' | 'history'>('candidates');

  const selectableCandidates = useMemo(
    () => candidates.filter((row) => row.remainQty > 0),
    [candidates],
  );

  const allSelected =
    selectableCandidates.length > 0 &&
    selectableCandidates.every((row) => selectedLineIds.has(row.outsourcingOrderLineId));

  const someSelected = selectableCandidates.some((row) => selectedLineIds.has(row.outsourcingOrderLineId));

  const loadCandidates = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await fetchOutsourcingReceiptCandidates(filters);
      setCandidates(data);
      setSelectedLineIds(new Set());
      setReceiptQtyByLineId({});
    } catch (e) {
      setError(e instanceof Error ? e.message : '미입고 목록을 불러오지 못했습니다.');
    } finally {
      setLoading(false);
    }
  }, [filters]);

  const loadReceipts = useCallback(async () => {
    setHistoryLoading(true);
    try {
      const data = await fetchOutsourcingReceipts(historyFilters);
      setReceipts(data);
    } catch (e) {
      setError(e instanceof Error ? e.message : '입고 이력을 불러오지 못했습니다.');
    } finally {
      setHistoryLoading(false);
    }
  }, [historyFilters]);

  useEffect(() => {
    void loadCandidates();
  }, [loadCandidates]);

  useEffect(() => {
    if (tab === 'history') {
      void loadReceipts();
    }
  }, [tab, loadReceipts]);

  const historyExportRows = useMemo(
    () =>
      receipts.flatMap((receipt) =>
        receipt.lines.map((line) => ({
          입고번호: receipt.receiptNo,
          거래처: receipt.partnerName,
          입고일: receipt.receiptDate,
          매입월: formatFiscalPeriodFromIso(receipt.receiptDate, fiscalCutoverSetting),
          상태: receiptStatusLabel(receipt.status),
          품목: `${line.itemNo} ${line.itemName}`,
          수량: formatQty(line.receiptQty),
          반영: line.stockPosted ? '완료' : line.qualityInspectionId ? '검사대기' : '미반영',
        })),
      ),
    [receipts, fiscalCutoverSetting],
  );

  const toggleLine = (row: OutsourcingReceiptCandidate, checked: boolean) => {
    setSelectedLineIds((prev) => {
      const next = new Set(prev);
      if (checked) {
        next.add(row.outsourcingOrderLineId);
      } else {
        next.delete(row.outsourcingOrderLineId);
      }
      return next;
    });
    setReceiptQtyByLineId((prev) => {
      if (!checked) {
        const next = { ...prev };
        delete next[row.outsourcingOrderLineId];
        return next;
      }
      return { ...prev, [row.outsourcingOrderLineId]: String(row.remainQty) };
    });
  };

  const toggleSelectAll = (checked: boolean) => {
    if (!checked) {
      setSelectedLineIds(new Set());
      setReceiptQtyByLineId({});
      return;
    }
    const ids = new Set<number>();
    const qtyMap: Record<number, string> = {};
    for (const row of selectableCandidates) {
      ids.add(row.outsourcingOrderLineId);
      qtyMap[row.outsourcingOrderLineId] = String(row.remainQty);
    }
    setSelectedLineIds(ids);
    setReceiptQtyByLineId(qtyMap);
  };

  const linesToSubmit = useMemo(() => {
    return candidates
      .filter((row) => selectedLineIds.has(row.outsourcingOrderLineId))
      .map((row) => {
        const raw = receiptQtyByLineId[row.outsourcingOrderLineId];
        const qty = raw ? Number(raw) : 0;
        return { row, qty };
      })
      .filter(({ qty }) => qty > 0);
  }, [candidates, receiptQtyByLineId, selectedLineIds]);

  const onSubmit = async () => {
    if (linesToSubmit.length === 0) {
      setError('입고할 라인을 선택하고 수량을 입력해 주세요.');
      return;
    }
    for (const { row, qty } of linesToSubmit) {
      if (qty > row.remainQty) {
        setError(`${row.itemNo} 입고 수량이 잔량(${formatQty(row.remainQty)})을 초과합니다.`);
        return;
      }
    }
    setSubmitting(true);
    setError(null);
    setSuccess(null);
    try {
      const result = await createOutsourcingReceipt({
        receiptDate,
        fiscalYear: fiscalPeriod.period.fiscalYear,
        fiscalMonth: fiscalPeriod.period.fiscalMonth,
        lines: linesToSubmit.map(({ row, qty }) => ({
          outsourcingOrderLineId: row.outsourcingOrderLineId,
          receiptQty: qty,
        })),
      });
      const partnerCount = new Set(linesToSubmit.map(({ row }) => row.partnerId)).size;
      const suffix = partnerCount > 1 ? ` (거래처 ${partnerCount}건 분할 등록)` : '';
      setSuccess(`입고 등록 완료: ${result.receiptNo}${suffix}`);
      setReceiptQtyByLineId({});
      setSelectedLineIds(new Set());
      await loadCandidates();
      await loadReceipts();
    } catch (e) {
      setError(e instanceof Error ? e.message : '입고 등록에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancelReceipt = async (receipt: OutsourcingReceipt) => {
    if (!window.confirm(`${receipt.receiptNo} 입고를 취소하시겠습니까?`)) {
      return;
    }
    setCancellingId(receipt.id);
    setError(null);
    setSuccess(null);
    try {
      await cancelOutsourcingReceipt(receipt.id);
      setSuccess(`입고 취소 완료: ${receipt.receiptNo}`);
      await loadCandidates();
      await loadReceipts();
    } catch (e) {
      setError(e instanceof Error ? e.message : '입고 취소에 실패했습니다.');
    } finally {
      setCancellingId(null);
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>외주입고</h1>
        <p>출고된 외주발주 라인에 대해 납품(입고)을 등록합니다. 무검사품은 즉시 공정창고에 반영됩니다.</p>
      </header>

      <div className="tab-row">
        <button type="button" className={tab === 'candidates' ? 'tab-active' : undefined} onClick={() => setTab('candidates')}>
          미입고 발주
        </button>
        <button type="button" className={tab === 'history' ? 'tab-active' : undefined} onClick={() => setTab('history')}>
          입고 이력
        </button>
      </div>

      {error && <p className="error-banner">{error}</p>}
      {success && <p className="success-banner">{success}</p>}

      {tab === 'candidates' && (
        <>
          <section className="filter-panel">
            <label>
              거래처
              <input
                value={filters.partnerName ?? ''}
                onChange={(e) => setFilters((f) => ({ ...f, partnerName: e.target.value }))}
              />
            </label>
            <label>
              발주번호
              <input
                value={filters.orderNo ?? ''}
                onChange={(e) => setFilters((f) => ({ ...f, orderNo: e.target.value }))}
              />
            </label>
            <label>
              발주일(부터)
              <input
                type="date"
                value={filters.orderDateFrom ?? ''}
                onChange={(e) => setFilters((f) => ({ ...f, orderDateFrom: e.target.value }))}
              />
            </label>
            <label>
              발주일(까지)
              <input
                type="date"
                value={filters.orderDateTo ?? ''}
                onChange={(e) => setFilters((f) => ({ ...f, orderDateTo: e.target.value }))}
              />
            </label>
            <label>
              품목번호
              <input
                value={filters.itemNo ?? ''}
                onChange={(e) => setFilters((f) => ({ ...f, itemNo: e.target.value }))}
              />
            </label>
            <button type="button" onClick={() => void loadCandidates()}>
              조회
            </button>
          </section>

          <section className="action-bar">
            <label>
              입고일
              <input type="date" value={receiptDate} onChange={(e) => setReceiptDate(e.target.value)} />
            </label>
            <FiscalPeriodDisplay
              baseDate={receiptDate}
              period={fiscalPeriod.period}
              onPeriodChange={fiscalPeriod.onPeriodChange}
            />
            <button type="button" disabled={submitting || linesToSubmit.length === 0} onClick={() => void onSubmit()}>
              {submitting ? '등록 중…' : `입고 등록 (${linesToSubmit.length}건)`}
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
                        disabled={selectableCandidates.length === 0 || submitting}
                        ref={(el) => {
                          if (el) {
                            el.indeterminate = !allSelected && someSelected;
                          }
                        }}
                        onChange={(e) => toggleSelectAll(e.target.checked)}
                      />
                    </th>
                    <th>발주번호</th>
                    <th>발주일</th>
                    <th>거래처</th>
                    <th>품목</th>
                    <th>공정</th>
                    <th>검사</th>
                    <th className="num">발주</th>
                    <th className="num">출고</th>
                    <th className="num">기입고</th>
                    <th className="num">검사대기</th>
                    <th className="num">입고잔량</th>
                    <th className="num">단가</th>
                    <th>입고수량</th>
                  </tr>
                </thead>
                <tbody>
                  {candidates.length === 0 ? (
                    <tr>
                      <td colSpan={14}>미입고 발주 라인이 없습니다.</td>
                    </tr>
                  ) : (
                    candidates.map((row) => {
                      const selected = selectedLineIds.has(row.outsourcingOrderLineId);
                      const disabled = row.remainQty <= 0;
                      return (
                        <tr key={row.outsourcingOrderLineId} className={disabled ? 'row-muted' : undefined}>
                          <td>
                            <input
                              type="checkbox"
                              checked={selected}
                              disabled={disabled || submitting}
                              onChange={(e) => toggleLine(row, e.target.checked)}
                            />
                          </td>
                          <td>{row.orderNo}</td>
                          <td>{row.orderDate}</td>
                          <td>{row.partnerName}</td>
                          <td>
                            {row.itemNo} {row.itemName}
                          </td>
                          <td>{row.processName}</td>
                          <td>{checkLabel(row.checkDistinction)}</td>
                          <td className="num">{formatQty(row.orderQty)}</td>
                          <td className="num">{formatQty(row.shippedQty)}</td>
                          <td className="num">{formatQty(row.receivedQty)}</td>
                          <td className="num">{formatQty(row.waitingInspectionQty)}</td>
                          <td className="num">{formatQty(row.remainQty)}</td>
                          <td className="num">{formatAmount(row.unitPrice)}</td>
                          <td>
                            <input
                              type="number"
                              min={0}
                              step="any"
                              disabled={!selected || submitting}
                              value={receiptQtyByLineId[row.outsourcingOrderLineId] ?? ''}
                              onChange={(e) =>
                                setReceiptQtyByLineId((prev) => ({
                                  ...prev,
                                  [row.outsourcingOrderLineId]: e.target.value,
                                }))
                              }
                            />
                          </td>
                        </tr>
                      );
                    })
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
              거래처
              <input
                value={historyFilters.partnerName ?? ''}
                onChange={(e) => setHistoryFilters((f) => ({ ...f, partnerName: e.target.value }))}
              />
            </label>
            <label>
              입고일(부터)
              <input
                type="date"
                value={historyFilters.receiptDateFrom ?? ''}
                onChange={(e) => setHistoryFilters((f) => ({ ...f, receiptDateFrom: e.target.value }))}
              />
            </label>
            <label>
              입고일(까지)
              <input
                type="date"
                value={historyFilters.receiptDateTo ?? ''}
                onChange={(e) => setHistoryFilters((f) => ({ ...f, receiptDateTo: e.target.value }))}
              />
            </label>
            <button type="button" onClick={() => void loadReceipts()}>
              조회
            </button>
          </section>

          <div className="panel-header-row">
            <h2>입고 이력</h2>
            <GridExcelExportButton
              fileBaseName="외주입고이력"
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
                    <th>입고번호</th>
                    <th>거래처</th>
                    <th>입고일</th>
                    <th>매입월</th>
                    <th>상태</th>
                    <th>품목</th>
                    <th className="num">수량</th>
                    <th>반영</th>
                    <th />
                  </tr>
                </thead>
                <tbody>
                  {receipts.length === 0 ? (
                    <tr>
                      <td colSpan={9}>입고 이력이 없습니다.</td>
                    </tr>
                  ) : (
                    receipts.flatMap((receipt) =>
                      receipt.lines.map((line, index) => (
                        <tr key={`${receipt.id}-${line.id}`}>
                          {index === 0 && (
                            <>
                              <td rowSpan={receipt.lines.length}>{receipt.receiptNo}</td>
                              <td rowSpan={receipt.lines.length}>{receipt.partnerName}</td>
                              <td rowSpan={receipt.lines.length}>{receipt.receiptDate}</td>
                              <td rowSpan={receipt.lines.length}>
                                {formatFiscalPeriodFromIso(receipt.receiptDate, fiscalCutoverSetting)}
                              </td>
                              <td rowSpan={receipt.lines.length}>{receiptStatusLabel(receipt.status)}</td>
                            </>
                          )}
                          <td>
                            {line.itemNo} {line.itemName}
                          </td>
                          <td className="num">{formatQty(line.receiptQty)}</td>
                          <td>{line.stockPosted ? '완료' : line.qualityInspectionId ? '검사대기' : '미반영'}</td>
                          {index === 0 && (
                            <td rowSpan={receipt.lines.length}>
                              {receipt.status !== 'CANCELLED' && (
                                <button
                                  type="button"
                                  disabled={cancellingId === receipt.id}
                                  onClick={() => void onCancelReceipt(receipt)}
                                >
                                  {cancellingId === receipt.id ? '취소 중…' : '취소'}
                                </button>
                              )}
                            </td>
                          )}
                        </tr>
                      )),
                    )
                  )}
                </tbody>
              </table>
            </div>
          )}
        </>
      )}
    </div>
  );
}
