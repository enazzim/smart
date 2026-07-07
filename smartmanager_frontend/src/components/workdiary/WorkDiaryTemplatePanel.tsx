import { useEffect, useState } from 'react';
import type { WorkDiaryFieldSchema, WorkDiaryTemplate } from '../../api/workDiary';
import {
  legacyFieldsFromSchema,
  parseWriterFields,
  updateWorkDiaryTemplate,
} from '../../api/workDiary';

interface WorkDiaryTemplatePanelProps {
  templateName: string;
  workDiaryGroupName: string;
  fieldSchema?: WorkDiaryFieldSchema;
  compact?: boolean;
}

export default function WorkDiaryTemplatePanel({
  templateName,
  workDiaryGroupName,
  fieldSchema,
  compact = false,
}: WorkDiaryTemplatePanelProps) {
  const fields = parseWriterFields(fieldSchema);

  return (
    <section className={`detail-panel work-diary-template-panel${compact ? ' work-diary-template-panel--compact' : ''}`}>
      <h2>그룹 양식</h2>
      <dl className="detail-grid">
        <div>
          <dt>그룹</dt>
          <dd>{workDiaryGroupName || '—'}</dd>
        </div>
        <div>
          <dt>템플릿</dt>
          <dd>{templateName || '—'}</dd>
        </div>
      </dl>
      <ol className="work-diary-template-field-list">
        {fields.length === 0 ? (
          <li>등록된 입력 항목이 없습니다.</li>
        ) : (
          fields.map(({ key, label }) => (
            <li key={key}>{label}</li>
          ))
        )}
      </ol>
      <p className="meta-text">지시사항은 결재 시 사장님이 입력하는 항목이며 작성 양식에 포함되지 않습니다.</p>
    </section>
  );
}

interface WorkDiaryTemplatesCatalogProps {
  templates: WorkDiaryTemplate[];
  canManageTemplates: boolean;
  onClose: () => void;
  onTemplatesChange: (templates: WorkDiaryTemplate[]) => void;
}

function nextFieldKey(existing: string[]): string {
  const numeric = existing
    .map((key) => Number.parseInt(key, 10))
    .filter((value) => !Number.isNaN(value));
  const next = numeric.length > 0 ? Math.max(...numeric) + 1 : 1;
  return String(next).padStart(2, '0');
}

interface TemplateFieldRow {
  key: string;
  label: string;
}

