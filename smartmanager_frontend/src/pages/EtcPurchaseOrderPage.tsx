import { useCallback, useEffect, useMemo, useState } from 'react';
import CompanySearchField, { type CompanySearchSelection } from '../components/CompanySearchField';
import {
  createEtcPurchaseOrder,
  deleteEtcPurchaseOrder,
  fetchEtcPurchaseOrders,
  updateEtcPurchaseOrder,
  type EtcPurchaseOrder,
  type EtcPurchaseOrderListParams,
} from '../api/etcPurchase';

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function formatQty(value: number): string {
  return Number.isInteger(value) ? String(value) : value.toLocaleString(undefined, { maximumFractionDigits: 4 });
}

function formatAmount(value: number): string {
  return value.toLocaleString(undefined, { maximumFractionDigits: 2 });
}

function parseNumber(value: string): number | null {
  const parsed = Number(value);
  return Number.isFinite(parsed) ? parsed : null;
}

const emptyForm = {
  itemName: '',
  unitPrice: '0',
  orderQty: '0',
  requestedDeliveryDate: todayIso(),
};

export default function EtcPurchaseOrderPage() {
  const [rows, setRows] = useState<EtcPurchaseOrder[]>([]);
  const [filters, setFilters] = useState<EtcPurchaseOrderListParams>({});
  const [form, setForm] = useState(emptyForm);
  const [partner, setPartner] = useState<CompanySearchSelection | null>(null);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const totalAmount = useMemo(() => {
    const unitPrice = parseNumber(form.unitPrice) ?? 0;
    const qty = parseNumber(form.orderQty) ?? 0;
    return unitPrice * qty;
  }, [form.unitPrice, form.orderQty]);

  const load = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      setRows(await fetchEtcPurchaseOrders(filters));
    } catch (e) {
      setError(e instanceof Error ? e.message : '기타구매발주 목록 조회 실패');
      setRows([]);
    } finally {
      setLoading(false);
    }
  }, [filters]);

  useEffect(() => {
    void load();
  }, [load]);

  const resetForm = () => {
    setForm({ ...emptyForm, requestedDeliveryDate: todayIso() });
    setPartner(null);
    setEditingId(null);
  };

  const startEdit = (row: EtcPurchaseOrder) => {
    if (!row.editable) {
      setError('대기 상태의 발주만 수정할 수 있습니다.');
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
    setError(null);
    setMessage(null);
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!partner) {
      setError('구매거래처를 선택해 주세요.');
      return;
    }
    const unitPrice = parseNumber(form.unitPrice);
    const orderQty = parseNumber(form.orderQty);
    if (!form.itemName.trim()) {
      setError('품목명을 입력해 주세요.');
      return;
    }
    if (unitPrice == null || unitPrice < 0) {
      setError('개별 단가를 확인해 주세요.');
      return;
    }
    if (orderQty == null || orderQty <= 0) {
      setError('수량은 0보다 커야 합니다.');
      return;
    }

    setSubmitting(true);
    setError(null);
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
      } else {
        await createEtcPurchaseOrder({ ...payload, orderDate: todayIso() });
        setMessage('기타구매발주를 등록했습니다.');
      }
      resetForm();
      await load();
    } catch (err) {
      setError(err instanceof Error ? err.message : editingId != null ? '수정 실패' : '등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (row: EtcPurchaseOrder) => {
    if (!row.editable) {
      setError('대기 상태의 발주만 삭제할 수 있습니다.');
      return;
    }
    if (!window.confirm(`「${row.itemName}」 발주를 삭제하시겠습니까?`)) {
      return;
    }
    setError(null);
    setMessage(null);
    try {
      await deleteEtcPurchaseOrder(row.id);
      if (editingId === row.id) {
        resetForm();
      }
      setMessage('기타구매발주를 삭제했습니다.');
      await load();
    } catch (err) {
      setError(err instanceof Error ? err.message : '삭제 실패');
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>기타구매발주</h1>
        <p>품목 마스터에 없는 품목을 구매거래처에 발주합니다. 입고 전(대기) 상태만 수정·삭제할 수 있습니다.</p>
      </header>

      {error && <p className="error-banner">{error}</p>}
      {message && <p className="success-banner">{message}</p>}

      <section className="panel">
        <div className="panel-header-row">
          <h2>{editingId != null ? '발주 수정' : '발주 등록'}</h2>
        </div>
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

      <section className="filter-panel">
        <label>
          품목명
          <input
            value={filters.itemName ?? ''}
            onChange={(e) => setFilters((f) => ({ ...f, itemName: e.target.value }))}
          />
        </label>
        <label>
          거래처
          <input
            value={filters.partnerName ?? ''}
            onChange={(e) => setFilters((f) => ({ ...f, partnerName: e.target.value }))}
          />
        </label>
        <label>
          납기요구일(부터)
          <input
            type="date"
            value={filters.deliveryFrom ?? ''}
            onChange={(e) => setFilters((f) => ({ ...f, deliveryFrom: e.target.value }))}
          />
        </label>
        <label>
          납기요구일(까지)
          <input
            type="date"
            value={filters.deliveryTo ?? ''}
            onChange={(e) => setFilters((f) => ({ ...f, deliveryTo: e.target.value }))}
          />
        </label>
        <button type="button" onClick={() => void load()}>
          조회
        </button>
      </section>

      {loading ? (
        <p>불러오는 중…</p>
      ) : (
        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>발주번호</th>
                <th>품목명</th>
                <th>거래처</th>
                <th>단가</th>
                <th>주문량</th>
                <th>잔량</th>
                <th>총금액</th>
                <th>납기요구일</th>
                <th>상태</th>
                <th />
              </tr>
            </thead>
            <tbody>
              {rows.length === 0 ? (
                <tr>
                  <td colSpan={10}>등록된 기타구매발주가 없습니다.</td>
                </tr>
              ) : (
                rows.map((row) => (
                  <tr key={row.id}>
                    <td>{row.orderNo}</td>
                    <td>{row.itemName}</td>
                    <td>{row.partnerName}</td>
                    <td>{formatAmount(row.unitPrice)}</td>
                    <td>{formatQty(row.orderQty)}</td>
                    <td>{formatQty(row.remainQty)}</td>
                    <td>{formatAmount(row.amount)}</td>
                    <td>{row.requestedDeliveryDate}</td>
                    <td>{row.statusLabel}</td>
                    <td className="actions">
                      <button type="button" className="secondary" disabled={!row.editable} onClick={() => startEdit(row)}>
                        수정
                      </button>
                      <button type="button" className="danger" disabled={!row.editable} onClick={() => void onDelete(row)}>
                        삭제
                      </button>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
