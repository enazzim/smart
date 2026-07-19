import { apiFetch, handleResponse } from './http';

export type PartnerPaymentStatus = 'ISSUED' | 'CANCELLED';
export type PartnerPaymentCostCategory = 'PURCHASE' | 'OUTSOURCE';
export type PartnerPaymentKind = 'NORMAL' | 'PREPAID';

export interface PartnerPaymentCandidate {
  partnerId: number;
  partnerName: string;
  partnerBusinessRegNo: string;
  purchasePayableAmount: number;
  outsourcePayableAmount: number;
  totalPayableAmount: number;
  paidAmount: number;
  unpaidAmount: number;
  payable: boolean;
}

export interface PartnerPaymentLine {
  id: number;
  itemId: number;
  itemNo: string;
  itemName: string;
  purchaseOrderLineId?: number | null;
  outsourcingOrderLineId?: number | null;
  orderNo?: string | null;
  supplyAmount: number;
  vatAmount: number;
  totalAmount: number;
  offsetAmount: number;
  remainingAmount: number;
}

export interface PartnerPayment {
  id: number;
  paymentNo: string;
  partnerId: number;
  partnerName: string;
  partnerBusinessRegNo: string;
  paymentDate: string;
  costCategory: PartnerPaymentCostCategory;
  costCategoryLabel: string;
  paymentKind: PartnerPaymentKind;
  paymentKindLabel: string;
  supplyAmount: number;
  vatAmount: number;
  totalAmount: number;
  paymentMethod?: string | null;
  remark?: string | null;
  status: PartnerPaymentStatus;
  statusLabel: string;
  createdAt: string;
  createdBy?: string | null;
  cancelable: boolean;
  lines: PartnerPaymentLine[];
  lineSummary?: string | null;
}

export interface PrepaidBalance {
  partnerId: number;
  partnerName: string;
  itemId: number;
  itemNo: string;
  itemName: string;
  costCategory: PartnerPaymentCostCategory;
  costCategoryLabel: string;
  prepaidIn: number;
  prepaidOut: number;
  prepaidRemaining: number;
}

export interface PrepaidOrderLineCandidate {
  costCategory: PartnerPaymentCostCategory;
  costCategoryLabel: string;
  orderLineId: number;
  orderId: number;
  orderNo: string;
  lineNo: number;
  partnerId: number;
  partnerName: string;
  itemId: number;
  itemNo: string;
  itemName: string;
  orderDate: string;
  orderAmount: number;
  prepaidLinkedAmount: number;
  remainingAmount: number;
}

export interface PartnerPaymentCandidateParams {
  partnerName?: string;
  includeZeroUnpaid?: boolean;
}

export interface PartnerPaymentListParams {
  paymentDateFrom?: string;
  paymentDateTo?: string;
  paymentNo?: string;
  partnerName?: string;
  status?: PartnerPaymentStatus;
  excludeCancelled?: boolean;
}

export interface CreatePartnerPaymentLinePayload {
  itemId: number;
  purchaseOrderLineId?: number | null;
  outsourcingOrderLineId?: number | null;
  supplyAmount: number;
  vatAmount?: number;
}

function buildCandidateQuery(params?: PartnerPaymentCandidateParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.partnerName?.trim()) search.set('partnerName', params.partnerName.trim());
  if (params.includeZeroUnpaid) search.set('includeZeroUnpaid', 'true');
  const query = search.toString();
  return query ? `?${query}` : '';
}

function buildListQuery(params?: PartnerPaymentListParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.paymentDateFrom) search.set('paymentDateFrom', params.paymentDateFrom);
  if (params.paymentDateTo) search.set('paymentDateTo', params.paymentDateTo);
  if (params.paymentNo?.trim()) search.set('paymentNo', params.paymentNo.trim());
  if (params.partnerName?.trim()) search.set('partnerName', params.partnerName.trim());
  if (params.status) search.set('status', params.status);
  if (params.excludeCancelled !== undefined) search.set('excludeCancelled', String(params.excludeCancelled));
  const query = search.toString();
  return query ? `?${query}` : '';
}

export async function fetchPartnerPaymentCandidates(
  params?: PartnerPaymentCandidateParams,
): Promise<PartnerPaymentCandidate[]> {
  return handleResponse(await apiFetch(`/api/v1/purchase/payments/candidates${buildCandidateQuery(params)}`));
}

export async function fetchPartnerPayments(params?: PartnerPaymentListParams): Promise<PartnerPayment[]> {
  return handleResponse(await apiFetch(`/api/v1/purchase/payments${buildListQuery(params)}`));
}

export async function fetchPrepaidBalances(params?: {
  partnerId?: number;
  costCategory?: PartnerPaymentCostCategory;
}): Promise<PrepaidBalance[]> {
  const search = new URLSearchParams();
  if (params?.partnerId != null) search.set('partnerId', String(params.partnerId));
  if (params?.costCategory) search.set('costCategory', params.costCategory);
  const query = search.toString();
  return handleResponse(
    await apiFetch(`/api/v1/purchase/payments/prepaid-balances${query ? `?${query}` : ''}`),
  );
}

export async function fetchPrepaidOrderLineCandidates(params?: {
  partnerId?: number;
  costCategory?: PartnerPaymentCostCategory;
  itemNo?: string;
  itemName?: string;
  orderNo?: string;
}): Promise<PrepaidOrderLineCandidate[]> {
  const search = new URLSearchParams();
  if (params?.partnerId != null) search.set('partnerId', String(params.partnerId));
  if (params?.costCategory) search.set('costCategory', params.costCategory);
  if (params?.itemNo?.trim()) search.set('itemNo', params.itemNo.trim());
  if (params?.itemName?.trim()) search.set('itemName', params.itemName.trim());
  if (params?.orderNo?.trim()) search.set('orderNo', params.orderNo.trim());
  const query = search.toString();
  return handleResponse(
    await apiFetch(`/api/v1/purchase/payments/order-line-candidates${query ? `?${query}` : ''}`),
  );
}

export async function createPartnerPayment(payload: {
  partnerId: number;
  paymentDate: string;
  costCategory: PartnerPaymentCostCategory;
  paymentKind?: PartnerPaymentKind;
  supplyAmount?: number;
  vatAmount?: number;
  paymentMethod?: string;
  remark?: string;
  lines?: CreatePartnerPaymentLinePayload[];
}): Promise<PartnerPayment> {
  return handleResponse(
    await apiFetch('/api/v1/purchase/payments', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function cancelPartnerPayment(id: number): Promise<void> {
  await handleResponse(await apiFetch(`/api/v1/purchase/payments/${id}/cancel`, { method: 'POST' }));
}
