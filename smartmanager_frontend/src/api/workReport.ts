import { apiFetch, handleResponse } from './http';
import type { WorkOrder } from './workOrder';

export type WorkReportStatus = 'REGISTERED' | 'CANCELLED';

export interface WorkReport {
  id: number;
  reportNum: string;
  workOrderId: number;
  orderNum: string;
  productionPlanId: number;
  planNo: string;
  itemId: number;
  itemNo: string;
  itemName: string;
  processSequenceId: number;
  processSequenceNum: number;
  processCode: string;
  processName: string;
  workCenterId?: number | null;
  workCenterName?: string | null;
  reportDate: string;
  goodQty: number;
  scrapQty: number;
  setupTime?: number | null;
  runTime?: number | null;
  workerName?: string | null;
  status: WorkReportStatus;
  statusLabel: string;
  cancellable: boolean;
  createdAt: string;
  createdBy?: string | null;
}

export interface WorkReportListParams {
  itemNo?: string;
  itemName?: string;
  processName?: string;
  orderNum?: string;
  reportNum?: string;
  reportDateFrom?: string;
  reportDateTo?: string;
}

export interface CreateWorkReportRequest {
  workOrderId: number;
  reportDate: string;
  goodQty: number;
  scrapQty?: number;
  setupTime?: number;
  runTime?: number;
  workerName?: string;
  issueLines?: WorkReportIssueLineRequest[];
}

export interface WorkReportIssueLineRequest {
  itemCompositionId: number;
  itemId: number;
  issueQty: number;
}

export interface WorkReportIssueOnHand {
  itemId: number;
  onHandQty: number;
}

export interface WorkReportConsumptionLine {
  itemCompositionId: number;
  itemId: number;
  itemNo: string;
  itemName: string;
  propertyClassification: string;
  unitRatio: number;
  requiredQty: number;
  issuedQty: number;
  remainingQty: number;
  satisfied: boolean;
  suggestedIssueQty: number;
  sourceProcessId?: number | null;
  sourceProcessName?: string | null;
}

export interface WorkReportConsumptionStatus {
  workOrderId: number;
  parentItemNo: string;
  parentItemName: string;
  cumulativeGoodQty: number;
  pendingGoodQty: number;
  lines: WorkReportConsumptionLine[];
  allSatisfied: boolean;
  materialIssueEnabled: boolean;
}

function buildQuery(params?: WorkReportListParams): string {
  if (!params) {
    return '';
  }
  const search = new URLSearchParams();
  if (params.itemNo?.trim()) search.set('itemNo', params.itemNo.trim());
  if (params.itemName?.trim()) search.set('itemName', params.itemName.trim());
  if (params.processName?.trim()) search.set('processName', params.processName.trim());
  if (params.orderNum?.trim()) search.set('orderNum', params.orderNum.trim());
  if (params.reportNum?.trim()) search.set('reportNum', params.reportNum.trim());
  if (params.reportDateFrom) search.set('reportDateFrom', params.reportDateFrom);
  if (params.reportDateTo) search.set('reportDateTo', params.reportDateTo);
  const query = search.toString();
  return query ? `?${query}` : '';
}

export async function fetchWorkReportTargets(): Promise<WorkOrder[]> {
  return handleResponse(await apiFetch('/api/v1/production/work-reports/report-targets'));
}

export async function fetchWorkReports(params?: WorkReportListParams): Promise<WorkReport[]> {
  return handleResponse(await apiFetch(`/api/v1/production/work-reports${buildQuery(params)}`));
}

export async function fetchWorkReportIssueOnHand(
  workOrderId: number,
  reportDate?: string,
): Promise<WorkReportIssueOnHand[]> {
  const search = new URLSearchParams({ workOrderId: String(workOrderId) });
  if (reportDate) {
    search.set('reportDate', reportDate);
  }
  return handleResponse(
    await apiFetch(`/api/v1/production/work-reports/issue-on-hand?${search.toString()}`),
  );
}

export async function fetchWorkReportConsumptionStatus(
  workOrderId: number,
  pendingGoodQty: number,
): Promise<WorkReportConsumptionStatus> {
  const search = new URLSearchParams({
    workOrderId: String(workOrderId),
    pendingGoodQty: String(pendingGoodQty),
  });
  return handleResponse(
    await apiFetch(`/api/v1/production/work-reports/consumption-status?${search.toString()}`),
  );
}

export async function createWorkReport(payload: CreateWorkReportRequest): Promise<WorkReport> {
  return handleResponse(
    await apiFetch('/api/v1/production/work-reports', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function cancelWorkReport(id: number): Promise<void> {
  await handleResponse(
    await apiFetch(`/api/v1/production/work-reports/${id}/cancel`, { method: 'POST' }),
  );
}
