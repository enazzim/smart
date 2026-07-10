import { Fragment, useCallback, useEffect, useMemo, useState } from 'react';
import {
  cancelPurchaseOrder,
  confirmPurchaseOrder,
  createPurchaseOrder,
  createPurchaseOrderFromMrp,
  fetchMrpPurchaseCandidates,
  fetchPurchaseOrders,
  openPurchaseOrderPrint,
  type MrpPurchaseCandidate,
  type MrpPurchaseCandidateVendor,
  type PurchaseOrder,
  type PurchaseOrderListParams,
} from '../api/purchaseOrder';
import { fetchUnitPrices } from '../api/unitPrice';
import CompanySearchField, { type CompanySearchSelection } from '../components/CompanySearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import {
  isUnitPriceEffective,
  toPartnerPriceItems,
  type PartnerPriceItem,
} from '../utils/unitPriceHelpers';
import { formatAmount, formatQty } from '../utils/numberFormat';

type ManualPurchaseLine = {
  key: string;
  item: ItemSearchSelection | null;
  orderQty: string;
  unitPrice: string;
};

function toItemSearchSelection(item: PartnerPriceItem): ItemSearchSelection {
  return {
    id: item.itemId,
    itemNo: item.itemNo,
    itemName: item.itemName,
  };
}

