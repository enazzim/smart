export interface Role {
  id: number;
  roleCode: string;
  roleName: string;
}

export interface User {
  id: number;
  loginId: string;
  name: string;
  contact?: string | null;
  email?: string | null;
  roleIds: number[];
  roleCodes: string[];
  workDiaryGroupId?: number | null;
  workDiaryGroupName?: string | null;
  createdAt: string;
}

export interface CreateUserRequest {
  loginId: string;
  password: string;
  name: string;
  contact?: string;
  email?: string;
  roleIds: number[];
  workDiaryGroupId?: number | null;
}

export interface UpdateUserRequest {
  name: string;
  password?: string;
  contact?: string;
  email?: string;
  roleIds: number[];
  workDiaryGroupId?: number | null;
}

export interface CodeOption {
  id: number;
  code: string;
  name: string;
}

const API_BASE = '/api/v1/basis/users';

async function handleResponse<T>(response: Response): Promise<T> {
  if (!response.ok) {
    const body = await response.json().catch(() => ({ message: response.statusText }));
    throw new Error(body.message ?? '요청에 실패했습니다.');
  }
  if (response.status === 204) {
    return undefined as T;
  }
  return response.json() as Promise<T>;
}

export async function fetchRoles(): Promise<Role[]> {
  return handleResponse<Role[]>(await fetch('/api/v1/system/roles'));
}

export async function fetchWorkDiaryGroups(): Promise<CodeOption[]> {
  return handleResponse<CodeOption[]>(
    await fetch('/api/v1/basis/code-groups/WORK_DIARY_GROUP/options'),
  );
}

export async function fetchUsers(query?: string): Promise<User[]> {
  const url = query?.trim() ? `${API_BASE}?q=${encodeURIComponent(query.trim())}` : API_BASE;
  return handleResponse<User[]>(await fetch(url));
}

export async function checkLoginId(loginId: string): Promise<boolean> {
  const result = await handleResponse<{ available: boolean }>(
    await fetch(`${API_BASE}/check-login-id?loginId=${encodeURIComponent(loginId.trim())}`),
  );
  return result.available;
}

export async function createUser(payload: CreateUserRequest): Promise<User> {
  return handleResponse<User>(
    await fetch(API_BASE, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateUser(id: number, payload: UpdateUserRequest): Promise<User> {
  return handleResponse<User>(
    await fetch(`${API_BASE}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteUser(id: number): Promise<void> {
  await handleResponse<void>(
    await fetch(`${API_BASE}/${id}`, {
      method: 'DELETE',
    }),
  );
}
