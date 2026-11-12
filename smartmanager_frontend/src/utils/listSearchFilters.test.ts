import { describe, expect, it } from 'vitest';
import { commitListSearchFilters } from './listSearchFilters';

describe('commitListSearchFilters', () => {
  it('검색 확정 후 draft 변경이 applied에 영향을 주지 않는다', () => {
    const draft = { partnerName: 'A', itemNo: 'X', orderDateFrom: '2026-01-01' };
    const applied = commitListSearchFilters(draft);

    draft.partnerName = 'B';
    draft.itemNo = 'Y';

    expect(applied).toEqual({
      partnerName: 'A',
      itemNo: 'X',
      orderDateFrom: '2026-01-01',
    });
  });
});
