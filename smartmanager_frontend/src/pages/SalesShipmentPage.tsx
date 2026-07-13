import { useCallback, useMemo, useState } from 'react';
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
import { fetchAvailableLots, type LotRow } from '../api/lot';
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

function shippableLabel(row: SalesShipmentCandidate): string {
  return translateInventoryLocationInText(row.shippableMessage) || (row.shippable ? '출고가능' : '불가');
}

function formatShipmentStock(row: SalesShipmentCandidate): string {
  const wip = row.wipOnHandQty ?? 0;
  if (wip > 0) {
    return formatQty(wip);
  }
  return formatQty(row.salesOnHandQty);
}

function lotQtyOnHand(lot: LotRow, locationCode: string, processId?: number | null): number {
  return lot.balances
    .filter(
      (b) =>
        b.locationCode === locationCode &&
        (processId == null
          ? b.outputProcessId == null
          : b.outputProcessId === processId),
    )
    .reduce((sum, b) => sum + Number(b.qtyOnHand || 0), 0);
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
  const [lotIdByLineId, setLotIdByLineId] = useState<Record<number, number | null>>({});
  const [lotsByLineId, setLotsByLineId] = useState<Record<number, LotRow[]>>({});
  const [loadingCandidates, setLoadingCandidates] = useState(false);
  const [loadingShipments, setLoadingShipments] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [candidateError, setCandidateError] = useState<string | null>(null);
  const [shipmentError, setShipmentError] = useState<string | null>(null);
  const [candidatesSearched, setCandidatesSearched] = useState(false);
  const [shipmentsSearched, setShipmentsSearched] = useState(false);
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

  const defaultCandidateFilters = (): SalesShipmentCandidateParams => ({
    orderDateFrom: addDaysIso(todayIso(), -30),
    orderDateTo: todayIso(),
  });

  const defaultListFilters = (): SalesShipmentListParams => ({
    shipmentDateFrom: addDaysIso(todayIso(), -30),
    shipmentDateTo: todayIso(),
    excludeCancelled: true,
  });

  const loadCandidates = useCallback(async (params: SalesShipmentCandidateParams = filters) => {
    setLoadingCandidates(true);
    setCandidateError(null);
    try {
      const rows = await fetchSalesShipmentCandidates(params);
      setCandidates(rows);
      setCandidatesSearched(true);
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
      setCandidatesSearched(true);
    } finally {
      setLoadingCandidates(false);
    }
  }, [filters]);

  const loadShipments = useCallback(async (params: SalesShipmentListParams = listFilters) => {
    setLoadingShipments(true);
    setShipmentError(null);
    try {
      setShipments(await fetchSalesShipments(params));
      setShipmentsSearched(true);
    } catch (e) {
      setShipmentError(e instanceof Error ? e.message : '출고·납품 목록 조회 실패');
      setShipments([]);
      setShipmentsSearched(true);
    } finally {
      setLoadingShipments(false);
    }
  }, [listFilters]);

  const handleResetCandidateFilters = () => {
    setFilters(defaultCandidateFilters());
  };

  const handleResetListFilters = () => {
    setListFilters(defaultListFilters());
  };

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
    if (checked) {
      const row = candidates.find((candidate) => candidate.orderLineId === orderLineId);
      if (row?.lotTracked) {
        void loadLotsForCandidate(row);
      }
    }
  };

  const loadLotsForCandidate = async (row: SalesShipmentCandidate) => {
    try {
      const lots = await fetchAvailableLots(
        row.itemId,
        row.lotLocationCode || 'SALES',
        row.finalProcessId,
      );
      setLotsByLineId((prev) => ({ ...prev, [row.orderLineId]: lots }));
      setLotIdByLineId((prev) => ({
        ...prev,
        [row.orderLineId]:
          prev[row.orderLineId] != null && lots.some((lot) => lot.id === prev[row.orderLineId])
            ? prev[row.orderLineId]
            : lots.length === 1
              ? lots[0].id
              : null,
      }));
    } catch {
      setLotsByLineId((prev) => ({ ...prev, [row.orderLineId]: [] }));
    }
  };

  const onCreateShipments = async () => {
    const lines = [...selectedLineIds]
      .map((orderLineId) => {
        const row = candidates.find((candidate) => candidate.orderLineId === orderLineId);
        const qty = parseQty(shipmentQtyByLineId[orderLineId] ?? (row ? String(row.remainingQty) : ''));
        if (qty == null || !row) return null;
        if (row.lotTracked && lotIdByLineId[orderLineId] == null) {
          return { error: `Lot 추적 품목은 Lot를 선택해야 합니다: ${row.itemNo}` };
        }
        return {
          salesOrderLineId: orderLineId,
          shipmentQty: qty,
          lotId: row.lotTracked ? lotIdByLineId[orderLineId] ?? null : null,
        };
      })
      .filter((line): line is NonNullable<typeof line> => line != null);

    const lotError = lines.find((line) => 'error' in line);
    if (lotError && 'error' in lotError) {
      setShipmentError(String(lotError.error));
      return;
    }
    const payloadLines = lines.filter(
      (line): line is { salesOrderLineId: number; shipmentQty: number; lotId: number | null } =>
        !('error' in line),
    );

    if (payloadLines.length === 0) {
      setShipmentError('출고할 수주 라인과 수량을 선택하세요.');
      return;
    }

    setSubmitting(true);
    setShipmentError(null);
    setMessage(null);
    try {
      const created = await createSalesShipment({ shipmentDate, lines: payloadLines });
      setMessage(
        `출고·납품 ${created.shipmentNo}을(를) 등록했습니다. (${INVENTORY_LOCATION_LABEL.SALES} → ${INVENTORY_LOCATION_LABEL.DELIVERY})`,
      );
      setSelectedLineIds(new Set());
      setLotIdByLineId({});
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

  const renderShipmentQtyInput = (row: SalesShipmentCandidate) => (
    <input
      type="number"
      min={0}
      step="any"
      disabled={!row.shippable || submitting}
      value={shipmentQtyByLineId[row.orderLineId] ?? String(row.remainingQty)}
      onChange={(e) => setShipmentQtyByLineId((prev) => ({ ...prev, [row.orderLineId]: e.target.value }))}
    />
  );

  return (
    <div className="page sales-shipment-page">
      <header className="page-header">
        <div>
          <h1>출고·납품</h1>
          <p>
            수주 잔량을 기준으로 출고·납품합니다. 제품·상품은 {INVENTORY_LOCATION_LABEL.SALES}에서, 공정품은{' '}
            {INVENTORY_LOCATION_LABEL.WIP}(최종공정)에서 {INVENTORY_LOCATION_LABEL.DELIVERY}로 이동합니다.
          </p>
        </div>
      </header>

      {message && <p className="success-banner">{message}</p>}
      {shipmentError && <p className="error-banner">{shipmentError}</p>}

      <section className="panel">
        <h2>수주 출고 후보</h2>
        <div className="sales-shipment-filters">
          <label>
            거래처
            <input
              type="text"
              value={filters.partnerName ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, partnerName: e.target.value }))}
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
          <label>
            수주번호
            <input
              type="text"
              value={filters.orderNo ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, orderNo: e.target.value }))}
            />
          </label>
          <label>
            수주일(부터)
            <input
              type="date"
              value={filters.orderDateFrom ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, orderDateFrom: e.target.value }))}
            />
          </label>
          <label>
            수주일(까지)
            <input
              type="date"
              value={filters.orderDateTo ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, orderDateTo: e.target.value }))}
            />
          </label>
          <div className="sales-shipment-filter-actions">
            <button type="button" onClick={() => void loadCandidates()} disabled={loadingCandidates}>
              {loadingCandidates ? '조회 중…' : '조회'}
            </button>
            <button type="button" className="secondary" onClick={handleResetCandidateFilters}>
              초기화
            </button>
          </div>
        </div>

        <div className="sales-revenue-action-bar">
          <label>
            출고일
            <input
              type="date"
              value={shipmentDate}
              onChange={(e) => setShipmentDate(e.target.value)}
              disabled={submitting}
            />
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
          <p className="hint sales-revenue-empty">
            {candidatesSearched
              ? '출고 가능한 수주 라인이 없습니다.'
              : '검색 조건을 입력한 뒤 조회 버튼을 눌러 주세요.'}
          </p>
        ) : (
          <>
            <div className="table-wrap sales-shipment-candidate-table">
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
                    <th className="num">출고가용재고</th>
                    <th>출고수량 입력</th>
                    <th>Lot</th>
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
                        {row.lotTracked ? ' · Lot' : ''}
                      </td>
                      <td className="num">{formatQty(row.orderQty)}</td>
                      <td className="num">{formatQty(row.shippedQty)}</td>
                      <td className="num">{formatQty(row.remainingQty)}</td>
                      <td className="num">{formatShipmentStock(row)}</td>
                      <td>{renderShipmentQtyInput(row)}</td>
                      <td>
                        {row.lotTracked ? (
                          <select
                            value={lotIdByLineId[row.orderLineId] ?? ''}
                            disabled={submitting || !selectedLineIds.has(row.orderLineId)}
                            onChange={(e) =>
                              setLotIdByLineId((prev) => ({
                                ...prev,
                                [row.orderLineId]: e.target.value ? Number(e.target.value) : null,
                              }))
                            }
                            onFocus={() => {
                              if (!lotsByLineId[row.orderLineId]) void loadLotsForCandidate(row);
                            }}
                          >
                            <option value="">선택</option>
                            {(lotsByLineId[row.orderLineId] ?? []).map((lot) => (
                              <option key={lot.id} value={lot.id}>
                                {lot.lotNo} (
                                {formatQty(
                                  lotQtyOnHand(lot, row.lotLocationCode || 'SALES', row.finalProcessId),
                                )}
                                )
                              </option>
                            ))}
                          </select>
                        ) : (
                          '—'
                        )}
                      </td>
                      <td>{shippableLabel(row)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>

            <div className="sales-shipment-candidate-cards" aria-label="수주 출고 후보 목록">
              <div className="sales-revenue-candidate-cards__toolbar">
                <label className="sales-revenue-select-all">
                  <input
                    type="checkbox"
                    aria-label="전체 선택"
                    checked={allSelected}
                    disabled={selectableLineIds.size === 0 || submitting}
                    onChange={(e) => toggleSelectAll(e.target.checked)}
                  />
                  전체 선택
                </label>
                <span className="sales-revenue-selected-count">{selectedCount}건 선택</span>
              </div>
              {candidates.map((row) => (
                <article
                  key={row.orderLineId}
                  className={`sales-revenue-candidate-card${!row.shippable ? ' is-muted' : ''}${
                    selectedLineIds.has(row.orderLineId) ? ' is-selected' : ''
                  }`}
                >
                  <div className="sales-revenue-candidate-card__head">
                    <input
                      type="checkbox"
                      aria-label={`${row.itemNo} 선택`}
                      checked={selectedLineIds.has(row.orderLineId)}
                      disabled={!row.shippable || submitting}
                      onChange={(e) => toggleLine(row.orderLineId, e.target.checked)}
                    />
                    <div className="sales-revenue-candidate-card__title">
                      <strong>{row.orderNo}</strong>
                      <span>{row.partnerName}</span>
                    </div>
                    <span className={`sales-revenue-status${row.shippable ? ' is-ok' : ''}`}>{shippableLabel(row)}</span>
                  </div>
                  <dl className="sales-revenue-candidate-card__meta">
                    <div className="sales-revenue-meta-span-2">
                      <dt>품목</dt>
                      <dd>
                        {row.itemNo} {row.itemName}
                      </dd>
                    </div>
                    <div>
                      <dt>수주수량</dt>
                      <dd>{formatQty(row.orderQty)}</dd>
                    </div>
                    <div>
                      <dt>출고누적</dt>
                      <dd>{formatQty(row.shippedQty)}</dd>
                    </div>
                    <div>
                      <dt>잔량</dt>
                      <dd>{formatQty(row.remainingQty)}</dd>
                    </div>
                    <div>
                      <dt>출고가용재고</dt>
                      <dd>{formatShipmentStock(row)}</dd>
                    </div>
                  </dl>
                  <label className="sales-revenue-candidate-card__qty">
                    출고수량
                    {renderShipmentQtyInput(row)}
                  </label>
                </article>
              ))}
            </div>
          </>
        )}
      </section>

      <section className="panel">
        <div className="panel-header-row">
          <h2>출고·납품 목록</h2>
          <GridExcelExportButton fileBaseName="출고납품목록" disabled={loadingShipments} rows={shipmentExportRows} />
        </div>
        <div className="sales-shipment-filters">
          <label>
            출고일(부터)
            <input
              type="date"
              value={listFilters.shipmentDateFrom ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, shipmentDateFrom: e.target.value }))}
            />
          </label>
          <label>
            출고일(까지)
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
          <div className="sales-shipment-filter-actions">
            <button type="button" onClick={() => void loadShipments()} disabled={loadingShipments}>
              {loadingShipments ? '조회 중…' : '조회'}
            </button>
            <button type="button" className="secondary" onClick={handleResetListFilters}>
              초기화
            </button>
          </div>
        </div>
        {loadingShipments ? (
          <p>불러오는 중…</p>
        ) : shipments.length === 0 ? (
          <p className="hint sales-revenue-empty">
            {shipmentsSearched
              ? '출고·납품 내역이 없습니다.'
              : '검색 조건을 입력한 뒤 조회 버튼을 눌러 주세요.'}
          </p>
        ) : (
          <>
            <div className="table-wrap sales-shipment-list-table">
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
                          <button
                            type="button"
                            className="secondary"
                            disabled={submitting}
                            onClick={() => void onCancel(shipment)}
                          >
                            취소
                          </button>
                        )}
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>

            <div className="sales-shipment-list-cards" aria-label="출고·납품 목록">
              {shipments.map((shipment) => (
                <article key={shipment.id} className="sales-revenue-list-card">
                  <div className="sales-revenue-list-card__head">
                    <div>
                      <strong>{shipment.shipmentNo}</strong>
                      <span>{shipment.shipmentDate}</span>
                    </div>
                    <span className="sales-revenue-list-card__status">{shipment.statusLabel}</span>
                  </div>
                  <p className="sales-revenue-list-card__partner">{shipment.partnerName}</p>
                  <ul className="sales-revenue-list-card__lines">
                    {shipment.lines.map((line) => (
                      <li key={line.id}>
                        <span className="sales-revenue-list-card__item">
                          {line.orderNo} — {line.itemNo} {line.itemName}
                        </span>
                        <span className="sales-revenue-list-card__amounts">{formatQty(line.shipmentQty)}</span>
                      </li>
                    ))}
                  </ul>
                  {shipment.cancelable && (
                    <div className="sales-revenue-list-card__actions">
                      <button
                        type="button"
                        className="secondary"
                        disabled={submitting}
                        onClick={() => void onCancel(shipment)}
                      >
                        취소
                      </button>
                    </div>
                  )}
                </article>
              ))}
            </div>
          </>
        )}
      </section>
    </div>
  );
}
