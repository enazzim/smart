import { apiFetch, handleResponse } from './http';

export interface SystemSetting {
  settingKey: string;
  label: string;
  description: string;
  value: string;
  allowedValues: string[];
  updatedAt?: string | null;
  updatedBy?: string | null;
}

export async function fetchSystemSettings(): Promise<SystemSetting[]> {
  const res = await apiFetch('/api/v1/system/settings');
  return handleResponse(res);
}

export async function updateSystemSetting(settingKey: string, value: string): Promise<SystemSetting> {
  const res = await apiFetch(`/api/v1/system/settings/${encodeURIComponent(settingKey)}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ value }),
  });
  return handleResponse(res);
}

export const MRP_GROUPING_MODE_LABELS: Record<string, string> = {
  BY_PLAN: '생산계획별',
  BY_COMPONENT: '자재별 합산',
};

export const MATERIAL_ISSUE_ENABLED_LABELS: Record<string, string> = {
  YES: '예',
  NO: '아니오',
};

export const YES_NO_LABELS = MATERIAL_ISSUE_ENABLED_LABELS;

export const SETTING_KEY_MATERIAL_ISSUE_ENABLED = 'production.material_issue.enabled';
export const SETTING_KEY_INVENTORY_ALLOW_NEGATIVE_STOCK = 'inventory.allow_negative_stock';
