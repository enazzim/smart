import type {
  WorkDiaryChecklistItem,
  WorkDiaryFieldDefinition,
  WorkDiaryFieldValues,
  WorkDiaryChecklistEntry,
  WorkDiaryChecklistValue,
} from '../../api/workDiary';
import { DEFAULT_CHECKLIST_OPTIONS, parseSchemaFields } from '../../api/workDiary';

export const fieldsFromSchema = parseSchemaFields;

export function newChecklistItemId(): string {
  return `chk-${crypto.randomUUID().slice(0, 8)}`;
}

export function nextFieldKey(existing: string[]): string {
  const numeric = existing
    .map((key) => Number.parseInt(key, 10))
    .filter((value) => !Number.isNaN(value));
  const next = numeric.length > 0 ? Math.max(...numeric) + 1 : 1;
  return String(next).padStart(2, '0');
}

export function emptyFieldValues(fields: WorkDiaryFieldDefinition[]): WorkDiaryFieldValues {
  const values: WorkDiaryFieldValues = {};
  for (const field of fields) {
    if (field.type === 'checklist') {
      const checklist: WorkDiaryChecklistValue = {};
      const options = field.options?.length ? field.options : [...DEFAULT_CHECKLIST_OPTIONS];
      for (const item of field.items ?? []) {
        checklist[item.id] = { status: options[0], note: '' };
      }
      values[field.key] = checklist;
    } else {
      values[field.key] = '';
    }
  }
  return values;
}

export function mergeFieldValues(
  fields: WorkDiaryFieldDefinition[],
  existing?: WorkDiaryFieldValues,
): WorkDiaryFieldValues {
  const base = emptyFieldValues(fields);
  if (!existing) return base;
  for (const field of fields) {
    const current = existing[field.key];
    if (field.type === 'checklist' && current && typeof current === 'object' && !Array.isArray(current)) {
      const merged: WorkDiaryChecklistValue = { ...(base[field.key] as WorkDiaryChecklistValue) };
      for (const [itemId, entry] of Object.entries(current as WorkDiaryChecklistValue)) {
        if (entry && typeof entry === 'object') {
          const checklistEntry = entry as WorkDiaryChecklistEntry;
          merged[itemId] = {
            status: checklistEntry.status ?? merged[itemId]?.status ?? DEFAULT_CHECKLIST_OPTIONS[0],
            note: checklistEntry.note ?? '',
          };
        }
      }
      base[field.key] = merged;
    } else if (typeof current === 'string') {
      base[field.key] = current;
    }
  }
  return base;
}

export function fieldToEditorRow(field: WorkDiaryFieldDefinition): EditorFieldRow {
  return {
    key: field.key,
    label: field.label,
    type: field.type ?? 'textarea',
    options: field.options?.length ? [...field.options] : [...DEFAULT_CHECKLIST_OPTIONS],
    items: (field.items ?? []).map((item) => ({
      ...item,
      group: item.group ?? '',
    })),
  };
}

export interface EditorChecklistItemRow {
  id: string;
  group: string;
  text: string;
  sortOrder: number;
}

export interface EditorFieldRow {
  key: string;
  label: string;
  type: 'textarea' | 'checklist';
  options: string[];
  items: EditorChecklistItemRow[];
}

export function editorRowsToFields(rows: EditorFieldRow[]): WorkDiaryFieldDefinition[] {
  return rows
    .filter((row) => row.key.trim() && row.label.trim())
    .map((row) => ({
      key: row.key.trim(),
      label: row.label.trim(),
      type: row.type,
      options: row.type === 'checklist' ? row.options.filter((opt) => opt.trim()) : undefined,
      items:
        row.type === 'checklist'
          ? row.items
              .filter((item) => item.id.trim() && item.text.trim())
              .map((item, itemIndex) => ({
                id: item.id.trim(),
                group: item.group.trim(),
                text: item.text.trim(),
                sortOrder: item.sortOrder > 0 ? item.sortOrder : itemIndex + 1,
              }))
          : undefined,
    }));
}

export function checklistItemsSorted(items: WorkDiaryChecklistItem[]): WorkDiaryChecklistItem[] {
  return [...items].sort((a, b) => {
    const order = (a.sortOrder ?? 0) - (b.sortOrder ?? 0);
    return order !== 0 ? order : a.text.localeCompare(b.text);
  });
}

export function formatChecklistEntry(entry?: WorkDiaryChecklistEntry | null): string {
  if (!entry) return '—';
  const note = entry.note?.trim();
  return note ? `${entry.status} (${note})` : entry.status;
}

export function orphanedChecklistEntries(
  field: WorkDiaryFieldDefinition,
  value?: WorkDiaryChecklistValue,
): Array<{ id: string; entry: WorkDiaryChecklistEntry }> {
  if (!value) return [];
  const known = new Set((field.items ?? []).map((item: WorkDiaryChecklistItem) => item.id));
  return Object.entries(value)
    .filter(([id]) => !known.has(id))
    .map(([id, entry]) => ({ id, entry }));
}

export function checklistValueOrEmpty(
  value: WorkDiaryFieldValues[string] | undefined,
): WorkDiaryChecklistValue {
  if (value && typeof value === 'object' && !Array.isArray(value)) {
    return value as WorkDiaryChecklistValue;
  }
  return {};
}

export function textareaValue(value: WorkDiaryFieldValues[string] | undefined): string {
  return typeof value === 'string' ? value : '';
}
