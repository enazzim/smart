import { useCallback, useEffect, useState } from 'react';
import {
  cancelWorkReport,
  createWorkReport,
  fetchWorkReportConsumptionStatus,
  fetchWorkReportIssueOnHand,
  fetchWorkReportTargets,
  fetchWorkReports,
  type WorkReport,
  type WorkReportConsumptionLine,
  type WorkReportListParams,
} from '../api/workReport';
import type { WorkOrder } from '../api/workOrder';
import { fetchWorkStandards } from '../api/workStandard';
import { useMaterialIssueSetting } from '../context/MaterialIssueSettingContext';
import WorkerSearchField from '../components/WorkerSearchField';

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function formatQty(value: number): string {
  return Number.isInteger(value) ? String(value) : value.toLocaleString(undefined, { maximumFractionDigits: 4 });
}

function parseQty(value: string): number | null {
  const parsed = Number(value);
  return Number.isFinite(parsed) && parsed >= 0 ? parsed : null;
}

function calcIssueQty(unitRatio: number, goodQty: number): string {
  if (!Number.isFinite(goodQty) || goodQty < 0) return '0';
  const raw = unitRatio * goodQty;
  return String(Math.round(raw * 10000) / 10000);
}

function consumptionLineKey(line: { itemCompositionId: number | null; itemId: number }): string {
  return line.itemCompositionId != null ? `bom-${line.itemCompositionId}` : `item-${line.itemId}`;
}

function formatConsumptionClassification(line: {
  propertyClassification: string;
  sourceProcessName?: string | null;
}): string {
  if (line.sourceProcessName) {
    return `${line.propertyClassification} · ${line.sourceProcessName}`;
  }
  return line.propertyClassification;
}

interface IssueLineEdit {
  lineKey: string;
  itemCompositionId: number | null;
  itemId: number;
  itemNo: string;
  itemName: string;
  propertyClassification: string;
  sourceProcessName: string | null;
  unitRatio: number;
  requiredQty: number;
  onHandQty: number;
  checked: boolean;
  issueQty: string;
}

