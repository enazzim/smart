/**
 * 공급가액용: 소수점(원 미만)만 절사합니다.
 */
export function truncateDecimals(value: number): number {
  if (!Number.isFinite(value) || value <= 0) {
    return 0;
  }
  return Math.floor(value);
}

/**
 * 부가세용: 소수점이 있으면 절상(올림)합니다.
 */
export function ceilIfDecimal(value: number): number {
  if (!Number.isFinite(value) || value <= 0) {
    return 0;
  }
  return Math.ceil(value);
}

export type VatBreakdown = {
  supply: number;
  vat: number;
  total: number;
};

/**
 * 공급가 기준:
 * - 공급가: 소수점 절사
 * - 부가세: 공급가 × 10% (소수점이 있으면 절상)
 * - 총액: 공급가 + 부가세
 */
export function breakdownFromSupply(supply: number): VatBreakdown {
  const normalizedSupply = truncateDecimals(supply);
  const vat = ceilIfDecimal(normalizedSupply * 0.1);
  return {
    supply: normalizedSupply,
    vat,
    total: normalizedSupply + vat,
  };
}

/**
 * 총액 입력 시에도 공급가(/1.1)를 구한 뒤, 부가세·총액은 공급가 기준으로만 산출
 */
export function breakdownFromTotal(total: number): VatBreakdown {
  const supply = truncateDecimals(total / 1.1);
  return breakdownFromSupply(supply);
}

export function formatMoneyInput(value: number): string {
  if (!Number.isFinite(value)) {
    return '';
  }
  return String(value);
}
