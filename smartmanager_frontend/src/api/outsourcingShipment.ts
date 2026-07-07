import { apiFetch, handleResponse } from './http';

export type OutsourcingShipmentType = 'ORDER' | 'ADVANCE';
export type OutsourcingShipmentStatus = 'ISSUED' | 'CANCELLED';

export interface OutsourcingShipmentCandidate {
  orderLineId: number;
  orderId: number;
  orderNo: string;
  orderDate: string;
  partnerId: number;
  partnerName: string;
  itemId: number;
  itemNo: string;
  itemName: string;
  processName: string;
  beginProcessName: string;
  endProcessName: string;
  orderQty: number;
  shippedQty: number;
  remainingQty: number;
  shippable: boolean;
  shippableMessage?: string | null;
}

export interface OutsourcingShipmentInputPreviewLine {
  itemId: number;
  itemNo: string;
  itemName: string;
  propertyClassification: string;
  itemCompositionId?: number | null;
  unitRatio: number;
  issueQty: number;
  sourceLocationCode: string;
  sourceProcessId?: number | null;
  inputProcessId: number;
  inputProcessName: string;
  onHandQty: number;
}

export interface OutsourcingShipmentInputPreview {
  orderLineId?: number | null;
  orderNo: string;
  itemNo: string;
  shipmentQty: number;
  lines: OutsourcingShipmentInputPreviewLine[];
}

export interface OutsourceAdvanceProcessOption {
  beginProcessCodeId: number;
  beginProcessName: string;
  endProcessCodeId: number;
  endProcessName: string;
}

export interface OutsourcingShipmentInputLine {
  itemId: number;
  itemNo: string;
  itemName: string;
  itemCompositionId?: number | null;
  issueQty: number;
  sourceLocationCode: string;
  sourceProcessId?: number | null;
  inputProcessId: number;
}

export interface OutsourcingShipmentLine {
  id: number;
  lineNo: number;
  orderLineId?: number | null;
  orderNo: string;
  parentItemId?: number | null;
  parentItemNo?: string | null;
  parentItemName?: string | null;
  partnerId: number;
  partnerName: string;
  itemNo: string;
  itemName: string;
  processName: string;
  shipmentQty: number;
  inputLines: OutsourcingShipmentInputLine[];
}

export interface OutsourcingShipment {
  id: number;
  shipmentNo: string;
  shipmentDate: string;
  shipmentType: OutsourcingShipmentType;
  shipmentTypeLabel: string;
  partnerId?: number | null;
  partnerName?: string | null;
  status: OutsourcingShipmentStatus;
  statusLabel: string;
  createdAt: string;
  createdBy?: string | null;
  cancelable: boolean;
  lines: OutsourcingShipmentLine[];
}

export interface OutsourcingShipmentListParams {
  shipmentDateFrom?: string;
  shipmentDateTo?: string;
  shipmentNo?: string;
  partnerName?: string;
  status?: OutsourcingShipmentStatus;
  excludeCancelled?: boolean;
}

function buildQuery(params?: OutsourcingShipmentListParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.shipmentDateFrom) search.set('shipmentDateFrom', params.shipmentDateFrom);
  if (params.shipmentDateTo) search.set('shipmentDateTo', params.shipmentDateTo);
  if (params.shipmentNo?.trim()) search.set('shipmentNo', params.shipmentNo.trim());
  if (params.partnerName?.trim()) search.set('partnerName', params.partnerName.trim());
  if (params.status) search.set('status', params.status);
  if (params.excludeCancelled !== undefined) search.set('excludeCancelled', String(params.excludeCancelled));
  const query = search.toString();
  return query ? `?${query}` : '';
}

export async function fetchOutsourcingShipmentCandidates(): Promise<OutsourcingShipmentCandidate[]> {
  return handleResponse(await apiFetch('/api/v1/outsource/shipments/candidates'));
}

export async function fetchOutsourcingShipmentInputPreview(
  orderLineId: number,
  shipmentQty: number,
  shipmentDate?: string,
): Promise<OutsourcingShipmentInputPreview> {
  const search = new URLSearchParams({
    orderLineId: String(orderLineId),
    shipmentQty: String(shipmentQty),
  });
  if (shipmentDate) search.set('shipmentDate', shipmentDate);
  return handleResponse(await apiFetch(`/api/v1/outsource/shipments/input-preview?${search.toString()}`));
}

export async function fetchOutsourcingShipments(
  params?: OutsourcingShipmentListParams,
): Promise<OutsourcingShipment[]> {
  return handleResponse(await apiFetch(`/api/v1/outsource/shipments${buildQuery(params)}`));
}

export async function createOutsourcingShipment(payload: {
  shipmentDate: string;
  lines: Array<{ orderLineId: number; shipmentQty: number }>;
}): Promise<OutsourcingShipment> {
  return handleResponse(
    await apiFetch('/api/v1/outsource/shipments', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function fetchOutsourceAdvanceProcessOptions(
  partnerId: number,
  parentItemId: number,
  refDate?: string,
): Promise<OutsourceAdvanceProcessOption[]> {
  const search = new URLSearchParams({
    partnerId: String(partnerId),
    parentItemId: String(parentItemId),
  });
  if (refDate) search.set('refDate', refDate);
  return handleResponse(
    await apiFetch(`/api/v1/outsource/shipments/advance-process-options?${search.toString()}`),
  );
}

export async function fetchOutsourcingAdvanceInputPreview(
  partnerId: number,
  parentItemId: number,
  beginProcessCodeId: number,
  endProcessCodeId: number,
  referenceQty: number,
  shipmentDate?: string,
): Promise<OutsourcingShipmentInputPreview> {
  const search = new URLSearchParams({
    partnerId: String(partnerId),
    parentItemId: String(parentItemId),
    beginProcessCodeId: String(beginProcessCodeId),
    endProcessCodeId: String(endProcessCodeId),
    referenceQty: String(referenceQty),
  });
  if (shipmentDate) search.set('shipmentDate', shipmentDate);
  return handleResponse(
    await apiFetch(`/api/v1/outsource/shipments/advance-input-preview?${search.toString()}`),
  );
}

export interface OutsourcingAdvanceInputLine {
  itemId: number;
  itemCompositionId?: number | null;
  issueQty: number;
  sourceLocationCode: string;
  sourceProcessId?: number | null;
  inputProcessId: number;
}

export async function createOutsourcingAdvanceShipment(payload: {
  shipmentDate: string;
  partnerId: number;
  lines: Array<{
    parentItemId: number;
    beginProcessCodeId: number;
    endProcessCodeId: number;
    referenceQty: number;
    inputLines?: OutsourcingAdvanceInputLine[];
  }>;
}): Promise<OutsourcingShipment> {
  return handleResponse(
    await apiFetch('/api/v1/outsource/shipments/advance', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function cancelOutsourcingShipment(id: number): Promise<void> {
  await handleResponse(
    await apiFetch(`/api/v1/outsource/shipments/${id}/cancel`, { method: 'POST' }),
  );
}
