import { useCallback, useEffect, useMemo, useState } from 'react';
import {
  cancelWorkOrder,
  createWorkOrders,
  fetchWorkOrderTargets,
  fetchWorkOrders,
  type WorkOrder,
  type WorkOrderListParams,
} from '../api/workOrder';
import GridExcelExportButton from '../components/GridExcelExportButton';

function formatQty(value: number): string {
  return Number.isInteger(value) ? String(value) : value.toLocaleString(undefined, { maximumFractionDigits: 4 });
}

export default function WorkOrderPage() {
  const [targets, setTargets] = useState<WorkOrder[]>([]);
  const [orders, setOrders] = useState<WorkOrder[]>([]);
  const [selectedPlanIds, setSelectedPlanIds] = useState<Set<number>>(new Set());
  const [filters, setFilters] = useState<WorkOrderListParams>({});
  const [loadingTargets, setLoadingTargets] = useState(true);
  const [loadingOrders, setLoadingOrders] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);
  const [tab, setTab] = useState<'targets' | 'orders'>('targets');

  const allSelected = targets.length > 0 && targets.every((row) => selectedPlanIds.has(row.workPlanId));
  const selectedCount = selectedPlanIds.size;

  const orderExportRows = useMemo(
    () =>
      orders.map((row) => ({
        지시번호: row.orderNum,
        계획번호: row.planNo,
        품목: `${row.itemNo} ${row.itemName}`,
        공정: row.processName,
        작업장: row.workCenterName ?? '',
        지시수량: row.orderedQty,
        실적수량: row.reportedQty,
        잔량: row.remainingQty,
        상태: row.statusLabel,
      })),
    [orders],
  );

  const loadTargets = useCallback(async () => {
    setLoadingTargets(true);
    setError(null);
    try {
      const data = await fetchWorkOrderTargets();
      setTargets(data);
      setSelectedPlanIds(new Set());
    } catch (e) {
      setError(e instanceof Error ? e.message : '발행 대상을 불러오지 못했습니다.');
    } finally {
      setLoadingTargets(false);
    }
  }, []);

  const loadOrders = useCallback(async () => {
    setLoadingOrders(true);
    setError(null);
    try {
      setOrders(await fetchWorkOrders(filters));
    } catch (e) {
      setError(e instanceof Error ? e.message : '작업지시 목록을 불러오지 못했습니다.');
    } finally {
      setLoadingOrders(false);
    }
  }, [filters]);

  useEffect(() => {
    void loadTargets();
  }, [loadTargets]);

  useEffect(() => {
    if (tab === 'orders') {
      void loadOrders();
    }
  }, [tab, loadOrders]);

  const toggleSelectAll = (checked: boolean) => {
    setSelectedPlanIds(checked ? new Set(targets.map((row) => row.workPlanId)) : new Set());
  };

  const toggleSelect = (workPlanId: number, checked: boolean) => {
    setSelectedPlanIds((prev) => {
      const next = new Set(prev);
      if (checked) next.add(workPlanId);
      else next.delete(workPlanId);
      return next;
    });
  };

  const onCreate = async () => {
    if (selectedCount === 0) return;
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      const created = await createWorkOrders([...selectedPlanIds]);
      setMessage(`작업지시 ${created.length}건을 발행했습니다.`);
      await loadTargets();
      await loadOrders();
      setTab('orders');
    } catch (e) {
      setError(e instanceof Error ? e.message : '작업지시 발행에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancel = async (id: number, orderNum: string) => {
    if (!window.confirm(`작업지시 ${orderNum}을(를) 취소하시겠습니까?`)) return;
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      await cancelWorkOrder(id);
      setMessage(`작업지시 ${orderNum}을(를) 취소했습니다.`);
      await loadTargets();
      await loadOrders();
    } catch (e) {
      setError(e instanceof Error ? e.message : '작업지시 취소에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>작업지시</h1>
        <p>사내공정 작업계획에 대해 작업지시를 발행합니다. 실적이 등록되면 취소할 수 없습니다.</p>
      </header>

      <div className="tab-row">
        <button type="button" className={tab === 'targets' ? 'tab-active' : undefined} onClick={() => setTab('targets')}>
          발행 대상
        </button>
        <button type="button" className={tab === 'orders' ? 'tab-active' : undefined} onClick={() => setTab('orders')}>
          작업지시 목록
        </button>
      </div>

      {error && <p className="error-banner">{error}</p>}
      {message && <p className="success-banner">{message}</p>}

      {tab === 'targets' && (
        <>
          <section className="action-bar">
            <button type="button" className="secondary" onClick={() => void loadTargets()}>
              새로고침
            </button>
            <button type="button" disabled={submitting || selectedCount === 0} onClick={() => void onCreate()}>
              {submitting ? '발행 중…' : `작업지시 발행 (${selectedCount}건)`}
            </button>
          </section>
          {loadingTargets ? (
            <p>불러오는 중…</p>
          ) : (
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>
                      <input
                        type="checkbox"
                        aria-label="전체 선택"
                        checked={allSelected}
                        disabled={targets.length === 0 || submitting}
                        onChange={(e) => toggleSelectAll(e.target.checked)}
                      />
                    </th>
                    <th>계획번호</th>
                    <th>품목</th>
                    <th>공정</th>
                    <th>작업장</th>
                    <th>지시수량</th>
                    <th>시작일</th>
                  </tr>
                </thead>
                <tbody>
                  {targets.length === 0 ? (
                    <tr>
                      <td colSpan={7}>발행 대상이 없습니다.</td>
                    </tr>
                  ) : (
                    targets.map((row) => (
                      <tr key={row.workPlanId}>
                        <td>
                          <input
                            type="checkbox"
                            checked={selectedPlanIds.has(row.workPlanId)}
                            disabled={submitting}
                            onChange={(e) => toggleSelect(row.workPlanId, e.target.checked)}
                          />
                        </td>
                        <td>{row.planNo}</td>
                        <td>
                          {row.itemNo} {row.itemName}
                        </td>
                        <td>{row.processName}</td>
                        <td>{row.workCenterName ?? '—'}</td>
                        <td>{formatQty(row.orderedQty)}</td>
                        <td>{row.planStartDate ?? '—'}</td>
                      </tr>
                    ))
                  )}
                </tbody>
              </table>
            </div>
          )}
        </>
      )}

      {tab === 'orders' && (
        <>
          <section className="filter-panel">
            <label>
              품목번호
              <input value={filters.itemNo ?? ''} onChange={(e) => setFilters((f) => ({ ...f, itemNo: e.target.value }))} />
            </label>
            <label>
              품목명
              <input value={filters.itemName ?? ''} onChange={(e) => setFilters((f) => ({ ...f, itemName: e.target.value }))} />
            </label>
            <label>
              지시번호
              <input value={filters.orderNum ?? ''} onChange={(e) => setFilters((f) => ({ ...f, orderNum: e.target.value }))} />
            </label>
            <button type="button" onClick={() => void loadOrders()}>
              검색
            </button>
          </section>
          <div className="panel-header-row">
            <h2>작업지시 목록</h2>
            <GridExcelExportButton fileBaseName="작업지시목록" disabled={loadingOrders} rows={orderExportRows} />
          </div>
          {loadingOrders ? (
            <p>불러오는 중…</p>
          ) : (
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>지시번호</th>
                    <th>계획번호</th>
                    <th>품목</th>
                    <th>공정</th>
                    <th>작업장</th>
                    <th>지시</th>
                    <th>실적</th>
                    <th>잔량</th>
                    <th>상태</th>
                    <th />
                  </tr>
                </thead>
                <tbody>
                  {orders.length === 0 ? (
                    <tr>
                      <td colSpan={10}>작업지시가 없습니다.</td>
                    </tr>
                  ) : (
                    orders.map((row) => (
                      <tr key={row.id}>
                        <td>{row.orderNum}</td>
                        <td>{row.planNo}</td>
                        <td>
                          {row.itemNo} {row.itemName}
                        </td>
                        <td>{row.processName}</td>
                        <td>{row.workCenterName ?? '—'}</td>
                        <td>{formatQty(row.orderedQty)}</td>
                        <td>{formatQty(row.reportedQty)}</td>
                        <td>{formatQty(row.remainingQty)}</td>
                        <td>{row.statusLabel}</td>
                        <td>
                          {row.cancellable && (
                            <button type="button" className="secondary" disabled={submitting} onClick={() => void onCancel(row.id, row.orderNum)}>
                              취소
                            </button>
                          )}
                        </td>
                      </tr>
                    ))
                  )}
                </tbody>
              </table>
            </div>
          )}
        </>
      )}
    </div>
  );
}
