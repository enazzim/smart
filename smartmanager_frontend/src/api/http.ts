import { translateInventoryLocationInText } from '../utils/inventoryLocation';

const TOKEN_KEY = 'smartmanager.accessToken';

let sessionExpiredNotified = false;
let onSessionExpired: (() => void) | null = null;

export function setSessionExpiredHandler(handler: (() => void) | null): void {
  onSessionExpired = handler;
}

export function resetSessionExpiredGuard(): void {
  sessionExpiredNotified = false;
}

function notifySessionExpired(): void {
  if (sessionExpiredNotified) {
    return;
  }
  sessionExpiredNotified = true;
  clearAccessToken();
  onSessionExpired?.();
}

export function getAccessToken(): string | null {
  return localStorage.getItem(TOKEN_KEY);
}

export function setAccessToken(token: string): void {
  resetSessionExpiredGuard();
  localStorage.setItem(TOKEN_KEY, token);
}

export function clearAccessToken(): void {
  localStorage.removeItem(TOKEN_KEY);
}

export function isAuthenticated(): boolean {
  return getAccessToken() !== null;
}

export async function apiFetch(input: RequestInfo | URL, init: RequestInit = {}): Promise<Response> {
  const headers = new Headers(init.headers);
  const token = getAccessToken();
  if (token) {
    headers.set('Authorization', `Bearer ${token}`);
  }
  return fetch(input, { ...init, headers });
}

function apiErrorMessage(message: string | undefined, fallback: string): string {
  const raw = message?.trim() || fallback;
  return translateInventoryLocationInText(raw) || fallback;
}

export async function handleResponse<T>(response: Response): Promise<T> {
  if (response.status === 401) {
    const body = await response.clone().json().catch(() => ({ message: '' }));
    const message = typeof body.message === 'string' ? body.message : '';
    const hadToken = getAccessToken() !== null;
    // "인증이 필요합니다"는 미인증/권한 선검증 실패에도 쓰이므로 만료로 보지 않는다.
    // 만료 문구가 명확할 때만 토큰을 지우고 로그인 화면으로 보낸다.
    const isSessionExpired =
      message.includes('인증이 만료') ||
      message.includes('만료되었습니다') ||
      message.toLowerCase().includes('expired');
    if (hadToken && isSessionExpired) {
      notifySessionExpired();
      throw new Error(apiErrorMessage(message, '인증이 만료되었습니다. 다시 로그인해 주세요.'));
    }
    throw new Error(apiErrorMessage(message, '인증이 필요합니다. 다시 로그인해 주세요.'));
  }
  if (response.status === 403) {
    const body = await response.json().catch(() => ({ message: '' }));
    const raw = typeof body.message === 'string' ? body.message.trim() : '';
    const normalized =
      !raw || raw === 'Forbidden' || raw.toLowerCase() === 'access denied'
        ? '접근 권한이 없습니다. 로그아웃 후 다시 로그인해 보세요.'
        : raw;
    throw new Error(apiErrorMessage(normalized, '접근 권한이 없습니다.'));
  }
  if (!response.ok) {
    const body = await response.json().catch(() => ({ message: response.statusText }));
    throw new Error(apiErrorMessage(body.message, '요청에 실패했습니다.'));
  }
  if (response.status === 204 || response.status === 205) {
    return undefined as T;
  }
  const text = await response.text();
  if (!text.trim()) {
    return undefined as T;
  }
  return JSON.parse(text) as T;
}
