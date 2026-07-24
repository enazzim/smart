import { apiFetch, handleResponse } from './http';
import { sortByKoreanField } from '../utils/koreanSort';

export interface WorkCenter {
  id: number;
  wcName: string;
  mainProcessCodeId: number;
  mainProcessCode: string;
  mainProcessName: string;
  operationTime: number;
  createdAt: string;
}

export interface CreateWorkCenterRequest {
  wcName: string;
  mainProcessCodeId: number;
  operationTime: number;
}

export type UpdateWorkCenterRequest = CreateWorkCenterRequest;

const API_BASE = '/api/v1/basis/work-centers';

export async function fetchWorkCenters(query?: string): Promise<WorkCenter[]> {
  const url = query?.trim() ? `${API_BASE}?q=${encodeURIComponent(query.trim())}` : API_BASE;
  const centers = await handleResponse<WorkCenter[]>(await apiFetch(url));
  return sortByKoreanField(centers, (wc) => wc.wcName);
}

export async function fetchWorkCenter(id: number): Promise<WorkCenter> {
  return handleResponse<WorkCenter>(await apiFetch(`${API_BASE}/${id}`));
}

export async function createWorkCenter(payload: CreateWorkCenterRequest): Promise<WorkCenter> {
  return handleResponse<WorkCenter>(
    await apiFetch(API_BASE, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateWorkCenter(id: number, payload: UpdateWorkCenterRequest): Promise<WorkCenter> {
  return handleResponse<WorkCenter>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteWorkCenter(id: number): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'DELETE',
    }),
  );
}
