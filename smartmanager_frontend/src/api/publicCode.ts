import { apiFetch, handleResponse } from './http';

export type PublicCodeUsageType =
  | 'GENERIC'
  | 'PROCESS'
  | 'UNIT'
  | 'NC_REASON'
  | 'NC_DETAIL'
  | 'WORK_DIARY_GROUP';

export interface PublicCodeLarge {
  id: number;
  largeCode: string;
  largeName: string;
  usageType: PublicCodeUsageType;
}

export interface PublicCodeSmall {
  id: number;
  largeCode: string;
  largeName: string;
  smallCode: string;
  smallName: string;
  usageType: PublicCodeUsageType;
}

export interface CreateLargePublicCodeRequest {
  largeCode: string;
  largeName: string;
  usageType: PublicCodeUsageType;
}

export interface CreateSmallPublicCodeRequest {
  largeCode: string;
  smallCode: string;
  smallName: string;
}

export interface UpdateLargePublicCodeRequest {
  largeName: string;
  usageType: PublicCodeUsageType;
}

export interface UpdateSmallPublicCodeRequest {
  smallName: string;
}

const SYSTEM_API_BASE = '/api/v1/system/public-codes';

export const USAGE_TYPE_OPTIONS: { value: PublicCodeUsageType; label: string }[] = [
  { value: 'GENERIC', label: 'GENERIC (일반)' },
  { value: 'PROCESS', label: 'PROCESS (공정)' },
  { value: 'UNIT', label: 'UNIT (단위)' },
  { value: 'NC_REASON', label: 'NC_REASON (부적합원인)' },
  { value: 'NC_DETAIL', label: 'NC_DETAIL (부적합현상)' },
  { value: 'WORK_DIARY_GROUP', label: 'WORK_DIARY_GROUP (업무일지)' },
];

export async function fetchLargePublicCodes(): Promise<PublicCodeLarge[]> {
  return handleResponse<PublicCodeLarge[]>(await apiFetch(`${SYSTEM_API_BASE}/large`));
}

export async function fetchSmallPublicCodes(largeCode: string): Promise<PublicCodeSmall[]> {
  const url = `${SYSTEM_API_BASE}/small?largeCode=${encodeURIComponent(largeCode)}`;
  return handleResponse<PublicCodeSmall[]>(await apiFetch(url));
}

export async function createLargePublicCode(
  payload: CreateLargePublicCodeRequest,
): Promise<PublicCodeLarge> {
  return handleResponse<PublicCodeLarge>(
    await apiFetch(`${SYSTEM_API_BASE}/large`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function createSmallPublicCode(
  payload: CreateSmallPublicCodeRequest,
): Promise<PublicCodeSmall> {
  return handleResponse<PublicCodeSmall>(
    await apiFetch(`${SYSTEM_API_BASE}/small`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateLargePublicCode(
  largeCode: string,
  payload: UpdateLargePublicCodeRequest,
): Promise<PublicCodeLarge> {
  return handleResponse<PublicCodeLarge>(
    await apiFetch(`${SYSTEM_API_BASE}/large/${encodeURIComponent(largeCode)}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateSmallPublicCode(
  id: number,
  payload: UpdateSmallPublicCodeRequest,
): Promise<PublicCodeSmall> {
  return handleResponse<PublicCodeSmall>(
    await apiFetch(`${SYSTEM_API_BASE}/small/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteLargePublicCode(largeCode: string): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${SYSTEM_API_BASE}/large/${encodeURIComponent(largeCode)}`, {
      method: 'DELETE',
    }),
  );
}

export async function deleteSmallPublicCode(id: number): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${SYSTEM_API_BASE}/small/${id}`, {
      method: 'DELETE',
    }),
  );
}
