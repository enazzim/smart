export const DRAWING_PDF_MAX_BYTES = 104_857_600;
export const DRAWING_PDF_MAX_SIZE_LABEL = '100MB';

export function isDrawingPdfFile(file: File): boolean {
  return file.type === 'application/pdf' || file.name.toLowerCase().endsWith('.pdf');
}

export function isDrawingPdfWithinSizeLimit(file: File): boolean {
  return file.size <= DRAWING_PDF_MAX_BYTES;
}

export function validateDrawingPdfFile(file: File): string | null {
  if (!isDrawingPdfFile(file)) {
    return 'PDF 파일만 업로드 가능합니다.';
  }
  if (!isDrawingPdfWithinSizeLimit(file)) {
    return `도면 PDF는 ${DRAWING_PDF_MAX_SIZE_LABEL} 이하여야 합니다.`;
  }
  return null;
}
