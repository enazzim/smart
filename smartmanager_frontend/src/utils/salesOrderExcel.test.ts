import { describe, expect, it } from 'vitest';
import {
  buildBulkOrdersFromRows,
  buildUnitPriceLookup,
  indexCompaniesByRegNo,
  indexItemsByNo,
  mapServerFailuresToErrorRows,
  type ParsedSalesOrderRow,
} from './salesOrderExcel';

function row(partial: Partial<ParsedSalesOrderRow> & Pick<ParsedSalesOrderRow, 'rowNumber'>): ParsedSalesOrderRow {
  return {
    itemNo: '',
    partnerName: '',
    businessRegNo: '',
    orderDate: '',
    orderQty: 0,
    requestedDeliveryDate: '',
    orderNo: '',
    deliveryPlace: '',
    productionRequestYn: '',
    ...partial,
  };
}

describe('buildBulkOrdersFromRows', () => {
  const companiesByRegNo = indexCompaniesByRegNo([
    {
      id: 1,
      companyName: '대동',
      businessRegNo: '613-81-04369',
      roles: ['SALES'],
    },
  ]);
  const itemsByNo = indexItemsByNo([
    { id: 10, itemNo: 'M3235-00001' },
    { id: 20, itemNo: 'M3235-00002' },
  ]);
  const unitPriceByPartnerItem = buildUnitPriceLookup([
    { companyId: 1, itemId: 10, standardUnitCost: 1000 },
  ]);

  it('거래처·품목 오류를 한 행에 함께 수집한다', () => {
    const result = buildBulkOrdersFromRows(
      [
        row({
          rowNumber: 2,
          businessRegNo: '000-00-00000',
          itemNo: 'UNKNOWN',
          orderDate: '2007-07-01',
          requestedDeliveryDate: '2007-07-31',
          orderQty: 10,
          orderNo: 'TS1',
        }),
      ],
      { companiesByRegNo, itemsByNo, unitPriceByPartnerItem },
    );

    expect(result.orders).toHaveLength(0);
    expect(result.errors).toHaveLength(1);
    expect(result.errors[0].errorReason).toContain('거래처');
    expect(result.errors[0].errorReason).toContain('품목');
  });

  it('유효 행은 발주번호 기준 그룹으로 주문을 만든다', () => {
    const result = buildBulkOrdersFromRows(
      [
        row({
          rowNumber: 2,
          businessRegNo: '6138104369',
          itemNo: 'M3235-00001',
          orderDate: '2007-07-01',
          requestedDeliveryDate: '2007-07-31',
          orderQty: 100,
          orderNo: 'TS1',
          unitPrice: 2000,
        }),
        row({
          rowNumber: 3,
          businessRegNo: '6138104369',
          itemNo: 'M3235-00002',
          orderDate: '2007-07-01',
          requestedDeliveryDate: '2007-07-31',
          orderQty: 50,
          orderNo: 'TS1',
          unitPrice: 500,
        }),
      ],
      {
        companiesByRegNo,
        itemsByNo,
        unitPriceByPartnerItem: buildUnitPriceLookup([
          { companyId: 1, itemId: 10, standardUnitCost: 1000 },
          { companyId: 1, itemId: 20, standardUnitCost: 500 },
        ]),
      },
    );

    expect(result.errors).toHaveLength(0);
    expect(result.orders).toHaveLength(1);
    expect(result.orders[0].lines).toHaveLength(2);
    expect(result.orderSourceRows[0]).toHaveLength(2);
  });

  it('서버 실패를 원본 엑셀 행 오류로 매핑한다', () => {
    const sources: ParsedSalesOrderRow[][] = [
      [row({ rowNumber: 2, itemNo: 'A', orderNo: 'TS1' })],
    ];
    const mapped = mapServerFailuresToErrorRows(
      [{ rowIndex: 0, message: '마감된 기간입니다.' }],
      sources,
    );
    expect(mapped).toHaveLength(1);
    expect(mapped[0].errorReason).toBe('마감된 기간입니다.');
    expect(mapped[0].itemNo).toBe('A');
  });
});
