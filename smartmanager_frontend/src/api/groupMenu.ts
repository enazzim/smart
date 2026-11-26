import { apiFetch, handleResponse } from './http';

export type GroupMenuAssignment = {
  id: number;
  name: string;
  menuIds: string[];
};

export type GroupMenuItem = {
  categoryLabel: string;
  menuId: string;
  menuLabel: string;
};

export type GroupMenuSettings = {
  groups: GroupMenuAssignment[];
  menus: GroupMenuItem[];
};

export async function fetchGroupMenuSettings(): Promise<GroupMenuSettings> {
  return handleResponse(await apiFetch('/api/v1/system/group-menus'));
}

export async function saveGroupMenus(workDiaryGroupId: number, menuIds: string[]): Promise<void> {
  await handleResponse(
    await apiFetch(`/api/v1/system/group-menus/${workDiaryGroupId}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ menuIds }),
    }),
  );
}
