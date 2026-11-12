import { describe, expect, it } from 'vitest';
import { getVisibleMenuCategories, isTransactionReadOnly } from './menuAccess';

describe('isTransactionReadOnly', () => {
  it('VIEWER 단독이면 조회 전용', () => {
    expect(isTransactionReadOnly(['VIEWER'])).toBe(true);
  });

  it('VIEWER + 구매 담당이면 쓰기 가능', () => {
    expect(isTransactionReadOnly(['VIEWER', 'PURCHASE_OPERATOR'])).toBe(false);
  });

  it('VIEWER + 영업/생산 담당이면 쓰기 가능', () => {
    expect(isTransactionReadOnly(['VIEWER', 'SALES_OPERATOR'])).toBe(false);
    expect(isTransactionReadOnly(['VIEWER', 'PRODUCTION_OPERATOR'])).toBe(false);
  });

  it('SYSTEM_ADMIN은 VIEWER와 함께여도 쓰기 가능', () => {
    expect(isTransactionReadOnly(['SYSTEM_ADMIN', 'VIEWER', 'PURCHASE_OPERATOR'])).toBe(false);
  });

  it('담당 역할만 있으면 쓰기 가능', () => {
    expect(isTransactionReadOnly(['PURCHASE_OPERATOR'])).toBe(false);
  });
});

describe('getVisibleMenuCategories', () => {
  it('구매 담당 + VIEWER면 구매·재고·품질 메뉴가 보인다', () => {
    const cats = getVisibleMenuCategories(['PURCHASE_OPERATOR', 'VIEWER']);
    expect(cats).toContain('purchase');
    expect(cats).toContain('inventory');
    expect(cats).toContain('quality');
  });
});
