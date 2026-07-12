import { apiFetch, getAccessToken, handleResponse } from './http';

export type DrawingType = 'DEV' | 'PROD';

export interface DrawingListItem {
  id: string;
  partNo: string;
  partName: string;
  modelGroup: string;
  itemId?: number | null;
  itemNo?: string | null;
  majorVersion: number;
  minorVersion: number;
  updatedAt: string;
  drawingType: DrawingType;
}

export interface DrawingHistoryItem {
  id: string;
  majorVersion: number;
  minorVersion: number;
  isLatest: string;
  changeType: string;
  changeReason: string;
  createdAt: string;
}

export interface DrawingRegisterPayload {
  partNo: string;
  partName: string;
  modelGroup: string;
  itemId?: number | null;
  drawingType: DrawingType;
}

export interface DrawingRevisePayload {
  changeType: string;
  changeReason: string;
}

export interface DrawingInfoUpdatePayload {
  partNo: string;
  partName: string;
  modelGroup: string;
  itemId?: number | null;
}

const API_BASE = '/api/v1/basis/drawings';

function actorHeaders(actorUserId?: string): HeadersInit | undefined {
  if (!actorUserId) {
    return undefined;
  }
  return { 'X-Actor-User-Id': actorUserId };
}

function appendJsonPart(formData: FormData, name: string, value: unknown): void {
  formData.append(name, new Blob([JSON.stringify(value)], { type: 'application/json' }));
}

async function parseActionError(response: Response, fallback: string): Promise<never> {
  const body = await response.json().catch(() => ({ message: fallback }));
  const message = typeof body.message === 'string' ? body.message : fallback;
  throw new Error(message);
}

export async function fetchDrawings(): Promise<DrawingListItem[]> {
  return handleResponse<DrawingListItem[]>(await apiFetch(API_BASE, { cache: 'no-store' }));
}

export async function fetchDeletedDrawings(): Promise<DrawingListItem[]> {
  return handleResponse<DrawingListItem[]>(
    await apiFetch(`${API_BASE}/deleted`, { cache: 'no-store' }),
  );
}

export async function checkPartNoExists(partNo: string): Promise<boolean> {
  const params = new URLSearchParams({ partNo });
  const result = await handleResponse<{ exists: boolean }>(
    await apiFetch(`${API_BASE}/check-part-no?${params.toString()}`),
  );
  return result.exists;
}

export async function fetchDrawingHistories(masterId: string): Promise<DrawingHistoryItem[]> {
  return handleResponse<DrawingHistoryItem[]>(await apiFetch(`${API_BASE}/${masterId}/history`));
}

export interface DrawingReferencePeer {
  historyId: string;
  masterId: string;
  partNo: string;
  partName: string;
  drawingType: DrawingType;
  majorVersion: number;
  minorVersion: number;
  itemId?: number | null;
  itemNo?: string | null;
}

export interface DrawingReferenceItem {
  id: string;
  parentHistoryId: string;
  childHistoryId: string;
  refRole: string;
  sortOrder: number;
  remark?: string | null;
  child: DrawingReferencePeer;
}

export interface DrawingWhereUsedItem {
  id: string;
  parentHistoryId: string;
  childHistoryId: string;
  refRole: string;
  sortOrder: number;
  remark?: string | null;
  parent: DrawingReferencePeer;
}

export interface DrawingReferenceChildPayload {
  childHistoryId: string;
  refRole?: string;
  sortOrder?: number;
  remark?: string | null;
}

export async function fetchDrawingReferences(
  masterId: string,
  historyId: string,
): Promise<DrawingReferenceItem[]> {
  return handleResponse<DrawingReferenceItem[]>(
    await apiFetch(`${API_BASE}/${masterId}/history/${historyId}/references`, { cache: 'no-store' }),
  );
}

export interface DrawingReferenceCandidate {
  masterId: string;
  historyId: string;
  partNo: string;
  partName: string;
  drawingType: DrawingType;
  majorVersion: number;
  minorVersion: number;
  itemId?: number | null;
  itemNo?: string | null;
  bomLevel: number;
}

export interface DrawingReferenceIntegrityIssue {
  code: string;
  parentPartNo: string;
  parentHistoryId: string;
  childPartNo?: string | null;
  childHistoryId?: string | null;
  message: string;
}

export async function fetchDrawingReferenceCandidates(
  masterId: string,
): Promise<DrawingReferenceCandidate[]> {
  return handleResponse<DrawingReferenceCandidate[]>(
    await apiFetch(`${API_BASE}/${masterId}/reference-candidates`, { cache: 'no-store' }),
  );
}

export async function fetchDrawingReferenceIntegrity(): Promise<DrawingReferenceIntegrityIssue[]> {
  return handleResponse<DrawingReferenceIntegrityIssue[]>(
    await apiFetch(`${API_BASE}/reference-integrity`, { cache: 'no-store' }),
  );
}

