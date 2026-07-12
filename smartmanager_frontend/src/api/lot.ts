import { apiFetch, handleResponse } from './http';
import type { StockMovement } from './inventoryLedger';

export type LotStatus = 'ACTIVE' | 'BLOCKED' | 'DEPLETED';
export type LotOriginType =
  | 'MANUAL'
  | 'PURCHASE'
  | 'PRODUCTION'
  | 'SPLIT'
  | 'MERGE'
  | 'ADJUSTMENT';

export interface LotBalanceRow {
  id: number;
  lotId: number;
  inventoryBalanceId: number;
  locationCode: string;
  locationLabel: string;
  outputProcessId?: number | null;
  outputProcessSequence?: number | null;
  outputProcessName?: string | null;
  qtyOnHand: number;
}

export interface LotRow {
  id: number;
  itemId: number;
  itemNo: string;
  itemName: string;
  lotNo: string;
  status: LotStatus;
  statusLabel: string;
  originType: LotOriginType;
  originTypeLabel: string;
  originDocType?: string | null;
  originDocId?: number | null;
  p1?: string | null;
  p2?: string | null;
  expiryDate?: string | null;
  certificateRef?: string | null;
  remark?: string | null;
  createdAt: string;
  updatedAt?: string | null;
  balances: LotBalanceRow[];
}

export interface LotListParams {
  itemId?: number;
  itemNo?: string;
  lotNo?: string;
  status?: LotStatus;
  locationCode?: string;
}

export interface CreateLotPayload {
  itemId: number;
  lotNo?: string;
  autoGenerate?: boolean;
  originType?: LotOriginType;
  originDocType?: string;
  originDocId?: number;
  p1?: string;
  p2?: string;
  expiryDate?: string;
  certificateRef?: string;
  remark?: string;
}

export interface UpdateLotPayload {
  status: LotStatus;
  p1?: string;
  p2?: string;
  expiryDate?: string | null;
  certificateRef?: string;
  remark?: string;
}

function buildQuery(params: LotListParams): string {
  const search = new URLSearchParams();
  if (params.itemId != null) search.set('itemId', String(params.itemId));
  if (params.itemNo?.trim()) search.set('itemNo', params.itemNo.trim());
  if (params.lotNo?.trim()) search.set('lotNo', params.lotNo.trim());
  if (params.status) search.set('status', params.status);
  if (params.locationCode?.trim()) search.set('locationCode', params.locationCode.trim());
  const query = search.toString();
  return query ? `?${query}` : '';
}

export async function fetchLots(params: LotListParams = {}): Promise<LotRow[]> {
  return handleResponse(await apiFetch(`/api/v1/inventory/lots${buildQuery(params)}`));
}

export async function fetchLot(id: number): Promise<LotRow> {
  return handleResponse(await apiFetch(`/api/v1/inventory/lots/${id}`));
}

export async function fetchAvailableLots(
  itemId: number,
  locationCode: string,
  outputProcessId?: number | null,
): Promise<LotRow[]> {
  const search = new URLSearchParams({
    itemId: String(itemId),
    locationCode,
  });
  if (outputProcessId != null) search.set('outputProcessId', String(outputProcessId));
  return handleResponse(await apiFetch(`/api/v1/inventory/lots/available?${search.toString()}`));
}

export type LotGenealogyDirection = 'UP' | 'DOWN';
export type LotGenealogyLinkType = 'CONSUME' | 'PRODUCE' | 'SPLIT' | 'MERGE';

export interface LotGenealogyLink {
  id: number;
  parentLotId: number;
  parentLotNo: string;
  parentItemId: number;
  parentItemNo: string;
  parentItemName: string;
  childLotId: number;
  childLotNo: string;
  childItemId: number;
  childItemNo: string;
  childItemName: string;
  linkType: LotGenealogyLinkType;
  linkTypeLabel: string;
  qty: number;
  stockMovementId?: number | null;
  sourceDocType?: string | null;
  sourceDocId?: number | null;
  createdAt: string;
}

export async function fetchLotGenealogy(
  lotId: number,
  direction: LotGenealogyDirection = 'UP',
): Promise<LotGenealogyLink[]> {
  const search = new URLSearchParams({ direction });
  return handleResponse(
    await apiFetch(`/api/v1/inventory/lots/${lotId}/genealogy?${search.toString()}`),
  );
}

export async function fetchLotMovements(lotId: number): Promise<StockMovement[]> {
  return handleResponse(await apiFetch(`/api/v1/inventory/lots/${lotId}/movements`));
}

export async function createLot(payload: CreateLotPayload): Promise<LotRow> {
  return handleResponse(
    await apiFetch('/api/v1/inventory/lots', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateLot(id: number, payload: UpdateLotPayload): Promise<LotRow> {
  return handleResponse(
    await apiFetch(`/api/v1/inventory/lots/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteLot(id: number): Promise<void> {
  await handleResponse(await apiFetch(`/api/v1/inventory/lots/${id}`, { method: 'DELETE' }));
}
