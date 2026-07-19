import { useEffect, useState } from 'react';
import { X } from 'lucide-react';
import { useQueryClient } from '@tanstack/react-query';
import { linkDrawingItem } from '../../api/drawing';
import ItemSearchField, { type ItemSearchSelection } from '../ItemSearchField';

interface DrawingLinkItemModalProps {
  open: boolean;
  onClose: () => void;
  drawingId: string;
  partNo: string;
  initialItemId?: number | null;
  initialItemNo?: string | null;
  initialItemName?: string | null;
  actorUserId?: string;
  onSuccess: (message: string) => void;
  onError: (message: string) => void;
}

export default function DrawingLinkItemModal({
  open,
  onClose,
  drawingId,
  partNo,
  initialItemId,
  initialItemNo,
  initialItemName,
  actorUserId,
  onSuccess,
  onError,
}: DrawingLinkItemModalProps) {
  const queryClient = useQueryClient();
  const [selectedItem, setSelectedItem] = useState<ItemSearchSelection | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const [localError, setLocalError] = useState<string | null>(null);

  useEffect(() => {
    if (open) {
      setSelectedItem(
        initialItemId && initialItemNo
          ? {
              id: initialItemId,
              itemNo: initialItemNo,
              itemName: initialItemName ?? initialItemNo,
            }
          : null,
      );
      setLocalError(null);
    }
  }, [open, initialItemId, initialItemNo, initialItemName]);

  if (!open) {
    return null;
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLocalError(null);

    if (!selectedItem) {
      setLocalError('연결할 품목을 선택해 주세요.');
      return;
    }

    setIsLoading(true);
    try {
      await linkDrawingItem(drawingId, { itemId: selectedItem.id }, actorUserId);
      await queryClient.invalidateQueries({ queryKey: ['drawings'] });
      onSuccess(`[${partNo}] 도면에 품목 ${selectedItem.itemNo}이(가) 연결되었습니다.`);
      onClose();
    } catch (err) {
      const message = err instanceof Error ? err.message : '품목 연결에 실패했습니다.';
      setLocalError(message);
      onError(message);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="modal-backdrop drawing-link-item-backdrop" role="presentation" onClick={!isLoading ? onClose : undefined}>
      <div className="modal modal-wide" role="dialog" onClick={(e) => e.stopPropagation()}>
        <div className="modal-header-row">
          <h2>기준정보 품목 연결</h2>
          <button type="button" className="modal-close-btn" onClick={onClose} disabled={isLoading} aria-label="닫기">
            <X size={20} />
          </button>
        </div>

        <form onSubmit={(e) => void handleSubmit(e)}>
          {localError && <p className="error-banner">{localError}</p>}
          <p className="hint">
            양산(PROD) 도면이 <strong>양산 준비</strong> 단계일 때 기준정보에 등록된 품목과 연결합니다.
            품목은 기준정보에서 먼저 등록한 뒤 여기서 연결해 주세요.
          </p>

          <div style={{ margin: '1rem 0' }}>
            <ItemSearchField
              label="연결 품목 *"
              selectedItem={selectedItem}
              onSelect={setSelectedItem}
              disabled={isLoading}
              placeholder="품목번호 또는 품목명으로 검색"
            />
          </div>

          <div className="form-actions">
            <button type="button" className="secondary" onClick={onClose} disabled={isLoading}>
              취소
            </button>
            <button type="submit" disabled={isLoading || !selectedItem}>
              {isLoading ? '연결 중…' : '품목 연결'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
