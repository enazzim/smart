import * as XLSX from 'xlsx';
import type { BomTreeNode, ItemComposition } from '../api/itemComposition';

function fileTimestamp(): string {
  const d = new Date();
  const pad = (n: number) => String(n).padStart(2, '0');
  return `${d.getFullYear()}${pad(d.getMonth() + 1)}${pad(d.getDate())}_${pad(d.getHours())}${pad(d.getMinutes())}`;
}

function flattenVendorPrices(prices: BomTreeNode['outsourcePrices']): string {
  if (!prices || prices.length === 0) return '';
  return prices
    .map((price) => {
      const detail = price.detail ? ` (${price.detail})` : '';
      return `${price.partnerName}${detail} ${price.unitPrice}`;
    })
    .join(' / ');
}

function flattenExplosion(node: BomTreeNode): Record<string, string | number>[] {
  const rows: Record<string, string | number>[] = [];

  const walk = (current: BomTreeNode) => {
    rows.push({
      레벨: `L${current.level}`,
      품목번호: current.itemNum,
      품목명: current.itemName,
      자산분류: current.propertyClassification,
      누적수량: current.quantity,
      '외주거래처·단가': flattenVendorPrices(current.outsourcePrices),
      '구매거래처·단가': flattenVendorPrices(current.purchasePrices),
    });
    current.children.forEach(walk);
  };

  walk(node);
  return rows;
}

function writeWorkbook(filename: string, sheetName: string, rows: Record<string, string | number>[]) {
  const worksheet = XLSX.utils.json_to_sheet(rows);
  const workbook = XLSX.utils.book_new();
  XLSX.utils.book_append_sheet(workbook, worksheet, sheetName);
  XLSX.writeFile(workbook, filename);
}

export function downloadExplosionExcel(root: BomTreeNode) {
  writeWorkbook(
    `BOM정전개_${root.itemNum}_${fileTimestamp()}.xlsx`,
    '정전개',
    flattenExplosion(root),
  );
}

export function downloadReverseExcel(childItemNum: string, rows: ItemComposition[]) {
  writeWorkbook(
    `BOM역전개_${childItemNum}_${fileTimestamp()}.xlsx`,
    '역전개',
    rows.map((row) => ({
      모품목번호: row.parentItemNo,
      모품목명: row.parentItemName,
      자품목번호: row.childItemNo,
      자품목명: row.childItemName,
      모품수량: row.parentQuantity,
      자품수량: row.childQuantity,
    })),
  );
}
