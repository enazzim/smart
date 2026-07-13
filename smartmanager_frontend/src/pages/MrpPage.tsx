import { Fragment, useCallback, useEffect, useMemo, useState } from 'react';
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
  type MrpRun,
} from '../api/mrp';
import GridExcelExportButton from '../components/GridExcelExportButton';
import { formatInteger, formatQty } from '../utils/numberFormat';
import { useConfirm } from '../context/ConfirmContext';

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
  const [targets, setTargets] = useState<ProductionPlan[]>([]);
  const [runs, setRuns] = useState<MrpRun[]>([]);
  const [grouped, setGrouped] = useState<MaterialRequirementGrouped | null>(null);
  const [expandedGroupIds, setExpandedGroupIds] = useState<Set<number>>(new Set());
  const [selectedPlanIds, setSelectedPlanIds] = useState<Set<number>>(new Set());
  const [selectedRunId, setSelectedRunId] = useState<number | null>(null);
  const [loadingTargets, setLoadingTargets] = useState(true);
  const [loadingRuns, setLoadingRuns] = useState(true);
  const [loadingLines, setLoadingLines] = useState(true);
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

  const loadTargets = useCallback(async () => {
    setLoadingTargets(true);
    setTargetError(null);
    try {
      setTargets(await fetchMrpTargets());
    } catch (e) {
      setTargetError(e instanceof Error ? e.message : '산출 대상 조회 실패');
      setTargets([]);
    } finally {
      setLoadingTargets(false);
    }
  }, []);

  const loadRuns = useCallback(async () => {
    setLoadingRuns(true);
    setRunError(null);
    try {
      setRuns(await fetchMrpRuns());
    } catch (e) {
      setRunError(e instanceof Error ? e.message : '산출 이력 조회 실패');
      setRuns([]);
    } finally {
      setLoadingRuns(false);
    }
  }, []);

  const loadGroupedLines = useCallback(async (runId: number | null) => {
    setLoadingLines(true);
    setLineError(null);
    try {
      setGrouped(await fetchGroupedMrpLines(runId));
      setExpandedGroupIds(new Set());
    } catch (e) {
      setLineError(e instanceof Error ? e.message : '소요 자재 조회 실패');
      setGrouped(null);
    } finally {
      setLoadingLines(false);
    }
  }, []);

  const refreshAll = useCallback(async () => {
    await loadTargets();
    await loadRuns();
    await loadGroupedLines(selectedRunId);
  }, [loadGroupedLines, loadRuns, loadTargets, selectedRunId]);

  useEffect(() => {
    void loadTargets();
    void loadRuns();
    void loadGroupedLines(null);
  }, [loadTargets, loadRuns, loadGroupedLines]);

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
      setSelectedRunId(run.id);
      await loadTargets();
      await loadRuns();
      await loadGroupedLines(run.id);
    } catch (e) {
      setTargetError(e instanceof Error ? e.message : '자재소요 산출 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onSelectRun = (runId: number | null) => {
    setSelectedRunId(runId);
    void loadGroupedLines(runId);
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
      if (selectedRunId === runId) {
        setSelectedRunId(null);
      }
      await refreshAll();
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
      await refreshAll();
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
      await refreshAll();
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

  return (
    <div className="page">
      <header className="page-header">
        <div>
          <h1>자재소요</h1>
          <p>BOM 정전개 기준으로 생산계획별 자재 소요량을 산출합니다.</p>
        </div>
      </header>

      <section className="panel">
        <h2>산출 대상</h2>
        <p className="hint-text">
          자재소요가 <strong>미산출</strong>인 생산계획을 선택하여 BOM 정전개 기준으로 소요량을 산출합니다.
          산출 후 생산계획의 자재소요 상태가 <strong>산출완료</strong>로 변경됩니다.
        </p>
        {targetError && <div className="error">{targetError}</div>}
        <div className="form-actions">
          <button
            type="button"
            disabled={submitting || selectedPlanIds.size === 0}
            onClick={() => void onCalculate()}
          >
            {submitting ? '처리 중…' : `선택 산출 (${selectedPlanIds.size})`}
          </button>
          <button type="button" className="secondary" onClick={() => void loadTargets()}>
            새로고침
          </button>
        </div>
        {loadingTargets ? (
          <p>불러오는 중…</p>
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
        <h2>산출 이력</h2>
        {message && <p>{message}</p>}
        {runError && <div className="error">{runError}</div>}
        <div className="form-actions">
          <button type="button" className="secondary" onClick={() => onSelectRun(null)}>
            전체 소요 보기
          </button>
          <button type="button" className="secondary" onClick={() => void loadRuns()}>
            새로고침
          </button>
        </div>
        {loadingRuns ? (
          <p>불러오는 중…</p>
        ) : runs.length === 0 ? (
          <p>산출 이력이 없습니다.</p>
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
              {runs.map((run) => (
                <tr key={run.id} className={selectedRunId === run.id ? 'row-selected' : undefined}>
                  <td>{run.runNo}</td>
                  <td className="num">{formatInteger(run.planCount)}</td>
                  <td className="num">{formatInteger(run.lineCount)}</td>
                  <td>{formatDateTime(run.createdAt)}</td>
                  <td>{run.createdBy ?? '—'}</td>
                  <td className="actions">
                    <button type="button" className="btn-action" onClick={() => onSelectRun(run.id)}>
                      소요 보기
                    </button>
                    {run.cancellable && (
                      <button
                        type="button"
                        className="btn-action danger"
                        disabled={submitting}
                        onClick={() => void onCancelRun(run.id, run.runNo)}
                      >
                        취소
                      </button>
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          </div>
        )}
      </section>

      <section className="panel">
        <div className="panel-header-row">
          <h2>소요 자재 목록{selectedRunId != null ? ` (산출 #${selectedRunId})` : ''}</h2>
          <GridExcelExportButton fileBaseName="소요자재목록" disabled={loadingLines} rows={lineExportRows} />
        </div>
        <p className="hint-text">
          집계 모드: <strong>{grouped?.groupingModeLabel ?? '—'}</strong> (시스템정보 &gt; 시스템 설정에서
          변경). 라인 1건 취소 시 해당 생산계획에 잔여 소요가 있으면 <strong>산출완료</strong>를 유지합니다
          (정책 A).
        </p>
        {lineError && <div className="error">{lineError}</div>}
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
    </div>
  );
}