function WorkDiaryTemplateEditor({
  template,
  onClose,
  onSaved,
}: {
  template: WorkDiaryTemplate;
  onClose: () => void;
  onSaved: (updated: WorkDiaryTemplate) => void;
}) {
  const [templateName, setTemplateName] = useState(template.templateName);
  const [rows, setRows] = useState<TemplateFieldRow[]>(() =>
    Object.entries(legacyFieldsFromSchema(template.fieldSchema)).map(([key, label]) => ({ key, label })),
  );
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    setTemplateName(template.templateName);
    setRows(
      Object.entries(legacyFieldsFromSchema(template.fieldSchema)).map(([key, label]) => ({ key, label })),
    );
  }, [template]);

  const save = async () => {
    setSubmitting(true);
    setError(null);
    const legacyFields: Record<string, string> = {};
    for (const row of rows) {
      const key = row.key.trim();
      const label = row.label.trim();
      if (!key || !label) continue;
      legacyFields[key] = label;
    }
    if (Object.keys(legacyFields).length === 0) {
      setError('입력 항목을 1개 이상 등록해 주세요.');
      setSubmitting(false);
      return;
    }
    try {
      const updated = await updateWorkDiaryTemplate(template.workDiaryGroupId, {
        templateName: templateName.trim(),
        legacyFields,
      });
      onSaved(updated);
      onClose();
    } catch (e) {
      setError(e instanceof Error ? e.message : '양식 저장에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="modal-backdrop" role="presentation" onClick={onClose}>
      <div
        className="modal-panel work-diary-template-editor-modal"
        role="dialog"
        aria-labelledby="work-diary-template-editor-title"
        onClick={(e) => e.stopPropagation()}
      >
        <header className="modal-header">
          <h2 id="work-diary-template-editor-title">{template.workDiaryGroupName} 양식 수정</h2>
          <button type="button" className="secondary" onClick={onClose}>
            닫기
          </button>
        </header>

        {error && <p className="error-banner">{error}</p>}

        <label className="work-diary-field">
          템플릿명
          <input value={templateName} onChange={(e) => setTemplateName(e.target.value)} maxLength={100} />
        </label>

        <section className="detail-panel">
          <div className="inline-actions">
            <h3>입력 항목</h3>
            <button
              type="button"
              className="secondary"
              onClick={() =>
                setRows((prev) => [...prev, { key: nextFieldKey(prev.map((row) => row.key)), label: '' }])
              }
            >
              항목 추가
            </button>
          </div>
          {rows.map((row, index) => (
            <div key={`${row.key}-${index}`} className="work-diary-template-editor-row">
              <label>
                키
                <input
                  value={row.key}
                  onChange={(e) =>
                    setRows((prev) =>
                      prev.map((item, itemIndex) =>
                        itemIndex === index ? { ...item, key: e.target.value } : item,
                      ),
                    )
                  }
                  maxLength={2}
                />
              </label>
              <label className="work-diary-field">
                항목명
                <input
                  value={row.label}
                  onChange={(e) =>
                    setRows((prev) =>
                      prev.map((item, itemIndex) =>
                        itemIndex === index ? { ...item, label: e.target.value } : item,
                      ),
                    )
                  }
                  maxLength={200}
                />
              </label>
              <button
                type="button"
                className="secondary"
                disabled={rows.length <= 1}
                onClick={() => setRows((prev) => prev.filter((_, itemIndex) => itemIndex !== index))}
              >
                삭제
              </button>
            </div>
          ))}
        </section>

        <p className="meta-text">지시사항(키 06·「지시사항」 문구)은 저장 시 자동 제외됩니다.</p>

        <div className="form-actions">
          <button type="button" disabled={submitting} onClick={() => void save()}>
            {submitting ? '저장 중…' : '저장'}
          </button>
          <button type="button" className="secondary" onClick={onClose}>
            취소
          </button>
        </div>
      </div>
    </div>
  );
}

export function WorkDiaryTemplatesCatalog({
  templates,
  canManageTemplates,
  onClose,
  onTemplatesChange,
}: WorkDiaryTemplatesCatalogProps) {
  const [editingTemplate, setEditingTemplate] = useState<WorkDiaryTemplate | null>(null);

  return (
    <>
      <div className="modal-backdrop" role="presentation" onClick={onClose}>
        <div
          className="modal-panel work-diary-templates-modal"
          role="dialog"
          aria-labelledby="work-diary-templates-title"
          onClick={(e) => e.stopPropagation()}
        >
          <header className="modal-header">
            <h2 id="work-diary-templates-title">업무일지 그룹 양식</h2>
            <button type="button" className="secondary" onClick={onClose}>
              닫기
            </button>
          </header>
          {canManageTemplates && (
            <p className="meta-text">시스템 관리자: 그룹 카드를 더블클릭하면 양식을 수정할 수 있습니다.</p>
          )}
          <div className="work-diary-templates-grid">
            {templates.map((template) => (
              <article
                key={`${template.workDiaryGroupId}-${template.templateCode}`}
                className={`work-diary-template-card${canManageTemplates ? ' work-diary-template-card--editable' : ''}`}
                onDoubleClick={() => {
                  if (canManageTemplates) setEditingTemplate(template);
                }}
                title={canManageTemplates ? '더블클릭하여 양식 수정' : undefined}
              >
                <h3>{template.workDiaryGroupName || '—'}</h3>
                <p className="meta-text">{template.templateName}</p>
                <ol>
                  {parseWriterFields(template.fieldSchema).map(({ key, label }) => (
                    <li key={key}>{label}</li>
                  ))}
                </ol>
              </article>
            ))}
          </div>
        </div>
      </div>
      {editingTemplate && (
        <WorkDiaryTemplateEditor
          template={editingTemplate}
          onClose={() => setEditingTemplate(null)}
          onSaved={(updated) => {
            onTemplatesChange(
              templates.map((item) =>
                item.workDiaryGroupId === updated.workDiaryGroupId ? updated : item,
              ),
            );
          }}
        />
      )}
    </>
  );
}
