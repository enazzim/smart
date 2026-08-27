import { apiFetch, handleResponse } from './http';

const BASE = '/api/v1/system/imports/master-data';

/** 대용량(1만 행+) 한 요청 시 프록시 타임아웃·Failed to fetch 방지 */
const BULK_CHUNK_SIZE = 200;
const MAX_FAILURE_DETAILS = 100;

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

async function postBulkOnce<T>(path: string, rows: T[]): Promise<BulkImportResult> {
  return handleResponse<BulkImportResult>(
    await apiFetch(`${BASE}${path}`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ rows }),
    }),
  );
}

async function postBulk<T>(path: string, rows: T[]): Promise<BulkImportResult> {
  if (rows.length <= BULK_CHUNK_SIZE) {
    return postBulkOnce(path, rows);
  }

  let successCount = 0;
  let failureCount = 0;
  const failures: BulkImportFailure[] = [];

  for (let offset = 0; offset < rows.length; offset += BULK_CHUNK_SIZE) {
    const chunk = rows.slice(offset, offset + BULK_CHUNK_SIZE);
    const result = await postBulkOnce(path, chunk);
    successCount += result.successCount;
    failureCount += result.failureCount;
    for (const failure of result.failures) {
      if (failures.length >= MAX_FAILURE_DETAILS) {
        break;
      }
      failures.push({
        ...failure,
        rowIndex: failure.rowIndex + offset,
      });
    }
  }

  return { successCount, failureCount, failures };
}

export const importCompaniesBulk = (rows: unknown[]) => postBulk('/company', rows);
export const importItemsBulk = (rows: unknown[]) => postBulk('/item', rows);
export const importItemCompositionsBulk = (rows: unknown[]) => postBulk('/item-composition', rows);
export const importWorkCentersBulk = (rows: unknown[]) => postBulk('/work-center', rows);
export const importProcessesBulk = (rows: unknown[]) => postBulk('/process', rows);
export const importWorkStandardsBulk = (rows: unknown[]) => postBulk('/work-standard', rows);
export const importUnitPricesBulk = (rows: unknown[]) => postBulk('/unit-price', rows);
