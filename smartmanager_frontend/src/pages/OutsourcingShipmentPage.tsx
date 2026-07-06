import { Fragment, useCallback, useEffect, useMemo, useState } from 'react';
import {
  cancelOutsourcingShipment,
  createOutsourcingShipment,
  fetchOutsourcingShipmentCandidates,
  fetchOutsourcingShipmentInputPreview,
  fetchOutsourcingShipments,
  type OutsourcingShipment,
  type OutsourcingShipmentCandidate,
  type OutsourcingShipmentInputPreview,
  type OutsourcingShipmentListParams,
} from '../api/outsourcingShipment';

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

function formatQty(value: number): string {
  return Number.isInteger(value) ? String(value) : value.toLocaleString(undefined, { maximumFractionDigits: 4 });
}

function parseQty(value: string): number | null {
  const parsed = Number(value);
  return Number.isFinite(parsed) && parsed > 0 ? parsed : null;
}

export default function OutsourcingShipmentPage() {
  const [candidates, setCandidates] = useState<OutsourcingShipmentCandidate[]>([]);
  const [shipments, setShipments] = useState<OutsourcingShipment[]>([]);
  const [shipmentDate, setShipmentDate] = useState(todayIso());
  const [listFilters, setListFilters] = useState<OutsourcingShipmentListParams>(() => ({
    shipmentDateFrom: addDaysIso(todayIso(), -7),
    shipmentDateTo: todayIso(),
    excludeCancelled: true,
  }));
  const [selectedLineIds, setSelectedLineIds] = useState<Set<number>>(new Set());
  const [shipmentQtyByLineId, setShipmentQtyByLineId] = useState<Record<number, string>>({});
  const [expandedLineIds, setExpandedLineIds] = useState<Set<number>>(new Set());
  const [previewByLineId, setPreviewByLineId] = useState<Record<number, OutsourcingShipmentInputPreview>>({});
  const [loadingCandidates, setLoadingCandidates] = useState(true);
  const [loadingShipments, setLoadingShipments] = useState(true);
  const [loadingPreviewId, setLoadingPreviewId] = useState<number | null>(null);
  const [submitting, setSubmitting] = useState(false);
  const [candidateError, setCandidateError] = useState<string | null>(null);
  const [shipmentError, setShipmentError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const loadCandidates = useCallback(async () => {
    setLoadingCandidates(true);
    setCandidateError(null);
    try {
      const rows = await fetchOutsourcingShipmentCandidates();
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
  }, []);

  const loadShipments = useCallback(async () => {
    setLoadingShipments(true);
    setShipmentError(null);
    try {
      setShipments(await fetchOutsourcingShipments(listFilters));
    } catch (e) {
      setShipmentError(e instanceof Error ? e.message : '외주출고 목록 조회 실패');
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
    if (checked) {
      setSelectedLineIds(new Set(selectableLineIds));
    } else {
      setSelectedLineIds(new Set());
    }
  };

  const toggleLine = (orderLineId: number, checked: boolean) => {
    setSelectedLineIds((prev) => {
      const next = new Set(prev);
      if (checked) next.add(orderLineId);
      else next.delete(orderLineId);
      return next;
    });
  };

  const toggleExpand = async (row: OutsourcingShipmentCandidate) => {
    const nextExpanded = !expandedLineIds.has(row.orderLineId);
    setExpandedLineIds((prev) => {
      const next = new Set(prev);
      if (nextExpanded) next.add(row.orderLineId);
      else next.delete(row.orderLineId);
      return next;
    });
    if (!nextExpanded || previewByLineId[row.orderLineId]) {
      return;
    }
    const qty = parseQty(shipmentQtyByLineId[row.orderLineId] ?? String(row.remainingQty));
    if (qty == null) {
      return;
    }
    setLoadingPreviewId(row.orderLineId);
    try {
      const preview = await fetchOutsourcingShipmentInputPreview(row.orderLineId, qty, shipmentDate);
      setPreviewByLineId((prev) => ({ ...prev, [row.orderLineId]: preview }));
    } catch (e) {
      setCandidateError(e instanceof Error ? e.message : '투입 미리보기 조회 실패');
    } finally {
      setLoadingPreviewId(null);
    }
  };

  const refreshPreview = async (orderLineId: number) => {
    const row = candidates.find((candidate) => candidate.orderLineId === orderLineId);
    if (!row) return;
    const qty = parseQty(shipmentQtyByLineId[orderLineId] ?? String(row.remainingQty));
    if (qty == null) return;
    setLoadingPreviewId(orderLineId);
    try {
      const preview = await fetchOutsourcingShipmentInputPreview(orderLineId, qty, shipmentDate);
      setPreviewByLineId((prev) => ({ ...prev, [orderLineId]: preview }));
    } catch (e) {
      setCandidateError(e instanceof Error ? e.message : '투입 미리보기 조회 실패');
    } finally {
      setLoadingPreviewId(null);
    }
  };

  const onCreateShipments = async () => {
    const lines = [...selectedLineIds].map((orderLineId) => {
      const row = candidates.find((candidate) => candidate.orderLineId === orderLineId);
      const qty = parseQty(shipmentQtyByLineId[orderLineId] ?? (row ? String(row.remainingQty) : ''));
      return qty != null ? { orderLineId, shipmentQty: qty } : null;
    }).filter((line): line is { orderLineId: number; shipmentQty: number } => line != null);

    if (lines.length === 0) {
      setShipmentError('출고할 발주 라인과 수량을 선택하세요.');
      return;
    }

    setSubmitting(true);
    setShipmentError(null);
    setMessage(null);
    try {
      const created = await createOutsourcingShipment({ shipmentDate, lines });
      setMessage(`외주출고 ${created.shipmentNo}을(를) 등록했습니다.`);
      setSelectedLineIds(new Set());
      setExpandedLineIds(new Set());
      setPreviewByLineId({});
      await loadCandidates();
      await loadShipments();
    } catch (e) {
      setShipmentError(e instanceof Error ? e.message : '외주출고 등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancel = async (shipment: OutsourcingShipment) => {
    if (!window.confirm(`외주출고 ${shipment.shipmentNo}을(를) 취소하시겠습니까?`)) return;
    setSubmitting(true);
    setShipmentError(null);
    try {
      await cancelOutsourcingShipment(shipment.id);
      setMessage(`외주출고 ${shipment.shipmentNo}을(를) 취소했습니다.`);
      await loadCandidates();
      await loadShipments();
    } catch (e) {
      setShipmentError(e instanceof Error ? e.message : '외주출고 취소 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const selectedCount = selectedLineIds.size;

  return (
    <div className="page">
      <header className="page-header">
        <h1>외주출고</h1>
        <p>외주발주 잔량을 기준으로 투입 자재·반제품을 출고하고 외주창고(OUTSOURCE)로 이동합니다.</p>
      </header>

      {message && <p className="success-banner">{message}</p>}
      {shipmentError && <p className="error-banner">{shipmentError}</p>}

      <section className="panel">
        <h2>발주 출고 후보</h2>
        <div className="action-bar">
          <label>
            출고일
            <input type="date" value={shipmentDate} onChange={(e) => setShipmentDate(e.target.value)} disabled={submitting} />
          </label>
          <button type="button" className="secondary" onClick={() => void loadCandidates()} disabled={loadingCandidates}>
            새로고침
          </button>
          <button type="button" disabled={submitting || selectedCount === 0} onClick={() => void onCreateShipments()}>
            {submitting ? '등록 중…' : `선택 출고 (${selectedCount})`}
          </button>
        </div>
        {loadingCandidates ? (
          <p>불러오는 중…</p>
        ) : candidateError ? (
          <p className="error-banner">{candidateError}</p>
        ) : candidates.length === 0 ? (
          <p className="hint">출고 가능한 외주발주가 없습니다.</p>
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
                  <th />
                  <th>발주번호</th>
                  <th>거래처</th>
                  <th>품목</th>
                  <th>공정구간</th>
                  <th>발주수량</th>
                  <th>출고수량</th>
                  <th>잔량</th>
                  <th>출고수량 입력</th>
                  <th>상태</th>
                </tr>
              </thead>
              <tbody>
                {candidates.map((row) => {
                  const expanded = expandedLineIds.has(row.orderLineId);
                  const preview = previewByLineId[row.orderLineId];
                  return (
                    <Fragment key={row.orderLineId}>
                      <tr className={!row.shippable ? 'row-muted' : undefined}>
                        <td>
                          <input
                            type="checkbox"
                            aria-label={`${row.itemNo} 선택`}
                            checked={selectedLineIds.has(row.orderLineId)}
                            disabled={!row.shippable || submitting}
                            onChange={(e) => toggleLine(row.orderLineId, e.target.checked)}
                          />
                        </td>
                        <td>
                          <button
                            type="button"
                            className="btn-action"
                            disabled={!row.shippable}
                            onClick={() => void toggleExpand(row)}
                          >
                            {expanded ? '접기' : '펼치기'}
                          </button>
                        </td>
                        <td>{row.orderNo}</td>
                        <td>{row.partnerName}</td>
                        <td>
                          {row.itemNo} {row.itemName}
                        </td>
                        <td>
                          {row.beginProcessName} ~ {row.endProcessName}
                        </td>
                        <td>{formatQty(row.orderQty)}</td>
                        <td>{formatQty(row.shippedQty)}</td>
                        <td>{formatQty(row.remainingQty)}</td>
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
                            onBlur={() => {
                              if (expanded) void refreshPreview(row.orderLineId);
                            }}
                          />
                        </td>
                        <td>{row.shippable ? '출고가능' : row.shippableMessage ?? '불가'}</td>
                      </tr>
                      {expanded && (
                        <tr>
                          <td colSpan={11}>
                            {loadingPreviewId === row.orderLineId ? (
                              <p>투입 미리보기 불러오는 중…</p>
                            ) : preview ? (
                              <table className="nested-table">
                                <thead>
                                  <tr>
                                    <th>투입품목</th>
                                    <th>분류</th>
                                    <th>창고</th>
                                    <th>투입수량</th>
                                    <th>현재고</th>
                                  </tr>
                                </thead>
                                <tbody>
                                  {preview.lines.map((line) => (
                                    <tr key={`${line.itemId}-${line.inputProcessId}`}>
                                      <td>
                                        {line.itemNo} {line.itemName}
                                      </td>
                                      <td>{line.propertyClassification}</td>
                                      <td>{line.sourceLocationCode}</td>
                                      <td>{formatQty(line.issueQty)}</td>
                                      <td>{formatQty(line.onHandQty)}</td>
                                    </tr>
                                  ))}
                                </tbody>
                              </table>
                            ) : (
                              <p className="hint">출고수량을 입력한 뒤 펼치기를 다시 눌러 주세요.</p>
                            )}
                          </td>
                        </tr>
                      )}
                    </Fragment>
                  );
                })}
              </tbody>
            </table>
          </div>
        )}
      </section>

      <section className="panel">
        <h2>외주출고 목록</h2>
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
          <p className="hint">외주출고가 없습니다.</p>
        ) : (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>출고번호</th>
                  <th>출고일</th>
                  <th>거래처</th>
                  <th>품목·공정</th>
                  <th>출고수량</th>
                  <th>상태</th>
                  <th />
                </tr>
              </thead>
              <tbody>
                {shipments.map((shipment) => (
                  <tr key={shipment.id}>
                    <td>{shipment.shipmentNo}</td>
                    <td>{shipment.shipmentDate}</td>
                    <td>{shipment.lines.map((line) => line.partnerName).filter((v, i, a) => a.indexOf(v) === i).join(', ')}</td>
                    <td>
                      {shipment.lines.map((line) => (
                        <div key={line.id}>
                          {line.itemNo} {line.processName} × {formatQty(line.shipmentQty)}
                        </div>
                      ))}
                    </td>
                    <td>{formatQty(shipment.lines.reduce((sum, line) => sum + line.shipmentQty, 0))}</td>
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
