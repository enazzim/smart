import { useCallback, useMemo, useRef, useState } from 'react';
import type { BulkImportResult } from '../api/masterImport';
import {
  downloadImportTemplate,
  IMPORT_DOMAINS,
  parseImportExcel,
  uploadImportDomain,
  validateImportRows,
  type ImportDomain,
  type ImportDomainConfig,
} from '../utils/masterImportExcel';

type StepStatus = 'idle' | 'ready' | 'uploading' | 'done' | 'error';

interface StagedFile {
  file: File;
  rows: Record<string, unknown>[];
  parseErrors: { rowNumber: number; message: string }[];
}

interface DomainUploadResult {
  domain: ImportDomain;
  label: string;
  result?: BulkImportResult;
  error?: string;
}

function UploadIcon() {
  return (
    <svg className="import-upload-icon" viewBox="0 0 24 24" aria-hidden="true">
      <path
        d="M12 16V4m0 0L7 9m5-5 5 5M4 20h16"
        fill="none"
        stroke="currentColor"
        strokeWidth="1.8"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  );
}

function ImportUploadCard({
  config,
  staged,
  status,
  disabled,
  onFile,
  onClear,
}: {
  config: ImportDomainConfig;
  staged: StagedFile | null;
  status: StepStatus;
  disabled: boolean;
  onFile: (file: File) => void;
  onClear: () => void;
}) {
  const inputRef = useRef<HTMLInputElement>(null);
  const [dragOver, setDragOver] = useState(false);

  const handleFiles = (files: FileList | null) => {
    const file = files?.[0];
    if (file) {
      onFile(file);
    }
  };

  return (
    <article className={`import-card import-card--${status}`}>
      <div className="import-card__header">
        <h3>
          {config.order}. {config.uploadTitle}
        </h3>
        <button
          type="button"
          className="import-template-btn"
          disabled={disabled}
          onClick={() => downloadImportTemplate(config.id)}
        >
          ↓ 양식 다운로드
        </button>
      </div>

      <div
        className={`import-dropzone${dragOver ? ' import-dropzone--active' : ''}${staged ? ' import-dropzone--filled' : ''}`}
        role="button"
        tabIndex={0}
        onClick={() => !disabled && inputRef.current?.click()}
        onKeyDown={(e) => {
          if (!disabled && (e.key === 'Enter' || e.key === ' ')) {
            e.preventDefault();
            inputRef.current?.click();
          }
        }}
        onDragOver={(e) => {
          e.preventDefault();
          if (!disabled) setDragOver(true);
        }}
        onDragLeave={() => setDragOver(false)}
        onDrop={(e) => {
          e.preventDefault();
          setDragOver(false);
          if (!disabled) handleFiles(e.dataTransfer.files);
        }}
      >
        <input
          ref={inputRef}
          type="file"
          accept=".xlsx,.xls,.csv"
          hidden
          disabled={disabled}
          onChange={(e) => handleFiles(e.target.files)}
        />
        <UploadIcon />
        {staged ? (
          <div className="import-dropzone__file">
            <strong>{staged.file.name}</strong>
            <span>{staged.rows.length}행</span>
            {!disabled && (
              <button
                type="button"
                className="import-clear-btn"
                onClick={(e) => {
                  e.stopPropagation();
                  onClear();
                }}
              >
                제거
              </button>
            )}
          </div>
        ) : (
          <>
            <p className="import-dropzone__title">{config.uploadTitle}</p>
            <p className="import-dropzone__hint">클릭하거나 파일을 여기로 드래그하세요 (.xlsx, .csv)</p>
          </>
        )}
      </div>

      {staged && staged.parseErrors.length > 0 && (
        <ul className="import-card__errors">
          {staged.parseErrors.slice(0, 3).map((err) => (
            <li key={`${err.rowNumber}-${err.message}`}>
              {err.rowNumber > 0 ? `${err.rowNumber}행: ` : ''}
              {err.message}
            </li>
          ))}
        </ul>
      )}
    </article>
  );
}

