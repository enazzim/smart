export interface ProductionCalendarDay {
  id: number;
  calendarDate: string;
  workTime: number;
  content?: string | null;
}

export interface UpsertProductionCalendarRequest {
  workTime: number;
  content?: string;
}

const API_BASE = '/api/v1/basis/production-calendars';

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

export async function fetchProductionCalendars(year: number, month: number): Promise<ProductionCalendarDay[]> {
  return handleResponse<ProductionCalendarDay[]>(
    await fetch(`${API_BASE}?year=${year}&month=${month}`),
  );
}

export async function upsertProductionCalendarByDate(
  calendarDate: string,
  payload: UpsertProductionCalendarRequest,
): Promise<ProductionCalendarDay> {
  return handleResponse<ProductionCalendarDay>(
    await fetch(`${API_BASE}/by-date/${calendarDate}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteProductionCalendarByDate(calendarDate: string): Promise<void> {
  await handleResponse<void>(
    await fetch(`${API_BASE}/by-date/${calendarDate}`, {
      method: 'DELETE',
    }),
  );
}

export const DEFAULT_WORK_TIME = 480;

export function formatWorkTimeLabel(minutes: number): string {
  if (minutes === 0) {
    return '휴무(0)';
  }
  return `${minutes}분`;
}
