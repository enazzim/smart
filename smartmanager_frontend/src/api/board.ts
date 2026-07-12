import { apiFetch, getAccessToken, handleResponse } from './http';

export type BoardType =
  | 'NOTICE'
  | 'PRESIDENT_NOTICE'
  | 'PRODUCT'
  | 'LASER'
  | 'INSTITUTE'
  | 'SALES_QC';

export const BOARD_TYPE_LABELS: Record<BoardType, string> = {
  NOTICE: '공지사항',
  PRESIDENT_NOTICE: '대표공지',
  PRODUCT: '생산자재공지',
  LASER: '레이저 공지',
  INSTITUTE: '연구소 공지',
  SALES_QC: '영업 QC공지',
};

export interface BoardPostSummary {
  id: number;
  boardType: BoardType;
  postKind: 'TOP' | 'REPLY';
  title: string;
  authorName: string;
  viewCount: number;
  pinned: boolean;
  hasAttachment: boolean;
  createdAt: string;
}

export interface BoardAttachment {
  id: number;
  postId: number;
  originalFileName: string;
  contentType: string | null;
  fileSize: number;
  createdAt: string;
}

export interface BoardPostDetail {
  id: number;
  boardType: BoardType;
  postKind: 'TOP' | 'REPLY';
  parentPostId: number | null;
  threadRootId: number | null;
  title: string;
  content: string;
  authorUserId: number;
  authorName: string;
  viewCount: number;
  pinned: boolean;
  createdAt: string;
  updatedAt: string | null;
  attachments: BoardAttachment[];
  replies: BoardPostDetail[];
  canEdit: boolean;
  canDelete: boolean;
}

export interface BoardPostPage {
  items: BoardPostSummary[];
  totalElements: number;
  page: number;
  size: number;
}

export interface DashboardWidget {
  boardType: BoardType;
  title: string;
  items: BoardPostSummary[];
}

export interface DashboardWidgetsResponse {
  widgets: DashboardWidget[];
}

export async function fetchEditorTemplate(): Promise<string> {
  const res = await apiFetch('/api/v1/boards/editor-template');
  const body = await handleResponse<{ html: string }>(res);
  return body.html;
}

export async function fetchDashboardWidgets(limit = 5): Promise<DashboardWidget[]> {
  const res = await apiFetch(`/api/v1/dashboard/widgets?limit=${limit}`);
  const body = await handleResponse<DashboardWidgetsResponse>(res);
  return body.widgets;
}

export async function fetchBoardPosts(
  boardType: BoardType,
  params?: { keyword?: string; page?: number; size?: number },
): Promise<BoardPostPage> {
  const query = new URLSearchParams();
  if (params?.keyword) query.set('keyword', params.keyword);
  if (params?.page != null) query.set('page', String(params.page));
  if (params?.size != null) query.set('size', String(params.size));
  const suffix = query.toString() ? `?${query.toString()}` : '';
  const res = await apiFetch(`/api/v1/boards/${boardType}/posts${suffix}`);
  return handleResponse<BoardPostPage>(res);
}

export async function fetchBoardPost(
  boardType: BoardType,
  postId: number,
  options?: { incrementView?: boolean },
): Promise<BoardPostDetail> {
  const incrementView = options?.incrementView !== false;
  const suffix = incrementView ? '' : '?incrementView=false';
  const res = await apiFetch(`/api/v1/boards/${boardType}/posts/${postId}${suffix}`);
  return handleResponse<BoardPostDetail>(res);
}

export async function createBoardPost(
  boardType: BoardType,
  payload: { title: string; content: string; files?: File[] },
): Promise<BoardPostDetail> {
  const form = new FormData();
  form.append('title', payload.title);
  form.append('content', payload.content);
  payload.files?.forEach((file) => form.append('files', file));
  const res = await apiFetch(`/api/v1/boards/${boardType}/posts`, {
    method: 'POST',
    body: form,
  });
  return handleResponse<BoardPostDetail>(res);
}

export async function createBoardReply(
  boardType: BoardType,
  postId: number,
  payload: { content: string; files?: File[] },
): Promise<BoardPostDetail> {
  const form = new FormData();
  form.append('content', payload.content);
  payload.files?.forEach((file) => form.append('files', file));
  const res = await apiFetch(`/api/v1/boards/${boardType}/posts/${postId}/replies`, {
    method: 'POST',
    body: form,
  });
  return handleResponse<BoardPostDetail>(res);
}

