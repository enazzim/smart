import { apiFetch, handleResponse } from './http';

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

export async function fetchUnitPrices(type: CostType, q?: string): Promise<UnitPrice[]> {
  const params = new URLSearchParams({ type });
  if (q?.trim()) params.set('q', q.trim());
  return handleResponse<UnitPrice[]>(await apiFetch(`${API_BASE}?${params}`));
}

export function resolveUnitPriceAmount(price: UnitPrice): number {
  return price.discountUnitCost ?? price.standardUnitCost;
}

function isEffectiveUnitPrice(price: UnitPrice, refDate: string): boolean {
  if (price.beginDate > refDate) return false;
  if (price.endDate && price.endDate < refDate) return false;
  return true;
}

export async function lookupPurchaseUnitPrice(
  partnerId: number,
  itemId: number,
  itemNo: string,
  refDate: string,
): Promise<number | null> {
  const prices = await fetchUnitPrices('PURCHASE', itemNo);
  const match = prices
    .filter((price) => price.companyId === partnerId && price.itemId === itemId && isEffectiveUnitPrice(price, refDate))
    .sort((left, right) => right.beginDate.localeCompare(left.beginDate))[0];
  return match != null ? resolveUnitPriceAmount(match) : null;
}

export async function createUnitPrice(payload: CreateUnitPriceRequest): Promise<UnitPrice> {
  return handleResponse<UnitPrice>(
    await apiFetch(API_BASE, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateUnitPrice(id: number, payload: UpdateUnitPriceRequest): Promise<UnitPrice> {
  return handleResponse<UnitPrice>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteUnitPrice(id: number): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'DELETE',
    }),
  );
}
