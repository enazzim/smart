import { apiFetch, handleResponse } from './http';

export type CheckDistinction = 'NONE' | 'INSPECTION';

export interface PurchaseReceiptCandidate {
  purchaseOrderId: number;
  purchaseOrderLineId: number;
  orderNo: string;
  orderDate: string;
  partnerId: number;
  partnerName: string;
  itemId: number;
  itemNum: string;
  itemName: string;
  checkDistinction: CheckDistinction;
  lotTracked: boolean;
  orderQty: number;
  receivedQty: number;
  remainQty: number;
  waitingInspectionQty: number;
  unitPrice: number;
  requestedDeliveryDate?: string | null;
}

export interface PurchaseReceiptCandidateParams {
  partnerName?: string;
  orderNo?: string;
  orderDateFrom?: string;
  orderDateTo?: string;
  itemNum?: string;
  itemName?: string;
  itemPropertyScope?: 'GENERAL' | 'SUB_MATERIAL';
}

export interface CreatePurchaseReceiptLineRequest {
  purchaseOrderLineId: number;
  receiptQty: number;
  lotNo?: string;
  autoGenerateLot?: boolean;
}

export interface CreatePurchaseReceiptRequest {
  receiptDate: string;
  fiscalYear?: number;
  fiscalMonth?: number;
  lines: CreatePurchaseReceiptLineRequest[];
  allowOverQty?: boolean;
}

export interface PurchaseReceiptLine {
  id: number;
  lineNo: number;
  purchaseOrderLineId: number;
  itemId: number;
  itemNum: string;
  itemName: string;
  receiptQty: number;
  postedQty: number;
  unitPrice: number;
  amount: number;
  qualityInspectionId?: number | null;
  postedImmediately: boolean;
}

export interface PurchaseReceipt {
  id: number;
  receiptNo: string;
  partnerId: number;
  partnerName: string;
  receiptDate: string;
  purchaseOrderId?: number | null;
  status: string;
  createdAt: string;
  createdBy?: string | null;
  lines: PurchaseReceiptLine[];
}

export interface PurchaseReceiptListParams {
  partnerName?: string;
  receiptDateFrom?: string;
  receiptDateTo?: string;
  itemNum?: string;
  itemName?: string;
  itemPropertyScope?: 'GENERAL' | 'SUB_MATERIAL';
}

function toCandidateQuery(params: PurchaseReceiptCandidateParams): string {
  const search = new URLSearchParams();
  if (params.partnerName) search.set('partnerName', params.partnerName);
  if (params.orderNo) search.set('orderNo', params.orderNo);
  if (params.orderDateFrom) search.set('orderDateFrom', params.orderDateFrom);
  if (params.orderDateTo) search.set('orderDateTo', params.orderDateTo);
  if (params.itemNum) search.set('itemNum', params.itemNum);
  if (params.itemName) search.set('itemName', params.itemName);
  if (params.itemPropertyScope) search.set('itemPropertyScope', params.itemPropertyScope);
  const q = search.toString();
  return q ? `?${q}` : '';
}

function toReceiptListQuery(params: PurchaseReceiptListParams): string {
  const search = new URLSearchParams();
  if (params.partnerName) search.set('partnerName', params.partnerName);
  if (params.receiptDateFrom) search.set('receiptDateFrom', params.receiptDateFrom);
  if (params.receiptDateTo) search.set('receiptDateTo', params.receiptDateTo);
  if (params.itemNum) search.set('itemNum', params.itemNum);
  if (params.itemName) search.set('itemName', params.itemName);
  if (params.itemPropertyScope) search.set('itemPropertyScope', params.itemPropertyScope);
  const q = search.toString();
  return q ? `?${q}` : '';
}

export async function fetchPurchaseReceiptCandidates(
  params: PurchaseReceiptCandidateParams = {},
): Promise<PurchaseReceiptCandidate[]> {
  return handleResponse(
    await apiFetch(`/api/v1/purchase/receipt-candidates${toCandidateQuery(params)}`),
  );
}

export async function fetchPurchaseReceipts(
  params: PurchaseReceiptListParams = {},
): Promise<PurchaseReceipt[]> {
  return handleResponse(await apiFetch(`/api/v1/purchase/receipts${toReceiptListQuery(params)}`));
}

export async function createPurchaseReceipt(body: CreatePurchaseReceiptRequest): Promise<PurchaseReceipt> {
  return handleResponse(
    await apiFetch('/api/v1/purchase/receipts', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body),
    }),
  );
}

export async function cancelPurchaseReceipt(id: number): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`/api/v1/purchase/receipts/${id}/cancel`, {
      method: 'POST',
    }),
  );
}
