import { createWorkbookWithSheet, downloadExcelWorkbook, normalizeExcelDate, parseFirstSheetRows } from './excelHelpers';
import type { SalesOrderRequest } from '../api/salesOrder';

/** KIT_ERP 수주일괄등록 양식 컬럼 (ExcelSample.xlsx) */
export const SALES_ORDER_TEMPLATE_HEADERS = [
  '품목번호',
  '거래처명',
  '사업자등록번호',
  '수주일자',
  '적용단가',
  '총발주량',
  '납기요구일',
  '발주번호',
  '납품장소',
  '생산의뢰여부',
] as const;

export const SALES_ORDER_ERROR_HEADERS = [...SALES_ORDER_TEMPLATE_HEADERS, '오류사유'] as const;

export type SalesOrderTemplateRow = Record<(typeof SALES_ORDER_TEMPLATE_HEADERS)[number], string | number>;

export interface ParsedSalesOrderRow {
  rowNumber: number;
  itemNo: string;
  partnerName: string;
  businessRegNo: string;
  /** ISO yyyy-MM-dd */
  orderDate: string;
  unitPrice?: number;
  orderQty: number;
  /** ISO yyyy-MM-dd */
  requestedDeliveryDate: string;
  /** 레거시 발주번호 → Smart 수주번호 */
  orderNo: string;
  deliveryPlace: string;
  productionRequestYn: string;
}

