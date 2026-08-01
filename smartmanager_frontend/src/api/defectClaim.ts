import { apiFetch, handleResponse } from './http';

export type DefectClaimRecognition = 'PENDING' | 'APPROVED';

export interface DefectClaim {
  id: number;
  partnerId: number;
  partnerName: string;
  partnerBusinessRegNo: string;
  itemId: number;
  itemNo: string;
  itemName: string;
  drawingNo?: string | null;
  receiptDate: string;
  claimQty: number;
  amount: number;
  reason: string;
  fiscalYear: number;
  fiscalMonth: number;
  recognition: DefectClaimRecognition;
  createdBy?: string | null;
  createdAt: string;
  editable: boolean;
}

export interface DefectClaimListParams {
  partnerId?: number;
  partnerName?: string;
  itemId?: number;
  itemNo?: string;
  itemName?: string;
  receiptDateFrom?: string;
  receiptDateTo?: string;
}

export interface SaveDefectClaimRequest {
  partnerId: number;
  itemId: number;
  receiptDate: string;
  claimQty: number;
  amount: number;
  reason: string;
}

const API_BASE = '/api/v1/purchase/defect-claims';

export async function fetchDefectClaims(params: DefectClaimListParams = {}): Promise<DefectClaim[]> {
  const search = new URLSearchParams();
  if (params.partnerId != null) search.set('partnerId', String(params.partnerId));
  if (params.partnerName?.trim()) search.set('partnerName', params.partnerName.trim());
  if (params.itemId != null) search.set('itemId', String(params.itemId));
  if (params.itemNo?.trim()) search.set('itemNo', params.itemNo.trim());
  if (params.itemName?.trim()) search.set('itemName', params.itemName.trim());
  if (params.receiptDateFrom) search.set('receiptDateFrom', params.receiptDateFrom);
  if (params.receiptDateTo) search.set('receiptDateTo', params.receiptDateTo);
  const query = search.toString();
  return handleResponse<DefectClaim[]>(await apiFetch(query ? `${API_BASE}?${query}` : API_BASE));
}

export async function createDefectClaim(payload: SaveDefectClaimRequest): Promise<DefectClaim> {
  return handleResponse<DefectClaim>(
    await apiFetch(API_BASE, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateDefectClaim(id: number, payload: SaveDefectClaimRequest): Promise<DefectClaim> {
  return handleResponse<DefectClaim>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteDefectClaim(id: number): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'DELETE',
    }),
  );
}
