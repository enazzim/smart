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
  const data = await handleResponse<AuthTokenResponse>(response);
  setAccessToken(data.accessToken);
  return data;
}

export async function fetchCurrentUser(): Promise<AuthenticatedUser> {
  return handleResponse<AuthenticatedUser>(await apiFetch(`${AUTH_BASE}/me`));
}

export function logout(): void {
  clearAccessToken();
}
