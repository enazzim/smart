import { useMemo, useState } from 'react';
import {
  fetchOrderVsReceipt,
  type OrderVsReceiptDivision,
  type OrderVsReceiptRow,
} from '../api/stats';
import CompanySearchField, {
  PURCHASE_OUTSOURCE_PARTNER_ROLES,
  type CompanySearchSelection,
} from '../components/CompanySearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import type { PropertyClassification } from '../api/item';
import { formatAmount, formatQty } from '../utils/numberFormat';

const ALL_ITEM_CLASSES: PropertyClassification[] = ['원자재', '제품', '상품', '공정품', '부자재'];

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

function divisionLabel(division: string) {
  switch (division) {
    case 'PURCHASE':
      return '구매';
    case 'OUTSOURCE':
      return '외주';
    default:
      return division;
  }
}

export default function OrderVsReceiptPage() {
  const [company, setCompany] = useState<CompanySearchSelection | null>(null);
  const [companyClearToken, setCompanyClearToken] = useState(0);
  const [selectedItem, setSelectedItem] = useState<ItemSearchSelection | null>(null);
  const [itemClearToken, setItemClearToken] = useState(0);
  const [orderDateFrom, setOrderDateFrom] = useState(addDaysIso(todayIso(), -30));
  const [orderDateTo, setOrderDateTo] = useState(todayIso());
  const [receiptDateFrom, setReceiptDateFrom] = useState('');
  const [receiptDateTo, setReceiptDateTo] = useState('');
  const [division, setDivision] = useState<OrderVsReceiptDivision>('ALL');
  const [rows, setRows] = useState<OrderVsReceiptRow[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [searched, setSearched] = useState(false);

  const exportRows = useMemo(
    () =>
      rows.map((row) => ({
        구분: divisionLabel(row.division),
        기종: row.modelType,
        거래처: row.companyName,
        품목번호: row.itemNo,
        품목명: row.itemName,
        공정: row.processName,
        단위: row.unit,
        규격: row.standard,
        발주일: row.orderDate,
        납기요구일: row.requestedDeliveryDate ?? '',
        발주량: Number(row.orderQty),
        발주단가: Number(row.orderUnitPrice),
        발주금액: Number(row.orderAmount),
        입고량: Number(row.receivedQty),
        검사대기: Number(row.waitingInspectionQty),
        입고금액: Number(row.receiptAmount),
        최종입고일: row.lastReceiptDate ?? '',
        미입고량: Number(row.remainQty),
        미입고금액: Number(row.remainAmount),
        현재고: Number(row.currentStockQty),
      })),
    [rows],
  );

  const resetFilters = () => {
    setCompany(null);
    setCompanyClearToken((n) => n + 1);
    setSelectedItem(null);
    setItemClearToken((n) => n + 1);
    setOrderDateFrom(addDaysIso(todayIso(), -30));
    setOrderDateTo(todayIso());
    setReceiptDateFrom('');
    setReceiptDateTo('');
    setDivision('ALL');
    setError(null);
  };

  const onSearch = async () => {
    setLoading(true);
    setError(null);
    try {
      setRows(
        await fetchOrderVsReceipt({
          companyId: company?.id,
          companyName: company?.companyName,
          itemId: selectedItem?.id,
          itemNo: selectedItem?.itemNo,
          itemName: selectedItem?.itemName,
          orderDateFrom: orderDateFrom || undefined,
          orderDateTo: orderDateTo || undefined,
          receiptDateFrom: receiptDateFrom || undefined,
          receiptDateTo: receiptDateTo || undefined,
          division,
        }),
      );
      setSearched(true);
    } catch (e) {
      setError(e instanceof Error ? e.message : '발주대비입고 조회에 실패했습니다.');
      setRows([]);
      setSearched(true);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>발주대비입고</h1>
        <p>구매·외주 발주 대비 입고 진행과 발주 품목의 현재고를 확인합니다.</p>
      </header>

      <section className="filter-panel">
        <CompanySearchField
          label="거래처"
          partnerTypes={PURCHASE_OUTSOURCE_PARTNER_ROLES}
          selectedCompany={company}
          onSelect={setCompany}
          clearToken={companyClearToken}
        />
        <ItemSearchField
          label="품목"
          selectedItem={selectedItem}
          onSelect={setSelectedItem}
          allowedClassifications={ALL_ITEM_CLASSES}
          clearToken={itemClearToken}
        />
        <label>
          발주일(부터)
          <input type="date" value={orderDateFrom} onChange={(e) => setOrderDateFrom(e.target.value)} />
        </label>
        <label>
          발주일(까지)
          <input type="date" value={orderDateTo} onChange={(e) => setOrderDateTo(e.target.value)} />
        </label>
        <label>
          입고일(부터)
          <input type="date" value={receiptDateFrom} onChange={(e) => setReceiptDateFrom(e.target.value)} />
        </label>
        <label>
          입고일(까지)
          <input type="date" value={receiptDateTo} onChange={(e) => setReceiptDateTo(e.target.value)} />
        </label>
        <label>
          구분
          <select value={division} onChange={(e) => setDivision(e.target.value as OrderVsReceiptDivision)}>
            <option value="ALL">전체</option>
            <option value="PURCHASE">구매</option>
            <option value="OUTSOURCE">외주</option>
          </select>
        </label>
        <div className="form-actions">
          <button type="button" onClick={() => void onSearch()} disabled={loading}>
            {loading ? '조회 중…' : '검색'}
          </button>
          <button type="button" className="secondary" onClick={resetFilters} disabled={loading}>
            초기화
          </button>
        </div>
      </section>

      {error && <p className="error-banner">{error}</p>}

      <section className="panel">
        <div className="panel-header-row">
          <h2>검색결과</h2>
          <GridExcelExportButton fileBaseName="발주대비입고" disabled={!searched || loading} rows={exportRows} />
        </div>
        {!searched ? (
          <p className="hint-text">조건을 입력한 뒤 검색 버튼을 눌러 주세요.</p>
        ) : loading ? (
          <p>불러오는 중…</p>
        ) : rows.length === 0 ? (
          <p>조건에 맞는 발주 라인이 없습니다.</p>
        ) : (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>구분</th>
                  <th>기종</th>
                  <th>거래처</th>
                  <th>품목번호</th>
                  <th>품목명</th>
                  <th>공정</th>
                  <th>단위</th>
                  <th>규격</th>
                  <th>발주일</th>
                  <th className="num">발주량</th>
                  <th className="num">발주단가</th>
                  <th className="num">발주금액</th>
                  <th className="num">입고량</th>
                  <th className="num">검사대기</th>
                  <th className="num">입고금액</th>
                  <th>최종입고일</th>
                  <th className="num">미입고량</th>
                  <th className="num">현재고</th>
                </tr>
              </thead>
              <tbody>
                {rows.map((row) => (
                  <tr key={`${row.division}-${row.orderLineId}`}>
                    <td>{divisionLabel(row.division)}</td>
                    <td>{row.modelType || '—'}</td>
                    <td>{row.companyName}</td>
                    <td>{row.itemNo}</td>
                    <td>{row.itemName}</td>
                    <td>{row.processName || '—'}</td>
                    <td>{row.unit || '—'}</td>
                    <td>{row.standard || '—'}</td>
                    <td>{row.orderDate}</td>
                    <td className="num">{formatQty(row.orderQty)}</td>
                    <td className="num">{formatAmount(row.orderUnitPrice)}</td>
                    <td className="num">{formatAmount(row.orderAmount)}</td>
                    <td className="num">{formatQty(row.receivedQty)}</td>
                    <td className="num">{formatQty(row.waitingInspectionQty)}</td>
                    <td className="num">{formatAmount(row.receiptAmount)}</td>
                    <td>{row.lastReceiptDate || '—'}</td>
                    <td className="num">{formatQty(row.remainQty)}</td>
                    <td className="num">{formatQty(row.currentStockQty)}</td>
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
