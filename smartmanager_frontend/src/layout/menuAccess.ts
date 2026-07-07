import type { MenuCategory } from './menuConfig';

const TRANSACTION_CATEGORIES: MenuCategory[] = [
  'sales',
  'production',
  'purchase',
  'outsource',
  'quality',
];

export function getVisibleMenuCategories(roleCodes: string[]): MenuCategory[] {
  if (roleCodes.includes('SYSTEM_ADMIN')) {
    return [
      'home',
      'sales',
      'production',
      'purchase',
      'outsource',
      'quality',
      'basis',
      'system',
    ];
  }

  const visible = new Set<MenuCategory>(['home', 'basis']);

  if (roleCodes.includes('BASIS_MANAGER')) {
    return ['home', 'basis'];
  }

  if (roleCodes.includes('SALES_OPERATOR')) {
    visible.add('sales');
  }
  if (roleCodes.includes('PRODUCTION_OPERATOR')) {
    visible.add('production');
    visible.add('outsource');
    visible.add('quality');
  }
  if (roleCodes.includes('PURCHASE_OPERATOR')) {
    visible.add('purchase');
    visible.add('quality');
  }
  if (roleCodes.includes('VIEWER')) {
    TRANSACTION_CATEGORIES.forEach((category) => visible.add(category));
  }

  return Array.from(visible);
}

export function canManageBasisData(roleCodes: string[]): boolean {
  return roleCodes.includes('SYSTEM_ADMIN') || roleCodes.includes('BASIS_MANAGER');
}

export function canManageUsers(authorities: string[]): boolean {
  return authorities.includes('basis:user:write');
}

/** VIEWER 단독(또는 조회 전용) — 업무 메뉴는 읽기 전용 */
export function isTransactionReadOnly(roleCodes: string[]): boolean {
  if (
    roleCodes.includes('SYSTEM_ADMIN') ||
    roleCodes.includes('BASIS_MANAGER') ||
    roleCodes.includes('SALES_OPERATOR') ||
    roleCodes.includes('PRODUCTION_OPERATOR') ||
    roleCodes.includes('PURCHASE_OPERATOR')
  ) {
    return false;
  }
  return roleCodes.includes('VIEWER');
}

export function isCategoryVisible(category: MenuCategory | string, roleCodes: string[]): boolean {
  return getVisibleMenuCategories(roleCodes).includes(category as MenuCategory);
}

export function isSelectionVisible(selection: { category: string }, roleCodes: string[]): boolean {
  switch (selection.category) {
    case 'home':
    case 'board':
    case 'workdiary':
      return isCategoryVisible('home', roleCodes);
    case 'basis':
      return isCategoryVisible('basis', roleCodes);
    case 'system':
      return isCategoryVisible('system', roleCodes);
    default:
      return isCategoryVisible(selection.category, roleCodes);
  }
}
