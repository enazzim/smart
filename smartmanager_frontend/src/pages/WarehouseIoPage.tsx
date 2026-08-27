import { useCallback, useMemo, useState } from 'react';
import { fetchWarehouseMonthlyIo, type WarehouseMonthlyIo } from '../api/stats';
import GridExcelExportButton from '../components/GridExcelExportButton';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import {
  formatInventoryLocation,
  INVENTORY_LOCATION_CODES,
  INVENTORY_LOCATION_LABEL,
} from '../utils/inventoryLocation';
import { formatQty } from '../utils/numberFormat';

export default function WarehouseIoPage() {
  const [locationCode, setLocationCode] = useState<string>('RAW');
  const [selectedItem, setSelectedItem] = useState<ItemSearchSelection | null>(null);
  const [itemQueryText, setItemQueryText] = useState('');
  const [itemClearToken, setItemClearToken] = useState(0);
  const [fiscalYear, setFiscalYear] = useState(String(new Date().getFullYear()));
  const [fiscalMonth, setFiscalMonth] = useState(String(new Date().getMonth() + 1));
  const [rows, setRows] = useState<WarehouseMonthlyIo[]>([]);
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
        품번: row.itemNo,
        품명: row.itemName,
        창고: formatInventoryLocation(row.locationCode, {
          outputProcessSequence: row.outputProcessSequence,
          outputProcessName: row.outputProcessName,
        }),
        공정: row.outputProcessName ?? '',
        전월재고: Number(row.carryInQty),
        입고: Number(row.inQty),
        출고: Number(row.outQty),
        월말재고: Number(row.endingQty),
      })),
    [rows],
  );

  const load = useCallback(async () => {
    if (!locationCode) {
      setError('창고를 선택해 주세요.');
      return;
    }
    setLoading(true);
    setError(null);
    try {
      const year = Number(fiscalYear);
      const month = Number(fiscalMonth);
      const now = new Date();
      setRows(
        await fetchWarehouseMonthlyIo({
          locationCode,
          ...itemSearchParams,
          fiscalYear: Number.isFinite(year) ? year : now.getFullYear(),
          fiscalMonth: Number.isFinite(month) && month > 0 ? month : now.getMonth() + 1,
        }),
      );
      setSearched(true);
    } catch (e) {
      setError(e instanceof Error ? e.message : '창고별 수불현황을 불러오지 못했습니다.');
      setRows([]);
      setSearched(true);
    } finally {
      setLoading(false);
    }
  }, [locationCode, itemSearchParams, fiscalYear, fiscalMonth]);

  const reset = () => {
    const d = new Date();
    setLocationCode('RAW');
    setSelectedItem(null);
    setItemQueryText('');
    setItemClearToken((n) => n + 1);
    setFiscalYear(String(d.getFullYear()));
    setFiscalMonth(String(d.getMonth() + 1));
    setRows([]);
    setError(null);
    setSearched(false);
  };

  return (
    <div className="page">
      <header className="page-header">
        <div>
          <h1>창고별 수불현황</h1>
          <p>선택한 창고·회계월의 전월이월·입고·출고·월말재고를 품목 단위로 조회합니다.</p>
        </div>
      </header>

      <section className="filter-panel">
        <label>
          창고
          <select value={locationCode} onChange={(e) => setLocationCode(e.target.value)}>
            {INVENTORY_LOCATION_CODES.map((code) => (
              <option key={code} value={code}>
                {INVENTORY_LOCATION_LABEL[code]}
              </option>
            ))}
          </select>
        </label>
        <ItemSearchField
          label="품목"
          selectedItem={selectedItem}
          onSelect={setSelectedItem}
          onQueryTextChange={setItemQueryText}
          clearToken={itemClearToken}
        />
        <label>
          년도
          <input type="number" value={fiscalYear} onChange={(e) => setFiscalYear(e.target.value)} />
        </label>
        <label>
          월
          <select value={fiscalMonth} onChange={(e) => setFiscalMonth(e.target.value)}>
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
        <GridExcelExportButton fileBaseName="창고별수불현황" disabled={loading} rows={exportRows} />
      </section>

      {error && <p className="hint error-text">{error}</p>}
      {!searched && <p className="hint">검색 조건 입력 후 검색을 눌러 주세요.</p>}
      {searched && !loading && rows.length === 0 && !error && <p className="hint">조회 결과가 없습니다.</p>}

      {rows.length > 0 && (
        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>품번</th>
                <th>품명</th>
                <th>창고</th>
                <th>공정</th>
                <th className="num">전월재고</th>
                <th className="num">입고</th>
                <th className="num">출고</th>
                <th className="num">월말재고</th>
              </tr>
            </thead>
            <tbody>
              {rows.map((row, idx) => (
                <tr
                  key={`${row.itemId}-${row.locationCode}-${row.outputProcessSequence ?? 'x'}-${row.fiscalYear}-${row.fiscalMonth}-${idx}`}
                >
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
                  <td className="num">{formatQty(row.carryInQty)}</td>
                  <td className="num">{formatQty(row.inQty)}</td>
                  <td className="num">{formatQty(row.outQty)}</td>
                  <td className="num">{formatQty(row.endingQty)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
