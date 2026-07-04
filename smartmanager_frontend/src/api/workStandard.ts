import { apiFetch, handleResponse } from './http';
import type { WorkDistinction } from './process';

export interface WorkStandard {
  id: number;
  itemId: number;
  itemNum: string;
  itemName: string;
  processSequenceId: number;
  processSequenceNum: number;
  processCodeId: number;
  processCode: string;
  processName: string;
  workDistinction: WorkDistinction;
  workCenterId: number;
  wcName: string;
  equipmentId?: number | null;
  equipmentName?: string | null;
  priorityOrder: number;
  mainWorkerId?: number | null;
  mainWorkerName?: string | null;
  toolName?: string | null;
  setupTime: number;
  standardTime: number;
  createdAt: string;
}

export interface CreateWorkStandardRequest {
  itemNum: string;
  processSequenceId: number;
  workCenterId: number;
  equipmentId?: number | null;
  priorityOrder: number;
  mainWorkerId?: number | null;
  toolName?: string;
  setupTime: number;
  standardTime: number;
}

export type UpdateWorkStandardRequest = Omit<
  CreateWorkStandardRequest,
  'itemNum' | 'processSequenceId'
>;

export interface CopyWorkStandardRequest {
  sourceItemNum: string;
  targetItemNum: string;
}

const API_BASE = '/api/v1/basis/work-standards/plan';

export async function fetchWorkStandards(itemNum?: string): Promise<WorkStandard[]> {
  const url = itemNum?.trim()
    ? `${API_BASE}?itemNum=${encodeURIComponent(itemNum.trim())}`
    : API_BASE;
  return handleResponse<WorkStandard[]>(await apiFetch(url));
}

export async function fetchWorkStandard(id: number): Promise<WorkStandard> {
  return handleResponse<WorkStandard>(await apiFetch(`${API_BASE}/${id}`));
}

export async function createWorkStandard(payload: CreateWorkStandardRequest): Promise<WorkStandard> {
  return handleResponse<WorkStandard>(
    await apiFetch(API_BASE, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateWorkStandard(
  id: number,
  payload: UpdateWorkStandardRequest,
): Promise<WorkStandard> {
  return handleResponse<WorkStandard>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteWorkStandard(id: number): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'DELETE',
    }),
  );
}

export async function copyWorkStandards(payload: CopyWorkStandardRequest): Promise<number> {
  const body = await handleResponse<{ copied: number }>(
    await apiFetch(`${API_BASE}/copy`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
  return body.copied;
}