export default function MasterImportPage() {
  const [stagedByDomain, setStagedByDomain] = useState<Partial<Record<ImportDomain, StagedFile>>>({});
  const [stepStatus, setStepStatus] = useState<Partial<Record<ImportDomain, StepStatus>>>({});
  const [submitting, setSubmitting] = useState(false);
  const [uploadResults, setUploadResults] = useState<DomainUploadResult[]>([]);
  const [message, setMessage] = useState<string | null>(null);

  const stagedCount = useMemo(
    () => IMPORT_DOMAINS.filter((d) => stagedByDomain[d.id]?.rows.length).length,
    [stagedByDomain],
  );

  const canStart = stagedCount > 0 && !submitting;

  const stageFile = useCallback(async (domain: ImportDomain, file: File) => {
    try {
      const buffer = await file.arrayBuffer();
      const rows = parseImportExcel(domain, buffer);
      const parseErrors = validateImportRows(domain, rows);
      setStagedByDomain((prev) => ({
        ...prev,
        [domain]: { file, rows, parseErrors },
      }));
      setStepStatus((prev) => ({
        ...prev,
        [domain]: rows.length > 0 && parseErrors.length === 0 ? 'ready' : 'error',
      }));
      setUploadResults([]);
      setMessage(null);
    } catch (e) {
      setStagedByDomain((prev) => ({
        ...prev,
        [domain]: {
          file,
          rows: [],
          parseErrors: [{ rowNumber: 0, message: e instanceof Error ? e.message : '엑셀 파싱 실패' }],
        },
      }));
      setStepStatus((prev) => ({ ...prev, [domain]: 'error' }));
    }
  }, []);

  const clearFile = useCallback((domain: ImportDomain) => {
    setStagedByDomain((prev) => {
      const next = { ...prev };
      delete next[domain];
      return next;
    });
    setStepStatus((prev) => ({ ...prev, [domain]: 'idle' }));
  }, []);

  const onStartUpload = async () => {
    setSubmitting(true);
    setMessage(null);
    setUploadResults([]);
    const results: DomainUploadResult[] = [];

    for (const domain of IMPORT_DOMAINS) {
      const staged = stagedByDomain[domain.id];
      if (!staged || staged.rows.length === 0) {
        continue;
      }
      if (staged.parseErrors.length > 0) {
        results.push({
          domain: domain.id,
          label: domain.uploadTitle,
          error: '파일 검증 오류를 먼저 수정해 주세요.',
        });
        setStepStatus((prev) => ({ ...prev, [domain.id]: 'error' }));
        continue;
      }

      setStepStatus((prev) => ({ ...prev, [domain.id]: 'uploading' }));
      try {
        const bulkResult = await uploadImportDomain(domain.id, staged.rows);
        results.push({ domain: domain.id, label: domain.uploadTitle, result: bulkResult });
        setStepStatus((prev) => ({
          ...prev,
          [domain.id]: bulkResult.failureCount > 0 ? 'error' : 'done',
        }));
      } catch (e) {
        results.push({
          domain: domain.id,
          label: domain.uploadTitle,
          error: e instanceof Error ? e.message : '일괄 등록 실패',
        });
        setStepStatus((prev) => ({ ...prev, [domain.id]: 'error' }));
      }
    }

    setUploadResults(results);
    const totalSuccess = results.reduce((sum, r) => sum + (r.result?.successCount ?? 0), 0);
    const totalFail = results.reduce(
      (sum, r) => sum + (r.result?.failureCount ?? 0) + (r.error ? 1 : 0),
      0,
    );
    setMessage(`업로드 완료 — 성공 ${totalSuccess}건, 실패 ${totalFail}건`);
    setSubmitting(false);
  };

  return (
    <div className="page master-import-page">
      <header className="master-import-header">
        <h1>데이터 일괄 업로드</h1>
        <p>엑셀/CSV 파일을 이용해 기준정보를 한 번에 등록합니다.</p>
        <p className="import-order-note">
          <strong>※ 업로드 순서:</strong> 거래처 → 품목 → 품목구성 → 작업장 → 공정 → 작업표준 → 단가 순으로
          등록해야 데이터 무결성이 유지됩니다.
        </p>
      </header>

      <ol className="import-steps" aria-label="업로드 단계">
        {IMPORT_DOMAINS.map((domain) => {
          const status = stepStatus[domain.id] ?? (stagedByDomain[domain.id] ? 'ready' : 'idle');
          return (
            <li key={domain.id} className={`import-step import-step--${status}`}>
              <span className="import-step__badge">{domain.order}</span>
              <span className="import-step__label">{domain.label.replace(/^[①-⑦]\s*/, '')}</span>
            </li>
          );
        })}
      </ol>

      <div className="import-card-grid">
        {IMPORT_DOMAINS.map((domain) => (
          <ImportUploadCard
            key={domain.id}
            config={domain}
            staged={stagedByDomain[domain.id] ?? null}
            status={stepStatus[domain.id] ?? (stagedByDomain[domain.id] ? 'ready' : 'idle')}
            disabled={submitting}
            onFile={(file) => void stageFile(domain.id, file)}
            onClear={() => clearFile(domain.id)}
          />
        ))}
      </div>

      <div className="import-start-wrap">
        <button
          type="button"
          className="import-start-btn"
          disabled={!canStart}
          onClick={() => void onStartUpload()}
        >
          {submitting ? '업로드 중…' : '일괄 업로드 시작'}
        </button>
      </div>

      <aside className="import-caution">
        <h2>업로드 주의사항</h2>
        <ul>
          <li>각 항목별 <strong>양식 다운로드</strong> 후 데이터를 입력해 주세요.</li>
          <li>엑셀 파일의 <strong>첫 번째 행(헤더)</strong>은 수정하지 마세요.</li>
          <li>실제 데이터는 <strong>두 번째 행(샘플) 다음</strong>부터 입력하거나, 샘플 행을 삭제 후 입력하세요.</li>
          <li>FK가 필요한 항목은 앞 단계 데이터가 먼저 등록되어 있어야 합니다.</li>
        </ul>
      </aside>

      {message && <p className="import-summary">{message}</p>}

      {uploadResults.length > 0 && (
        <section className="panel import-results">
          <h2>업로드 결과</h2>
          {uploadResults.map((item) => (
            <div key={item.domain} className="import-result-block">
              <h3>{item.label}</h3>
              {item.error && <p className="error">{item.error}</p>}
              {item.result && (
                <p>
                  성공 {item.result.successCount}건 / 실패 {item.result.failureCount}건
                </p>
              )}
              {item.result && item.result.failures.length > 0 && (
                <ul className="import-card__errors">
                  {item.result.failures.map((f) => (
                    <li key={`${f.rowIndex}-${f.message}`}>
                      {f.rowIndex + 2}행 ({f.key}): {f.message}
                    </li>
                  ))}
                </ul>
              )}
            </div>
          ))}
        </section>
      )}
    </div>
  );
}
