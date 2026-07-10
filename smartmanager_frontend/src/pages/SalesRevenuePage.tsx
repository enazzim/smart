import { useCallback, useEffect, useMemo, useState } from 'react';
import {
  cancelSalesRevenue,
  createSalesRevenue,
  fetchSalesRevenueCandidates,
  fetchSalesRevenues,
  type SalesRevenue,
  type SalesRevenueCandidate,
  type SalesRevenueCandidateParams,
  type SalesRevenueListParams,
} from '../api/salesRevenue';
import { INVENTORY_LOCATION_LABEL, translateInventoryLocationInText } from '../utils/inventoryLocation';
import GridExcelExportButton from '../components/GridExcelExportButton';
import { formatAmount, formatQty } from '../utils/numberFormat';

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

function parseQty(value: string): number | null {
  const parsed = Number(value);
  return Number.isFinite(parsed) && parsed > 0 ? parsed : null;
}

export default function SalesRevenuePage() {
  const [candidates, setCandidates] = useState<SalesRevenueCandidate[]>([]);
  const [revenues, setRevenues] = useState<SalesRevenue[]>([]);
  const [revenueDate, setRevenueDate] = useState(todayIso());
  const [filters, setFilters] = useState<SalesRevenueCandidateParams>(() => ({
    shipmentDateFrom: addDaysIso(todayIso(), -30),
    shipmentDateTo: todayIso(),
  }));
  const [listFilters, setListFilters] = useState<SalesRevenueListParams>(() => ({
    revenueDateFrom: addDaysIso(todayIso(), -30),
    revenueDateTo: todayIso(),
    excludeCancelled: true,
  }));
  const [selectedLineIds, setSelectedLineIds] = useState<Set<number>>(new Set());
  const [revenueQtyByLineId, setRevenueQtyByLineId] = useState<Record<number, string>>({});
  const [loadingCandidates, setLoadingCandidates] = useState(true);
  const [loadingRevenues, setLoadingRevenues] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [candidateError, setCandidateError] = useState<string | null>(null);
  const [revenueError, setRevenueError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const revenueExportRows = useMemo(
    () =>
      revenues.map((revenue) => ({
        매출번호: revenue.revenueNo,
        매출일: revenue.revenueDate,
        거래처: revenue.partnerName,
        품목: revenue.lines.map((line) => `${line.shipmentNo} — ${line.itemNo} ${line.itemName}`).join(' / '),
        매출수량: revenue.lines.map((line) => formatQty(line.revenueQty)).join(' / '),
        금액: revenue.lines.map((line) => formatAmount(line.amount)).join(' / '),
        상태: revenue.statusLabel,
      })),
    [revenues],
  );

  const loadCandidates = useCallback(async () => {
    setLoadingCandidates(true);
    setCandidateError(null);
    try {
      const rows = await fetchSalesRevenueCandidates(filters);
      setCandidates(rows);
      setRevenueQtyByLineId((prev) => {
        const next = { ...prev };
        for (const row of rows) {
          if (next[row.shipmentLineId] === undefined) {
            next[row.shipmentLineId] = String(row.remainingQty);
          }
        }
        return next;
      });
    } catch (e) {
      setCandidateError(e instanceof Error ? e.message : '매출 후보 조회 실패');
      setCandidates([]);
    } finally {
      setLoadingCandidates(false);
    }
  }, [filters]);

  const loadRevenues = useCallback(async () => {
    setLoadingRevenues(true);
    setRevenueError(null);
    try {
      setRevenues(await fetchSalesRevenues(listFilters));
    } catch (e) {
      setRevenueError(e instanceof Error ? e.message : '매출 목록 조회 실패');
      setRevenues([]);
    } finally {
      setLoadingRevenues(false);
    }
  }, [listFilters]);

  useEffect(() => {
    void loadCandidates();
  }, [loadCandidates]);

  useEffect(() => {
    void loadRevenues();
  }, [loadRevenues]);

  const selectableLineIds = useMemo(
    () => new Set(candidates.filter((row) => row.billable).map((row) => row.shipmentLineId)),
    [candidates],
  );

  const allSelected =
    selectableLineIds.size > 0 && [...selectableLineIds].every((id) => selectedLineIds.has(id));

  const toggleSelectAll = (checked: boolean) => {
    setSelectedLineIds(checked ? new Set(selectableLineIds) : new Set());
  };

  const toggleLine = (shipmentLineId: number, checked: boolean) => {
    setSelectedLineIds((prev) => {
      const next = new Set(prev);
      if (checked) next.add(shipmentLineId);
      else next.delete(shipmentLineId);
      return next;
    });
  };

  const onCreateRevenues = async () => {
    const lines = [...selectedLineIds]
      .map((shipmentLineId) => {
        const row = candidates.find((candidate) => candidate.shipmentLineId === shipmentLineId);
        const qty = parseQty(revenueQtyByLineId[shipmentLineId] ?? (row ? String(row.remainingQty) : ''));
        return qty != null ? { salesShipmentLineId: shipmentLineId, revenueQty: qty } : null;
      })
      .filter((line): line is { salesShipmentLineId: number; revenueQty: number } => line != null);

    if (lines.length === 0) {
      setRevenueError('매출 등록할 출고 라인과 수량을 선택하세요.');
      return;
    }

    setSubmitting(true);
    setRevenueError(null);
    setMessage(null);
    try {
      const created = await createSalesRevenue({ revenueDate, lines });
      setMessage(`매출 ${created.revenueNo}을(를) 등록했습니다. (${INVENTORY_LOCATION_LABEL.DELIVERY} 감소)`);
      setSelectedLineIds(new Set());
      await loadCandidates();
      await loadRevenues();
    } catch (e) {
      setRevenueError(e instanceof Error ? e.message : '매출 등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancel = async (revenue: SalesRevenue) => {
    if (!window.confirm(`매출 ${revenue.revenueNo}을(를) 취소하시겠습니까?`)) return;
    setSubmitting(true);
    setRevenueError(null);
    try {
      await cancelSalesRevenue(revenue.id);
      setMessage(`매출 ${revenue.revenueNo}을(를) 취소했습니다.`);
      await loadCandidates();
      await loadRevenues();
    } catch (e) {
      setRevenueError(e instanceof Error ? e.message : '매출 취소 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const selectedCount = selectedLineIds.size;

  return (
    <div className="page">
      <header className="page-header">
        <h1>매출</h1>
        <p>출고·납품 잔량을 기준으로 {INVENTORY_LOCATION_LABEL.DELIVERY}에서 매출을 인식합니다.</p>
      </header>

      {message && <p className="success-banner">{message}</p>}
      {revenueError && <p className="error-banner">{revenueError}</p>}

      <section className="panel">
        <h2>출고 매출 후보</h2>
        <div className="filter-row">
          <label>
            거래처
            <input
              type="text"
              value={filters.partnerName ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, partnerName: e.target.value }))}
            />
          </label>
          <label>
            출고번호
            <input
              type="text"
              value={filters.shipmentNo ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, shipmentNo: e.target.value }))}
            />
          </label>
          <label>
            출고일 From
            <input
              type="date"
              value={filters.shipmentDateFrom ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, shipmentDateFrom: e.target.value }))}
            />
          </label>
          <label>
            To
            <input
              type="date"
              value={filters.shipmentDateTo ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, shipmentDateTo: e.target.value }))}
            />
          </label>
          <label>
            수주번호
            <input
              type="text"
              value={filters.orderNo ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, orderNo: e.target.value }))}
            />
          </label>
          <label>
            품번
            <input
              type="text"
              value={filters.itemNum ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, itemNum: e.target.value }))}
            />
          </label>
          <button type="button" className="secondary" onClick={() => void loadCandidates()} disabled={loadingCandidates}>
            조회
          </button>
        </div>
        <div className="action-bar">
          <label>
            매출일
            <input type="date" value={revenueDate} onChange={(e) => setRevenueDate(e.target.value)} disabled={submitting} />
          </label>
          <button type="button" disabled={submitting || selectedCount === 0} onClick={() => void onCreateRevenues()}>
            {submitting ? '등록 중…' : `선택 매출 (${selectedCount})`}
          </button>
        </div>
        {loadingCandidates ? (
          <p>불러오는 중…</p>
        ) : candidateError ? (
          <p className="error-banner">{candidateError}</p>
        ) : candidates.length === 0 ? (
          <p className="hint">매출 가능한 출고 라인이 없습니다.</p>
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
                      disabled={selectableLineIds.size === 0 || submitting}
                      onChange={(e) => toggleSelectAll(e.target.checked)}
                    />
                  </th>
                  <th>출고번호</th>
                  <th>거래처</th>
                  <th>수주번호</th>
                  <th>품목</th>
                  <th className="num">출고수량</th>
                  <th className="num">매출누적</th>
                  <th className="num">잔량</th>
                  <th className="num">단가</th>
                  <th className="num">{INVENTORY_LOCATION_LABEL.DELIVERY} 재고</th>
                  <th>매출수량 입력</th>
                  <th>비고</th>
                </tr>
              </thead>
              <tbody>
                {candidates.map((row) => (
                  <tr key={row.shipmentLineId} className={!row.billable ? 'row-muted' : undefined}>
                    <td>
                      <input
                        type="checkbox"
                        aria-label={`${row.itemNo} 선택`}
                        checked={selectedLineIds.has(row.shipmentLineId)}
                        disabled={!row.billable || submitting}
                        onChange={(e) => toggleLine(row.shipmentLineId, e.target.checked)}
                      />
                    </td>
                    <td>{row.shipmentNo}</td>
                    <td>{row.partnerName}</td>
                    <td>{row.orderNo}</td>
                    <td>
                      {row.itemNo} {row.itemName}
                    </td>
                    <td className="num">{formatQty(row.shippedQty)}</td>
                    <td className="num">{formatQty(row.invoicedQty)}</td>
                    <td className="num">{formatQty(row.remainingQty)}</td>
                    <td className="num">{formatAmount(row.unitPrice)}</td>
                    <td className="num">{formatQty(row.deliveryOnHandQty)}</td>
                    <td>
                      <input
                        type="number"
                        min={0}
                        step="any"
                        disabled={!row.billable || submitting}
                        value={revenueQtyByLineId[row.shipmentLineId] ?? String(row.remainingQty)}
                        onChange={(e) =>
                          setRevenueQtyByLineId((prev) => ({ ...prev, [row.shipmentLineId]: e.target.value }))
                        }
                      />
                    </td>
                    <td>{translateInventoryLocationInText(row.billableMessage) || (row.billable ? '매출가능' : '불가')}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </section>

      <section className="panel">
        <div className="panel-header-row">
          <h2>매출 목록</h2>
          <GridExcelExportButton fileBaseName="매출목록" disabled={loadingRevenues} rows={revenueExportRows} />
        </div>
        <div className="filter-row">
          <label>
            매출일 From
            <input
              type="date"
              value={listFilters.revenueDateFrom ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, revenueDateFrom: e.target.value }))}
            />
          </label>
          <label>
            To
            <input
              type="date"
              value={listFilters.revenueDateTo ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, revenueDateTo: e.target.value }))}
            />
          </label>
          <label>
            거래처
            <input
              type="text"
              value={listFilters.partnerName ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, partnerName: e.target.value }))}
            />
          </label>
          <label>
            매출번호
            <input
              type="text"
              value={listFilters.revenueNo ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, revenueNo: e.target.value }))}
            />
          </label>
          <button type="button" className="secondary" onClick={() => void loadRevenues()}>
            조회
          </button>
        </div>
        {loadingRevenues ? (
          <p>불러오는 중…</p>
        ) : revenues.length === 0 ? (
          <p className="hint">매출 내역이 없습니다.</p>
        ) : (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>매출번호</th>
                  <th>매출일</th>
                  <th>거래처</th>
                  <th>품목</th>
                  <th className="num">매출수량</th>
                  <th className="num">금액</th>
                  <th>상태</th>
                  <th />
                </tr>
              </thead>
              <tbody>
                {revenues.map((revenue) => (
                  <tr key={revenue.id}>
                    <td>{revenue.revenueNo}</td>
                    <td>{revenue.revenueDate}</td>
                    <td>{revenue.partnerName}</td>
                    <td>
                      {revenue.lines.map((line) => (
                        <div key={line.id}>
                          {line.shipmentNo} — {line.itemNo} {line.itemName}
                        </div>
                      ))}
                    </td>
                    <td className="num">
                      {revenue.lines.map((line) => (
                        <div key={line.id}>{formatQty(line.revenueQty)}</div>
                      ))}
                    </td>
                    <td className="num">
                      {revenue.lines.map((line) => (
                        <div key={line.id}>{formatAmount(line.amount)}</div>
                      ))}
                    </td>
                    <td>{revenue.statusLabel}</td>
                    <td>
                      {revenue.cancelable && (
                        <button type="button" className="secondary" disabled={submitting} onClick={() => void onCancel(revenue)}>
                          취소
                        </button>
                      )}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </section>
    </div>
  );
}
