import { useCallback, useMemo, useState } from 'react';
import {
  fetchVendorPurchaseStatus,
  type VendorPurchaseDivision,
  type VendorPurchaseStatusRow,
} from '../api/stats';
import CompanySearchField, {
  PURCHASE_OUTSOURCE_PARTNER_ROLES,
  type CompanySearchSelection,
} from '../components/CompanySearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import type { PropertyClassification } from '../api/item';
import { currentFiscalYearMonth } from '../utils/fiscalCalendar';
import { useMaterialIssueSetting } from '../context/MaterialIssueSettingContext';
import { formatAmount, formatQty } from '../utils/numberFormat';

const ALL_ITEM_CLASSES: PropertyClassification[] = ['원자재', '제품', '상품', '공정품'];

type DisplayRow =
  | { kind: 'data'; row: VendorPurchaseStatusRow }
  | {
      kind: 'subtotal';
      companyId: number;
      companyName: string;
      receiptQty: number;
      amount: number;
    }
  | { kind: 'total'; receiptQty: number; amount: number };

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
    case 'ETC':
      return '기타';
    case 'CLAIM':
      return '공제';
    default:
      return division;
  }
}

function buildDisplayRows(rows: VendorPurchaseStatusRow[]): DisplayRow[] {
  if (rows.length === 0) {
    return [];
  }
  const result: DisplayRow[] = [];
  let groupCompanyId = rows[0].companyId;
  let groupCompanyName = rows[0].companyName;
  let groupQty = 0;
  let groupAmount = 0;
  let totalQty = 0;
  let totalAmount = 0;

  const flushSubtotal = () => {
    result.push({
      kind: 'subtotal',
      companyId: groupCompanyId,
      companyName: groupCompanyName,
      receiptQty: groupQty,
      amount: groupAmount,
    });
  };

  for (const row of rows) {
    if (row.companyId !== groupCompanyId) {
      flushSubtotal();
      groupCompanyId = row.companyId;
      groupCompanyName = row.companyName;
      groupQty = 0;
      groupAmount = 0;
    }
    result.push({ kind: 'data', row });
    groupQty += Number(row.receiptQty);
    groupAmount += Number(row.amount);
    totalQty += Number(row.receiptQty);
    totalAmount += Number(row.amount);
  }
  flushSubtotal();
  result.push({ kind: 'total', receiptQty: totalQty, amount: totalAmount });
  return result;
}

