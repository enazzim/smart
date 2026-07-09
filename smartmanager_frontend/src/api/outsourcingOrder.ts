import { apiFetch, handleResponse } from './http';

export type OutsourcingOrderStatus = 'CONFIRMED' | 'IN_PROGRESS' | 'RECEIVED' | 'CANCELLED';
export type OutsourcingOrderSourceType = 'WORK_PLAN' | 'MANUAL';

export interface OutsourcingOrderLine {
  id: number;
  lineNo: number;
  itemId: number;
  itemNo: string;
  itemName: string;
  propertyClassification: string;
  processSequenceId: number;
  processSequenceNum: number;
  processCode: string;
  processName: string;
  beginProcessCodeId: number;
  beginProcessCode: string;
  beginProcessName: string;
  endProcessCodeId: number;
  endProcessCode: string;
  endProcessName: string;
  workPlanId?: number | null;
  planNo?: string | null;
  orderQty: number;
  shippedQty: number;
  receivedQty: number;
  unitPrice: number;
  amount: number;
  requestedDeliveryDate?: string | null;
}

export interface OutsourcingOrder {
  id: number;
  orderNo: string;
  partnerId: number;
  partnerName: string;
  partnerBusinessRegNo: string;
  orderDate: string;
  sourceType: OutsourcingOrderSourceType;
  sourceTypeLabel: string;
  status: OutsourcingOrderStatus;
  statusLabel: string;
  createdAt: string;
  createdBy?: string | null;
  cancelable: boolean;
  lines: OutsourcingOrderLine[];
}

export interface OutsourcingOrderListParams {
  orderDateFrom?: string;
  orderDateTo?: string;
  partnerName?: string;
  orderNo?: string;
  status?: OutsourcingOrderStatus;
  excludeCancelled?: boolean;
}

export interface WorkPlanOutsourceCandidateVendor {
  partnerId: number;
  partnerName: string;
  businessRegNo: string;
  beginProcessCodeId: number;
  beginProcessCode: string;
  beginProcessName: string;
  endProcessCodeId: number;
  endProcessCode: string;
  endProcessName: string;
  orderRate: number;
  orderQty: number;
  unitPrice: number;
  amount: number;
  requestedDeliveryDate?: string | null;
}

export interface WorkPlanOutsourceCandidate {
  workPlanId: number;
  productionPlanId: number;
  planNo: string;
  itemId: number;
  itemNo: string;
  itemName: string;
  processSequenceId: number;
  processSequenceNum: number;
  processCode: string;
  processName: string;
  plannedQty: number;
  orderedQty: number;
  remainingQty: number;
  planEndDate?: string | null;
  orderRateTotal: number;
  orderable: boolean;
  orderableMessage?: string | null;
  vendors: WorkPlanOutsourceCandidateVendor[];
}

function buildQuery(params?: OutsourcingOrderListParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.orderDateFrom) search.set('orderDateFrom', params.orderDateFrom);
  if (params.orderDateTo) search.set('orderDateTo', params.orderDateTo);
  if (params.partnerName?.trim()) search.set('partnerName', params.partnerName.trim());
  if (params.orderNo?.trim()) search.set('orderNo', params.orderNo.trim());
  if (params.status) search.set('status', params.status);
  if (params.excludeCancelled !== undefined) search.set('excludeCancelled', String(params.excludeCancelled));
  const query = search.toString();
  return query ? `?${query}` : '';
}

export async function fetchOutsourcingOrders(params?: OutsourcingOrderListParams): Promise<OutsourcingOrder[]> {
  return handleResponse(await apiFetch(`/api/v1/outsource/orders${buildQuery(params)}`));
}

export async function fetchWorkPlanOutsourceCandidates(orderDate?: string): Promise<WorkPlanOutsourceCandidate[]> {
  const search = orderDate ? `?orderDate=${encodeURIComponent(orderDate)}` : '';
  return handleResponse(await apiFetch(`/api/v1/outsource/orders/work-plan-candidates${search}`));
}

export async function createOutsourcingOrderFromWorkPlan(payload: {
  partnerId: number;
  orderDate: string;
  lines: Array<{
    workPlanId: number;
    beginProcessCodeId: number;
    endProcessCodeId: number;
    orderQty: number;
    unitPrice?: number;
    requestedDeliveryDate?: string;
  }>;
}): Promise<OutsourcingOrder> {
  return handleResponse(
    await apiFetch('/api/v1/outsource/orders/from-work-plan', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function cancelOutsourcingOrder(id: number): Promise<void> {
  await handleResponse(
    await apiFetch(`/api/v1/outsource/orders/${id}/cancel`, { method: 'POST' }),
  );
}

export async function fetchOutsourcingOrderPrintHtml(orderId: number): Promise<string> {
  const res = await apiFetch(`/api/v1/outsource/orders/${orderId}/print`);
  if (!res.ok) {
    await handleResponse(res);
  }
  return res.text();
}

export async function fetchOutsourcingOrdersPrintHtml(orderIds: number[]): Promise<string> {
  const res = await apiFetch('/api/v1/outsource/orders/print', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ orderIds }),
  });
  if (!res.ok) {
    await handleResponse(res);
  }
  return res.text();
}

export async function openOutsourcingOrderPrint(orderIds: number[]): Promise<void> {
  if (orderIds.length === 0) {
    throw new Error('출력할 발주를 1건 이상 선택해 주세요.');
  }
  const html =
    orderIds.length === 1
      ? await fetchOutsourcingOrderPrintHtml(orderIds[0])
      : await fetchOutsourcingOrdersPrintHtml(orderIds);
  const printWindow = window.open('', '_blank', 'width=980,height=760');
  if (!printWindow) {
    throw new Error('팝업이 차단되었습니다. 팝업 허용 후 다시 시도해 주세요.');
  }
  printWindow.document.write(html);
  printWindow.document.close();
  printWindow.focus();
}
