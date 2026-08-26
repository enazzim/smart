import { useCallback, useMemo, useState } from 'react';
import { fetchItemStockMovements, type ItemStockMovementRow } from '../api/stats';
import CompanySearchField, { type CompanySearchSelection } from '../components/CompanySearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import {
  formatInventoryLocation,
  INVENTORY_LOCATION_FILTER_OPTIONS,
} from '../utils/inventoryLocation';
import { ALL_ITEM_CLASSES } from '../utils/itemClassFilters';
import { formatInteger, formatQty } from '../utils/numberFormat';

function referenceTypeLabel(type: string) {
  const map: Record<string, string> = {
    WORK_REPORT: '작업일보',
    MATERIAL_ISSUE: '자재투입',
    PURCHASE_RECEIPT: '구매입고',
    OUTSOURCING_SHIPMENT: '외주출고',
    OUTSOURCING_RECEIPT: '외주입고',
    SALES_SHIPMENT: '영업출고',
    SALES_REVENUE: '매출',
    MISC_STOCK_MOVEMENT: '기타입출고',
    QUALITY_INSPECTION: '품질검사',
  };
  return map[type] ?? type;
}

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function monthStartIso(): string {
  const now = new Date();
  return `${now.getFullYear()}-${String(now.getMonth() + 1).padStart(2, '0')}-01`;
}

export default function ItemIoPage() {
  const [company, setCompany] = useState<CompanySearchSelection | null>(null);
  const [companyClearToken, setCompanyClearToken] = useState(0);
  const [selectedItem, setSelectedItem] = useState<ItemSearchSelection | null>(null);
  const [itemQueryText, setItemQueryText] = useState('');
  const [itemClearToken, setItemClearToken] = useState(0);
  const [locationCode, setLocationCode] = useState('');
  const [dateFrom, setDateFrom] = useState(monthStartIso);
  const [dateTo, setDateTo] = useState(todayIso);
  const [rows, setRows] = useState<ItemStockMovementRow[]>([]);
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
        일자: row.movementDate,
        품번: row.itemNo,
        품명: row.itemName,
        창고: formatInventoryLocation(row.locationCode, {
          outputProcessSequence: row.outputProcessSequence,
          outputProcessName: row.outputProcessName,
        }),
        공정: row.outputProcessName ?? '',
        수불처: row.partnerName ?? '',
        수불사유: referenceTypeLabel(row.referenceType),
        입고: Number(row.inQty),
        출고: Number(row.outQty),
        금액: Number(row.amount),
      })),
    [rows],
  );

  const load = useCallback(async () => {
    if (!('itemId' in itemSearchParams) && !('itemNo' in itemSearchParams)) {
      setError('품목을 선택하거나 품번/품명을 입력해 주세요.');
      return;
    }
    setLoading(true);
    setError(null);
    try {
      setRows(
        await fetchItemStockMovements({
          ...itemSearchParams,
          companyId: company?.id,
          locationCode: locationCode || undefined,
          movementDateFrom: dateFrom || undefined,
          movementDateTo: dateTo || undefined,
        }),
      );
      setSearched(true);
    } catch (e) {
      setError(e instanceof Error ? e.message : '품목별 수불현황을 불러오지 못했습니다.');
      setRows([]);
      setSearched(true);
    } finally {
      setLoading(false);
    }
  }, [itemSearchParams, company, locationCode, dateFrom, dateTo]);

  const reset = () => {
    setCompany(null);
    setCompanyClearToken((n) => n + 1);
    setSelectedItem(null);
    setItemQueryText('');
    setItemClearToken((n) => n + 1);
    setLocationCode('');
    setDateFrom(monthStartIso());
    setDateTo(todayIso());
    setRows([]);
    setError(null);
    setSearched(false);
  };

  return (
    <div className="page">
      <header className="page-header">
        <div>
          <h1>품목별 수불현황</h1>
          <p>품목 기준 입출고 이력(수불처·사유·수량)을 일자 범위로 조회합니다.</p>
        </div>
      </header>

      <section className="filter-panel">
        <ItemSearchField
          label="품목"
          selectedItem={selectedItem}
          onSelect={setSelectedItem}
          onQueryTextChange={setItemQueryText}
          clearToken={itemClearToken}
          allowedClassifications={ALL_ITEM_CLASSES}
        />
        <CompanySearchField
          label="수불처"
          selectedCompany={company}
          onSelect={setCompany}
          clearToken={companyClearToken}
        />
        <label>
          창고
          <select value={locationCode} onChange={(e) => setLocationCode(e.target.value)}>
            {INVENTORY_LOCATION_FILTER_OPTIONS.map((opt) => (
              <option key={opt.value || 'all'} value={opt.value}>
                {opt.label}
              </option>
            ))}
          </select>
        </label>
        <label>
          수불일자 From
          <input type="date" value={dateFrom} onChange={(e) => setDateFrom(e.target.value)} />
        </label>
        <label>
          수불일자 To
          <input type="date" value={dateTo} onChange={(e) => setDateTo(e.target.value)} />
        </label>
        <button type="button" onClick={() => void load()} disabled={loading}>
          {loading ? '조회 중…' : '검색'}
        </button>
        <button type="button" className="secondary" onClick={reset} disabled={loading}>
          초기화
        </button>
        <GridExcelExportButton fileBaseName="품목별수불현황" disabled={loading} rows={exportRows} />
      </section>

      {error && <p className="hint error-text">{error}</p>}
      {!searched && <p className="hint">품목을 지정한 뒤 검색을 눌러 주세요.</p>}
      {searched && !loading && rows.length === 0 && !error && <p className="hint">조회 결과가 없습니다.</p>}

      {rows.length > 0 && (
        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>일자</th>
                <th>품번</th>
                <th>품명</th>
                <th>창고</th>
                <th>공정</th>
                <th>수불처</th>
                <th>수불사유</th>
                <th className="num">입고</th>
                <th className="num">출고</th>
                <th className="num">금액</th>
              </tr>
            </thead>
            <tbody>
              {rows.map((row) => (
                <tr key={row.id}>
                  <td>{row.movementDate}</td>
                  <td>{row.itemNo}</td>
                  <td>{row.itemName}</td>
                  <td>
                    {formatInventoryLocation(row.locationCode, {
                      outputProcessSequence: row.outputProcessSequence,
                      outputProcessName: row.outputProcessName,
                    })}
                  </td>
                  <td>
                    {row.outputProcessName
                      ? `${row.outputProcessSequence != null ? `${row.outputProcessSequence}. ` : ''}${row.outputProcessName}`
                      : '-'}
                  </td>
                  <td>{row.partnerName ?? '-'}</td>
                  <td>{referenceTypeLabel(row.referenceType)}</td>
                  <td className="num">{formatQty(row.inQty)}</td>
                  <td className="num">{formatQty(row.outQty)}</td>
                  <td className="num">{formatInteger(row.amount)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
