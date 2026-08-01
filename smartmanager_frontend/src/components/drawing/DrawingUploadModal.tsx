import { useCallback, useState } from 'react';
import { UploadCloud, X } from 'lucide-react';
import { useQueryClient } from '@tanstack/react-query';
import { checkPartNoExists, registerDrawing } from '../../api/drawing';
import CompanySearchField, { type CompanySearchSelection } from '../CompanySearchField';
import { DRAWING_PDF_MAX_SIZE_LABEL, validateDrawingPdfFile } from '../../utils/drawingUpload';

interface DrawingUploadModalProps {
  open: boolean;
  onClose: () => void;
  actorUserId?: string;
  onSuccess: (message: string) => void;
  onError: (message: string) => void;
}

export default function DrawingUploadModal({
  open,
  onClose,
  actorUserId,
  onSuccess,
  onError,
}: DrawingUploadModalProps) {
  const queryClient = useQueryClient();
  const [isDragging, setIsDragging] = useState(false);
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [selectedPartner, setSelectedPartner] = useState<CompanySearchSelection | null>(null);
  const [partNo, setPartNo] = useState('');
  const [partName, setPartName] = useState('');
  const [modelType, setModelType] = useState('');
  const [isUploading, setIsUploading] = useState(false);
  const [partNoError, setPartNoError] = useState<string | null>(null);
  const [isCheckingPartNo, setIsCheckingPartNo] = useState(false);
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
    parseFileName(file);
  };

  const parseFileName = (file: File) => {
    const regex = /^([^_]+)_([^_]+)_([^_]+)_(DEV|PROD)\.pdf$/i;
    const match = file.name.match(regex);
    if (match) {
      setPartNo(match[1]);
      setPartName(match[2]);
      setModelType(match[3]);
      setPartNoError(null);
    } else {
      setPartNo('');
      setPartName('');
      setModelType('');
    }
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
        const file = files[0];
        selectPdfFile(file);
      }
    },
    [onError],
  );

  if (!open) {
    return null;
  }

  const handlePartNoBlur = async () => {
    const trimmed = partNo.trim();
    if (!trimmed) {
      setPartNoError(null);
      return;
    }
    setIsCheckingPartNo(true);
    try {
      const exists = await checkPartNoExists(trimmed);
      setPartNoError(exists ? '이미 등록된 품번입니다. 개정 메뉴를 이용해 주세요.' : null);
    } catch {
      setPartNoError(null);
    } finally {
      setIsCheckingPartNo(false);
    }
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files;
    if (files && files.length > 0) {
      selectPdfFile(files[0]);
    }
  };

  const handleUpload = async () => {
    if (!selectedFile || !partNo || partNoError) {
      return;
    }

    setIsUploading(true);
    try {
      await registerDrawing(
        {
          partNo,
          partName,
          modelType,
          sourcePartnerId: selectedPartner?.id ?? null,
          drawingType: 'DEV',
        },
        selectedFile,
        actorUserId,
      );
      await queryClient.invalidateQueries({ queryKey: ['drawings'] });
      onSuccess('도면이 성공적으로 등록되었습니다.');
      handleClose();
    } catch (error) {
      onError(error instanceof Error ? error.message : '등록에 실패했습니다.');
    } finally {
      setIsUploading(false);
    }
  };

  const handleClose = () => {
    if (isUploading) {
      return;
    }
    setSelectedFile(null);
    setSelectedPartner(null);
    setPartNo('');
    setPartName('');
    setModelType('');
    setPartNoError(null);
    setFileError(null);
    onClose();
  };

  return (
    <div className="modal-backdrop" role="presentation" onClick={handleClose}>
      <div className="modal modal-wide" role="dialog" onClick={(e) => e.stopPropagation()}>
        <div className="modal-header-row">
          <h2>신규 도면 등록</h2>
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
          <UploadCloud className="import-upload-icon" size={48} />
          <p className="import-dropzone__title">이곳에 PDF 파일을 드래그 앤 드롭하세요</p>
          <p className="import-dropzone__hint">
            PDF 1개, {DRAWING_PDF_MAX_SIZE_LABEL} 이하 · 파일명 규칙: [품번]_[품명]_[기종]_DEV.pdf
          </p>
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

        <div style={{ margin: '1rem 0' }}>
          <CompanySearchField
            label="선수신 거래처 (선택)"
            selectedCompany={selectedPartner}
            onSelect={setSelectedPartner}
            placeholder="거래처명 또는 사업자번호로 검색"
          />
          <p className="hint" style={{ marginTop: '0.5rem' }}>
            거래처로부터 선수신한 도면인 경우 출처 거래처를 지정합니다. 등록 시 업무 단계는 <strong>선수신</strong>으로
            시작하며, 품목 연결은 양산 이관 후 별도 메뉴에서 진행합니다.
          </p>
        </div>

        <div className="form-grid form-grid-wide">
          <label>
            품번 *
            <input
              value={partNo}
              onChange={(e) => {
                setPartNo(e.target.value);
                setPartNoError(null);
              }}
              onBlur={() => void handlePartNoBlur()}
            />
            {(partNoError || isCheckingPartNo) && (
              <span className="hint" style={{ color: partNoError ? '#b91c1c' : '#6b7280' }}>
                {isCheckingPartNo ? '품번 확인 중…' : partNoError}
              </span>
            )}
          </label>
          <label>
            품명 *
            <input value={partName} onChange={(e) => setPartName(e.target.value)} />
          </label>
          <label>
            기종 *
            <input value={modelType} onChange={(e) => setModelType(e.target.value)} />
          </label>
        </div>

        <p className="hint">신규 등록은 개발품(DEV)으로만 가능합니다. 양산 이관은 개발 완료 후 상세 화면에서 진행합니다.</p>

        <div className="form-actions">
          <button type="button" className="secondary" onClick={handleClose} disabled={isUploading}>
            취소
          </button>
          <button
            type="button"
            disabled={!selectedFile || !partNo || !!partNoError || !!fileError || isUploading || isCheckingPartNo}
            onClick={() => void handleUpload()}
          >
            {isUploading ? '등록 중…' : '등록하기'}
          </button>
        </div>
      </div>
    </div>
  );
}
