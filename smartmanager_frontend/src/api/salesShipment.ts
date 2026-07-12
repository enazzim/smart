import { apiFetch, handleResponse } from './http';

export type SalesShipmentStatus = 'ISSUED' | 'CANCELLED';

export interface SalesShipmentCandidate {
  orderLineId: number;
  orderId: number;
  orderNo: string;
  orderDate: string;
  partnerId: number;
  partnerName: string;
  itemId: number;
  itemNo: string;
  itemName: string;
  orderQty: number;
  shippedQty: number;
  remainingQty: number;
  salesOnHandQty: number;
  wipOnHandQty: number;
  shippable: boolean;
  shippableMessage?: string | null;
}

export interface SalesShipmentLine {
  id: number;
  lineNo: number;
  salesOrderLineId: number;
  orderNo: string;
  partnerId: number;
  partnerName: string;
  itemNo: string;
  itemName: string;
  shipmentQty: number;
  unitPrice: number;
  amount: number;
}

export interface SalesShipment {
  id: number;
  shipmentNo: string;
  partnerId: number;
  partnerName: string;
  shipmentDate: string;
  salesOrderId?: number | null;
  status: SalesShipmentStatus;
  statusLabel: string;
  createdAt: string;
  createdBy?: string | null;
  cancelable: boolean;
  lines: SalesShipmentLine[];
}

export interface SalesShipmentCandidateParams {
  partnerName?: string;
  orderNo?: string;
  orderDateFrom?: string;
  orderDateTo?: string;
  itemNum?: string;
  itemName?: string;
}

export interface SalesShipmentListParams {
  shipmentDateFrom?: string;
  shipmentDateTo?: string;
  shipmentNo?: string;
  partnerName?: string;
  status?: SalesShipmentStatus;
  excludeCancelled?: boolean;
}

function buildCandidateQuery(params?: SalesShipmentCandidateParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.partnerName?.trim()) search.set('partnerName', params.partnerName.trim());
  if (params.orderNo?.trim()) search.set('orderNo', params.orderNo.trim());
  if (params.orderDateFrom) search.set('orderDateFrom', params.orderDateFrom);
  if (params.orderDateTo) search.set('orderDateTo', params.orderDateTo);
  if (params.itemNum?.trim()) search.set('itemNum', params.itemNum.trim());
  if (params.itemName?.trim()) search.set('itemName', params.itemName.trim());
  const query = search.toString();
  return query ? `?${query}` : '';
}

function buildListQuery(params?: SalesShipmentListParams): string {
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

export async function fetchSalesShipmentCandidates(
  params?: SalesShipmentCandidateParams,
): Promise<SalesShipmentCandidate[]> {
  return handleResponse(await apiFetch(`/api/v1/sales/shipments/candidates${buildCandidateQuery(params)}`));
}

export async function fetchSalesShipments(params?: SalesShipmentListParams): Promise<SalesShipment[]> {
  return handleResponse(await apiFetch(`/api/v1/sales/shipments${buildListQuery(params)}`));
}

export async function createSalesShipment(payload: {
  shipmentDate: string;
  lines: Array<{ salesOrderLineId: number; shipmentQty: number }>;
}): Promise<SalesShipment> {
  return handleResponse(
    await apiFetch('/api/v1/sales/shipments', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function cancelSalesShipment(id: number): Promise<void> {
  await handleResponse(
    await apiFetch(`/api/v1/sales/shipments/${id}/cancel`, { method: 'POST' }),
  );
}
