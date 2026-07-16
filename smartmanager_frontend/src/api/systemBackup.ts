import { apiFetch, handleResponse } from './http';

const API = '/api/v1/system/backups';

export interface BackupFileInfo {
  kind: 'db-only';
  fileName: string;
  fileSizeBytes: number;
  createdAt: string;
  reason?: string | null;
}

export interface FullBackupSetInfo {
  kind: 'full';
  setName: string;
  sqlFileName: string;
  totalSizeBytes: number;
  drawingPdfFileCount: number;
  createdAt: string;
  reason?: string | null;
}

export type BackupListItem = BackupFileInfo | FullBackupSetInfo;

async function parseError(response: Response, fallback: string): Promise<string> {
  try {
    const body = (await response.json()) as { message?: string };
    return body.message?.trim() || fallback;
  } catch {
    return fallback;
  }
}

export async function fetchBackupList(): Promise<BackupListItem[]> {
  const [dbBackups, fullBackups] = await Promise.all([fetchDbBackups(), fetchFullBackups()]);
  const merged: BackupListItem[] = [
    ...dbBackups.map((item) => ({ ...item, kind: 'db-only' as const })),
    ...fullBackups.map((item) => ({ ...item, kind: 'full' as const })),
  ];
  merged.sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime());
  return merged;
}

/** @deprecated use fetchBackupList */
export async function fetchBackups(): Promise<BackupFileInfo[]> {
  const rows = await fetchDbBackups();
  return rows.map((item) => ({ ...item, kind: 'db-only' as const }));
}

async function fetchDbBackups(): Promise<Omit<BackupFileInfo, 'kind'>[]> {
  return handleResponse<Omit<BackupFileInfo, 'kind'>[]>(await apiFetch(API));
}

async function fetchFullBackups(): Promise<Omit<FullBackupSetInfo, 'kind'>[]> {
  return handleResponse<Omit<FullBackupSetInfo, 'kind'>[]>(await apiFetch(`${API}/full`));
}

export async function createBackup(reason: string): Promise<BackupFileInfo> {
  const created = await handleResponse<Omit<BackupFileInfo, 'kind'>>(
    await apiFetch(API, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ reason }),
    }),
  );
  return { ...created, kind: 'db-only' };
}

export async function createFullBackup(reason: string): Promise<FullBackupSetInfo> {
  const created = await handleResponse<Omit<FullBackupSetInfo, 'kind'>>(
    await apiFetch(`${API}/full`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ reason }),
    }),
  );
  return { ...created, kind: 'full' };
}

export async function downloadBackup(fileName: string): Promise<void> {
  const response = await apiFetch(`${API}/${encodeURIComponent(fileName)}/download`);
  if (response.status === 401) {
    throw new Error('인증이 필요합니다. 다시 로그인해 주세요.');
  }
  if (response.status === 403) {
    throw new Error('백업 파일 다운로드 권한이 없습니다.');
  }
  if (!response.ok) {
    throw new Error(await parseError(response, '백업 파일 다운로드에 실패했습니다.'));
  }
  const blob = await response.blob();
  const url = URL.createObjectURL(blob);
  const anchor = document.createElement('a');
  anchor.href = url;
  anchor.download = fileName;
  document.body.appendChild(anchor);
  anchor.click();
  anchor.remove();
  URL.revokeObjectURL(url);
}

export async function downloadFullBackup(setName: string): Promise<void> {
  const response = await apiFetch(`${API}/full/${encodeURIComponent(setName)}/download`);
  if (response.status === 401) {
    throw new Error('인증이 필요합니다. 다시 로그인해 주세요.');
  }
  if (response.status === 403) {
    throw new Error('백업 파일 다운로드 권한이 없습니다.');
  }
  if (!response.ok) {
    throw new Error(await parseError(response, '전체 백업 다운로드에 실패했습니다.'));
  }
  const blob = await response.blob();
  const url = URL.createObjectURL(blob);
  const anchor = document.createElement('a');
  anchor.href = url;
  anchor.download = `${setName}.zip`;
  document.body.appendChild(anchor);
  anchor.click();
  anchor.remove();
  URL.revokeObjectURL(url);
}

export async function deleteBackup(fileName: string): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API}/${encodeURIComponent(fileName)}`, { method: 'DELETE' }),
  );
}

export async function deleteFullBackup(setName: string): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API}/full/${encodeURIComponent(setName)}`, { method: 'DELETE' }),
  );
}

export async function restoreBackup(fileName: string): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API}/${encodeURIComponent(fileName)}/restore`, { method: 'POST' }),
  );
}

export async function restoreFullBackup(setName: string): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API}/full/${encodeURIComponent(setName)}/restore`, { method: 'POST' }),
  );
}
