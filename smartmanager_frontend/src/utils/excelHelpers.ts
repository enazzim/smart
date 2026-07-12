import ExcelJS from 'exceljs';

export function extractCellValue(value: ExcelJS.CellValue): unknown {
  if (value == null) return '';
  if (value instanceof Date) return value;
  if (typeof value === 'object') {
    if ('result' in value && value.result != null) {
      return extractCellValue(value.result as ExcelJS.CellValue);
    }
    if ('text' in value && typeof value.text === 'string') {
      return value.text;
    }
    if ('richText' in value && Array.isArray(value.richText)) {
      return value.richText.map((part) => part.text).join('');
    }
    if ('hyperlink' in value) {
      return value.text ?? value.hyperlink;
    }
  }
  return value;
}

export function excelSerialToIsoDate(serial: number): string | null {
  if (!Number.isFinite(serial) || serial < 1) return null;
  const wholeDays = Math.floor(serial);
  const excelEpoch = Date.UTC(1899, 11, 30);
  return new Date(excelEpoch + wholeDays * 86_400_000).toISOString().slice(0, 10);
}

export function normalizeExcelDate(value: unknown): string {
  if (value == null || value === '') return '';
  if (value instanceof Date) return value.toISOString().slice(0, 10);
  if (typeof value === 'number') {
    const parsed = excelSerialToIsoDate(value);
    if (parsed) return parsed;
  }
  const text = String(value).trim();
  if (/^\d{4}-\d{2}-\d{2}$/.test(text)) return text;
  if (/^\d{8}$/.test(text)) return `${text.slice(0, 4)}-${text.slice(4, 6)}-${text.slice(6, 8)}`;
  if (/^\d{4}\/\d{2}\/\d{2}$/.test(text)) return text.replace(/\//g, '-');
  return text;
}

export async function downloadExcelWorkbook(workbook: ExcelJS.Workbook, fileName: string): Promise<void> {
  const buffer = await workbook.xlsx.writeBuffer();
  const blob = new Blob([buffer], {
    type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
  });
  const url = URL.createObjectURL(blob);
  const anchor = document.createElement('a');
  anchor.href = url;
  anchor.download = fileName;
  anchor.click();
  URL.revokeObjectURL(url);
}

export async function parseFirstSheetRows(buffer: ArrayBuffer): Promise<Record<string, unknown>[]> {
  const workbook = new ExcelJS.Workbook();
  await workbook.xlsx.load(buffer);
  const worksheet = workbook.worksheets[0];
  if (!worksheet) throw new Error('엑셀 시트를 찾을 수 없습니다.');

  const headers: string[] = [];
  worksheet.getRow(1).eachCell({ includeEmpty: false }, (cell, colNumber) => {
    headers[colNumber - 1] = String(extractCellValue(cell.value)).trim();
  });

  const rows: Record<string, unknown>[] = [];
  worksheet.eachRow((row, rowNumber) => {
    if (rowNumber === 1) return;
    const record: Record<string, unknown> = {};
    headers.forEach((header, index) => {
      if (!header) return;
      const raw = extractCellValue(row.getCell(index + 1).value);
      record[header] = raw === '' || raw == null ? '' : raw;
    });
    rows.push(record);
  });
  return rows;
}

export function createWorkbookWithSheet(
  sheetName: string,
  headers: readonly string[],
  rows: Record<string, string | number>[],
): ExcelJS.Workbook {
  const workbook = new ExcelJS.Workbook();
  const sheet = workbook.addWorksheet(sheetName);
  sheet.addRow([...headers]);
  for (const row of rows) {
    sheet.addRow(headers.map((header) => row[header] ?? ''));
  }
  return workbook;
}

export function createWorkbookFromObjects(sheetName: string, rows: Record<string, string | number>[]): ExcelJS.Workbook {
  const headers = rows.length > 0 ? Object.keys(rows[0]) : [];
  return createWorkbookWithSheet(sheetName, headers, rows);
}
