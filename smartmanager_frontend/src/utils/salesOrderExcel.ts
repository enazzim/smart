import { createWorkbookWithSheet, downloadExcelWorkbook, normalizeExcelDate, parseFirstSheetRows } from './excelHelpers';
import type { SalesOrderRequest } from '../api/salesOrder';

export const SALES_ORDER_TEMPLATE_HEADERS = [
  '수주번호',
  '사업자등록번호',
  '수주일',
  '납기요구일',
  '품목번호',
  '수주수량',
  '단가',
  '비고',
] as const;

export type SalesOrderTemplateRow = Record<(typeof SALES_ORDER_TEMPLATE_HEADERS)[number], string | number>;

export interface ParsedSalesOrderRow {
  rowNumber: number;
  orderNo: string;
  businessRegNo: string;
  orderDate: string;
  requestedDeliveryDate: string;
  itemNo: string;
  orderQty: number;
  unitPrice?: number;
  remark: string;
}

function normalizeBusinessRegNo(value: string): string {
  return value.replace(/\D/g, '');
}

function cellString(value: unknown): string {
  if (value == null) {
    return '';
  }
  return String(value).trim();
}

function cellNumber(value: unknown): number | undefined {
  const text = cellString(value).replace(/,/g, '');
  if (!text) {
    return undefined;
  }
  const parsed = Number(text);
  return Number.isFinite(parsed) ? parsed : undefined;
}

function normalizeDate(value: unknown): string {
  return normalizeExcelDate(value);
}

export async function downloadSalesOrderTemplate() {
  const sampleRows: SalesOrderTemplateRow[] = [
    {
      수주번호: '',
      사업자등록번호: '123-45-67890',
      수주일: '2026-07-05',
      납기요구일: '2026-07-20',
      품목번호: 'ITEM-001',
      수주수량: 10,
      단가: 1000,
      비고: '',
    },
  ];
  const workbook = createWorkbookWithSheet('수주양식', SALES_ORDER_TEMPLATE_HEADERS, sampleRows);
  await downloadExcelWorkbook(workbook, '수주일괄등록양식.xlsx');
}

export async function parseSalesOrderExcel(buffer: ArrayBuffer): Promise<ParsedSalesOrderRow[]> {
  const rawRows = await parseFirstSheetRows(buffer);

  return rawRows
    .map((row, index) => ({
      rowNumber: index + 2,
      orderNo: cellString(row['수주번호']),
      businessRegNo: cellString(row['사업자등록번호']),
      orderDate: normalizeDate(row['수주일']),
      requestedDeliveryDate: normalizeDate(row['납기요구일']),
      itemNo: cellString(row['품목번호']),
      orderQty: cellNumber(row['수주수량']) ?? 0,
      unitPrice: cellNumber(row['단가']),
      remark: cellString(row['비고']),
    }))
    .filter(
      (row) =>
        row.businessRegNo ||
        row.itemNo ||
        row.orderDate ||
        row.requestedDeliveryDate ||
        row.orderQty > 0,
    );
}

export interface BuildBulkOrderContext {
  companiesByRegNo: Map<string, { id: number; companyName: string }>;
  itemsByNo: Map<string, { id: number; itemNo: string }>;
  unitPriceByPartnerItem: Map<string, number>;
}

function unitPriceKey(partnerId: number, itemId: number): string {
  return `${partnerId}:${itemId}`;
}

export function buildUnitPriceLookup(
  unitPrices: { companyId: number; itemId: number; standardUnitCost: number; discountUnitCost?: number | null }[],
): Map<string, number> {
  const map = new Map<string, number>();
  for (const unitPrice of unitPrices) {
    const amount =
      unitPrice.discountUnitCost != null && unitPrice.discountUnitCost > 0
        ? unitPrice.discountUnitCost
        : unitPrice.standardUnitCost;
    map.set(unitPriceKey(unitPrice.companyId, unitPrice.itemId), amount);
  }
  return map;
}

