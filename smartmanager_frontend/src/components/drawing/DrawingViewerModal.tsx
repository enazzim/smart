import { useEffect, useId, useMemo, useRef, useState } from 'react';
import { ArrowRightCircle, Eye, ListPlus, Plus, Trash2, X } from 'lucide-react';
import { useQueryClient } from '@tanstack/react-query';
import {
  drawingPdfUrl,
  fetchDrawingHistories,
  fetchDrawingReferenceCandidates,
  fetchDrawingReferences,
  fetchDrawingWhereUsed,
  promoteDrawing,
  replaceDrawingReferences,
  type DrawingHistoryItem,
  type DrawingReferenceCandidate,
  type DrawingReferenceItem,
  type DrawingType,
  type DrawingWhereUsedItem,
} from '../../api/drawing';
import { useDrawingWebSocket } from '../../hooks/useDrawingWebSocket';
import PdfViewer from './PdfViewer';
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
  isDeleted?: boolean;
  readOnly?: boolean;
  canManage?: boolean;
  actorUserId?: string;
  onSuccess: (message: string) => void;
  onError: (message: string) => void;
}

export default function DrawingViewerModal({
  open,
  onClose,
  masterId,
  partNo,
  drawingType,
  isDeleted,
  readOnly,
  canManage,
  actorUserId,
  onSuccess,
  onError,
}: DrawingViewerModalProps) {
  const confirm = useConfirm();
  const queryClient = useQueryClient();
  const [histories, setHistories] = useState<DrawingHistoryItem[]>([]);
  const [selectedHistory, setSelectedHistory] = useState<DrawingHistoryItem | null>(null);
  const [panel, setPanel] = useState<ViewerPanel>('pdf');
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
  const candidateListId = useId();
  const addBlurTimer = useRef<ReturnType<typeof setTimeout> | null>(null);
  const { isRevisionAlertOpen, revisionData, dismissRevisionAlert } = useDrawingWebSocket(
    open ? partNo : '',
  );

  const editable = Boolean(canManage && !readOnly && !isDeleted && selectedHistory?.isLatest === 'Y');

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

  if (!open) {
    return null;
  }

  const handlePromote = async () => {
    if (!(await confirm('이 도면을 양산품(PROD) V1.0으로 이관하시겠습니까?\n구성 참조 자식은 모두 PROD여야 합니다.', { cancelLabel: '닫기', danger: true }))) {
      return;
    }
    try {
      await promoteDrawing(partNo, actorUserId);
      await queryClient.invalidateQueries({ queryKey: ['drawings'] });
      onSuccess('양산 이관이 완료되었습니다.');
      onClose();
    } catch (error) {
      onError(error instanceof Error ? error.message : '이관에 실패했습니다.');
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
        actorUserId,
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

          <div className="drawing-viewer-body">
            <aside className="drawing-history-sidebar">
              <h3>개정 이력 (최신순)</h3>
              <ul className="drawing-history-list">
                {histories.map((history) => (
                  <li key={history.id}>
                    <button
                      type="button"
                      className={`drawing-history-item${selectedHistory?.id === history.id ? ' drawing-history-item--active' : ''}`}
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
                      !readOnly && drawingType === 'DEV' && selectedHistory.isLatest === 'Y' && !isDeleted ? (
                        <button type="button" className="btn-promote" onClick={() => void handlePromote()}>
                          <ArrowRightCircle size={16} />
                          양산 도면으로 이관
                        </button>
                      ) : null
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
