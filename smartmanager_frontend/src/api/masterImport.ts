import { apiFetch, handleResponse } from './http';

const BASE = '/api/v1/system/imports/master-data';

export interface BulkImportFailure {
  rowIndex: number;
  key: string;
  message: string;
}

export interface BulkImportResult {
  successCount: number;
  failureCount: number;
  failures: BulkImportFailure[];
}

async function postBulk<T>(path: string, rows: T[]): Promise<BulkImportResult> {
  return handleResponse<BulkImportResult>(
    await apiFetch(`${BASE}${path}`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ rows }),
    }),
  );
}

export const importCompaniesBulk = (rows: unknown[]) => postBulk('/company', rows);
export const importItemsBulk = (rows: unknown[]) => postBulk('/item', rows);
export const importItemCompositionsBulk = (rows: unknown[]) => postBulk('/item-composition', rows);
export const importWorkCentersBulk = (rows: unknown[]) => postBulk('/work-center', rows);
export const importProcessesBulk = (rows: unknown[]) => postBulk('/process', rows);
export const importWorkStandardsBulk = (rows: unknown[]) => postBulk('/work-standard', rows);
export const importUnitPricesBulk = (rows: unknown[]) => postBulk('/unit-price', rows);
