import { useEffect, useState } from 'react';
import { X } from 'lucide-react';
import { useQueryClient } from '@tanstack/react-query';
import { updateDrawingInfo } from '../../api/drawing';
import ItemSearchField, { type ItemSearchSelection } from '../ItemSearchField';

interface DrawingInfoEditModalProps {
  open: boolean;
  onClose: () => void;
  drawingId: string;
  initialPartNo: string;
  initialPartName: string;
  initialModelGroup: string;
  initialItemId?: number | null;
  initialItemNo?: string | null;
  actorUserId?: string;
  onSuccess: (message: string) => void;
  onError: (message: string) => void;
}

export default function DrawingInfoEditModal({
  open,
  onClose,
  drawingId,
  initialPartNo,
  initialPartName,
  initialModelGroup,
  initialItemId,
  initialItemNo,
  actorUserId,
  onSuccess,
  onError,
}: DrawingInfoEditModalProps) {
  const queryClient = useQueryClient();
  const [partNo, setPartNo] = useState(initialPartNo);
  const [partName, setPartName] = useState(initialPartName);
  const [modelGroup, setModelGroup] = useState(initialModelGroup);
  const [selectedItem, setSelectedItem] = useState<ItemSearchSelection | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const [localError, setLocalError] = useState<string | null>(null);

  useEffect(() => {
    if (open) {
      setPartNo(initialPartNo);
      setPartName(initialPartName);
      setModelGroup(initialModelGroup);
      setSelectedItem(
        initialItemId && initialItemNo
          ? { id: initialItemId, itemNo: initialItemNo, itemName: initialPartName }
          : null,
      );
      setLocalError(null);
    }
  }, [open, initialPartNo, initialPartName, initialModelGroup, initialItemId, initialItemNo]);

  if (!open) {
    return null;
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLocalError(null);

    if (!partNo.trim() || !partName.trim() || !modelGroup.trim()) {
      setLocalError('모든 필수 정보를 입력해 주세요.');
      return;
    }

    setIsLoading(true);
    try {
      await updateDrawingInfo(
        drawingId,
        {
          partNo,
          partName,
          modelGroup,
          itemId: selectedItem?.id ?? null,
        },
        actorUserId,
      );
      await queryClient.invalidateQueries({ queryKey: ['drawings'] });
      onSuccess('도면 정보가 성공적으로 수정되었습니다.');
      onClose();
    } catch (err) {
      const message = err instanceof Error ? err.message : '정보 수정 중 오류가 발생했습니다.';
      setLocalError(message);
      onError(message);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="modal-backdrop" role="presentation" onClick={!isLoading ? onClose : undefined}>
      <div className="modal modal-wide" role="dialog" onClick={(e) => e.stopPropagation()}>
        <div className="modal-header-row">
          <h2>도면 정보 수정</h2>
          <button type="button" className="modal-close-btn" onClick={onClose} disabled={isLoading} aria-label="닫기">
            <X size={20} />
          </button>
        </div>

        <form onSubmit={(e) => void handleSubmit(e)}>
          {localError && <p className="error-banner">{localError}</p>}
          <p className="hint">도면 파일(버전 이력)은 그대로 유지되며, 기본 메타 정보만 수정합니다.</p>

          <div style={{ margin: '1rem 0' }}>
            <ItemSearchField
              label="연결 품목 (선택)"
              selectedItem={selectedItem}
              onSelect={setSelectedItem}
              disabled={isLoading}
            />
          </div>

          <div className="form-grid form-grid-wide">
            <label>
              품번 *
              <input value={partNo} onChange={(e) => setPartNo(e.target.value)} disabled={isLoading} />
            </label>
            <label>
              품명 *
              <input value={partName} onChange={(e) => setPartName(e.target.value)} disabled={isLoading} />
            </label>
            <label>
              기종 *
              <input value={modelGroup} onChange={(e) => setModelGroup(e.target.value)} disabled={isLoading} />
            </label>
          </div>

          <div className="form-actions">
            <button type="button" className="secondary" onClick={onClose} disabled={isLoading}>
              취소
            </button>
            <button type="submit" disabled={isLoading || !partNo || !partName || !modelGroup}>
              {isLoading ? '저장 중…' : '저장'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
