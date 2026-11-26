import { apiFetch, handleResponse } from './http';
import type { ProductionPlan } from './productionPlan';

export interface MrpRun {
  id: number;
  runNo: string;
  planCount: number;
  lineCount: number;
  createdAt: string;
  createdBy?: string | null;
  cancellable: boolean;
}

export interface MaterialRequirementLine {
  id: number;
  mrpRunId: number;
  runNo: string;
  productionPlanId: number;
  planNo: string;
  parentItemId: number;
  parentItemNo: string;
  parentItemName: string;
  componentItemId: number;
  componentItemNo: string;
  componentItemName: string;
  componentPropertyClassification: string;
  componentPropertyClassificationLabel: string;
  unit: string;
  bomUnitQty: number;
  plannedQty: number;
  grossQty: number;
  createdAt: string;
  cancellable: boolean;
}

export type MrpGroupingMode = 'BY_PLAN' | 'BY_COMPONENT';

export interface MaterialRequirementComponentGroup {
  componentItemId: number;
  componentItemNo: string;
  componentItemName: string;
  componentPropertyClassification: string;
  componentPropertyClassificationLabel: string;
  unit: string;
  totalGrossQty: number;
  lineCount: number;
  details: MaterialRequirementLine[];
}

export interface MaterialRequirementGrouped {
  groupingMode: MrpGroupingMode;
  groupingModeLabel: string;
  lines: MaterialRequirementLine[];
  groups: MaterialRequirementComponentGroup[];
}

export interface MrpCancelPlanResult {
  productionPlanId: number;
  planNo: string;
}

export interface MrpTargetParams {
  itemId?: number;
  requestedDeliveryDateFrom?: string;
  requestedDeliveryDateTo?: string;
}

export interface MrpLineSearchParams {
  runId?: number;
  parentItemId?: number;
  componentItemId?: number;
  requestedDeliveryDateFrom?: string;
  requestedDeliveryDateTo?: string;
}

function toQuery(params: Record<string, string | number | undefined>): string {
  const search = new URLSearchParams();
  for (const [key, value] of Object.entries(params)) {
    if (value !== undefined && value !== '') {
      search.set(key, String(value));
    }
  }
  const qs = search.toString();
  return qs ? `?${qs}` : '';
}

export async function fetchMrpTargets(params: MrpTargetParams = {}): Promise<ProductionPlan[]> {
  const res = await apiFetch(
    `/api/v1/production/mrp/targets${toQuery({
      itemId: params.itemId,
      requestedDeliveryDateFrom: params.requestedDeliveryDateFrom,
      requestedDeliveryDateTo: params.requestedDeliveryDateTo,
    })}`,
  );
  return handleResponse(res);
}

export async function fetchMrpRuns(): Promise<MrpRun[]> {
  const res = await apiFetch('/api/v1/production/mrp/runs');
  return handleResponse(res);
}

export async function fetchMrpLinesByRun(runId: number): Promise<MaterialRequirementLine[]> {
  const res = await apiFetch(`/api/v1/production/mrp/runs/${runId}/lines`);
  return handleResponse(res);
}

export async function fetchAllMrpLines(): Promise<MaterialRequirementLine[]> {
  const res = await apiFetch('/api/v1/production/mrp/lines');
  return handleResponse(res);
}

export async function fetchGroupedMrpLines(
  params: MrpLineSearchParams = {},
): Promise<MaterialRequirementGrouped> {
  const res = await apiFetch(
    `/api/v1/production/mrp/lines/grouped${toQuery({
      runId: params.runId,
      parentItemId: params.parentItemId,
      componentItemId: params.componentItemId,
      requestedDeliveryDateFrom: params.requestedDeliveryDateFrom,
      requestedDeliveryDateTo: params.requestedDeliveryDateTo,
    })}`,
  );
  return handleResponse(res);
}

export async function calculateMrp(productionPlanIds: number[]): Promise<MrpRun> {
  const res = await apiFetch('/api/v1/production/mrp/calculate', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ productionPlanIds }),
  });
  return handleResponse(res);
}

export async function cancelMrpRun(runId: number): Promise<MrpRun> {
  const res = await apiFetch(`/api/v1/production/mrp/runs/${runId}/cancel`, { method: 'POST' });
  return handleResponse(res);
}

export async function cancelMrpPlan(productionPlanId: number): Promise<MrpCancelPlanResult> {
  const res = await apiFetch(`/api/v1/production/mrp/plans/${productionPlanId}/cancel`, { method: 'POST' });
  return handleResponse(res);
}

export async function cancelMrpLine(lineId: number): Promise<MaterialRequirementLine> {
  const res = await apiFetch(`/api/v1/production/mrp/lines/${lineId}/cancel`, { method: 'POST' });
  return handleResponse(res);
}
