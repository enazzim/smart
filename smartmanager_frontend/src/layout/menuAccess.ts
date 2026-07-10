import type { MenuCategory } from './menuConfig';

const TRANSACTION_CATEGORIES: MenuCategory[] = [
  'sales',
  'production',
  'purchase',
  'outsource',
  'quality',
];

const SCOPED_OPERATOR_ROLES = [
  'SALES_OPERATOR',
  'PRODUCTION_OPERATOR',
  'PURCHASE_OPERATOR',
] as const;

function hasScopedOperatorRole(roleCodes: string[]): boolean {
  return SCOPED_OPERATOR_ROLES.some((role) => roleCodes.includes(role));
}

/** 역할별 사이드바 대메뉴 */
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

  const visible = new Set<MenuCategory>(['home']);

  if (roleCodes.includes('BASIS_MANAGER')) {
    visible.add('basis');
    return Array.from(visible);
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

  // VIEWER 단독: 업무·기준정보 메뉴 노출(읽기 전용)
  if (roleCodes.includes('VIEWER') && !hasScopedOperatorRole(roleCodes)) {
    TRANSACTION_CATEGORIES.forEach((category) => visible.add(category));
    visible.add('basis');
  }

  return Array.from(visible);
}

export function canManageBasisData(roleCodes: string[]): boolean {
  return roleCodes.includes('SYSTEM_ADMIN') || roleCodes.includes('BASIS_MANAGER');
}

export function canManageUsers(authorities: string[]): boolean {
  return authorities.includes('basis:user:write');
}

/** 운영자 등 — 사이드바 없이 내 계정(비밀번호 변경)만 필요할 때 */
export function canAccessSelfAccount(roleCodes: string[], authorities: string[]): boolean {
  if (canManageBasisData(roleCodes)) {
    return false;
  }
  return authorities.includes('basis:user:read');
}

/** VIEWER 포함 시 등록·수정·삭제 불가 (SYSTEM_ADMIN 제외) */
export function isTransactionReadOnly(roleCodes: string[]): boolean {
  if (roleCodes.includes('SYSTEM_ADMIN')) {
    return false;
  }
  return roleCodes.includes('VIEWER');
}

export function isCategoryVisible(category: MenuCategory | string, roleCodes: string[]): boolean {
  return getVisibleMenuCategories(roleCodes).includes(category as MenuCategory);
}

export function canOpenBasis(roleCodes: string[], authorities: string[]): boolean {
  if (isCategoryVisible('basis', roleCodes)) {
    return true;
  }
  return canAccessSelfAccount(roleCodes, authorities);
}

export function isDashboardSelection(category: string): boolean {
  return category === 'home' || category === 'board' || category === 'workdiary';
}

export function isSelectionVisible(
  selection: { category: string },
  roleCodes: string[],
  authorities: string[] = [],
): boolean {
  switch (selection.category) {
    case 'home':
    case 'board':
    case 'workdiary':
      return isCategoryVisible('home', roleCodes);
    case 'basis':
      return canOpenBasis(roleCodes, authorities);
    case 'system':
      return isCategoryVisible('system', roleCodes);
    default:
      return isCategoryVisible(selection.category, roleCodes);
  }
}
