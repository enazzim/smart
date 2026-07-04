import { apiFetch, handleResponse } from './http';

export interface MonthClosing {
  id: number;
  fiscalYear: number;
  fiscalMonth: number;
  closedAt: string;
  closedBy: string;
  closedById: string | null;
}

export interface FiscalPeriodStatus {
  referenceDate: string;
  fiscalYear: number;
  fiscalMonth: number;
  closed: boolean;
}

export interface CloseMonthClosingRequest {
  fiscalYear: number;
  fiscalMonth: number;
}

export async function fetchMonthClosings(): Promise<MonthClosing[]> {
  return handleResponse<MonthClosing[]>(await apiFetch('/api/v1/system/month-closings'));
}

export async function fetchFiscalPeriodStatus(date?: string): Promise<FiscalPeriodStatus> {
  const query = date ? `?date=${encodeURIComponent(date)}` : '';
  return handleResponse<FiscalPeriodStatus>(
    await apiFetch(`/api/v1/system/month-closings/period${query}`)
  );
}

export async function closeMonthClosing(body: CloseMonthClosingRequest): Promise<MonthClosing> {
  return handleResponse<MonthClosing>(
    await apiFetch('/api/v1/system/month-closings', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body),
    })
  );
}

export async function reopenMonthClosing(fiscalYear: number, fiscalMonth: number): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`/api/v1/system/month-closings/${fiscalYear}/${fiscalMonth}`, {
      method: 'DELETE',
    })
  );
}
