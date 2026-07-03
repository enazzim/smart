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

export interface BomTreeNode {
  itemNum: string;
  itemName: string;
  propertyClassification: string;
  level: number;
  quantity: number;
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

export async function fetchItemCompositions(
  parentItemNum?: string,
  childItemNum?: string,
): Promise<ItemComposition[]> {
  const params = new URLSearchParams();
  if (parentItemNum) params.set('parentItemNum', parentItemNum);
  if (childItemNum) params.set('childItemNum', childItemNum);
  const query = params.toString();
  const url = query ? `${API_BASE}?${query}` : API_BASE;
  return handleResponse<ItemComposition[]>(await fetch(url));
}

export async function createItemComposition(
  payload: CreateItemCompositionRequest,
): Promise<ItemComposition> {
  return handleResponse<ItemComposition>(
    await fetch(API_BASE, {
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
    await fetch(`${API_BASE}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteItemComposition(id: number): Promise<void> {
  await handleResponse<void>(
    await fetch(`${API_BASE}/${id}`, {
      method: 'DELETE',
    }),
  );
}

export async function fetchBomExplosion(itemNum: string): Promise<BomTreeNode> {
  return handleResponse<BomTreeNode>(
    await fetch(`${API_BASE}/${encodeURIComponent(itemNum)}/explosion`),
  );
}

export async function fetchBomReverse(itemNum: string): Promise<ItemComposition[]> {
  return handleResponse<ItemComposition[]>(
    await fetch(`${API_BASE}/${encodeURIComponent(itemNum)}/reverse`),
  );
}

export async function copyBom(payload: CopyBomRequest): Promise<{ copiedCount: number }> {
  return handleResponse<{ copiedCount: number }>(
    await fetch(`${API_BASE}/copy`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}
