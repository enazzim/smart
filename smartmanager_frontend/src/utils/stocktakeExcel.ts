import ExcelJS from 'exceljs';
import type { StocktakeBookRow, StocktakeLinePayload } from '../api/stocktake';
import { downloadExcelWorkbook, extractCellValue, loadWorkbookFromBuffer } from './excelHelpers';

const HEADERS = ['품목번호', '품목명', '재고분류', '창고', '거래처번호', '거래처명', '공정ID', '공정명', 'Lot번호', '장부수량', '실사수량', '오류'];

export async function downloadStocktakeBook(rows: StocktakeBookRow[], stocktakeDate: string): Promise<void> {
  const workbook = new ExcelJS.Workbook();
  const sheet = workbook.addWorksheet('재고실사');
  sheet.addRow(HEADERS);
  for (const row of rows) {
    sheet.addRow([
      row.itemNo,
      row.itemName,
      row.propertyClassification ?? '',
      row.locationCode,
      row.partnerNo ?? '',
      row.partnerName ?? '',
      row.inputProcessId ?? row.outputProcessId ?? '',
      row.processName ?? '',
      row.lotNo ?? '',
      row.bookQty ?? '',
      '',
      row.error ?? '',
    ]);
  }
  sheet.getRow(1).font = { bold: true };
  sheet.columns.forEach((column) => {
    column.width = 16;
  });
  await downloadExcelWorkbook(workbook, `재고실사_${stocktakeDate}.xlsx`);
}

export async function readStocktakeUpload(file: ArrayBuffer): Promise<StocktakeLinePayload[]> {
  const workbook = await loadWorkbookFromBuffer(file);
  const sheet = workbook.worksheets[0];
  if (!sheet) {
    throw new Error('시트가 없습니다.');
  }
  const header = sheet.getRow(1);
  const index = new Map<string, number>();
  header.eachCell((cell, col) => {
    const name = String(extractCellValue(cell.value) ?? '').trim();
    if (name) index.set(name, col);
  });
  const itemCol = index.get('품목번호');
  const processCol = index.get('공정ID');
  const countedCol = index.get('실사수량');
  if (!itemCol || !countedCol) {
    throw new Error('품목번호, 실사수량 열이 있는 실사 양식을 사용해 주세요.');
  }
  const partnerCol = index.get('거래처번호') ?? index.get('거래처');
  const lotCol = index.get('Lot번호');
  const lines: StocktakeLinePayload[] = [];
  for (let rowNo = 2; rowNo <= sheet.rowCount; rowNo += 1) {
    const row = sheet.getRow(rowNo);
    const itemNo = String(extractCellValue(row.getCell(itemCol).value) ?? '').trim();
    if (!itemNo) continue;
    lines.push({
      rowNo,
      itemNo,
      processSequenceId: processCol ? readLong(row.getCell(processCol).value) : null,
      partnerNo: partnerCol ? text(row.getCell(partnerCol).value) : null,
      lotNo: lotCol ? text(row.getCell(lotCol).value) : null,
      countedQty: readDecimal(row.getCell(countedCol).value),
      expectedBookQty: null,
    });
  }
  return lines;
}

function text(value: ExcelJS.CellValue): string | null {
  const parsed = String(extractCellValue(value) ?? '').trim();
  return parsed === '' ? null : parsed;
}

function readLong(value: ExcelJS.CellValue): number | null {
  const parsed = text(value);
  if (!parsed) return null;
  const number = Number(parsed);
  return Number.isFinite(number) ? number : null;
}

function readDecimal(value: ExcelJS.CellValue): number | null {
  const parsed = text(value);
  if (!parsed) return null;
  const number = Number(parsed.replace(/,/g, ''));
  return Number.isFinite(number) ? number : null;
}
