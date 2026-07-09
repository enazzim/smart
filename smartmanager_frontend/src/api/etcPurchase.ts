import { apiFetch, handleResponse } from './http';

export type EtcPurchaseOrderStatus = 'WAITING' | 'IN_PROGRESS' | 'COMPLETED';

export interface EtcPurchaseCategory {
  id: number;
  largeCode: string;
  largeName: string;
  smallCode: string;
  smallName: string;
  usageType: string;
}

export interface EtcPurchaseOrder {
  id: number;
  orderNo: string;
  itemName: string;
  partnerId: number;
  partnerName: string;
  partnerBusinessRegNo: string;
  unitPrice: number;
  orderQty: number;
  remainQty: number;
  amount: number;
  requestedDeliveryDate: string;
  categoryCodeId: number | null;
  categoryName: string | null;
  status: EtcPurchaseOrderStatus;
  statusLabel: string;
  orderDate: string;
  editable: boolean;
}

export interface EtcPurchaseReceiptCandidate {
  etcPurchaseOrderId: number;
  orderNo: string;
  itemName: string;
  partnerId: number;
  partnerName: string;
  unitPrice: number;
  orderQty: number;
  remainQty: number;
  amount: number;
  requestedDeliveryDate: string;
  categoryName: string | null;
  status: EtcPurchaseOrderStatus;
  statusLabel: string;
}

export interface EtcPurchaseReceipt {
  id: number;
  receiptNo: string;
  etcPurchaseOrderId: number;
  orderNo: string;
  partnerId: number;
  partnerName: string;
  itemName: string;
  receiptQty: number;
  unitPrice: number;
  amount: number;
  receiptDate: string;
  fiscalYear: number;
  fiscalMonth: number;
}

export interface EtcPurchaseOrderListParams {
  itemName?: string;
  partnerName?: string;
  deliveryFrom?: string;
  deliveryTo?: string;
}

export interface EtcPurchaseReceiptCandidateParams {
  itemName?: string;
  partnerName?: string;
  deliveryFrom?: string;
  deliveryTo?: string;
}

export interface EtcPurchaseReceiptListParams {
  itemName?: string;
  partnerName?: string;
  receiptFrom?: string;
  receiptTo?: string;
}

export interface CreateEtcPurchaseOrderRequest {
  itemName: string;
  partnerId: number;
  unitPrice: number;
  orderQty: number;
  requestedDeliveryDate: string;
  categoryCodeId?: number | null;
  orderDate?: string;
}

export interface UpdateEtcPurchaseOrderRequest {
  itemName: string;
  partnerId: number;
  unitPrice: number;
  orderQty: number;
  requestedDeliveryDate: string;
  categoryCodeId?: number | null;
}

const API_BASE = '/api/v1/purchase/etc';

function buildQuery(params: Record<string, string | undefined>): string {
  const search = new URLSearchParams();
  for (const [key, value] of Object.entries(params)) {
    if (value) {
      search.set(key, value);
    }
  }
  const query = search.toString();
  return query ? `?${query}` : '';
}

export async function fetchEtcPurchaseCategories(): Promise<EtcPurchaseCategory[]> {
  return handleResponse<EtcPurchaseCategory[]>(await apiFetch(`${API_BASE}/categories`));
}

export async function fetchEtcPurchaseOrders(params?: EtcPurchaseOrderListParams): Promise<EtcPurchaseOrder[]> {
  const query = buildQuery({
    itemName: params?.itemName,
    partnerName: params?.partnerName,
    deliveryFrom: params?.deliveryFrom,
    deliveryTo: params?.deliveryTo,
  });
  return handleResponse<EtcPurchaseOrder[]>(await apiFetch(`${API_BASE}/orders${query}`));
}

export async function createEtcPurchaseOrder(request: CreateEtcPurchaseOrderRequest): Promise<EtcPurchaseOrder> {
  return handleResponse<EtcPurchaseOrder>(
    await apiFetch(`${API_BASE}/orders`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request),
    }),
  );
}

export async function updateEtcPurchaseOrder(
  id: number,
  request: UpdateEtcPurchaseOrderRequest,
): Promise<EtcPurchaseOrder> {
  return handleResponse<EtcPurchaseOrder>(
    await apiFetch(`${API_BASE}/orders/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request),
    }),
  );
}

export async function deleteEtcPurchaseOrder(id: number): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API_BASE}/orders/${id}`, { method: 'DELETE' }),
  );
}

export async function fetchEtcPurchaseReceiptCandidates(
  params?: EtcPurchaseReceiptCandidateParams,
): Promise<EtcPurchaseReceiptCandidate[]> {
  const query = buildQuery({
    itemName: params?.itemName,
    partnerName: params?.partnerName,
    deliveryFrom: params?.deliveryFrom,
    deliveryTo: params?.deliveryTo,
  });
  return handleResponse<EtcPurchaseReceiptCandidate[]>(
    await apiFetch(`${API_BASE}/receipt-candidates${query}`),
  );
}

export async function fetchEtcPurchaseReceipts(
  params?: EtcPurchaseReceiptListParams,
): Promise<EtcPurchaseReceipt[]> {
  const query = buildQuery({
    itemName: params?.itemName,
    partnerName: params?.partnerName,
    receiptFrom: params?.receiptFrom,
    receiptTo: params?.receiptTo,
  });
  return handleResponse<EtcPurchaseReceipt[]>(await apiFetch(`${API_BASE}/receipts${query}`));
}

export async function createEtcPurchaseReceipts(request: {
  receiptDate: string;
  lines: { etcPurchaseOrderId: number; receiptQty: number }[];
}): Promise<EtcPurchaseReceipt[]> {
  return handleResponse<EtcPurchaseReceipt[]>(
    await apiFetch(`${API_BASE}/receipts`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request),
    }),
  );
}

export async function updateEtcPurchaseReceipt(
  id: number,
  request: { receiptDate: string; receiptQty: number },
): Promise<EtcPurchaseReceipt> {
  return handleResponse<EtcPurchaseReceipt>(
    await apiFetch(`${API_BASE}/receipts/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request),
    }),
  );
}

export async function cancelEtcPurchaseReceipt(id: number): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API_BASE}/receipts/${id}`, { method: 'DELETE' }),
  );
}
