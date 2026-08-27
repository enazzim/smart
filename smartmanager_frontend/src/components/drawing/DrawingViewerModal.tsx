import { useEffect, useId, useMemo, useRef, useState } from 'react';
import { Archive, ArrowRightCircle, Eye, Link2, ListPlus, PanelLeftClose, PanelLeftOpen, Plus, RefreshCw, RotateCcw, Trash2, X } from 'lucide-react';
import { useQueryClient } from '@tanstack/react-query';
import {
  drawingPdfUrl,
  fetchDrawingHistories,
  fetchDrawingReferenceCandidates,
  fetchDrawingReferences,
  fetchDrawingWhereUsed,
  promoteDrawing,
  reopenDrawingDev,
  replaceDrawingReferences,
  updateDrawingLifecycle,
  type DrawingHistoryItem,
  type DrawingListItem,
  type DrawingReferenceCandidate,
  type DrawingReferenceItem,
  type DrawingType,
  type DrawingWhereUsedItem,
} from '../../api/drawing';
import {
  archiveActionLabel,
  canArchiveDrawing,
  canEditLifecycleManually,
  canLinkItem,
  canPromoteDrawing,
  canReopenDev,
  canUnarchiveDrawing,
  DEV_ACTIVE_LIFECYCLE_STAGES,
  isArchivedLifecycle,
  lifecycleBadgeClass,
  lifecycleStageLabel,
  restoreStageAfterUnarchive,
  type DrawingLifecycleStage,
} from '../../utils/drawingLifecycle';
import { useDrawingWebSocket } from '../../hooks/useDrawingWebSocket';
import PdfViewer from './PdfViewer';
import DrawingLinkItemModal from './DrawingLinkItemModal';
import { useConfirm } from '../../context/ConfirmContext';

type ViewerPanel = 'pdf' | 'contains' | 'where-used';

interface PeerPdfView {
  partNo: string;
  historyId: string;
  title: string;
}

function formatCandidateLabel(row: DrawingReferenceCandidate): string {
  return `L${row.bomLevel} ${row.partNo} / ${row.partName} (${row.drawingType} V${row.majorVersion}.${row.minorVersion}${
    row.itemNo ? ` · ${row.itemNo}` : ''
  })`;
}

function matchesCandidateQuery(row: DrawingReferenceCandidate, query: string): boolean {
  const q = query.trim().toLowerCase();
  if (!q) {
    return true;
  }
  const haystack = [
    row.partNo,
    row.partName,
    row.itemNo ?? '',
    row.drawingType,
    `v${row.majorVersion}.${row.minorVersion}`,
    `l${row.bomLevel}`,
  ]
    .join(' ')
    .toLowerCase();
  return haystack.includes(q);
}

interface DrawingViewerModalProps {
  open: boolean;
  onClose: () => void;
  masterId: string;
  partNo: string;
  drawingType: DrawingType;
  lifecycleStage?: DrawingLifecycleStage | null;
  sourcePartnerName?: string | null;
  itemId?: number | null;
  itemNo?: string | null;
  itemLinkedAt?: string | null;
  historyHighlightQuery?: string;
  isDeleted?: boolean;
  readOnly?: boolean;
  canManage?: boolean;
  onSuccess: (message: string) => void;
  onError: (message: string) => void;
  /** 목록·상세 props 동기화 (lifecycle·품목 연결 등) */
  onDrawingMetaChange?: (patch: Partial<DrawingListItem>) => void;
}

