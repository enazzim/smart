import { apiFetch, handleResponse } from './http';

export type PayableApprovalLedgerKind = 'PURCHASE' | 'OUTSOURCE' | 'ETC_CLAIM' | 'DEFECT_CLAIM';
export type PayableApprovalStatus = 'PENDING' | 'APPROVED';
/** 전체 / 구매입고 / 외주입고 / 기타구매입고 / 기타공제 */
export type PayableApprovalCategory = 'ALL' | 'PURCHASE' | 'OUTSOURCE' | 'ETC' | 'CLAIM';

export interface PayableApprovalRow {
  ledgerKind: PayableApprovalLedgerKind;
  historyId: number;
  partnerId: number;
  partnerName: string;
  receiptDate: string;
  itemNo: string;
  itemName: string;
  drawingNo: string;
  processName: string;
  qty: number;
  standardUnitPrice: number;
  unitPrice: number;
  amount: number;
  fiscalYear: number;
  fiscalMonth: number;
  categoryLabel: string;
  approvalStatus: PayableApprovalStatus;
  approvedAt?: string | null;
  approvedByName?: string | null;
  approvalCancelledAt?: string | null;
  approvalCancelledByName?: string | null;
}

export interface PayableApprovalSearchParams {
  partnerName?: string;
  itemNo?: string;
  itemName?: string;
  receiptDateFrom?: string;
  receiptDateTo?: string;
  fiscalYear?: number;
  fiscalMonth?: number;
  category?: PayableApprovalCategory;
}

function buildQuery(params: PayableApprovalSearchParams): string {
  const search = new URLSearchParams();
  if (params.partnerName?.trim()) search.set('partnerName', params.partnerName.trim());
  if (params.itemNo?.trim()) search.set('itemNo', params.itemNo.trim());
  if (params.itemName?.trim()) search.set('itemName', params.itemName.trim());
  if (params.receiptDateFrom) search.set('receiptDateFrom', params.receiptDateFrom);
  if (params.receiptDateTo) search.set('receiptDateTo', params.receiptDateTo);
  if (params.fiscalYear != null) search.set('fiscalYear', String(params.fiscalYear));
  if (params.fiscalMonth != null) search.set('fiscalMonth', String(params.fiscalMonth));
  if (params.category && params.category !== 'ALL') search.set('category', params.category);
  const query = search.toString();
  return query ? `?${query}` : '';
}

export async function fetchPendingPayableApprovals(
  params: PayableApprovalSearchParams = {},
): Promise<PayableApprovalRow[]> {
  return handleResponse(
    await apiFetch(`/api/v1/purchase/payable-approvals/pending${buildQuery(params)}`),
  );
}

export async function fetchApprovedPayableApprovals(
  params: PayableApprovalSearchParams = {},
): Promise<PayableApprovalRow[]> {
  return handleResponse(
    await apiFetch(`/api/v1/purchase/payable-approvals/approved${buildQuery(params)}`),
  );
}

export async function approvePayableItems(
  items: { ledgerKind: PayableApprovalLedgerKind; historyId: number }[],
): Promise<void> {
  await handleResponse(
    await apiFetch('/api/v1/purchase/payable-approvals/approve', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ items }),
    }),
  );
}

export async function cancelPayableApproval(
  items: { ledgerKind: PayableApprovalLedgerKind; historyId: number }[],
): Promise<void> {
  await handleResponse(
    await apiFetch('/api/v1/purchase/payable-approvals/cancel-approval', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ items }),
    }),
  );
}

export async function updatePayableApprovalFiscalPeriod(payload: {
  ledgerKind: PayableApprovalLedgerKind;
  historyId: number;
  fiscalYear: number;
  fiscalMonth: number;
}): Promise<void> {
  await handleResponse(
    await apiFetch('/api/v1/purchase/payable-approvals/fiscal-period', {
      method: 'PATCH',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}
