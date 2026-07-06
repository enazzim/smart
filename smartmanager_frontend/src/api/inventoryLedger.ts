import { apiFetch, handleResponse } from './http';

export interface StockMovement {
  id: number;
  itemId: number;
  itemNo: string;
  itemName: string;
  locationCode: string;
  locationName: string;
  movementType: 'IN' | 'OUT' | 'ADJUST';
  qty: number;
  amount: number;
  referenceType: string;
  referenceId: number;
  movementDate: string;
  fiscalYear: number;
  fiscalMonth: number;
}

export interface InventoryBalanceMonthly {
  monthNum: number;
  inQty: number;
  outQty: number;
  stockQty: number;
}

export interface InventoryBalance {
  balanceId: number;
  itemId: number;
  itemNo: string;
  itemName: string;
  locationCode: string;
  locationName: string;
  fiscalYear: number;
  stockQty: number;
  stockAmount: number;
  months: InventoryBalanceMonthly[];
}

export interface StockMovementListParams {
  itemNo?: string;
  locationCode?: string;
  referenceType?: string;
  movementDateFrom?: string;
  movementDateTo?: string;
}

export interface InventoryBalanceListParams {
  itemNo?: string;
  locationCode?: string;
  fiscalYear?: number;
}

function buildMovementQuery(params?: StockMovementListParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.itemNo?.trim()) search.set('itemNo', params.itemNo.trim());
  if (params.locationCode?.trim()) search.set('locationCode', params.locationCode.trim());
  if (params.referenceType?.trim()) search.set('referenceType', params.referenceType.trim());
  if (params.movementDateFrom) search.set('movementDateFrom', params.movementDateFrom);
  if (params.movementDateTo) search.set('movementDateTo', params.movementDateTo);
  const q = search.toString();
  return q ? `?${q}` : '';
}

function buildBalanceQuery(params?: InventoryBalanceListParams): string {
  if (!params) return '';
  const search = new URLSearchParams();
  if (params.itemNo?.trim()) search.set('itemNo', params.itemNo.trim());
  if (params.locationCode?.trim()) search.set('locationCode', params.locationCode.trim());
  if (params.fiscalYear != null) search.set('fiscalYear', String(params.fiscalYear));
  const q = search.toString();
  return q ? `?${q}` : '';
}

export async function fetchStockMovements(params?: StockMovementListParams): Promise<StockMovement[]> {
  return handleResponse(await apiFetch(`/api/v1/inventory/stock-movements${buildMovementQuery(params)}`));
}

export async function fetchInventoryBalances(params?: InventoryBalanceListParams): Promise<InventoryBalance[]> {
  return handleResponse(await apiFetch(`/api/v1/inventory/balances${buildBalanceQuery(params)}`));
}
