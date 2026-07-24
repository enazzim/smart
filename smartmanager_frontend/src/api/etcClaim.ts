import { apiFetch, handleResponse } from './http';

export type EtcClaimRecognition = 'PENDING' | 'APPROVED';

export interface EtcClaim {
  id: number;
  partnerId: number;
  partnerName: string;
  partnerBusinessRegNo: string;
  receiptDate: string;
  reason: string;
  amount: number;
  fiscalYear: number;
  fiscalMonth: number;
  recognition: EtcClaimRecognition;
  createdBy: string;
  createdAt: string;
  editable: boolean;
}

export interface EtcClaimListParams {
  partnerId?: number;
  partnerName?: string;
  receiptDateFrom?: string;
  receiptDateTo?: string;
  reason?: string;
  registeredOn?: string;
}

export interface SaveEtcClaimRequest {
  partnerId: number;
  receiptDate: string;
  reason: string;
  amount: number;
}

const API_BASE = '/api/v1/purchase/etc-claims';

export async function fetchEtcClaims(params: EtcClaimListParams = {}): Promise<EtcClaim[]> {
  const search = new URLSearchParams();
  if (params.partnerId != null) search.set('partnerId', String(params.partnerId));
  if (params.partnerName?.trim()) search.set('partnerName', params.partnerName.trim());
  if (params.receiptDateFrom) search.set('receiptDateFrom', params.receiptDateFrom);
  if (params.receiptDateTo) search.set('receiptDateTo', params.receiptDateTo);
  if (params.reason?.trim()) search.set('reason', params.reason.trim());
  if (params.registeredOn) search.set('registeredOn', params.registeredOn);
  const query = search.toString();
  return handleResponse<EtcClaim[]>(await apiFetch(query ? `${API_BASE}?${query}` : API_BASE));
}

export async function createEtcClaim(payload: SaveEtcClaimRequest): Promise<EtcClaim> {
  return handleResponse<EtcClaim>(
    await apiFetch(API_BASE, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateEtcClaim(id: number, payload: SaveEtcClaimRequest): Promise<EtcClaim> {
  return handleResponse<EtcClaim>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteEtcClaim(id: number): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'DELETE',
    }),
  );
}
