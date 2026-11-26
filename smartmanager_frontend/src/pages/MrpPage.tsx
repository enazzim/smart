import { Fragment, useCallback, useMemo, useState } from 'react';
import type { PropertyClassification } from '../api/item';
import type { ProductionPlan } from '../api/productionPlan';
import {
  calculateMrp,
  cancelMrpLine,
  cancelMrpPlan,
  cancelMrpRun,
  fetchGroupedMrpLines,
  fetchMrpRuns,
  fetchMrpTargets,
  type MaterialRequirementGrouped,
  type MaterialRequirementLine,
  type MrpLineSearchParams,
  type MrpRun,
  type MrpTargetParams,
} from '../api/mrp';
import GridExcelExportButton from '../components/GridExcelExportButton';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import { formatInteger, formatQty } from '../utils/numberFormat';
import { useConfirm } from '../context/ConfirmContext';

const PARENT_ITEM_CLASSES: PropertyClassification[] = ['제품', '공정품'];
const COMPONENT_ITEM_CLASSES: PropertyClassification[] = ['원자재', '부자재', '상품', '공정품', '제품', '팬텀'];

type MrpTab = 'targets' | 'lines';

function formatDateTime(value: string): string {
  const date = new Date(value);
  if (Number.isNaN(date.getTime())) {
    return value;
  }
  return date.toLocaleString();
}

function flattenGroupedLines(grouped: MaterialRequirementGrouped | null): MaterialRequirementLine[] {
  if (!grouped) {
    return [];
  }
  if (grouped.groupingMode === 'BY_PLAN') {
    return grouped.lines;
  }
  return grouped.groups.flatMap((group) => group.details);
}

function renderLineRow(
  line: MaterialRequirementLine,
  submitting: boolean,
  onCancelLine: (lineId: number, componentLabel: string) => void,
) {
  return (
    <tr key={line.id}>
      <td>{line.runNo}</td>
      <td>{line.planNo}</td>
      <td>
        {line.parentItemNo} — {line.parentItemName}
      </td>
      <td>
        {line.componentItemNo} — {line.componentItemName}
      </td>
      <td>{line.componentPropertyClassificationLabel}</td>
      <td>{line.unit}</td>
      <td className="num">{formatQty(line.bomUnitQty)}</td>
      <td className="num">{formatQty(line.plannedQty)}</td>
      <td className="num">{formatQty(line.grossQty)}</td>
      <td className="actions">
        {line.cancellable && (
          <button
            type="button"
            className="btn-action danger"
            disabled={submitting}
            onClick={() => void onCancelLine(line.id, `${line.componentItemNo} ${line.componentItemName}`)}
          >
            취소
          </button>
        )}
      </td>
    </tr>
  );
}

