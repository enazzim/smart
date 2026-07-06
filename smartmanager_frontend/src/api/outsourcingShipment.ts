import { apiFetch, handleResponse } from './http';

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
  orderLineId: number;
  orderNo: string;
  itemNo: string;
  shipmentQty: number;
  lines: OutsourcingShipmentInputPreviewLine[];
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
  orderLineId: number;
  orderNo: string;
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

export async function cancelOutsourcingShipment(id: number): Promise<void> {
  await handleResponse(
    await apiFetch(`/api/v1/outsource/shipments/${id}/cancel`, { method: 'POST' }),
  );
}
