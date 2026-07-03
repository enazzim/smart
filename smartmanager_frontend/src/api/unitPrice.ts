export type CostType = 'SALE' | 'PURCHASE' | 'OUTSOURCE';

export interface UnitPrice {
  id: number;
  type: CostType;
  itemId: number;
  itemNum: string;
  itemName: string;
  companyId: number;
  companyName: string;
  businessRegistrationNum: string;
  beginProcessCodeId?: number | null;
  processCode?: string | null;
  processName?: string | null;
  endProcessCodeId?: number | null;
  endProcessCode?: string | null;
  endProcessName?: string | null;
  orderRate: number;
  standardUnitCost: number;
  discountUnitCost?: number | null;
  beginDate: string;
  endDate?: string | null;
  createdAt: string;
}

export interface CreateUnitPriceRequest {
  type: CostType;
  itemNum: string;
  companyId: number;
  beginProcessCodeId?: number;
  endProcessCodeId?: number;
  orderRate?: number;
  standardUnitCost: number;
  discountUnitCost?: number;
  beginDate: string;
  endDate?: string;
}

export interface UpdateUnitPriceRequest {
  orderRate: number;
  standardUnitCost: number;
  discountUnitCost?: number;
  beginDate: string;
  endDate?: string;
  updateReason: string;
}

const API_BASE = '/api/v1/basis/unit-prices';

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

export async function fetchUnitPrices(type: CostType, q?: string): Promise<UnitPrice[]> {
  const params = new URLSearchParams({ type });
  if (q?.trim()) params.set('q', q.trim());
  return handleResponse<UnitPrice[]>(await fetch(`${API_BASE}?${params}`));
}

export async function createUnitPrice(payload: CreateUnitPriceRequest): Promise<UnitPrice> {
  return handleResponse<UnitPrice>(
    await fetch(API_BASE, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateUnitPrice(id: number, payload: UpdateUnitPriceRequest): Promise<UnitPrice> {
  return handleResponse<UnitPrice>(
    await fetch(`${API_BASE}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteUnitPrice(id: number): Promise<void> {
  await handleResponse<void>(
    await fetch(`${API_BASE}/${id}`, {
      method: 'DELETE',
    }),
  );
}
