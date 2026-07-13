import { Fragment, useCallback, useEffect, useMemo, useState } from 'react';
import {
  cancelOutsourcingOrder,
  createOutsourcingOrderFromWorkPlan,
  fetchOutsourcingOrders,
  fetchWorkPlanOutsourceCandidates,
  openOutsourcingOrderPrint,
  type OutsourcingOrder,
  type OutsourcingOrderListParams,
  type WorkPlanOutsourceCandidate,
} from '../api/outsourcingOrder';
import { formatAmount, formatQty } from '../utils/numberFormat';
import { useConfirm } from '../context/ConfirmContext';

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

function vendorKey(workPlanId: number, partnerId: number): string {
  return `${workPlanId}:${partnerId}`;
}

function parseVendorKey(key: string): { workPlanId: number; partnerId: number } {
  const [workPlanId, partnerId] = key.split(':').map(Number);
  return { workPlanId, partnerId };
}

function collectAllSelectableVendorKeys(candidates: WorkPlanOutsourceCandidate[]): Set<string> {
  const keys = new Set<string>();
  for (const candidate of candidates) {
    if (!candidate.orderable) {
      continue;
    }
    for (const vendor of candidate.vendors) {
      keys.add(vendorKey(candidate.workPlanId, vendor.partnerId));
    }
  }
  return keys;
}

