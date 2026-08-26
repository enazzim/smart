import { apiFetch, getAccessToken, handleResponse } from './http';
import type { DrawingLifecycleStage } from '../utils/drawingLifecycle';

export type DrawingType = 'DEV' | 'PROD';

export interface DrawingListItem {
  id: string;
  partNo: string;
  partName: string;
  modelType: string;
  itemId?: number | null;
  itemNo?: string | null;
  lifecycleStage?: DrawingLifecycleStage | null;
  sourcePartnerId?: number | null;
  sourcePartnerName?: string | null;
  itemLinkedAt?: string | null;
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
  modelType: string;
  sourcePartnerId?: number | null;
  drawingType: DrawingType;
}

export interface DrawingRevisePayload {
  changeType: string;
  changeReason: string;
}

export interface DrawingInfoUpdatePayload {
  partNo: string;
  partName: string;
  modelType: string;
}

export interface DrawingLifecycleUpdatePayload {
  lifecycleStage: DrawingLifecycleStage;
}

export interface DrawingLinkItemPayload {
  itemId: number;
}

export interface DrawingReopenDevPayload {
  reason?: string | null;
}

const API_BASE = '/api/v1/basis/drawings';

function appendJsonPart(formData: FormData, name: string, value: unknown): void {
  formData.append(name, new Blob([JSON.stringify(value)], { type: 'application/json' }));
}

async function parseActionError(response: Response, fallback: string): Promise<never> {
  const body = await response.json().catch(() => ({ message: fallback }));
  const message = typeof body.message === 'string' ? body.message : fallback;
  throw new Error(message);
}

export interface DrawingListQuery {
  lifecycleStage?: DrawingLifecycleStage | null;
  historyQuery?: string | null;
}

export async function fetchDrawings(query?: DrawingListQuery): Promise<DrawingListItem[]> {
  const params = new URLSearchParams();
  if (query?.lifecycleStage) {
    params.set('lifecycleStage', query.lifecycleStage);
  }
  if (query?.historyQuery?.trim()) {
    params.set('historyQuery', query.historyQuery.trim());
  }
  const suffix = params.toString();
  const url = suffix ? `${API_BASE}?${suffix}` : API_BASE;
  return handleResponse<DrawingListItem[]>(await apiFetch(url, { cache: 'no-store' }));
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
): Promise<void> {
  const response = await apiFetch(`${API_BASE}/${masterId}/history/${historyId}/references`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
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
): Promise<void> {
  const formData = new FormData();
  formData.append('file', file);
  appendJsonPart(formData, 'data', payload);

  const response = await apiFetch(`${API_BASE}/register`, {
    method: 'POST',
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
): Promise<void> {
  const formData = new FormData();
  formData.append('file', file);
  appendJsonPart(formData, 'data', payload);

  const response = await apiFetch(`${API_BASE}/${encodeURIComponent(partNo)}/revise`, {
    method: 'POST',
    body: formData,
  });
  if (!response.ok) {
    await parseActionError(response, '도면 개정에 실패했습니다.');
  }
}

export async function deleteDrawing(partNo: string): Promise<void> {
  const response = await apiFetch(`${API_BASE}/${encodeURIComponent(partNo)}`, {
    method: 'DELETE',
  });
  if (!response.ok) {
    await parseActionError(response, '도면 삭제에 실패했습니다.');
  }
}

export async function restoreDrawing(id: string): Promise<void> {
  const response = await apiFetch(`${API_BASE}/${id}/restore`, {
    method: 'POST',
  });
  if (!response.ok) {
    await parseActionError(response, '도면 복구에 실패했습니다.');
  }
}

export async function hardDeleteDrawing(id: string): Promise<void> {
  const response = await apiFetch(`${API_BASE}/${id}/hard`, {
    method: 'DELETE',
  });
  if (!response.ok) {
    await parseActionError(response, '도면 영구 삭제에 실패했습니다.');
  }
}

export async function promoteDrawing(partNo: string): Promise<void> {
  const response = await apiFetch(`${API_BASE}/${encodeURIComponent(partNo)}/promote`, {
    method: 'POST',
  });
  if (!response.ok) {
    await parseActionError(response, '양산 이관에 실패했습니다.');
  }
}

export async function linkDrawingItem(
  masterId: string,
  payload: DrawingLinkItemPayload,
): Promise<void> {
  const response = await apiFetch(`${API_BASE}/${masterId}/link-item`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(payload),
  });
  if (!response.ok) {
    await parseActionError(response, '품목 연결에 실패했습니다.');
  }
}

export async function updateDrawingLifecycle(
  masterId: string,
  payload: DrawingLifecycleUpdatePayload,
): Promise<void> {
  const response = await apiFetch(`${API_BASE}/${masterId}/lifecycle`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(payload),
  });
  if (!response.ok) {
    await parseActionError(response, '업무 단계 변경에 실패했습니다.');
  }
}

export async function reopenDrawingDev(
  partNo: string,
  payload: DrawingReopenDevPayload,
): Promise<void> {
  const response = await apiFetch(`${API_BASE}/${encodeURIComponent(partNo)}/reopen-dev`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(payload),
  });
  if (!response.ok) {
    await parseActionError(response, '개발 재개에 실패했습니다.');
  }
}

export async function updateDrawingInfo(
  id: string,
  payload: DrawingInfoUpdatePayload,
): Promise<void> {
  const response = await apiFetch(`${API_BASE}/${id}/info`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
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
