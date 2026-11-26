import { useCallback, useMemo, useRef, useState } from 'react';
import type { PropertyClassification } from '../api/item';
import {
  cancelWorkPlanLine,
  cancelWorkPlansByProductionPlan,
  createWorkPlans,
  fetchWorkPlanTargets,
  fetchWorkPlans,
  type WorkPlan,
  type WorkPlanListParams,
  type WorkPlanTargetParams,
} from '../api/workPlan';
import type { ProductionPlan } from '../api/productionPlan';
import { formatQty } from '../utils/numberFormat';
import { useConfirm } from '../context/ConfirmContext';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';

const TARGET_ITEM_CLASSES: PropertyClassification[] = ['제품', '공정품'];

type PlanFilterDraft = Omit<WorkPlanListParams, 'itemNo' | 'itemName'>;

export default function WorkPlanPage() {
  const confirm = useConfirm();
  const [targets, setTargets] = useState<ProductionPlan[]>([]);
  const [targetsSearched, setTargetsSearched] = useState(false);
  const [targetItem, setTargetItem] = useState<ItemSearchSelection | null>(null);
  const [targetRegisteredFrom, setTargetRegisteredFrom] = useState('');
  const [targetRegisteredTo, setTargetRegisteredTo] = useState('');
  const [targetItemClearToken, setTargetItemClearToken] = useState(0);
  const [plans, setPlans] = useState<WorkPlan[]>([]);
  const [plansSearched, setPlansSearched] = useState(false);
  const [selectedPlanIds, setSelectedPlanIds] = useState<Set<number>>(new Set());
  const [planFilters, setPlanFilters] = useState<PlanFilterDraft>({});
  const [selectedItem, setSelectedItem] = useState<ItemSearchSelection | null>(null);
  const [itemClearToken, setItemClearToken] = useState(0);
  const appliedParamsRef = useRef<WorkPlanListParams>({});
  const [loadingTargets, setLoadingTargets] = useState(false);
  const [loadingPlans, setLoadingPlans] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);
  const [tab, setTab] = useState<'targets' | 'plans'>('targets');

  const allSelected = targets.length > 0 && targets.every((row) => selectedPlanIds.has(row.id));

  const loadTargets = useCallback(async (params: WorkPlanTargetParams = {}) => {
    setLoadingTargets(true);
    setError(null);
    try {
      const data = await fetchWorkPlanTargets(params);
      setTargets(data);
      setTargetsSearched(true);
      setSelectedPlanIds((prev) => {
        const visible = new Set(data.map((row) => row.id));
        return new Set([...prev].filter((id) => visible.has(id)));
      });
    } catch (e) {
      setError(e instanceof Error ? e.message : '수립 대상을 불러오지 못했습니다.');
      setTargets([]);
      setTargetsSearched(true);
    } finally {
      setLoadingTargets(false);
    }
  }, []);

  const buildPlanParams = (): WorkPlanListParams => ({
    ...planFilters,
    itemNo: selectedItem?.itemNo,
  });

  const loadPlans = useCallback(async (params: WorkPlanListParams) => {
    appliedParamsRef.current = params;
    setLoadingPlans(true);
    setError(null);
    try {
      setPlans(await fetchWorkPlans(params));
      setPlansSearched(true);
    } catch (e) {
      setError(e instanceof Error ? e.message : '작업계획 목록을 불러오지 못했습니다.');
      setPlans([]);
      setPlansSearched(true);
    } finally {
      setLoadingPlans(false);
    }
  }, []);

  const buildTargetParams = (): WorkPlanTargetParams => ({
    itemId: targetItem?.id,
    registeredFrom: targetRegisteredFrom || undefined,
    registeredTo: targetRegisteredTo || undefined,
  });

  const selectedCount = useMemo(() => selectedPlanIds.size, [selectedPlanIds]);

  const cancellableProductionPlanIds = useMemo(() => {
    const linesByPlan = new Map<number, WorkPlan[]>();
    for (const line of plans) {
      const planLines = linesByPlan.get(line.productionPlanId) ?? [];
      planLines.push(line);
      linesByPlan.set(line.productionPlanId, planLines);
    }
    const ids = new Set<number>();
    for (const [planId, planLines] of linesByPlan) {
      if (planLines.length > 0 && planLines.every((line) => line.cancellable)) {
        ids.add(planId);
      }
    }
    return ids;
  }, [plans]);

  const toggleSelectAll = (checked: boolean) => {
    if (!checked) {
      setSelectedPlanIds(new Set());
      return;
    }
    setSelectedPlanIds(new Set(targets.map((row) => row.id)));
  };

  const toggleRow = (id: number, checked: boolean) => {
    setSelectedPlanIds((prev) => {
      const next = new Set(prev);
      if (checked) {
        next.add(id);
      } else {
        next.delete(id);
      }
      return next;
    });
  };

  const onCreate = async () => {
    if (selectedCount === 0) {
      setError('수립할 생산계획을 선택해 주세요.');
      return;
    }
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      const created = await createWorkPlans([...selectedPlanIds]);
      setMessage(`작업계획 ${created.length}건을 수립했습니다.`);
      setSelectedPlanIds(new Set());
      if (targetsSearched) {
        await loadTargets(buildTargetParams());
      }
    } catch (e) {
      setError(e instanceof Error ? e.message : '작업계획 수립에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancelLine = async (id: number, label: string) => {
    if (!(await confirm(`${label} 작업계획 1건을 취소하시겠습니까?`, { title: '취소 확인', confirmLabel: '예, 취소', cancelLabel: '닫기', danger: true }))) {
      return;
    }
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      await cancelWorkPlanLine(id);
      setMessage('작업계획 1건을 취소했습니다.');
      if (targetsSearched) {
        await loadTargets(buildTargetParams());
      }
      if (plansSearched) {
        await loadPlans(appliedParamsRef.current);
      }
    } catch (e) {
      setError(e instanceof Error ? e.message : '작업계획 취소에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancelPlan = async (productionPlanId: number, planNo: string) => {
    if (!(await confirm(`생산계획 ${planNo}의 작업계획을 전체 취소하시겠습니까?`, { title: '취소 확인', confirmLabel: '예, 취소', cancelLabel: '닫기', danger: true }))) {
      return;
    }
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      const result = await cancelWorkPlansByProductionPlan(productionPlanId);
      setMessage(`생산계획 ${result.planNo}의 작업계획 ${result.cancelledCount}건을 취소했습니다.`);
      if (targetsSearched) {
        await loadTargets(buildTargetParams());
      }
      if (plansSearched) {
        await loadPlans(appliedParamsRef.current);
      }
    } catch (e) {
      setError(e instanceof Error ? e.message : '작업계획 취소에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const resetPlanFilters = () => {
    setPlanFilters({});
    setSelectedItem(null);
    setItemClearToken((token) => token + 1);
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>작업계획</h1>
        <p>MRP 산출이 완료된 생산계획에 대해 공정별 작업계획을 수립합니다. 취소 시 생산계획 작업계획 상태가 복원됩니다.</p>
      </header>

      <div className="tab-row">
        <button type="button" className={tab === 'targets' ? 'tab-active' : undefined} onClick={() => setTab('targets')}>
          수립 대상
        </button>
        <button type="button" className={tab === 'plans' ? 'tab-active' : undefined} onClick={() => setTab('plans')}>
          작업계획 목록
        </button>
      </div>

      {error && <p className="error-banner">{error}</p>}
      {message && <p className="success-banner">{message}</p>}

      {tab === 'targets' && (
        <>
          <div className="production-plan-list-filters">
            <ItemSearchField
              label="생산계획 품목"
              allowedClassifications={TARGET_ITEM_CLASSES}
              selectedItem={targetItem}
              onSelect={setTargetItem}
              clearToken={targetItemClearToken}
            />
            <label>
              생산계획등록일(부터)
              <input
                type="date"
                value={targetRegisteredFrom}
                onChange={(e) => setTargetRegisteredFrom(e.target.value)}
              />
            </label>
            <label>
              생산계획등록일(까지)
              <input
                type="date"
                value={targetRegisteredTo}
                onChange={(e) => setTargetRegisteredTo(e.target.value)}
              />
            </label>
            <div className="production-plan-list-filter-actions">
              <button type="button" onClick={() => void loadTargets(buildTargetParams())} disabled={loadingTargets}>
                {loadingTargets ? '조회 중…' : '검색'}
              </button>
              <button
                type="button"
                className="secondary"
                disabled={loadingTargets}
                onClick={() => {
                  setTargetItem(null);
                  setTargetRegisteredFrom('');
                  setTargetRegisteredTo('');
                  setTargetItemClearToken((token) => token + 1);
                }}
              >
                초기화
              </button>
            </div>
          </div>
          <section className="action-bar">
            <button type="button" disabled={submitting || selectedCount === 0} onClick={() => void onCreate()}>
              {submitting ? '수립 중…' : `작업계획 수립 (${selectedCount}건)`}
            </button>
          </section>

          {loadingTargets ? (
            <p>불러오는 중…</p>
          ) : !targetsSearched ? (
            <p>검색 조건을 입력한 뒤 검색 버튼을 눌러 주세요. 조건을 비우면 수립 대상 전체가 조회됩니다.</p>
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
                    <th>수주번호</th>
                    <th>거래처</th>
                    <th>품목</th>
                    <th className="num">계획수량</th>
                    <th>납기요구일</th>
                    <th>MRP</th>
                    <th>작업계획</th>
                  </tr>
                </thead>
                <tbody>
                  {targets.length === 0 ? (
                    <tr>
                      <td colSpan={9}>수립 대상 생산계획이 없습니다.</td>
                    </tr>
                  ) : (
                    targets.map((row) => (
                      <tr key={row.id}>
                        <td>
                          <input
                            type="checkbox"
                            checked={selectedPlanIds.has(row.id)}
                            disabled={submitting}
                            onChange={(e) => toggleRow(row.id, e.target.checked)}
                          />
                        </td>
                        <td>{row.planNo}</td>
                        <td>{row.orderNo}</td>
                        <td>{row.partnerName}</td>
                        <td>
                          {row.itemNo} {row.itemName}
                        </td>
                        <td className="num">{formatQty(row.plannedQty)}</td>
                        <td>{row.requestedDeliveryDate ?? '-'}</td>
                        <td>{row.mrpStatusLabel}</td>
                        <td>{row.workPlanStatusLabel}</td>
                      </tr>
                    ))
                  )}
                </tbody>
              </table>
            </div>
          )}
        </>
      )}

      {tab === 'plans' && (
        <>
          <section className="production-plan-list-filters">
            <ItemSearchField
              label="품목"
              selectedItem={selectedItem}
              onSelect={setSelectedItem}
              clearToken={itemClearToken}
            />
            <label>
              공정명
              <input
                value={planFilters.processName ?? ''}
                onChange={(e) => setPlanFilters((f) => ({ ...f, processName: e.target.value }))}
              />
            </label>
            <label>
              작업장
              <input
                value={planFilters.workCenterName ?? ''}
                onChange={(e) => setPlanFilters((f) => ({ ...f, workCenterName: e.target.value }))}
              />
            </label>
            <label>
              시작일(부터)
              <input
                type="date"
                value={planFilters.planStartDateFrom ?? ''}
                onChange={(e) => setPlanFilters((f) => ({ ...f, planStartDateFrom: e.target.value }))}
              />
            </label>
            <label>
              시작일(까지)
              <input
                type="date"
                value={planFilters.planStartDateTo ?? ''}
                onChange={(e) => setPlanFilters((f) => ({ ...f, planStartDateTo: e.target.value }))}
              />
            </label>
            <div className="production-plan-list-filter-actions">
              <button type="button" disabled={submitting || loadingPlans} onClick={() => void loadPlans(buildPlanParams())}>
                {loadingPlans ? '조회 중…' : '검색'}
              </button>
              <button type="button" className="secondary" disabled={submitting} onClick={resetPlanFilters}>
                초기화
              </button>
            </div>
          </section>

          {cancellableProductionPlanIds.size > 0 && (
            <section className="form-actions">
              {[...cancellableProductionPlanIds].map((productionPlanId) => {
                const sample = plans.find((row) => row.productionPlanId === productionPlanId);
                if (!sample) {
                  return null;
                }
                return (
                  <button
                    key={productionPlanId}
                    type="button"
                    className="secondary"
                    disabled={submitting}
                    onClick={() => void onCancelPlan(productionPlanId, sample.planNo)}
                  >
                    {`${sample.planNo} ${sample.itemNo} ${sample.itemName} 작업계획 전체 취소`}
                  </button>
                );
              })}
            </section>
          )}

          {loadingPlans ? (
            <p>불러오는 중…</p>
          ) : !plansSearched ? (
            <p>검색 조건을 입력한 뒤 검색 버튼을 눌러 주세요. 조건을 비우면 작업계획 전체가 조회됩니다.</p>
          ) : (
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>계획번호</th>
                    <th>품목</th>
                    <th>공정</th>
                    <th>작업구분</th>
                    <th>작업장</th>
                    <th className="num">계획수량</th>
                    <th>시작일</th>
                    <th className="num">표준(준비/가동)</th>
                    <th>상태</th>
                    <th>관리</th>
                  </tr>
                </thead>
                <tbody>
                  {plans.length === 0 ? (
                    <tr>
                      <td colSpan={10}>작업계획이 없습니다.</td>
                    </tr>
                  ) : (
                    plans.map((row) => (
                      <tr key={row.id}>
                        <td>{row.planNo}</td>
                        <td>
                          {row.itemNo} {row.itemName}
                        </td>
                        <td>
                          {row.processSequenceNum}. {row.processName}
                        </td>
                        <td>{row.workDistinctionLabel}</td>
                        <td>{row.workCenterName ?? '-'}</td>
                        <td className="num">{formatQty(row.plannedQty)}</td>
                        <td>{row.planStartDate ?? '-'}</td>
                        <td className="num">
                          {row.setupTime} / {row.standardTime}
                        </td>
                        <td>{row.statusLabel}</td>
                        <td className="actions">
                          {row.cancellable && (
                            <button
                              type="button"
                              className="btn-action danger"
                              disabled={submitting}
                              onClick={() =>
                                void onCancelLine(
                                  row.id,
                                  `${row.planNo} ${row.processSequenceNum}. ${row.processName}`,
                                )
                              }
                            >
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

