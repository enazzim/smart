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
import { fetchAvailableLots, type LotRow } from '../api/lot';
import type { PropertyClassification } from '../api/item';
import CompanySearchField, { type CompanySearchSelection } from '../components/CompanySearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import {
  formatInventoryLocation,
  INVENTORY_LOCATION_LABEL,
  translateInventoryLocationInText,
} from '../utils/inventoryLocation';
import { formatQty } from '../utils/numberFormat';

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
  lotId: number | null;
  availableLots: LotRow[];
};

async function toAdvanceInputLineEdits(
  lines: OutsourcingShipmentInputPreviewLine[],
): Promise<AdvanceInputLineEdit[]> {
  return Promise.all(
    lines.map(async (line) => {
      let availableLots: LotRow[] = [];
      let lotId: number | null = null;
      if (line.lotTracked) {
        try {
          availableLots = await fetchAvailableLots(
            line.itemId,
            line.sourceLocationCode,
            line.sourceProcessId,
          );
          if (availableLots.length === 1) lotId = availableLots[0].id;
        } catch {
          availableLots = [];
        }
      }
      return {
        ...line,
        lineKey: advanceInputLineKey(line),
        issueQtyText: String(line.issueQty),
        lotId,
        availableLots,
      };
    }),
  );
}

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
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
  /** 발주출고 미리보기 라인별 Lot 선택: `${orderLineId}:${itemId}:${inputProcessId}` */
  const [previewLotIdByKey, setPreviewLotIdByKey] = useState<Record<string, number | null>>({});
  const [previewLotsByKey, setPreviewLotsByKey] = useState<Record<string, LotRow[]>>({});

  const previewLotKey = (orderLineId: number, line: OutsourcingShipmentInputPreviewLine) =>
    `${orderLineId}:${line.itemId}:${line.inputProcessId}:${line.itemCompositionId ?? 0}`;

  const loadPreviewLots = async (orderLineId: number, preview: OutsourcingShipmentInputPreview) => {
    const nextLots: Record<string, LotRow[]> = {};
    const nextIds: Record<string, number | null> = {};
    for (const line of preview.lines) {
      const key = previewLotKey(orderLineId, line);
      if (!line.lotTracked) {
        nextLots[key] = [];
        nextIds[key] = null;
        continue;
      }
      try {
        const lots = await fetchAvailableLots(
          line.itemId,
          line.sourceLocationCode,
          line.sourceProcessId,
        );
        nextLots[key] = lots;
        nextIds[key] = lots.length === 1 ? lots[0].id : null;
      } catch {
        nextLots[key] = [];
        nextIds[key] = null;
      }
    }
    setPreviewLotsByKey((prev) => ({ ...prev, ...nextLots }));
    setPreviewLotIdByKey((prev) => ({ ...prev, ...nextIds }));
  };

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

  const shipmentExportRows = useMemo(
    () =>
      shipments.map((shipment) => ({
        출고번호: shipment.shipmentNo,
        유형: shipment.shipmentTypeLabel,
        출고일: shipment.shipmentDate,
        거래처:
          shipment.partnerName ??
          shipment.lines
            .map((line) => line.partnerName)
            .filter((v, i, a) => a.indexOf(v) === i)
            .join(', '),
        '품목·공정': shipment.lines
          .map((line) => `${line.itemNo} ${line.processName} × ${formatQty(line.shipmentQty)}`)
          .join(' / '),
        출고수량: formatQty(shipment.lines.reduce((sum, line) => sum + line.shipmentQty, 0)),
        상태: shipment.statusLabel,
      })),
    [shipments],
  );

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
      setAdvanceInputEdits(await toAdvanceInputLineEdits(preview.lines));
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
    const inputLines = [];
    for (const line of advanceInputEdits) {
      const issueQty = Number(line.issueQtyText);
      if (!Number.isFinite(issueQty) || issueQty <= 0) continue;
      if (line.lotTracked && line.lotId == null) {
        setAdvanceError(`Lot 추적 품목은 Lot를 선택해야 합니다: ${line.itemNo}`);
        return;
      }
      inputLines.push({
        itemId: line.itemId,
        itemCompositionId: line.itemCompositionId ?? undefined,
        issueQty,
        sourceLocationCode: line.sourceLocationCode,
        sourceProcessId: line.sourceProcessId ?? undefined,
        inputProcessId: line.inputProcessId,
        lotId: line.lotTracked ? line.lotId : null,
      });
    }
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
      await loadPreviewLots(row.orderLineId, preview);
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
      await loadPreviewLots(orderLineId, preview);
    } catch (e) {
      setCandidateError(e instanceof Error ? e.message : '투입 미리보기 조회 실패');
    } finally {
      setLoadingPreviewId(null);
    }
  };

  const onCreateShipments = async () => {
    const baseLines = [...selectedLineIds]
      .map((orderLineId) => {
        const row = candidates.find((candidate) => candidate.orderLineId === orderLineId);
        const qty = parseQty(shipmentQtyByLineId[orderLineId] ?? (row ? String(row.remainingQty) : ''));
        return qty != null ? { orderLineId, shipmentQty: qty } : null;
      })
      .filter((line): line is { orderLineId: number; shipmentQty: number } => line != null);

    if (baseLines.length === 0) {
      setShipmentError('출고할 발주 라인과 수량을 선택하세요.');
      return;
    }

    setSubmitting(true);
    setShipmentError(null);
    setMessage(null);
    try {
      const lines = await Promise.all(
        baseLines.map(async (line) => {
          let preview = previewByLineId[line.orderLineId];
          if (!preview) {
            preview = await fetchOutsourcingShipmentInputPreview(
              line.orderLineId,
              line.shipmentQty,
              shipmentDate,
            );
            await loadPreviewLots(line.orderLineId, preview);
          }
          const inputLots = [];
          for (const input of preview.lines) {
            if (!input.lotTracked) {
              inputLots.push({ itemId: input.itemId, lotId: null as number | null });
              continue;
            }
            const key = previewLotKey(line.orderLineId, input);
            let lotId = previewLotIdByKey[key] ?? null;
            if (lotId == null) {
              const lots = await fetchAvailableLots(
                input.itemId,
                input.sourceLocationCode,
                input.sourceProcessId,
              );
              if (lots.length === 1) lotId = lots[0].id;
            }
            if (lotId == null) {
              throw new Error(
                `Lot 추적 품목은 Lot를 선택해야 합니다: ${input.itemNo} (투입 미리보기를 펼쳐 Lot를 선택하세요)`,
              );
            }
            inputLots.push({ itemId: input.itemId, lotId });
          }
          return { ...line, inputLots };
        }),
      );
      const created = await createOutsourcingShipment({ shipmentDate, lines });
      setMessage(`외주출고 ${created.shipmentNo}을(를) 등록했습니다.`);
      setSelectedLineIds(new Set());
      setExpandedLineIds(new Set());
      setPreviewByLineId({});
      setPreviewLotIdByKey({});
      setPreviewLotsByKey({});
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
                  <th>Lot</th>
                  <th className="num">투입수량</th>
                  <th className="num">현재고</th>
                </tr>
              </thead>
              <tbody>
                {advanceInputEdits.map((line) => (
                  <tr key={line.lineKey}>
                    <td>
                      {line.itemNo} {line.itemName}
                      {line.lotTracked ? ' · Lot' : ''}
                    </td>
                    <td>{line.processSequenceNum ?? '-'}</td>
                    <td>{line.inputProcessName || '-'}</td>
                    <td>{line.propertyClassification}</td>
                    <td>{formatInventoryLocation(line.sourceLocationCode)}</td>
                    <td>
                      {line.lotTracked ? (
                        <select
                          value={line.lotId ?? ''}
                          disabled={submitting}
                          onChange={(e) =>
                            setAdvanceInputEdits((rows) =>
                              rows.map((row) =>
                                row.lineKey === line.lineKey
                                  ? { ...row, lotId: e.target.value ? Number(e.target.value) : null }
                                  : row,
                              ),
                            )
                          }
                        >
                          <option value="">선택</option>
                          {line.availableLots.map((lot) => (
                            <option key={lot.id} value={lot.id}>
                              {lot.lotNo}
                            </option>
                          ))}
                        </select>
                      ) : (
                        '—'
                      )}
                    </td>
                    <td className="num issue-qty-cell">
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
                    <td className="num">{formatQty(line.onHandQty)}</td>
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
                  <th className="num">발주수량</th>
                  <th className="num">출고수량</th>
                  <th className="num">잔량</th>
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
                        <td className="num">{formatQty(row.orderQty)}</td>
                        <td className="num">{formatQty(row.shippedQty)}</td>
                        <td className="num">{formatQty(row.remainingQty)}</td>
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
                                    <th>Lot</th>
                                    <th className="num">투입수량</th>
                                    <th className="num">현재고</th>
                                  </tr>
                                </thead>
                                <tbody>
                                  {preview.lines.map((line) => {
                                    const lotKey = previewLotKey(row.orderLineId, line);
                                    return (
                                    <tr key={`${line.itemId}-${line.inputProcessId}`}>
                                      <td>
                                        {line.itemNo} {line.itemName}
                                        {line.lotTracked ? ' · Lot' : ''}
                                      </td>
                                      <td>{line.processSequenceNum ?? '-'}</td>
                                      <td>{line.inputProcessName || '-'}</td>
                                      <td>{line.propertyClassification}</td>
                                      <td>{formatInventoryLocation(line.sourceLocationCode)}</td>
                                      <td>
                                        {line.lotTracked ? (
                                          <select
                                            value={previewLotIdByKey[lotKey] ?? ''}
                                            disabled={submitting}
                                            onChange={(e) =>
                                              setPreviewLotIdByKey((prev) => ({
                                                ...prev,
                                                [lotKey]: e.target.value ? Number(e.target.value) : null,
                                              }))
                                            }
                                          >
                                            <option value="">선택</option>
                                            {(previewLotsByKey[lotKey] ?? []).map((lot) => (
                                              <option key={lot.id} value={lot.id}>
                                                {lot.lotNo}
                                              </option>
                                            ))}
                                          </select>
                                        ) : (
                                          '—'
                                        )}
                                      </td>
                                      <td className="num">{formatQty(line.issueQty)}</td>
                                      <td className="num">{formatQty(line.onHandQty)}</td>
                                    </tr>
                                    );
                                  })}
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
        <div className="panel-header-row">
          <h2>외주출고 목록</h2>
          <GridExcelExportButton
            fileBaseName="외주출고목록"
            disabled={loadingShipments}
            rows={shipmentExportRows}
          />
        </div>
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
                  <th className="num">출고수량</th>
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
                    <td className="num">{formatQty(shipment.lines.reduce((sum, line) => sum + line.shipmentQty, 0))}</td>
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
