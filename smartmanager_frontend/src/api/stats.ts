import { apiFetch, handleResponse } from './http';

export type VendorPurchaseDivision = 'ALL' | 'PURCHASE' | 'OUTSOURCE' | 'ETC' | 'CLAIM';

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

export interface VendorPurchaseStatusRow {
  historyId: number;
  ledgerKind: string;
  companyId: number;
  companyName: string;
  itemId: number | null;
  itemNo: string;
  itemName: string;
  modelType: string;
  processName: string;
  receiptDate: string;
  currentStockQty: number;
  receiptQty: number;
  unitPrice: number;
  amount: number;
  division: 'PURCHASE' | 'OUTSOURCE' | 'ETC' | 'CLAIM';
  fiscalYear: number;
  fiscalMonth: number;
}

export interface VendorPurchaseStatusParams {
  companyId?: number;
  companyName?: string;
  itemId?: number;
  itemNo?: string;
  itemName?: string;
  modelType?: string;
  receiptDateFrom?: string;
  receiptDateTo?: string;
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

export async function fetchVendorPurchaseStatus(
  params: VendorPurchaseStatusParams = {},
): Promise<VendorPurchaseStatusRow[]> {
  const res = await apiFetch(
    `/api/v1/stats/vendor-purchase-status${toQuery({
      companyId: params.companyId,
      companyName: params.companyName,
      itemId: params.itemId,
      itemNo: params.itemNo,
      itemName: params.itemName,
      modelType: params.modelType,
      receiptDateFrom: params.receiptDateFrom,
      receiptDateTo: params.receiptDateTo,
      fiscalYear: params.fiscalYear,
      fiscalMonth: params.fiscalMonth,
      division: params.division === 'ALL' ? undefined : params.division,
    })}`,
  );
  return handleResponse(res);
}

export type PurchaseDailyApprovalFilter = 'ALL' | 'APPROVED' | 'PENDING';

export interface PurchaseDailyReportRow {
  historyId: number;
  ledgerKind: string;
  companyId: number;
  companyName: string;
  itemId: number | null;
  itemNo: string;
  itemName: string;
  unit: string;
  processName: string;
  inputDate: string;
  receiptDate: string;
  currentStockQty: number;
  receiptQty: number;
  passedQty: number;
  failedQty: number;
  standardUnitPrice: number;
  unitPrice: number;
  amount: number;
  /** 선급 상계액 */
  offsetAmount: number;
  /** 승인 행의 실지급대상 증가분 */
  unpaidIncrease: number;
  division: 'PURCHASE' | 'OUTSOURCE' | 'ETC' | 'CLAIM';
  approvalStatus: string;
  fiscalYear: number;
  fiscalMonth: number;
  /** 거래처별 선택월 합계 (검색 품목·일자와 무관) */
  monthTotal: number;
  /** 거래처별 선택연도 합계 (검색 품목·일자와 무관) */
  yearTotal: number;
}

export interface PurchaseDailyReportParams {
  companyId?: number;
  companyName?: string;
  itemId?: number;
  itemNo?: string;
  itemName?: string;
  inputDateFrom?: string;
  inputDateTo?: string;
  receiptDateFrom?: string;
  receiptDateTo?: string;
  fiscalYear?: number;
  fiscalMonth?: number;
  division?: VendorPurchaseDivision;
  approvalStatus?: PurchaseDailyApprovalFilter;
}

export async function fetchPurchaseDailyReport(
  params: PurchaseDailyReportParams = {},
): Promise<PurchaseDailyReportRow[]> {
  const res = await apiFetch(
    `/api/v1/stats/purchase-daily-report${toQuery({
      companyId: params.companyId,
      companyName: params.companyName,
      itemId: params.itemId,
      itemNo: params.itemNo,
      itemName: params.itemName,
      inputDateFrom: params.inputDateFrom,
      inputDateTo: params.inputDateTo,
      receiptDateFrom: params.receiptDateFrom,
      receiptDateTo: params.receiptDateTo,
      fiscalYear: params.fiscalYear,
      fiscalMonth: params.fiscalMonth,
      division: params.division === 'ALL' ? undefined : params.division,
      approvalStatus: params.approvalStatus === 'ALL' ? undefined : params.approvalStatus,
    })}`,
  );
  return handleResponse(res);
}

export interface PartnerMonthlyPayableRow {
  companyId: number;
  companyName: string;
  fiscalYear: number;
  fiscalMonth: number;
  approvedAmount: number;
  offsetAmount: number;
  payableAmount: number;
  paidAmount: number;
  unpaidAmount: number;
}

export interface PartnerMonthlyPayableParams {
  companyId?: number;
  companyName?: string;
  fiscalYear: number;
  fiscalMonth: number;
}

export async function fetchPartnerMonthlyPayable(
  params: PartnerMonthlyPayableParams,
): Promise<PartnerMonthlyPayableRow[]> {
  const res = await apiFetch(
    `/api/v1/stats/partner-monthly-payable${toQuery({
      companyId: params.companyId,
      companyName: params.companyName,
      fiscalYear: params.fiscalYear,
      fiscalMonth: params.fiscalMonth,
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

export type OrderVsReceiptDivision = 'ALL' | 'PURCHASE' | 'OUTSOURCE';

export interface OrderVsReceiptRow {
  orderLineId: number;
  division: 'PURCHASE' | 'OUTSOURCE';
  companyId: number;
  companyName: string;
  itemId: number;
  itemNo: string;
  itemName: string;
  modelType: string;
  processName: string;
  unit: string;
  standard: string;
  orderDate: string;
  orderQty: number;
  orderUnitPrice: number;
  orderAmount: number;
  requestedDeliveryDate?: string | null;
  lastReceiptDate?: string | null;
  receivedQty: number;
  waitingInspectionQty: number;
  receiptAmount: number;
  remainQty: number;
  remainAmount: number;
  currentStockQty: number;
}

export interface OrderVsReceiptParams {
  companyId?: number;
  companyName?: string;
  itemId?: number;
  itemNo?: string;
  itemName?: string;
  orderDateFrom?: string;
  orderDateTo?: string;
  receiptDateFrom?: string;
  receiptDateTo?: string;
  division?: OrderVsReceiptDivision;
}

export async function fetchOrderVsReceipt(params: OrderVsReceiptParams = {}): Promise<OrderVsReceiptRow[]> {
  const res = await apiFetch(
    `/api/v1/stats/order-vs-receipt${toQuery({
      companyId: params.companyId,
      companyName: params.companyName,
      itemId: params.itemId,
      itemNo: params.itemNo,
      itemName: params.itemName,
      orderDateFrom: params.orderDateFrom,
      orderDateTo: params.orderDateTo,
      receiptDateFrom: params.receiptDateFrom,
      receiptDateTo: params.receiptDateTo,
      division: params.division === 'ALL' ? undefined : params.division,
    })}`,
  );
  return handleResponse(res);
}
