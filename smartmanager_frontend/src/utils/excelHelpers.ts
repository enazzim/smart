import ExcelJS from 'exceljs';
import JSZip from 'jszip';

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

export type ParseSheetOptions = {
  fileName?: string;
};

function looksLikeCsv(buffer: ArrayBuffer, fileName?: string): boolean {
  if (fileName && /\.csv$/i.test(fileName)) {
    return true;
  }
  const head = new TextDecoder('utf-8').decode(buffer.slice(0, 64));
  // xlsx(zip)는 PK로 시작. CSV는 일반 텍스트.
  return !head.startsWith('PK') && /[,;\t]/.test(head);
}

function parseCsvText(text: string): Record<string, unknown>[] {
  const lines = text
    .replace(/^\uFEFF/, '')
    .split(/\r?\n/)
    .filter((line) => line.trim().length > 0);
  if (lines.length === 0) {
    return [];
  }
  const delimiter = lines[0].includes('\t') ? '\t' : lines[0].includes(';') ? ';' : ',';
  const split = (line: string): string[] => {
    const cells: string[] = [];
    let current = '';
    let inQuotes = false;
    for (let i = 0; i < line.length; i += 1) {
      const ch = line[i];
      if (ch === '"') {
        if (inQuotes && line[i + 1] === '"') {
          current += '"';
          i += 1;
        } else {
          inQuotes = !inQuotes;
        }
        continue;
      }
      if (ch === delimiter && !inQuotes) {
        cells.push(current.trim());
        current = '';
        continue;
      }
      current += ch;
    }
    cells.push(current.trim());
    return cells;
  };
  const headers = split(lines[0]);
  return lines.slice(1).map((line) => {
    const values = split(line);
    const record: Record<string, unknown> = {};
    headers.forEach((header, index) => {
      if (!header) return;
      record[header] = values[index] ?? '';
    });
    return record;
  });
}

/**
 * 한셀/비표준 오피스에서 저장한 xlsx는 ExcelJS가 docProps 파싱에 실패하는 경우가 있음.
 * (Cannot read properties of undefined (reading 'company'))
 */
async function sanitizeXlsxForExcelJs(buffer: ArrayBuffer): Promise<ArrayBuffer> {
  const zip = await JSZip.loadAsync(buffer);
  // 메타데이터 파싱 실패를 피하기 위해 제거 (시트 데이터 읽기에는 불필요)
  zip.remove('docProps/app.xml');
  zip.remove('docProps/core.xml');

  const xmlPaths = Object.keys(zip.files).filter(
    (path) => !zip.files[path].dir && (path.endsWith('.xml') || path.endsWith('.rels')),
  );
  await Promise.all(
    xmlPaths.map(async (path) => {
      const file = zip.file(path);
      if (!file) return;
      let text = await file.async('string');
      // 한셀 등에서 사용하는 x: 네임스페이스 접두사 제거
      if (text.includes('<x:') || text.includes('</x:')) {
        text = text.replace(/<\/?x:/g, (match) => match.replace('x:', ''));
        zip.file(path, text);
      }
    }),
  );

  return zip.generateAsync({ type: 'arraybuffer' });
}

async function loadWorkbookFromBuffer(buffer: ArrayBuffer): Promise<ExcelJS.Workbook> {
  const workbook = new ExcelJS.Workbook();
  try {
    await workbook.xlsx.load(buffer);
    return workbook;
  } catch (firstError) {
    try {
      const sanitized = await sanitizeXlsxForExcelJs(buffer);
      const recovered = new ExcelJS.Workbook();
      await recovered.xlsx.load(sanitized);
      return recovered;
    } catch {
      const detail = firstError instanceof Error ? firstError.message : String(firstError);
      throw new Error(
        `엑셀 파일을 읽지 못했습니다. 화면의 「양식 다운로드」로 받은 파일을 사용하거나, `
          + `Microsoft Excel/한셀에서 다시 저장(.xlsx)한 뒤 업로드해 주세요. (${detail})`,
      );
    }
  }
}

function worksheetToRows(worksheet: ExcelJS.Worksheet): Record<string, unknown>[] {
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

export async function parseFirstSheetRows(
  buffer: ArrayBuffer,
  options?: ParseSheetOptions,
): Promise<Record<string, unknown>[]> {
  if (looksLikeCsv(buffer, options?.fileName)) {
    const text = new TextDecoder('utf-8').decode(buffer);
    return parseCsvText(text);
  }

  const workbook = await loadWorkbookFromBuffer(buffer);
  const worksheet = workbook.worksheets[0];
  if (!worksheet) throw new Error('엑셀 시트를 찾을 수 없습니다.');
  return worksheetToRows(worksheet);
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
