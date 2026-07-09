import { useCallback, useEffect, useRef, useState } from 'react';
import {
  cancelMaterialIssue,
  createMaterialIssue,
  fetchMaterialIssueOnHand,
  fetchMaterialIssuePreview,
  fetchMaterialIssueTargets,
  fetchMaterialIssues,
  type MaterialIssue,
  type MaterialIssueListParams,
} from '../api/materialIssue';
import type { WorkOrder } from '../api/workOrder';

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

export default function MaterialIssuePage() {
  const [targets, setTargets] = useState<WorkOrder[]>([]);
  const [issues, setIssues] = useState<MaterialIssue[]>([]);
  const [filters, setFilters] = useState<MaterialIssueListParams>({});
  const [loadingTargets, setLoadingTargets] = useState(true);
  const [loadingIssues, setLoadingIssues] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [modalError, setModalError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);
  const [tab, setTab] = useState<'targets' | 'issues'>('targets');
  const [selected, setSelected] = useState<WorkOrder | null>(null);
  const [goodQty, setGoodQty] = useState('');
  const [appliedGoodQty, setAppliedGoodQty] = useState('');
  const [issueDate, setIssueDate] = useState(todayIso());
  const [issueLineEdits, setIssueLineEdits] = useState<IssueLineEdit[]>([]);
  const [loadingPreview, setLoadingPreview] = useState(false);
  const [previewError, setPreviewError] = useState<string | null>(null);
  const [editingIssueLineId, setEditingIssueLineId] = useState<string | null>(null);
  const [editDraft, setEditDraft] = useState('');
  const issueQtyInputRef = useRef<HTMLInputElement>(null);

  const loadTargets = useCallback(async () => {
    setLoadingTargets(true);
    setError(null);
    try {
      setTargets(await fetchMaterialIssueTargets());
    } catch (e) {
      setError(e instanceof Error ? e.message : '등록 대상을 불러오지 못했습니다.');
    } finally {
      setLoadingTargets(false);
    }
  }, []);

  const loadIssues = useCallback(async () => {
    setLoadingIssues(true);
    setError(null);
    try {
      setIssues(await fetchMaterialIssues(filters));
    } catch (e) {
      setError(e instanceof Error ? e.message : '자재투입 목록을 불러오지 못했습니다.');
    } finally {
      setLoadingIssues(false);
    }
  }, [filters]);

  useEffect(() => {
    void loadTargets();
  }, [loadTargets]);

  useEffect(() => {
    if (tab === 'issues') {
      void loadIssues();
    }
  }, [tab, loadIssues]);

  useEffect(() => {
    if (editingIssueLineId !== null) {
      issueQtyInputRef.current?.focus();
      issueQtyInputRef.current?.select();
    }
  }, [editingIssueLineId]);

  const openRegister = (order: WorkOrder) => {
    setSelected(order);
    const remaining = String(order.remainingQty);
    setGoodQty(remaining);
    setAppliedGoodQty(remaining);
    setIssueDate(todayIso());
    setIssueLineEdits([]);
    setEditingIssueLineId(null);
    setPreviewError(null);
    setError(null);
    setModalError(null);
  };

  const onGoodQtyBlur = () => {
    const good = parseQty(goodQty);
    if (good !== null) {
      setAppliedGoodQty(goodQty);
    }
  };

  useEffect(() => {
    if (!selected) {
      setIssueLineEdits([]);
      setEditingIssueLineId(null);
      setPreviewError(null);
      return;
    }
    const pending = Number(appliedGoodQty);
    if (!Number.isFinite(pending) || pending < 0) {
      setIssueLineEdits([]);
      setPreviewError('양품 기준량을 확인해 주세요.');
      return;
    }

    let cancelled = false;
    setLoadingPreview(true);
    setPreviewError(null);
    void Promise.all([
      fetchMaterialIssuePreview(selected.id, pending),
      fetchMaterialIssueOnHand(selected.id, issueDate),
    ])
      .then(([preview, onHandList]) => {
        if (!cancelled) {
          const onHandByItemId = new Map(onHandList.map((row) => [row.itemId, row.onHandQty]));
          setIssueLineEdits((prev) => {
            const checkedMap = new Map(prev.map((line) => [line.lineKey, line.checked]));
            return preview.lines.map((line) => {
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
        }
      })
      .catch((e) => {
        if (!cancelled) {
          setIssueLineEdits([]);
          setPreviewError(e instanceof Error ? e.message : '투입 소요를 불러오지 못했습니다.');
        }
      })
      .finally(() => {
        if (!cancelled) {
          setLoadingPreview(false);
        }
      });

    return () => {
      cancelled = true;
    };
  }, [selected, appliedGoodQty, issueDate]);

  const closeModal = () => {
    setEditingIssueLineId(null);
    setModalError(null);
    setSelected(null);
  };

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

  const onRegister = async () => {
    if (!selected) return;
    const good = parseQty(goodQty);
    setSubmitting(true);
    setError(null);
    setModalError(null);
    setMessage(null);
    try {
      if (good === null || good <= 0) {
        setModalError('양품 기준량을 입력하세요.');
        setSubmitting(false);
        return;
      }

      const lines = issueLineEdits
        .filter((line) => line.checked && Number(line.issueQty) > 0)
        .map((line) => ({
          itemCompositionId: line.itemCompositionId,
          itemId: line.itemId,
          issueQty:
            goodQty !== appliedGoodQty ? Number(calcIssueQty(line.unitRatio, good)) : Number(line.issueQty),
        }))
        .filter((line) => line.issueQty > 0);

      if (lines.length === 0) {
        setModalError('투입할 품목을 1건 이상 선택하세요.');
        setSubmitting(false);
        return;
      }

      const issue = await createMaterialIssue({
        workOrderId: selected.id,
        issueDate,
        lines,
      });
      setMessage(`자재투입 ${issue.issueNum}을(를) 등록했습니다. 재고가 즉시 반영됩니다.`);
      closeModal();
      await loadTargets();
      await loadIssues();
      setTab('issues');
    } catch (e) {
      setModalError(e instanceof Error ? e.message : '자재투입 등록에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancel = async (id: number, issueNum: string) => {
    if (!window.confirm(`자재투입 ${issueNum}을(를) 취소하시겠습니까? 재고가 역전기됩니다.`)) return;
    setSubmitting(true);
    setError(null);
    setMessage(null);
    try {
      await cancelMaterialIssue(id);
      setMessage(`자재투입 ${issueNum}을(를) 취소했습니다.`);
      await loadTargets();
      await loadIssues();
    } catch (e) {
      setError(e instanceof Error ? e.message : '자재투입 취소에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>자재투입</h1>
        <p>작업지시에 대한 BOM 1단계 자재·공정품을 출고합니다. 투입 후 작업일보에서 실적을 등록하세요.</p>
      </header>

      <div className="tab-row">
        <button type="button" className={tab === 'targets' ? 'tab-active' : undefined} onClick={() => setTab('targets')}>
          등록 대상
        </button>
        <button type="button" className={tab === 'issues' ? 'tab-active' : undefined} onClick={() => setTab('issues')}>
          투입 목록
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
                    <th>품목</th>
                    <th>공정</th>
                    <th>잔량</th>
                    <th />
                  </tr>
                </thead>
                <tbody>
                  {targets.length === 0 ? (
                    <tr>
                      <td colSpan={5}>등록 대상이 없습니다.</td>
                    </tr>
                  ) : (
                    targets.map((row) => (
                      <tr key={row.id}>
                        <td>{row.orderNum}</td>
                        <td>
                          {row.itemNo} {row.itemName}
                        </td>
                        <td>{row.processName}</td>
                        <td>{formatQty(row.remainingQty)}</td>
                        <td className="actions">
                          <button type="button" disabled={submitting} onClick={() => openRegister(row)}>
                            투입
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

      {tab === 'issues' && (
        <>
          <section className="filter-panel">
            <label>
              품목번호
              <input value={filters.itemNo ?? ''} onChange={(e) => setFilters((f) => ({ ...f, itemNo: e.target.value }))} />
            </label>
            <label>
              투입번호
              <input value={filters.issueNum ?? ''} onChange={(e) => setFilters((f) => ({ ...f, issueNum: e.target.value }))} />
            </label>
            <button type="button" onClick={() => void loadIssues()}>
              검색
            </button>
          </section>
          {loadingIssues ? (
            <p>불러오는 중…</p>
          ) : (
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>투입번호</th>
                    <th>지시번호</th>
                    <th>품목</th>
                    <th>공정</th>
                    <th>투입일</th>
                    <th>상태</th>
                    <th />
                  </tr>
                </thead>
                <tbody>
                  {issues.length === 0 ? (
                    <tr>
                      <td colSpan={7}>자재투입 이력이 없습니다.</td>
                    </tr>
                  ) : (
                    issues.map((row) => (
                      <tr key={row.id}>
                        <td>{row.issueNum}</td>
                        <td>{row.orderNum}</td>
                        <td>
                          {row.itemNo} {row.itemName}
                        </td>
                        <td>{row.processName}</td>
                        <td>{row.issueDate}</td>
                        <td>{row.statusLabel}</td>
                        <td>
                          {row.cancellable && (
                            <button
                              type="button"
                              className="secondary"
                              disabled={submitting}
                              onClick={() => void onCancel(row.id, row.issueNum)}
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

      {selected && (
        <div className="modal-backdrop" role="presentation" onClick={closeModal}>
          <div className="modal modal-wide" role="dialog" onClick={(e) => e.stopPropagation()}>
            <h2>자재투입 등록</h2>
            <p>
              {selected.orderNum} · {selected.itemNo} {selected.itemName} · {selected.processName}
            </p>
            <p>잔량: {formatQty(selected.remainingQty)}</p>
            {modalError && <p className="error-banner">{modalError}</p>}
            <div className="form-grid">
              <label>
                투입일
                <input type="date" value={issueDate} onChange={(e) => setIssueDate(e.target.value)} disabled={submitting} />
              </label>
              <label>
                양품 기준량
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
            </div>

            <h3 className="section-subtitle">투입요소 (1단계 BOM)</h3>
            <p className="hint">양품 기준량에 따라 필요량·투입수량이 자동 계산됩니다. 투입수량은 더블클릭하여 수정할 수 있습니다.</p>
            {loadingPreview ? (
              <p>소요량 계산 중…</p>
            ) : previewError ? (
              <p className="error-banner">{previewError}</p>
            ) : issueLineEdits.length > 0 ? (
              <div className="table-wrap">
                <table>
                  <thead>
                    <tr>
                      <th />
                      <th>품목</th>
                      <th>분류</th>
                      <th>단위소요</th>
                      <th>필요량</th>
                      <th>투입수량</th>
                      <th>가용재고</th>
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
                        <td>{formatQty(line.unitRatio)}</td>
                        <td>{formatQty(line.requiredQty)}</td>
                        <td className="issue-qty-cell">
                          {editingIssueLineId === line.lineKey ? (
                            <input
                              ref={issueQtyInputRef}
                              type="number"
                              min={0}
                              step="any"
                              className="issue-qty-input"
                              value={editDraft}
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
                              onDoubleClick={() => startEditIssueQty(line)}
                              title={line.checked ? '더블클릭하여 수정' : undefined}
                            >
                              {formatQty(Number(line.issueQty))}
                            </span>
                          )}
                        </td>
                        <td>{formatQty(line.onHandQty)}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            ) : (
              <p className="hint">품목구성에 등록된 1단계 BOM 자식이 없습니다.</p>
            )}

            <div className="modal-actions">
              <button type="button" onClick={closeModal} disabled={submitting}>
                닫기
              </button>
              <button type="button" disabled={submitting} onClick={() => void onRegister()}>
                {submitting ? '등록 중…' : '등록'}
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
