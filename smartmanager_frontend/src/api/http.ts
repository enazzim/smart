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

export async function handleResponse<T>(response: Response): Promise<T> {
  if (response.status === 401) {
    const body = await response.clone().json().catch(() => ({ message: '' }));
    const message = typeof body.message === 'string' ? body.message : '';
    const hadToken = getAccessToken() !== null;
    const isSessionExpired =
      message.includes('인증이 필요합니다') ||
      message.includes('인증이 만료') ||
      message.includes('만료');
    if (hadToken && isSessionExpired) {
      notifySessionExpired();
      throw new Error(message || '인증이 만료되었습니다. 다시 로그인해 주세요.');
    }
    throw new Error(message || '요청에 실패했습니다. API 서버 상태를 확인해 주세요.');
  }
  if (response.status === 403) {
    const body = await response.json().catch(() => ({ message: response.statusText }));
    throw new Error(body.message ?? '접근 권한이 없습니다.');
  }
  if (!response.ok) {
    const body = await response.json().catch(() => ({ message: response.statusText }));
    throw new Error(body.message ?? '요청에 실패했습니다.');
  }
  if (response.status === 204) {
    return undefined as T;
  }
  return response.json() as Promise<T>;
}
