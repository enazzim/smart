import { useEffect, useState } from 'react';
import type { WorkDiaryFieldSchema, WorkDiaryTemplate } from '../../api/workDiary';
import { parseSchemaFields, updateWorkDiaryTemplate } from '../../api/workDiary';
import {
  type EditorFieldRow,
  editorRowsToFields,
  fieldToEditorRow,
  fieldsFromSchema,
  newChecklistItemId,
  nextFieldKey,
} from './workDiaryFieldUtils';

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
  const fields = parseSchemaFields(fieldSchema);

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
          fields.map((field) => (
            <li key={field.key}>
              {field.label}
              {field.type === 'checklist' && (
                <span className="meta-text"> — 체크리스트 {field.items?.length ?? 0}항목</span>
              )}
            </li>
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
  const [rows, setRows] = useState<EditorFieldRow[]>(() =>
    fieldsFromSchema(template.fieldSchema).map(fieldToEditorRow),
  );
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    setTemplateName(template.templateName);
    setRows(fieldsFromSchema(template.fieldSchema).map(fieldToEditorRow));
  }, [template]);

  const save = async () => {
    setSubmitting(true);
    setError(null);
    const fields = editorRowsToFields(rows);
    if (fields.length === 0) {
      setError('입력 항목을 1개 이상 등록해 주세요.');
      setSubmitting(false);
      return;
    }
    try {
      const updated = await updateWorkDiaryTemplate(template.workDiaryGroupId, {
        templateName: templateName.trim(),
        fields,
      });
      onSaved(updated);
      onClose();
    } catch (e) {
      setError(e instanceof Error ? e.message : '양식 저장에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const updateRow = (index: number, patch: Partial<EditorFieldRow>) => {
    setRows((prev) => prev.map((row, rowIndex) => (rowIndex === index ? { ...row, ...patch } : row)));
  };

  const addFieldRow = () => {
    setRows((prev) => [
      ...prev,
      {
        key: nextFieldKey(prev.map((row) => row.key)),
        label: '',
        type: 'textarea',
        options: ['이상무', '이상있음'],
        items: [],
      },
    ]);
  };

  const addChecklistItem = (fieldIndex: number) => {
    setRows((prev) =>
      prev.map((row, rowIndex) => {
        if (rowIndex !== fieldIndex) return row;
        const nextSort = row.items.length > 0 ? Math.max(...row.items.map((item) => item.sortOrder)) + 1 : 1;
        return {
          ...row,
          items: [
            ...row.items,
            { id: newChecklistItemId(), group: '', text: '', sortOrder: nextSort },
          ],
        };
      }),
    );
  };

  const updateChecklistItem = (
    fieldIndex: number,
    itemIndex: number,
    patch: Partial<EditorFieldRow['items'][number]>,
  ) => {
    setRows((prev) =>
      prev.map((row, rowIndex) => {
        if (rowIndex !== fieldIndex) return row;
        return {
          ...row,
          items: row.items.map((item, currentIndex) =>
            currentIndex === itemIndex ? { ...item, ...patch } : item,
          ),
        };
      }),
    );
  };

  const removeChecklistItem = (fieldIndex: number, itemIndex: number) => {
    setRows((prev) =>
      prev.map((row, rowIndex) => {
        if (rowIndex !== fieldIndex) return row;
        return { ...row, items: row.items.filter((_, currentIndex) => currentIndex !== itemIndex) };
      }),
    );
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
            <button type="button" className="secondary" onClick={addFieldRow}>
              항목 추가
            </button>
          </div>

          {rows.map((row, index) => (
            <article key={`${row.key}-${index}`} className="work-diary-template-editor-block">
              <div className="work-diary-template-editor-row">
                <label>
                  키
                  <input
                    value={row.key}
                    onChange={(e) => updateRow(index, { key: e.target.value })}
                    maxLength={2}
                  />
                </label>
                <label className="work-diary-field">
                  항목명
                  <input
                    value={row.label}
                    onChange={(e) => updateRow(index, { label: e.target.value })}
                    maxLength={200}
                  />
                </label>
                <label>
                  유형
                  <select
                    value={row.type}
                    onChange={(e) =>
                      updateRow(index, {
                        type: e.target.value as EditorFieldRow['type'],
                        items: e.target.value === 'checklist' ? row.items : [],
                      })
                    }
                  >
                    <option value="textarea">자유 입력</option>
                    <option value="checklist">체크리스트</option>
                  </select>
                </label>
                <button
                  type="button"
                  className="secondary"
                  disabled={rows.length <= 1}
                  onClick={() => setRows((prev) => prev.filter((_, rowIndex) => rowIndex !== index))}
                >
                  삭제
                </button>
              </div>

              {row.type === 'checklist' && (
                <div className="work-diary-template-checklist-editor">
                  <div className="inline-actions">
                    <h4>체크리스트 항목</h4>
                    <button type="button" className="secondary" onClick={() => addChecklistItem(index)}>
                      체크 항목 추가
                    </button>
                  </div>
                  {row.items.length === 0 && (
                    <p className="meta-text">체크리스트 항목을 추가해 주세요.</p>
                  )}
                  {row.items.map((item, itemIndex) => (
                    <div key={`${item.id}-${itemIndex}`} className="work-diary-template-checklist-item-row">
                      <label>
                        그룹
                        <input
                          value={item.group}
                          onChange={(e) => updateChecklistItem(index, itemIndex, { group: e.target.value })}
                          maxLength={100}
                          placeholder="예: 전기"
                        />
                      </label>
                      <label className="work-diary-field">
                        점검 내용
                        <input
                          value={item.text}
                          onChange={(e) => updateChecklistItem(index, itemIndex, { text: e.target.value })}
                          maxLength={500}
                        />
                      </label>
                      <button
                        type="button"
                        className="secondary"
                        onClick={() => removeChecklistItem(index, itemIndex)}
                      >
                        삭제
                      </button>
                    </div>
                  ))}
                </div>
              )}
            </article>
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
                  {parseSchemaFields(template.fieldSchema).map((field) => (
                    <li key={field.key}>
                      {field.label}
                      {field.type === 'checklist' ? ` (${field.items?.length ?? 0})` : ''}
                    </li>
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
