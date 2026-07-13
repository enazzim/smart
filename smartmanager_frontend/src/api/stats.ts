import { apiFetch, handleResponse } from './http';

export type VendorPurchaseDivision = 'ALL' | 'PURCHASE' | 'OUTSOURCE' | 'ETC';

export interface VendorPurchaseTotal {
  companyId: number;
  companyName: string;
  itemId: number | null;
  itemNo: string | null;
  itemName: string;
  division: 'PURCHASE' | 'OUTSOURCE' | 'ETC';
  purchaseQty: number;
  unitPrice: number;
  amount: number;
  fiscalYear: number;
  fiscalMonth: number;
}

export interface WarehouseMonthlyIo {
  itemId: number;
  itemNo: string;
  itemName: string;
  locationCode: string;
  locationName: string;
  outputProcessSequence: number | null;
  outputProcessName: string | null;
  fiscalYear: number;
  fiscalMonth: number;
  carryInQty: number;
  inQty: number;
  outQty: number;
  endingQty: number;
}

export interface ItemStockMovementRow {
  id: number;
  itemId: number;
  itemNo: string;
  itemName: string;
  locationCode: string;
  locationName: string;
  outputProcessSequence: number | null;
  outputProcessName: string | null;
  partnerId: number | null;
  partnerName: string | null;
  movementType: string;
  inQty: number;
  outQty: number;
  amount: number;
  referenceType: string;
  referenceId: number;
  movementDate: string;
}

export interface VendorPurchaseTotalParams {
  companyId?: number;
  itemId?: number;
  itemNo?: string;
  fiscalYear?: number;
  fiscalMonth?: number;
  division?: VendorPurchaseDivision;
}

export interface WarehouseMonthlyIoParams {
  locationCode: string;
  itemId?: number;
  itemNo?: string;
  fiscalYear?: number;
  fiscalMonth?: number;
}

export interface ItemStockMovementParams {
  itemId?: number;
  itemNo?: string;
  companyId?: number;
  locationCode?: string;
  outputProcessId?: number;
  movementDateFrom?: string;
  movementDateTo?: string;
}

function toQuery(params: Record<string, string | number | undefined>): string {
  const search = new URLSearchParams();
  for (const [key, value] of Object.entries(params)) {
    if (value == null || value === '') continue;
    search.set(key, String(value));
  }
  const q = search.toString();
  return q ? `?${q}` : '';
}

export async function fetchVendorPurchaseTotals(
  params: VendorPurchaseTotalParams = {},
): Promise<VendorPurchaseTotal[]> {
  const res = await apiFetch(
    `/api/v1/stats/vendor-purchase-totals${toQuery({
      companyId: params.companyId,
      itemId: params.itemId,
      itemNo: params.itemNo,
      fiscalYear: params.fiscalYear,
      fiscalMonth: params.fiscalMonth,
      division: params.division === 'ALL' ? undefined : params.division,
    })}`,
  );
  return handleResponse(res);
}

export async function fetchWarehouseMonthlyIo(
  params: WarehouseMonthlyIoParams,
): Promise<WarehouseMonthlyIo[]> {
  const res = await apiFetch(
    `/api/v1/stats/warehouse-monthly-io${toQuery({
      locationCode: params.locationCode,
      itemId: params.itemId,
      itemNo: params.itemNo,
      fiscalYear: params.fiscalYear,
      fiscalMonth: params.fiscalMonth,
    })}`,
  );
  return handleResponse(res);
}

export async function fetchItemStockMovements(
  params: ItemStockMovementParams,
): Promise<ItemStockMovementRow[]> {
  const res = await apiFetch(
    `/api/v1/stats/item-stock-movements${toQuery({
      itemId: params.itemId,
      itemNo: params.itemNo,
      companyId: params.companyId,
      locationCode: params.locationCode,
      outputProcessId: params.outputProcessId,
      movementDateFrom: params.movementDateFrom,
      movementDateTo: params.movementDateTo,
    })}`,
  );
  return handleResponse(res);
}
