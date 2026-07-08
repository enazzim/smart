import { Fragment, useCallback, useEffect, useMemo, useRef, useState } from 'react';
import {
  cancelOutsourcingShipment,
  createOutsourcingAdvanceShipment,
  createOutsourcingShipment,
  fetchOutsourceAdvanceProcessOptions,
  fetchOutsourcingAdvanceInputPreview,
  fetchOutsourcingShipmentCandidates,
  fetchOutsourcingShipmentInputPreview,
  fetchOutsourcingShipments,
  type OutsourceAdvanceProcessOption,
  type OutsourcingShipment,
  type OutsourcingShipmentCandidate,
  type OutsourcingShipmentInputPreview,
  type OutsourcingShipmentInputPreviewLine,
  type OutsourcingShipmentListParams,
} from '../api/outsourcingShipment';
import type { PropertyClassification } from '../api/item';
import CompanySearchField, { type CompanySearchSelection } from '../components/CompanySearchField';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import {
  formatInventoryLocation,
  INVENTORY_LOCATION_LABEL,
  translateInventoryLocationInText,
} from '../utils/inventoryLocation';

const ADVANCE_PARENT_CLASSES: PropertyClassification[] = ['제품', '공정품'];

function processOptionKey(option: OutsourceAdvanceProcessOption): string {
  return `${option.beginProcessCodeId}:${option.endProcessCodeId}`;
}

function advanceInputLineKey(line: OutsourcingShipmentInputPreviewLine): string {
  return `${line.itemId}-${line.inputProcessId}-${line.itemCompositionId ?? 0}`;
}

type AdvanceInputLineEdit = OutsourcingShipmentInputPreviewLine & {
  lineKey: string;
  issueQtyText: string;
};

function toAdvanceInputLineEdits(lines: OutsourcingShipmentInputPreviewLine[]): AdvanceInputLineEdit[] {
  return lines.map((line) => ({
    ...line,
    lineKey: advanceInputLineKey(line),
    issueQtyText: String(line.issueQty),
  }));
}

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

function formatQty(value: number): string {
  return Number.isInteger(value) ? String(value) : value.toLocaleString(undefined, { maximumFractionDigits: 4 });
}

function parseQty(value: string): number | null {
  const parsed = Number(value);
  return Number.isFinite(parsed) && parsed > 0 ? parsed : null;
}