export default function VendorPurchaseStatusPage() {
  const { fiscalCutoverSetting } = useMaterialIssueSetting();
  const currentPeriod = currentFiscalYearMonth(fiscalCutoverSetting);

  const [company, setCompany] = useState<CompanySearchSelection | null>(null);
  const [companyClearToken, setCompanyClearToken] = useState(0);
  const [selectedItem, setSelectedItem] = useState<ItemSearchSelection | null>(null);
  const [itemNoQuery, setItemNoQuery] = useState('');
  const [itemNameQuery, setItemNameQuery] = useState('');
  const [itemClearToken, setItemClearToken] = useState(0);
  const [receiptDateFrom, setReceiptDateFrom] = useState(addDaysIso(todayIso(), -30));
  const [receiptDateTo, setReceiptDateTo] = useState(todayIso());
  const [fiscalYear, setFiscalYear] = useState(String(currentPeriod.fiscalYear));
  const [fiscalMonth, setFiscalMonth] = useState(String(currentPeriod.fiscalMonth));
  const [division, setDivision] = useState<VendorPurchaseDivision>('ALL');
  const [rows, setRows] = useState<VendorPurchaseStatusRow[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [searched, setSearched] = useState(false);

  const displayRows = useMemo(() => buildDisplayRows(rows), [rows]);

  const exportRows = useMemo(
    () =>
      displayRows.map((entry) => {
        if (entry.kind === 'data') {
          const row = entry.row;
          return {
            매입년: row.fiscalYear,
            매입월: row.fiscalMonth,
            거래처명: row.companyName,
            품목번호: row.itemNo,
            품목명: row.itemName,
            도면번호: row.modelType,
            공정명: row.processName,
            입고일자: row.receiptDate,
            현재고: Number(row.currentStockQty),
            입고량: Number(row.receiptQty),
            단가: Number(row.unitPrice),
            '총 금액': Number(row.amount),
            구분: divisionLabel(row.division),
          };
        }
        if (entry.kind === 'subtotal') {
          return {
            매입년: '',
            매입월: '',
            거래처명: `${entry.companyName} 소계`,
            품목번호: '',
            품목명: '',
            도면번호: '',
            공정명: '',
            입고일자: '',
            현재고: '',
            입고량: entry.receiptQty,
            단가: '',
            '총 금액': entry.amount,
            구분: '',
          };
        }
        return {
          매입년: '',
          매입월: '',
          거래처명: '합계',
          품목번호: '',
          품목명: '',
          도면번호: '',
          공정명: '',
          입고일자: '',
          현재고: '',
          입고량: entry.receiptQty,
          단가: '',
          '총 금액': entry.amount,
          구분: '',
        };
      }),
    [displayRows],
  );

  const load = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const year = Number(fiscalYear);
      const month = Number(fiscalMonth);
      setRows(
        await fetchVendorPurchaseStatus({
          companyId: company?.id,
          itemId: selectedItem?.id,
          itemNo: selectedItem ? undefined : itemNoQuery.trim() || undefined,
          itemName: selectedItem ? undefined : itemNameQuery.trim() || undefined,
          receiptDateFrom: receiptDateFrom || undefined,
          receiptDateTo: receiptDateTo || undefined,
          fiscalYear: Number.isFinite(year) && year > 0 ? year : undefined,
          fiscalMonth: Number.isFinite(month) && month > 0 ? month : undefined,
          division,
        }),
      );
      setSearched(true);
    } catch (e) {
      setError(e instanceof Error ? e.message : '매입처별 매입현황을 불러오지 못했습니다.');
      setRows([]);
      setSearched(true);
    } finally {
      setLoading(false);
    }
  }, [
    company,
    selectedItem,
    itemNoQuery,
    itemNameQuery,
    receiptDateFrom,
    receiptDateTo,
    fiscalYear,
    fiscalMonth,
    division,
  ]);

  const reset = () => {
    const period = currentFiscalYearMonth(fiscalCutoverSetting);
    setCompany(null);
    setCompanyClearToken((n) => n + 1);
    setSelectedItem(null);
    setItemNoQuery('');
    setItemNameQuery('');
    setItemClearToken((n) => n + 1);
    setReceiptDateFrom(addDaysIso(todayIso(), -30));
    setReceiptDateTo(todayIso());
    setFiscalYear(String(period.fiscalYear));
    setFiscalMonth(String(period.fiscalMonth));
    setDivision('ALL');
    setRows([]);
    setError(null);
    setSearched(false);
  };

  return (
    <div className="page">
      <header className="page-header">
        <div>
          <h1>매입처별 매입현황</h1>
          <p>승인된 구매·외주·기타구매 입고 이력을 매입처별로 조회합니다. 거래처 소계와 합계를 함께 표시합니다.</p>
        </div>
      </header>

      <section className="filter-panel vendor-purchase-status-filter">
        <div className="vendor-purchase-status-filter-row">
          <CompanySearchField
            label="거래처명"
            partnerTypes={PURCHASE_OUTSOURCE_PARTNER_ROLES}
            selectedCompany={company}
            onSelect={setCompany}
            clearToken={companyClearToken}
          />
          <ItemSearchField
            label="품목번호"
            selectedItem={selectedItem}
            onSelect={setSelectedItem}
            onQueryTextChange={setItemNoQuery}
            clearToken={itemClearToken}
            allowedClassifications={ALL_ITEM_CLASSES}
            displayMode="itemNo"
            placeholder="품목번호 입력"
          />
          <ItemSearchField
            label="품목명"
            selectedItem={selectedItem}
            onSelect={setSelectedItem}
            onQueryTextChange={setItemNameQuery}
            clearToken={itemClearToken}
            allowedClassifications={ALL_ITEM_CLASSES}
            displayMode="itemName"
            placeholder="품목명 입력"
          />
          <label>
            입고일자 From
            <input type="date" value={receiptDateFrom} onChange={(e) => setReceiptDateFrom(e.target.value)} />
          </label>
          <label>
            To
            <input type="date" value={receiptDateTo} onChange={(e) => setReceiptDateTo(e.target.value)} />
          </label>
        </div>
        <div className="vendor-purchase-status-filter-row">
          <label className="vendor-purchase-status-field-compact">
            구분
            <select value={division} onChange={(e) => setDivision(e.target.value as VendorPurchaseDivision)}>
              <option value="ALL">전체</option>
              <option value="PURCHASE">구매</option>
              <option value="OUTSOURCE">외주</option>
              <option value="ETC">기타</option>
              <option value="CLAIM">공제</option>
            </select>
          </label>
          <label className="vendor-purchase-status-field-year">
            매입년도
            <input type="number" value={fiscalYear} onChange={(e) => setFiscalYear(e.target.value)} />
          </label>
          <label className="vendor-purchase-status-field-compact">
            매입월
            <select value={fiscalMonth} onChange={(e) => setFiscalMonth(e.target.value)}>
              <option value="">전체</option>
              {Array.from({ length: 12 }, (_, i) => i + 1).map((m) => (
                <option key={m} value={m}>
                  {m}월
                </option>
              ))}
            </select>
          </label>
          <div className="vendor-purchase-status-filter-actions">
            <button type="button" className="secondary" onClick={reset} disabled={loading}>
              초기화
            </button>
            <button type="button" onClick={() => void load()} disabled={loading}>
              {loading ? '조회 중…' : '검색'}
            </button>
            <GridExcelExportButton
              fileBaseName="매입처별매입현황"
              disabled={loading || rows.length === 0}
              rows={exportRows}
            />
          </div>
        </div>
      </section>

      {error && <p className="hint error-text">{error}</p>}
      {!searched && <p className="hint">검색 조건 입력 후 검색을 눌러 주세요.</p>}
      {searched && !loading && rows.length === 0 && !error && <p className="hint">조회 결과가 없습니다.</p>}

      {displayRows.length > 0 && (
        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th className="num">매입년</th>
                <th className="num">매입월</th>
                <th>거래처명</th>
                <th>품목번호</th>
                <th>품목명</th>
                <th>공정명</th>
                <th>입고일자</th>
                <th className="num">현재고</th>
                <th className="num">입고량</th>
                <th className="num">단가</th>
                <th className="num">총 금액</th>
                <th>구분</th>
              </tr>
            </thead>
            <tbody>
              {displayRows.map((entry, index) => {
                if (entry.kind === 'data') {
                  const row = entry.row;
                  return (
                    <tr key={`${row.ledgerKind}-${row.historyId}`}>
                      <td className="num">{row.fiscalYear}</td>
                      <td className="num">{row.fiscalMonth}</td>
                      <td>{row.companyName}</td>
                      <td>{row.itemNo || '—'}</td>
                      <td>{row.itemName}</td>
                      <td>{row.processName || '—'}</td>
                      <td>{row.receiptDate}</td>
                      <td className="num">{formatQty(row.currentStockQty)}</td>
                      <td className="num">{formatQty(row.receiptQty)}</td>
                      <td className="num">{formatAmount(row.unitPrice)}</td>
                      <td className="num">{formatAmount(row.amount)}</td>
                      <td>{divisionLabel(row.division)}</td>
                    </tr>
                  );
                }
                if (entry.kind === 'subtotal') {
                  return (
                    <tr key={`subtotal-${entry.companyId}-${index}`} className="report-subtotal-row">
                      <td colSpan={3}>{entry.companyName} 소계</td>
                      <td colSpan={5} />
                      <td className="num">{formatQty(entry.receiptQty)}</td>
                      <td />
                      <td className="num">{formatAmount(entry.amount)}</td>
                      <td />
                    </tr>
                  );
                }
                return (
                  <tr key="grand-total" className="report-total-row">
                    <td colSpan={3}>합계</td>
                    <td colSpan={5} />
                    <td className="num">{formatQty(entry.receiptQty)}</td>
                    <td />
                    <td className="num">{formatAmount(entry.amount)}</td>
                    <td />
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
