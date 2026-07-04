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

export async function fetchEquipmentCategories(): Promise<CodeOption[]> {
  return handleResponse<CodeOption[]>(
    await fetch('/api/v1/basis/code-groups/EQUIPMENT_CLASS/options'),
  );
}

export async function fetchEquipment(query?: string): Promise<Equipment[]> {
  const url = query?.trim() ? `${API_BASE}?q=${encodeURIComponent(query.trim())}` : API_BASE;
  return handleResponse<Equipment[]>(await fetch(url));
}

export async function fetchEquipmentById(id: number): Promise<Equipment> {
  return handleResponse<Equipment>(await fetch(`${API_BASE}/${id}`));
}

export async function createEquipment(payload: CreateEquipmentRequest): Promise<Equipment> {
  return handleResponse<Equipment>(
    await fetch(API_BASE, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateEquipment(id: number, payload: UpdateEquipmentRequest): Promise<Equipment> {
  return handleResponse<Equipment>(
    await fetch(`${API_BASE}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteEquipment(id: number): Promise<void> {
  await handleResponse<void>(
    await fetch(`${API_BASE}/${id}`, {
      method: 'DELETE',
    }),
  );
}
