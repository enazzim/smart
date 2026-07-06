import { apiFetch, handleResponse } from './http';

export type WorkOrderStatus = 'ISSUED' | 'CANCELLED';

export interface WorkOrder {
  id: number;
  workPlanId: number;
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
  orderedQty: number;
  reportedQty: number;
  remainingQty: number;
  planStartDate?: string | null;
  status: WorkOrderStatus;
  statusLabel: string;
  cancellable: boolean;
  createdAt: string;
  createdBy?: string | null;
}

export interface WorkOrderListParams {
  itemNo?: string;
  itemName?: string;
  processName?: string;
  workCenterName?: string;
  orderNum?: string;
  planStartDateFrom?: string;
  planStartDateTo?: string;
}

function buildQuery(params?: WorkOrderListParams): string {
  if (!params) {
    return '';
  }
  const search = new URLSearchParams();
  if (params.itemNo?.trim()) search.set('itemNo', params.itemNo.trim());
  if (params.itemName?.trim()) search.set('itemName', params.itemName.trim());
  if (params.processName?.trim()) search.set('processName', params.processName.trim());
  if (params.workCenterName?.trim()) search.set('workCenterName', params.workCenterName.trim());
  if (params.orderNum?.trim()) search.set('orderNum', params.orderNum.trim());
  if (params.planStartDateFrom) search.set('planStartDateFrom', params.planStartDateFrom);
  if (params.planStartDateTo) search.set('planStartDateTo', params.planStartDateTo);
  const query = search.toString();
  return query ? `?${query}` : '';
}

export async function fetchWorkOrderTargets(): Promise<WorkOrder[]> {
  return handleResponse(await apiFetch('/api/v1/production/work-orders/order-targets'));
}

export async function fetchWorkOrders(params?: WorkOrderListParams): Promise<WorkOrder[]> {
  return handleResponse(await apiFetch(`/api/v1/production/work-orders${buildQuery(params)}`));
}

export async function createWorkOrders(workPlanIds: number[]): Promise<WorkOrder[]> {
  return handleResponse(
    await apiFetch('/api/v1/production/work-orders', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ workPlanIds }),
    }),
  );
}

export async function cancelWorkOrder(id: number): Promise<WorkOrder> {
  return handleResponse(
    await apiFetch(`/api/v1/production/work-orders/${id}/cancel`, { method: 'POST' }),
  );
}
