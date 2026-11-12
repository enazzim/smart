import { describe, expect, it } from 'vitest';
import { excelSerialToIsoDate, normalizeExcelDate } from './excelHelpers';

describe('normalizeExcelDate', () => {
  it('YYYYMMDD 숫자를 날짜로 파싱한다 (엑셀 serial과 구분)', () => {
    expect(normalizeExcelDate(20070701)).toBe('2007-07-01');
  });

  it('엑셀 serial 숫자를 ISO 날짜로 변환한다', () => {
    // 2007-07-01 ≈ Excel serial 39264
    expect(excelSerialToIsoDate(39264)).toBe('2007-07-01');
    expect(normalizeExcelDate(39264)).toBe('2007-07-01');
  });

  it('YYYYMMDD 문자열과 ISO 문자열을 정규화한다', () => {
    expect(normalizeExcelDate('20070731')).toBe('2007-07-31');
    expect(normalizeExcelDate('2007-07-31')).toBe('2007-07-31');
    expect(normalizeExcelDate('2007/07/31')).toBe('2007-07-31');
  });

  it('빈 값은 빈 문자열', () => {
    expect(normalizeExcelDate(null)).toBe('');
    expect(normalizeExcelDate('')).toBe('');
  });
});
