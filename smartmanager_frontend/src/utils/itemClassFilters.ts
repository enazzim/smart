import type { PropertyClassification } from '../api/item';

/** 화면·검색 콤보용 자산분류 상수 */

export const ALL_ITEM_CLASSES: PropertyClassification[] = [
  '원자재',
  '제품',
  '상품',
  '공정품',
  '부자재',
  '팬텀',
];

/** 원자재 제외 (공정·작업표준·품목구성 모품목 등) */
export const NON_RAW_ITEM_CLASSES: PropertyClassification[] = ALL_ITEM_CLASSES.filter(
  (c) => c !== '원자재',
);

/** 품목구성 자품목: 제품·팬텀 제외 */
export const BOM_CHILD_ITEM_CLASSES: PropertyClassification[] = ALL_ITEM_CLASSES.filter(
  (c) => c !== '제품' && c !== '팬텀',
);
