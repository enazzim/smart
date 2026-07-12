import { apiFetch, handleResponse } from './http';

export interface ItemComposition {
  id: number;
  parentItemId: number;
  parentItemNo: string;
  parentItemName: string;
  childItemId: number;
  childItemNo: string;
  childItemName: string;
  parentQuantity: number;
  childQuantity: number;
  beginDate: string;
  endDate?: string | null;
  createdAt: string;
}

export interface BomVendorPrice {
  partnerName: string;
  unitPrice: number;
  detail: string;
}

export interface BomTreeNode {
  itemId: number;
  itemNum: string;
  itemName: string;
  propertyClassification: string;
  level: number;
  quantity: number;
  lotTracked: boolean;
  outsourcePrices: BomVendorPrice[];
  purchasePrices: BomVendorPrice[];
  children: BomTreeNode[];
}

export interface CreateItemCompositionRequest {
  parentItemNum: string;
  childItemNum: string;
  parentQuantity: number;
  childQuantity: number;
}

export interface UpdateItemCompositionRequest {
  parentQuantity: number;
  childQuantity: number;
}

export interface CopyBomRequest {
  sourceItemNum: string;
  targetItemNum: string;
}

const API_BASE = '/api/v1/basis/item-composition/plan';

export async function fetchItemCompositions(
  parentItemNum?: string,
  childItemNum?: string,
  parentItemId?: number,
  childItemId?: number,
): Promise<ItemComposition[]> {
  const params = new URLSearchParams();
  if (parentItemId != null) params.set('parentItemId', String(parentItemId));
  if (childItemId != null) params.set('childItemId', String(childItemId));
  if (parentItemNum) params.set('parentItemNum', parentItemNum);
  if (childItemNum) params.set('childItemNum', childItemNum);
  const query = params.toString();
  const url = query ? `${API_BASE}?${query}` : API_BASE;
  return handleResponse<ItemComposition[]>(await apiFetch(url));
}

export async function createItemComposition(
  payload: CreateItemCompositionRequest,
): Promise<ItemComposition> {
  return handleResponse<ItemComposition>(
    await apiFetch(API_BASE, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateItemComposition(
  id: number,
  payload: UpdateItemCompositionRequest,
): Promise<ItemComposition> {
  return handleResponse<ItemComposition>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteItemComposition(id: number): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'DELETE',
    }),
  );
}

export async function fetchBomExplosion(itemNum: string): Promise<BomTreeNode> {
  return handleResponse<BomTreeNode>(
    await apiFetch(`${API_BASE}/${encodeURIComponent(itemNum)}/explosion`),
  );
}

export async function fetchBomReverse(itemNum: string): Promise<ItemComposition[]> {
  return handleResponse<ItemComposition[]>(
    await apiFetch(`${API_BASE}/${encodeURIComponent(itemNum)}/reverse`),
  );
}

export interface LotTrackedEnablePreviewItem {
  itemId: number;
  itemNo: string;
  itemName: string;
  alreadyLotTracked: boolean;
  otherParentItemNos: string[];
}

export async function previewEnableLotTracked(itemNum: string): Promise<LotTrackedEnablePreviewItem[]> {
  return handleResponse(
    await apiFetch(`${API_BASE}/${encodeURIComponent(itemNum)}/lot-tracked/enable-preview`, {
      method: 'POST',
    }),
  );
}

export async function enableLotTrackedForExplosion(
  itemNum: string,
  itemIds: number[],
): Promise<{ updatedCount: number }> {
  return handleResponse(
    await apiFetch(`${API_BASE}/${encodeURIComponent(itemNum)}/lot-tracked/enable`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ itemIds }),
    }),
  );
}

export async function copyBom(payload: CopyBomRequest): Promise<{ copiedCount: number }> {
  return handleResponse<{ copiedCount: number }>(
    await apiFetch(`${API_BASE}/copy`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}
