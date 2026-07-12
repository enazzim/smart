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

export function drawingPdfUrl(partNo: string, historyId?: string): string {
  const params = new URLSearchParams();
  if (historyId) {
    params.set('historyId', historyId);
  }
  params.set('t', String(Date.now()));
  const query = params.toString();
  return query ? `${API_BASE}/pdf/${encodeURIComponent(partNo)}?${query}` : `${API_BASE}/pdf/${encodeURIComponent(partNo)}`;
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
