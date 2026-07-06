import { apiFetch, handleResponse } from './http';

export type CheckDistinction = 'NONE' | 'INSPECTION';

export type OutsourcingReceiptStatus = 'REGISTERED' | 'PARTIALLY_POSTED' | 'POSTED' | 'CANCELLED';

export interface OutsourcingReceiptCandidate {
  outsourcingOrderLineId: number;
  outsourcingOrderId: number;
  orderNo: string;
  orderDate: string;
  partnerId: number;
  partnerName: string;
  itemId: number;
  itemNo: string;
  itemName: string;
  processName: string;
  checkDistinction: CheckDistinction;
  orderQty: number;
  shippedQty: number;
  receivedQty: number;
  waitingInspectionQty: number;
  remainQty: number;
  unitPrice: number;
  requestedDeliveryDate?: string | null;
}

export interface OutsourcingReceiptCandidateParams {
  partnerName?: string;
  orderNo?: string;
  orderDateFrom?: string;
  orderDateTo?: string;
  itemNo?: string;
  itemName?: string;
}

export interface OutsourcingReceiptLine {
  id: number;
  lineNo: number;
  outsourcingOrderLineId: number;
  itemId: number;
  itemNo: string;
  itemName: string;
  receiptQty: number;
  postedQty: number;
  unitPrice: number;
  amount: number;
  qualityInspectionId?: number | null;
  stockPosted: boolean;
}

export interface OutsourcingReceipt {
  id: number;
  receiptNo: string;
  partnerId: number;
  partnerName: string;
  receiptDate: string;
  outsourcingOrderId?: number | null;
  status: OutsourcingReceiptStatus;
  createdAt: string;
  createdBy?: string | null;
  lines: OutsourcingReceiptLine[];
}

export interface OutsourcingReceiptListParams {
  partnerName?: string;
  receiptDateFrom?: string;
  receiptDateTo?: string;
  itemNo?: string;
  itemName?: string;
}

function toCandidateQuery(params?: OutsourcingReceiptCandidateParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.partnerName) search.set('partnerName', params.partnerName);
  if (params.orderNo) search.set('orderNo', params.orderNo);
  if (params.orderDateFrom) search.set('orderDateFrom', params.orderDateFrom);
  if (params.orderDateTo) search.set('orderDateTo', params.orderDateTo);
  if (params.itemNo) search.set('itemNo', params.itemNo);
  if (params.itemName) search.set('itemName', params.itemName);
  const q = search.toString();
  return q ? `?${q}` : '';
}

function toReceiptListQuery(params?: OutsourcingReceiptListParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.partnerName) search.set('partnerName', params.partnerName);
  if (params.receiptDateFrom) search.set('receiptDateFrom', params.receiptDateFrom);
  if (params.receiptDateTo) search.set('receiptDateTo', params.receiptDateTo);
  if (params.itemNo) search.set('itemNo', params.itemNo);
  if (params.itemName) search.set('itemName', params.itemName);
  const q = search.toString();
  return q ? `?${q}` : '';
}

export async function fetchOutsourcingReceiptCandidates(
  params?: OutsourcingReceiptCandidateParams,
): Promise<OutsourcingReceiptCandidate[]> {
  return handleResponse(
    await apiFetch(`/api/v1/outsource/receipts/candidates${toCandidateQuery(params)}`),
  );
}

export async function fetchOutsourcingReceipts(
  params?: OutsourcingReceiptListParams,
): Promise<OutsourcingReceipt[]> {
  return handleResponse(await apiFetch(`/api/v1/outsource/receipts${toReceiptListQuery(params)}`));
}

export async function createOutsourcingReceipt(payload: {
  receiptDate: string;
  lines: Array<{ outsourcingOrderLineId: number; receiptQty: number }>;
}): Promise<OutsourcingReceipt> {
  return handleResponse(
    await apiFetch('/api/v1/outsource/receipts', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function cancelOutsourcingReceipt(id: number): Promise<void> {
  await handleResponse(
    await apiFetch(`/api/v1/outsource/receipts/${id}/cancel`, { method: 'POST' }),
  );
}