export default function WorkReportPage() {
  const { materialIssueEnabled, setMaterialIssueEnabled, negativeStockAllowed } = useMaterialIssueSetting();
  const [targets, setTargets] = useState<WorkOrder[]>([]);
  const [reports, setReports] = useState<WorkReport[]>([]);
  const [filters, setFilters] = useState<WorkReportListParams>({});
  const [loadingTargets, setLoadingTargets] = useState(true);
  const [loadingReports, setLoadingReports] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [modalError, setModalError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);
  const [tab, setTab] = useState<'targets' | 'reports'>('targets');
  const [selected, setSelected] = useState<WorkOrder | null>(null);
  const [workQty, setWorkQty] = useState('');
  const [goodQty, setGoodQty] = useState('');
  const [scrapQty, setScrapQty] = useState('');
  const [appliedGoodQty, setAppliedGoodQty] = useState('');
  const [reportDate, setReportDate] = useState(todayIso());
  const [workerName, setWorkerName] = useState('');
  const [consumptionLines, setConsumptionLines] = useState<WorkReportConsumptionLine[]>([]);
  const [allSatisfied, setAllSatisfied] = useState(true);
  const [issueLineEdits, setIssueLineEdits] = useState<IssueLineEdit[]>([]);
  const [editingIssueLineId, setEditingIssueLineId] = useState<string | null>(null);
  const [editDraft, setEditDraft] = useState('');
  const [consumptionMaterialIssueEnabled, setConsumptionMaterialIssueEnabled] = useState<boolean | null>(null);
  const [loadingConsumption, setLoadingConsumption] = useState(false);
  const [consumptionError, setConsumptionError] = useState<string | null>(null);

  const effectiveMaterialIssueEnabled = consumptionMaterialIssueEnabled ?? materialIssueEnabled;

  const loadTargets = useCallback(async () => {
    setLoadingTargets(true);
    setError(null);
    try {
      setTargets(await fetchWorkReportTargets());
    } catch (e) {
      setError(e instanceof Error ? e.message : '등록 대상을 불러오지 못했습니다.');
    } finally {
      setLoadingTargets(false);
    }
  }, []);

  const loadReports = useCallback(async () => {
    setLoadingReports(true);
    setError(null);
    try {
      setReports(await fetchWorkReports(filters));
    } catch (e) {
      setError(e instanceof Error ? e.message : '작업일보 목록을 불러오지 못했습니다.');
    } finally {
      setLoadingReports(false);
    }
  }, [filters]);

  useEffect(() => {
    void loadTargets();
  }, [loadTargets]);

  useEffect(() => {
    if (tab === 'reports') {
      void loadReports();
    }
  }, [tab, loadReports]);

  const openRegister = (order: WorkOrder) => {
    setSelected(order);
    const remaining = String(order.remainingQty);
    setWorkQty(remaining);
    setGoodQty(remaining);
    setScrapQty('0');
    setAppliedGoodQty(remaining);
    setReportDate(todayIso());
    setWorkerName('');
    setConsumptionLines([]);
    setAllSatisfied(true);
    setIssueLineEdits([]);
    setConsumptionMaterialIssueEnabled(null);
    setEditingIssueLineId(null);
    setConsumptionError(null);
    setError(null);
    setModalError(null);

    void fetchWorkStandards(order.itemNo)
      .then((standards) => {
        const match = standards
          .filter((standard) => standard.processSequenceId === order.processSequenceId)
          .sort((a, b) => a.priorityOrder - b.priorityOrder)[0];
        if (match?.mainWorkerName) {
          setWorkerName(match.mainWorkerName);
        }
      })
      .catch(() => {
        /* 작업표준 조회 실패 시 작업자 빈 값 유지 */
      });
  };

  const syncScrapQty = (workValue: string, goodValue: string) => {
    const work = parseQty(workValue);
    const good = parseQty(goodValue);
    if (work === null || good === null) return;
    setScrapQty(String(Math.max(0, work - good)));
  };

  const onWorkQtyBlur = () => {
    const work = parseQty(workQty);
    if (work === null) return;
    setGoodQty(workQty);
    setScrapQty('0');
    setAppliedGoodQty(workQty);
  };

  const onGoodQtyBlur = () => {
    syncScrapQty(workQty, goodQty);
    const good = parseQty(goodQty);
    if (good !== null) {
      setAppliedGoodQty(goodQty);
    }
  };

  useEffect(() => {
    if (!selected) {
      setConsumptionLines([]);
      setAllSatisfied(true);
      setIssueLineEdits([]);
      setConsumptionMaterialIssueEnabled(null);
      setConsumptionError(null);
      return;
    }
    const pending = Number(appliedGoodQty);
    if (!Number.isFinite(pending) || pending < 0) {
      setConsumptionLines([]);
      setAllSatisfied(false);
      setIssueLineEdits([]);
      setConsumptionError('양품 수량을 확인해 주세요.');
      return;
    }

    let cancelled = false;
    setLoadingConsumption(true);
    setConsumptionError(null);

    void fetchWorkReportConsumptionStatus(selected.id, pending)
      .then(async (status) => {
        if (cancelled) return;
        setConsumptionLines(status.lines);
        setAllSatisfied(status.allSatisfied);
        setConsumptionMaterialIssueEnabled(status.materialIssueEnabled);
        if (status.materialIssueEnabled !== materialIssueEnabled) {
          setMaterialIssueEnabled(status.materialIssueEnabled);
        }

        if (status.materialIssueEnabled) {
          setIssueLineEdits([]);
          return;
        }

        const onHandList = await fetchWorkReportIssueOnHand(selected.id, reportDate);
        if (cancelled) return;
        const onHandByItemId = new Map(onHandList.map((row) => [row.itemId, row.onHandQty]));
        setIssueLineEdits((prev) => {
          const checkedMap = new Map(prev.map((line) => [line.lineKey, line.checked]));
          return status.lines.map((line) => {
            const lineKey = consumptionLineKey(line);
            return {
              lineKey,
              itemCompositionId: line.itemCompositionId,
              itemId: line.itemId,
              itemNo: line.itemNo,
              itemName: line.itemName,
              propertyClassification: line.propertyClassification,
              sourceProcessName: line.sourceProcessName ?? null,
              unitRatio: line.unitRatio,
              requiredQty: line.requiredQty,
              onHandQty: onHandByItemId.get(line.itemId) ?? 0,
              checked: checkedMap.get(lineKey) ?? true,
              issueQty: calcIssueQty(line.unitRatio, pending),
            };
          });
        });
        setEditingIssueLineId(null);
      })
      .catch((e) => {
        if (!cancelled) {
          setConsumptionLines([]);
          setAllSatisfied(false);
          setIssueLineEdits([]);
          setConsumptionMaterialIssueEnabled(null);
          setConsumptionError(e instanceof Error ? e.message : '투입 현황을 불러오지 못했습니다.');
        }
      })
      .finally(() => {
        if (!cancelled) {
          setLoadingConsumption(false);
        }
      });

    return () => {
      cancelled = true;
    };
  }, [selected, appliedGoodQty, reportDate, materialIssueEnabled, setMaterialIssueEnabled]);

  const toggleIssueLine = (lineKey: string, checked: boolean) => {
    setIssueLineEdits((lines) =>
      lines.map((line) => (line.lineKey === lineKey ? { ...line, checked } : line)),
    );
  };

  const startEditIssueQty = (line: IssueLineEdit) => {
    if (submitting || !line.checked) return;
    setEditingIssueLineId(line.lineKey);
    setEditDraft(line.issueQty);
  };

  const commitEditIssueQty = () => {
    if (editingIssueLineId === null) return;
    const parsed = Number(editDraft);
    const nextQty = Number.isFinite(parsed) && parsed >= 0 ? String(parsed) : '0';
    setIssueLineEdits((lines) =>
      lines.map((line) =>
        line.lineKey === editingIssueLineId ? { ...line, issueQty: nextQty } : line,
      ),
    );
    setEditingIssueLineId(null);
  };

  const closeModal = () => {
    setModalError(null);
    setSelected(null);
  };

  const hasStockShortage =
    !effectiveMaterialIssueEnabled &&
    !negativeStockAllowed &&
    issueLineEdits.some(
      (line) => line.checked && Number(line.issueQty) > 0 && Number(line.issueQty) > line.onHandQty,
    );

  const onRegister = async () => {
    if (!selected) return;
    syncScrapQty(workQty, goodQty);
    const good = parseQty(goodQty);
    const work = parseQty(workQty);
    const scrap = work !== null && good !== null ? Math.max(0, work - good) : null;
    setSubmitting(true);
    setError(null);
    setModalError(null);
    setMessage(null);
    try {
      if (good === null || scrap === null || good + scrap <= 0) {
        setModalError('작업수량(양품+불량)을 확인해 주세요.');
        setSubmitting(false);
        return;
      }
      if (good + scrap > selected.remainingQty) {
        setModalError(`작업수량이 잔량(${formatQty(selected.remainingQty)})을 초과합니다.`);
        setSubmitting(false);
        return;
      }
      if (effectiveMaterialIssueEnabled && !allSatisfied && consumptionLines.length > 0) {
        setModalError('자재투입이 부족합니다. [자재투입] 메뉴에서 선행 투입을 등록하세요.');
        setSubmitting(false);
        return;
      }

      if (!effectiveMaterialIssueEnabled && hasStockShortage) {
        setModalError('창고 재고가 부족합니다. 투입량을 확인하거나 입고 후 다시 시도하세요.');
        setSubmitting(false);
        return;
      }

      const issueLines = effectiveMaterialIssueEnabled
        ? undefined
        : issueLineEdits
            .filter((line) => line.checked && Number(line.issueQty) > 0)
            .map((line) => ({
              itemCompositionId: line.itemCompositionId,
              itemId: line.itemId,
              issueQty:
                goodQty !== appliedGoodQty ? Number(calcIssueQty(line.unitRatio, good)) : Number(line.issueQty),
            }))
            .filter((line) => line.issueQty > 0);

      if (!effectiveMaterialIssueEnabled && issueLines && issueLines.length === 0 && issueLineEdits.length > 0) {
        setModalError('투입할 품목을 1건 이상 선택하세요.');
        setSubmitting(false);
        return;
      }

      const report = await createWorkReport({
        workOrderId: selected.id,
        reportDate,
        goodQty: good,
        scrapQty: scrap,
        workerName: workerName.trim() || undefined,
        issueLines,
      });
      setMessage(`작업일보 ${report.reportNum}을(를) 등록했습니다. 재고가 즉시 반영됩니다.`);
      closeModal();
      await loadTargets();
      await loadReports();
      setTab('reports');
    } catch (e) {
      setModalError(e instanceof Error ? e.message : '작업일보 등록에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancel = async (id: number, reportNum: string) => {
    if (!window.confirm(`작업일보 ${reportNum}을(를) 취소하시겠습니까? 재고가 역전기됩니다.`)) return;
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      await cancelWorkReport(id);
      setMessage(`작업일보 ${reportNum}을(를) 취소했습니다.`);
      await loadTargets();
      await loadReports();
    } catch (e) {
      setError(e instanceof Error ? e.message : '작업일보 취소에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>작업일보</h1>
        <p>
          작업지시에 대한 완성품 실적을 등록합니다.
          {materialIssueEnabled
            ? ' 자재투입이 충족된 경우에만 등록할 수 있습니다.'
            : ' 등록 시 BOM 자재가 창고에서 자동 차감(백플러시)됩니다.'}
        </p>
      </header>

      <div className="tab-row">
        <button type="button" className={tab === 'targets' ? 'tab-active' : undefined} onClick={() => setTab('targets')}>
          등록 대상
        </button>
        <button type="button" className={tab === 'reports' ? 'tab-active' : undefined} onClick={() => setTab('reports')}>
          작업일보 목록
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
          </section>
          {loadingTargets ? (
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
                    <th>잔량</th>
                    <th />
                  </tr>
                </thead>
                <tbody>
                  {targets.length === 0 ? (
                    <tr>
                      <td colSpan={7}>등록 대상이 없습니다.</td>
                    </tr>
                  ) : (
                    targets.map((row) => (
                      <tr key={row.id}>
                        <td>{row.orderNum}</td>
                        <td>{row.planNo}</td>
                        <td>
                          {row.itemNo} {row.itemName}
                        </td>
                        <td>{row.processName}</td>
                        <td>{row.workCenterName ?? '—'}</td>
                        <td>{formatQty(row.remainingQty)}</td>
                        <td>
                          <button type="button" disabled={submitting} onClick={() => openRegister(row)}>
                            등록
                          </button>
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

      {tab === 'reports' && (
        <>
          <section className="filter-panel">
            <label>
              품목번호
              <input value={filters.itemNo ?? ''} onChange={(e) => setFilters((f) => ({ ...f, itemNo: e.target.value }))} />
            </label>
            <label>
              일보번호
              <input value={filters.reportNum ?? ''} onChange={(e) => setFilters((f) => ({ ...f, reportNum: e.target.value }))} />
            </label>
            <label>
              실적일(부터)
              <input
                type="date"
                value={filters.reportDateFrom ?? ''}
                onChange={(e) => setFilters((f) => ({ ...f, reportDateFrom: e.target.value }))}
              />
            </label>
            <button type="button" onClick={() => void loadReports()}>
              검색
            </button>
          </section>
          {loadingReports ? (
            <p>불러오는 중…</p>
          ) : (
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>일보번호</th>
                    <th>지시번호</th>
                    <th>품목</th>
                    <th>공정</th>
                    <th>실적일</th>
                    <th>작업</th>
                    <th>양품</th>
                    <th>불량</th>
                    <th>상태</th>
                    <th />
                  </tr>
                </thead>
                <tbody>
                  {reports.length === 0 ? (
                    <tr>
                      <td colSpan={10}>작업일보가 없습니다.</td>
                    </tr>
                  ) : (
                    reports.map((row) => (
                      <tr key={row.id}>
                        <td>{row.reportNum}</td>
                        <td>{row.orderNum}</td>
                        <td>
                          {row.itemNo} {row.itemName}
                        </td>
                        <td>{row.processName}</td>
                        <td>{row.reportDate}</td>
                        <td>{formatQty(row.goodQty + row.scrapQty)}</td>
                        <td>{formatQty(row.goodQty)}</td>
                        <td>{formatQty(row.scrapQty)}</td>
                        <td>{row.statusLabel}</td>
                        <td>
                          {row.cancellable && (
                            <button type="button" className="secondary" disabled={submitting} onClick={() => void onCancel(row.id, row.reportNum)}>
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

      {selected && (
        <div className="modal-backdrop" role="presentation" onClick={closeModal}>
          <div className="modal modal-wide" role="dialog" onClick={(e) => e.stopPropagation()}>
            <h2>작업일보 등록</h2>
            <p>
              {selected.orderNum} · {selected.itemNo} {selected.itemName} · {selected.processName}
            </p>
            <p>잔량: {formatQty(selected.remainingQty)}</p>
            {modalError && <p className="error-banner">{modalError}</p>}
            <div className="form-grid">
              <label>
                실적일
                <input type="date" value={reportDate} onChange={(e) => setReportDate(e.target.value)} disabled={submitting} />
              </label>
              <label>
                작업수량
                <input
                  type="number"
                  min={0}
                  step="any"
                  value={workQty}
                  onChange={(e) => setWorkQty(e.target.value)}
                  onBlur={onWorkQtyBlur}
                  disabled={submitting}
                />
              </label>
              <label>
                양품 수량
                <input
                  type="number"
                  min={0}
                  step="any"
                  value={goodQty}
                  onChange={(e) => setGoodQty(e.target.value)}
                  onBlur={onGoodQtyBlur}
                  disabled={submitting}
                />
              </label>
              <label>
                불량 수량
                <input type="number" min={0} step="any" value={scrapQty} readOnly disabled={submitting} />
              </label>
              <WorkerSearchField label="작업자" value={workerName} onChange={setWorkerName} disabled={submitting} />
            </div>

            <h3 className="section-subtitle">
              {effectiveMaterialIssueEnabled ? '투입 현황 (1단계 BOM · 양품 기준)' : '자재 투입 (등록 시 창고 차감)'}
            </h3>
            {loadingConsumption ? (
              <p>{effectiveMaterialIssueEnabled ? '투입 현황 조회 중…' : '투입 소요·현재고 조회 중…'}</p>
            ) : consumptionError ? (
              <p className="error-banner">{consumptionError}</p>
            ) : effectiveMaterialIssueEnabled ? (
              consumptionLines.length > 0 ? (
                <>
                  {!allSatisfied && (
                    <p className="error-banner">투입 부족 — [자재투입] 메뉴에서 선행 투입을 등록하세요.</p>
                  )}
                  <div className="table-wrap">
                    <table>
                      <thead>
                        <tr>
                          <th>품목</th>
                          <th>분류</th>
                          <th>필요(누적)</th>
                          <th>투입(누적)</th>
                          <th>잔량</th>
                          <th>상태</th>
                        </tr>
                      </thead>
                      <tbody>
                        {consumptionLines.map((line) => (
                          <tr key={consumptionLineKey(line)}>
                            <td>
                              {line.itemNo} {line.itemName}
                            </td>
                            <td>{formatConsumptionClassification(line)}</td>
                            <td>{formatQty(line.requiredQty)}</td>
                            <td>{formatQty(line.issuedQty)}</td>
                            <td>{formatQty(line.remainingQty)}</td>
                            <td>{line.satisfied ? 'OK' : '부족'}</td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </div>
                </>
              ) : (
                <p className="hint">BOM 자식이 없거나 투입 검증 대상이 없습니다.</p>
              )
            ) : issueLineEdits.length > 0 ? (
              <>
                {hasStockShortage && (
                  <p className="error-banner">창고 재고가 부족한 품목이 있습니다. 투입량 또는 입고를 확인하세요.</p>
                )}
              <div className="table-wrap">
                <table>
                  <thead>
                    <tr>
                      <th />
                      <th>품목</th>
                      <th>분류</th>
                      <th>소요</th>
                      <th>현재고</th>
                      <th>투입량</th>
                    </tr>
                  </thead>
                  <tbody>
                    {issueLineEdits.map((line) => (
                      <tr key={line.lineKey}>
                        <td>
                          <input
                            type="checkbox"
                            checked={line.checked}
                            disabled={submitting}
                            onChange={(e) => toggleIssueLine(line.lineKey, e.target.checked)}
                          />
                        </td>
                        <td>
                          {line.itemNo} {line.itemName}
                        </td>
                        <td>
                          {formatConsumptionClassification({
                            propertyClassification: line.propertyClassification,
                            sourceProcessName: line.sourceProcessName,
                          })}
                        </td>
                        <td>{formatQty(line.requiredQty)}</td>
                        <td>{formatQty(line.onHandQty)}</td>
                        <td className="issue-qty-cell">
                          {editingIssueLineId === line.lineKey ? (
                            <input
                              type="number"
                              min={0}
                              step="any"
                              className="issue-qty-input"
                              value={editDraft}
                              autoFocus
                              disabled={submitting}
                              onChange={(e) => setEditDraft(e.target.value)}
                              onBlur={commitEditIssueQty}
                              onKeyDown={(e) => {
                                if (e.key === 'Enter') e.currentTarget.blur();
                                if (e.key === 'Escape') setEditingIssueLineId(null);
                              }}
                            />
                          ) : (
                            <span
                              className={`issue-qty-label${line.checked ? '' : ' issue-qty-label-disabled'}`}
                              onClick={() => startEditIssueQty(line)}
                              onKeyDown={(e) => {
                                if (e.key === 'Enter' || e.key === ' ') startEditIssueQty(line);
                              }}
                              role="button"
                              tabIndex={line.checked ? 0 : -1}
                              title={line.checked ? '클릭하여 수정' : undefined}
                            >
                              {formatQty(Number(line.issueQty))}
                            </span>
                          )}
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
              </>
            ) : (
              <p className="hint">BOM 자식이 없거나 투입 대상이 없습니다.</p>
            )}

            <div className="modal-actions">
              <button type="button" onClick={closeModal} disabled={submitting}>
                닫기
              </button>
              <button
                type="button"
                disabled={
                  submitting ||
                  (effectiveMaterialIssueEnabled && consumptionLines.length > 0 && !allSatisfied) ||
                  hasStockShortage
                }
                onClick={() => void onRegister()}
              >
                {submitting ? '등록 중…' : '등록'}
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