export default function DrawingViewerModal({
  open,
  onClose,
  masterId,
  partNo,
  drawingType,
  lifecycleStage,
  sourcePartnerName,
  itemId,
  itemNo,
  itemLinkedAt,
  historyHighlightQuery,
  isDeleted,
  readOnly,
  canManage,
  onSuccess,
  onError,
  onDrawingMetaChange,
}: DrawingViewerModalProps) {
  const confirm = useConfirm();
  const queryClient = useQueryClient();
  const [histories, setHistories] = useState<DrawingHistoryItem[]>([]);
  const [selectedHistory, setSelectedHistory] = useState<DrawingHistoryItem | null>(null);
  const [panel, setPanel] = useState<ViewerPanel>('pdf');
  const [historySidebarCollapsed, setHistorySidebarCollapsed] = useState(true);
  const [references, setReferences] = useState<DrawingReferenceItem[]>([]);
  const [whereUsed, setWhereUsed] = useState<DrawingWhereUsedItem[]>([]);
  const [candidates, setCandidates] = useState<DrawingReferenceCandidate[]>([]);
  const [candidatesHint, setCandidatesHint] = useState<string | null>(null);
  const [addHistoryId, setAddHistoryId] = useState('');
  const [addQuery, setAddQuery] = useState('');
  const [addListOpen, setAddListOpen] = useState(false);
  const [bulkPreviewOpen, setBulkPreviewOpen] = useState(false);
  const [bulkSelectedIds, setBulkSelectedIds] = useState<Set<string>>(new Set());
  const [savingRefs, setSavingRefs] = useState(false);
  const [peerPdf, setPeerPdf] = useState<PeerPdfView | null>(null);
  const [linkItemOpen, setLinkItemOpen] = useState(false);
  const [lifecycleBusy, setLifecycleBusy] = useState(false);
  const [currentLifecycleStage, setCurrentLifecycleStage] = useState<DrawingLifecycleStage | null | undefined>(
    lifecycleStage,
  );
  const candidateListId = useId();
  const addBlurTimer = useRef<ReturnType<typeof setTimeout> | null>(null);
  const { isRevisionAlertOpen, revisionData, dismissRevisionAlert } = useDrawingWebSocket(
    open ? partNo : '',
  );

  const archived = isArchivedLifecycle(currentLifecycleStage);
  const editable = Boolean(
    canManage && !readOnly && !isDeleted && !archived && selectedHistory?.isLatest === 'Y',
  );
  const lifecycleEditable = Boolean(
    canManage &&
      canEditLifecycleManually(
        drawingType,
        selectedHistory?.isLatest === 'Y',
        Boolean(isDeleted),
        Boolean(readOnly),
        currentLifecycleStage,
      ),
  );
  const archiveAvailable = Boolean(
    canManage &&
      canArchiveDrawing(
        drawingType,
        selectedHistory?.isLatest === 'Y',
        Boolean(isDeleted),
        Boolean(readOnly),
        currentLifecycleStage,
      ),
  );
  const unarchiveAvailable = Boolean(
    canManage &&
      canUnarchiveDrawing(
        selectedHistory?.isLatest === 'Y',
        Boolean(isDeleted),
        Boolean(readOnly),
        currentLifecycleStage,
      ),
  );

  useEffect(() => {
    if (open) {
      setCurrentLifecycleStage(lifecycleStage);
    }
  }, [open, lifecycleStage]);

  const reloadHistories = (preferHistoryId?: string | null) => {
    if (!masterId) {
      return;
    }
    void fetchDrawingHistories(masterId)
      .then((data) => {
        setHistories(data);
        const preferred =
          (preferHistoryId ? data.find((row) => row.id === preferHistoryId) : undefined) ?? data[0] ?? null;
        setSelectedHistory(preferred);
      })
      .catch(() => {
        setHistories([]);
        setSelectedHistory(null);
        onError('도면 이력을 불러오지 못했습니다.');
      });
  };

  useEffect(() => {
    if (!open || !masterId) {
      return;
    }
    setPanel('pdf');
    setHistorySidebarCollapsed(true);
    setPeerPdf(null);
    setSelectedHistory(null);
    setReferences([]);
    setWhereUsed([]);
    setCandidates([]);
    setAddHistoryId('');
    setAddQuery('');
    setAddListOpen(false);
    setBulkPreviewOpen(false);
    setBulkSelectedIds(new Set());
    reloadHistories();
    // eslint-disable-next-line react-hooks/exhaustive-deps -- open/masterId 전환 시에만 초기 로드
  }, [open, masterId]);

  useEffect(() => {
    if (panel !== 'pdf') {
      setHistorySidebarCollapsed(false);
    }
  }, [panel]);

  useEffect(() => {
    if (!open || !selectedHistory) {
      return;
    }
    setReferences([]);
    setWhereUsed([]);
    setAddHistoryId('');
    setAddQuery('');
    setAddListOpen(false);
    setBulkPreviewOpen(false);
    setBulkSelectedIds(new Set());
    if (panel === 'pdf') {
      return;
    }
    if (panel === 'contains') {
      void fetchDrawingReferences(masterId, selectedHistory.id)
        .then(setReferences)
        .catch((error) => {
          setReferences([]);
          onError(error instanceof Error ? error.message : '구성 참조를 불러오지 못했습니다.');
        });
      if (editable) {
        void fetchDrawingReferenceCandidates(masterId)
          .then((rows) => {
            setCandidates(rows);
            setCandidatesHint(
              rows.length === 0
                ? '연결 품목의 BOM 하위에 도면이 없거나, 도면에 품목이 연결되지 않았습니다.'
                : null,
            );
          })
          .catch(() => {
            setCandidates([]);
            setCandidatesHint('BOM 하위 도면 후보를 불러오지 못했습니다.');
          });
      }
      return;
    }
    void fetchDrawingWhereUsed(selectedHistory.id)
      .then(setWhereUsed)
      .catch((error) => {
        setWhereUsed([]);
        onError(error instanceof Error ? error.message : '역참조를 불러오지 못했습니다.');
      });
  }, [open, selectedHistory?.id, panel, masterId, editable, onError]);

  const candidateOptions = useMemo(
    () =>
      candidates.filter(
        (row) => !references.some((ref) => ref.childHistoryId === row.historyId || ref.child.masterId === row.masterId),
      ),
    [candidates, references],
  );

  const filteredCandidateOptions = useMemo(() => {
    const selected = candidateOptions.find((row) => row.historyId === addHistoryId);
    const selectedLabel = selected ? formatCandidateLabel(selected) : '';
    const filterQuery =
      addQuery.trim() && (!selected || addQuery !== selectedLabel) ? addQuery : '';
    return candidateOptions.filter((row) => matchesCandidateQuery(row, filterQuery));
  }, [candidateOptions, addQuery, addHistoryId]);

  const clearCandidateSelection = () => {
    setAddHistoryId('');
    setAddQuery('');
    setAddListOpen(false);
  };

  const pickCandidate = (row: DrawingReferenceCandidate) => {
    setAddHistoryId(row.historyId);
    setAddQuery(formatCandidateLabel(row));
    setAddListOpen(false);
  };

  const historyMatchesHighlight = (history: DrawingHistoryItem): boolean => {
    const query = historyHighlightQuery?.trim().toLowerCase();
    if (!query) {
      return false;
    }
    const version = `v${history.majorVersion}.${history.minorVersion}`;
    const haystack = [
      history.changeReason,
      history.changeType,
      version,
      `${history.majorVersion}.${history.minorVersion}`,
    ]
      .join(' ')
      .toLowerCase();
    return haystack.includes(query);
  };

  if (!open) {
    return null;
  }

  const syncDrawingListMeta = async (patch: Partial<DrawingListItem>) => {
    const nextPatch = { id: masterId, ...patch };
    queryClient.setQueriesData<DrawingListItem[]>(
      { queryKey: ['drawings'] },
      (prev) => {
        if (!Array.isArray(prev)) {
          return prev;
        }
        return prev.map((row) => (row.id === masterId ? { ...row, ...nextPatch } : row));
      },
    );
    onDrawingMetaChange?.(nextPatch);
    await queryClient.refetchQueries({ queryKey: ['drawings'] });
  };

  const handlePromote = async () => {
    if (
      !(await confirm(
        '이 도면을 양산품(PROD)으로 이관하시겠습니까?\n구성 참조 자식은 모두 PROD여야 합니다.',
        { cancelLabel: '닫기', danger: true },
      ))
    ) {
      return;
    }
    try {
      await promoteDrawing(partNo);
      await syncDrawingListMeta({
        drawingType: 'PROD',
        lifecycleStage: 'MASS_PROD_READY',
      });
      onSuccess('양산 이관이 완료되었습니다.');
      onClose();
    } catch (error) {
      onError(error instanceof Error ? error.message : '이관에 실패했습니다.');
    }
  };

  const handleReopenDev = async () => {
    const reason = window.prompt('개발 재개 사유를 입력해 주세요. (선택)');
    if (reason === null) {
      return;
    }
    if (
      !(await confirm(
        '양산(PROD) 도면을 개발 단계로 재개하시겠습니까?\n이후 변경은 개발 이력에서 진행합니다.',
        { title: '개발 재개', confirmLabel: '재개', cancelLabel: '닫기', danger: true },
      ))
    ) {
      return;
    }
    try {
      await reopenDrawingDev(partNo, { reason: reason.trim() || null });
      await syncDrawingListMeta({
        drawingType: 'DEV',
        lifecycleStage: 'SAMPLE',
      });
      onSuccess('개발 단계로 재개되었습니다.');
      onClose();
    } catch (error) {
      onError(error instanceof Error ? error.message : '개발 재개에 실패했습니다.');
    }
  };

  const handleLifecycleChange = async (nextStage: DrawingLifecycleStage) => {
    if (!lifecycleEditable || nextStage === currentLifecycleStage) {
      return;
    }
    setLifecycleBusy(true);
    try {
      await updateDrawingLifecycle(masterId, { lifecycleStage: nextStage });
      setCurrentLifecycleStage(nextStage);
      await syncDrawingListMeta({ lifecycleStage: nextStage });
      onSuccess(`업무 단계가 「${lifecycleStageLabel(nextStage)}」로 변경되었습니다.`);
    } catch (error) {
      onError(error instanceof Error ? error.message : '업무 단계 변경에 실패했습니다.');
    } finally {
      setLifecycleBusy(false);
    }
  };

  const handleArchive = async () => {
    if (!archiveAvailable) {
      return;
    }
    const isProd = drawingType === 'PROD';
    const message = isProd
      ? '이 양산 도면을 보관하시겠습니까?\n\n더 이상 양산·개정에 사용하지 않습니다.\n기출고분 AS·재제작 납품을 위해 PDF·이력은 열람만 가능하며, 개정·버전업·개발 재개·품목 연결은 할 수 없습니다.\n필요 시 「보관 해제」로 다시 활성 상태로 되돌릴 수 있습니다.'
      : '개발을 중단하고 보관하시겠습니까?\n\n프로젝트 취소·보류 시 사용합니다.\nPDF·이력은 남으며, 이후 개정·양산 이관은 할 수 없습니다.\n필요 시 「보관 해제」로 되돌릴 수 있습니다.\n(양산 종료 보관은 양산품 상세의 「보관」을 사용하세요.)';
    if (
      !(await confirm(message, {
        title: archiveActionLabel(drawingType),
        confirmLabel: '보관',
        cancelLabel: '닫기',
        danger: true,
      }))
    ) {
      return;
    }
    setLifecycleBusy(true);
    try {
      await updateDrawingLifecycle(masterId, { lifecycleStage: 'ARCHIVED' });
      setCurrentLifecycleStage('ARCHIVED');
      await syncDrawingListMeta({ lifecycleStage: 'ARCHIVED' });
      onSuccess(
        isProd
          ? '양산 도면을 보관했습니다. AS·기출고용으로 열람만 가능합니다.'
          : '개발 중단(보관) 처리되었습니다.',
      );
    } catch (error) {
      onError(error instanceof Error ? error.message : '보관 처리에 실패했습니다.');
    } finally {
      setLifecycleBusy(false);
    }
  };

  const handleUnarchive = async () => {
    if (!unarchiveAvailable) {
      return;
    }
    const restoreStage = restoreStageAfterUnarchive(drawingType, itemId);
    const restoreLabel = lifecycleStageLabel(restoreStage);
    if (
      !(await confirm(
        `보관을 해제하고 「${restoreLabel}」 단계로 되돌리시겠습니까?\n\n해제 후에는 다시 개정·품목 연결·개발 재개 등 업무를 진행할 수 있습니다.`,
        {
          title: '보관 해제',
          confirmLabel: '보관 해제',
          cancelLabel: '닫기',
        },
      ))
    ) {
      return;
    }
    setLifecycleBusy(true);
    try {
      await updateDrawingLifecycle(masterId, { lifecycleStage: restoreStage });
      setCurrentLifecycleStage(restoreStage);
      await syncDrawingListMeta({ lifecycleStage: restoreStage });
      onSuccess(`보관을 해제했습니다. 업무 단계: 「${restoreLabel}」`);
    } catch (error) {
      onError(error instanceof Error ? error.message : '보관 해제에 실패했습니다.');
    } finally {
      setLifecycleBusy(false);
    }
  };

  const persistReferences = async (next: DrawingReferenceItem[]) => {
    if (!selectedHistory) {
      return;
    }
    setSavingRefs(true);
    try {
      await replaceDrawingReferences(
        masterId,
        selectedHistory.id,
        next.map((ref, index) => ({
          childHistoryId: ref.childHistoryId,
          refRole: ref.refRole || 'COMPONENT',
          sortOrder: index + 1,
          remark: ref.remark ?? null,
        })),
      );
      const refreshed = await fetchDrawingReferences(masterId, selectedHistory.id);
      setReferences(refreshed);
      onSuccess('구성 참조가 저장되었습니다.');
    } catch (error) {
      onError(error instanceof Error ? error.message : '구성 참조 저장에 실패했습니다.');
    } finally {
      setSavingRefs(false);
    }
  };

  const candidateToReference = (
    candidate: DrawingReferenceCandidate,
    parentHistoryId: string,
    sortOrder: number,
  ): DrawingReferenceItem => ({
    id: `tmp-${candidate.historyId}`,
    parentHistoryId,
    childHistoryId: candidate.historyId,
    refRole: 'COMPONENT',
    sortOrder,
    remark: null,
    child: {
      historyId: candidate.historyId,
      masterId: candidate.masterId,
      partNo: candidate.partNo,
      partName: candidate.partName,
      drawingType: candidate.drawingType,
      majorVersion: candidate.majorVersion,
      minorVersion: candidate.minorVersion,
      itemId: candidate.itemId,
      itemNo: candidate.itemNo,
    },
  });

  const handleAddReference = async () => {
    if (!addHistoryId || !selectedHistory) {
      return;
    }
    const candidate = candidates.find((row) => row.historyId === addHistoryId);
    if (!candidate) {
      onError('선택한 도면 후보를 찾을 수 없습니다.');
      return;
    }
    const next: DrawingReferenceItem[] = [
      ...references,
      candidateToReference(candidate, selectedHistory.id, references.length + 1),
    ];
    clearCandidateSelection();
    await persistReferences(next);
  };

  const openBulkPreview = () => {
    if (candidateOptions.length === 0) {
      onError('추가할 BOM 하위 도면 후보가 없습니다.');
      return;
    }
    setBulkSelectedIds(new Set(candidateOptions.map((row) => row.historyId)));
    setBulkPreviewOpen(true);
  };

  const closeBulkPreview = () => {
    setBulkPreviewOpen(false);
    setBulkSelectedIds(new Set());
  };

  const toggleBulkSelected = (historyId: string) => {
    setBulkSelectedIds((prev) => {
      const next = new Set(prev);
      if (next.has(historyId)) {
        next.delete(historyId);
      } else {
        next.add(historyId);
      }
      return next;
    });
  };

  const toggleBulkSelectAll = () => {
    setBulkSelectedIds((prev) => {
      if (prev.size === candidateOptions.length) {
        return new Set();
      }
      return new Set(candidateOptions.map((row) => row.historyId));
    });
  };

  const handleConfirmBulkAdd = async () => {
    if (!selectedHistory) {
      return;
    }
    const selected = candidateOptions.filter((row) => bulkSelectedIds.has(row.historyId));
    if (selected.length === 0) {
      onError('저장할 하위 도면을 선택해 주세요.');
      return;
    }
    const confirmed = await confirm(
      `선택한 BOM 하위 도면 ${selected.length}건을 구성 참조에 추가하시겠습니까?`,
      { title: 'BOM 하위 일괄 추가', confirmLabel: '저장', cancelLabel: '닫기' },
    );
    if (!confirmed) {
      return;
    }
    let sortOrder = references.length;
    const next: DrawingReferenceItem[] = [
      ...references,
      ...selected.map((row) => {
        sortOrder += 1;
        return candidateToReference(row, selectedHistory.id, sortOrder);
      }),
    ];
    await persistReferences(next);
    closeBulkPreview();
    clearCandidateSelection();
  };

  const handleRemoveReference = async (childHistoryId: string) => {
    const next = references.filter((ref) => ref.childHistoryId !== childHistoryId);
    await persistReferences(next);
  };

  const openPeerPdf = (peerPartNo: string, historyId: string, label?: string) => {
    setPeerPdf({
      partNo: peerPartNo,
      historyId,
      title: label ? `${peerPartNo} (${label})` : peerPartNo,
    });
  };

  return (
    <>
      <div className="modal-backdrop drawing-viewer-backdrop" role="presentation" onClick={onClose}>
        <div className="drawing-viewer-modal" role="dialog" onClick={(e) => e.stopPropagation()}>
          <div className="modal-header-row" style={{ padding: '0.85rem 1rem', margin: 0, borderBottom: '1px solid #e5e7eb' }}>
            <h2>도면 상세 보기 ({partNo})</h2>
            <button type="button" className="modal-close-btn" onClick={onClose} aria-label="닫기">
              <X size={20} />
            </button>
          </div>

          <div className="drawing-viewer-meta">
            <div className="drawing-viewer-meta__row">
              <span className={lifecycleBadgeClass(currentLifecycleStage)}>
                {lifecycleStageLabel(currentLifecycleStage)}
              </span>
              {lifecycleEditable ? (
                <label className="drawing-lifecycle-select">
                  <span className="hint-text">업무 단계 (개발 진행)</span>
                  <select
                    value={currentLifecycleStage ?? 'RECEIVED'}
                    disabled={lifecycleBusy}
                    onChange={(e) => void handleLifecycleChange(e.target.value as DrawingLifecycleStage)}
                  >
                    {DEV_ACTIVE_LIFECYCLE_STAGES.map((stage) => (
                      <option key={stage} value={stage}>
                        {lifecycleStageLabel(stage)}
                      </option>
                    ))}
                  </select>
                </label>
              ) : null}
            </div>
            {archived ? (
              <p className="hint-text drawing-viewer-meta__archive-hint">
                보관 상태입니다. 기출고·AS용 PDF·이력 열람만 가능하며, 개정·버전업·품목 연결·개발 재개는 할 수
                없습니다. 다시 사용하려면 「보관 해제」를 눌러 주세요.
              </p>
            ) : null}
            {sourcePartnerName ? (
              <p className="drawing-viewer-meta__text">선수신 거래처: {sourcePartnerName}</p>
            ) : null}
            {itemNo ? (
              <p className="drawing-viewer-meta__text">
                연결 품목: {itemNo}
                {itemLinkedAt ? ` · ${itemLinkedAt}` : ''}
              </p>
            ) : null}
            {editable && canLinkItem(drawingType, currentLifecycleStage) ? (
              <button type="button" className="secondary drawing-viewer-meta__action" onClick={() => setLinkItemOpen(true)}>
                <Link2 size={16} />
                {itemNo ? '품목 재연결' : '품목 연결'}
              </button>
            ) : null}
            {editable && canReopenDev(drawingType, currentLifecycleStage) ? (
              <button type="button" className="secondary drawing-viewer-meta__action" onClick={() => void handleReopenDev()}>
                <RefreshCw size={16} />
                개발 재개
              </button>
            ) : null}
            {archiveAvailable ? (
              <button
                type="button"
                className="secondary drawing-viewer-meta__action drawing-viewer-meta__action--archive"
                disabled={lifecycleBusy}
                onClick={() => void handleArchive()}
              >
                <Archive size={16} />
                {archiveActionLabel(drawingType)}
              </button>
            ) : null}
            {unarchiveAvailable ? (
              <button
                type="button"
                className="secondary drawing-viewer-meta__action drawing-viewer-meta__action--unarchive"
                disabled={lifecycleBusy}
                onClick={() => void handleUnarchive()}
              >
                <RotateCcw size={16} />
                보관 해제
              </button>
            ) : null}
          </div>

          <div className="drawing-viewer-tabs" role="tablist">
            <button
              type="button"
              role="tab"
              className={panel === 'pdf' ? 'tab-active' : undefined}
              onClick={() => setPanel('pdf')}
            >
              PDF
            </button>
            <button
              type="button"
              role="tab"
              className={panel === 'contains' ? 'tab-active' : undefined}
              onClick={() => setPanel('contains')}
            >
              구성(참조)
            </button>
            <button
              type="button"
              role="tab"
              className={panel === 'where-used' ? 'tab-active' : undefined}
              onClick={() => setPanel('where-used')}
            >
              역참조
            </button>
          </div>

          <div
            className={`drawing-viewer-body${panel === 'pdf' && historySidebarCollapsed ? ' drawing-viewer-body--history-collapsed' : ''}`}
          >
            <aside className="drawing-history-sidebar">
              <div className="drawing-history-sidebar__header">
                <h3>개정 이력 (최신순)</h3>
                {panel === 'pdf' && (
                  <button
                    type="button"
                    className="drawing-history-sidebar__collapse"
                    title="개정 이력 접기"
                    aria-label="개정 이력 접기"
                    onClick={() => setHistorySidebarCollapsed(true)}
                  >
                    <PanelLeftClose size={16} />
                  </button>
                )}
              </div>
              {historyHighlightQuery ? (
                <p className="hint-text">AS 검색어 「{historyHighlightQuery}」와 일치하는 이력이 강조됩니다.</p>
              ) : (
                <p className="hint-text">과거 이력 PDF는 워터마크와 함께 열람됩니다. V1.1 = 메이저 1·마이너 1.</p>
              )}
              <ul className="drawing-history-list">
                {histories.map((history) => (
                  <li key={history.id}>
                    <button
                      type="button"
                      className={`drawing-history-item${selectedHistory?.id === history.id ? ' drawing-history-item--active' : ''}${historyMatchesHighlight(history) ? ' drawing-history-item--highlight' : ''}`}
                      onClick={() => setSelectedHistory(history)}
                    >
                      <div className="drawing-history-item__top">
                        <span className="drawing-history-item__version">
                          V{history.majorVersion}.{history.minorVersion}
                        </span>
                        {history.isLatest === 'Y' && <span className="drawing-version-badge">최신본</span>}
                      </div>
                      <p className="drawing-history-item__date">{history.createdAt}</p>
                      <p className="drawing-history-item__reason">{history.changeReason || '사유 없음'}</p>
                    </button>
                  </li>
                ))}
              </ul>
            </aside>

            <div className="drawing-viewer-main">
              {panel === 'pdf' && historySidebarCollapsed && (
                <button
                  type="button"
                  className="drawing-history-toggle"
                  title="개정 이력 펼치기"
                  aria-label="개정 이력 펼치기"
                  aria-expanded={false}
                  onClick={() => setHistorySidebarCollapsed(false)}
                >
                  <PanelLeftOpen size={16} />
                  <span>이력</span>
                </button>
              )}
              {panel === 'pdf' &&
                (selectedHistory ? (
                  <PdfViewer
                    key={selectedHistory.id}
                    pdfUrl={drawingPdfUrl(partNo, selectedHistory.id)}
                    partNo={partNo}
                    title={`${partNo} V${selectedHistory.majorVersion}.${selectedHistory.minorVersion}`}
                    updateOfflineCache={selectedHistory.isLatest === 'Y'}
                    onError={onError}
                    toolbarActions={
                      <>
                        {!readOnly &&
                        canPromoteDrawing(drawingType, currentLifecycleStage) &&
                        selectedHistory.isLatest === 'Y' &&
                        !isDeleted ? (
                          <button type="button" className="btn-promote" onClick={() => void handlePromote()}>
                            <ArrowRightCircle size={16} />
                            양산 도면으로 이관
                          </button>
                        ) : null}
                      </>
                    }
                  />
                ) : (
                  <p className="drawing-pdf-empty">이력을 불러오는 중입니다…</p>
                ))}

              {panel === 'contains' && (
                <div className="drawing-ref-panel">
                  <div className="drawing-ref-panel__header">
                    <h3>구성 참조 {selectedHistory ? `(V${selectedHistory.majorVersion}.${selectedHistory.minorVersion})` : ''}</h3>
                    <p className="hint-text">
                      이 이력(Rev)이 가리키는 하위 도면의 특정 버전입니다.
                      {!editable && ' 최신 이력만 수정할 수 있습니다.'}
                    </p>
                  </div>
                  {editable && (
                    <div className="drawing-ref-add">
                      <label className="item-combobox drawing-ref-candidate-combobox">
                        <input
                          role="combobox"
                          aria-label="BOM 하위 도면 선택"
                          aria-expanded={addListOpen}
                          aria-controls={candidateListId}
                          aria-autocomplete="list"
                          value={addQuery}
                          placeholder="품번·품명 입력 또는 BOM 하위 도면 선택…"
                          disabled={savingRefs}
                          onChange={(e) => {
                            const value = e.target.value;
                            setAddQuery(value);
                            setAddListOpen(true);
                            const selected = candidateOptions.find((row) => row.historyId === addHistoryId);
                            if (selected && value !== formatCandidateLabel(selected)) {
                              setAddHistoryId('');
                            }
                          }}
                          onFocus={() => {
                            if (addBlurTimer.current) {
                              clearTimeout(addBlurTimer.current);
                            }
                            setAddListOpen(true);
                          }}
                          onBlur={() => {
                            addBlurTimer.current = setTimeout(() => {
                              setAddListOpen(false);
                              const match = filteredCandidateOptions.find(
                                (row) => formatCandidateLabel(row) === addQuery.trim(),
                              );
                              if (match) {
                                pickCandidate(match);
                              }
                            }, 150);
                          }}
                          onKeyDown={(e) => {
                            if (e.key === 'Escape') {
                              setAddListOpen(false);
                            }
                            if (e.key === 'Enter' && addHistoryId) {
                              e.preventDefault();
                              void handleAddReference();
                            }
                          }}
                        />
                        {addListOpen && !savingRefs && (
                          <ul id={candidateListId} className="item-combobox-list" role="listbox">
                            {filteredCandidateOptions.length === 0 ? (
                              <li className="item-combobox-empty">
                                {candidateOptions.length === 0
                                  ? '추가 가능한 BOM 하위 도면이 없습니다.'
                                  : '일치하는 도면이 없습니다.'}
                              </li>
                            ) : (
                              filteredCandidateOptions.map((row) => (
                                <li key={row.historyId}>
                                  <button
                                    type="button"
                                    role="option"
                                    className="item-combobox-option"
                                    aria-selected={row.historyId === addHistoryId}
                                    onMouseDown={(e) => e.preventDefault()}
                                    onClick={() => pickCandidate(row)}
                                  >
                                    {formatCandidateLabel(row)}
                                  </button>
                                </li>
                              ))
                            )}
                          </ul>
                        )}
                      </label>
                      <button
                        type="button"
                        disabled={!addHistoryId || savingRefs}
                        onClick={() => void handleAddReference()}
                      >
                        <Plus size={16} />
                        추가
                      </button>
                      <button
                        type="button"
                        className="secondary"
                        disabled={savingRefs || candidateOptions.length === 0}
                        onClick={openBulkPreview}
                        title={
                          candidateOptions.length === 0
                            ? '추가 가능한 BOM 하위 도면이 없습니다'
                            : `미연결 BOM 하위 ${candidateOptions.length}건 미리보기`
                        }
                      >
                        <ListPlus size={16} />
                        BOM 하위 일괄 추가
                      </button>
                    </div>
                  )}
                  {editable && bulkPreviewOpen && (
                    <section className="drawing-ref-bulk-preview">
                      <div className="drawing-ref-bulk-preview__header">
                        <h4>BOM 하위 일괄 추가 미리보기</h4>
                        <div className="drawing-ref-bulk-preview__actions">
                          <button
                            type="button"
                            className="secondary"
                            disabled={savingRefs}
                            onClick={closeBulkPreview}
                          >
                            닫기
                          </button>
                          <button
                            type="button"
                            disabled={savingRefs || bulkSelectedIds.size === 0}
                            onClick={() => void handleConfirmBulkAdd()}
                          >
                            {savingRefs ? '저장 중…' : `선택 ${bulkSelectedIds.size}건 저장`}
                          </button>
                        </div>
                      </div>
                      <p className="hint-text">
                        이미 구성된 도면은 제외됩니다. 저장 전 체크 해제로 제외할 수 있습니다.
                      </p>
                      <div className="table-wrap">
                        <table className="drawing-ref-table">
                          <thead>
                            <tr>
                              <th>
                                <input
                                  type="checkbox"
                                  checked={
                                    candidateOptions.length > 0 &&
                                    bulkSelectedIds.size === candidateOptions.length
                                  }
                                  disabled={savingRefs || candidateOptions.length === 0}
                                  onChange={toggleBulkSelectAll}
                                  aria-label="전체 선택"
                                />
                              </th>
                              <th>레벨</th>
                              <th>품번</th>
                              <th>품명</th>
                              <th>품목</th>
                              <th>구분</th>
                              <th>버전</th>
                            </tr>
                          </thead>
                          <tbody>
                            {candidateOptions.map((row) => (
                              <tr key={row.historyId}>
                                <td>
                                  <input
                                    type="checkbox"
                                    checked={bulkSelectedIds.has(row.historyId)}
                                    disabled={savingRefs}
                                    onChange={() => toggleBulkSelected(row.historyId)}
                                    aria-label={`${row.partNo} 선택`}
                                  />
                                </td>
                                <td>L{row.bomLevel}</td>
                                <td>{row.partNo}</td>
                                <td>{row.partName}</td>
                                <td>{row.itemNo ?? '-'}</td>
                                <td>{row.drawingType}</td>
                                <td>
                                  V{row.majorVersion}.{row.minorVersion}
                                </td>
                              </tr>
                            ))}
                          </tbody>
                        </table>
                      </div>
                    </section>
                  )}
                  {editable && candidatesHint && <p className="hint-text">{candidatesHint}</p>}
                  <p className="hint-text">후보 목록은 이 도면에 연결된 품목의 BOM 하위(도면 있는 품목)만 표시합니다.</p>
                  {references.length === 0 ? (
                    <p className="drawing-pdf-empty">구성된 하위 도면이 없습니다.</p>
                  ) : (
                    <table className="drawing-ref-table">
                      <thead>
                        <tr>
                          <th>품번</th>
                          <th>품명</th>
                          <th>구분</th>
                          <th>버전</th>
                          <th>역할</th>
                          <th />
                        </tr>
                      </thead>
                      <tbody>
                        {references.map((ref) => (
                          <tr key={ref.id}>
                            <td>{ref.child.partNo}</td>
                            <td>{ref.child.partName}</td>
                            <td>{ref.child.drawingType}</td>
                            <td>
                              V{ref.child.majorVersion}.{ref.child.minorVersion}
                            </td>
                            <td>{ref.refRole}</td>
                            <td className="drawing-row-actions">
                              <button
                                type="button"
                                className="drawing-icon-btn drawing-icon-btn--info"
                                title="PDF 열람"
                                onClick={() =>
                                  openPeerPdf(
                                    ref.child.partNo,
                                    ref.child.historyId,
                                    `V${ref.child.majorVersion}.${ref.child.minorVersion}`,
                                  )
                                }
                              >
                                <Eye size={16} />
                              </button>
                              {editable && (
                                <button
                                  type="button"
                                  className="drawing-icon-btn drawing-icon-btn--danger"
                                  disabled={savingRefs}
                                  onClick={() => void handleRemoveReference(ref.childHistoryId)}
                                  title="참조 제거"
                                >
                                  <Trash2 size={16} />
                                </button>
                              )}
                            </td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  )}
                </div>
              )}

              {panel === 'where-used' && (
                <div className="drawing-ref-panel">
                  <div className="drawing-ref-panel__header">
                    <h3>역참조 {selectedHistory ? `(V${selectedHistory.majorVersion}.${selectedHistory.minorVersion})` : ''}</h3>
                    <p className="hint-text">이 이력을 구성으로 포함하는 상위 도면입니다.</p>
                  </div>
                  {whereUsed.length === 0 ? (
                    <p className="drawing-pdf-empty">이 이력을 참조하는 상위 도면이 없습니다.</p>
                  ) : (
                    <table className="drawing-ref-table">
                      <thead>
                        <tr>
                          <th>품번</th>
                          <th>품명</th>
                          <th>구분</th>
                          <th>버전</th>
                          <th>역할</th>
                          <th />
                        </tr>
                      </thead>
                      <tbody>
                        {whereUsed.map((ref) => (
                          <tr key={ref.id}>
                            <td>{ref.parent.partNo}</td>
                            <td>{ref.parent.partName}</td>
                            <td>{ref.parent.drawingType}</td>
                            <td>
                              V{ref.parent.majorVersion}.{ref.parent.minorVersion}
                            </td>
                            <td>{ref.refRole}</td>
                            <td className="drawing-row-actions">
                              <button
                                type="button"
                                className="drawing-icon-btn drawing-icon-btn--info"
                                title="PDF 열람"
                                onClick={() =>
                                  openPeerPdf(
                                    ref.parent.partNo,
                                    ref.parent.historyId,
                                    `V${ref.parent.majorVersion}.${ref.parent.minorVersion}`,
                                  )
                                }
                              >
                                <Eye size={16} />
                              </button>
                            </td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  )}
                </div>
              )}
            </div>
          </div>
        </div>
      </div>

      {peerPdf && (
        <div
          className="modal-backdrop drawing-peer-pdf-backdrop"
          style={{ zIndex: 130 }}
          role="presentation"
          onClick={() => setPeerPdf(null)}
        >
          <div
            className="drawing-viewer-modal drawing-peer-pdf-modal"
            role="dialog"
            onClick={(e) => e.stopPropagation()}
          >
            <div
              className="modal-header-row"
              style={{ padding: '0.85rem 1rem', margin: 0, borderBottom: '1px solid #e5e7eb' }}
            >
              <h2>참조 도면 PDF ({peerPdf.title})</h2>
              <button
                type="button"
                className="modal-close-btn"
                onClick={() => setPeerPdf(null)}
                aria-label="닫기"
              >
                <X size={20} />
              </button>
            </div>
            <div className="drawing-peer-pdf-body">
              <PdfViewer
                key={peerPdf.historyId}
                pdfUrl={drawingPdfUrl(peerPdf.partNo, peerPdf.historyId)}
                partNo={peerPdf.partNo}
                onError={onError}
              />
            </div>
          </div>
        </div>
      )}

      {linkItemOpen && (
        <DrawingLinkItemModal
          open={linkItemOpen}
          onClose={() => setLinkItemOpen(false)}
          drawingId={masterId}
          partNo={partNo}
          initialItemId={itemId}
          initialItemNo={itemNo}
          onSuccess={(message) => {
            setLinkItemOpen(false);
            onSuccess(message);
            void (async () => {
              await queryClient.refetchQueries({ queryKey: ['drawings'] });
              onClose();
            })();
          }}
          onError={onError}
        />
      )}

      {isRevisionAlertOpen && (
        <div className="modal-backdrop" style={{ zIndex: 120 }} role="presentation" onClick={dismissRevisionAlert}>
          <div className="modal" role="alertdialog" onClick={(e) => e.stopPropagation()}>
            <h2>도면 메이저 개정 알림</h2>
            <p className="drawing-alert-panel">{revisionData?.message ?? '도면이 개정되었습니다.'}</p>
            <p className="hint">
              품번 {revisionData?.partNo} — V{revisionData?.newMajorVersion}.0 으로 갱신되었습니다. 최신 이력을 다시
              불러오려면 아래 버튼을 눌러 주세요.
            </p>
            <div className="form-actions">
              <button type="button" className="secondary" onClick={dismissRevisionAlert}>
                나중에
              </button>
              <button
                type="button"
                onClick={() => {
                  dismissRevisionAlert();
                  void queryClient.invalidateQueries({ queryKey: ['drawings'] });
                  reloadHistories();
                  setPanel('pdf');
                  setPeerPdf(null);
                }}
              >
                최신본 불러오기
              </button>
            </div>
          </div>
        </div>
      )}
    </>
  );
}
