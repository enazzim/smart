import { apiFetch, handleResponse } from './http';
import type { SalesOrderLineListRow } from './salesOrder';

export type ProductionPlanStatus = 'PLANNED' | 'IN_PROGRESS' | 'COMPLETED' | 'CANCELLED';
export type ProductionPlanMrpStatus = 'NOT_CALCULATED' | 'CALCULATED';
export type ProductionPlanWorkPlanStatus = 'NOT_PLANNED' | 'PLANNED';

export interface ProductionPlan {
  id: number;
  planNo: string;
  salesOrderId: number;
  salesOrderLineId: number;
  orderNo: string;
  partnerId: number;
  partnerName: string;
  orderDate: string;
  itemId: number;
  itemNo: string;
  itemName: string;
  plannedQty: number;
  producedQty: number;
  requestedDeliveryDate?: string | null;
  status: ProductionPlanStatus;
  statusLabel: string;
  mrpStatus: ProductionPlanMrpStatus;
  mrpStatusLabel: string;
  workPlanStatus: ProductionPlanWorkPlanStatus;
  workPlanStatusLabel: string;
  cancellable: boolean;
}

export interface ProductionPlanSearchParams {
  partnerId?: number;
  itemId?: number;
  requestedDeliveryDateFrom?: string;
  requestedDeliveryDateTo?: string;
  status?: ProductionPlanStatus;
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

export async function fetchProductionPlanCandidates(): Promise<SalesOrderLineListRow[]> {
  const res = await apiFetch('/api/v1/production/plan-candidates');
  return handleResponse(res);
}

export async function fetchProductionPlans(
  params: ProductionPlanSearchParams = {},
): Promise<ProductionPlan[]> {
  const res = await apiFetch(
    `/api/v1/production/plans${toQuery({
      partnerId: params.partnerId,
      itemId: params.itemId,
      requestedDeliveryDateFrom: params.requestedDeliveryDateFrom,
      requestedDeliveryDateTo: params.requestedDeliveryDateTo,
      status: params.status,
    })}`,
  );
  return handleResponse(res);
}

export interface ProductionPlanCreateLine {
  salesOrderLineId: number;
  plannedQty: number;
}

export async function createProductionPlans(
  lines: ProductionPlanCreateLine[],
): Promise<ProductionPlan[]> {
  const res = await apiFetch('/api/v1/production/plans', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ lines }),
  });
  return handleResponse(res);
}

export async function cancelProductionPlan(id: number): Promise<ProductionPlan> {
  const res = await apiFetch(`/api/v1/production/plans/${id}/cancel`, { method: 'POST' });
  return handleResponse(res);
}