export default function OutsourcingShipmentPage() {
  const [candidates, setCandidates] = useState<OutsourcingShipmentCandidate[]>([]);
  const [shipments, setShipments] = useState<OutsourcingShipment[]>([]);
  const [shipmentDate, setShipmentDate] = useState(todayIso());
  const [listFilters, setListFilters] = useState<OutsourcingShipmentListParams>(() => ({
    shipmentDateFrom: addDaysIso(todayIso(), -7),
    shipmentDateTo: todayIso(),
    excludeCancelled: true,
  }));
  const [selectedLineIds, setSelectedLineIds] = useState<Set<number>>(new Set());
  const [shipmentQtyByLineId, setShipmentQtyByLineId] = useState<Record<number, string>>({});
  const [expandedLineIds, setExpandedLineIds] = useState<Set<number>>(new Set());
  const [previewByLineId, setPreviewByLineId] = useState<Record<number, OutsourcingShipmentInputPreview>>({});
  const [loadingCandidates, setLoadingCandidates] = useState(true);
  const [loadingShipments, setLoadingShipments] = useState(true);
  const [loadingPreviewId, setLoadingPreviewId] = useState<number | null>(null);
  const [submitting, setSubmitting] = useState(false);
  const [candidateError, setCandidateError] = useState<string | null>(null);
  const [shipmentError, setShipmentError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);
  const [advancePartner, setAdvancePartner] = useState<CompanySearchSelection | null>(null);
  const [advanceParentItem, setAdvanceParentItem] = useState<ItemSearchSelection | null>(null);
  const [advanceProcessOptions, setAdvanceProcessOptions] = useState<OutsourceAdvanceProcessOption[]>([]);
  const [advanceProcessKey, setAdvanceProcessKey] = useState('');
  const [advanceReferenceQty, setAdvanceReferenceQty] = useState('1');
  const [advanceInputEdits, setAdvanceInputEdits] = useState<AdvanceInputLineEdit[]>([]);
  const [editingAdvanceLineKey, setEditingAdvanceLineKey] = useState<string | null>(null);
  const [editAdvanceQtyDraft, setEditAdvanceQtyDraft] = useState('');
  const advanceQtyInputRef = useRef<HTMLInputElement | null>(null);
  const [advanceError, setAdvanceError] = useState<string | null>(null);
  const [loadingAdvanceOptions, setLoadingAdvanceOptions] = useState(false);
  const [loadingAdvancePreview, setLoadingAdvancePreview] = useState(false);

  const loadCandidates = useCallback(async () => {
    setLoadingCandidates(true);
    setCandidateError(null);
    try {
      const rows = await fetchOutsourcingShipmentCandidates();
      setCandidates(rows);
      setShipmentQtyByLineId((prev) => {
        const next = { ...prev };
        for (const row of rows) {
          if (next[row.orderLineId] === undefined) {
            next[row.orderLineId] = String(row.remainingQty);
          }
        }
        return next;
      });
    } catch (e) {
      setCandidateError(e instanceof Error ? e.message : '출고 후보 조회 실패');
      setCandidates([]);
    } finally {
      setLoadingCandidates(false);
    }
  }, []);

  const loadShipments = useCallback(async () => {
    setLoadingShipments(true);
    setShipmentError(null);
    try {
      setShipments(await fetchOutsourcingShipments(listFilters));
    } catch (e) {
      setShipmentError(e instanceof Error ? e.message : '외주출고 목록 조회 실패');
      setShipments([]);
    } finally {
      setLoadingShipments(false);
    }
  }, [listFilters]);

  useEffect(() => {
    void loadCandidates();
  }, [loadCandidates]);

  useEffect(() => {
    void loadShipments();
  }, [loadShipments]);

  useEffect(() => {
    if (!advancePartner || !advanceParentItem) {
      setAdvanceProcessOptions([]);
      setAdvanceProcessKey('');
      setAdvanceInputEdits([]);
      setEditingAdvanceLineKey(null);
      return;
    }
    let cancelled = false;
    setLoadingAdvanceOptions(true);
    setAdvanceError(null);
    void fetchOutsourceAdvanceProcessOptions(advancePartner.id, advanceParentItem.id, shipmentDate)
      .then((options) => {
        if (cancelled) return;
        setAdvanceProcessOptions(options);
        setAdvanceProcessKey(options.length > 0 ? processOptionKey(options[0]) : '');
        setAdvanceInputEdits([]);
        setEditingAdvanceLineKey(null);
      })
      .catch((e) => {
        if (cancelled) return;
        setAdvanceProcessOptions([]);
        setAdvanceProcessKey('');
        setAdvanceInputEdits([]);
        setEditingAdvanceLineKey(null);
        setAdvanceError(e instanceof Error ? e.message : '공정구간 조회 실패');
      })
      .finally(() => {
        if (!cancelled) setLoadingAdvanceOptions(false);
      });
    return () => {
      cancelled = true;
    };
  }, [advancePartner, advanceParentItem, shipmentDate]);

  const selectedAdvanceProcess = useMemo(
    () => advanceProcessOptions.find((option) => processOptionKey(option) === advanceProcessKey) ?? null,
    [advanceProcessOptions, advanceProcessKey],
  );

  useEffect(() => {
    if (editingAdvanceLineKey !== null) {
      advanceQtyInputRef.current?.focus();
      advanceQtyInputRef.current?.select();
    }
  }, [editingAdvanceLineKey]);

  const loadAdvancePreview = async () => {
    if (!advancePartner || !advanceParentItem || !selectedAdvanceProcess) {
      setAdvanceError('거래처·품목·공정구간을 선택하세요.');
      return;
    }
    const referenceQty = parseQty(advanceReferenceQty);
    if (referenceQty == null) {
      setAdvanceError('기준수량은 0보다 커야 합니다.');
      return;
    }
    setLoadingAdvancePreview(true);
    setAdvanceError(null);
    try {
      const preview = await fetchOutsourcingAdvanceInputPreview(
        advancePartner.id,
        advanceParentItem.id,
        selectedAdvanceProcess.beginProcessCodeId,
        selectedAdvanceProcess.endProcessCodeId,
        referenceQty,
        shipmentDate,
      );
      setAdvanceInputEdits(toAdvanceInputLineEdits(preview.lines));
      setEditingAdvanceLineKey(null);
    } catch (e) {
      setAdvanceInputEdits([]);
      setEditingAdvanceLineKey(null);
      setAdvanceError(e instanceof Error ? e.message : '선출고 투입 미리보기 조회 실패');
    } finally {
      setLoadingAdvancePreview(false);
    }
  };

  const startEditAdvanceIssueQty = (line: AdvanceInputLineEdit) => {
    if (submitting) return;
    setEditingAdvanceLineKey(line.lineKey);
    setEditAdvanceQtyDraft(line.issueQtyText);
  };

  const commitEditAdvanceIssueQty = () => {
    if (editingAdvanceLineKey === null) return;
    const parsed = Number(editAdvanceQtyDraft);
    const nextQty = Number.isFinite(parsed) && parsed > 0 ? String(parsed) : '';
    setAdvanceInputEdits((lines) =>
      lines.map((line) =>
        line.lineKey === editingAdvanceLineKey ? { ...line, issueQtyText: nextQty || line.issueQtyText } : line,
      ),
    );
    setEditingAdvanceLineKey(null);
  };

  const onCreateAdvanceShipment = async () => {
    if (!advancePartner || !advanceParentItem || !selectedAdvanceProcess) {
      setAdvanceError('거래처·품목·공정구간을 선택하세요.');
      return;
    }
    const referenceQty = parseQty(advanceReferenceQty);
    if (referenceQty == null) {
      setAdvanceError('기준수량은 0보다 커야 합니다.');
      return;
    }
    if (advanceInputEdits.length === 0) {
      setAdvanceError('투입 미리보기를 먼저 실행하세요.');
      return;
    }
    const inputLines = advanceInputEdits
      .map((line) => {
        const issueQty = Number(line.issueQtyText);
        if (!Number.isFinite(issueQty) || issueQty <= 0) return null;
        return {
          itemId: line.itemId,
          itemCompositionId: line.itemCompositionId ?? undefined,
          issueQty,
          sourceLocationCode: line.sourceLocationCode,
          sourceProcessId: line.sourceProcessId ?? undefined,
          inputProcessId: line.inputProcessId,
        };
      })
      .filter((line): line is NonNullable<typeof line> => line != null);
    if (inputLines.length === 0) {
      setAdvanceError('투입수량은 0보다 커야 합니다.');
      return;
    }
    setSubmitting(true);
    setAdvanceError(null);
    setShipmentError(null);
    setMessage(null);
    try {
      const created = await createOutsourcingAdvanceShipment({
        shipmentDate,
        partnerId: advancePartner.id,
        lines: [
          {
            parentItemId: advanceParentItem.id,
            beginProcessCodeId: selectedAdvanceProcess.beginProcessCodeId,
            endProcessCodeId: selectedAdvanceProcess.endProcessCodeId,
            referenceQty,
            inputLines,
          },
        ],
      });
      setMessage(`선출고 ${created.shipmentNo}을(를) 등록했습니다. (발주 잔량과 무관)`);
      setAdvanceInputEdits([]);
      setEditingAdvanceLineKey(null);
      await loadShipments();
    } catch (e) {
      setAdvanceError(e instanceof Error ? e.message : '선출고 등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const selectableLineIds = useMemo(
    () => new Set(candidates.filter((row) => row.shippable).map((row) => row.orderLineId)),
    [candidates],
  );

  const allSelected =
    selectableLineIds.size > 0 && [...selectableLineIds].every((id) => selectedLineIds.has(id));

  const toggleSelectAll = (checked: boolean) => {
    if (checked) {
      setSelectedLineIds(new Set(selectableLineIds));
    } else {
      setSelectedLineIds(new Set());
    }
  };

  const toggleLine = (orderLineId: number, checked: boolean) => {
    setSelectedLineIds((prev) => {
      const next = new Set(prev);
      if (checked) next.add(orderLineId);
      else next.delete(orderLineId);
      return next;
    });
  };

  const toggleExpand = async (row: OutsourcingShipmentCandidate) => {
    const nextExpanded = !expandedLineIds.has(row.orderLineId);
    setExpandedLineIds((prev) => {
      const next = new Set(prev);
      if (nextExpanded) next.add(row.orderLineId);
      else next.delete(row.orderLineId);
      return next;
    });
    if (!nextExpanded || previewByLineId[row.orderLineId]) {
      return;
    }
    const qty = parseQty(shipmentQtyByLineId[row.orderLineId] ?? String(row.remainingQty));
    if (qty == null) {
      return;
    }
    setLoadingPreviewId(row.orderLineId);
    try {
      const preview = await fetchOutsourcingShipmentInputPreview(row.orderLineId, qty, shipmentDate);
      setPreviewByLineId((prev) => ({ ...prev, [row.orderLineId]: preview }));
    } catch (e) {
      setCandidateError(e instanceof Error ? e.message : '투입 미리보기 조회 실패');
    } finally {
      setLoadingPreviewId(null);
    }
  };

  const refreshPreview = async (orderLineId: number) => {
    const row = candidates.find((candidate) => candidate.orderLineId === orderLineId);
    if (!row) return;
    const qty = parseQty(shipmentQtyByLineId[orderLineId] ?? String(row.remainingQty));
    if (qty == null) return;
    setLoadingPreviewId(orderLineId);
    try {
      const preview = await fetchOutsourcingShipmentInputPreview(orderLineId, qty, shipmentDate);
      setPreviewByLineId((prev) => ({ ...prev, [orderLineId]: preview }));
    } catch (e) {
      setCandidateError(e instanceof Error ? e.message : '투입 미리보기 조회 실패');
    } finally {
      setLoadingPreviewId(null);
    }
  };

  const onCreateShipments = async () => {
    const lines = [...selectedLineIds].map((orderLineId) => {
      const row = candidates.find((candidate) => candidate.orderLineId === orderLineId);
      const qty = parseQty(shipmentQtyByLineId[orderLineId] ?? (row ? String(row.remainingQty) : ''));
      return qty != null ? { orderLineId, shipmentQty: qty } : null;
    }).filter((line): line is { orderLineId: number; shipmentQty: number } => line != null);

    if (lines.length === 0) {
      setShipmentError('출고할 발주 라인과 수량을 선택하세요.');
      return;
    }

    setSubmitting(true);
    setShipmentError(null);
    setMessage(null);
    try {
      const created = await createOutsourcingShipment({ shipmentDate, lines });
      setMessage(`외주출고 ${created.shipmentNo}을(를) 등록했습니다.`);
      setSelectedLineIds(new Set());
      setExpandedLineIds(new Set());
      setPreviewByLineId({});
      await loadCandidates();
      await loadShipments();
    } catch (e) {
      setShipmentError(e instanceof Error ? e.message : '외주출고 등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancel = async (shipment: OutsourcingShipment) => {
    if (!window.confirm(`외주출고 ${shipment.shipmentNo}을(를) 취소하시겠습니까?`)) return;
    setSubmitting(true);
    setShipmentError(null);
    try {
      await cancelOutsourcingShipment(shipment.id);
      setMessage(`외주출고 ${shipment.shipmentNo}을(를) 취소했습니다.`);
      await loadCandidates();
      await loadShipments();
    } catch (e) {
      setShipmentError(e instanceof Error ? e.message : '외주출고 취소 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const selectedCount = selectedLineIds.size;

  return (
    <div className="page">
      <header className="page-header">
        <h1>외주출고</h1>
        <p>외주발주 잔량을 기준으로 투입 자재·반제품을 출고하고 {INVENTORY_LOCATION_LABEL.OUTSOURCE}로 이동합니다.</p>
      </header>

      {message && <p className="success-banner">{message}</p>}
      {shipmentError && <p className="error-banner">{shipmentError}</p>}

      <section className="panel">
        <h2>선출고 (발주 무관)</h2>
        <p className="hint">
          외주발주가 있어도 별도로 투입 자재·반제품을 {INVENTORY_LOCATION_LABEL.OUTSOURCE}로 선출고합니다. 발주
          출고수량(shipped_qty)에는 반영되지 않습니다.
        </p>
        {advanceError && <p className="error-banner">{advanceError}</p>}
        <div className="action-bar">
          <label>
            출고일
            <input type="date" value={shipmentDate} onChange={(e) => setShipmentDate(e.target.value)} disabled={submitting} />
          </label>
        </div>
        <div className="form-grid-wide">
          <CompanySearchField
            label="외주 거래처"
            partnerType="OUTSOURCE"
            selectedCompany={advancePartner}
            onSelect={setAdvancePartner}
          />
          <ItemSearchField
            label="공정품(모품목)"
            allowedClassifications={ADVANCE_PARENT_CLASSES}
            selectedItem={advanceParentItem}
            onSelect={setAdvanceParentItem}
          />
          <label>
            공정구간
            <select
              value={advanceProcessKey}
              disabled={loadingAdvanceOptions || advanceProcessOptions.length === 0}
              onChange={(e) => {
                setAdvanceProcessKey(e.target.value);
                setAdvanceInputEdits([]);
                setEditingAdvanceLineKey(null);
              }}
            >
              {advanceProcessOptions.length === 0 ? (
                <option value="">거래처·품목 선택 후 조회</option>
              ) : (
                advanceProcessOptions.map((option) => {
                  const key = processOptionKey(option);
                  return (
                    <option key={key} value={key}>
                      {option.beginProcessName} ~ {option.endProcessName}
                    </option>
                  );
                })
              )}
            </select>
          </label>
          <label>
            기준수량
            <input
              type="number"
              min={0}
              step="any"
              value={advanceReferenceQty}
              onChange={(e) => {
                setAdvanceReferenceQty(e.target.value);
                setAdvanceInputEdits([]);
                setEditingAdvanceLineKey(null);
              }}
            />
          </label>
        </div>
        <div className="action-bar">
          <button
            type="button"
            className="secondary"
            disabled={loadingAdvancePreview || submitting}
            onClick={() => void loadAdvancePreview()}
          >
            {loadingAdvancePreview ? '미리보기…' : '투입 미리보기'}
          </button>
          <button type="button" disabled={submitting} onClick={() => void onCreateAdvanceShipment()}>
            {submitting ? '등록 중…' : '선출고 등록'}
          </button>
        </div>
        {advanceInputEdits.length > 0 && (
          <div className="table-wrap">
            <p className="hint">투입수량은 더블클릭하여 수정할 수 있습니다.</p>
            <table className="nested-table">
              <thead>
                <tr>
                  <th>투입품목</th>
                  <th>공정순서</th>
                  <th>공정</th>
                  <th>분류</th>
                  <th>창고</th>
                  <th>투입수량</th>
                  <th>현재고</th>
                </tr>
              </thead>
              <tbody>
                {advanceInputEdits.map((line) => (
                  <tr key={line.lineKey}>
                    <td>
                      {line.itemNo} {line.itemName}
                    </td>
                    <td>{line.processSequenceNum ?? '-'}</td>
                    <td>{line.inputProcessName || '-'}</td>
                    <td>{line.propertyClassification}</td>
                    <td>{formatInventoryLocation(line.sourceLocationCode)}</td>
                    <td className="issue-qty-cell">
                      {editingAdvanceLineKey === line.lineKey ? (
                        <input
                          ref={advanceQtyInputRef}
                          type="number"
                          min={0.0001}
                          step="any"
                          className="issue-qty-input"
                          value={editAdvanceQtyDraft}
                          disabled={submitting}
                          onChange={(e) => setEditAdvanceQtyDraft(e.target.value)}
                          onBlur={commitEditAdvanceIssueQty}
                          onKeyDown={(e) => {
                            if (e.key === 'Enter') e.currentTarget.blur();
                            if (e.key === 'Escape') setEditingAdvanceLineKey(null);
                          }}
                        />
                      ) : (
                        <span
                          className="issue-qty-label"
                          onDoubleClick={() => startEditAdvanceIssueQty(line)}
                          title="더블클릭하여 수정"
                        >
                          {formatQty(Number(line.issueQtyText))}
                        </span>
                      )}
                    </td>
                    <td>{formatQty(line.onHandQty)}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </section>

      <section className="panel">
        <h2>발주 출고 후보</h2>
        <div className="action-bar">
          <label>
            출고일
            <input type="date" value={shipmentDate} onChange={(e) => setShipmentDate(e.target.value)} disabled={submitting} />
          </label>
          <button type="button" className="secondary" onClick={() => void loadCandidates()} disabled={loadingCandidates}>
            새로고침
          </button>
          <button type="button" disabled={submitting || selectedCount === 0} onClick={() => void onCreateShipments()}>
            {submitting ? '등록 중…' : `선택 출고 (${selectedCount})`}
          </button>
        </div>
        {loadingCandidates ? (
          <p>불러오는 중…</p>
        ) : candidateError ? (
          <p className="error-banner">{candidateError}</p>
        ) : candidates.length === 0 ? (
          <p className="hint">출고 가능한 외주발주가 없습니다.</p>
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
                      disabled={selectableLineIds.size === 0 || submitting}
                      onChange={(e) => toggleSelectAll(e.target.checked)}
                    />
                  </th>
                  <th />
                  <th>발주번호</th>
                  <th>거래처</th>
                  <th>품목</th>
                  <th>공정구간</th>
                  <th>발주수량</th>
                  <th>출고수량</th>
                  <th>잔량</th>
                  <th>출고수량 입력</th>
                  <th>상태</th>
                </tr>
              </thead>
              <tbody>
                {candidates.map((row) => {
                  const expanded = expandedLineIds.has(row.orderLineId);
                  const preview = previewByLineId[row.orderLineId];
                  return (
                    <Fragment key={row.orderLineId}>
                      <tr className={!row.shippable ? 'row-muted' : undefined}>
                        <td>
                          <input
                            type="checkbox"
                            aria-label={`${row.itemNo} 선택`}
                            checked={selectedLineIds.has(row.orderLineId)}
                            disabled={!row.shippable || submitting}
                            onChange={(e) => toggleLine(row.orderLineId, e.target.checked)}
                          />
                        </td>
                        <td>
                          <button
                            type="button"
                            className="btn-action"
                            disabled={!row.shippable}
                            onClick={() => void toggleExpand(row)}
                          >
                            {expanded ? '접기' : '펼치기'}
                          </button>
                        </td>
                        <td>{row.orderNo}</td>
                        <td>{row.partnerName}</td>
                        <td>
                          {row.itemNo} {row.itemName}
                        </td>
                        <td>
                          {row.beginProcessName} ~ {row.endProcessName}
                        </td>
                        <td>{formatQty(row.orderQty)}</td>
                        <td>{formatQty(row.shippedQty)}</td>
                        <td>{formatQty(row.remainingQty)}</td>
                        <td>
                          <input
                            type="number"
                            min={0}
                            step="any"
                            disabled={!row.shippable || submitting}
                            value={shipmentQtyByLineId[row.orderLineId] ?? String(row.remainingQty)}
                            onChange={(e) =>
                              setShipmentQtyByLineId((prev) => ({ ...prev, [row.orderLineId]: e.target.value }))
                            }
                            onBlur={() => {
                              if (expanded) void refreshPreview(row.orderLineId);
                            }}
                          />
                        </td>
                        <td>{translateInventoryLocationInText(row.shippableMessage) || (row.shippable ? '출고가능' : '불가')}</td>
                      </tr>
                      {expanded && (
                        <tr>
                          <td colSpan={11}>
                            {loadingPreviewId === row.orderLineId ? (
                              <p>투입 미리보기 불러오는 중…</p>
                            ) : preview ? (
                              <table className="nested-table">
                                <thead>
                                  <tr>
                                    <th>투입품목</th>
                                    <th>공정순서</th>
                                    <th>공정</th>
                                    <th>분류</th>
                                    <th>창고</th>
                                    <th>투입수량</th>
                                    <th>현재고</th>
                                  </tr>
                                </thead>
                                <tbody>
                                  {preview.lines.map((line) => (
                                    <tr key={`${line.itemId}-${line.inputProcessId}`}>
                                      <td>
                                        {line.itemNo} {line.itemName}
                                      </td>
                                      <td>{line.processSequenceNum ?? '-'}</td>
                                      <td>{line.inputProcessName || '-'}</td>
                                      <td>{line.propertyClassification}</td>
                                      <td>{formatInventoryLocation(line.sourceLocationCode)}</td>
                                      <td>{formatQty(line.issueQty)}</td>
                                      <td>{formatQty(line.onHandQty)}</td>
                                    </tr>
                                  ))}
                                </tbody>
                              </table>
                            ) : (
                              <p className="hint">출고수량을 입력한 뒤 펼치기를 다시 눌러 주세요.</p>
                            )}
                          </td>
                        </tr>
                      )}
                    </Fragment>
                  );
                })}
              </tbody>
            </table>
          </div>
        )}
      </section>

      <section className="panel">
        <h2>외주출고 목록</h2>
        <div className="filter-row">
          <label>
            출고일 From
            <input
              type="date"
              value={listFilters.shipmentDateFrom ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, shipmentDateFrom: e.target.value }))}
            />
          </label>
          <label>
            To
            <input
              type="date"
              value={listFilters.shipmentDateTo ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, shipmentDateTo: e.target.value }))}
            />
          </label>
          <label>
            거래처
            <input
              type="text"
              value={listFilters.partnerName ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, partnerName: e.target.value }))}
            />
          </label>
          <label>
            출고번호
            <input
              type="text"
              value={listFilters.shipmentNo ?? ''}
              onChange={(e) => setListFilters((f) => ({ ...f, shipmentNo: e.target.value }))}
            />
          </label>
          <button type="button" className="secondary" onClick={() => void loadShipments()}>
            조회
          </button>
        </div>
        {loadingShipments ? (
          <p>불러오는 중…</p>
        ) : shipments.length === 0 ? (
          <p className="hint">외주출고가 없습니다.</p>
        ) : (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>출고번호</th>
                  <th>유형</th>
                  <th>출고일</th>
                  <th>거래처</th>
                  <th>품목·공정</th>
                  <th>출고수량</th>
                  <th>상태</th>
                  <th />
                </tr>
              </thead>
              <tbody>
                {shipments.map((shipment) => (
                  <tr key={shipment.id}>
                    <td>{shipment.shipmentNo}</td>
                    <td>{shipment.shipmentTypeLabel}</td>
                    <td>{shipment.shipmentDate}</td>
                    <td>
                      {shipment.partnerName ??
                        shipment.lines
                          .map((line) => line.partnerName)
                          .filter((v, i, a) => a.indexOf(v) === i)
                          .join(', ')}
                    </td>
                    <td>
                      {shipment.lines.map((line) => (
                        <div key={line.id}>
                          {line.itemNo} {line.processName} × {formatQty(line.shipmentQty)}
                        </div>
                      ))}
                    </td>
                    <td>{formatQty(shipment.lines.reduce((sum, line) => sum + line.shipmentQty, 0))}</td>
                    <td>{shipment.statusLabel}</td>
                    <td>
                      {shipment.cancelable && (
                        <button type="button" className="secondary" disabled={submitting} onClick={() => void onCancel(shipment)}>
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
    </div>
  );
}
