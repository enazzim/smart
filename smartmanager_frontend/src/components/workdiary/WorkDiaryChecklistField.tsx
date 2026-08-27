import type {
  WorkDiaryChecklistEntry,
  WorkDiaryChecklistValue,
  WorkDiaryFieldDefinition,
} from '../../api/workDiary';
import { DEFAULT_CHECKLIST_OPTIONS } from '../../api/workDiary';
import {
  checklistItemsSorted,
  formatChecklistEntry,
  orphanedChecklistEntries,
} from './workDiaryFieldUtils';

interface WorkDiaryChecklistFieldProps {
  field: WorkDiaryFieldDefinition;
  value: WorkDiaryChecklistValue;
  readOnly?: boolean;
  onChange?: (next: WorkDiaryChecklistValue) => void;
}

export default function WorkDiaryChecklistField({
  field,
  value,
  readOnly = false,
  onChange,
}: WorkDiaryChecklistFieldProps) {
  const options = field.options?.length ? field.options : [...DEFAULT_CHECKLIST_OPTIONS];
  const items = checklistItemsSorted(field.items ?? []);
  const orphans = orphanedChecklistEntries(field, value);

  const updateEntry = (itemId: string, patch: Partial<WorkDiaryChecklistEntry>) => {
    if (!onChange) return;
    const current = value[itemId] ?? { status: options[0], note: '' };
    onChange({
      ...value,
      [itemId]: {
        status: patch.status ?? current.status,
        note: patch.note ?? current.note ?? '',
      },
    });
  };

  let lastGroup = '';

  return (
    <div className="work-diary-checklist">
      <table className="work-diary-checklist-table">
        <thead>
          <tr>
            <th>점검 항목</th>
            {options.map((option) => (
              <th key={option}>{option}</th>
            ))}
            <th>비고</th>
          </tr>
        </thead>
        <tbody>
          {items.length === 0 && (
            <tr>
              <td colSpan={options.length + 2} className="meta-text">
                등록된 체크리스트 항목이 없습니다. 그룹 양식에서 항목을 추가해 주세요.
              </td>
            </tr>
          )}
          {items.map((item) => {
            const showGroup = item.group && item.group !== lastGroup;
            if (showGroup) lastGroup = item.group ?? '';
            const entry = value[item.id] ?? { status: options[0], note: '' };
            return (
              <tr key={item.id}>
                <td>
                  {showGroup && <div className="work-diary-checklist-group">{item.group}</div>}
                  <div>{item.text}</div>
                </td>
                {options.map((option) => (
                  <td key={`${item.id}-${option}`} className="work-diary-checklist-choice">
                    {readOnly ? (
                      entry.status === option ? '●' : ''
                    ) : (
                      <label>
                        <input
                          type="radio"
                          name={`checklist-${field.key}-${item.id}`}
                          checked={entry.status === option}
                          onChange={() => updateEntry(item.id, { status: option })}
                        />
                      </label>
                    )}
                  </td>
                ))}
                <td className="work-diary-checklist-note">
                  {readOnly ? (
                    <div className="work-diary-checklist-note-text">{entry.note?.trim() ? entry.note : '—'}</div>
                  ) : (
                    <textarea
                      value={entry.note ?? ''}
                      onChange={(e) => updateEntry(item.id, { note: e.target.value })}
                      placeholder="비고"
                      rows={3}
                    />
                  )}
                </td>
              </tr>
            );
          })}
          {orphans.map(({ id, entry }) => (
            <tr key={`orphan-${id}`} className="work-diary-checklist-orphan">
              <td>
                <div className="meta-text">(템플릿에서 제거된 항목)</div>
                <div>{id}</div>
              </td>
              <td colSpan={options.length + 1}>{formatChecklistEntry(entry)}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export function renderChecklistSummary(field: WorkDiaryFieldDefinition, value: WorkDiaryChecklistValue): string {
  const items = checklistItemsSorted(field.items ?? []);
  if (items.length === 0) return '—';
  return items
    .map((item) => {
      const entry = value[item.id];
      return `${item.text}: ${formatChecklistEntry(entry)}`;
    })
    .join('\n');
}
