import { apiFetch, handleResponse } from './http';

export interface EffectiveCalendarDay {
  calendarDate: string;
  effectiveWorkTime: number;
  isOverride: boolean;
  baseWorkTime?: number | null;
  content?: string | null;
}

export interface UpsertWorkCenterCalendarOverrideRequest {
  workCenterId: number;
  calendarDate: string;
  workTime: number;
  content?: string;
}

const API_BASE = '/api/v1/basis/work-center-calendars';

export async function fetchEffectiveCalendar(
  workCenterId: number,
  year: number,
  month: number,
): Promise<EffectiveCalendarDay[]> {
  return handleResponse<EffectiveCalendarDay[]>(
    await apiFetch(
      `${API_BASE}/effective?workCenterId=${workCenterId}&year=${year}&month=${month}`,
    ),
  );
}

export async function upsertWorkCenterCalendarOverride(
  payload: UpsertWorkCenterCalendarOverrideRequest,
): Promise<EffectiveCalendarDay> {
  return handleResponse<EffectiveCalendarDay>(
    await apiFetch(`${API_BASE}/overrides`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteWorkCenterCalendarOverride(
  workCenterId: number,
  calendarDate: string,
): Promise<void> {
  await handleResponse<void>(
    await apiFetch(
      `${API_BASE}/overrides?workCenterId=${workCenterId}&calendarDate=${calendarDate}`,
      { method: 'DELETE' },
    ),
  );
}

export function formatWorkTimeLabel(minutes: number): string {
  if (minutes === 0) {
    return '휴무(0)';
  }
  return `${minutes}분`;
}
