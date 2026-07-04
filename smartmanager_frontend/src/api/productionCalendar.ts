import { apiFetch, handleResponse } from './http';

export interface ProductionCalendarDay {
  id: number;
  calendarDate: string;
  workTime: number;
  content?: string | null;
}

export interface ProductionCalendarEffectiveDay {
  calendarDate: string;
  effectiveWorkTime: number;
  registered: boolean;
  autoOffDay: boolean;
  registeredWorkTime?: number | null;
  content?: string | null;
}

export interface UpsertProductionCalendarRequest {
  workTime: number;
  content?: string;
}

const API_BASE = '/api/v1/basis/production-calendars';

export async function fetchProductionCalendars(year: number, month: number): Promise<ProductionCalendarDay[]> {
  return handleResponse<ProductionCalendarDay[]>(
    await apiFetch(`${API_BASE}?year=${year}&month=${month}`),
  );
}

export async function fetchProductionCalendarEffective(
  year: number,
  month: number,
): Promise<ProductionCalendarEffectiveDay[]> {
  return handleResponse<ProductionCalendarEffectiveDay[]>(
    await apiFetch(`/api/v1/basis/production-calendars/effective?year=${year}&month=${month}`),
  );
}

export async function upsertProductionCalendarByDate(
  calendarDate: string,
  payload: UpsertProductionCalendarRequest,
): Promise<ProductionCalendarDay> {
  return handleResponse<ProductionCalendarDay>(
    await apiFetch(`${API_BASE}/by-date/${calendarDate}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteProductionCalendarByDate(calendarDate: string): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API_BASE}/by-date/${calendarDate}`, {
      method: 'DELETE',
    }),
  );
}

export const DEFAULT_WORK_TIME = 480;

export function formatWorkTimeLabel(minutes: number, autoOffDay = false): string {
  if (minutes === 0) {
    return autoOffDay ? '휴무(자동)' : '휴무(0)';
  }
  return `${minutes}분`;
}