export default function OutsourcingOrderPage() {
  const confirm = useConfirm();
  const [candidates, setCandidates] = useState<WorkPlanOutsourceCandidate[]>([]);
  const [orders, setOrders] = useState<OutsourcingOrder[]>([]);
  const [orderDate, setOrderDate] = useState(todayIso());
  const [listFilters, setListFilters] = useState<OutsourcingOrderListParams>(() => ({
    orderDateFrom: addDaysIso(todayIso(), -7),
    orderDateTo: todayIso(),
    excludeCancelled: true,
  }));
  const [expandedWorkPlanIds, setExpandedWorkPlanIds] = useState<Set<number>>(new Set());
  const [selectedVendorKeys, setSelectedVendorKeys] = useState<Set<string>>(new Set());
  const [selectedOrderIds, setSelectedOrderIds] = useState<Set<number>>(new Set());
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
      setCandidates(await fetchWorkPlanOutsourceCandidates(orderDate));
    } catch (e) {
      setCandidateError(e instanceof Error ? e.message : '작업계획 발주 대상 조회 실패');
      setCandidates([]);
    } finally {
      setLoadingCandidates(false);
    }
  }, [orderDate]);

  const loadOrders = useCallback(async () => {
    setLoadingOrders(true);
    setOrderError(null);
    try {
      setOrders(await fetchOutsourcingOrders(listFilters));
      setSelectedOrderIds(new Set());
    } catch (e) {
      setOrderError(e instanceof Error ? e.message : '외주발주 목록 조회 실패');
      setOrders([]);
    } finally {
      setLoadingOrders(false);
    }
  }, [listFilters]);

  useEffect(() => {
    void loadCandidates();
  }, [loadCandidates]);

  useEffect(() => {
    void loadOrders();
  }, [loadOrders]);

  const candidateByWorkPlanId = useMemo(() => {
    const map = new Map<number, WorkPlanOutsourceCandidate>();
    for (const candidate of candidates) {
      map.set(candidate.workPlanId, candidate);
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

  const printableOrders = useMemo(
    () => orders.filter((order) => order.status !== 'CANCELLED'),
    [orders],
  );

  const allOrdersSelected =
    printableOrders.length > 0 && printableOrders.every((order) => selectedOrderIds.has(order.id));

  const toggleOrderSelection = (orderId: number, checked: boolean) => {
    setSelectedOrderIds((prev) => {
      const next = new Set(prev);
      if (checked) {
        next.add(orderId);
      } else {
        next.delete(orderId);
      }
      return next;
    });
  };

  const handlePrintSelected = async () => {
    const orderIds = [...selectedOrderIds];
    if (orderIds.length === 0) {
      setOrderError('출력할 발주를 1건 이상 선택해 주세요.');
      return;
    }
    setOrderError(null);
    try {
      await openOutsourcingOrderPrint(orderIds);
    } catch (e) {
      setOrderError(e instanceof Error ? e.message : '외주발주서 출력 실패');
    }
  };

  const handlePrintOrder = async (orderId: number) => {
    setOrderError(null);
    try {
      await openOutsourcingOrderPrint([orderId]);
    } catch (e) {
      setOrderError(e instanceof Error ? e.message : '외주발주서 출력 실패');
    }
  };

  const toggleExpand = (workPlanId: number) => {
    setExpandedWorkPlanIds((prev) => {
      const next = new Set(prev);
      if (next.has(workPlanId)) next.delete(workPlanId);
      else next.add(workPlanId);
      return next;
    });
  };

  const toggleVendor = (workPlanId: number, partnerId: number, checked: boolean) => {
    const key = vendorKey(workPlanId, partnerId);
    setSelectedVendorKeys((prev) => {
      const next = new Set(prev);
      if (checked) next.add(key);
      else next.delete(key);
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

  const onCreateOrders = async () => {
    const grouped = new Map<number, Array<{ workPlanId: number; vendor: WorkPlanOutsourceCandidate['vendors'][0] }>>();
    for (const key of selectedVendorKeys) {
      const { workPlanId, partnerId } = parseVendorKey(key);
      const candidate = candidateByWorkPlanId.get(workPlanId);
      const vendor = candidate?.vendors.find((v) => v.partnerId === partnerId);
      if (!candidate || !vendor) continue;
      const rows = grouped.get(partnerId) ?? [];
      rows.push({ workPlanId, vendor });
      grouped.set(partnerId, rows);
    }
    if (grouped.size === 0) {
      setOrderError('발주할 거래처·작업계획을 선택하세요.');
      return;
    }

    setSubmitting(true);
    setOrderError(null);
    setMessage(null);
    try {
      const created: OutsourcingOrder[] = [];
      for (const [partnerId, rows] of grouped) {
        const order = await createOutsourcingOrderFromWorkPlan({
          partnerId,
          orderDate,
          lines: rows.map(({ workPlanId, vendor }) => ({
            workPlanId,
            beginProcessCodeId: vendor.beginProcessCodeId,
            endProcessCodeId: vendor.endProcessCodeId,
            orderQty: vendor.orderQty,
            unitPrice: vendor.unitPrice,
            requestedDeliveryDate: vendor.requestedDeliveryDate ?? undefined,
          })),
        });
        created.push(order);
      }
      setMessage(`외주발주 ${created.map((o) => o.orderNo).join(', ')}을(를) 등록했습니다.`);
      setSelectedVendorKeys(new Set());
      await loadCandidates();
      await loadOrders();
    } catch (e) {
      setOrderError(e instanceof Error ? e.message : '외주발주 등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancel = async (order: OutsourcingOrder) => {
    if (!(await confirm(`외주발주 ${order.orderNo}을(를) 취소하시겠습니까?`, { title: '취소 확인', confirmLabel: '예, 취소', cancelLabel: '닫기', danger: true }))) return;
    setSubmitting(true);
    setOrderError(null);
    try {
      await cancelOutsourcingOrder(order.id);
      setMessage(`외주발주 ${order.orderNo}을(를) 취소했습니다.`);
      await loadCandidates();
      await loadOrders();
    } catch (e) {
      setOrderError(e instanceof Error ? e.message : '외주발주 취소 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const selectedCount = selectedVendorKeys.size;

  return (
    <div className="page">
      <header className="page-header">
        <h1>외주발주</h1>
        <p>외주 작업계획(OUTSOURCE)을 기준으로 외주거래처에 발주합니다. 등록 즉시 확정됩니다.</p>
      </header>

      {message && <p className="success-banner">{message}</p>}
      {orderError && <p className="error-banner">{orderError}</p>}

      <section className="panel">
        <h2>작업계획 발주 후보</h2>
        <div className="action-bar">
          <label>
            발주일
            <input type="date" value={orderDate} onChange={(e) => setOrderDate(e.target.value)} disabled={submitting} />
          </label>
          <button type="button" className="secondary" onClick={() => void loadCandidates()} disabled={loadingCandidates}>
            새로고침
          </button>
          <button
            type="button"
            disabled={submitting || selectedCount === 0}
            onClick={() => void onCreateOrders()}
          >
            {submitting ? '등록 중…' : `선택 발주 (${selectedCount})`}
          </button>
        </div>
        {loadingCandidates ? (
          <p>불러오는 중…</p>
        ) : candidateError ? (
          <p className="error-banner">{candidateError}</p>
        ) : candidates.length === 0 ? (
          <p className="hint">발주 가능한 외주 작업계획이 없습니다.</p>
        ) : (
          <div className="table-wrap">
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
                  <th>계획번호</th>
                  <th>품목</th>
                  <th>공정</th>
                  <th className="num">계획수량</th>
                  <th className="num">잔량</th>
                  <th>거래처</th>
                  <th>상태</th>
                </tr>
              </thead>
              <tbody>
                {candidates.map((row) => {
                  const expanded = expandedWorkPlanIds.has(row.workPlanId);
                  const vendorCount = row.vendors.length;
                  const rowVendorKeys = row.vendors.map((v) => vendorKey(row.workPlanId, v.partnerId));
                  const rowAllSelected =
                    row.orderable && rowVendorKeys.length > 0 && rowVendorKeys.every((k) => selectedVendorKeys.has(k));
                  const rowSomeSelected = rowVendorKeys.some((k) => selectedVendorKeys.has(k));

                  return (
                    <Fragment key={row.workPlanId}>
                      <tr className={!row.orderable ? 'row-muted' : undefined}>
                        <td>
                          <input
                            type="checkbox"
                            aria-label={`${row.itemNo} 전체 거래처 선택`}
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
                            onClick={() => toggleExpand(row.workPlanId)}
                          >
                            {vendorCount === 0 ? '—' : expanded ? '접기' : '펼치기'}
                          </button>
                        </td>
                        <td>{row.planNo}</td>
                        <td>
                          {row.itemNo} {row.itemName}
                        </td>
                        <td>
                          {row.processCode} {row.processName}
                        </td>
                        <td className="num">{formatQty(row.plannedQty)}</td>
                        <td className="num">{formatQty(row.remainingQty)}</td>
                        <td>{vendorCount > 0 ? `${vendorCount}곳` : '—'}</td>
                        <td>{row.orderable ? '발주가능' : row.orderableMessage ?? '불가'}</td>
                      </tr>
                      {expanded && vendorCount > 0 && (
                        <tr key={`${row.workPlanId}-vendors`}>
                          <td colSpan={9}>
                            <table className="nested-table">
                              <thead>
                                <tr>
                                  <th />
                                  <th>거래처</th>
                                  <th>공정구간</th>
                                  <th className="num">발주비율</th>
                                  <th className="num">발주수량</th>
                                  <th className="num">단가</th>
                                  <th className="num">금액</th>
                                  <th>납기</th>
                                </tr>
                              </thead>
                              <tbody>
                                {row.vendors.map((vendor) => {
                                  const key = vendorKey(row.workPlanId, vendor.partnerId);
                                  const disabled = !row.orderable || submitting;
                                  return (
                                    <tr key={key}>
                                      <td>
                                        <input
                                          type="checkbox"
                                          aria-label={`${vendor.partnerName} 선택`}
                                          checked={selectedVendorKeys.has(key)}
                                          disabled={disabled}
                                          onChange={(e) =>
                                            toggleVendor(row.workPlanId, vendor.partnerId, e.target.checked)
                                          }
                                        />
                                      </td>
                                      <td>{vendor.partnerName}</td>
                                      <td>
                                        {vendor.beginProcessName} ~ {vendor.endProcessName}
                                      </td>
                                      <td className="num">{formatQty(vendor.orderRate)}%</td>
                                      <td className="num">{formatQty(vendor.orderQty)}</td>
                                      <td className="num">{formatAmount(vendor.unitPrice)}</td>
                                      <td className="num">{formatAmount(vendor.amount)}</td>
                                      <td>{vendor.requestedDeliveryDate ?? '—'}</td>
                                    </tr>
                                  );
                                })}
                              </tbody>
                            </table>
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
        <div className="panel-header-row">
          <h2>외주발주 목록</h2>
          <button
            type="button"
            className="btn-action"
            disabled={submitting || selectedOrderIds.size === 0}
            onClick={() => void handlePrintSelected()}
          >
            발주서 발행
          </button>
        </div>
        {orderError && <div className="error">{orderError}</div>}
        <div className="filter-row">
          <label>
            발주일 From
            <input
              type="date"
              value={listFilters.orderDateFrom ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, orderDateFrom: e.target.value }))}
            />
          </label>
          <label>
            To
            <input
              type="date"
              value={listFilters.orderDateTo ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, orderDateTo: e.target.value }))}
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
            발주번호
            <input
              type="text"
              value={listFilters.orderNo ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, orderNo: e.target.value }))}
            />
          </label>
          <button type="button" className="secondary" onClick={() => void loadOrders()}>
            조회
          </button>
        </div>
        {loadingOrders ? (
          <p>불러오는 중…</p>
        ) : orders.length === 0 ? (
          <p className="hint">외주발주가 없습니다.</p>
        ) : (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>
                    <input
                      type="checkbox"
                      aria-label="전체 발주 선택"
                      checked={allOrdersSelected}
                      disabled={printableOrders.length === 0 || submitting}
                      ref={(el) => {
                        if (el) {
                          el.indeterminate =
                            !allOrdersSelected && printableOrders.some((order) => selectedOrderIds.has(order.id));
                        }
                      }}
                      onChange={(e) => {
                        if (e.target.checked) {
                          setSelectedOrderIds(new Set(printableOrders.map((order) => order.id)));
                        } else {
                          setSelectedOrderIds(new Set());
                        }
                      }}
                    />
                  </th>
                  <th>발주번호</th>
                  <th>발주일</th>
                  <th>거래처</th>
                  <th>출처</th>
                  <th>상태</th>
                  <th>라인</th>
                  <th>관리</th>
                </tr>
              </thead>
              <tbody>
                {orders.map((order) => {
                  const printable = order.status !== 'CANCELLED';
                  return (
                  <tr key={order.id}>
                    <td>
                      <input
                        type="checkbox"
                        aria-label={`${order.orderNo} 선택`}
                        checked={selectedOrderIds.has(order.id)}
                        disabled={!printable || submitting}
                        onChange={(e) => toggleOrderSelection(order.id, e.target.checked)}
                      />
                    </td>
                    <td>{order.orderNo}</td>
                    <td>{order.orderDate}</td>
                    <td>{order.partnerName}</td>
                    <td>{order.sourceTypeLabel}</td>
                    <td>{order.statusLabel}</td>
                    <td>
                      {order.lines.map((line) => (
                        <div key={line.id}>
                          {line.itemNo} {line.processName} × {formatQty(line.orderQty)}
                        </div>
                      ))}
                    </td>
                    <td className="actions">
                      {printable && (
                        <button
                          type="button"
                          className="btn-action"
                          disabled={submitting}
                          onClick={() => void handlePrintOrder(order.id)}
                        >
                          발주서 출력
                        </button>
                      )}
                      {order.cancelable && (
                        <button type="button" className="btn-action danger" disabled={submitting} onClick={() => void onCancel(order)}>
                          취소
                        </button>
                      )}
                    </td>
                  </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        )}
      </section>
    </div>
  );
}
