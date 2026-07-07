import { apiFetch, handleResponse } from './http';

export type WorkDiaryStatus = 'DRAFT' | 'SUBMITTED' | 'APPROVED' | 'REJECTED';

export const WORK_DIARY_STATUS_LABELS: Record<WorkDiaryStatus, string> = {
  DRAFT: '임시저장',
  SUBMITTED: '제출',
  APPROVED: '결재완료',
  REJECTED: '반려',
};

export interface WorkDiaryFieldSchema {
  legacyFields?: Record<string, string>;
}

export interface WorkDiaryTemplate {
  templateCode: string;
  workDiaryGroupId: number;
  workDiaryGroupName: string;
  templateName: string;
  fieldSchema: WorkDiaryFieldSchema;
}

export interface WorkDiaryListItem {
  id: number;
  workDate: string;
  workDateTitle: string;
  authorUserId: number;
  authorName: string;
  workDiaryGroupId: number;
  workDiaryGroupName: string;
  templateCode: string;
  status: WorkDiaryStatus;
  listed: boolean;
  approvedAt?: string | null;
  directiveNotePreview?: string | null;
}

export interface WorkDiaryDetail extends WorkDiaryListItem {
  templateName: string;
  fieldSchema: WorkDiaryFieldSchema;
  fieldValues: Record<string, string>;
  directiveNote?: string | null;
  closingNote?: string | null;
  submittedAt?: string | null;
  approvedByUserId?: number | null;
  approvedByName?: string | null;
  approvalCanceledAt?: string | null;
  approvalCanceledByUserId?: number | null;
  approvalCanceledByName?: string | null;
  canEdit: boolean;
  canDelete: boolean;
  canApprove: boolean;
  canCancelApproval: boolean;
  createdAt: string;
  updatedAt?: string | null;
}

export interface WorkDiaryPage {
  items: WorkDiaryListItem[];
  totalElements: number;
  page: number;
  size: number;
}

export interface WorkDiaryListParams {
  fromDate?: string;
  toDate?: string;
  status?: WorkDiaryStatus;
  authorUserId?: number;
  page?: number;
  size?: number;
}

function buildQuery(params?: WorkDiaryListParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.fromDate) search.set('fromDate', params.fromDate);
  if (params.toDate) search.set('toDate', params.toDate);
  if (params.status) search.set('status', params.status);
  if (params.authorUserId != null) search.set('authorUserId', String(params.authorUserId));
  if (params.page != null) search.set('page', String(params.page));
  if (params.size != null) search.set('size', String(params.size));
  const q = search.toString();
  return q ? `?${q}` : '';
}

export function parseLegacyFields(schema?: WorkDiaryFieldSchema): Array<{ key: string; label: string }> {
  const legacyFields = schema?.legacyFields ?? {};
  return Object.entries(legacyFields)
    .filter(([, label]) => label.trim() !== '')
    .sort(([a], [b]) => a.localeCompare(b, undefined, { numeric: true }))
    .map(([key, label]) => ({ key, label }));
}

export const DIRECTIVE_FIELD_KEY = '06';

export function isDirectiveWriterField(key: string, label: string): boolean {
  return key === DIRECTIVE_FIELD_KEY || /지시\s*사항/.test(label);
}

/** 작성자 입력 항목 — 레거시 지시사항(06) 제외 */
export function parseWriterFields(schema?: WorkDiaryFieldSchema): Array<{ key: string; label: string }> {
  return parseLegacyFields(schema).filter(({ key, label }) => !isDirectiveWriterField(key, label));
}

export function emptyFieldValues(schema?: WorkDiaryFieldSchema): Record<string, string> {
  const values: Record<string, string> = {};
  parseWriterFields(schema).forEach(({ key }) => {
    values[key] = '';
  });
  return values;
}

export function legacyFieldsFromSchema(schema?: WorkDiaryFieldSchema): Record<string, string> {
  const values: Record<string, string> = {};
  parseWriterFields(schema).forEach(({ key, label }) => {
    values[key] = label;
  });
  return values;
}

export async function updateWorkDiaryTemplate(
  workDiaryGroupId: number,
  body: { templateName: string; legacyFields: Record<string, string> },
): Promise<WorkDiaryTemplate> {
  return handleResponse(
    await apiFetch(`/api/v1/work-diaries/templates/${workDiaryGroupId}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body),
    }),
  );
}

export async function fetchMyWorkDiaryTemplate(): Promise<WorkDiaryTemplate> {
  return handleResponse(await apiFetch('/api/v1/work-diaries/my-template'));
}

export async function fetchWorkDiaryTemplates(): Promise<WorkDiaryTemplate[]> {
  return handleResponse(await apiFetch('/api/v1/work-diaries/templates'));
}

export async function fetchWorkDiaries(params?: WorkDiaryListParams): Promise<WorkDiaryPage> {
  return handleResponse(await apiFetch(`/api/v1/work-diaries${buildQuery(params)}`));
}

export async function fetchWorkDiary(id: number): Promise<WorkDiaryDetail> {
  return handleResponse(await apiFetch(`/api/v1/work-diaries/${id}`));
}

export async function fetchWorkDiaryByDate(workDate: string): Promise<WorkDiaryDetail> {
  return handleResponse(await apiFetch(`/api/v1/work-diaries/by-date/${workDate}`));
}

export async function createWorkDiary(body: {
  workDate: string;
  fieldValues: Record<string, string>;
  closingNote?: string;
  status: WorkDiaryStatus;
}): Promise<WorkDiaryDetail> {
  return handleResponse(
    await apiFetch('/api/v1/work-diaries', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ ...body, listed: true }),
    }),
  );
}

export async function updateWorkDiary(
  id: number,
  body: {
    fieldValues: Record<string, string>;
    closingNote?: string;
  },
): Promise<WorkDiaryDetail> {
  return handleResponse(
    await apiFetch(`/api/v1/work-diaries/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ ...body, listed: true }),
    }),
  );
}

export async function deleteWorkDiary(id: number): Promise<void> {
  await handleResponse(
    await apiFetch(`/api/v1/work-diaries/${id}`, { method: 'DELETE' }),
  );
}

export async function submitWorkDiary(id: number, comment?: string): Promise<void> {
  await handleResponse(
    await apiFetch(`/api/v1/work-diaries/${id}/submit`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ comment: comment ?? '' }),
    }),
  );
}

export async function approveWorkDiary(id: number, directiveNote?: string): Promise<void> {
  await handleResponse(
    await apiFetch(`/api/v1/work-diaries/${id}/approve`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(
        directiveNote != null && directiveNote !== '' ? { directiveNote } : {},
      ),
    }),
  );
}

export async function cancelWorkDiaryApproval(id: number, reason?: string): Promise<void> {
  await handleResponse(
    await apiFetch(`/api/v1/work-diaries/${id}/cancel-approval`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(reason != null && reason !== '' ? { reason } : {}),
    }),
  );
}

export function formatWorkDiaryDate(value?: string | null): string {
  if (!value) return '—';
  return value.slice(0, 10);
}
