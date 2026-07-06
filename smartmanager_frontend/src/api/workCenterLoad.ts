import { apiFetch, handleResponse } from './http';

export interface WorkCenterLoadDetail {
  workPlanId: number;
  planNo: string;
  itemNo: string;
  itemName: string;
  processName: string;
  plannedQty: number;
  demandMinutes: number;
  setupTime: number;
  standardTime: number;
}

export interface WorkCenterLoadDay {
  workCenterId: number;
  workCenterName: string;
  date: string;
  demandMinutes: number;
  capaMinutes: number;
  loadRate: number | null;
  workPlanCount: number;
  overThreshold: boolean;
  details: WorkCenterLoadDetail[];
}

export interface WorkCenterLoadResult {
  warnLoadThreshold: number;
  days: WorkCenterLoadDay[];
}

export interface WorkCenterLoadParams {
  workCenterId?: number;
  from: string;
  to: string;
}

function buildQuery(params: WorkCenterLoadParams): string {
  const search = new URLSearchParams();
  search.set('from', params.from);
  search.set('to', params.to);
  if (params.workCenterId != null && params.workCenterId > 0) {
    search.set('workCenterId', String(params.workCenterId));
  }
  return `?${search.toString()}`;
}

export async function fetchWorkCenterLoad(params: WorkCenterLoadParams): Promise<WorkCenterLoadResult> {
  return handleResponse(
    await apiFetch(`/api/v1/production/scheduling/work-center-load${buildQuery(params)}`),
  );
}
