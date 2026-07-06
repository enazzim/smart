import { apiFetch, handleResponse } from './http';
import type { WorkOrder } from './workOrder';

export interface MaterialIssue {
  id: number;
  issueNum: string;
  workOrderId: number;
  orderNum: string;
  itemId: number;
  itemNo: string;
  itemName: string;
  processName: string;
  issueDate: string;
  status: string;
  statusLabel: string;
  cancellable: boolean;
  createdAt: string;
  createdBy?: string | null;
}

export interface MaterialIssueListParams {
  itemNo?: string;
  itemName?: string;
  orderNum?: string;
  issueNum?: string;
  issueDateFrom?: string;
  issueDateTo?: string;
}

export interface MaterialIssuePreviewLine {
  itemCompositionId: number;
  itemId: number;
  itemNo: string;
  itemName: string;
  propertyClassification: string;
  unitRatio: number;
  requiredQty: number;
  sourceProcessId?: number | null;
  sourceProcessName?: string | null;
}

export interface MaterialIssueConsumptionPreview {
  workOrderId: number;
  parentItemNo: string;
  goodQty: number;
  lines: MaterialIssuePreviewLine[];
}

export interface MaterialIssueOnHand {
  itemId: number;
  onHandQty: number;
}

export interface CreateMaterialIssueRequest {
  workOrderId: number;
  issueDate: string;
  lines: { itemCompositionId: number; issueQty: number }[];
}

function buildQuery(params?: MaterialIssueListParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.itemNo?.trim()) search.set('itemNo', params.itemNo.trim());
  if (params.itemName?.trim()) search.set('itemName', params.itemName.trim());
  if (params.orderNum?.trim()) search.set('orderNum', params.orderNum.trim());
  if (params.issueNum?.trim()) search.set('issueNum', params.issueNum.trim());
  if (params.issueDateFrom) search.set('issueDateFrom', params.issueDateFrom);
  if (params.issueDateTo) search.set('issueDateTo', params.issueDateTo);
  const query = search.toString();
  return query ? `?${query}` : '';
}

export async function fetchMaterialIssueTargets(): Promise<WorkOrder[]> {
  return handleResponse(await apiFetch('/api/v1/production/material-issues/issue-targets'));
}

export async function fetchMaterialIssues(params?: MaterialIssueListParams): Promise<MaterialIssue[]> {
  return handleResponse(await apiFetch(`/api/v1/production/material-issues${buildQuery(params)}`));
}

export async function fetchMaterialIssuePreview(
  workOrderId: number,
  goodQty: number,
): Promise<MaterialIssueConsumptionPreview> {
  const search = new URLSearchParams({
    workOrderId: String(workOrderId),
    goodQty: String(goodQty),
  });
  return handleResponse(
    await apiFetch(`/api/v1/production/material-issues/consumption-preview?${search.toString()}`),
  );
}

export async function fetchMaterialIssueOnHand(
  workOrderId: number,
  issueDate?: string,
): Promise<MaterialIssueOnHand[]> {
  const search = new URLSearchParams({ workOrderId: String(workOrderId) });
  if (issueDate) search.set('issueDate', issueDate);
  return handleResponse(
    await apiFetch(`/api/v1/production/material-issues/issue-on-hand?${search.toString()}`),
  );
}

export async function createMaterialIssue(payload: CreateMaterialIssueRequest): Promise<MaterialIssue> {
  return handleResponse(
    await apiFetch('/api/v1/production/material-issues', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function cancelMaterialIssue(id: number): Promise<void> {
  await handleResponse(
    await apiFetch(`/api/v1/production/material-issues/${id}/cancel`, { method: 'POST' }),
  );
}
