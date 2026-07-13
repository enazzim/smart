import { useCallback, useEffect, useMemo, useState } from 'react';
import {
  approvePayableItems,
  cancelPayableApproval,
  fetchApprovedPayableApprovals,
  fetchPendingPayableApprovals,
  updatePayableApprovalFiscalPeriod,
  type PayableApprovalRow,
  type PayableApprovalSearchParams,
} from '../api/payableApproval';
import CompanySearchField, {
  PURCHASE_OUTSOURCE_PARTNER_ROLES,
  type CompanySearchSelection,
} from '../components/CompanySearchField';
import FiscalPeriodTableCells from '../components/FiscalPeriodTableCells';
import GridExcelExportButton from '../components/GridExcelExportButton';
import { currentFiscalYearMonth, type FiscalPeriod } from '../utils/fiscalCalendar';
import { useMaterialIssueSetting } from '../context/MaterialIssueSettingContext';
import { formatAmount } from '../utils/numberFormat';
import { useConfirm } from '../context/ConfirmContext';

type TabId = 'pending' | 'approved';

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

function formatDateTime(value: string): string {
  return new Date(value).toLocaleString('ko-KR');
}

function rowKey(row: PayableApprovalRow): string {
  return `${row.ledgerKind}-${row.historyId}`;
}

function createDefaultFilters(cutoverSetting: string): PayableApprovalSearchParams {
  const { fiscalYear, fiscalMonth } = currentFiscalYearMonth(cutoverSetting);
  return {
    receiptDateFrom: addDaysIso(todayIso(), -30),
    receiptDateTo: todayIso(),
    fiscalYear,
    fiscalMonth,
  };
}