export default function MrpPage() {
  const confirm = useConfirm();
  const [tab, setTab] = useState<MrpTab>('targets');
  const [targets, setTargets] = useState<ProductionPlan[]>([]);
  const [targetsSearched, setTargetsSearched] = useState(false);
  const [targetItem, setTargetItem] = useState<ItemSearchSelection | null>(null);
  const [targetDeliveryFrom, setTargetDeliveryFrom] = useState('');
  const [targetDeliveryTo, setTargetDeliveryTo] = useState('');
  const [targetItemClearToken, setTargetItemClearToken] = useState(0);
  const [sessionRun, setSessionRun] = useState<MrpRun | null>(null);
  const [runCatalog, setRunCatalog] = useState<MrpRun[]>([]);
  const [grouped, setGrouped] = useState<MaterialRequirementGrouped | null>(null);
  const [linesSearched, setLinesSearched] = useState(false);
  const [pinnedRunId, setPinnedRunId] = useState<number | null>(null);
  const [lineParentItem, setLineParentItem] = useState<ItemSearchSelection | null>(null);
  const [lineComponentItem, setLineComponentItem] = useState<ItemSearchSelection | null>(null);
  const [lineDeliveryFrom, setLineDeliveryFrom] = useState('');
  const [lineDeliveryTo, setLineDeliveryTo] = useState('');
  const [lineParentClearToken, setLineParentClearToken] = useState(0);
  const [lineComponentClearToken, setLineComponentClearToken] = useState(0);
  const [expandedGroupIds, setExpandedGroupIds] = useState<Set<number>>(new Set());
  const [selectedPlanIds, setSelectedPlanIds] = useState<Set<number>>(new Set());
  const [loadingTargets, setLoadingTargets] = useState(false);
  const [loadingLines, setLoadingLines] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [targetError, setTargetError] = useState<string | null>(null);
  const [runError, setRunError] = useState<string | null>(null);
  const [lineError, setLineError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const flatLines = useMemo(() => flattenGroupedLines(grouped), [grouped]);

  const lineExportRows = useMemo(
    () =>
      flatLines.map((line) => ({
        산출번호: line.runNo,
        계획번호: line.planNo,
        상위품목: `${line.parentItemNo} — ${line.parentItemName}`,
        자재품목: `${line.componentItemNo} — ${line.componentItemName}`,
        자산분류: line.componentPropertyClassificationLabel,
        단위: line.unit,
        BOM단위수량: line.bomUnitQty,
        계획수량: line.plannedQty,
        총소요량: line.grossQty,
      })),
    [flatLines],
  );

  const loadTargets = useCallback(async (params: MrpTargetParams = {}) => {
    setLoadingTargets(true);
    setTargetError(null);
    try {
      const rows = await fetchMrpTargets(params);
      setTargets(rows);
      setTargetsSearched(true);
      setSelectedPlanIds((prev) => {
        const visible = new Set(rows.map((plan) => plan.id));
        return new Set([...prev].filter((id) => visible.has(id)));
      });
    } catch (e) {
      setTargetError(e instanceof Error ? e.message : '산출 대상 조회 실패');
      setTargets([]);
      setTargetsSearched(true);
    } finally {
      setLoadingTargets(false);
    }
  }, []);

  const loadGroupedLines = useCallback(async (params: MrpLineSearchParams = {}) => {
    setLoadingLines(true);
    setLineError(null);
    try {
      const [groupedResult, runs] = await Promise.all([fetchGroupedMrpLines(params), fetchMrpRuns()]);
      setGrouped(groupedResult);
      setRunCatalog(runs);
      setLinesSearched(true);
      setExpandedGroupIds(new Set());
    } catch (e) {
      setLineError(e instanceof Error ? e.message : '소요 자재 조회 실패');
      setGrouped(null);
      setLinesSearched(true);
    } finally {
      setLoadingLines(false);
    }
  }, []);

  const togglePlan = (planId: number, checked: boolean) => {
    setSelectedPlanIds((prev) => {
      const next = new Set(prev);
      if (checked) {
        next.add(planId);
      } else {
        next.delete(planId);
      }
      return next;
    });
  };

  const toggleAllTargets = (checked: boolean) => {
    if (checked) {
      setSelectedPlanIds(new Set(targets.map((plan) => plan.id)));
    } else {
      setSelectedPlanIds(new Set());
    }
  };

  const toggleGroupExpanded = (componentItemId: number) => {
    setExpandedGroupIds((prev) => {
      const next = new Set(prev);
      if (next.has(componentItemId)) {
        next.delete(componentItemId);
      } else {
        next.add(componentItemId);
      }
      return next;
    });
  };

  const buildTargetParams = (): MrpTargetParams => ({
    itemId: targetItem?.id,
    requestedDeliveryDateFrom: targetDeliveryFrom || undefined,
    requestedDeliveryDateTo: targetDeliveryTo || undefined,
  });

  const buildLineParams = (): MrpLineSearchParams => ({
    runId: pinnedRunId ?? undefined,
    parentItemId: lineParentItem?.id,
    componentItemId: lineComponentItem?.id,
    requestedDeliveryDateFrom: lineDeliveryFrom || undefined,
    requestedDeliveryDateTo: lineDeliveryTo || undefined,
  });

  const onCalculate = async () => {
    const planIds = [...selectedPlanIds];
    if (planIds.length === 0) {
      setMessage(null);
      setTargetError('산출할 생산계획을 1건 이상 선택하세요.');
      return;
    }
    setSubmitting(true);
    setMessage(null);
    setTargetError(null);
    try {
      const run = await calculateMrp(planIds);
      setMessage(`자재소요 산출을 완료했습니다. (${run.runNo}, ${run.lineCount}건)`);
      setSelectedPlanIds(new Set());
      setSessionRun(run);
      if (targetsSearched) {
        await loadTargets(buildTargetParams());
      }
    } catch (e) {
      setTargetError(e instanceof Error ? e.message : '자재소요 산출 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const handleSearchTargets = () => {
    void loadTargets(buildTargetParams());
  };

  const handleResetTargets = () => {
    setTargetItem(null);
    setTargetDeliveryFrom('');
    setTargetDeliveryTo('');
    setTargetItemClearToken((token) => token + 1);
  };

  const handleSearchLines = () => {
    setPinnedRunId(null);
    void loadGroupedLines({
      parentItemId: lineParentItem?.id,
      componentItemId: lineComponentItem?.id,
      requestedDeliveryDateFrom: lineDeliveryFrom || undefined,
      requestedDeliveryDateTo: lineDeliveryTo || undefined,
    });
  };

  const handleResetLines = () => {
    setLineParentItem(null);
    setLineComponentItem(null);
    setLineDeliveryFrom('');
    setLineDeliveryTo('');
    setPinnedRunId(null);
    setLineParentClearToken((token) => token + 1);
    setLineComponentClearToken((token) => token + 1);
  };

  const openRunLines = (runId: number) => {
    setLineParentItem(null);
    setLineComponentItem(null);
    setLineDeliveryFrom('');
    setLineDeliveryTo('');
    setLineParentClearToken((token) => token + 1);
    setLineComponentClearToken((token) => token + 1);
    setPinnedRunId(runId);
    setTab('lines');
    void loadGroupedLines({ runId });
  };

  const onCancelRun = async (runId: number, runNo: string) => {
    if (!(await confirm(`산출 이력 ${runNo} 전체를 취소하시겠습니까?`, { title: '취소 확인', confirmLabel: '예, 취소', cancelLabel: '닫기', danger: true }))) {
      return;
    }
    setSubmitting(true);
    setMessage(null);
    setRunError(null);
    try {
      await cancelMrpRun(runId);
      setMessage(`산출 이력 ${runNo}을(를) 취소했습니다.`);
      if (sessionRun?.id === runId) {
        setSessionRun(null);
      }
      if (pinnedRunId === runId) {
        setPinnedRunId(null);
      }
      if (targetsSearched) {
        await loadTargets(buildTargetParams());
      }
      if (linesSearched) {
        const nextRunId = pinnedRunId === runId ? undefined : pinnedRunId ?? undefined;
        await loadGroupedLines({
          runId: nextRunId,
          parentItemId: lineParentItem?.id,
          componentItemId: lineComponentItem?.id,
          requestedDeliveryDateFrom: lineDeliveryFrom || undefined,
          requestedDeliveryDateTo: lineDeliveryTo || undefined,
        });
      }
    } catch (e) {
      setRunError(e instanceof Error ? e.message : '산출 이력 취소 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancelPlanLines = async (productionPlanId: number, planNo: string) => {
    if (!(await confirm(`생산계획 ${planNo}의 자재소요를 전체 취소하시겠습니까?`, { title: '취소 확인', confirmLabel: '예, 취소', cancelLabel: '닫기', danger: true }))) {
      return;
    }
    setSubmitting(true);
    setMessage(null);
    setLineError(null);
    try {
      await cancelMrpPlan(productionPlanId);
      setMessage(`생산계획 ${planNo}의 자재소요를 취소했습니다.`);
      if (targetsSearched) {
        await loadTargets(buildTargetParams());
      }
      if (linesSearched) {
        await loadGroupedLines(buildLineParams());
      }
    } catch (e) {
      setLineError(e instanceof Error ? e.message : '계획 소요 취소 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancelLine = async (lineId: number, componentLabel: string) => {
    if (!(await confirm(`자재 ${componentLabel} 소요 1건을 취소하시겠습니까?`, { title: '취소 확인', confirmLabel: '예, 취소', cancelLabel: '닫기', danger: true }))) {
      return;
    }
    setSubmitting(true);
    setMessage(null);
    setLineError(null);
    try {
      await cancelMrpLine(lineId);
      setMessage('자재 소요 1건을 취소했습니다.');
      if (targetsSearched) {
        await loadTargets(buildTargetParams());
      }
      if (linesSearched) {
        await loadGroupedLines(buildLineParams());
      }
    } catch (e) {
      setLineError(e instanceof Error ? e.message : '소요 라인 취소 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const allSelected = targets.length > 0 && targets.every((plan) => selectedPlanIds.has(plan.id));

  const cancellablePlanIds = useMemo(() => {
    const linesByPlan = new Map<number, MaterialRequirementLine[]>();
    for (const line of flatLines) {
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
  }, [flatLines]);

  const hasLineData =
    grouped != null &&
    (grouped.groupingMode === 'BY_PLAN' ? grouped.lines.length > 0 : grouped.groups.length > 0);

  const runsInResult = useMemo(() => {
    const byId = new Map<number, { id: number; runNo: string; cancellable: boolean }>();
    for (const line of flatLines) {
      if (!byId.has(line.mrpRunId)) {
        const catalog = runCatalog.find((run) => run.id === line.mrpRunId);
        byId.set(line.mrpRunId, {
          id: line.mrpRunId,
          runNo: line.runNo,
          cancellable: catalog?.cancellable ?? false,
        });
      }
    }
    return [...byId.values()];
  }, [flatLines, runCatalog]);

  return (
    <div className="page">
      <header className="page-header">
        <div>
          <h1>자재소요</h1>
          <p>BOM 정전개 기준으로 생산계획별 자재 소요량을 산출합니다.</p>
        </div>
      </header>

      <div className="tab-row" aria-label="자재소요">
        <button
          type="button"
          className={tab === 'targets' ? 'tab-active' : undefined}
          onClick={() => setTab('targets')}
        >
          산출 대상
        </button>
        <button
          type="button"
          className={tab === 'lines' ? 'tab-active' : undefined}
          onClick={() => setTab('lines')}
        >
          소요 자재
        </button>
      </div>
      {message && <p>{message}</p>}
      {runError && <div className="error">{runError}</div>}

      {tab === 'targets' && (
      <>
      <section className="panel">
        <h2>산출 대상</h2>
        <p className="hint-text">
          자재소요가 <strong>미산출</strong>인 생산계획을 선택하여 BOM 정전개 기준으로 소요량을 산출합니다.
          산출 후 생산계획의 자재소요 상태가 <strong>산출완료</strong>로 변경됩니다.
        </p>
        <div className="production-plan-list-filters">
          <ItemSearchField
            label="생산 품목"
            allowedClassifications={PARENT_ITEM_CLASSES}
            selectedItem={targetItem}
            onSelect={setTargetItem}
            clearToken={targetItemClearToken}
          />
          <label>
            납기요구일(부터)
            <input type="date" value={targetDeliveryFrom} onChange={(e) => setTargetDeliveryFrom(e.target.value)} />
          </label>
          <label>
            납기요구일(까지)
            <input type="date" value={targetDeliveryTo} onChange={(e) => setTargetDeliveryTo(e.target.value)} />
          </label>
          <div className="production-plan-list-filter-actions">
            <button type="button" onClick={handleSearchTargets} disabled={loadingTargets}>
              {loadingTargets ? '조회 중…' : '검색'}
            </button>
            <button type="button" className="secondary" onClick={handleResetTargets} disabled={loadingTargets}>
              초기화
            </button>
          </div>
        </div>
        {targetError && <div className="error">{targetError}</div>}
        <div className="form-actions">
          <button
            type="button"
            disabled={submitting || selectedPlanIds.size === 0}
            onClick={() => void onCalculate()}
          >
            {submitting ? '처리 중…' : `선택 산출 (${selectedPlanIds.size})`}
          </button>
        </div>
        {loadingTargets ? (
          <p>불러오는 중…</p>
        ) : !targetsSearched ? (
          <p>검색 조건을 입력한 뒤 검색 버튼을 눌러 주세요. 조건을 비우면 미산출 계획 전체가 조회됩니다.</p>
        ) : targets.length === 0 ? (
          <p>산출 대상 생산계획이 없습니다.</p>
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
                    onChange={(e) => toggleAllTargets(e.target.checked)}
                  />
                </th>
                <th>계획번호</th>
                <th>수주번호</th>
                <th>거래처</th>
                <th>모품목</th>
                <th className="num">계획수량</th>
                <th>납기요구일</th>
              </tr>
            </thead>
            <tbody>
              {targets.map((plan) => (
                <tr key={plan.id}>
                  <td>
                    <input
                      type="checkbox"
                      checked={selectedPlanIds.has(plan.id)}
                      onChange={(e) => togglePlan(plan.id, e.target.checked)}
                    />
                  </td>
                  <td>{plan.planNo}</td>
                  <td>{plan.orderNo}</td>
                  <td>{plan.partnerName}</td>
                  <td>
                    {plan.itemNo} — {plan.itemName}
                  </td>
                  <td className="num">{formatQty(plan.plannedQty)}</td>
                  <td>{plan.requestedDeliveryDate ?? '—'}</td>
                </tr>
              ))}
            </tbody>
          </table>
          </div>
        )}
      </section>

      <section className="panel">
        <h2>이번 산출</h2>
        <p className="hint-text">이 화면에서 방금 산출한 건만 표시됩니다. 화면을 다시 열면 비고, 과거 산출은 소요 자재 탭에서 조회합니다.</p>
        {sessionRun == null ? (
          <p>이번 화면에서 산출한 이력이 없습니다.</p>
        ) : (
          <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>산출번호</th>
                <th className="num">계획 건수</th>
                <th className="num">소요 라인</th>
                <th>산출일시</th>
                <th>산출자</th>
                <th>관리</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>{sessionRun.runNo}</td>
                <td className="num">{formatInteger(sessionRun.planCount)}</td>
                <td className="num">{formatInteger(sessionRun.lineCount)}</td>
                <td>{formatDateTime(sessionRun.createdAt)}</td>
                <td>{sessionRun.createdBy ?? '—'}</td>
                <td className="actions">
                  <button type="button" className="btn-action" onClick={() => openRunLines(sessionRun.id)}>
                    소요 보기
                  </button>
                  {sessionRun.cancellable && (
                    <button
                      type="button"
                      className="btn-action danger"
                      disabled={submitting}
                      onClick={() => void onCancelRun(sessionRun.id, sessionRun.runNo)}
                    >
                      취소
                    </button>
                  )}
                </td>
              </tr>
            </tbody>
          </table>
          </div>
        )}
      </section>
      </>
      )}

      {tab === 'lines' && (
      <section className="panel">
        <div className="panel-header-row">
          <h2>소요 자재{pinnedRunId != null ? ' (선택한 산출)' : ''}</h2>
          <GridExcelExportButton fileBaseName="소요자재목록" disabled={loadingLines} rows={lineExportRows} />
        </div>
        <p className="hint-text">
          집계 모드: <strong>{grouped?.groupingModeLabel ?? '—'}</strong> (시스템정보 &gt; 시스템 설정에서
          변경). 라인 1건 취소 시 해당 생산계획에 잔여 소요가 있으면 <strong>산출완료</strong>를 유지합니다
          (정책 A).
        </p>
        <div className="production-plan-list-filters">
          <ItemSearchField
            label="소요자재 품목"
            allowedClassifications={COMPONENT_ITEM_CLASSES}
            selectedItem={lineComponentItem}
            onSelect={setLineComponentItem}
            clearToken={lineComponentClearToken}
          />
          <ItemSearchField
            label="생산계획 품목"
            allowedClassifications={PARENT_ITEM_CLASSES}
            selectedItem={lineParentItem}
            onSelect={setLineParentItem}
            clearToken={lineParentClearToken}
          />
          <label>
            납기요구일(부터)
            <input type="date" value={lineDeliveryFrom} onChange={(e) => setLineDeliveryFrom(e.target.value)} />
          </label>
          <label>
            납기요구일(까지)
            <input type="date" value={lineDeliveryTo} onChange={(e) => setLineDeliveryTo(e.target.value)} />
          </label>
          <div className="production-plan-list-filter-actions">
            <button type="button" onClick={handleSearchLines} disabled={loadingLines}>
              {loadingLines ? '조회 중…' : '검색'}
            </button>
            <button type="button" className="secondary" onClick={handleResetLines} disabled={loadingLines}>
              초기화
            </button>
          </div>
        </div>
        {pinnedRunId != null && (
          <p className="hint-text">산출 이력에서 연 산출번호만 보고 있습니다. 검색을 누르면 아래 조건으로 다시 조회합니다.</p>
        )}
        {lineError && <div className="error">{lineError}</div>}
        {runsInResult.length > 0 && (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>산출번호</th>
                  <th>관리</th>
                </tr>
              </thead>
              <tbody>
                {runsInResult.map((run) => (
                  <tr key={run.id}>
                    <td>{run.runNo}</td>
                    <td className="actions">
                      {run.cancellable && (
                        <button
                          type="button"
                          className="btn-action danger"
                          disabled={submitting}
                          onClick={() => void onCancelRun(run.id, run.runNo)}
                        >
                          산출 전체 취소
                        </button>
                      )}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
        {cancellablePlanIds.size > 0 && (
          <div className="form-actions">
            {[...cancellablePlanIds].map((planId) => {
              const sample = flatLines.find((line) => line.productionPlanId === planId);
              if (!sample) {
                return null;
              }
              return (
                <button
                  key={planId}
                  type="button"
                  className="secondary"
                  disabled={submitting}
                  onClick={() => void onCancelPlanLines(planId, sample.planNo)}
                >
                  {sample.planNo} 소요 전체 취소
                </button>
              );
            })}
          </div>
        )}
        {loadingLines ? (
          <p>불러오는 중…</p>
        ) : !linesSearched ? (
          <p>검색 조건을 입력한 뒤 검색 버튼을 눌러 주세요. 조건을 비우면 산출된 소요 전체가 조회됩니다.</p>
        ) : !hasLineData ? (
          <p>소요 자재가 없습니다.</p>
        ) : grouped?.groupingMode === 'BY_COMPONENT' ? (
          <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th />
                <th>자재품목</th>
                <th>자산분류</th>
                <th>단위</th>
                <th className="num">합산 총소요량</th>
                <th className="num">상세 건수</th>
              </tr>
            </thead>
            <tbody>
              {grouped.groups.map((group) => {
                const expanded = expandedGroupIds.has(group.componentItemId);
                return (
                  <Fragment key={group.componentItemId}>
                    <tr>
                      <td>
                        <button
                          type="button"
                          className="btn-action"
                          onClick={() => toggleGroupExpanded(group.componentItemId)}
                        >
                          {expanded ? '접기' : '펼치기'}
                        </button>
                      </td>
                      <td>
                        {group.componentItemNo} — {group.componentItemName}
                      </td>
                      <td>{group.componentPropertyClassificationLabel}</td>
                      <td>{group.unit}</td>
                      <td className="num">{formatQty(group.totalGrossQty)}</td>
                      <td className="num">{formatInteger(group.lineCount)}</td>
                    </tr>
                    {expanded && (
                      <tr>
                        <td colSpan={6}>
                          <table>
                            <thead>
                              <tr>
                                <th>산출번호</th>
                                <th>계획번호</th>
                                <th>모품목</th>
                                <th>자재품목</th>
                                <th>자산분류</th>
                                <th>단위</th>
                                <th className="num">단위소요</th>
                                <th className="num">계획수량</th>
                                <th className="num">총소요량</th>
                                <th>관리</th>
                              </tr>
                            </thead>
                            <tbody>
                              {group.details.map((line) =>
                                renderLineRow(line, submitting, (lineId, label) => void onCancelLine(lineId, label)),
                              )}
                            </tbody>
                          </table>
                        </td>
                      </tr>
                    )}
                  </Fragment>
                );
              })}
            </tbody>
          </table>
          </div>
        ) : (
          <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>산출번호</th>
                <th>계획번호</th>
                <th>모품목</th>
                <th>자재품목</th>
                <th>자산분류</th>
                <th>단위</th>
                <th className="num">단위소요</th>
                <th className="num">계획수량</th>
                <th className="num">총소요량</th>
                <th>관리</th>
              </tr>
            </thead>
            <tbody>
              {grouped?.lines.map((line) =>
                renderLineRow(line, submitting, (lineId, label) => void onCancelLine(lineId, label)),
              )}
            </tbody>
          </table>
          </div>
        )}
      </section>
      )}
    </div>
  );
}
