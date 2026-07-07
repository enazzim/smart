import { useCallback, useEffect, useState } from 'react';
import type { SalesOrderLineListRow } from '../api/salesOrder';
import CompanySearchField, { type CompanySearchSelection } from '../components/CompanySearchField';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import type { PropertyClassification } from '../api/item';
import {
  cancelProductionPlan,
  createProductionPlans,
  createStandaloneProductionPlans,
  fetchProductionPlanCandidates,
  fetchProductionPlans,
  type ProductionPlan,
  type ProductionPlanMrpStatus,
  type ProductionPlanSearchParams,
  type ProductionPlanWorkPlanStatus,
} from '../api/productionPlan';

const PLAN_ITEM_CLASSES: PropertyClassification[] = ['제품', '공정품'];

const PLAN_MRP_STATUS_OPTIONS: { value: ProductionPlanMrpStatus | ''; label: string }[] = [
  { value: '', label: '전체' },
  { value: 'NOT_CALCULATED', label: '미산출' },
  { value: 'CALCULATED', label: '산출완료' },
];

const PLAN_WORK_PLAN_STATUS_OPTIONS: { value: ProductionPlanWorkPlanStatus | ''; label: string }[] = [
  { value: '', label: '전체' },
  { value: 'NOT_PLANNED', label: '미수립' },
  { value: 'PLANNED', label: '수립' },
];

function formatQty(value: number): string {
  return Number.isInteger(value) ? String(value) : value.toLocaleString(undefined, { maximumFractionDigits: 4 });
}