export default function PayableApprovalPage() {
  const confirm = useConfirm();
  const { fiscalCutoverSetting } = useMaterialIssueSetting();
  const [activeTab, setActiveTab] = useState<TabId>('pending');
  const [filters, setFilters] = useState<PayableApprovalSearchParams>(() => createDefaultFilters(fiscalCutoverSetting));
  const [appliedFilters, setAppliedFilters] = useState<PayableApprovalSearchParams>(() =>
    createDefaultFilters(fiscalCutoverSetting),
  );
  const [filterPartner, setFilterPartner] = useState<CompanySearchSelection | null>(null);
  const [appliedPartner, setAppliedPartner] = useState<CompanySearchSelection | null>(null);
  const [rows, setRows] = useState<PayableApprovalRow[]>([]);
  const [selectedKeys, setSelectedKeys] = useState<Set<string>>(new Set());
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const searchParams = useMemo(
    (): PayableApprovalSearchParams => ({
      ...appliedFilters,
      partnerName: appliedPartner?.companyName,
    }),
    [appliedFilters, appliedPartner],
  );

  const load = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const data =
        activeTab === 'pending'
          ? await fetchPendingPayableApprovals(searchParams)
          : await fetchApprovedPayableApprovals(searchParams);
      setRows(data);
      setSelectedKeys(new Set());
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
      setRows([]);
    } finally {
      setLoading(false);
    }
  }, [activeTab, searchParams]);

  useEffect(() => {
    void load();
  }, [load]);

  const allSelected = rows.length > 0 && selectedKeys.size === rows.length;

  const toggleAll = () => {
    if (allSelected) {
      setSelectedKeys(new Set());
    } else {
      setSelectedKeys(new Set(rows.map(rowKey)));
    }
  };

  const toggleRow = (row: PayableApprovalRow) => {
    const key = rowKey(row);
    setSelectedKeys((prev) => {
      const next = new Set(prev);
      if (next.has(key)) {
        next.delete(key);
      } else {
        next.add(key);
      }
      return next;
    });
  };

  const selectedItems = useMemo(
    () =>
      rows
        .filter((row) => selectedKeys.has(rowKey(row)))
        .map((row) => ({ ledgerKind: row.ledgerKind, historyId: row.historyId })),
    [rows, selectedKeys],
  );

  const exportRows = useMemo(
    () =>
      rows.map((row) => ({
        승인: row.approvalStatus === 'APPROVED' ? '승인' : '미승인',
        품목번호: row.itemNo,
        품목명: row.itemName,
        공정명: row.processName,
        합격수량: row.qty,
        표준단가: row.standardUnitPrice,
        단가: row.unitPrice,
        금액: row.amount,
        매입년도: row.fiscalYear,
        매입월: row.fiscalMonth,
        구분: row.categoryLabel,
        거래처: row.partnerName,
        입고일: row.receiptDate,
      })),
    [rows],
  );

  const onSearch = (e: React.FormEvent) => {
    e.preventDefault();
    const { fiscalYear, fiscalMonth } = currentFiscalYearMonth(fiscalCutoverSetting);
    const nextFilters: PayableApprovalSearchParams = {
      ...filters,
      fiscalYear: filters.fiscalYear ?? fiscalYear,
      fiscalMonth: filters.fiscalMonth ?? fiscalMonth,
    };
    setFilters(nextFilters);
    setAppliedFilters(nextFilters);
    setAppliedPartner(filterPartner);
  };

  const onResetFilters = () => {
    const defaults = createDefaultFilters(fiscalCutoverSetting);
    setFilters(defaults);
    setAppliedFilters(defaults);
    setFilterPartner(null);
    setAppliedPartner(null);
  };

  const onApprove = async () => {
    if (selectedItems.length === 0) {
      setError('승인할 항목을 선택해 주세요.');
      return;
    }
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      await approvePayableItems(selectedItems);
      setMessage(`${selectedItems.length}건 승인 처리되었습니다.`);
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '승인 처리 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onFiscalPeriodSave = async (row: PayableApprovalRow, period: FiscalPeriod) => {
    setError(null);
    setMessage(null);
    try {
      await updatePayableApprovalFiscalPeriod({
        ledgerKind: row.ledgerKind,
        historyId: row.historyId,
        fiscalYear: period.fiscalYear,
        fiscalMonth: period.fiscalMonth,
      });
      setRows((prev) =>
        prev.map((item) =>
          rowKey(item) === rowKey(row)
            ? { ...item, fiscalYear: period.fiscalYear, fiscalMonth: period.fiscalMonth }
            : item,
        ),
      );
      setMessage('매입년월이 수정되었습니다.');
    } catch (e) {
      setError(e instanceof Error ? e.message : '매입년월 수정 실패');
      throw e;
    }
  };

  const onCancelApproval = async () => {
    if (selectedItems.length === 0) {
      setError('승인취소할 항목을 선택해 주세요.');
      return;
    }
    if (!(await confirm(`선택한 ${selectedItems.length}건의 승인을 취소하시겠습니까?`, { title: '취소 확인', confirmLabel: '예, 취소', cancelLabel: '닫기', danger: true }))) {
      return;
    }
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      await cancelPayableApproval(selectedItems);
      setMessage(`${selectedItems.length}건 승인이 취소되었습니다.`);
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '승인취소 실패');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>승인처리</h1>
        <p>구매입고 · 외주입고 · 기타구매입고 지급 확정 — 재고와 별도로 승인 후 미지급에 반영됩니다.</p>
      </header>

      <div className="tab-row" aria-label="승인처리 탭">
        <button
          type="button"
          className={activeTab === 'pending' ? 'tab-active' : undefined}
          onClick={() => setActiveTab('pending')}
        >
          미승인 승인
        </button>
        <button
          type="button"
          className={activeTab === 'approved' ? 'tab-active' : undefined}
          onClick={() => setActiveTab('approved')}
        >
          승인 이력
        </button>
      </div>

      {error && <div className="error">{error}</div>}
      {message && <div className="success">{message}</div>}

      <section className="panel">
        <h2>검색</h2>
        <form onSubmit={onSearch} className="search-row">
          <CompanySearchField
            label="거래처 (선택)"
            partnerTypes={PURCHASE_OUTSOURCE_PARTNER_ROLES}
            selectedCompany={filterPartner}
            onSelect={setFilterPartner}
            placeholder="전체 조회 — 상호 또는 사업자번호 입력"
          />
          <label>
            품목번호
            <input
              value={filters.itemNo ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, itemNo: e.target.value }))}
            />
          </label>
          <label>
            품목명
            <input
              value={filters.itemName ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, itemName: e.target.value }))}
            />
          </label>
          <label>
            매입년도
            <input
              type="number"
              min={2000}
              max={2100}
              value={filters.fiscalYear ?? ''}
              onChange={(e) =>
                setFilters((f) => ({
                  ...f,
                  fiscalYear: e.target.value === '' ? undefined : Number(e.target.value),
                }))
              }
            />
          </label>
          <label>
            매입월
            <select
              value={filters.fiscalMonth ?? currentFiscalYearMonth(fiscalCutoverSetting).fiscalMonth}
              onChange={(e) => setFilters((f) => ({ ...f, fiscalMonth: Number(e.target.value) }))}
            >
              {Array.from({ length: 12 }, (_, index) => index + 1).map((month) => (
                <option key={month} value={month}>
                  {month}월
                </option>
              ))}
            </select>
          </label>
          <label>
            입고일 From
            <input
              type="date"
              value={filters.receiptDateFrom ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, receiptDateFrom: e.target.value }))}
            />
          </label>
          <label>
            To
            <input
              type="date"
              value={filters.receiptDateTo ?? ''}
              onChange={(e) => setFilters((f) => ({ ...f, receiptDateTo: e.target.value }))}
            />
          </label>
          <button type="button" className="secondary" disabled={filterPartner == null} onClick={() => setFilterPartner(null)}>
            전체
          </button>
          <button type="button" className="secondary" onClick={onResetFilters}>
            초기화
          </button>
          <button type="submit" disabled={loading}>
            검색
          </button>
        </form>
      </section>

      <section className="panel">
        <div className="panel-header-row">
          <h2>{activeTab === 'pending' ? '미승인 목록' : '승인 이력'}</h2>
          {activeTab === 'pending' ? (
            <button
              type="button"
              disabled={submitting || selectedItems.length === 0}
              onClick={() => void onApprove()}
            >
              {submitting ? '처리 중…' : '승인'}
            </button>
          ) : (
            <div className="inline-actions">
              <button
                type="button"
                className="secondary"
                disabled={submitting || selectedItems.length === 0}
                onClick={() => void onCancelApproval()}
              >
                {submitting ? '처리 중…' : '승인취소'}
              </button>
              <GridExcelExportButton fileBaseName="승인이력" disabled={loading} rows={exportRows} />
            </div>
          )}
        </div>

        {loading ? (
          <p>불러오는 중…</p>
        ) : rows.length === 0 ? (
          <p className="hint">조회 결과가 없습니다.</p>
        ) : (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>
                    <input type="checkbox" checked={allSelected} onChange={toggleAll} aria-label="전체 선택" />
                  </th>
                  <th>승인</th>
                  <th>품목번호</th>
                  <th>품목명</th>
                  <th>공정명</th>
                  <th className="num">합격수량</th>
                  <th className="num">표준단가</th>
                  <th className="num">단가</th>
                  <th className="num">금액</th>
                  <th className="num">매입년도</th>
                  <th className="num">매입월</th>
                  <th>구분</th>
                  <th>거래처</th>
                  <th>입고일</th>
                  {activeTab === 'approved' && (
                    <>
                      <th>승인자</th>
                      <th>승인일시</th>
                    </>
                  )}
                </tr>
              </thead>
              <tbody>
                {rows.map((row) => (
                  <tr key={rowKey(row)}>
                    <td>
                      <input
                        type="checkbox"
                        checked={selectedKeys.has(rowKey(row))}
                        onChange={() => toggleRow(row)}
                        aria-label={`${row.itemNo} 선택`}
                      />
                    </td>
                    <td>{row.approvalStatus === 'APPROVED' ? '승인' : '미승인'}</td>
                    <td>{row.itemNo}</td>
                    <td>{row.itemName}</td>
                    <td>{row.processName || '—'}</td>
                    <td className="num">{formatAmount(row.qty)}</td>
                    <td className="num">{formatAmount(row.standardUnitPrice)}</td>
                    <td className="num">{formatAmount(row.unitPrice)}</td>
                    <td className="num">{formatAmount(row.amount)}</td>
                    <FiscalPeriodTableCells
                      fiscalYear={row.fiscalYear}
                      fiscalMonth={row.fiscalMonth}
                      disabled={submitting}
                      numeric
                      onSave={(period) => onFiscalPeriodSave(row, period)}
                    />
                    <td>{row.categoryLabel}</td>
                    <td>{row.partnerName}</td>
                    <td>{row.receiptDate}</td>
                    {activeTab === 'approved' && (
                      <>
                        <td>{row.approvedByName ?? '—'}</td>
                        <td>{row.approvedAt ? formatDateTime(row.approvedAt) : '—'}</td>
                      </>
                    )}
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
