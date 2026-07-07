import { apiFetch, handleResponse } from './http';

export type PartnerPaymentStatus = 'ISSUED' | 'CANCELLED';
export type PartnerPaymentCostCategory = 'PURCHASE' | 'OUTSOURCE';

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

export interface PartnerPayment {
  id: number;
  paymentNo: string;
  partnerId: number;
  partnerName: string;
  partnerBusinessRegNo: string;
  paymentDate: string;
  costCategory: PartnerPaymentCostCategory;
  costCategoryLabel: string;
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
}

export interface PartnerPaymentCandidateParams {
  partnerName?: string;
}

export interface PartnerPaymentListParams {
  paymentDateFrom?: string;
  paymentDateTo?: string;
  paymentNo?: string;
  partnerName?: string;
  status?: PartnerPaymentStatus;
  excludeCancelled?: boolean;
}

function buildCandidateQuery(params?: PartnerPaymentCandidateParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.partnerName?.trim()) search.set('partnerName', params.partnerName.trim());
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

export async function createPartnerPayment(payload: {
  partnerId: number;
  paymentDate: string;
  costCategory: PartnerPaymentCostCategory;
  supplyAmount: number;
  vatAmount?: number;
  paymentMethod?: string;
  remark?: string;
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
