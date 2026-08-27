/**
 * 사업자등록번호: 저장·조회용 정규화 / 화면·문서 표시용 포맷.
 * 표준 10자리 → XXX-XX-XXXXX, 그 외는 원문(또는 숫자만) 유지.
 */

export function digitsOnlyBusinessRegNo(value: string | null | undefined): string {
  if (value == null) return '';
  return String(value).replace(/\D/g, '');
}

export function isStandardBusinessRegNo(value: string | null | undefined): boolean {
  return digitsOnlyBusinessRegNo(value).length === 10;
}

/** 10자리면 XXX-XX-XXXXX, 아니면 입력 trim 그대로 */
export function canonicalizeBusinessRegNo(value: string | null | undefined): string {
  const raw = value == null ? '' : String(value).trim();
  const digits = digitsOnlyBusinessRegNo(raw);
  if (digits.length === 10) {
    return formatBusinessRegNo(digits);
  }
  return raw;
}

/** 표시용: 숫자 10자리면 XXX-XX-XXXXX, 아니면 원문 */
export function formatBusinessRegNo(value: string | null | undefined): string {
  if (value == null || value === '') return '';
  const digits = digitsOnlyBusinessRegNo(value);
  if (digits.length === 10) {
    return `${digits.slice(0, 3)}-${digits.slice(3, 5)}-${digits.slice(5)}`;
  }
  return String(value).trim();
}