export async function updateBoardPost(
  boardType: BoardType,
  postId: number,
  payload: { title?: string; content?: string },
): Promise<BoardPostDetail> {
  const form = new FormData();
  if (payload.title != null) form.append('title', payload.title);
  if (payload.content != null) form.append('content', payload.content);
  const res = await apiFetch(`/api/v1/boards/${boardType}/posts/${postId}`, {
    method: 'PUT',
    body: form,
  });
  return handleResponse<BoardPostDetail>(res);
}

export async function deleteBoardPost(boardType: BoardType, postId: number): Promise<void> {
  const res = await apiFetch(`/api/v1/boards/${boardType}/posts/${postId}`, { method: 'DELETE' });
  await handleResponse<void>(res);
}

export async function addBoardAttachments(
  boardType: BoardType,
  postId: number,
  files: File[],
): Promise<BoardAttachment[]> {
  const form = new FormData();
  files.forEach((file) => form.append('files', file));
  const res = await apiFetch(`/api/v1/boards/${boardType}/posts/${postId}/attachments`, {
    method: 'POST',
    body: form,
  });
  return handleResponse<BoardAttachment[]>(res);
}

export async function replaceBoardAttachment(
  boardType: BoardType,
  postId: number,
  attachmentId: number,
  file: File,
): Promise<BoardAttachment> {
  const form = new FormData();
  form.append('file', file);
  const res = await apiFetch(
    `/api/v1/boards/${boardType}/posts/${postId}/attachments/${attachmentId}`,
    { method: 'PUT', body: form },
  );
  return handleResponse<BoardAttachment>(res);
}

export async function deleteBoardAttachment(
  boardType: BoardType,
  postId: number,
  attachmentId: number,
): Promise<void> {
  const res = await apiFetch(
    `/api/v1/boards/${boardType}/posts/${postId}/attachments/${attachmentId}`,
    { method: 'DELETE' },
  );
  await handleResponse<void>(res);
}

export async function downloadBoardAttachment(
  boardType: BoardType,
  postId: number,
  attachmentId: number,
  fileName: string,
): Promise<void> {
  const token = getAccessToken();
  const res = await fetch(
    `/api/v1/boards/${boardType}/posts/${postId}/attachments/${attachmentId}/download`,
    {
      headers: token ? { Authorization: `Bearer ${token}` } : {},
    },
  );
  if (!res.ok) {
    const body = await res.json().catch(() => ({ message: '다운로드에 실패했습니다.' }));
    throw new Error(body.message ?? '다운로드에 실패했습니다.');
  }
  const blob = await res.blob();
  const url = URL.createObjectURL(blob);
  const anchor = document.createElement('a');
  anchor.href = url;
  anchor.download = fileName;
  anchor.click();
  URL.revokeObjectURL(url);
}

export function formatBoardDate(value: string | null | undefined): string {
  if (!value) return '—';
  const date = new Date(value);
  if (Number.isNaN(date.getTime())) return value;
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const day = String(date.getDate()).padStart(2, '0');
  return `${month}/${day}`;
}

/** 로컬 기준으로 당일 등록(또는 당일 일자) 여부 */
export function isRegisteredToday(value: string | null | undefined, now = new Date()): boolean {
  if (!value) return false;
  const raw = value.trim();
  if (/^\d{4}-\d{2}-\d{2}$/.test(raw)) {
    const [y, m, d] = raw.split('-').map(Number);
    return y === now.getFullYear() && m === now.getMonth() + 1 && d === now.getDate();
  }
  const date = new Date(raw);
  if (Number.isNaN(date.getTime())) return false;
  return (
    date.getFullYear() === now.getFullYear() &&
    date.getMonth() === now.getMonth() &&
    date.getDate() === now.getDate()
  );
}

export function formatFileSize(bytes: number): string {
  if (bytes < 1024) return `${bytes} B`;
  if (bytes < 1024 * 1024) return `${(bytes / 1024).toFixed(1)} KB`;
  return `${(bytes / (1024 * 1024)).toFixed(1)} MB`;
}