export default function ProductionPlanPage() {
  const [candidates, setCandidates] = useState<SalesOrderLineListRow[]>([]);
  const [plans, setPlans] = useState<ProductionPlan[]>([]);
  const [selectedLineIds, setSelectedLineIds] = useState<Set<number>>(new Set());
  const [plannedQtyByLineId, setPlannedQtyByLineId] = useState<Record<number, number>>({});
  const [loadingCandidates, setLoadingCandidates] = useState(true);
  const [loadingPlans, setLoadingPlans] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [candidateError, setCandidateError] = useState<string | null>(null);
  const [planError, setPlanError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);
  const [standaloneItem, setStandaloneItem] = useState<ItemSearchSelection | null>(null);
  const [standaloneQty, setStandaloneQty] = useState('1');
  const [standaloneDeliveryDate, setStandaloneDeliveryDate] = useState('');
  const [standaloneError, setStandaloneError] = useState<string | null>(null);

  const [searchPartner, setSearchPartner] = useState<CompanySearchSelection | null>(null);
  const [searchItem, setSearchItem] = useState<ItemSearchSelection | null>(null);
  const [searchDeliveryFrom, setSearchDeliveryFrom] = useState('');
  const [searchDeliveryTo, setSearchDeliveryTo] = useState('');
  const [searchMrpStatus, setSearchMrpStatus] = useState<ProductionPlanMrpStatus | ''>('');
  const [searchWorkPlanStatus, setSearchWorkPlanStatus] = useState<ProductionPlanWorkPlanStatus | ''>('');

  const loadCandidates = useCallback(async () => {
    setLoadingCandidates(true);
    setCandidateError(null);
    try {
      const rows = await fetchProductionPlanCandidates();
      setCandidates(rows);
      setPlannedQtyByLineId((prev) => {
        const next = { ...prev };
        for (const row of rows) {
          if (next[row.lineId] === undefined) {
            next[row.lineId] = row.orderQty;
          }
        }
        return next;
      });
    } catch (e) {
      setCandidateError(e instanceof Error ? e.message : '수립 대기 목록 조회 실패');
      setCandidates([]);
    } finally {
      setLoadingCandidates(false);
    }
  }, []);

  const loadPlans = useCallback(async (params: ProductionPlanSearchParams = {}) => {
    setLoadingPlans(true);
    setPlanError(null);
    try {
      setPlans(await fetchProductionPlans(params));
    } catch (e) {
      setPlanError(e instanceof Error ? e.message : '생산계획 목록 조회 실패');
      setPlans([]);
    } finally {
      setLoadingPlans(false);
    }
  }, []);

  useEffect(() => {
    void loadCandidates();
    void loadPlans();
  }, [loadCandidates, loadPlans]);

  const toggleLine = (lineId: number, checked: boolean) => {
    setSelectedLineIds((prev) => {
      const next = new Set(prev);
      if (checked) {
        next.add(lineId);
      } else {
        next.delete(lineId);
      }
      return next;
    });
  };

  const toggleAllCandidates = (checked: boolean) => {
    if (checked) {
      setSelectedLineIds(new Set(candidates.map((row) => row.lineId)));
    } else {
      setSelectedLineIds(new Set());
    }
  };

  const updatePlannedQty = (lineId: number, value: number) => {
    setPlannedQtyByLineId((prev) => ({ ...prev, [lineId]: value }));
  };

  const buildCreateLines = (lineIds: number[]) =>
    lineIds
      .filter((lineId) => Number.isFinite(lineId) && lineId > 0)
      .map((lineId) => ({
        salesOrderLineId: lineId,
        plannedQty: plannedQtyByLineId[lineId] ?? candidates.find((row) => row.lineId === lineId)?.orderQty ?? 0,
      }));

  const onCreatePlans = async (lineIds: number[]) => {
    const normalizedLineIds = lineIds.filter((lineId) => Number.isFinite(lineId) && lineId > 0);
    if (normalizedLineIds.length === 0) {
      setMessage(null);
      setPlanError(null);
      setCandidateError('수립할 수주 라인을 1건 이상 선택하세요.');
      return;
    }
    const createLines = buildCreateLines(normalizedLineIds);
    if (createLines.length === 0) {
      setCandidateError('수립할 수주 라인을 1건 이상 선택하세요.');
      return;
    }
    const invalid = createLines.find((line) => !Number.isFinite(line.plannedQty) || line.plannedQty <= 0);
    if (invalid) {
      setPlanError(null);
      setCandidateError('계획수량은 0보다 커야 합니다.');
      return;
    }
    setSubmitting(true);
    setMessage(null);
    setPlanError(null);
    setCandidateError(null);
    try {
      const created = await createProductionPlans(createLines);
      setMessage(`${created.length}건의 생산계획을 수립했습니다.`);
      setSelectedLineIds(new Set());
      await loadCandidates();
      await loadPlans(buildPlanSearchParams());
    } catch (e) {
      setCandidateError(e instanceof Error ? e.message : '생산계획 수립 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onCreateStandalone = async () => {
    if (!standaloneItem) {
      setStandaloneError('품목을 선택하세요.');
      return;
    }
    const plannedQty = Number(standaloneQty);
    if (!Number.isFinite(plannedQty) || plannedQty <= 0) {
      setStandaloneError('계획수량은 0보다 커야 합니다.');
      return;
    }
    setSubmitting(true);
    setStandaloneError(null);
    setPlanError(null);
    setMessage(null);
    try {
      const created = await createStandaloneProductionPlans([
        {
          itemId: standaloneItem.id,
          plannedQty,
          requestedDeliveryDate: standaloneDeliveryDate || undefined,
        },
      ]);
      setMessage(`수주 없이 생산계획 ${created[0]?.planNo ?? ''}을(를) 등록했습니다.`);
      setStandaloneItem(null);
      setStandaloneQty('1');
      setStandaloneDeliveryDate('');
      await loadPlans(buildPlanSearchParams());
    } catch (e) {
      setStandaloneError(e instanceof Error ? e.message : '생산계획 직접 등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const buildPlanSearchParams = (): ProductionPlanSearchParams => ({
    partnerId: searchPartner?.id,
    itemId: searchItem?.id,
    requestedDeliveryDateFrom: searchDeliveryFrom || undefined,
    requestedDeliveryDateTo: searchDeliveryTo || undefined,
  });

  const filteredPlans = plans.filter((plan) => {
    if (searchMrpStatus && plan.mrpStatus !== searchMrpStatus) {
      return false;
    }
    if (searchWorkPlanStatus && plan.workPlanStatus !== searchWorkPlanStatus) {
      return false;
    }
    return true;
  });

  const handleSearchPlans = () => {
    void loadPlans(buildPlanSearchParams());
  };

  const handleResetPlanSearch = () => {
    setSearchPartner(null);
    setSearchItem(null);
    setSearchDeliveryFrom('');
    setSearchDeliveryTo('');
    setSearchMrpStatus('');
    setSearchWorkPlanStatus('');
    void loadPlans({});
  };

  const canCancelPlan = (plan: ProductionPlan) => plan.cancellable;

  const onCancelPlan = async (plan: ProductionPlan) => {
    const confirmMessage =
      plan.sourceType === 'MANUAL'
        ? '생산계획을 취소하시겠습니까? (수주와 연결되지 않은 계획입니다.)'
        : '생산계획을 취소하시겠습니까? 수주 라인 이행상태가 대기로 되돌아가며, 해당 수주에 남은 생산계획이 없으면 수주는 작성중으로 복원됩니다.';
    if (!window.confirm(confirmMessage)) {
      return;
    }
    setSubmitting(true);
    setPlanError(null);
    setCandidateError(null);
    try {
      await cancelProductionPlan(plan.id);
      setMessage('생산계획을 취소했습니다.');
      await loadCandidates();
      await loadPlans(buildPlanSearchParams());
    } catch (e) {
      setPlanError(e instanceof Error ? e.message : '생산계획 취소 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const allSelected = candidates.length > 0 && candidates.every((row) => selectedLineIds.has(row.lineId));

  return (
    <div className="page">
      <h1>생산계획</h1>

      <section className="panel">
        <h2>계획 직접 추가</h2>
        <p className="hint-text">
          수주 없이 <strong>제품·공정품</strong> 생산계획을 등록합니다. 수주 확정·이행상태는 변경되지 않습니다.
        </p>
        {standaloneError && <div className="error">{standaloneError}</div>}
        <div className="form-grid-wide">
          <ItemSearchField
            label="품목"
            allowedClassifications={PLAN_ITEM_CLASSES}
            selectedItem={standaloneItem}
            onSelect={setStandaloneItem}
          />
          <label>
            계획수량
            <input
              type="number"
              min={0.0001}
              step="any"
              value={standaloneQty}
              onChange={(e) => setStandaloneQty(e.target.value)}
            />
          </label>
          <label>
            납기요구일
            <input
              type="date"
              value={standaloneDeliveryDate}
              onChange={(e) => setStandaloneDeliveryDate(e.target.value)}
            />
          </label>
        </div>
        <div className="form-actions">
          <button type="button" disabled={submitting} onClick={() => void onCreateStandalone()}>
            {submitting ? '등록 중…' : '계획 추가'}
          </button>
        </div>
      </section>

      <section className="panel">
        <h2>수립 대기</h2>
        <p className="hint-text">
          이행상태가 <strong>대기</strong>이고 생산 라우트(제품·공정품)이며 납품이 완료되지 않은 수주 라인입니다.
          생산계획 수립 시 해당 수주는 자동으로 <strong>확정</strong>되며, 라인 이행상태는 <strong>진행</strong>으로 바뀝니다.
          계획수량은 수주수량과 다르게 지정할 수 있으며, 수주 원장의 수주수량은 변경되지 않습니다.
        </p>
        {candidateError && <div className="error">{candidateError}</div>}
        <div className="form-actions">
          <button
            type="button"
            disabled={submitting || selectedLineIds.size === 0}
            onClick={() => void onCreatePlans([...selectedLineIds])}
          >
            {submitting ? '수립 중…' : `선택 수립 (${selectedLineIds.size})`}
          </button>
          <button type="button" className="secondary" onClick={() => void loadCandidates()}>
            새로고침
          </button>
        </div>
        {loadingCandidates ? (
          <p>불러오는 중…</p>
        ) : candidates.length === 0 ? (
          <p>수립 대기 중인 수주 라인이 없습니다.</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>
                  <input
                    type="checkbox"
                    aria-label="전체 선택"
                    checked={allSelected}
                    onChange={(e) => toggleAllCandidates(e.target.checked)}
                  />
                </th>
                <th>수주번호</th>
                <th>거래처</th>
                <th>품목</th>
                <th>수주수량</th>
                <th>계획수량</th>
                <th>납기요구일</th>
                <th>납품상태</th>
                <th>수주일</th>
                <th>관리</th>
              </tr>
            </thead>
            <tbody>
              {candidates.map((row) => (
                <tr key={row.lineId}>
                  <td>
                    <input
                      type="checkbox"
                      checked={selectedLineIds.has(row.lineId)}
                      onChange={(e) => toggleLine(row.lineId, e.target.checked)}
                    />
                  </td>
                  <td>{row.orderNo}</td>
                  <td>{row.partnerName}</td>
                  <td>
                    {row.itemNo} — {row.itemName}
                  </td>
                  <td>{formatQty(row.orderQty)}</td>
                  <td>
                    <input
                      type="number"
                      min={0.0001}
                      step="any"
                      className="qty-input"
                      value={plannedQtyByLineId[row.lineId] ?? row.orderQty}
                      onChange={(e) => updatePlannedQty(row.lineId, Number(e.target.value))}
                    />
                  </td>
                  <td>{row.requestedDeliveryDate ?? '—'}</td>
                  <td>{row.deliveryStatusLabel}</td>
                  <td>{row.orderDate}</td>
                  <td className="actions">
                    <button
                      type="button"
                      className="btn-action"
                      disabled={submitting}
                      onClick={() => void onCreatePlans([row.lineId])}
                    >
                      수립
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </section>

      <section className="panel">
        <h2>생산계획 목록</h2>
        <p className="hint-text">
          취소는 <strong>자재소요 미산출</strong>이고 <strong>작업계획 미수립</strong>인 경우에만 가능합니다.
        </p>
        <div className="form-grid-wide">
          <CompanySearchField
            label="거래처"
            partnerType="SALES"
            selectedCompany={searchPartner}
            onSelect={setSearchPartner}
          />
          <ItemSearchField
            label="품목"
            allowedClassifications={PLAN_ITEM_CLASSES}
            selectedItem={searchItem}
            onSelect={setSearchItem}
          />
          <label>
            납기요구일(부터)
            <input
              type="date"
              value={searchDeliveryFrom}
              onChange={(e) => setSearchDeliveryFrom(e.target.value)}
            />
          </label>
          <label>
            납기요구일(까지)
            <input
              type="date"
              value={searchDeliveryTo}
              onChange={(e) => setSearchDeliveryTo(e.target.value)}
            />
          </label>
          <label>
            자재소요
            <select
              value={searchMrpStatus}
              onChange={(e) => setSearchMrpStatus(e.target.value as ProductionPlanMrpStatus | '')}
            >
              {PLAN_MRP_STATUS_OPTIONS.map((option) => (
                <option key={option.value || 'all'} value={option.value}>
                  {option.label}
                </option>
              ))}
            </select>
          </label>
          <label>
            작업계획
            <select
              value={searchWorkPlanStatus}
              onChange={(e) => setSearchWorkPlanStatus(e.target.value as ProductionPlanWorkPlanStatus | '')}
            >
              {PLAN_WORK_PLAN_STATUS_OPTIONS.map((option) => (
                <option key={option.value || 'all'} value={option.value}>
                  {option.label}
                </option>
              ))}
            </select>
          </label>
        </div>
        <div className="form-actions">
          <button type="button" onClick={handleSearchPlans}>
            검색
          </button>
          <button type="button" className="secondary" onClick={handleResetPlanSearch}>
            초기화
          </button>
        </div>
        {message && <p>{message}</p>}
        {planError && <div className="error">{planError}</div>}
        {loadingPlans ? (
          <p>불러오는 중…</p>
        ) : filteredPlans.length === 0 ? (
          <p>등록된 생산계획이 없습니다.</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>계획번호</th>
                <th>출처</th>
                <th>수주번호</th>
                <th>거래처</th>
                <th>품목</th>
                <th>계획수량</th>
                <th>생산수량</th>
                <th>납기요구일</th>
                <th>자재소요</th>
                <th>작업계획</th>
                <th>수주일</th>
                <th>관리</th>
              </tr>
            </thead>
            <tbody>
              {filteredPlans.map((plan) => (
                <tr key={plan.id}>
                  <td>{plan.planNo}</td>
                  <td>{plan.sourceTypeLabel}</td>
                  <td>{plan.orderNo ?? '—'}</td>
                  <td>{plan.partnerName ?? '—'}</td>
                  <td>
                    {plan.itemNo} — {plan.itemName}
                  </td>
                  <td>{formatQty(plan.plannedQty)}</td>
                  <td>{formatQty(plan.producedQty)}</td>
                  <td>{plan.requestedDeliveryDate ?? '—'}</td>
                  <td>{plan.mrpStatusLabel}</td>
                  <td>{plan.workPlanStatusLabel}</td>
                  <td>{plan.orderDate ?? '—'}</td>
                  <td className="actions">
                    {canCancelPlan(plan) && (
                      <button
                        type="button"
                        className="btn-action danger"
                        disabled={submitting}
                        onClick={() => void onCancelPlan(plan)}
                      >
                        취소
                      </button>
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </section>
    </div>
  );
}