export interface BulkOrderBuildResult {
  orders: SalesOrderRequest[];
  errors: { rowNumber: number; message: string }[];
}

export function buildBulkOrdersFromRows(
  rows: ParsedSalesOrderRow[],
  context: BuildBulkOrderContext,
): BulkOrderBuildResult {
  const errors: { rowNumber: number; message: string }[] = [];
  const grouped = new Map<string, ParsedSalesOrderRow[]>();

  rows.forEach((row, index) => {
    const groupKey = row.orderNo.trim() || `__row_${index}`;
    const bucket = grouped.get(groupKey) ?? [];
    bucket.push(row);
    grouped.set(groupKey, bucket);
  });

  const orders: SalesOrderRequest[] = [];

  grouped.forEach((groupRows) => {
    const header = groupRows[0];
    const regNo = normalizeBusinessRegNo(header.businessRegNo);
    const partner = context.companiesByRegNo.get(regNo);

    if (!partner) {
      groupRows.forEach((row) =>
        errors.push({ rowNumber: row.rowNumber, message: `사업자등록번호를 찾을 수 없습니다: ${row.businessRegNo}` }),
      );
      return;
    }
    if (!header.orderDate) {
      errors.push({ rowNumber: header.rowNumber, message: '수주일은 필수입니다.' });
      return;
    }
    if (!header.requestedDeliveryDate) {
      errors.push({ rowNumber: header.rowNumber, message: '납기요구일은 필수입니다.' });
      return;
    }

    const lines: SalesOrderRequest['lines'] = [];
    for (const row of groupRows) {
      if (!row.itemNo) {
        errors.push({ rowNumber: row.rowNumber, message: '품목번호는 필수입니다.' });
        continue;
      }
      if (!row.orderQty || row.orderQty <= 0) {
        errors.push({ rowNumber: row.rowNumber, message: '수주수량은 0보다 커야 합니다.' });
        continue;
      }

      const item = context.itemsByNo.get(row.itemNo.toUpperCase()) ?? context.itemsByNo.get(row.itemNo);
      if (!item) {
        errors.push({ rowNumber: row.rowNumber, message: `품목을 찾을 수 없습니다: ${row.itemNo}` });
        continue;
      }

      const defaultUnitPrice = context.unitPriceByPartnerItem.get(unitPriceKey(partner.id, item.id));
      const unitPrice = row.unitPrice ?? defaultUnitPrice;
      if (unitPrice == null) {
        errors.push({
          rowNumber: row.rowNumber,
          message: `판매단가가 없습니다. 단가를 입력하거나 판매단가를 등록해 주세요.`,
        });
        continue;
      }

      lines.push({
        itemId: item.id,
        orderQty: row.orderQty,
        unitPrice,
        deliveryDate: row.requestedDeliveryDate || header.requestedDeliveryDate,
      });
    }

    if (lines.length === 0) {
      return;
    }

    orders.push({
      orderNo: header.orderNo.trim() || undefined,
      partnerId: partner.id,
      orderDate: header.orderDate,
      requestedDeliveryDate: header.requestedDeliveryDate,
      remark: header.remark || undefined,
      lines,
    });
  });

  return { orders, errors };
}

export function indexCompaniesByRegNo(
  companies: { id: number; companyName: string; businessRegNo: string; roles: string[] }[],
) {
  const map = new Map<string, { id: number; companyName: string }>();
  for (const company of companies) {
    if (!company.roles.includes('SALES')) {
      continue;
    }
    map.set(normalizeBusinessRegNo(company.businessRegNo), {
      id: company.id,
      companyName: company.companyName,
    });
  }
  return map;
}

export function indexItemsByNo(items: { id: number; itemNo: string }[]) {
  const map = new Map<string, { id: number; itemNo: string }>();
  for (const item of items) {
    map.set(item.itemNo, { id: item.id, itemNo: item.itemNo });
    map.set(item.itemNo.toUpperCase(), { id: item.id, itemNo: item.itemNo });
  }
  return map;
}
