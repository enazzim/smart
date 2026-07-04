import { apiFetch, handleResponse } from './http';

export type SalesOrderStatus = 'DRAFT' | 'CONFIRMED' | 'CANCELLED';
export type SalesFulfillmentRoute = 'COMMODITY' | 'MANUFACTURING';
export type SalesLineFulfillmentStatus = 'WAITING' | 'IN_PROGRESS' | 'COMPLETED' | 'FORCE_COMPLETED';
export type SalesLineDeliveryStatus = 'NOT_STARTED' | 'IN_PROGRESS' | 'COMPLETED';

export interface SalesOrderLine {
  id: number;
  lineNo: number;
  itemId: number;
  itemNo: string;
  itemName: string;
  propertyClassification: string;
  orderQty: number;
  unitPrice: number;
  amount: number;
  deliveryDate?: string | null;
  fulfillmentRoute: SalesFulfillmentRoute;
  fulfillmentRouteLabel: string;
  fulfillmentStatus: SalesLineFulfillmentStatus;
  fulfillmentStatusLabel: string;
  deliveryStatus: SalesLineDeliveryStatus;
  deliveryStatusLabel: string;
}

export interface SalesOrder {
  id: number;
  orderNo: string;
  partnerId: number;
  partnerName: string;
  partnerBusinessRegNo: string;
  orderDate: string;
  requestedDeliveryDate?: string | null;
  status: SalesOrderStatus;
  remark?: string | null;
  confirmedAt?: string | null;
  confirmedBy?: string | null;
  lines: SalesOrderLine[];
}

export interface SalesOrderLineListRow {
  orderId: number;
  lineId: number;
  orderNo: string;
  partnerId: number;
  partnerName: string;
  partnerBusinessRegNo: string;
  orderDate: string;
  requestedDeliveryDate?: string | null;
  itemId: number;
  itemNo: string;
  itemName: string;
  unitPrice: number;
  orderQty: number;
  amount: number;
  fulfillmentRoute: SalesFulfillmentRoute;
  fulfillmentRouteLabel: string;
  fulfillmentStatus: SalesLineFulfillmentStatus;
  fulfillmentStatusLabel: string;
  executionStatusLabel: string;
  deliveryStatus: SalesLineDeliveryStatus;
  deliveryStatusLabel: string;
  orderStatus: SalesOrderStatus;
  orderEditable: boolean;
}

export interface SalesOrderLineSearchParams {
  partnerId?: number;
  itemId?: number;
  requestedDeliveryDateFrom?: string;
  requestedDeliveryDateTo?: string;
  fulfillmentStatus?: SalesLineFulfillmentStatus;
  deliveryStatus?: SalesLineDeliveryStatus;
}

export interface SalesOrderLineRequest {
  itemId: number;
  orderQty: number;
  unitPrice?: number;
  deliveryDate?: string;
}

export interface SalesOrderRequest {
  orderNo?: string;
  partnerId: number;
  orderDate: string;
  requestedDeliveryDate: string;
  remark?: string;
  lines: SalesOrderLineRequest[];
}

export interface SalesOrderBulkFailure {
  rowIndex: number;
  orderNo?: string | null;
  message: string;
}

export interface SalesOrderBulkResult {
  successCount: number;
  failureCount: number;
  created: SalesOrder[];
  failures: SalesOrderBulkFailure[];
}

const ORDERS_API = '/api/v1/sales/orders';
const ORDER_LINES_API = '/api/v1/sales/order-lines';

function toSearchParams(params: SalesOrderLineSearchParams): string {
  const search = new URLSearchParams();
  if (params.partnerId) search.set('partnerId', String(params.partnerId));
  if (params.itemId) search.set('itemId', String(params.itemId));
  if (params.requestedDeliveryDateFrom) search.set('requestedDeliveryDateFrom', params.requestedDeliveryDateFrom);
  if (params.requestedDeliveryDateTo) search.set('requestedDeliveryDateTo', params.requestedDeliveryDateTo);
  if (params.fulfillmentStatus) search.set('fulfillmentStatus', params.fulfillmentStatus);
  if (params.deliveryStatus) search.set('deliveryStatus', params.deliveryStatus);
  const query = search.toString();
  return query ? `?${query}` : '';
}

export async function fetchSalesOrderLines(params: SalesOrderLineSearchParams = {}): Promise<SalesOrderLineListRow[]> {
  return handleResponse<SalesOrderLineListRow[]>(
    await apiFetch(`${ORDER_LINES_API}${toSearchParams(params)}`),
  );
}

export async function fetchSalesOrders(): Promise<SalesOrder[]> {
  return handleResponse<SalesOrder[]>(await apiFetch(ORDERS_API));
}

export async function fetchSalesOrder(id: number): Promise<SalesOrder> {
  return handleResponse<SalesOrder>(await apiFetch(`${ORDERS_API}/${id}`));
}

export async function fetchNextOrderNo(orderDate: string): Promise<string> {
  const params = new URLSearchParams({ orderDate });
  const response = await handleResponse<{ orderNo: string }>(
    await apiFetch(`${ORDERS_API}/next-order-no?${params}`),
  );
  return response.orderNo;
}

export async function createSalesOrder(body: SalesOrderRequest): Promise<SalesOrder> {
  return handleResponse<SalesOrder>(
    await apiFetch(ORDERS_API, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body),
    }),
  );
}

export async function createSalesOrdersBulk(orders: SalesOrderRequest[]): Promise<SalesOrderBulkResult> {
  return handleResponse<SalesOrderBulkResult>(
    await apiFetch(`${ORDERS_API}/bulk`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ orders }),
    }),
  );
}

export async function updateSalesOrder(id: number, body: SalesOrderRequest): Promise<SalesOrder> {
  return handleResponse<SalesOrder>(
    await apiFetch(`${ORDERS_API}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body),
    }),
  );
}

export async function confirmSalesOrder(id: number): Promise<SalesOrder> {
  return handleResponse<SalesOrder>(
    await apiFetch(`${ORDERS_API}/${id}/confirm`, { method: 'POST' }),
  );
}

export async function cancelSalesOrder(id: number): Promise<void> {
  await handleResponse<void>(await apiFetch(`${ORDERS_API}/${id}/cancel`, { method: 'POST' }));
}

export async function startSalesOrderLineProgress(lineId: number): Promise<SalesOrderLineListRow> {
  return handleResponse<SalesOrderLineListRow>(
    await apiFetch(`${ORDER_LINES_API}/${lineId}/start-progress`, { method: 'POST' }),
  );
}

export async function completeSalesOrderLine(lineId: number): Promise<SalesOrderLineListRow> {
  return handleResponse<SalesOrderLineListRow>(
    await apiFetch(`${ORDER_LINES_API}/${lineId}/complete`, { method: 'POST' }),
  );
}

export async function forceCompleteSalesOrderLine(lineId: number): Promise<SalesOrderLineListRow> {
  return handleResponse<SalesOrderLineListRow>(
    await apiFetch(`${ORDER_LINES_API}/${lineId}/force-complete`, { method: 'POST' }),
  );
}