export async function fetchDrawingWhereUsed(historyId: string): Promise<DrawingWhereUsedItem[]> {
  return handleResponse<DrawingWhereUsedItem[]>(
    await apiFetch(`${API_BASE}/history/${historyId}/where-used`, { cache: 'no-store' }),
  );
}

export async function replaceDrawingReferences(
  masterId: string,
  historyId: string,
  children: DrawingReferenceChildPayload[],
  actorUserId?: string,
): Promise<void> {
  const response = await apiFetch(`${API_BASE}/${masterId}/history/${historyId}/references`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
      ...actorHeaders(actorUserId),
    },
    body: JSON.stringify({ children }),
  });
  if (!response.ok) {
    await parseActionError(response, '도면 구성 참조 저장에 실패했습니다.');
  }
}

export function drawingPdfUrl(partNo: string, historyId?: string): string {
  const params = new URLSearchParams();
  if (historyId) {
    // 이력별 고유 키 — 매 렌더 Date.now()를 넣으면 Document가 불필요하게 리마운트되며
    // 이력 전환 시 이전 PDF가 남는 증상이 난다.
    params.set('historyId', historyId);
  }
  const query = params.toString();
  return query
    ? `${API_BASE}/pdf/${encodeURIComponent(partNo)}?${query}`
    : `${API_BASE}/pdf/${encodeURIComponent(partNo)}`;
}

export async function fetchDrawingPdfBlob(url: string): Promise<Blob> {
  const response = await apiFetch(url);
  if (!response.ok) {
    await parseActionError(response, 'PDF를 불러오지 못했습니다.');
  }
  return response.blob();
}

export async function registerDrawing(
  payload: DrawingRegisterPayload,
  file: File,
  actorUserId?: string,
): Promise<void> {
  const formData = new FormData();
  formData.append('file', file);
  appendJsonPart(formData, 'data', payload);

  const response = await apiFetch(`${API_BASE}/register`, {
    method: 'POST',
    headers: actorHeaders(actorUserId),
    body: formData,
  });
  if (!response.ok) {
    await parseActionError(response, '도면 등록에 실패했습니다.');
  }
}

export async function reviseDrawing(
  partNo: string,
  payload: DrawingRevisePayload,
  file: File,
  actorUserId?: string,
): Promise<void> {
  const formData = new FormData();
  formData.append('file', file);
  appendJsonPart(formData, 'data', payload);

  const response = await apiFetch(`${API_BASE}/${encodeURIComponent(partNo)}/revise`, {
    method: 'POST',
    headers: actorHeaders(actorUserId),
    body: formData,
  });
  if (!response.ok) {
    await parseActionError(response, '도면 개정에 실패했습니다.');
  }
}

export async function deleteDrawing(partNo: string, actorUserId?: string): Promise<void> {
  const response = await apiFetch(`${API_BASE}/${encodeURIComponent(partNo)}`, {
    method: 'DELETE',
    headers: actorHeaders(actorUserId),
  });
  if (!response.ok) {
    await parseActionError(response, '도면 삭제에 실패했습니다.');
  }
}

export async function restoreDrawing(id: string, actorUserId?: string): Promise<void> {
  const response = await apiFetch(`${API_BASE}/${id}/restore`, {
    method: 'POST',
    headers: actorHeaders(actorUserId),
  });
  if (!response.ok) {
    await parseActionError(response, '도면 복구에 실패했습니다.');
  }
}

export async function hardDeleteDrawing(id: string, actorUserId?: string): Promise<void> {
  const response = await apiFetch(`${API_BASE}/${id}/hard`, {
    method: 'DELETE',
    headers: actorHeaders(actorUserId),
  });
  if (!response.ok) {
    await parseActionError(response, '도면 영구 삭제에 실패했습니다.');
  }
}

export async function promoteDrawing(partNo: string, actorUserId?: string): Promise<void> {
  const response = await apiFetch(`${API_BASE}/${encodeURIComponent(partNo)}/promote`, {
    method: 'POST',
    headers: actorHeaders(actorUserId),
  });
  if (!response.ok) {
    await parseActionError(response, '양산 이관에 실패했습니다.');
  }
}

export async function updateDrawingInfo(
  id: string,
  payload: DrawingInfoUpdatePayload,
  actorUserId?: string,
): Promise<void> {
  const response = await apiFetch(`${API_BASE}/${id}/info`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
      ...actorHeaders(actorUserId),
    },
    body: JSON.stringify(payload),
  });
  if (!response.ok) {
    await parseActionError(response, '도면 정보 수정에 실패했습니다.');
  }
}

export function drawingPdfAuthHeaders(): Record<string, string> | undefined {
  const token = getAccessToken();
  if (!token) {
    return undefined;
  }
  return { Authorization: `Bearer ${token}` };
}
