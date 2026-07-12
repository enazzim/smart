import { useEffect, useState } from 'react';
import { ArrowRightCircle, X } from 'lucide-react';
import { useQueryClient } from '@tanstack/react-query';
import {
  drawingPdfUrl,
  fetchDrawingHistories,
  promoteDrawing,
  type DrawingHistoryItem,
  type DrawingType,
} from '../../api/drawing';
import { useDrawingWebSocket } from '../../hooks/useDrawingWebSocket';
import PdfViewer from './PdfViewer';

interface DrawingViewerModalProps {
  open: boolean;
  onClose: () => void;
  masterId: string;
  partNo: string;
  drawingType: DrawingType;
  isDeleted?: boolean;
  readOnly?: boolean;
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
  actorUserId,
  onSuccess,
  onError,
}: DrawingViewerModalProps) {
  const queryClient = useQueryClient();
  const [histories, setHistories] = useState<DrawingHistoryItem[]>([]);
  const [selectedHistory, setSelectedHistory] = useState<DrawingHistoryItem | null>(null);
  const { isRevisionAlertOpen, revisionData, dismissRevisionAlert, forceReload } = useDrawingWebSocket(
    open ? partNo : '',
  );

  useEffect(() => {
    if (!open || !masterId) {
      return;
    }
    void fetchDrawingHistories(masterId)
      .then((data) => {
        setHistories(data);
        setSelectedHistory(data[0] ?? null);
      })
      .catch(() => {
        setHistories([]);
        setSelectedHistory(null);
        onError('도면 이력을 불러오지 못했습니다.');
      });
  }, [open, masterId, onError]);

  if (!open) {
    return null;
  }

  const handlePromote = async () => {
    if (!window.confirm('이 도면을 양산품(PROD) V1.0으로 이관하시겠습니까?')) {
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
              {selectedHistory ? (
                <PdfViewer
                  pdfUrl={drawingPdfUrl(partNo, selectedHistory.id)}
                  partNo={partNo}
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
              )}
            </div>
          </div>
        </div>
      </div>

      {isRevisionAlertOpen && (
        <div className="modal-backdrop" style={{ zIndex: 120 }} role="presentation" onClick={dismissRevisionAlert}>
          <div className="modal" role="alertdialog" onClick={(e) => e.stopPropagation()}>
            <h2>도면 메이저 개정 알림</h2>
            <p className="drawing-alert-panel">{revisionData?.message ?? '도면이 개정되었습니다.'}</p>
            <p className="hint">
              품번 {revisionData?.partNo} — V{revisionData?.newMajorVersion}.0 으로 갱신되었습니다. 구버전 도면을 보고
              계시면 즉시 새로고침해 주세요.
            </p>
            <div className="form-actions">
              <button type="button" className="secondary" onClick={dismissRevisionAlert}>
                나중에
              </button>
              <button type="button" onClick={forceReload}>
                지금 새로고침
              </button>
            </div>
          </div>
        </div>
      )}
    </>
  );
}
