import type { UnitPrice } from '../api/unitPrice';

export type PartnerPriceItem = {
  itemId: number;
  itemNo: string;
  itemName: string;
  propertyClassification?: string;
  unitPrice: number;
};

export function resolveUnitPriceAmount(unitPrice: UnitPrice): number {
  // 할인단가 0은 엑셀 빈칸/미적용으로 들어온 경우가 많아 표준단가를 사용한다.
  if (unitPrice.discountUnitCost != null && unitPrice.discountUnitCost > 0) {
    return unitPrice.discountUnitCost;
  }
  return unitPrice.standardUnitCost;
}

export function isUnitPriceEffective(unitPrice: UnitPrice, refDate: string): boolean {
  if (unitPrice.beginDate > refDate) {
    return false;
  }
  if (unitPrice.endDate && unitPrice.endDate < refDate) {
    return false;
  }
  return true;
}

export function toPartnerPriceItems(unitPrices: UnitPrice[]): PartnerPriceItem[] {
  const latestByItem = new Map<number, UnitPrice>();
  for (const unitPrice of unitPrices) {
    const existing = latestByItem.get(unitPrice.itemId);
    if (!existing || unitPrice.beginDate > existing.beginDate) {
      latestByItem.set(unitPrice.itemId, unitPrice);
    }
  }

  return [...latestByItem.values()]
    .map((unitPrice) => ({
      itemId: unitPrice.itemId,
      itemNo: unitPrice.itemNum,
      itemName: unitPrice.itemName,
      propertyClassification: unitPrice.propertyClassification,
      unitPrice: resolveUnitPriceAmount(unitPrice),
    }))
    .sort((a, b) => a.itemNo.localeCompare(b.itemNo, 'ko'));
}