export interface SalesOrderBulkErrorRow extends ParsedSalesOrderRow {
  errorReason: string;
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

function toLegacyDateDisplay(iso: string): string {
  if (!iso) {
    return '';
  }
  return iso.replace(/-/g, '');
}

function toErrorExportRow(row: SalesOrderBulkErrorRow): Record<string, string | number> {
  return {
    품목번호: row.itemNo,
    거래처명: row.partnerName,
    사업자등록번호: row.businessRegNo,
    수주일자: toLegacyDateDisplay(row.orderDate),
    적용단가: row.unitPrice ?? '',
    총발주량: row.orderQty || '',
    납기요구일: toLegacyDateDisplay(row.requestedDeliveryDate),
    발주번호: row.orderNo,
    납품장소: row.deliveryPlace,
    생산의뢰여부: row.productionRequestYn,
    오류사유: row.errorReason,
  };
}

function toBulkError(row: ParsedSalesOrderRow, errorReason: string): SalesOrderBulkErrorRow {
  return { ...row, errorReason };
}

export async function downloadSalesOrderTemplate() {
  const sampleRows: SalesOrderTemplateRow[] = [
    {
      품목번호: 'M3235-00001',
      거래처명: '대동공업㈜',
      사업자등록번호: '613-81-04369',
      수주일자: '20070701',
      적용단가: 20419,
      총발주량: 2000,
      납기요구일: '20070731',
      발주번호: 'TS0001',
      납품장소: '',
      생산의뢰여부: '',
    },
  ];
  const workbook = createWorkbookWithSheet('수주양식', SALES_ORDER_TEMPLATE_HEADERS, sampleRows);
  await downloadExcelWorkbook(workbook, '수주일괄등록양식.xlsx');
}

export async function downloadSalesOrderErrorExcel(errors: SalesOrderBulkErrorRow[]) {
  const rows = errors.map(toErrorExportRow);
  const workbook = createWorkbookWithSheet('오류목록', SALES_ORDER_ERROR_HEADERS, rows);
  await downloadExcelWorkbook(workbook, '수주일괄등록_오류목록.xlsx');
}

export async function parseSalesOrderExcel(buffer: ArrayBuffer): Promise<ParsedSalesOrderRow[]> {
  const rawRows = await parseFirstSheetRows(buffer);

  return rawRows
    .map((row, index) => ({
      rowNumber: index + 2,
      itemNo: cellString(row['품목번호']),
      partnerName: cellString(row['거래처명']),
      businessRegNo: cellString(row['사업자등록번호']),
      orderDate: normalizeDate(row['수주일자'] ?? row['수주일']),
      unitPrice: cellNumber(row['적용단가'] ?? row['단가']),
      orderQty: cellNumber(row['총발주량'] ?? row['수주수량']) ?? 0,
      requestedDeliveryDate: normalizeDate(row['납기요구일']),
      orderNo: cellString(row['발주번호'] ?? row['수주번호']),
      deliveryPlace: cellString(row['납품장소']),
      productionRequestYn: cellString(row['생산의뢰여부']),
    }))
    .filter(
      (row) =>
        row.businessRegNo ||
        row.itemNo ||
        row.orderDate ||
        row.requestedDeliveryDate ||
        row.orderQty > 0 ||
        row.orderNo,
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
  /** orders[i]에 대응하는 원본 엑셀 행들 (서버 실패 매핑용) */
  orderSourceRows: ParsedSalesOrderRow[][];
  errors: SalesOrderBulkErrorRow[];
}

export function buildBulkOrdersFromRows(
  rows: ParsedSalesOrderRow[],
  context: BuildBulkOrderContext,
): BulkOrderBuildResult {
  const errors: SalesOrderBulkErrorRow[] = [];
  const grouped = new Map<string, ParsedSalesOrderRow[]>();

  rows.forEach((row, index) => {
    const groupKey = row.orderNo.trim() || `__row_${index}`;
    const bucket = grouped.get(groupKey) ?? [];
    bucket.push(row);
    grouped.set(groupKey, bucket);
  });

  const orders: SalesOrderRequest[] = [];
  const orderSourceRows: ParsedSalesOrderRow[][] = [];

  grouped.forEach((groupRows) => {
    const header = groupRows[0];
    const regNo = normalizeBusinessRegNo(header.businessRegNo);
    const partner = context.companiesByRegNo.get(regNo);

    const partnerReason = !partner
      ? header.businessRegNo
        ? `거래처(사업자등록번호)를 찾을 수 없습니다: ${header.businessRegNo}`
        : '사업자등록번호는 필수입니다.'
      : null;
    const orderDateReason = !header.orderDate ? '수주일자는 필수입니다.' : null;
    const deliveryDateReason = !header.requestedDeliveryDate ? '납기요구일은 필수입니다.' : null;

    const lines: SalesOrderRequest['lines'] = [];
    const acceptedRows: ParsedSalesOrderRow[] = [];

    for (const row of groupRows) {
      const reasons: string[] = [];
      if (partnerReason) {
        reasons.push(
          row.businessRegNo
            ? `거래처(사업자등록번호)를 찾을 수 없습니다: ${row.businessRegNo}`
            : '사업자등록번호는 필수입니다.',
        );
      }
      if (orderDateReason) {
        reasons.push(orderDateReason);
      }
      if (deliveryDateReason) {
        reasons.push(deliveryDateReason);
      }
      if (!row.itemNo) {
        reasons.push('품목번호는 필수입니다.');
      }
      if (!row.orderQty || row.orderQty <= 0) {
        reasons.push('총발주량은 0보다 커야 합니다.');
      }

      const item =
        row.itemNo
          ? context.itemsByNo.get(row.itemNo.toUpperCase()) ?? context.itemsByNo.get(row.itemNo)
          : undefined;
      if (row.itemNo && !item) {
        reasons.push(`품목을 찾을 수 없습니다: ${row.itemNo}`);
      }

      let unitPrice: number | undefined = row.unitPrice;
      if (partner && item) {
        const defaultUnitPrice = context.unitPriceByPartnerItem.get(unitPriceKey(partner.id, item.id));
        unitPrice = row.unitPrice ?? defaultUnitPrice;
        if (unitPrice == null) {
          reasons.push('적용단가가 없습니다. 단가를 입력하거나 판매단가를 등록해 주세요.');
        }
      }

      if (reasons.length > 0) {
        errors.push(toBulkError(row, reasons.join(' / ')));
        continue;
      }

      lines.push({
        itemId: item!.id,
        orderQty: row.orderQty,
        unitPrice: unitPrice!,
        deliveryDate: row.requestedDeliveryDate || header.requestedDeliveryDate,
      });
      acceptedRows.push(row);
    }

    if (lines.length === 0 || !partner) {
      return;
    }

    orders.push({
      orderNo: header.orderNo.trim() || undefined,
      partnerId: partner.id,
      orderDate: header.orderDate,
      requestedDeliveryDate: header.requestedDeliveryDate,
      lines,
    });
    orderSourceRows.push(acceptedRows);
  });

  return { orders, orderSourceRows, errors };
}

export function mapServerFailuresToErrorRows(
  failures: { rowIndex: number; message: string }[],
  orderSourceRows: ParsedSalesOrderRow[][],
): SalesOrderBulkErrorRow[] {
  const errors: SalesOrderBulkErrorRow[] = [];
  for (const failure of failures) {
    const sources = orderSourceRows[failure.rowIndex] ?? [];
    if (sources.length === 0) {
      continue;
    }
    for (const row of sources) {
      errors.push(toBulkError(row, failure.message));
    }
  }
  return errors;
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
