import { apiFetch, handleResponse } from './http';

export type QualityInspectionStatus = 'PENDING' | 'COMPLETED' | 'CANCELLED';
export type QualityInspectionSourceType = 'PURCHASE' | 'OUTSOURCE';

export interface QualityInspection {
  id: number;
  inspectionNo?: string | null;
  sourceType: QualityInspectionSourceType;
  sourceReceiptLineId: number;
  itemId: number;
  itemNum: string;
  itemName: string;
  companyId: number;
  companyName: string;
  requestQty: number;
  passedQty: number;
  failedQty: number;
  status: QualityInspectionStatus;
  orderNo?: string | null;
  receiptDate?: string | null;
  createdAt: string;
  completedAt?: string | null;
  lotTracked: boolean;
  failureReason?: string | null;
}

export interface QualityInspectionListParams {
  status?: QualityInspectionStatus;
  sourceType?: QualityInspectionSourceType | '';
  partnerName?: string;
  itemNo?: string;
  itemName?: string;
  receiptDateFrom?: string;
  receiptDateTo?: string;
  completedDateFrom?: string;
  completedDateTo?: string;
}

export interface CompleteQualityInspectionRequest {
  passedQty: number;
  failedQty: number;
  inspectionDecisionCodeId?: number | null;
  unsuitabilityCauseCodeId?: number | null;
  unsuitabilityStatusCodeId?: number | null;
  failureReason?: string | null;
  completedDate: string;
  fiscalYear?: number;
  fiscalMonth?: number;
  lotNo?: string;
  autoGenerateLot?: boolean;
  allowOverQty?: boolean;
}

function buildQuery(params?: QualityInspectionListParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.status) search.set('status', params.status);
  if (params.sourceType) search.set('sourceType', params.sourceType);
  if (params.partnerName?.trim()) search.set('partnerName', params.partnerName.trim());
  if (params.itemNo?.trim()) search.set('itemNo', params.itemNo.trim());
  if (params.itemName?.trim()) search.set('itemName', params.itemName.trim());
  if (params.receiptDateFrom) search.set('receiptDateFrom', params.receiptDateFrom);
  if (params.receiptDateTo) search.set('receiptDateTo', params.receiptDateTo);
  if (params.completedDateFrom) search.set('completedDateFrom', params.completedDateFrom);
  if (params.completedDateTo) search.set('completedDateTo', params.completedDateTo);
  const q = search.toString();
  return q ? `?${q}` : '';
}

export async function fetchQualityInspections(
  params?: QualityInspectionListParams,
): Promise<QualityInspection[]> {
  const query = buildQuery(params);
  const status = params?.status ?? 'PENDING';
  const suffix = query || `?status=${status}`;
  return handleResponse(await apiFetch(`/api/v1/quality/inspections${suffix}`));
}

export async function completeQualityInspection(
  id: number,
  body: CompleteQualityInspectionRequest,
): Promise<QualityInspection> {
  return handleResponse(
    await apiFetch(`/api/v1/quality/inspections/${id}/complete`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body),
    }),
  );
}

export async function cancelQualityInspection(id: number): Promise<void> {
  await handleResponse(
    await apiFetch(`/api/v1/quality/inspections/${id}/cancel`, { method: 'POST' }),
  );
}
