import { useCallback, useEffect, useMemo, useState } from 'react';
import FiscalPeriodDisplay from '../components/FiscalPeriodDisplay';
import GridExcelExportButton from '../components/GridExcelExportButton';
import { useFiscalPeriod } from '../hooks/useFiscalPeriod';
import { formatFiscalPeriodFromIso } from '../utils/fiscalCalendar';
import { useMaterialIssueSetting } from '../context/MaterialIssueSettingContext';
import {
  cancelPurchaseReceipt,
  createPurchaseReceipt,
  fetchPurchaseReceiptCandidates,
  fetchPurchaseReceipts,
  type PurchaseReceipt,
  type PurchaseReceiptCandidate,
  type PurchaseReceiptCandidateParams,
  type PurchaseReceiptListParams,
} from '../api/purchaseReceipt';
import { formatAmount, formatQty } from '../utils/numberFormat';
import { useConfirm } from '../context/ConfirmContext';

type ReceiptTab = 'candidates' | 'history' | 'subCandidates' | 'subHistory';

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
export default function PurchaseReceiptPage() {
  const confirm = useConfirm();
  const [candidates, setCandidates] = useState<PurchaseReceiptCandidate[]>([]);
  const [receipts, setReceipts] = useState<PurchaseReceipt[]>([]);
  const [receiptQtyByLineId, setReceiptQtyByLineId] = useState<Record<number, string>>({});
  const [lotNoByLineId, setLotNoByLineId] = useState<Record<number, string>>({});
  const [autoLotByLineId, setAutoLotByLineId] = useState<Record<number, boolean>>({});
  const [selectedLineIds, setSelectedLineIds] = useState<Set<number>>(new Set());
  const [receiptDate, setReceiptDate] = useState(todayIso());
  const { fiscalCutoverSetting } = useMaterialIssueSetting();
  const fiscalPeriod = useFiscalPeriod(receiptDate, fiscalCutoverSetting);
  const [filters, setFilters] = useState<PurchaseReceiptCandidateParams>(() => ({
    orderDateFrom: addDaysIso(todayIso(), -30),
    orderDateTo: todayIso(),
  }));
  const [historyFilters, setHistoryFilters] = useState<PurchaseReceiptListParams>(() => ({
    receiptDateFrom: addDaysIso(todayIso(), -30),
    receiptDateTo: todayIso(),
  }));
  const [loading, setLoading] = useState(true);
  const [historyLoading, setHistoryLoading] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [cancellingId, setCancellingId] = useState<number | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [tab, setTab] = useState<ReceiptTab>('candidates');
  const itemPropertyScope: 'GENERAL' | 'SUB_MATERIAL' =
    tab === 'subCandidates' || tab === 'subHistory' ? 'SUB_MATERIAL' : 'GENERAL';
  const isCandidateTab = tab === 'candidates' || tab === 'subCandidates';
  const isHistoryTab = tab === 'history' || tab === 'subHistory';
  const isSubMaterialTab = itemPropertyScope === 'SUB_MATERIAL';
  const selectableCandidates = useMemo(
    () => candidates.filter((row) => row.remainQty > 0),
    [candidates],
  );
  const historyExportRows = useMemo(
    () =>
      receipts.map((receipt) => ({
        입고번호: receipt.receiptNo,
        입고일: receipt.receiptDate,
        매입월: formatFiscalPeriodFromIso(receipt.receiptDate, fiscalCutoverSetting),
        거래처: receipt.partnerName,
        상태: receiptStatusLabel(receipt.status),
        품목수량: receipt.lines.map((line) => `${line.itemNum} × ${line.receiptQty}`).join(' / '),
      })),
    [receipts, fiscalCutoverSetting],
  );
  const allSelected =
    selectableCandidates.length > 0 &&
    selectableCandidates.every((row) => selectedLineIds.has(row.purchaseOrderLineId));
  const someSelected = selectableCandidates.some((row) => selectedLineIds.has(row.purchaseOrderLineId));
  const loadCandidates = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await fetchPurchaseReceiptCandidates({ ...filters, itemPropertyScope });
      setCandidates(data);
      setSelectedLineIds(new Set());
      setReceiptQtyByLineId({});
      setLotNoByLineId({});
      setAutoLotByLineId({});
    } catch (e) {
      setError(e instanceof Error ? e.message : '미입고 목록을 불러오지 못했습니다.');
    } finally {
      setLoading(false);
    }
  }, [filters, itemPropertyScope]);
  const loadReceipts = useCallback(async () => {
    setHistoryLoading(true);
    try {
      const data = await fetchPurchaseReceipts({ ...historyFilters, itemPropertyScope });
      setReceipts(data);
    } catch (e) {
      setError(e instanceof Error ? e.message : '입고 이력을 불러오지 못했습니다.');
    } finally {
      setHistoryLoading(false);
    }
  }, [historyFilters, itemPropertyScope]);
  useEffect(() => {
    if (isCandidateTab) {
      void loadCandidates();
    }
  }, [isCandidateTab, loadCandidates]);
  useEffect(() => {
    if (isHistoryTab) {
      void loadReceipts();
    }
  }, [isHistoryTab, loadReceipts]);
  const toggleLine = (row: PurchaseReceiptCandidate, checked: boolean) => {
    setSelectedLineIds((prev) => {
      const next = new Set(prev);
      if (checked) {
        next.add(row.purchaseOrderLineId);
      } else {
        next.delete(row.purchaseOrderLineId);
      }
      return next;
    });
    setReceiptQtyByLineId((prev) => {
      const next = { ...prev };
      if (checked) {
        next[row.purchaseOrderLineId] = String(row.remainQty);
      } else {
        delete next[row.purchaseOrderLineId];
      }
      return next;
    });
    if (!checked) {
      setLotNoByLineId((prev) => {
        const next = { ...prev };
        delete next[row.purchaseOrderLineId];
        return next;
      });
      setAutoLotByLineId((prev) => {
        const next = { ...prev };
        delete next[row.purchaseOrderLineId];
        return next;
      });
    }
  };
  const toggleSelectAll = (checked: boolean) => {
    if (!checked) {
      setSelectedLineIds(new Set());
      setReceiptQtyByLineId({});
      setLotNoByLineId({});
      setAutoLotByLineId({});
      return;
    }
    const ids = new Set<number>();
    const qtyMap: Record<number, string> = {};
    for (const row of selectableCandidates) {
      ids.add(row.purchaseOrderLineId);
      qtyMap[row.purchaseOrderLineId] = String(row.remainQty);
    }
    setSelectedLineIds(ids);
    setReceiptQtyByLineId(qtyMap);
  };
  const linesToSubmit = useMemo(() => {
    return candidates
      .filter((row) => selectedLineIds.has(row.purchaseOrderLineId))
      .map((row) => {
        const raw = receiptQtyByLineId[row.purchaseOrderLineId];
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
    for (const { row } of linesToSubmit) {
      if (
        row.lotTracked &&
        row.checkDistinction === 'NONE' &&
        !autoLotByLineId[row.purchaseOrderLineId] &&
        !(lotNoByLineId[row.purchaseOrderLineId] ?? '').trim()
      ) {
        setError(`${row.itemNum} Lot 번호를 입력하거나 자동생성을 선택해 주세요.`);
        return;
      }
    }
    const overLines = linesToSubmit.filter(({ row, qty }) => qty > row.remainQty);
    let allowOverQty = false;
    if (overLines.length > 0) {
      const detail = overLines
        .map(
          ({ row, qty }) =>
            `· ${row.itemNum}: 입고 ${formatQty(qty)} / 잔량 ${formatQty(row.remainQty)}`,
        )
        .join('\n');
      const ok = await confirm(
        `잔량보다 많은 수량이 있습니다.\n\n${detail}\n\n그래도 등록하시겠습니까?\n(초과분은 승인 시 지급금액에 반영됩니다.)`,
        { title: '잔량 초과 확인', confirmLabel: '그래도 등록', cancelLabel: '닫기' },
      );
      if (!ok) {
        return;
      }
      allowOverQty = true;
    }
    setSubmitting(true);
    setError(null);
    setSuccess(null);
    try {
      const result = await createPurchaseReceipt({
        receiptDate,
        fiscalYear: fiscalPeriod.period.fiscalYear,
        fiscalMonth: fiscalPeriod.period.fiscalMonth,
        allowOverQty,
        lines: linesToSubmit.map(({ row, qty }) => ({
          purchaseOrderLineId: row.purchaseOrderLineId,
          receiptQty: qty,
          lotNo: lotNoByLineId[row.purchaseOrderLineId]?.trim() || undefined,
          autoGenerateLot: Boolean(autoLotByLineId[row.purchaseOrderLineId]),
        })),
      });
      const partnerCount = new Set(linesToSubmit.map(({ row }) => row.partnerId)).size;
      const suffix = partnerCount > 1 ? ` (거래처 ${partnerCount}건 분할 등록)` : '';
      setSuccess(`입고 등록 완료: ${result.receiptNo}${suffix}`);
      setReceiptQtyByLineId({});
      setLotNoByLineId({});
      setAutoLotByLineId({});
      setSelectedLineIds(new Set());
      await loadCandidates();
      await loadReceipts();
    } catch (e) {
      setError(e instanceof Error ? e.message : '입고 등록에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };
  const onCancelReceipt = async (receipt: PurchaseReceipt) => {
    if (!(await confirm(
      `${receipt.receiptNo} 입고를 취소하시겠습니까?\n품질검사가 완료된 입고는 취소할 수 없습니다. (검사 대기로 복귀 후 취소)`,
      { title: '취소 확인', confirmLabel: '예, 취소', cancelLabel: '닫기', danger: true },
    ))) {
      return;
    }
    setCancellingId(receipt.id);
    setError(null);
    setSuccess(null);
    try {
      await cancelPurchaseReceipt(receipt.id);
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
        <h1>구매입고</h1>
        <p>
          {isSubMaterialTab
            ? '부자재 발주 잔량을 입고 등록합니다. 부자재는 창고 재고 반영 없이 매입이력만 기록합니다.'
            : '미입고 발주 라인에 대해 입고 등록합니다. 무검사품은 즉시 창고에 반영됩니다.'}
        </p>
      </header>
      <div className="tab-row">
        <button type="button" className={tab === 'candidates' ? 'tab-active' : undefined} onClick={() => setTab('candidates')}>
          미입고 발주
        </button>
        <button type="button" className={tab === 'history' ? 'tab-active' : undefined} onClick={() => setTab('history')}>
          입고 이력
        </button>
        <button
          type="button"
          className={tab === 'subCandidates' ? 'tab-active' : undefined}
          onClick={() => setTab('subCandidates')}
        >
          부자재입고
        </button>
        <button
          type="button"
          className={tab === 'subHistory' ? 'tab-active' : undefined}
          onClick={() => setTab('subHistory')}
        >
          부자재입고이력
        </button>
      </div>
      {error && <p className="error-banner">{error}</p>}
      {success && <p className="success-banner">{success}</p>}
      {isCandidateTab && (
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
                value={filters.itemNum ?? ''}
                onChange={(e) => setFilters((f) => ({ ...f, itemNum: e.target.value }))}
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
              {submitting
                ? '등록 중…'
                : `${isSubMaterialTab ? '부자재 ' : ''}입고 등록 (${linesToSubmit.length}건)`}
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
                    <th>검사</th>
                    <th className="num">발주</th>
                    <th className="num">기입고</th>
                    <th className="num">검사대기</th>
                    <th className="num">잔량</th>
                    <th>입고수량</th>
                    <th>Lot</th>
                    <th className="num">단가</th>
                  </tr>
                </thead>
                <tbody>
                  {candidates.length === 0 ? (
                    <tr>
                      <td colSpan={13}>미입고 발주 라인이 없습니다.</td>
                    </tr>
                  ) : (
                    candidates.map((row) => {
                      const selected = selectedLineIds.has(row.purchaseOrderLineId);
                      const disabled = row.remainQty <= 0;
                      const needLot = row.lotTracked && row.checkDistinction === 'NONE';
                      const autoLot = Boolean(autoLotByLineId[row.purchaseOrderLineId]);
                      return (
                        <tr key={row.purchaseOrderLineId} className={disabled ? 'row-muted' : undefined}>
                          <td>
                            <input
                              type="checkbox"
                              aria-label={`${row.itemNum} 선택`}
                              checked={selected}
                              disabled={disabled || submitting}
                              onChange={(e) => toggleLine(row, e.target.checked)}
                            />
                          </td>
                          <td>{row.orderNo}</td>
                          <td>{row.orderDate}</td>
                          <td>{row.partnerName}</td>
                          <td>
                            {row.itemNum} {row.itemName}
                            {row.lotTracked ? ' · Lot' : ''}
                          </td>
                          <td>
                            <span className={row.checkDistinction === 'INSPECTION' ? 'badge-warn' : 'badge-ok'}>
                              {checkLabel(row.checkDistinction)}
                            </span>
                          </td>
                          <td className="num">{formatQty(row.orderQty)}</td>
                          <td className="num">{formatQty(row.receivedQty)}</td>
                          <td className="num">{formatQty(row.waitingInspectionQty)}</td>
                          <td className="num">{formatQty(row.remainQty)}</td>
                          <td>
                            <input
                              type="number"
                              min={0}
                              step="any"
                              disabled={!selected || submitting}
                              value={receiptQtyByLineId[row.purchaseOrderLineId] ?? ''}
                              onChange={(e) =>
                                setReceiptQtyByLineId((prev) => ({
                                  ...prev,
                                  [row.purchaseOrderLineId]: e.target.value,
                                }))
                              }
                            />
                          </td>
                          <td>
                            {needLot ? (
                              <div className="lot-input-cell">
                                <label className="checkbox">
                                  <input
                                    type="checkbox"
                                    disabled={!selected || submitting}
                                    checked={autoLot}
                                    onChange={(e) =>
                                      setAutoLotByLineId((prev) => ({
                                        ...prev,
                                        [row.purchaseOrderLineId]: e.target.checked,
                                      }))
                                    }
                                  />
                                  자동
                                </label>
                                <input
                                  placeholder="Lot 번호"
                                  disabled={!selected || submitting || autoLot}
                                  value={lotNoByLineId[row.purchaseOrderLineId] ?? ''}
                                  onChange={(e) =>
                                    setLotNoByLineId((prev) => ({
                                      ...prev,
                                      [row.purchaseOrderLineId]: e.target.value,
                                    }))
                                  }
                                />
                              </div>
                            ) : row.lotTracked ? (
                              <span className="muted">검사완료 시 입력</span>
                            ) : (
                              '—'
                            )}
                          </td>
                          <td className="num">{formatAmount(row.unitPrice)}</td>
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
      {isHistoryTab && (
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
              품목번호
              <input
                value={historyFilters.itemNum ?? ''}
                onChange={(e) => setHistoryFilters((f) => ({ ...f, itemNum: e.target.value }))}
              />
            </label>
            <label>
              품목명
              <input
                value={historyFilters.itemName ?? ''}
                onChange={(e) => setHistoryFilters((f) => ({ ...f, itemName: e.target.value }))}
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
            <h2>{isSubMaterialTab ? '부자재 입고 이력' : '입고 이력'}</h2>
            <GridExcelExportButton
              fileBaseName={isSubMaterialTab ? '부자재입고이력' : '구매입고이력'}
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
                <th>입고일</th>
                <th>매입월</th>
                <th>거래처</th>
                <th>상태</th>
                <th>품목·수량</th>
                <th />
              </tr>
            </thead>
            <tbody>
              {receipts.length === 0 ? (
                <tr>
                  <td colSpan={7}>입고 이력이 없습니다.</td>
                </tr>
              ) : (
                receipts.map((receipt) => (
                  <tr key={receipt.id}>
                    <td>{receipt.receiptNo}</td>
                    <td>{receipt.receiptDate}</td>
                    <td>{formatFiscalPeriodFromIso(receipt.receiptDate, fiscalCutoverSetting)}</td>
                    <td>{receipt.partnerName}</td>
                    <td>{receiptStatusLabel(receipt.status)}</td>
                    <td>
                      {receipt.lines.map((line) => (
                        <div key={line.id}>
                          {line.itemNum} × {formatQty(line.receiptQty)}
                          {line.postedImmediately ? ' (즉시반영)' : ' (검사대기)'}
                        </div>
                      ))}
                    </td>
                    <td>
                      {receipt.status !== 'CANCELLED' && (
                        <button
                          type="button"
                          className="btn-action danger"
                          disabled={cancellingId === receipt.id}
                          onClick={() => void onCancelReceipt(receipt)}
                        >
                          {cancellingId === receipt.id ? '취소 중…' : '취소'}
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
    </div>
  );
}


