import { apiFetch, handleResponse } from './http';

export type PropertyClassification = '원자재' | '제품' | '상품' | '공정품' | '부자재' | '팬텀';
export type CheckDistinction = 'NONE' | 'INSPECTION';

export interface Item {
  id: number;
  itemNo: string;
  itemName: string;
  propertyClassification: PropertyClassification;
  modelType: string;
  unit: string;
  standard?: string | null;
  standardUnitCost?: number | null;
  checkDistinction?: CheckDistinction | null;
  leadTime?: number | null;
  safetyStockQuantity?: number | null;
  orderIntervalQuantity?: number | null;
  minOrderQuantity?: number | null;
  lotTracked: boolean;
  createdAt: string;
}

export interface CreateItemRequest {
  itemNo: string;
  itemName: string;
  propertyClassification: PropertyClassification;
  modelType: string;
  unit: string;
  standard?: string;
  standardUnitCost?: number;
  checkDistinction?: CheckDistinction;
  leadTime?: number;
  safetyStockQuantity?: number;
  orderIntervalQuantity?: number;
  minOrderQuantity?: number;
  lotTracked?: boolean;
}

export type UpdateItemRequest = Omit<CreateItemRequest, 'itemNo'>;

const API_BASE = '/api/v1/basis/items';

export async function fetchItems(itemNo?: string, itemName?: string): Promise<Item[]> {
  const params = new URLSearchParams();
  if (itemNo) params.set('itemNo', itemNo);
  if (itemName) params.set('itemName', itemName);
  const query = params.toString();
  const url = query ? `${API_BASE}?${query}` : API_BASE;
  return handleResponse<Item[]>(await apiFetch(url));
}

export async function createItem(payload: CreateItemRequest): Promise<Item> {
  return handleResponse<Item>(
    await apiFetch(API_BASE, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateItem(id: number, payload: UpdateItemRequest): Promise<Item> {
  return handleResponse<Item>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateItemLotTracked(id: number, lotTracked: boolean): Promise<Item> {
  return handleResponse<Item>(
    await apiFetch(`${API_BASE}/${id}/lot-tracked`, {
      method: 'PATCH',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ lotTracked }),
    }),
  );
}

export async function deleteItem(id: number): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'DELETE',
    }),
  );
}
