import { createWorkbookFromObjects, downloadExcelWorkbook } from './excelHelpers';

function fileTimestamp(): string {
  const d = new Date();
  const pad = (n: number) => String(n).padStart(2, '0');
  return `${d.getFullYear()}${pad(d.getMonth() + 1)}${pad(d.getDate())}_${pad(d.getHours())}${pad(d.getMinutes())}`;
}

export async function downloadGridExcel(
  fileBaseName: string,
  sheetName: string,
  rows: Record<string, string | number>[],
) {
  const workbook = createWorkbookFromObjects(sheetName.slice(0, 31), rows);
  await downloadExcelWorkbook(workbook, `${fileBaseName}_${fileTimestamp()}.xlsx`);
}
