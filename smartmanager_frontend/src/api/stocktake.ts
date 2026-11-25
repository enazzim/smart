import { apiFetch, handleResponse } from './http';

export interface StocktakeBookRow {
  itemNo: string;
  itemName: string;
  propertyClassification: string | null;
  locationCode: string;
  locationName: string;
  outputProcessId: number | null;
  processName: string | null;
  bookQty: number | null;
  error: string | null;
  partnerNo: string | null;
  partnerName: string | null;
  inputProcessId: number | null;
  lotNo: string | null;
}

export interface StocktakeLinePayload {
  rowNo: number;
  itemNo: string;
  processSequenceId: number | null;
  partnerNo: string | null;
  lotNo: string | null;
  countedQty: number | null;
  expectedBookQty: number | null;
}

export interface StocktakePreviewRow {
  rowNo: number;
  itemNo: string | null;
  itemName: string | null;
  locationCode: string | null;
  outputProcessId: number | null;
  processName: string | null;
  bookQty: number | null;
  countedQty: number | null;
  action: string;
  movementQty: number | null;
  reasonName: string | null;
  error: string | null;
  partnerNo: string | null;
  lotNo: string | null;
}

export function fetchStocktakeBook(stocktakeDate: string): Promise<StocktakeBookRow[]> {
  const params = new URLSearchParams({ stocktakeDate });
  return apiFetch(`/api/v1/inventory/stocktakes/book?${params.toString()}`).then((response) =>
    handleResponse<StocktakeBookRow[]>(response),
  );
}

export function previewStocktake(
  stocktakeDate: string,
  lines: StocktakeLinePayload[],
): Promise<StocktakePreviewRow[]> {
  return apiFetch('/api/v1/inventory/stocktakes/preview', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ stocktakeDate, lines }),
  }).then((response) => handleResponse<StocktakePreviewRow[]>(response));
}

export function commitStocktake(
  stocktakeDate: string,
  lines: StocktakeLinePayload[],
): Promise<{ batchNo: string }> {
  return apiFetch('/api/v1/inventory/stocktakes', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ stocktakeDate, lines }),
  }).then((response) => handleResponse<{ batchNo: string }>(response));
}
