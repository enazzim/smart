import { apiFetch, handleResponse } from './http';

export type SalesRevenueStatus = 'ISSUED' | 'CANCELLED';

export interface SalesRevenueCandidate {
  shipmentLineId: number;
  shipmentId: number;
  shipmentNo: string;
  shipmentDate: string;
  partnerId: number;
  partnerName: string;
  orderLineId: number;
  orderNo: string;
  itemId: number;
  itemNo: string;
  itemName: string;
  shippedQty: number;
  invoicedQty: number;
  remainingQty: number;
  deliveryOnHandQty: number;
  unitPrice: number;
  billable: boolean;
  billableMessage?: string | null;
  lotTracked: boolean;
  shipmentLotId?: number | null;
}

export interface SalesRevenueLine {
  id: number;
  lineNo: number;
  salesShipmentLineId: number;
  shipmentNo: string;
  orderNo: string;
  partnerId: number;
  partnerName: string;
  itemNo: string;
  itemName: string;
  revenueQty: number;
  unitPrice: number;
  amount: number;
  lotId?: number | null;
}

export interface SalesRevenue {
  id: number;
  revenueNo: string;
  partnerId: number;
  partnerName: string;
  revenueDate: string;
  salesShipmentId?: number | null;
  status: SalesRevenueStatus;
  statusLabel: string;
  createdAt: string;
  createdBy?: string | null;
  cancelable: boolean;
  lines: SalesRevenueLine[];
}

export interface SalesRevenueCandidateParams {
  partnerName?: string;
  shipmentDateFrom?: string;
  shipmentDateTo?: string;
  itemId?: number;
}

export interface SalesRevenueListParams {
  revenueDateFrom?: string;
  revenueDateTo?: string;
  revenueNo?: string;
  partnerName?: string;
  itemId?: number;
  status?: SalesRevenueStatus;
  excludeCancelled?: boolean;
}

function buildCandidateQuery(params?: SalesRevenueCandidateParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.partnerName?.trim()) search.set('partnerName', params.partnerName.trim());
  if (params.shipmentDateFrom) search.set('shipmentDateFrom', params.shipmentDateFrom);
  if (params.shipmentDateTo) search.set('shipmentDateTo', params.shipmentDateTo);
  if (params.itemId) search.set('itemId', String(params.itemId));
  const query = search.toString();
  return query ? `?${query}` : '';
}

function buildListQuery(params?: SalesRevenueListParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.revenueDateFrom) search.set('revenueDateFrom', params.revenueDateFrom);
  if (params.revenueDateTo) search.set('revenueDateTo', params.revenueDateTo);
  if (params.revenueNo?.trim()) search.set('revenueNo', params.revenueNo.trim());
  if (params.partnerName?.trim()) search.set('partnerName', params.partnerName.trim());
  if (params.itemId) search.set('itemId', String(params.itemId));
  if (params.status) search.set('status', params.status);
  if (params.excludeCancelled !== undefined) search.set('excludeCancelled', String(params.excludeCancelled));
  const query = search.toString();
  return query ? `?${query}` : '';
}

export async function fetchSalesRevenueCandidates(
  params?: SalesRevenueCandidateParams,
): Promise<SalesRevenueCandidate[]> {
  return handleResponse(await apiFetch(`/api/v1/sales/revenues/candidates${buildCandidateQuery(params)}`));
}

export async function fetchSalesRevenues(params?: SalesRevenueListParams): Promise<SalesRevenue[]> {
  return handleResponse(await apiFetch(`/api/v1/sales/revenues${buildListQuery(params)}`));
}

export async function createSalesRevenue(payload: {
  revenueDate: string;
  lines: Array<{ salesShipmentLineId: number; revenueQty: number; lotId?: number | null }>;
}): Promise<SalesRevenue> {
  return handleResponse(
    await apiFetch('/api/v1/sales/revenues', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function cancelSalesRevenue(id: number): Promise<void> {
  await handleResponse(
    await apiFetch(`/api/v1/sales/revenues/${id}/cancel`, { method: 'POST' }),
  );
}

