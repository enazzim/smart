import { apiFetch, handleResponse } from './http';

export type SalesCollectionStatus = 'ISSUED' | 'CANCELLED';

export interface SalesCollectionCandidate {
  partnerId: number;
  partnerName: string;
  partnerBusinessRegNo: string;
  revenueAmount: number;
  collectedAmount: number;
  uncollectedAmount: number;
  collectable: boolean;
}

export interface SalesCollection {
  id: number;
  collectionNo: string;
  partnerId: number;
  partnerName: string;
  partnerBusinessRegNo: string;
  collectionDate: string;
  supplyAmount: number;
  vatAmount: number;
  totalAmount: number;
  paymentMethod?: string | null;
  remark?: string | null;
  status: SalesCollectionStatus;
  statusLabel: string;
  createdAt: string;
  createdBy?: string | null;
  cancelable: boolean;
}

export interface SalesCollectionCandidateParams {
  partnerName?: string;
}

export interface SalesCollectionListParams {
  collectionDateFrom?: string;
  collectionDateTo?: string;
  collectionNo?: string;
  partnerName?: string;
  status?: SalesCollectionStatus;
  excludeCancelled?: boolean;
}

function buildCandidateQuery(params?: SalesCollectionCandidateParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.partnerName?.trim()) search.set('partnerName', params.partnerName.trim());
  const query = search.toString();
  return query ? `?${query}` : '';
}

function buildListQuery(params?: SalesCollectionListParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.collectionDateFrom) search.set('collectionDateFrom', params.collectionDateFrom);
  if (params.collectionDateTo) search.set('collectionDateTo', params.collectionDateTo);
  if (params.collectionNo?.trim()) search.set('collectionNo', params.collectionNo.trim());
  if (params.partnerName?.trim()) search.set('partnerName', params.partnerName.trim());
  if (params.status) search.set('status', params.status);
  if (params.excludeCancelled !== undefined) search.set('excludeCancelled', String(params.excludeCancelled));
  const query = search.toString();
  return query ? `?${query}` : '';
}

export async function fetchSalesCollectionCandidates(
  params?: SalesCollectionCandidateParams,
): Promise<SalesCollectionCandidate[]> {
  return handleResponse(await apiFetch(`/api/v1/sales/collections/candidates${buildCandidateQuery(params)}`));
}

export async function fetchSalesCollections(params?: SalesCollectionListParams): Promise<SalesCollection[]> {
  return handleResponse(await apiFetch(`/api/v1/sales/collections${buildListQuery(params)}`));
}

export async function createSalesCollection(payload: {
  partnerId: number;
  collectionDate: string;
  supplyAmount: number;
  vatAmount?: number;
  paymentMethod?: string;
  remark?: string;
}): Promise<SalesCollection> {
  return handleResponse(
    await apiFetch('/api/v1/sales/collections', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function cancelSalesCollection(id: number): Promise<void> {
  await handleResponse(await apiFetch(`/api/v1/sales/collections/${id}/cancel`, { method: 'POST' }));
}
