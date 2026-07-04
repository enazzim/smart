import { apiFetch, handleResponse } from './http';

export interface Equipment {
  id: number;
  equipmentNum: string;
  equipmentName: string;
  equipmentCategoryId: number;
  equipmentCategoryName: string;
  categoryCode: string;
  workCenterId?: number | null;
  wcName?: string | null;
  designShot: number;
  initialShot: number;
  workShot: number;
  accumulatedShot: number;
  replacementDue: boolean;
  createdAt: string;
}

export interface CreateEquipmentRequest {
  equipmentNum: string;
  equipmentName: string;
  equipmentCategoryId: number;
  workCenterId?: number | null;
  designShot: number;
  initialShot: number;
}

export type UpdateEquipmentRequest = Omit<CreateEquipmentRequest, 'equipmentNum'>;

export interface CodeOption {
  id: number;
  code: string;
  name: string;
}

const API_BASE = '/api/v1/basis/equipment';

export async function fetchEquipmentCategories(): Promise<CodeOption[]> {
  return handleResponse<CodeOption[]>(
    await apiFetch('/api/v1/basis/code-groups/EQUIPMENT_CLASS/options'),
  );
}

export async function fetchEquipment(query?: string): Promise<Equipment[]> {
  const url = query?.trim() ? `${API_BASE}?q=${encodeURIComponent(query.trim())}` : API_BASE;
  return handleResponse<Equipment[]>(await apiFetch(url));
}

export async function fetchEquipmentById(id: number): Promise<Equipment> {
  return handleResponse<Equipment>(await apiFetch(`${API_BASE}/${id}`));
}

export async function createEquipment(payload: CreateEquipmentRequest): Promise<Equipment> {
  return handleResponse<Equipment>(
    await apiFetch(API_BASE, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateEquipment(id: number, payload: UpdateEquipmentRequest): Promise<Equipment> {
  return handleResponse<Equipment>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteEquipment(id: number): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'DELETE',
    }),
  );
}
