import { apiFetch, handleResponse } from './http';
import type { ProductionPlan } from './productionPlan';

export type WorkDistinction = 'INHOUSE' | 'OUTSOURCE' | 'SPLIT';
export type WorkPlanStatus = 'PLANNED' | 'CANCELLED';

export interface WorkPlan {
  id: number;
  productionPlanId: number;
  planNo: string;
  itemId: number;
  itemNo: string;
  itemName: string;
  processSequenceId: number;
  processSequenceNum: number;
  processCode: string;
  processName: string;
  workDistinction: WorkDistinction;
  workDistinctionLabel: string;
  workCenterId?: number | null;
  workCenterName?: string | null;
  plannedQty: number;
  planStartDate?: string | null;
  planEndDate?: string | null;
  setupTime: number;
  standardTime: number;
  status: WorkPlanStatus;
  statusLabel: string;
  cancellable: boolean;
  createdAt: string;
  createdBy?: string | null;
}

export interface WorkPlanListParams {
  itemNo?: string;
  itemName?: string;
  processName?: string;
  workCenterName?: string;
  planStartDateFrom?: string;
  planStartDateTo?: string;
}

function buildQuery(params?: WorkPlanListParams): string {
  if (!params) {
    return '';
  }
  const search = new URLSearchParams();
  if (params.itemNo?.trim()) {
    search.set('itemNo', params.itemNo.trim());
  }
  if (params.itemName?.trim()) {
    search.set('itemName', params.itemName.trim());
  }
  if (params.processName?.trim()) {
    search.set('processName', params.processName.trim());
  }
  if (params.workCenterName?.trim()) {
    search.set('workCenterName', params.workCenterName.trim());
  }
  if (params.planStartDateFrom) {
    search.set('planStartDateFrom', params.planStartDateFrom);
  }
  if (params.planStartDateTo) {
    search.set('planStartDateTo', params.planStartDateTo);
  }
  const query = search.toString();
  return query ? `?${query}` : '';
}

export async function fetchWorkPlanTargets(): Promise<ProductionPlan[]> {
  return handleResponse(await apiFetch('/api/v1/production/work-plans/planning-targets'));
}

export async function fetchWorkPlans(params?: WorkPlanListParams): Promise<WorkPlan[]> {
  return handleResponse(await apiFetch(`/api/v1/production/work-plans${buildQuery(params)}`));
}

export async function createWorkPlans(productionPlanIds: number[]): Promise<WorkPlan[]> {
  return handleResponse(
    await apiFetch('/api/v1/production/work-plans', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ productionPlanIds }),
    }),
  );
}

export async function cancelWorkPlanLine(id: number): Promise<WorkPlan> {
  return handleResponse(
    await apiFetch(`/api/v1/production/work-plans/${id}/cancel`, {
      method: 'POST',
    }),
  );
}

export async function cancelWorkPlansByProductionPlan(productionPlanId: number): Promise<{
  productionPlanId: number;
  planNo: string;
  cancelledCount: number;
}> {
  return handleResponse(
    await apiFetch(`/api/v1/production/work-plans/production-plans/${productionPlanId}/cancel`, {
      method: 'POST',
    }),
  );
}
