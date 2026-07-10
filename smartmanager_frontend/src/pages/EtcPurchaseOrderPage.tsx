import { useCallback, useEffect, useMemo, useState } from 'react';
import CompanySearchField, { type CompanySearchSelection } from '../components/CompanySearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import {
  createEtcPurchaseOrder,
  deleteEtcPurchaseOrder,
  fetchEtcPurchaseOrders,
  openEtcPurchaseOrderPrint,
  updateEtcPurchaseOrder,
  type EtcPurchaseOrder,
  type EtcPurchaseOrderListParams,
} from '../api/etcPurchase';
import { formatAmount, formatQty } from '../utils/numberFormat';

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

function parseNumber(value: string): number | null {
  const parsed = Number(value);
  return Number.isFinite(parsed) ? parsed : null;
}

function defaultListFilters(today = todayIso()): EtcPurchaseOrderListParams {
  return {
    orderDateFrom: addDaysIso(today, -7),
    orderDateTo: today,
    partnerName: '',
    orderNo: '',
  };
}

const emptyForm = {
  itemName: '',
  unitPrice: '0',
  orderQty: '0',
  requestedDeliveryDate: todayIso(),
};

export default function EtcPurchaseOrderPage() {
  const [rows, setRows] = useState<EtcPurchaseOrder[]>([]);
  const [listFilters, setListFilters] = useState<EtcPurchaseOrderListParams>(() => defaultListFilters());
  const [appliedListFilters, setAppliedListFilters] = useState<EtcPurchaseOrderListParams>(() => defaultListFilters());
  const [form, setForm] = useState(emptyForm);
  const [partner, setPartner] = useState<CompanySearchSelection | null>(null);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [loadingList, setLoadingList] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [formError, setFormError] = useState<string | null>(null);
  const [listError, setListError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);
  const [selectedOrderIds, setSelectedOrderIds] = useState<Set<number>>(new Set());

  const orderExportRows = useMemo(
    () =>
      rows.map((row) => ({
        발주번호: row.orderNo,
        거래처: row.partnerName,
        발주일: row.orderDate,
        품목명: row.itemName,
        단가: row.unitPrice,
        주문량: row.orderQty,
        잔량: row.remainQty,
        총금액: row.amount,
        납기요구일: row.requestedDeliveryDate,
        상태: row.statusLabel,
      })),
    [rows],
  );

  const allOrdersSelected = rows.length > 0 && rows.every((row) => selectedOrderIds.has(row.id));

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
      setListError('출력할 발주를 1건 이상 선택해 주세요.');
      return;
    }
    setListError(null);
    try {
      await openEtcPurchaseOrderPrint(orderIds);
    } catch (e) {
      setListError(e instanceof Error ? e.message : '발주서 출력 실패');
    }
  };

  const totalAmount = useMemo(() => {
    const unitPrice = parseNumber(form.unitPrice) ?? 0;
    const qty = parseNumber(form.orderQty) ?? 0;
    return unitPrice * qty;
  }, [form.unitPrice, form.orderQty]);

  const loadOrders = useCallback(async () => {
    setLoadingList(true);
    setListError(null);
    try {
      setRows(await fetchEtcPurchaseOrders(appliedListFilters));
      setSelectedOrderIds(new Set());
    } catch (e) {
      setListError(e instanceof Error ? e.message : '기타구매발주 목록 조회 실패');
      setRows([]);
    } finally {
      setLoadingList(false);
    }
  }, [appliedListFilters]);

  useEffect(() => {
    void loadOrders();
  }, [loadOrders]);

  const resetForm = () => {
    setForm({ ...emptyForm, requestedDeliveryDate: todayIso() });
    setPartner(null);
    setEditingId(null);
  };

  const onListSearch = () => {
    setAppliedListFilters({ ...listFilters });
  };

  const onResetListFilters = () => {
    const defaults = defaultListFilters();
    setListFilters(defaults);
    setAppliedListFilters(defaults);
  };

  const startEdit = (row: EtcPurchaseOrder) => {
    if (!row.editable) {
      setFormError('대기 상태의 발주만 수정할 수 있습니다.');
      return;
    }
    setEditingId(row.id);
    setForm({
      itemName: row.itemName,
      unitPrice: String(row.unitPrice),
      orderQty: String(row.orderQty),
      requestedDeliveryDate: row.requestedDeliveryDate,
    });
    setPartner({
      id: row.partnerId,
      companyName: row.partnerName,
      businessRegNo: row.partnerBusinessRegNo,
    });
    setFormError(null);
    setMessage(null);
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!partner) {
      setFormError('구매거래처를 선택해 주세요.');
      return;
    }
    const unitPrice = parseNumber(form.unitPrice);
    const orderQty = parseNumber(form.orderQty);
    if (!form.itemName.trim()) {
      setFormError('품목명을 입력해 주세요.');
      return;
    }
    if (unitPrice == null || unitPrice < 0) {
      setFormError('개별 단가를 확인해 주세요.');
      return;
    }
    if (orderQty == null || orderQty <= 0) {
      setFormError('수량은 0보다 커야 합니다.');
      return;
    }

    const orderDate = todayIso();
    setSubmitting(true);
    setFormError(null);
    setMessage(null);
    try {
      const payload = {
        itemName: form.itemName.trim(),
        partnerId: partner.id,
        unitPrice,
        orderQty,
        requestedDeliveryDate: form.requestedDeliveryDate,
      };
      if (editingId != null) {
        await updateEtcPurchaseOrder(editingId, payload);
        setMessage('기타구매발주를 수정했습니다.');
        resetForm();
        await loadOrders();
      } else {
        await createEtcPurchaseOrder({ ...payload, orderDate });
        setMessage('기타구매발주를 등록했습니다.');
        const nextListFilters: EtcPurchaseOrderListParams = {
          ...appliedListFilters,
          orderDateFrom:
            appliedListFilters.orderDateFrom && appliedListFilters.orderDateFrom <= orderDate
              ? appliedListFilters.orderDateFrom
              : orderDate,
          orderDateTo:
            appliedListFilters.orderDateTo && appliedListFilters.orderDateTo >= orderDate
              ? appliedListFilters.orderDateTo
              : orderDate,
        };
        setListFilters(nextListFilters);
        setAppliedListFilters(nextListFilters);
        resetForm();
      }
    } catch (err) {
      setFormError(err instanceof Error ? err.message : editingId != null ? '수정 실패' : '등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (row: EtcPurchaseOrder) => {
    if (!row.editable) {
      setFormError('대기 상태의 발주만 삭제할 수 있습니다.');
      return;
    }
    if (!window.confirm(`「${row.itemName}」 발주를 삭제하시겠습니까?`)) {
      return;
    }
    setFormError(null);
    setMessage(null);
    try {
      await deleteEtcPurchaseOrder(row.id);
      if (editingId === row.id) {
        resetForm();
      }
      setMessage('기타구매발주를 삭제했습니다.');
      await loadOrders();
    } catch (err) {
      setFormError(err instanceof Error ? err.message : '삭제 실패');
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>기타구매발주</h1>
        <p>품목 마스터에 없는 품목을 구매거래처에 발주합니다. 입고 전(대기) 상태만 수정·삭제할 수 있습니다.</p>
      </header>

      <section className="panel">
        <div className="panel-header-row">
          <h2>{editingId != null ? '발주 수정' : '발주 등록'}</h2>
        </div>
        {formError && <div className="error">{formError}</div>}
        <form onSubmit={(e) => void onSubmit(e)}>
          <div
            className="form-grid-wide"
            style={{ gridTemplateColumns: 'minmax(12rem, 2fr) repeat(3, minmax(6rem, 1fr))' }}
          >
            <label>
              품목명
              <input
                value={form.itemName}
                onChange={(e) => setForm((f) => ({ ...f, itemName: e.target.value }))}
                placeholder="자유 입력"
              />
            </label>
            <label>
              개별 단가
              <input
                type="number"
                min={0}
                step="any"
                value={form.unitPrice}
                onChange={(e) => setForm((f) => ({ ...f, unitPrice: e.target.value }))}
              />
            </label>
            <label>
              수량
              <input
                type="number"
                min={0}
                step="any"
                value={form.orderQty}
                onChange={(e) => setForm((f) => ({ ...f, orderQty: e.target.value }))}
              />
            </label>
            <label>
              총금액
              <input type="text" className="readonly" readOnly value={formatAmount(totalAmount)} />
            </label>
          </div>
          <div
            className="form-grid-wide"
            style={{ gridTemplateColumns: 'minmax(16rem, 2fr) minmax(10rem, 1fr)', marginTop: '0.85rem' }}
          >
            <CompanySearchField
              label="거래처"
              partnerType="PURCHASE"
              selectedCompany={partner}
              onSelect={setPartner}
            />
            <label>
              납기요구일
              <input
                type="date"
                value={form.requestedDeliveryDate}
                onChange={(e) => setForm((f) => ({ ...f, requestedDeliveryDate: e.target.value }))}
              />
            </label>
          </div>
          <div className="form-actions">
            <button type="button" className="secondary" onClick={resetForm} disabled={submitting}>
              초기화
            </button>
            <button type="submit" disabled={submitting}>
              {submitting ? '저장 중…' : editingId != null ? '수정' : '등록'}
            </button>
          </div>
        </form>
      </section>

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
            <GridExcelExportButton fileBaseName="기타구매발주목록" disabled={loadingList} rows={orderExportRows} />
          </div>
        </div>
        {message && <p>{message}</p>}
        {listError && <div className="error">{listError}</div>}
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
              placeholder="EPO-"
              onChange={(e) => setListFilters((prev) => ({ ...prev, orderNo: e.target.value }))}
            />
          </label>
          <label>
            품목명
            <input
              type="text"
              value={listFilters.itemName ?? ''}
              onChange={(e) => setListFilters((prev) => ({ ...prev, itemName: e.target.value }))}
            />
          </label>
        </div>
        <div className="form-actions">
          <button type="button" onClick={onListSearch}>
            검색
          </button>
          <button type="button" className="secondary" onClick={onResetListFilters}>
            초기화
          </button>
        </div>
        {loadingList ? (
          <p>불러오는 중…</p>
        ) : rows.length === 0 ? (
          <p>조건에 맞는 기타구매발주가 없습니다.</p>
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
                    disabled={rows.length === 0 || submitting}
                    ref={(el) => {
                      if (el) {
                        el.indeterminate =
                          !allOrdersSelected && rows.some((row) => selectedOrderIds.has(row.id));
                      }
                    }}
                    onChange={(e) => {
                      if (e.target.checked) {
                        setSelectedOrderIds(new Set(rows.map((row) => row.id)));
                      } else {
                        setSelectedOrderIds(new Set());
                      }
                    }}
                  />
                </th>
                <th>발주번호</th>
                <th>거래처</th>
                <th>발주일</th>
                <th>품목명</th>
                <th className="num">단가</th>
                <th className="num">주문량</th>
                <th className="num">잔량</th>
                <th className="num">총금액</th>
                <th>납기요구일</th>
                <th>상태</th>
                <th>관리</th>
              </tr>
            </thead>
            <tbody>
              {rows.map((row) => (
                <tr key={row.id}>
                  <td>
                    <input
                      type="checkbox"
                      aria-label={`${row.orderNo} 선택`}
                      checked={selectedOrderIds.has(row.id)}
                      disabled={submitting}
                      onChange={(e) => toggleOrderSelection(row.id, e.target.checked)}
                    />
                  </td>
                  <td>{row.orderNo}</td>
                  <td>{row.partnerName}</td>
                  <td>{row.orderDate}</td>
                  <td>{row.itemName}</td>
                  <td className="num">{formatAmount(row.unitPrice)}</td>
                  <td className="num">{formatQty(row.orderQty)}</td>
                  <td className="num">{formatQty(row.remainQty)}</td>
                  <td className="num">{formatAmount(row.amount)}</td>
                  <td>{row.requestedDeliveryDate}</td>
                  <td>{row.statusLabel}</td>
                  <td className="actions">
                    <button
                      type="button"
                      className="btn-action"
                      disabled={!row.editable}
                      onClick={() => startEdit(row)}
                    >
                      수정
                    </button>
                    <button
                      type="button"
                      className="btn-action danger"
                      disabled={!row.editable}
                      onClick={() => void onDelete(row)}
                    >
                      삭제
                    </button>
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
