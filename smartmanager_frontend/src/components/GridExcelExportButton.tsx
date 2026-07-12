import { downloadGridExcel } from '../utils/gridExcelExport';

export interface GridExcelExportButtonProps {
  fileBaseName: string;
  sheetName?: string;
  rows: Record<string, string | number>[];
  disabled?: boolean;
}

export default function GridExcelExportButton({
  fileBaseName,
  sheetName = '목록',
  rows,
  disabled = false,
}: GridExcelExportButtonProps) {
  return (
    <button
      type="button"
      className="btn-grid-excel"
      disabled={disabled || rows.length === 0}
      onClick={() => void downloadGridExcel(fileBaseName, sheetName, rows)}
    >
      엑셀저장
    </button>
  );
}
