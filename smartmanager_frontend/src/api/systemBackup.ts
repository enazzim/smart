import { apiFetch, handleResponse } from './http';

const API = '/api/v1/system/backups';

export interface BackupFileInfo {
  fileName: string;
  fileSizeBytes: number;
  createdAt: string;
}

async function parseError(response: Response, fallback: string): Promise<string> {
  try {
    const body = (await response.json()) as { message?: string };
    return body.message?.trim() || fallback;
  } catch {
    return fallback;
  }
}

export async function fetchBackups(): Promise<BackupFileInfo[]> {
  return handleResponse<BackupFileInfo[]>(await apiFetch(API));
}

export async function createBackup(): Promise<BackupFileInfo> {
  return handleResponse<BackupFileInfo>(
    await apiFetch(API, { method: 'POST' }),
  );
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

export async function deleteBackup(fileName: string): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API}/${encodeURIComponent(fileName)}`, { method: 'DELETE' }),
  );
}

export async function restoreBackup(fileName: string): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API}/${encodeURIComponent(fileName)}/restore`, { method: 'POST' }),
  );
}
