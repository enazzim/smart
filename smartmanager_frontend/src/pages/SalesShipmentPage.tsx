import { useCallback, useEffect, useMemo, useState } from 'react';
import {
  cancelSalesShipment,
  createSalesShipment,
  fetchSalesShipmentCandidates,
  fetchSalesShipments,
  type SalesShipment,
  type SalesShipmentCandidate,
  type SalesShipmentCandidateParams,
  type SalesShipmentListParams,
} from '../api/salesShipment';
import {
  INVENTORY_LOCATION_LABEL,
  translateInventoryLocationInText,
} from '../utils/inventoryLocation';
import GridExcelExportButton from '../components/GridExcelExportButton';
import { formatQty } from '../utils/numberFormat';

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

export default function SalesShipmentPage() {
  const [candidates, setCandidates] = useState<SalesShipmentCandidate[]>([]);
  const [shipments, setShipments] = useState<SalesShipment[]>([]);
  const [shipmentDate, setShipmentDate] = useState(todayIso());
  const [filters, setFilters] = useState<SalesShipmentCandidateParams>(() => ({
    orderDateFrom: addDaysIso(todayIso(), -30),
    orderDateTo: todayIso(),
  }));
  const [listFilters, setListFilters] = useState<SalesShipmentListParams>(() => ({
    shipmentDateFrom: addDaysIso(todayIso(), -30),
    shipmentDateTo: todayIso(),
    excludeCancelled: true,
  }));
  const [selectedLineIds, setSelectedLineIds] = useState<Set<number>>(new Set());
  const [shipmentQtyByLineId, setShipmentQtyByLineId] = useState<Record<number, string>>({});
  const [loadingCandidates, setLoadingCandidates] = useState(true);
  const [loadingShipments, setLoadingShipments] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [candidateError, setCandidateError] = useState<string | null>(null);
  const [shipmentError, setShipmentError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const shipmentExportRows = useMemo(
    () =>
      shipments.map((shipment) => ({
        출고번호: shipment.shipmentNo,
        출고일: shipment.shipmentDate,
        거래처: shipment.partnerName,
        품목: shipment.lines.map((line) => `${line.orderNo} — ${line.itemNo} ${line.itemName}`).join(' / '),
        출고수량: shipment.lines.map((line) => formatQty(line.shipmentQty)).join(' / '),
        상태: shipment.statusLabel,
      })),
    [shipments],
  );

  const loadCandidates = useCallback(async () => {
    setLoadingCandidates(true);
    setCandidateError(null);
    try {
      const rows = await fetchSalesShipmentCandidates(filters);
      setCandidates(rows);
      setShipmentQtyByLineId((prev) => {
        const next = { ...prev };
        for (const row of rows) {
          if (next[row.orderLineId] === undefined) {
            next[row.orderLineId] = String(row.remainingQty);
          }
        }
        return next;
      });
    } catch (e) {
      setCandidateError(e instanceof Error ? e.message : '출고 후보 조회 실패');
      setCandidates([]);
    } finally {
      setLoadingCandidates(false);
    }
  }, [filters]);

  const loadShipments = useCallback(async () => {
    setLoadingShipments(true);
    setShipmentError(null);
    try {
      setShipments(await fetchSalesShipments(listFilters));
    } catch (e) {
      setShipmentError(e instanceof Error ? e.message : '출고·납품 목록 조회 실패');
      setShipments([]);
    } finally {
      setLoadingShipments(false);
    }
  }, [listFilters]);

  useEffect(() => {
    void loadCandidates();
  }, [loadCandidates]);

  useEffect(() => {
    void loadShipments();
  }, [loadShipments]);

  const selectableLineIds = useMemo(
    () => new Set(candidates.filter((row) => row.shippable).map((row) => row.orderLineId)),
    [candidates],
  );

  const allSelected =
    selectableLineIds.size > 0 && [...selectableLineIds].every((id) => selectedLineIds.has(id));

  const toggleSelectAll = (checked: boolean) => {
    setSelectedLineIds(checked ? new Set(selectableLineIds) : new Set());
  };

  const toggleLine = (orderLineId: number, checked: boolean) => {
    setSelectedLineIds((prev) => {
      const next = new Set(prev);
      if (checked) next.add(orderLineId);
      else next.delete(orderLineId);
      return next;
    });
  };

  const onCreateShipments = async () => {
    const lines = [...selectedLineIds]
      .map((orderLineId) => {
        const row = candidates.find((candidate) => candidate.orderLineId === orderLineId);
        const qty = parseQty(shipmentQtyByLineId[orderLineId] ?? (row ? String(row.remainingQty) : ''));
        return qty != null ? { salesOrderLineId: orderLineId, shipmentQty: qty } : null;
      })
      .filter((line): line is { salesOrderLineId: number; shipmentQty: number } => line != null);

    if (lines.length === 0) {
      setShipmentError('출고할 수주 라인과 수량을 선택하세요.');
      return;
    }

    setSubmitting(true);
    setShipmentError(null);
    setMessage(null);
    try {
      const created = await createSalesShipment({ shipmentDate, lines });
      setMessage(`출고·납품 ${created.shipmentNo}을(를) 등록했습니다. (${INVENTORY_LOCATION_LABEL.SALES} → ${INVENTORY_LOCATION_LABEL.DELIVERY})`);
      setSelectedLineIds(new Set());
      await loadCandidates();
      await loadShipments();
    } catch (e) {
      setShipmentError(e instanceof Error ? e.message : '출고·납품 등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancel = async (shipment: SalesShipment) => {
    if (!window.confirm(`출고·납품 ${shipment.shipmentNo}을(를) 취소하시겠습니까?`)) return;
    setSubmitting(true);
    setShipmentError(null);
    try {
      await cancelSalesShipment(shipment.id);
      setMessage(`출고·납품 ${shipment.shipmentNo}을(를) 취소했습니다.`);
      await loadCandidates();
      await loadShipments();
    } catch (e) {
      setShipmentError(e instanceof Error ? e.message : '출고·납품 취소 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const selectedCount = selectedLineIds.size;

  return (
    <div className="page">
      <header className="page-header">
        <h1>출고·납품</h1>
        <p>수주 잔량을 기준으로 {INVENTORY_LOCATION_LABEL.SALES}에서 출고하고 {INVENTORY_LOCATION_LABEL.DELIVERY}로 이동합니다.</p>
      </header>

      {message && <p className="success-banner">{message}</p>}
      {shipmentError && <p className="error-banner">{shipmentError}</p>}

      <section className="panel">
        <h2>수주 출고 후보</h2>
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
            수주번호
            <input
              type="text"
              value={filters.orderNo ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, orderNo: e.target.value }))}
            />
          </label>
          <label>
            수주일 From
            <input
              type="date"
              value={filters.orderDateFrom ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, orderDateFrom: e.target.value }))}
            />
          </label>
          <label>
            To
            <input
              type="date"
              value={filters.orderDateTo ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, orderDateTo: e.target.value }))}
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
            출고일
            <input type="date" value={shipmentDate} onChange={(e) => setShipmentDate(e.target.value)} disabled={submitting} />
          </label>
          <button type="button" disabled={submitting || selectedCount === 0} onClick={() => void onCreateShipments()}>
            {submitting ? '등록 중…' : `선택 출고 (${selectedCount})`}
          </button>
        </div>
        {loadingCandidates ? (
          <p>불러오는 중…</p>
        ) : candidateError ? (
          <p className="error-banner">{candidateError}</p>
        ) : candidates.length === 0 ? (
          <p className="hint">출고 가능한 수주 라인이 없습니다.</p>
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
                  <th>수주번호</th>
                  <th>거래처</th>
                  <th>품목</th>
                  <th className="num">수주수량</th>
                  <th className="num">출고누적</th>
                  <th className="num">잔량</th>
                  <th className="num">{INVENTORY_LOCATION_LABEL.SALES} 재고</th>
                  <th>출고수량 입력</th>
                  <th>비고</th>
                </tr>
              </thead>
              <tbody>
                {candidates.map((row) => (
                  <tr key={row.orderLineId} className={!row.shippable ? 'row-muted' : undefined}>
                    <td>
                      <input
                        type="checkbox"
                        aria-label={`${row.itemNo} 선택`}
                        checked={selectedLineIds.has(row.orderLineId)}
                        disabled={!row.shippable || submitting}
                        onChange={(e) => toggleLine(row.orderLineId, e.target.checked)}
                      />
                    </td>
                    <td>{row.orderNo}</td>
                    <td>{row.partnerName}</td>
                    <td>
                      {row.itemNo} {row.itemName}
                    </td>
                    <td className="num">{formatQty(row.orderQty)}</td>
                    <td className="num">{formatQty(row.shippedQty)}</td>
                    <td className="num">{formatQty(row.remainingQty)}</td>
                    <td className="num">{formatQty(row.salesOnHandQty)}</td>
                    <td>
                      <input
                        type="number"
                        min={0}
                        step="any"
                        disabled={!row.shippable || submitting}
                        value={shipmentQtyByLineId[row.orderLineId] ?? String(row.remainingQty)}
                        onChange={(e) =>
                          setShipmentQtyByLineId((prev) => ({ ...prev, [row.orderLineId]: e.target.value }))
                        }
                      />
                    </td>
                    <td>{translateInventoryLocationInText(row.shippableMessage) || (row.shippable ? '출고가능' : '불가')}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </section>

      <section className="panel">
        <div className="panel-header-row">
          <h2>출고·납품 목록</h2>
          <GridExcelExportButton fileBaseName="출고납품목록" disabled={loadingShipments} rows={shipmentExportRows} />
        </div>
        <div className="filter-row">
          <label>
            출고일 From
            <input
              type="date"
              value={listFilters.shipmentDateFrom ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, shipmentDateFrom: e.target.value }))}
            />
          </label>
          <label>
            To
            <input
              type="date"
              value={listFilters.shipmentDateTo ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, shipmentDateTo: e.target.value }))}
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
            출고번호
            <input
              type="text"
              value={listFilters.shipmentNo ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, shipmentNo: e.target.value }))}
            />
          </label>
          <button type="button" className="secondary" onClick={() => void loadShipments()}>
            조회
          </button>
        </div>
        {loadingShipments ? (
          <p>불러오는 중…</p>
        ) : shipments.length === 0 ? (
          <p className="hint">출고·납품 내역이 없습니다.</p>
        ) : (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>출고번호</th>
                  <th>출고일</th>
                  <th>거래처</th>
                  <th>품목</th>
                  <th className="num">출고수량</th>
                  <th>상태</th>
                  <th />
                </tr>
              </thead>
              <tbody>
                {shipments.map((shipment) => (
                  <tr key={shipment.id}>
                    <td>{shipment.shipmentNo}</td>
                    <td>{shipment.shipmentDate}</td>
                    <td>{shipment.partnerName}</td>
                    <td>
                      {shipment.lines.map((line) => (
                        <div key={line.id}>
                          {line.orderNo} — {line.itemNo} {line.itemName}
                        </div>
                      ))}
                    </td>
                    <td className="num">
                      {shipment.lines.map((line) => (
                        <div key={line.id}>{formatQty(line.shipmentQty)}</div>
                      ))}
                    </td>
                    <td>{shipment.statusLabel}</td>
                    <td>
                      {shipment.cancelable && (
                        <button type="button" className="secondary" disabled={submitting} onClick={() => void onCancel(shipment)}>
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
