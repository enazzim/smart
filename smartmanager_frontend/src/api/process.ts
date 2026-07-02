export type WorkDistinction = 'INHOUSE' | 'OUTSOURCE' | 'SPLIT';

export interface ProcessPlan {
  id: number;
  itemId: number;
  itemNo: string;
  itemName: string;
  processSequenceNum: number;
  processCodeId: number;
  processCode: string;
  processName: string;
  workDistinction: WorkDistinction;
  workCenterId?: number | null;
  wcName?: string | null;
  outsideOrderRate: number;
  progressRate: number;
  createdAt: string;
}

export interface CreateProcessRequest {
  itemId: number;
  processSequenceNum: number;
  processCodeId: number;
  workDistinction: WorkDistinction;
  workCenterId?: number | null;
  outsideOrderRate?: number;
  progressRate: number;
}

export type UpdateProcessRequest = CreateProcessRequest;

export interface CodeOption {
  id: number;
  code: string;
  name: string;
}

export interface WorkCenter {
  id: number;
  wcName: string;
  mainProcessCodeId: number;
}

const API_BASE = '/api/v1/basis/processes/plan';

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

export async function fetchProcessPlans(itemId?: number): Promise<ProcessPlan[]> {
  const url = itemId != null ? `${API_BASE}?itemId=${itemId}` : API_BASE;
  return handleResponse<ProcessPlan[]>(await fetch(url));
}

export async function createProcessPlan(payload: CreateProcessRequest): Promise<ProcessPlan> {
  return handleResponse<ProcessPlan>(
    await fetch(API_BASE, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateProcessPlan(id: number, payload: UpdateProcessRequest): Promise<ProcessPlan> {
  return handleResponse<ProcessPlan>(
    await fetch(`${API_BASE}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteProcessPlan(id: number): Promise<void> {
  await handleResponse<void>(
    await fetch(`${API_BASE}/${id}`, {
      method: 'DELETE',
    }),
  );
}

export async function fetchProcessCodeOptions(): Promise<CodeOption[]> {
  return handleResponse<CodeOption[]>(
    await fetch('/api/v1/basis/code-groups/PROCESS_CODE/options'),
  );
}

export async function fetchWorkCenters(): Promise<WorkCenter[]> {
  return handleResponse<WorkCenter[]>(await fetch('/api/v1/basis/work-centers'));
}