function newManualLine(): ManualPurchaseLine {
  return {
    key: `line-${Date.now()}-${Math.random().toString(36).slice(2, 8)}`,
    item: null,
    orderQty: '1',
    unitPrice: '',
  };
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
  const [selectedOrderIds, setSelectedOrderIds] = useState<Set<number>>(new Set());
  const [orderDate, setOrderDate] = useState(todayIso());
  const [listFilters, setListFilters] = useState<PurchaseOrderListParams>(() => defaultListFilters());
  const [lastCreatedOrders, setLastCreatedOrders] = useState<PurchaseOrder[]>([]);
  const [loadingCandidates, setLoadingCandidates] = useState(true);
  const [loadingOrders, setLoadingOrders] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [candidateError, setCandidateError] = useState<string | null>(null);
  const [orderError, setOrderError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);
  const [manualPartner, setManualPartner] = useState<CompanySearchSelection | null>(null);
  const [partnerPriceItems, setPartnerPriceItems] = useState<PartnerPriceItem[]>([]);
  const partnerItemOptions = useMemo(
    () => partnerPriceItems.map(toItemSearchSelection),
    [partnerPriceItems],
  );
  const [manualLines, setManualLines] = useState<ManualPurchaseLine[]>(() => [newManualLine()]);
  const [manualError, setManualError] = useState<string | null>(null);

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
      setSelectedOrderIds(new Set());
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

  const printableOrders = useMemo(
    () => orders.filter((order) => order.status !== 'CANCELLED'),
    [orders],
  );

  const orderExportRows = useMemo(
    () =>
      orders.map((order) => ({
        발주번호: order.orderNo,
        거래처: order.partnerName,
        발주일: order.orderDate,
        출처: order.sourceTypeLabel,
        상태: order.statusLabel,
        라인수: order.lines.length,
        등록일시: formatDateTime(order.createdAt),
      })),
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

  const handlePrint = async (orderIds: number[]) => {
    if (orderIds.length === 0) {
      setOrderError('출력할 발주를 1건 이상 선택해 주세요.');
      return;
    }
    setOrderError(null);
    try {
      await openPurchaseOrderPrint(orderIds);
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

  const loadPartnerPriceItems = useCallback(async (companyId: number, refDate: string): Promise<PartnerPriceItem[]> => {
    const allPrices = await fetchUnitPrices('PURCHASE');
    const filtered = allPrices.filter(
      (unitPrice) => unitPrice.companyId === companyId && isUnitPriceEffective(unitPrice, refDate),
    );
    const items = toPartnerPriceItems(filtered);
    setPartnerPriceItems(items);
    return items;
  }, []);

  useEffect(() => {
    if (!manualPartner) {
      setPartnerPriceItems([]);
      setManualLines([newManualLine()]);
      return;
    }
    void loadPartnerPriceItems(manualPartner.id, orderDate);
    setManualLines([newManualLine()]);
  }, [manualPartner, orderDate, loadPartnerPriceItems]);

  const selectManualLineItem = (index: number, item: ItemSearchSelection | null) => {
    if (!item) {
      setManualLines((prev) =>
        prev.map((row, i) => (i === index ? { ...row, item: null, unitPrice: '' } : row)),
      );
      return;
    }
    const priceItem = partnerPriceItems.find((p) => p.itemId === item.id);
    setManualLines((prev) =>
      prev.map((row, i) =>
        i === index
          ? {
              ...row,
              item,
              unitPrice: priceItem != null ? String(priceItem.unitPrice) : '',
            }
          : row,
      ),
    );
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

  const onCreateManual = async () => {
    if (!manualPartner) {
      setManualError('구매 거래처를 선택하세요.');
      return;
    }
    const createLines: Array<{
      itemId: number;
      orderQty: number;
      unitPrice?: number;
    }> = [];
    for (let index = 0; index < manualLines.length; index += 1) {
      const line = manualLines[index];
      if (!line.item) {
        setManualError(`라인 ${index + 1}: 품목을 선택하세요.`);
        return;
      }
      const orderQty = Number(line.orderQty);
      if (!Number.isFinite(orderQty) || orderQty <= 0) {
        setManualError(`라인 ${index + 1}: 발주수량은 0보다 커야 합니다.`);
        return;
      }
      const unitPrice = line.unitPrice.trim() ? Number(line.unitPrice) : undefined;
      if (unitPrice != null && (!Number.isFinite(unitPrice) || unitPrice < 0)) {
        setManualError(`라인 ${index + 1}: 단가가 올바르지 않습니다.`);
        return;
      }
      createLines.push({
        itemId: line.item.id,
        orderQty,
        unitPrice,
      });
    }
    if (createLines.length === 0) {
      setManualError('발주 라인을 1건 이상 입력하세요.');
      return;
    }

    setSubmitting(true);
    setManualError(null);
    setMessage(null);
    try {
      const draft = await createPurchaseOrder({
        partnerId: manualPartner.id,
        orderDate,
        sourceType: 'MANUAL',
        lines: createLines,
      });
      const confirmed = await confirmPurchaseOrder(draft.id);
      setLastCreatedOrders([confirmed]);
      setListFilters((prev) => ({
        ...prev,
        orderDateFrom: prev.orderDateFrom && prev.orderDateFrom <= orderDate ? prev.orderDateFrom : orderDate,
        orderDateTo: prev.orderDateTo && prev.orderDateTo >= orderDate ? prev.orderDateTo : orderDate,
      }));
      setMessage(`직접 발주 ${confirmed.orderNo}을(를) 등록·확정했습니다.`);
      setManualLines([newManualLine()]);
      await loadOrders();
    } catch (e) {
      setManualError(e instanceof Error ? e.message : '직접 발주 등록 실패');
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
      <header className="page-header">
        <div>
          <h1>구매발주</h1>
          <p>MRP·수주 연동 발주와 직접 발주를 등록·확정합니다.</p>
        </div>
      </header>

      <section className="panel">
        <h2>직접 발주</h2>
        <p className="hint-text">
          MRP·수주 없이 거래처 구매단가에 등록된 품목을 직접 발주합니다. 등록 시 자동으로 확정됩니다.
        </p>
        {manualError && <div className="error">{manualError}</div>}
        <div className="form-grid-wide">
          <CompanySearchField
            label="구매 거래처"
            partnerType="PURCHASE"
            selectedCompany={manualPartner}
            onSelect={setManualPartner}
          />
          <label>
            발주일
            <input type="date" value={orderDate} onChange={(e) => setOrderDate(e.target.value)} />
          </label>
        </div>
        {!manualPartner && (
          <p className="hint-text">구매 거래처를 먼저 선택하면 구매단가 품목이 표시됩니다.</p>
        )}
        {manualPartner && partnerPriceItems.length === 0 && (
          <p className="hint-text">선택한 거래처·발주일에 유효한 구매단가 품목이 없습니다.</p>
        )}
        <table>
          <thead>
            <tr>
              <th>품목</th>
              <th>발주수량</th>
              <th>단가</th>
              <th />
            </tr>
          </thead>
          <tbody>
            {manualLines.map((line, index) => (
              <tr key={line.key}>
                <td>
                  <ItemSearchField
                    label=""
                    items={partnerItemOptions}
                    selectedItem={line.item}
                    disabled={!manualPartner}
                    emptyMessage="일치하는 구매단가 품목이 없습니다."
                    onSelect={(item) => selectManualLineItem(index, item)}
                  />
                </td>
                <td>
                  <input
                    type="number"
                    min={0.0001}
                    step="any"
                    value={line.orderQty}
                    onChange={(e) =>
                      setManualLines((prev) =>
                        prev.map((row, i) => (i === index ? { ...row, orderQty: e.target.value } : row)),
                      )
                    }
                  />
                </td>
                <td>
                  <input
                    type="number"
                    min={0}
                    step="any"
                    placeholder="선택"
                    value={line.unitPrice}
                    onChange={(e) =>
                      setManualLines((prev) =>
                        prev.map((row, i) => (i === index ? { ...row, unitPrice: e.target.value } : row)),
                      )
                    }
                  />
                </td>
                <td>
                  <button
                    type="button"
                    className="btn-action danger"
                    disabled={manualLines.length <= 1 || submitting}
                    onClick={() => setManualLines((prev) => prev.filter((_, i) => i !== index))}
                  >
                    삭제
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
        <div className="form-actions">
          <button type="button" className="secondary" onClick={() => setManualLines((prev) => [...prev, newManualLine()])}>
            라인 추가
          </button>
          <button type="button" disabled={submitting} onClick={() => void onCreateManual()}>
            {submitting ? '처리 중…' : '직접 발주 등록'}
          </button>
        </div>
      </section>

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
                <th>산출번호</th>
                <th>계획번호</th>
                <th>자재품목</th>
                <th>자산분류</th>
                <th>단위</th>
                <th className="num">총소요</th>
                <th className="num">잔여</th>
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
                      <td className="num">{formatQty(row.grossQty)}</td>
                      <td className="num">{formatQty(row.suggestedQty)}</td>
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
                                <th className="num">발주비율</th>
                                <th className="num">발주수량</th>
                                <th className="num">단가</th>
                                <th className="num">금액</th>
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
                                    <td className="num">{formatQty(vendor.orderRate)}%</td>
                                    <td className="num">{formatQty(vendor.orderQty)}</td>
                                    <td className="num">{formatAmount(vendor.unitPrice)}</td>
                                    <td className="num">{formatAmount(vendor.amount)}</td>
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
          </div>
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
                  void handlePrint([order.id]);
                }
              }}
            >
              전체 발주서 출력
            </button>
            <button type="button" className="secondary" onClick={() => setLastCreatedOrders([])}>
              닫기
            </button>
          </div>
          <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>발주번호</th>
                <th>거래처</th>
                <th>발주일</th>
                <th className="num">라인</th>
                <th>관리</th>
              </tr>
            </thead>
            <tbody>
              {lastCreatedOrders.map((order) => (
                <tr key={`created-${order.id}`}>
                  <td>{order.orderNo}</td>
                  <td>{order.partnerName}</td>
                  <td>{order.orderDate}</td>
                  <td className="num">{order.lines.length}</td>
                  <td>
                    <button type="button" className="btn-action" onClick={() => void handlePrint([order.id])}>
                      발주서 출력
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          </div>
        </section>
      )}

      <section className="panel">
        <div className="panel-header-row">
          <h2>발주 목록</h2>
          <div className="inline-actions">
            <button
              type="button"
              className="btn-action"
              disabled={submitting || selectedOrderIds.size === 0}
              onClick={() => void handlePrint([...selectedOrderIds])}
            >
              발주서 발행
            </button>
            <GridExcelExportButton fileBaseName="구매발주목록" disabled={loadingOrders} rows={orderExportRows} />
          </div>
        </div>
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
                <th>거래처</th>
                <th>발주일</th>
                <th>출처</th>
                <th>상태</th>
                <th className="num">라인</th>
                <th>등록일시</th>
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
                  <td>{order.partnerName}</td>
                  <td>{order.orderDate}</td>
                  <td>{order.sourceTypeLabel}</td>
                  <td>{order.statusLabel}</td>
                  <td className="num">{order.lines.length}</td>
                  <td>{formatDateTime(order.createdAt)}</td>
                  <td className="actions">
                    {printable && (
                      <button
                        type="button"
                        className="btn-action"
                        disabled={submitting}
                        onClick={() => void handlePrint([order.id])}
                      >
                        발주서 출력
                      </button>
                    )}
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
                );
              })}
            </tbody>
          </table>
          </div>
        )}
        {orders.some((order) => order.lines.length > 0) && (
          <div className="detail-panel">
            <h3>발주 상세</h3>
            {orders.map((order) => (
              <div key={`detail-${order.id}`} className="sub-panel">
                <h4>
                  {order.orderNo} — {order.partnerName} ({order.statusLabel})
                </h4>
                <div className="table-wrap">
                <table>
                  <thead>
                    <tr>
                      <th>라인</th>
                      <th>품목</th>
                      <th className="num">수량</th>
                      <th className="num">단가</th>
                      <th className="num">금액</th>
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
                        <td className="num">{formatQty(line.orderQty)}</td>
                        <td className="num">{formatAmount(line.unitPrice)}</td>
                        <td className="num">{formatAmount(line.amount)}</td>
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
              </div>
            ))}
          </div>
        )}
      </section>
    </div>
  );
}
