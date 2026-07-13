import { useCallback, useMemo, useState } from 'react';
import {
  fetchVendorPurchaseTotals,
  type VendorPurchaseDivision,
  type VendorPurchaseTotal,
} from '../api/stats';
import CompanySearchField, {
  PURCHASE_OUTSOURCE_PARTNER_ROLES,
  type CompanySearchSelection,
} from '../components/CompanySearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import { formatInteger, formatQty } from '../utils/numberFormat';

function divisionLabel(division: string) {
  switch (division) {
    case 'PURCHASE':
      return '구매';
    case 'OUTSOURCE':
      return '외주';
    case 'ETC':
      return '기타구매';
    default:
      return division;
  }
}

export default function VendorPurchaseTotalPage() {
  const [company, setCompany] = useState<CompanySearchSelection | null>(null);
  const [companyClearToken, setCompanyClearToken] = useState(0);
  const [selectedItem, setSelectedItem] = useState<ItemSearchSelection | null>(null);
  const [itemQueryText, setItemQueryText] = useState('');
  const [itemClearToken, setItemClearToken] = useState(0);
  const [fiscalYear, setFiscalYear] = useState(String(new Date().getFullYear()));
  const [fiscalMonth, setFiscalMonth] = useState(String(new Date().getMonth() + 1));
  const [division, setDivision] = useState<VendorPurchaseDivision>('ALL');
  const [rows, setRows] = useState<VendorPurchaseTotal[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [searched, setSearched] = useState(false);

  const itemSearchParams = useMemo(() => {
    if (selectedItem) return { itemId: selectedItem.id };
    const text = itemQueryText.trim();
    return text ? { itemNo: text } : {};
  }, [selectedItem, itemQueryText]);

  const exportRows = useMemo(
    () =>
      rows.map((row) => ({
        매입처: row.companyName,
        구분: divisionLabel(row.division),
        품번: row.itemNo ?? '',
        품명: row.itemName,
        수량: Number(row.purchaseQty),
        단가: Number(row.unitPrice),
        금액: Number(row.amount),
        년: row.fiscalYear,
        월: row.fiscalMonth,
      })),
    [rows],
  );

  const load = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const year = Number(fiscalYear);
      const month = Number(fiscalMonth);
      setRows(
        await fetchVendorPurchaseTotals({
          companyId: company?.id,
          ...itemSearchParams,
          fiscalYear: Number.isFinite(year) ? year : undefined,
          fiscalMonth: Number.isFinite(month) && month > 0 ? month : undefined,
          division,
        }),
      );
      setSearched(true);
    } catch (e) {
      setError(e instanceof Error ? e.message : '매입처별 집계를 불러오지 못했습니다.');
      setRows([]);
      setSearched(true);
    } finally {
      setLoading(false);
    }
  }, [company, itemSearchParams, fiscalYear, fiscalMonth, division]);

  const reset = () => {
    const d = new Date();
    setCompany(null);
    setCompanyClearToken((n) => n + 1);
    setSelectedItem(null);
    setItemQueryText('');
    setItemClearToken((n) => n + 1);
    setFiscalYear(String(d.getFullYear()));
    setFiscalMonth(String(d.getMonth() + 1));
    setDivision('ALL');
    setRows([]);
    setError(null);
    setSearched(false);
  };

  return (
    <div className="page">
      <header className="page-header">
        <div>
          <h1>매입처별 집계</h1>
          <p>승인된 구매·외주·기타구매 이력을 매입처·품목·회계월 기준으로 집계합니다.</p>
        </div>
      </header>

      <section className="filter-panel">
        <CompanySearchField
          label="매입처"
          partnerTypes={PURCHASE_OUTSOURCE_PARTNER_ROLES}
          selectedCompany={company}
          onSelect={setCompany}
          clearToken={companyClearToken}
        />
        <ItemSearchField
          label="품목"
          selectedItem={selectedItem}
          onSelect={setSelectedItem}
          onQueryTextChange={setItemQueryText}
          clearToken={itemClearToken}
        />
        <label>
          구분
          <select value={division} onChange={(e) => setDivision(e.target.value as VendorPurchaseDivision)}>
            <option value="ALL">전체</option>
            <option value="PURCHASE">구매</option>
            <option value="OUTSOURCE">외주</option>
            <option value="ETC">기타구매</option>
          </select>
        </label>
        <label>
          년도
          <input type="number" value={fiscalYear} onChange={(e) => setFiscalYear(e.target.value)} />
        </label>
        <label>
          월
          <select value={fiscalMonth} onChange={(e) => setFiscalMonth(e.target.value)}>
            <option value="">전체</option>
            {Array.from({ length: 12 }, (_, i) => i + 1).map((m) => (
              <option key={m} value={m}>
                {m}
              </option>
            ))}
          </select>
        </label>
        <button type="button" onClick={() => void load()} disabled={loading}>
          {loading ? '조회 중…' : '검색'}
        </button>
        <button type="button" className="secondary" onClick={reset} disabled={loading}>
          초기화
        </button>
        <GridExcelExportButton fileBaseName="매입처별집계" disabled={loading} rows={exportRows} />
      </section>

      {error && <p className="hint error-text">{error}</p>}
      {!searched && <p className="hint">검색 조건 입력 후 검색을 눌러 주세요.</p>}
      {searched && !loading && rows.length === 0 && !error && <p className="hint">조회 결과가 없습니다.</p>}

      {rows.length > 0 && (
        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>매입처</th>
                <th>구분</th>
                <th>품번</th>
                <th>품명</th>
                <th className="num">수량</th>
                <th className="num">단가</th>
                <th className="num">금액</th>
                <th className="num">년</th>
                <th className="num">월</th>
              </tr>
            </thead>
            <tbody>
              {rows.map((row, idx) => (
                <tr
                  key={`${row.companyId}-${row.itemId ?? row.itemName}-${row.division}-${row.fiscalYear}-${row.fiscalMonth}-${idx}`}
                >
                  <td>{row.companyName}</td>
                  <td>{divisionLabel(row.division)}</td>
                  <td>{row.itemNo ?? '-'}</td>
                  <td>{row.itemName}</td>
                  <td className="num">{formatQty(row.purchaseQty)}</td>
                  <td className="num">{formatInteger(row.unitPrice)}</td>
                  <td className="num">{formatInteger(row.amount)}</td>
                  <td className="num">{row.fiscalYear}</td>
                  <td className="num">{row.fiscalMonth}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
