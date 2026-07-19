import { useCallback, useMemo, useState } from 'react';
import {
  fetchPartnerMonthlyPayable,
  type PartnerMonthlyPayableRow,
} from '../api/stats';
import CompanySearchField, {
  PURCHASE_OUTSOURCE_PARTNER_ROLES,
  type CompanySearchSelection,
} from '../components/CompanySearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import { currentFiscalYearMonth } from '../utils/fiscalCalendar';
import { useMaterialIssueSetting } from '../context/MaterialIssueSettingContext';
import { formatAmount } from '../utils/numberFormat';

export default function PartnerMonthlyPayablePage() {
  const { fiscalCutoverSetting } = useMaterialIssueSetting();
  const currentPeriod = currentFiscalYearMonth(fiscalCutoverSetting);

  const [company, setCompany] = useState<CompanySearchSelection | null>(null);
  const [companyClearToken, setCompanyClearToken] = useState(0);
  const [fiscalYear, setFiscalYear] = useState(String(currentPeriod.fiscalYear));
  const [fiscalMonth, setFiscalMonth] = useState(String(currentPeriod.fiscalMonth));
  const [rows, setRows] = useState<PartnerMonthlyPayableRow[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [searched, setSearched] = useState(false);

  const totals = useMemo(() => {
    let approved = 0;
    let offset = 0;
    let payable = 0;
    for (const row of rows) {
      approved += Number(row.approvedAmount);
      offset += Number(row.offsetAmount);
      payable += Number(row.payableAmount);
    }
    return { approved, offset, payable };
  }, [rows]);

  const exportRows = useMemo(
    () =>
      rows.map((row) => ({
        년도: row.fiscalYear,
        월: row.fiscalMonth,
        거래처명: row.companyName,
        승인합: Number(row.approvedAmount),
        선급상계: Number(row.offsetAmount),
        실지급대상: Number(row.payableAmount),
      })),
    [rows],
  );

  const load = useCallback(async () => {
    const year = Number(fiscalYear);
    const month = Number(fiscalMonth);
    if (!Number.isFinite(year) || year < 2000) {
      setError('매입년도를 확인해 주세요.');
      return;
    }
    if (!Number.isFinite(month) || month < 1 || month > 12) {
      setError('매입월을 선택해 주세요.');
      return;
    }
    setLoading(true);
    setError(null);
    setSearched(true);
    try {
      const data = await fetchPartnerMonthlyPayable({
        companyId: company?.id,
        fiscalYear: year,
        fiscalMonth: month,
      });
      setRows(data);
    } catch (e) {
      setRows([]);
      setError(e instanceof Error ? e.message : '조회에 실패했습니다.');
    } finally {
      setLoading(false);
    }
  }, [company?.id, fiscalMonth, fiscalYear]);

  const reset = () => {
    setCompany(null);
    setCompanyClearToken((t) => t + 1);
    setFiscalYear(String(currentPeriod.fiscalYear));
    setFiscalMonth(String(currentPeriod.fiscalMonth));
    setRows([]);
    setError(null);
    setSearched(false);
  };

  return (
    <div className="page">
      <header className="page-header">
        <div>
          <h1>월별 실지급액</h1>
          <p>
            거래처별 해당 월 승인 매입 합에서 선급 상계를 뺀 실지급 대상 증가분입니다. (승인된 구매·외주만)
          </p>
        </div>
      </header>

      <section className="filter-panel vendor-purchase-status-filter">
        <div className="vendor-purchase-status-filter-row">
          <CompanySearchField
            label="거래처"
            selectedCompany={company}
            onSelect={setCompany}
            clearToken={companyClearToken}
            partnerTypes={PURCHASE_OUTSOURCE_PARTNER_ROLES}
            placeholder="거래처명 검색"
          />
          <label className="vendor-purchase-status-field-year">
            매입년도
            <input type="number" value={fiscalYear} onChange={(e) => setFiscalYear(e.target.value)} />
          </label>
          <label className="vendor-purchase-status-field-compact">
            매입월
            <select value={fiscalMonth} onChange={(e) => setFiscalMonth(e.target.value)}>
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
              fileBaseName="월별실지급액"
              disabled={loading || rows.length === 0}
              rows={exportRows}
            />
          </div>
        </div>
      </section>

      {error && <p className="hint error-text">{error}</p>}
      {!searched && <p className="hint">검색 조건 입력 후 검색을 눌러 주세요.</p>}
      {searched && !loading && rows.length === 0 && !error && <p className="hint">조회 결과가 없습니다.</p>}

      {rows.length > 0 && (
        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th className="num">년도</th>
                <th className="num">월</th>
                <th>거래처명</th>
                <th className="num">승인합</th>
                <th className="num">선급상계</th>
                <th className="num">실지급대상</th>
              </tr>
            </thead>
            <tbody>
              {rows.map((row) => {
                const offset = Number(row.offsetAmount);
                return (
                  <tr key={row.companyId}>
                    <td className="num">{row.fiscalYear}</td>
                    <td className="num">{row.fiscalMonth}</td>
                    <td>{row.companyName}</td>
                    <td className="num">{formatAmount(row.approvedAmount)}</td>
                    <td className={`num${offset > 0 ? ' amount-offset' : ''}`}>
                      {offset > 0 ? formatAmount(offset) : '—'}
                    </td>
                    <td className="num">{formatAmount(row.payableAmount)}</td>
                  </tr>
                );
              })}
              <tr className="report-total-row">
                <td colSpan={2} />
                <td>합계</td>
                <td className="num">{formatAmount(totals.approved)}</td>
                <td className={`num${totals.offset > 0 ? ' amount-offset' : ''}`}>
                  {formatAmount(totals.offset)}
                </td>
                <td className="num">{formatAmount(totals.payable)}</td>
              </tr>
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
