import { apiFetch, clearAccessToken, handleResponse, isAuthenticated, setAccessToken } from './http';

export { isAuthenticated };

export interface AuthenticatedUser {
  id: number;
  loginId: string;
  name: string;
  roleCodes: string[];
  authorities: string[];
}

export interface AuthTokenResponse {
  accessToken: string;
  tokenType: string;
  expiresInSeconds: number;
  user: AuthenticatedUser;
}

const AUTH_BASE = '/api/v1/auth';

export async function login(loginId: string, password: string): Promise<AuthTokenResponse> {
  const response = await fetch(`${AUTH_BASE}/login`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ loginId, password }),
  });
  if (!response.ok) {
    const body = await response.json().catch(() => ({ message: '로그인에 실패했습니다.' }));
    throw new Error(body.message ?? '로그인에 실패했습니다.');
  }
  const data = (await response.json()) as AuthTokenResponse;
  setAccessToken(data.accessToken);
  return data;
}

export async function fetchCurrentUser(): Promise<AuthenticatedUser> {
  return handleResponse<AuthenticatedUser>(await apiFetch(`${AUTH_BASE}/me`));
}

export function logout(): void {
  clearAccessToken();
}

export async function changeMyPassword(currentPassword: string, newPassword: string): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${AUTH_BASE}/me/password`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ currentPassword, newPassword }),
    }),
  );
}
