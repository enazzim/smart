import { useCallback, useState } from 'react';
import { UploadCloud, X } from 'lucide-react';
import { useQueryClient } from '@tanstack/react-query';
import { reviseDrawing } from '../../api/drawing';
import { DRAWING_PDF_MAX_SIZE_LABEL, validateDrawingPdfFile } from '../../utils/drawingUpload';

interface DrawingReviseModalProps {
  open: boolean;
  onClose: () => void;
  partNo: string;
  currentMajor: number;
  currentMinor: number;
  onSuccess: (message: string) => void;
  onError: (message: string) => void;
}

export default function DrawingReviseModal({
  open,
  onClose,
  partNo,
  currentMajor,
  currentMinor,
  onSuccess,
  onError,
}: DrawingReviseModalProps) {
  const queryClient = useQueryClient();
  const [isDragging, setIsDragging] = useState(false);
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [changeType, setChangeType] = useState('MINOR');
  const [changeReason, setChangeReason] = useState('');
  const [isUploading, setIsUploading] = useState(false);
  const [fileError, setFileError] = useState<string | null>(null);

  const selectPdfFile = (file: File) => {
    const validationError = validateDrawingPdfFile(file);
    if (validationError) {
      setSelectedFile(null);
      setFileError(validationError);
      onError(validationError);
      return;
    }
    setFileError(null);
    setSelectedFile(file);
  };

  const handleDragOver = useCallback((e: React.DragEvent) => {
    e.preventDefault();
    setIsDragging(true);
  }, []);

  const handleDragLeave = useCallback((e: React.DragEvent) => {
    e.preventDefault();
    setIsDragging(false);
  }, []);

  const handleDrop = useCallback(
    (e: React.DragEvent) => {
      e.preventDefault();
      setIsDragging(false);
      const files = e.dataTransfer.files;
      if (files && files.length > 0) {
        selectPdfFile(files[0]);
      }
    },
    [onError],
  );

  if (!open) {
    return null;
  }

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files;
    if (files && files.length > 0) {
      selectPdfFile(files[0]);
    }
  };

  const handleRevise = async () => {
    if (!selectedFile) {
      return;
    }
    setIsUploading(true);
    try {
      await reviseDrawing(partNo, { changeType, changeReason }, selectedFile);
      await queryClient.invalidateQueries({ queryKey: ['drawings'] });
      onSuccess('도면이 개정되었습니다.');
      handleClose();
    } catch (error) {
      onError(error instanceof Error ? error.message : '개정에 실패했습니다.');
    } finally {
      setIsUploading(false);
    }
  };

  const handleClose = () => {
    if (isUploading) {
      return;
    }
    setSelectedFile(null);
    setFileError(null);
    setChangeReason('');
    setChangeType('MINOR');
    onClose();
  };

  return (
    <div className="modal-backdrop" role="presentation" onClick={handleClose}>
      <div className="modal modal-wide" role="dialog" onClick={(e) => e.stopPropagation()}>
        <div className="modal-header-row">
          <h2>도면 개정 ({partNo})</h2>
          <button type="button" className="modal-close-btn" onClick={handleClose} aria-label="닫기">
            <X size={20} />
          </button>
        </div>

        <label
          className={`import-dropzone${isDragging ? ' import-dropzone--active' : ''}${selectedFile ? ' import-dropzone--filled' : ''}`}
          onDragOver={handleDragOver}
          onDragLeave={handleDragLeave}
          onDrop={handleDrop}
        >
          <input type="file" accept="application/pdf" hidden onChange={handleFileChange} />
          <UploadCloud className="import-upload-icon" size={40} />
          <p className="import-dropzone__title">새 리비전 PDF 파일 드롭</p>
          <p className="import-dropzone__hint">PDF 1개, {DRAWING_PDF_MAX_SIZE_LABEL} 이하</p>
          {fileError && (
            <p className="hint" style={{ color: '#b91c1c' }}>
              {fileError}
            </p>
          )}
          {selectedFile && (
            <span className="drawing-file-chip">
              {selectedFile.name}
              <button
                type="button"
                aria-label="파일 제거"
                onClick={(e) => {
                  e.preventDefault();
                  setSelectedFile(null);
                }}
              >
                <X size={14} />
              </button>
            </span>
          )}
        </label>

        <div className="form-grid" style={{ marginTop: '1rem' }}>
          <label>
            개정 유형 *
            <select value={changeType} onChange={(e) => setChangeType(e.target.value)}>
              <option value="MINOR">
                마이너 개정 (V{currentMajor}.{currentMinor} → V{currentMajor}.{currentMinor + 1})
              </option>
              <option value="MAJOR">
                메이저 개정 (V{currentMajor}.{currentMinor} → V{currentMajor + 1}.0)
              </option>
            </select>
          </label>
          <label>
            개정 사유 *
            <textarea rows={3} value={changeReason} onChange={(e) => setChangeReason(e.target.value)} />
          </label>
        </div>

        <div className="form-actions">
          <button type="button" className="secondary" onClick={handleClose} disabled={isUploading}>
            취소
          </button>
          <button
            type="button"
            disabled={!selectedFile || !changeReason || !!fileError || isUploading}
            onClick={() => void handleRevise()}
          >
            {isUploading ? '개정 중…' : '개정하기'}
          </button>
        </div>
      </div>
    </div>
  );
}
