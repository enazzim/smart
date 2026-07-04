import type { UnitPrice } from '../api/unitPrice';

export type PartnerPriceItem = {
  itemId: number;
  itemNo: string;
  itemName: string;
  propertyClassification?: string;
  unitPrice: number;
};

export function resolveUnitPriceAmount(unitPrice: UnitPrice): number {
  return unitPrice.discountUnitCost ?? unitPrice.standardUnitCost;
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
      propertyClassification: undefined,
      unitPrice: resolveUnitPriceAmount(unitPrice),
    }))
    .sort((a, b) => a.itemNo.localeCompare(b.itemNo, 'ko'));
}
