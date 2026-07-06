import { Fragment, useCallback, useEffect, useMemo, useState } from 'react';
import {
  cancelPurchaseOrder,
  createPurchaseOrderFromMrp,
  fetchMrpPurchaseCandidates,
  fetchPurchaseOrders,
  openPurchaseOrderPrint,
  type MrpPurchaseCandidate,
  type MrpPurchaseCandidateVendor,
  type PurchaseOrder,
  type PurchaseOrderListParams,
} from '../api/purchaseOrder';

function formatQty(value: number): string {
  return Number.isInteger(value) ? String(value) : value.toLocaleString(undefined, { maximumFractionDigits: 4 });
}

function formatAmount(value: number): string {
  return value.toLocaleString(undefined, { maximumFractionDigits: 2 });
}

function formatDateTime(value: string): string {
  const date = new Date(value);
  if (Number.isNaN(date.getTime())) {
    return value;
  }
  return date.toLocaleString('ko-KR');
}

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

function defaultListFilters(today = todayIso()): PurchaseOrderListParams {
  return {
    orderDateFrom: addDaysIso(today, -7),
    orderDateTo: today,
    partnerName: '',
    orderNo: '',
    excludeCancelled: true,
  };
}

function vendorSelectionKey(requirementLineId: number, partnerId: number): string {
  return `${requirementLineId}:${partnerId}`;
}

function parseVendorSelectionKey(key: string): { requirementLineId: number; partnerId: number } {
  const [requirementLineId, partnerId] = key.split(':').map(Number);
  return { requirementLineId, partnerId };
}

function collectAllSelectableVendorKeys(candidates: MrpPurchaseCandidate[]): Set<string> {
  const keys = new Set<string>();
  for (const candidate of candidates) {
    if (!candidate.orderable) {
      continue;
    }
    for (const vendor of candidate.vendors) {
      keys.add(vendorSelectionKey(candidate.requirementLineId, vendor.partnerId));
    }
  }
  return keys;
}

