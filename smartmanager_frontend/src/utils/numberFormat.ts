const QTY_FORMAT: Intl.NumberFormatOptions = {
  minimumFractionDigits: 0,
  maximumFractionDigits: 4,
};

const AMOUNT_FORMAT: Intl.NumberFormatOptions = {
  minimumFractionDigits: 0,
  maximumFractionDigits: 2,
};

export function formatQty(value: number | null | undefined): string {
  if (value == null || !Number.isFinite(value)) {
    return '—';
  }
  return value.toLocaleString('ko-KR', QTY_FORMAT);
}

export function formatAmount(value: number | null | undefined): string {
  if (value == null || !Number.isFinite(value)) {
    return '—';
  }
  return value.toLocaleString('ko-KR', AMOUNT_FORMAT);
}

export function formatInteger(value: number | null | undefined): string {
  if (value == null || !Number.isFinite(value)) {
    return '—';
  }
  return value.toLocaleString('ko-KR', { maximumFractionDigits: 0 });
}
