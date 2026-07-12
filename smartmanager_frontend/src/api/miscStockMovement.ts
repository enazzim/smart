import { apiFetch, handleResponse } from './http';

export type MiscStockMovementDirection = 'IN' | 'OUT';
export type MiscStockMovementStatus = 'REGISTERED' | 'CANCELLED';
export type PropertyClassification = '원자재' | '제품' | '상품' | '공정품';

export interface MiscStockMovementPreview {
  itemId: number;
  itemNo: string;
  itemName: string;
  propertyClassification: PropertyClassification;
  locationCode: string;
  locationLabel: string;
  outputProcessId?: number | null;
  outputProcessSequence?: number | null;
  outputProcessName?: string | null;
  processRequired: boolean;
  onHandQty: number;
  lotTracked: boolean;
}

export interface MiscStockMovementRow {
  id: number;
  movementNo: string;
  movementDate: string;
  movementDirection: MiscStockMovementDirection;
  movementDirectionLabel: string;
  itemId: number;
  itemNo: string;
  itemName: string;
  propertyClassification: PropertyClassification;
  locationCode: string;
  locationLabel: string;
  outputProcessId?: number | null;
  outputProcessSequence?: number | null;
  outputProcessName?: string | null;
  qty: number;
  reasonCodeId?: number | null;
  reasonLabel?: string | null;
  note?: string | null;
  status: MiscStockMovementStatus;
  statusLabel: string;
  createdAt: string;
  lotId?: number | null;
}

export interface MiscStockMovementListParams {
  itemNo?: string;
  itemName?: string;
  movementDateFrom?: string;
  movementDateTo?: string;
}

function buildQuery(params: MiscStockMovementListParams): string {
  const search = new URLSearchParams();
  if (params.itemNo?.trim()) search.set('itemNo', params.itemNo.trim());
  if (params.itemName?.trim()) search.set('itemName', params.itemName.trim());
  if (params.movementDateFrom) search.set('movementDateFrom', params.movementDateFrom);
  if (params.movementDateTo) search.set('movementDateTo', params.movementDateTo);
  const query = search.toString();
  return query ? `?${query}` : '';
}

export async function fetchMiscStockMovementPreview(
  itemId: number,
  processSequenceId?: number | null,
  movementDate?: string,
): Promise<MiscStockMovementPreview> {
  const search = new URLSearchParams({ itemId: String(itemId) });
  if (processSequenceId != null) search.set('processSequenceId', String(processSequenceId));
  if (movementDate) search.set('movementDate', movementDate);
  return handleResponse(
    await apiFetch(`/api/v1/inventory/misc-movements/preview?${search.toString()}`),
  );
}

export async function fetchMiscStockMovements(
  params: MiscStockMovementListParams = {},
): Promise<MiscStockMovementRow[]> {
  return handleResponse(
    await apiFetch(`/api/v1/inventory/misc-movements${buildQuery(params)}`),
  );
}

export async function createMiscStockMovement(payload: {
  movementDate: string;
  movementDirection: MiscStockMovementDirection;
  itemId: number;
  processSequenceId?: number | null;
  qty: number;
  reasonCodeId?: number | null;
  note?: string;
  lotId?: number | null;
  lotNo?: string;
  autoGenerateLot?: boolean;
}): Promise<MiscStockMovementRow> {
  return handleResponse(
    await apiFetch('/api/v1/inventory/misc-movements', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateMiscStockMovement(
  id: number,
  payload: {
    movementDate: string;
    movementDirection: MiscStockMovementDirection;
    itemId: number;
    processSequenceId?: number | null;
    qty: number;
    reasonCodeId?: number | null;
    note?: string;
    lotId?: number | null;
    lotNo?: string;
    autoGenerateLot?: boolean;
  },
): Promise<MiscStockMovementRow> {
  return handleResponse(
    await apiFetch(`/api/v1/inventory/misc-movements/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function cancelMiscStockMovement(id: number): Promise<void> {
  await handleResponse(
    await apiFetch(`/api/v1/inventory/misc-movements/${id}/cancel`, {
      method: 'POST',
    }),
  );
}