export default function PurchaseOrderPage() {
  const [candidates, setCandidates] = useState<MrpPurchaseCandidate[]>([]);
  const [orders, setOrders] = useState<PurchaseOrder[]>([]);
  const [expandedRequirementIds, setExpandedRequirementIds] = useState<Set<number>>(new Set());
  const [selectedVendorKeys, setSelectedVendorKeys] = useState<Set<string>>(new Set());
  const [orderDate, setOrderDate] = useState(todayIso());
  const [listFilters, setListFilters] = useState<PurchaseOrderListParams>(() => defaultListFilters());
  const [lastCreatedOrders, setLastCreatedOrders] = useState<PurchaseOrder[]>([]);
  const [loadingCandidates, setLoadingCandidates] = useState(true);
  const [loadingOrders, setLoadingOrders] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [candidateError, setCandidateError] = useState<string | null>(null);
  const [orderError, setOrderError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const loadCandidates = useCallback(async () => {
    setLoadingCandidates(true);
    setCandidateError(null);
    try {
      setCandidates(await fetchMrpPurchaseCandidates(orderDate));
    } catch (e) {
      setCandidateError(e instanceof Error ? e.message : 'MRP 발주 대상 조회 실패');
      setCandidates([]);
    } finally {
      setLoadingCandidates(false);
    }
  }, [orderDate]);

  const loadOrders = useCallback(async () => {
    setLoadingOrders(true);
    setOrderError(null);
    try {
      setOrders(await fetchPurchaseOrders(listFilters));
    } catch (e) {
      setOrderError(e instanceof Error ? e.message : '구매발주 목록 조회 실패');
      setOrders([]);
    } finally {
      setLoadingOrders(false);
    }
  }, [listFilters]);

  const refreshAll = useCallback(async () => {
    setSelectedVendorKeys(new Set());
    await loadCandidates();
    await loadOrders();
  }, [loadCandidates, loadOrders]);

  useEffect(() => {
    void loadCandidates();
  }, [loadCandidates]);

  useEffect(() => {
    void loadOrders();
  }, [loadOrders]);

  const candidateByRequirementId = useMemo(() => {
    const map = new Map<number, MrpPurchaseCandidate>();
    for (const candidate of candidates) {
      map.set(candidate.requirementLineId, candidate);
    }
    return map;
  }, [candidates]);

  const allSelectableVendorKeys = useMemo(
    () => collectAllSelectableVendorKeys(candidates),
    [candidates],
  );

  const allVendorsSelected =
    allSelectableVendorKeys.size > 0 &&
    [...allSelectableVendorKeys].every((key) => selectedVendorKeys.has(key));

  const handlePrint = async (orderId: number) => {
    setOrderError(null);
    try {
      await openPurchaseOrderPrint(orderId);
    } catch (e) {
      setOrderError(e instanceof Error ? e.message : '발주서 출력 실패');
    }
  };

  const toggleExpanded = (requirementLineId: number) => {
    setExpandedRequirementIds((prev) => {
      const next = new Set(prev);
      if (next.has(requirementLineId)) {
        next.delete(requirementLineId);
      } else {
        next.add(requirementLineId);
      }
      return next;
    });
  };

  const toggleVendorSelection = (
    candidate: MrpPurchaseCandidate,
    vendor: MrpPurchaseCandidateVendor,
    checked: boolean,
  ) => {
    const key = vendorSelectionKey(candidate.requirementLineId, vendor.partnerId);
    setSelectedVendorKeys((prev) => {
      const next = new Set(prev);
      if (checked) {
        next.add(key);
      } else {
        next.delete(key);
      }
      return next;
    });
  };

  const toggleSelectAllVendors = (checked: boolean) => {
    if (checked) {
      setSelectedVendorKeys(new Set(allSelectableVendorKeys));
    } else {
      setSelectedVendorKeys(new Set());
    }
  };

  const onCreateFromMrp = async () => {
    const selections = [...selectedVendorKeys].map(parseVendorSelectionKey);
    if (selections.length === 0) {
      setCandidateError('발주할 거래처를 1건 이상 선택하세요.');
      return;
    }

    const groupedByPartner = new Map<
      number,
      { requirementLineId: number; orderQty: number; unitPrice: number; requestedDeliveryDate?: string | null }[]
    >();

    for (const selection of selections) {
      const candidate = candidateByRequirementId.get(selection.requirementLineId);
      if (!candidate || !candidate.orderable) {
        setCandidateError('발주할 수 없는 자재소요가 포함되어 있습니다.');
        return;
      }
      const vendor = candidate.vendors.find((row) => row.partnerId === selection.partnerId);
      if (!vendor) {
        setCandidateError('선택한 거래처 정보를 찾을 수 없습니다.');
        return;
      }
      const lines = groupedByPartner.get(selection.partnerId) ?? [];
      lines.push({
        requirementLineId: selection.requirementLineId,
        orderQty: vendor.orderQty,
        unitPrice: vendor.unitPrice,
        requestedDeliveryDate: vendor.requestedDeliveryDate,
      });
      groupedByPartner.set(selection.partnerId, lines);
    }

    setSubmitting(true);
    setMessage(null);
    setCandidateError(null);
    try {
      const createdOrders: PurchaseOrder[] = [];
      for (const [partnerId, lines] of groupedByPartner) {
        const order = await createPurchaseOrderFromMrp({
          partnerId,
          orderDate,
          lines: lines.map((line) => ({
            requirementLineId: line.requirementLineId,
            orderQty: line.orderQty,
            unitPrice: line.unitPrice,
            requestedDeliveryDate: line.requestedDeliveryDate ?? undefined,
          })),
        });
        createdOrders.push(order);
      }
      setLastCreatedOrders(createdOrders);
      setListFilters((prev) => ({
        ...prev,
        orderDateFrom: prev.orderDateFrom && prev.orderDateFrom <= orderDate ? prev.orderDateFrom : orderDate,
        orderDateTo: prev.orderDateTo && prev.orderDateTo >= orderDate ? prev.orderDateTo : orderDate,
      }));
      if (createdOrders.length === 1) {
        setMessage(`구매발주 ${createdOrders[0].orderNo}을(를) 등록했습니다.`);
      } else {
        setMessage(
          `구매발주 ${createdOrders.length}건을 등록했습니다. (${createdOrders.map((o) => o.orderNo).join(', ')})`,
        );
      }
      setSelectedVendorKeys(new Set());
      await loadCandidates();
      await loadOrders();
    } catch (e) {
      setCandidateError(e instanceof Error ? e.message : 'MRP 발주 등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancel = async (order: PurchaseOrder) => {
    if (!window.confirm(`발주 ${order.orderNo}을(를) 취소하시겠습니까?`)) {
      return;
    }
    setSubmitting(true);
    setMessage(null);
    setOrderError(null);
    try {
      await cancelPurchaseOrder(order.id);
      setMessage(`발주 ${order.orderNo}을(를) 취소했습니다.`);
      setLastCreatedOrders((prev) => prev.filter((row) => row.id !== order.id));
      await refreshAll();
    } catch (e) {
      setOrderError(e instanceof Error ? e.message : '발주 취소 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const selectedCount = selectedVendorKeys.size;

  return (
    <div className="page">
      <h1>구매발주</h1>

      <section className="panel">
        <h2>MRP 발주 대상</h2>
        <p className="hint-text">
          자재소요 산출 결과 중 <strong>아직 발주되지 않은</strong> 라인입니다. 행을 펼쳐 거래처·단가·납기요구일을
          확인한 뒤 발주할 거래처를 선택하세요. 발주비율에 따라 <strong>분할 수량</strong>으로 거래처별 발주됩니다.
        </p>
        {candidateError && <div className="error">{candidateError}</div>}
        <div className="form-grid-wide">
          <label>
            발주일
            <input type="date" value={orderDate} onChange={(e) => setOrderDate(e.target.value)} />
          </label>
        </div>
        <div className="form-actions">
          <button
            type="button"
            disabled={submitting || selectedCount === 0}
            onClick={() => void onCreateFromMrp()}
          >
            {submitting ? '처리 중…' : `선택 발주 (${selectedCount})`}
          </button>
          <button type="button" className="secondary" onClick={() => void loadCandidates()}>
            새로고침
          </button>
        </div>
        {loadingCandidates ? (
          <p>불러오는 중…</p>
        ) : candidates.length === 0 ? (
          <p>발주 가능한 자재소요가 없습니다.</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>
                  <input
                    type="checkbox"
                    aria-label="전체 선택"
                    checked={allVendorsSelected}
                    disabled={allSelectableVendorKeys.size === 0 || submitting}
                    onChange={(e) => toggleSelectAllVendors(e.target.checked)}
                  />
                </th>
                <th />
                <th>산출번호</th>
                <th>계획번호</th>
                <th>자재품목</th>
                <th>자산분류</th>
                <th>단위</th>
                <th>총소요</th>
                <th>잔여</th>
                <th>거래처</th>
                <th>상태</th>
              </tr>
            </thead>
            <tbody>
              {candidates.map((row) => {
                const expanded = expandedRequirementIds.has(row.requirementLineId);
                const vendorCount = row.vendors.length;
                const rowVendorKeys = row.vendors.map((v) =>
                  vendorSelectionKey(row.requirementLineId, v.partnerId),
                );
                const rowAllSelected =
                  row.orderable && rowVendorKeys.length > 0 && rowVendorKeys.every((k) => selectedVendorKeys.has(k));
                const rowSomeSelected = rowVendorKeys.some((k) => selectedVendorKeys.has(k));

                return (
                  <Fragment key={row.requirementLineId}>
                    <tr className={!row.orderable ? 'row-muted' : undefined}>
                      <td>
                        <input
                          type="checkbox"
                          aria-label={`${row.componentItemNo} 전체 거래처 선택`}
                          checked={rowAllSelected}
                          disabled={!row.orderable || vendorCount === 0 || submitting}
                          ref={(el) => {
                            if (el) {
                              el.indeterminate = !rowAllSelected && rowSomeSelected;
                            }
                          }}
                          onChange={(e) => {
                            setSelectedVendorKeys((prev) => {
                              const next = new Set(prev);
                              for (const key of rowVendorKeys) {
                                if (e.target.checked) {
                                  next.add(key);
                                } else {
                                  next.delete(key);
                                }
                              }
                              return next;
                            });
                          }}
                        />
                      </td>
                      <td>
                        <button
                          type="button"
                          className="btn-action"
                          disabled={vendorCount === 0}
                          onClick={() => toggleExpanded(row.requirementLineId)}
                        >
                          {vendorCount === 0 ? '—' : expanded ? '접기' : '펼치기'}
                        </button>
                      </td>
                      <td>{row.runNo}</td>
                      <td>{row.planNo}</td>
                      <td>
                        {row.componentItemNo} — {row.componentItemName}
                      </td>
                      <td>{row.componentPropertyClassification}</td>
                      <td>{row.unit}</td>
                      <td>{formatQty(row.grossQty)}</td>
                      <td>{formatQty(row.suggestedQty)}</td>
                      <td>{vendorCount > 0 ? `${vendorCount}곳` : '—'}</td>
                      <td>{row.orderable ? '발주 가능' : row.orderableMessage ?? '발주 불가'}</td>
                    </tr>
                    {expanded && vendorCount > 0 && (
                      <tr>
                        <td colSpan={11}>
                          <table className="nested-table">
                            <thead>
                              <tr>
                                <th />
                                <th>거래처</th>
                                <th>사업자번호</th>
                                <th>발주비율</th>
                                <th>발주수량</th>
                                <th>단가</th>
                                <th>금액</th>
                                <th>리드타임</th>
                                <th>납기요구일</th>
                              </tr>
                            </thead>
                            <tbody>
                              {row.vendors.map((vendor) => {
                                const key = vendorSelectionKey(row.requirementLineId, vendor.partnerId);
                                const checked = selectedVendorKeys.has(key);
                                return (
                                  <tr key={key}>
                                    <td>
                                      <input
                                        type="checkbox"
                                        aria-label={`${vendor.partnerName} 선택`}
                                        checked={checked}
                                        disabled={!row.orderable || submitting}
                                        onChange={(e) =>
                                          toggleVendorSelection(row, vendor, e.target.checked)
                                        }
                                      />
                                    </td>
                                    <td>{vendor.partnerName}</td>
                                    <td>{vendor.businessRegNo}</td>
                                    <td>{formatQty(vendor.orderRate)}%</td>
                                    <td>{formatQty(vendor.orderQty)}</td>
                                    <td>{formatAmount(vendor.unitPrice)}</td>
                                    <td>{formatAmount(vendor.amount)}</td>
                                    <td>{vendor.leadTimeDays != null ? `${vendor.leadTimeDays}일` : '—'}</td>
                                    <td>{vendor.requestedDeliveryDate ?? '—'}</td>
                                  </tr>
                                );
                              })}
                            </tbody>
                          </table>
                        </td>
                      </tr>
                    )}
                    {expanded && vendorCount === 0 && (
                      <tr>
                        <td colSpan={11}>
                          <p className="hint-text">{row.orderableMessage ?? '등록된 구매단가·거래처가 없습니다.'}</p>
                        </td>
                      </tr>
                    )}
                  </Fragment>
                );
              })}
            </tbody>
          </table>
        )}
      </section>

      {lastCreatedOrders.length > 0 && (
        <section className="panel sub-panel">
          <h2>방금 등록한 발주</h2>
          <p className="hint-text">아래에서 발주서(주문서)를 바로 출력할 수 있습니다.</p>
          <div className="form-actions">
            <button
              type="button"
              className="secondary"
              onClick={() => {
                for (const order of lastCreatedOrders) {
                  void handlePrint(order.id);
                }
              }}
            >
              전체 발주서 출력
            </button>
            <button type="button" className="secondary" onClick={() => setLastCreatedOrders([])}>
              닫기
            </button>
          </div>
          <table>
            <thead>
              <tr>
                <th>발주번호</th>
                <th>거래처</th>
                <th>발주일</th>
                <th>라인</th>
                <th>관리</th>
              </tr>
            </thead>
            <tbody>
              {lastCreatedOrders.map((order) => (
                <tr key={`created-${order.id}`}>
                  <td>{order.orderNo}</td>
                  <td>{order.partnerName}</td>
                  <td>{order.orderDate}</td>
                  <td>{order.lines.length}</td>
                  <td>
                    <button type="button" className="btn-action" onClick={() => void handlePrint(order.id)}>
                      발주서 출력
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </section>
      )}

      <section className="panel">
        <h2>발주 목록</h2>
        {message && <p>{message}</p>}
        {orderError && <div className="error">{orderError}</div>}
        <div className="form-grid-wide">
          <label>
            발주일(부터)
            <input
              type="date"
              value={listFilters.orderDateFrom ?? ''}
              onChange={(e) => setListFilters((prev) => ({ ...prev, orderDateFrom: e.target.value || undefined }))}
            />
          </label>
          <label>
            발주일(까지)
            <input
              type="date"
              value={listFilters.orderDateTo ?? ''}
              onChange={(e) => setListFilters((prev) => ({ ...prev, orderDateTo: e.target.value || undefined }))}
            />
          </label>
          <label>
            거래처명
            <input
              type="text"
              value={listFilters.partnerName ?? ''}
              placeholder="상호 검색"
              onChange={(e) => setListFilters((prev) => ({ ...prev, partnerName: e.target.value }))}
            />
          </label>
          <label>
            발주번호
            <input
              type="text"
              value={listFilters.orderNo ?? ''}
              placeholder="PO-"
              onChange={(e) => setListFilters((prev) => ({ ...prev, orderNo: e.target.value }))}
            />
          </label>
        </div>
        <div className="form-actions">
          <button type="button" onClick={() => void loadOrders()}>
            검색
          </button>
          <button
            type="button"
            className="secondary"
            onClick={() => setListFilters(defaultListFilters())}
          >
            초기화
          </button>
        </div>
        {loadingOrders ? (
          <p>불러오는 중…</p>
        ) : orders.length === 0 ? (
          <p>조건에 맞는 구매발주가 없습니다.</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>발주번호</th>
                <th>거래처</th>
                <th>발주일</th>
                <th>출처</th>
                <th>상태</th>
                <th>라인</th>
                <th>등록일시</th>
                <th>관리</th>
              </tr>
            </thead>
            <tbody>
              {orders.map((order) => (
                <tr key={order.id}>
                  <td>{order.orderNo}</td>
                  <td>{order.partnerName}</td>
                  <td>{order.orderDate}</td>
                  <td>{order.sourceTypeLabel}</td>
                  <td>{order.statusLabel}</td>
                  <td>{order.lines.length}</td>
                  <td>{formatDateTime(order.createdAt)}</td>
                  <td className="actions">
                    <button
                      type="button"
                      className="btn-action"
                      disabled={submitting}
                      onClick={() => void handlePrint(order.id)}
                    >
                      발주서 출력
                    </button>
                    {order.cancelable && (
                      <button
                        type="button"
                        className="btn-action danger"
                        disabled={submitting}
                        onClick={() => void onCancel(order)}
                      >
                        취소
                      </button>
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
        {orders.some((order) => order.lines.length > 0) && (
          <div className="detail-panel">
            <h3>발주 상세</h3>
            {orders.map((order) => (
              <div key={`detail-${order.id}`} className="sub-panel">
                <h4>
                  {order.orderNo} — {order.partnerName} ({order.statusLabel})
                </h4>
                <table>
                  <thead>
                    <tr>
                      <th>라인</th>
                      <th>품목</th>
                      <th>수량</th>
                      <th>단가</th>
                      <th>금액</th>
                      <th>납기요구일</th>
                      <th>자재소요</th>
                    </tr>
                  </thead>
                  <tbody>
                    {order.lines.map((line) => (
                      <tr key={line.id}>
                        <td>{line.lineNo}</td>
                        <td>
                          {line.itemNo} — {line.itemName}
                        </td>
                        <td>{formatQty(line.orderQty)}</td>
                        <td>{formatAmount(line.unitPrice)}</td>
                        <td>{formatAmount(line.amount)}</td>
                        <td>{line.requestedDeliveryDate ?? '—'}</td>
                        <td>
                          {line.requirementLineId
                            ? `${line.runNo ?? '—'} / ${line.planNo ?? '—'}`
                            : '—'}
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            ))}
          </div>
        )}
      </section>
    </div>
  );
}
