export type InventoryLocationCode = 'RAW' | 'WIP' | 'SALES' | 'DELIVERY' | 'OUTSOURCE';

export const INVENTORY_LOCATION_CODES: InventoryLocationCode[] = [
  'RAW',
  'WIP',
  'SALES',
  'DELIVERY',
  'OUTSOURCE',
];

export const INVENTORY_LOCATION_LABEL: Record<InventoryLocationCode, string> = {
  RAW: '원자재창고',
  WIP: '공정창고',
  SALES: '영업창고',
  DELIVERY: '납품창고',
  OUTSOURCE: '외주창고',
};

export function isInventoryLocationCode(code: string): code is InventoryLocationCode {
  return INVENTORY_LOCATION_CODES.includes(code.toUpperCase() as InventoryLocationCode);
}

export function getInventoryLocationLabel(code: string | null | undefined): string {
  if (!code) {
    return '';
  }
  const normalized = code.trim().toUpperCase();
  if (isInventoryLocationCode(normalized)) {
    return INVENTORY_LOCATION_LABEL[normalized];
  }
  return code.trim();
}

export interface InventoryLocationDisplayOptions {
  outputProcessSequence?: number | null;
  outputProcessName?: string | null;
}

export function formatInventoryLocation(
  locationCode: string,
  options?: InventoryLocationDisplayOptions,
): string {
  const label = getInventoryLocationLabel(locationCode);
  if (locationCode.trim().toUpperCase() === 'WIP' && options?.outputProcessName) {
    const sequence =
      options.outputProcessSequence != null ? `${options.outputProcessSequence}. ` : '';
    return `${label} · ${sequence}${options.outputProcessName}`;
  }
  return label;
}

export const INVENTORY_LOCATION_FILTER_OPTIONS: Array<{ value: string; label: string }> = [
  { value: '', label: '전체' },
  ...INVENTORY_LOCATION_CODES.map((code) => ({
    value: code,
    label: INVENTORY_LOCATION_LABEL[code],
  })),
];

/** API 오류·안내 문구에 남아 있는 영문 창고 코드를 한글명으로 치환 */
export function translateInventoryLocationInText(text: string | null | undefined): string {
  if (!text) {
    return '';
  }
  let result = text;
  for (const code of INVENTORY_LOCATION_CODES) {
    const label = INVENTORY_LOCATION_LABEL[code];
    result = result.replaceAll(`${label}(${code})`, label);
    result = result.replace(new RegExp(`\\(${code}\\)`, 'g'), label);
    result = result.replace(new RegExp(`\\b${code}\\b`, 'g'), label);
  }
  return result;
}
