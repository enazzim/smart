import { describe, expect, it } from 'vitest';
import { formatAmount, formatInteger, formatQty } from './numberFormat';
import { breakdownFromSupply, breakdownFromTotal } from './vatAmount';
import { digitsOnlyBusinessRegNo, formatBusinessRegNo } from './businessRegNo';

describe('numberFormat', () => {
  it('수량·금액을 ko-KR로 포맷하고 null은 대시', () => {
    expect(formatQty(1234.5)).toBe((1234.5).toLocaleString('ko-KR', { maximumFractionDigits: 4 }));
    expect(formatAmount(1000)).toBe((1000).toLocaleString('ko-KR', { maximumFractionDigits: 2 }));
    expect(formatInteger(null)).toBe('—');
    expect(formatQty(Number.NaN)).toBe('—');
  });
});

describe('vatAmount', () => {
  it('공급가 기준 부가세를 절상한다', () => {
    expect(breakdownFromSupply(1000)).toEqual({ supply: 1000, vat: 100, total: 1100 });
    expect(breakdownFromSupply(1000.9)).toEqual({ supply: 1000, vat: 100, total: 1100 });
  });

  it('총액에서 공급가를 역산한다 (절사)', () => {
    // 1100/1.1 은 IEEE 부동소수로 999.999… → floor 후 공급가 999
    expect(breakdownFromTotal(1100)).toEqual({ supply: 999, vat: 100, total: 1099 });
    expect(breakdownFromTotal(2200).supply).toBe(Math.floor(2200 / 1.1));
  });
});

describe('businessRegNo', () => {
  it('숫자만 추출하고 표준 형식으로 표시한다', () => {
    expect(digitsOnlyBusinessRegNo('613-81-04369')).toBe('6138104369');
    expect(formatBusinessRegNo('6138104369')).toBe('613-81-04369');
  });
});
