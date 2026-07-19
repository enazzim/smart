import { useCallback, useMemo, useState } from 'react';
import {
  fetchPurchaseDailyReport,
  type PurchaseDailyApprovalFilter,
  type PurchaseDailyReportRow,
  type VendorPurchaseDivision,
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
  | { kind: 'data'; row: PurchaseDailyReportRow }
  | {
      kind: 'subtotal';
      companyId: number;
      companyName: string;
      receiptQty: number;
      passedQty: number;
      failedQty: number;
      amount: number;
      offsetAmount: number;
      unpaidIncrease: number;
      monthTotal: number;
      yearTotal: number;
    }
  | {
      kind: 'total';
      receiptQty: number;
      passedQty: number;
      failedQty: number;
      amount: number;
      offsetAmount: number;
      unpaidIncrease: number;
      monthTotal: number;
      yearTotal: number;
    };

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

function approvalLabel(status: string) {
  return status === 'APPROVED' ? '승인' : status === 'PENDING' ? '미승인' : status;
}

function buildDisplayRows(rows: PurchaseDailyReportRow[]): DisplayRow[] {
  if (rows.length === 0) {
    return [];
  }

  const monthTotalByCompany = new Map<number, number>();
  const yearTotalByCompany = new Map<number, number>();
  for (const row of rows) {
    if (!monthTotalByCompany.has(row.companyId)) {
      monthTotalByCompany.set(row.companyId, Number(row.monthTotal));
      yearTotalByCompany.set(row.companyId, Number(row.yearTotal));
    }
  }

  const result: DisplayRow[] = [];
  let groupCompanyId = rows[0].companyId;
  let groupCompanyName = rows[0].companyName;
  let groupReceiptQty = 0;
  let groupPassedQty = 0;
  let groupFailedQty = 0;
  let groupAmount = 0;
  let groupOffsetAmount = 0;
  let groupUnpaidIncrease = 0;
  let totalReceiptQty = 0;
  let totalPassedQty = 0;
  let totalFailedQty = 0;
  let totalAmount = 0;
  let totalOffsetAmount = 0;
  let totalUnpaidIncrease = 0;
  let grandMonthTotal = 0;
  let grandYearTotal = 0;

  const flushSubtotal = () => {
    const monthTotal = monthTotalByCompany.get(groupCompanyId) ?? 0;
    const yearTotal = yearTotalByCompany.get(groupCompanyId) ?? 0;
    grandMonthTotal += monthTotal;
    grandYearTotal += yearTotal;
    result.push({
      kind: 'subtotal',
      companyId: groupCompanyId,
      companyName: groupCompanyName,
      receiptQty: groupReceiptQty,
      passedQty: groupPassedQty,
      failedQty: groupFailedQty,
      amount: groupAmount,
      offsetAmount: groupOffsetAmount,
      unpaidIncrease: groupUnpaidIncrease,
      monthTotal,
      yearTotal,
    });
  };

  for (const row of rows) {
    if (row.companyId !== groupCompanyId) {
      flushSubtotal();
      groupCompanyId = row.companyId;
      groupCompanyName = row.companyName;
      groupReceiptQty = 0;
      groupPassedQty = 0;
      groupFailedQty = 0;
      groupAmount = 0;
      groupOffsetAmount = 0;
      groupUnpaidIncrease = 0;
    }
    result.push({ kind: 'data', row });
    groupReceiptQty += Number(row.receiptQty);
    groupPassedQty += Number(row.passedQty);
    groupFailedQty += Number(row.failedQty);
    groupAmount += Number(row.amount);
    groupOffsetAmount += Number(row.offsetAmount ?? 0);
    groupUnpaidIncrease += Number(row.unpaidIncrease ?? 0);
    totalReceiptQty += Number(row.receiptQty);
    totalPassedQty += Number(row.passedQty);
    totalFailedQty += Number(row.failedQty);
    totalAmount += Number(row.amount);
    totalOffsetAmount += Number(row.offsetAmount ?? 0);
    totalUnpaidIncrease += Number(row.unpaidIncrease ?? 0);
  }
  flushSubtotal();
  result.push({
    kind: 'total',
    receiptQty: totalReceiptQty,
    passedQty: totalPassedQty,
    failedQty: totalFailedQty,
    amount: totalAmount,
    offsetAmount: totalOffsetAmount,
    unpaidIncrease: totalUnpaidIncrease,
    monthTotal: grandMonthTotal,
    yearTotal: grandYearTotal,
  });
  return result;
}

export default function PurchaseDailyReportPage() {
  const { fiscalCutoverSetting } = useMaterialIssueSetting();
  const currentPeriod = currentFiscalYearMonth(fiscalCutoverSetting);

  const [company, setCompany] = useState<CompanySearchSelection | null>(null);
  const [companyClearToken, setCompanyClearToken] = useState(0);
  const [selectedItem, setSelectedItem] = useState<ItemSearchSelection | null>(null);
  const [itemNoQuery, setItemNoQuery] = useState('');
  const [itemNameQuery, setItemNameQuery] = useState('');
  const [itemClearToken, setItemClearToken] = useState(0);
  const [approvalStatus, setApprovalStatus] = useState<PurchaseDailyApprovalFilter>('ALL');
  const [fiscalYear, setFiscalYear] = useState(String(currentPeriod.fiscalYear));
  const [fiscalMonth, setFiscalMonth] = useState(String(currentPeriod.fiscalMonth));
  const [inputDateFrom, setInputDateFrom] = useState('');
  const [inputDateTo, setInputDateTo] = useState('');
  const [receiptDateFrom, setReceiptDateFrom] = useState(addDaysIso(todayIso(), -30));
  const [receiptDateTo, setReceiptDateTo] = useState(todayIso());
  const [division, setDivision] = useState<VendorPurchaseDivision>('ALL');
  const [rows, setRows] = useState<PurchaseDailyReportRow[]>([]);
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
            년도: row.fiscalYear,
            월: row.fiscalMonth,
            승인: approvalLabel(row.approvalStatus),
            거래처명: row.companyName,
            품목번호: row.itemNo,
            품목명: row.itemName,
            단위: row.unit,
            공정: row.processName,
            입력일자: row.inputDate,
            입고일자: row.receiptDate,
            현재고: Number(row.currentStockQty),
            입고수: Number(row.receiptQty),
            합격수: Number(row.passedQty),
            불량수: Number(row.failedQty),
            기준단가: Number(row.standardUnitPrice),
            단가: Number(row.unitPrice),
            금액: Number(row.amount),
            선급상계: Number(row.offsetAmount ?? 0),
            실지급대상: Number(row.unpaidIncrease ?? 0),
            월계: '',
            구분: divisionLabel(row.division),
          };
        }
        if (entry.kind === 'subtotal') {
          return {
            년도: '',
            월: '',
            승인: '',
            거래처명: `${entry.companyName} 소계`,
            품목번호: '',
            품목명: '',
            단위: '',
            공정: '',
            입력일자: '',
            입고일자: '',
            현재고: '',
            입고수: entry.receiptQty,
            합격수: entry.passedQty,
            불량수: entry.failedQty,
            기준단가: '',
            단가: '',
            금액: entry.amount,
            선급상계: entry.offsetAmount,
            실지급대상: entry.unpaidIncrease,
            월계: entry.monthTotal,
            구분: '',
          };
        }
        return {
          년도: '',
          월: '',
          승인: '',
          거래처명: '합계',
          품목번호: '',
          품목명: '',
          단위: '',
          공정: '',
          입력일자: '',
          입고일자: '',
          현재고: '',
          입고수: entry.receiptQty,
          합격수: entry.passedQty,
          불량수: entry.failedQty,
          기준단가: '',
          단가: '',
          금액: entry.amount,
          선급상계: entry.offsetAmount,
          실지급대상: entry.unpaidIncrease,
          월계: entry.monthTotal,
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
        await fetchPurchaseDailyReport({
          companyId: company?.id,
          itemId: selectedItem?.id,
          itemNo: selectedItem ? undefined : itemNoQuery.trim() || undefined,
          itemName: selectedItem ? undefined : itemNameQuery.trim() || undefined,
          inputDateFrom: inputDateFrom || undefined,
          inputDateTo: inputDateTo || undefined,
          receiptDateFrom: receiptDateFrom || undefined,
          receiptDateTo: receiptDateTo || undefined,
          fiscalYear: Number.isFinite(year) && year > 0 ? year : undefined,
          fiscalMonth: Number.isFinite(month) && month > 0 ? month : undefined,
          division,
          approvalStatus,
        }),
      );
      setSearched(true);
    } catch (e) {
      setError(e instanceof Error ? e.message : '매입일보를 불러오지 못했습니다.');
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
    inputDateFrom,
    inputDateTo,
    receiptDateFrom,
    receiptDateTo,
    fiscalYear,
    fiscalMonth,
    division,
    approvalStatus,
  ]);

  const reset = () => {
    const period = currentFiscalYearMonth(fiscalCutoverSetting);
    setCompany(null);
    setCompanyClearToken((n) => n + 1);
    setSelectedItem(null);
    setItemNoQuery('');
    setItemNameQuery('');
    setItemClearToken((n) => n + 1);
    setApprovalStatus('ALL');
    setFiscalYear(String(period.fiscalYear));
    setFiscalMonth(String(period.fiscalMonth));
    setInputDateFrom('');
    setInputDateTo('');
    setReceiptDateFrom(addDaysIso(todayIso(), -30));
    setReceiptDateTo(todayIso());
    setDivision('ALL');
    setRows([]);
    setError(null);
    setSearched(false);
  };

  return (
    <div className="page">
      <header className="page-header">
        <div>
          <h1>매입일보</h1>
          <p>
            구매·외주·기타구매 입고 이력을 일보 형태로 조회합니다. 기준단가는 단가정보, 단가는 입고 적용단가를
            사용합니다.
          </p>
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
          <label className="vendor-purchase-status-field-compact">
            승인
            <select
              value={approvalStatus}
              onChange={(e) => setApprovalStatus(e.target.value as PurchaseDailyApprovalFilter)}
            >
              <option value="ALL">전체</option>
              <option value="APPROVED">승인</option>
              <option value="PENDING">미승인</option>
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
        </div>
        <div className="vendor-purchase-status-filter-row">
          <label>
            입력일 From
            <input type="date" value={inputDateFrom} onChange={(e) => setInputDateFrom(e.target.value)} />
          </label>
          <label>
            To
            <input type="date" value={inputDateTo} onChange={(e) => setInputDateTo(e.target.value)} />
          </label>
          <label>
            입고일 From
            <input type="date" value={receiptDateFrom} onChange={(e) => setReceiptDateFrom(e.target.value)} />
          </label>
          <label>
            To
            <input type="date" value={receiptDateTo} onChange={(e) => setReceiptDateTo(e.target.value)} />
          </label>
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
          <div className="vendor-purchase-status-filter-actions">
            <button type="button" className="secondary" onClick={reset} disabled={loading}>
              초기화
            </button>
            <button type="button" onClick={() => void load()} disabled={loading}>
              {loading ? '조회 중…' : '검색'}
            </button>
            <GridExcelExportButton
              fileBaseName="매입일보"
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
                <th className="num">년도</th>
                <th className="num">월</th>
                <th>승인</th>
                <th>거래처명</th>
                <th>품목번호</th>
                <th>품목명</th>
                <th>단위</th>
                <th>공정</th>
                <th>입력일자</th>
                <th>입고일자</th>
                <th className="num">현재고</th>
                <th className="num">입고수</th>
                <th className="num">합격수</th>
                <th className="num">불량수</th>
                <th className="num">기준단가</th>
                <th className="num">단가</th>
                <th className="num">금액</th>
                <th className="num">선급상계</th>
                <th className="num">실지급대상</th>
                <th className="num">월계</th>
                <th>구분</th>
              </tr>
            </thead>
            <tbody>
              {displayRows.map((entry, index) => {
                if (entry.kind === 'data') {
                  const row = entry.row;
                  const offset = Number(row.offsetAmount ?? 0);
                  return (
                    <tr key={`${row.ledgerKind}-${row.historyId}`}>
                      <td className="num">{row.fiscalYear}</td>
                      <td className="num">{row.fiscalMonth}</td>
                      <td>{approvalLabel(row.approvalStatus)}</td>
                      <td>{row.companyName}</td>
                      <td>{row.itemNo || '—'}</td>
                      <td>{row.itemName}</td>
                      <td>{row.unit || '—'}</td>
                      <td>{row.processName || '—'}</td>
                      <td>{row.inputDate}</td>
                      <td>{row.receiptDate}</td>
                      <td className="num">{formatQty(row.currentStockQty)}</td>
                      <td className="num">{formatQty(row.receiptQty)}</td>
                      <td className="num">{formatQty(row.passedQty)}</td>
                      <td className="num">{formatQty(row.failedQty)}</td>
                      <td className="num">{formatAmount(row.standardUnitPrice)}</td>
                      <td className="num">{formatAmount(row.unitPrice)}</td>
                      <td className="num">{formatAmount(row.amount)}</td>
                      <td className={`num${offset > 0 ? ' amount-offset' : ''}`}>
                        {offset > 0 ? formatAmount(offset) : '—'}
                      </td>
                      <td className="num">{formatAmount(row.unpaidIncrease ?? 0)}</td>
                      <td className="num">—</td>
                      <td>{divisionLabel(row.division)}</td>
                    </tr>
                  );
                }
                if (entry.kind === 'subtotal') {
                  return (
                    <tr key={`subtotal-${entry.companyId}-${index}`} className="report-subtotal-row">
                      <td colSpan={3} />
                      <td>{entry.companyName} 소계</td>
                      <td colSpan={6} />
                      <td />
                      <td className="num">{formatQty(entry.receiptQty)}</td>
                      <td className="num">{formatQty(entry.passedQty)}</td>
                      <td className="num">{formatQty(entry.failedQty)}</td>
                      <td />
                      <td />
                      <td className="num">{formatAmount(entry.amount)}</td>
                      <td className={`num${entry.offsetAmount > 0 ? ' amount-offset' : ''}`}>
                        {formatAmount(entry.offsetAmount)}
                      </td>
                      <td className="num">{formatAmount(entry.unpaidIncrease)}</td>
                      <td className="num">{formatAmount(entry.monthTotal)}</td>
                      <td />
                    </tr>
                  );
                }
                return (
                  <tr key="grand-total" className="report-total-row">
                    <td colSpan={3} />
                    <td>합계</td>
                    <td colSpan={6} />
                    <td />
                    <td className="num">{formatQty(entry.receiptQty)}</td>
                    <td className="num">{formatQty(entry.passedQty)}</td>
                    <td className="num">{formatQty(entry.failedQty)}</td>
                    <td />
                    <td />
                    <td className="num">{formatAmount(entry.amount)}</td>
                    <td className={`num${entry.offsetAmount > 0 ? ' amount-offset' : ''}`}>
                      {formatAmount(entry.offsetAmount)}
                    </td>
                    <td className="num">{formatAmount(entry.unpaidIncrease)}</td>
                    <td className="num">{formatAmount(entry.monthTotal)}</td>
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
