import { apiFetch, handleResponse } from './http';

export type PurchaseOrderSourceType = 'MRP' | 'SALES_ORDER' | 'MANUAL';
export type PurchaseOrderStatus = 'DRAFT' | 'CONFIRMED' | 'IN_PROGRESS' | 'RECEIVED' | 'CANCELLED';

export interface PurchaseOrderLine {
  id: number;
  lineNo: number;
  itemId: number;
  itemNo: string;
  itemName: string;
  propertyClassification: string;
  orderQty: number;
  receivedQty: number;
  waitingInspectionQty: number;
  unitPrice: number;
  amount: number;
  requirementLineId?: number | null;
  planNo?: string | null;
  runNo?: string | null;
  requestedDeliveryDate?: string | null;
}

export interface PurchaseOrder {
  id: number;
  orderNo: string;
  partnerId: number;
  partnerName: string;
  partnerBusinessRegNo: string;
  orderDate: string;
  sourceType: PurchaseOrderSourceType;
  sourceTypeLabel: string;
  status: PurchaseOrderStatus;
  statusLabel: string;
  cancelable: boolean;
  createdAt: string;
  createdBy?: string | null;
  lines: PurchaseOrderLine[];
}

export interface MrpPurchaseCandidateVendor {
  partnerId: number;
  partnerName: string;
  businessRegNo: string;
  orderRate: number;
  orderQty: number;
  unitPrice: number;
  amount: number;
  leadTimeDays?: number | null;
  requestedDeliveryDate?: string | null;
}

export interface MrpPurchaseCandidate {
  requirementLineId: number;
  mrpRunId: number;
  runNo: string;
  productionPlanId: number;
  planNo: string;
  componentItemId: number;
  componentItemNo: string;
  componentItemName: string;
  componentPropertyClassification: string;
  grossQty: number;
  orderedQty: number;
  suggestedQty: number;
  unit: string;
  createdAt: string;
  orderRateTotal: number;
  orderable: boolean;
  orderableMessage?: string | null;
  vendors: MrpPurchaseCandidateVendor[];
}

export interface CreatePurchaseOrderFromMrpLineRequest {
  requirementLineId: number;
  orderQty: number;
  unitPrice?: number;
  requestedDeliveryDate?: string | null;
}

export interface CreatePurchaseOrderFromMrpRequest {
  partnerId: number;
  orderDate?: string;
  lines: CreatePurchaseOrderFromMrpLineRequest[];
}

export interface PurchaseOrderListParams {
  orderDateFrom?: string;
  orderDateTo?: string;
  partnerName?: string;
  orderNo?: string;
  status?: PurchaseOrderStatus;
  excludeCancelled?: boolean;
}

export async function fetchPurchaseOrders(params?: PurchaseOrderListParams): Promise<PurchaseOrder[]> {
  const search = new URLSearchParams();
  if (params?.orderDateFrom) {
    search.set('orderDateFrom', params.orderDateFrom);
  }
  if (params?.orderDateTo) {
    search.set('orderDateTo', params.orderDateTo);
  }
  if (params?.partnerName?.trim()) {
    search.set('partnerName', params.partnerName.trim());
  }
  if (params?.orderNo?.trim()) {
    search.set('orderNo', params.orderNo.trim());
  }
  if (params?.status) {
    search.set('status', params.status);
  }
  if (params?.excludeCancelled !== undefined) {
    search.set('excludeCancelled', String(params.excludeCancelled));
  }
  const query = search.toString();
  const res = await apiFetch(`/api/v1/purchase/orders${query ? `?${query}` : ''}`);
  return handleResponse(res);
}

export async function fetchMrpPurchaseCandidates(orderDate?: string): Promise<MrpPurchaseCandidate[]> {
  const query = orderDate ? `?orderDate=${encodeURIComponent(orderDate)}` : '';
  const res = await apiFetch(`/api/v1/purchase/orders/mrp-candidates${query}`);
  return handleResponse(res);
}

export async function fetchPurchaseOrderPrintHtml(orderId: number): Promise<string> {
  const res = await apiFetch(`/api/v1/purchase/orders/${orderId}/print`);
  if (!res.ok) {
    await handleResponse(res);
  }
  return res.text();
}

export async function openPurchaseOrderPrint(orderId: number): Promise<void> {
  const html = await fetchPurchaseOrderPrintHtml(orderId);
  const printWindow = window.open('', '_blank', 'width=920,height=720');
  if (!printWindow) {
    throw new Error('팝업이 차단되었습니다. 브라우저에서 팝업을 허용해 주세요.');
  }
  printWindow.document.write(html);
  printWindow.document.close();
  printWindow.focus();
}

export async function createPurchaseOrderFromMrp(
  request: CreatePurchaseOrderFromMrpRequest,
): Promise<PurchaseOrder> {
  const res = await apiFetch('/api/v1/purchase/orders/from-mrp', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(request),
  });
  return handleResponse(res);
}

export async function cancelPurchaseOrder(orderId: number): Promise<void> {
  const res = await apiFetch(`/api/v1/purchase/orders/${orderId}/cancel`, { method: 'POST' });
  if (!res.ok) {
    await handleResponse(res);
  }
}
