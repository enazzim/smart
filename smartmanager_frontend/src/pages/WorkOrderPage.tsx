import { useCallback, useEffect, useMemo, useRef, useState } from 'react';
import {
  cancelWorkOrder,
  createWorkOrders,
  fetchWorkOrderTargets,
  fetchWorkOrders,
  type WorkOrder,
  type WorkOrderListParams,
  type WorkOrderTargetSearchParams,
} from '../api/workOrder';
import GridExcelExportButton from '../components/GridExcelExportButton';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import { formatQty } from '../utils/numberFormat';
import { useConfirm } from '../context/ConfirmContext';

type OrderFilterDraft = Omit<WorkOrderListParams, 'itemNo' | 'itemName'>;

function formatSeoulDate(value?: string | null): string {
  if (!value) return '—';
  const date = new Date(value);
  if (Number.isNaN(date.getTime())) return value.slice(0, 10);
  return date.toLocaleDateString('sv-SE', { timeZone: 'Asia/Seoul' });
}

export default function WorkOrderPage() {
  const confirm = useConfirm();
  const [targets, setTargets] = useState<WorkOrder[]>([]);
  const [orders, setOrders] = useState<WorkOrder[]>([]);
  const [selectedPlanIds, setSelectedPlanIds] = useState<Set<number>>(new Set());
  const [filters, setFilters] = useState<OrderFilterDraft>({});
  const [selectedItem, setSelectedItem] = useState<ItemSearchSelection | null>(null);
  const [targetItem, setTargetItem] = useState<ItemSearchSelection | null>(null);
  const [targetProcessName, setTargetProcessName] = useState('');
  const [targetDateFrom, setTargetDateFrom] = useState('');
  const [targetDateTo, setTargetDateTo] = useState('');
  const [targetsSearched, setTargetsSearched] = useState(false);
  const appliedParamsRef = useRef<WorkOrderListParams>({});
  const appliedTargetRef = useRef<WorkOrderTargetSearchParams>({});
  const [loadingTargets, setLoadingTargets] = useState(false);
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

  const loadTargets = useCallback(async (params: WorkOrderTargetSearchParams) => {
    appliedTargetRef.current = params;
    setTargetsSearched(true);
    setLoadingTargets(true);
    setError(null);
    try {
      const data = await fetchWorkOrderTargets(params);
      setTargets(data);
      setSelectedPlanIds(new Set());
    } catch (e) {
      setError(e instanceof Error ? e.message : '발행 대상을 불러오지 못했습니다.');
    } finally {
      setLoadingTargets(false);
    }
  }, []);

  const buildOrderParams = (): WorkOrderListParams => ({
    ...filters,
    itemNo: selectedItem?.itemNo,
  });

  const loadOrders = useCallback(async (params: WorkOrderListParams) => {
    appliedParamsRef.current = params;
    setLoadingOrders(true);
    setError(null);
    try {
      setOrders(await fetchWorkOrders(params));
    } catch (e) {
      setError(e instanceof Error ? e.message : '작업지시 목록을 불러오지 못했습니다.');
    } finally {
      setLoadingOrders(false);
    }
  }, []);

  const onSearchTargets = () => {
    void loadTargets({
      itemNo: targetItem?.itemNo,
      processName: targetProcessName.trim() || undefined,
      registeredFrom: targetDateFrom || undefined,
      registeredTo: targetDateTo || undefined,
    });
  };

  useEffect(() => {
    if (tab === 'orders') {
      void loadOrders(appliedParamsRef.current);
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
      if (targetsSearched) {
        await loadTargets(appliedTargetRef.current);
      }
      setTab('orders');
    } catch (e) {
      setError(e instanceof Error ? e.message : '작업지시 발행에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancel = async (id: number, orderNum: string) => {
    if (!(await confirm(`작업지시 ${orderNum}을(를) 취소하시겠습니까?`, { title: '취소 확인', confirmLabel: '예, 취소', cancelLabel: '닫기', danger: true }))) return;
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      await cancelWorkOrder(id);
      setMessage(`작업지시 ${orderNum}을(를) 취소했습니다.`);
      if (targetsSearched) {
        await loadTargets(appliedTargetRef.current);
      }
      await loadOrders(appliedParamsRef.current);
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
          <section className="filter-panel">
            <ItemSearchField label="품목" selectedItem={targetItem} onSelect={setTargetItem} />
            <label>
              공정
              <input value={targetProcessName} onChange={(e) => setTargetProcessName(e.target.value)} />
            </label>
            <label>
              작업계획 등록일(부터)
              <input type="date" value={targetDateFrom} onChange={(e) => setTargetDateFrom(e.target.value)} />
            </label>
            <label>
              작업계획 등록일(까지)
              <input type="date" value={targetDateTo} onChange={(e) => setTargetDateTo(e.target.value)} />
            </label>
            <button type="button" onClick={onSearchTargets} disabled={loadingTargets}>
              {loadingTargets ? '조회 중…' : '검색'}
            </button>
          </section>
          <section className="action-bar">
            <button type="button" disabled={submitting || selectedCount === 0} onClick={() => void onCreate()}>
              {submitting ? '발행 중…' : `작업지시 발행 (${selectedCount}건)`}
            </button>
          </section>
          {loadingTargets ? (
            <p>불러오는 중…</p>
          ) : !targetsSearched ? (
            <p>검색 조건을 입력한 뒤 검색 버튼을 눌러 주세요. 조건을 비우면 발행 대상 전체가 조회됩니다.</p>
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
                    <th className="num">지시수량</th>
                    <th>시작일</th>
                    <th>등록일</th>
                  </tr>
                </thead>
                <tbody>
                  {targets.length === 0 ? (
                    <tr>
                      <td colSpan={8}>발행 대상이 없습니다.</td>
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
                        <td className="num">{formatQty(row.orderedQty)}</td>
                        <td>{row.planStartDate ?? '—'}</td>
                        <td>{formatSeoulDate(row.createdAt)}</td>
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
            <ItemSearchField label="품목" selectedItem={selectedItem} onSelect={setSelectedItem} />
            <label>
              지시번호
              <input value={filters.orderNum ?? ''} onChange={(e) => setFilters((f) => ({ ...f, orderNum: e.target.value }))} />
            </label>
            <button type="button" onClick={() => void loadOrders(buildOrderParams())}>
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
                    <th className="num">지시</th>
                    <th className="num">실적</th>
                    <th className="num">잔량</th>
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
                        <td className="num">{formatQty(row.orderedQty)}</td>
                        <td className="num">{formatQty(row.reportedQty)}</td>
                        <td className="num">{formatQty(row.remainingQty)}</td>
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
