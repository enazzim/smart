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

async function handleResponse<T>(response: Response): Promise<T> {
  if (!response.ok) {
    const body = await response.json().catch(() => ({ message: response.statusText }));
    throw new Error(body.message ?? '요청에 실패했습니다.');
  }
  if (response.status === 204) {
    return undefined as T;
  }
  return response.json() as Promise<T>;
}

export async function fetchWorkCenters(query?: string): Promise<WorkCenter[]> {
  const url = query?.trim() ? `${API_BASE}?q=${encodeURIComponent(query.trim())}` : API_BASE;
  return handleResponse<WorkCenter[]>(await fetch(url));
}

export async function fetchWorkCenter(id: number): Promise<WorkCenter> {
  return handleResponse<WorkCenter>(await fetch(`${API_BASE}/${id}`));
}

export async function createWorkCenter(payload: CreateWorkCenterRequest): Promise<WorkCenter> {
  return handleResponse<WorkCenter>(
    await fetch(API_BASE, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateWorkCenter(id: number, payload: UpdateWorkCenterRequest): Promise<WorkCenter> {
  return handleResponse<WorkCenter>(
    await fetch(`${API_BASE}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteWorkCenter(id: number): Promise<void> {
  await handleResponse<void>(
    await fetch(`${API_BASE}/${id}`, {
      method: 'DELETE',
    }),
  );
}
