import type { AppSelection } from '../layout/selection';
import type { BoardScreen } from '../pages/BoardPage';
import type { WorkDiaryScreen } from '../pages/WorkDiaryPage';
import {
  defaultChildId,
  type InventoryPageId,
  type MenuCategory,
  type OutsourcePageId,
  type ProductionPageId,
  type PurchasePageId,
  type QualityPageId,
  type SalesPageId,
  type SystemPage,
} from '../layout/menuConfig';
import type { BoardType } from '../api/board';

const BOARD_TYPES = new Set<string>([
  'NOTICE',
  'PRESIDENT_NOTICE',
  'PRODUCT',
  'LASER',
  'INSTITUTE',
  'SALES_QC',
]);

const SYSTEM_PAGES = new Set<string>([
  'publicCode',
  'masterImport',
  'role',
  'monthClosing',
  'systemSettings',
]);

const SALES_PAGES = new Set<string>(['sales-order', 'sales-shipment', 'sales-revenue', 'sales-collection']);
const PRODUCTION_PAGES = new Set<string>([
  'prod-plan',
  'prod-mrp',
  'prod-work-plan',
  'prod-schedule',
  'prod-work-order',
  'prod-material-issue',
  'prod-work-diary',
]);
const PURCHASE_PAGES = new Set<string>([
  'purchase-order',
  'purchase-receipt',
  'purchase-etc-order',
  'purchase-etc-receipt',
  'purchase-payable-approval',
  'purchase-payment',
]);
const INVENTORY_PAGES = new Set<string>(['inventory-misc-movement', 'inventory-ledger', 'inventory-lot']);
const OUTSOURCE_PAGES = new Set<string>(['outsource-order', 'outsource-shipment', 'outsource-receipt']);
const QUALITY_PAGES = new Set<string>(['quality-inspection']);

export const NAV_PATH_STORAGE_KEY = 'smartmanager.navPath';

export function pathFromSelection(selection: AppSelection): string {
  switch (selection.category) {
    case 'home':
      return '/';
    case 'basis':
      return '/basis';
    case 'system':
      return `/system/${selection.page}`;
    case 'sales':
      return `/sales/${selection.page}`;
    case 'production':
      return `/production/${selection.page}`;
    case 'purchase':
      return `/purchase/${selection.page}`;
    case 'inventory':
      return `/inventory/${selection.page}`;
    case 'quality':
      return `/quality/${selection.page}`;
    case 'outsource':
      return `/outsource/${selection.page}`;
    case 'board':
      return boardPath(selection.boardType, selection.screen);
    case 'workdiary':
      return workDiaryPath(selection.screen);
    default:
      return '/';
  }
}

function boardPath(boardType: BoardType, screen: BoardScreen): string {
  if (screen.mode === 'list') {
    return `/board/${boardType}`;
  }
  if (screen.mode === 'detail') {
    return `/board/${boardType}/posts/${screen.postId}`;
  }
  const params = new URLSearchParams();
  params.set('mode', screen.composeMode);
  if (screen.composeMode === 'edit' && screen.postId != null) {
    params.set('postId', String(screen.postId));
  }
  if (screen.composeMode === 'reply' && screen.parentPostId != null) {
    params.set('parentPostId', String(screen.parentPostId));
  }
  return `/board/${boardType}/compose?${params.toString()}`;
}

function workDiaryPath(screen: WorkDiaryScreen): string {
  if (screen.mode === 'list') {
    return '/workdiary';
  }
  if (screen.mode === 'detail') {
    return `/workdiary/${screen.id}`;
  }
  const params = new URLSearchParams();
  if (screen.editId != null) {
    params.set('editId', String(screen.editId));
  }
  if (screen.workDate) {
    params.set('workDate', screen.workDate);
  }
  const query = params.toString();
  return query ? `/workdiary/compose?${query}` : '/workdiary/compose';
}

export function selectionFromLocation(pathname: string, search: string): AppSelection | null {
  const path = pathname.replace(/\/+$/, '') || '/';
  const params = new URLSearchParams(search.startsWith('?') ? search.slice(1) : search);

  if (path === '/' || path === '') {
    return { category: 'home' };
  }
  if (path === '/basis') {
    return { category: 'basis' };
  }
  if (path === '/workdiary') {
    return { category: 'workdiary', screen: { mode: 'list' } };
  }
  if (path === '/workdiary/compose') {
    const editIdRaw = params.get('editId');
    const editId = editIdRaw != null && editIdRaw !== '' ? Number(editIdRaw) : undefined;
    const workDate = params.get('workDate') ?? undefined;
    if (editId != null && Number.isFinite(editId)) {
      return { category: 'workdiary', screen: { mode: 'compose', editId } };
    }
    return { category: 'workdiary', screen: { mode: 'compose', workDate } };
  }

  const workDiaryDetail = /^\/workdiary\/(\d+)$/.exec(path);
  if (workDiaryDetail) {
    return { category: 'workdiary', screen: { mode: 'detail', id: Number(workDiaryDetail[1]) } };
  }

  const boardCompose = /^\/board\/([^/]+)\/compose$/.exec(path);
  if (boardCompose && BOARD_TYPES.has(boardCompose[1])) {
    const boardType = boardCompose[1] as BoardType;
    const mode = params.get('mode');
    if (mode === 'edit') {
      const postId = Number(params.get('postId'));
      if (!Number.isFinite(postId)) return null;
      return {
        category: 'board',
        boardType,
        screen: { mode: 'compose', composeMode: 'edit', postId },
      };
    }
    if (mode === 'reply') {
      const parentPostId = Number(params.get('parentPostId'));
      if (!Number.isFinite(parentPostId)) return null;
      return {
        category: 'board',
        boardType,
        screen: { mode: 'compose', composeMode: 'reply', parentPostId },
      };
    }
    return {
      category: 'board',
      boardType,
      screen: { mode: 'compose', composeMode: 'create' },
    };
  }

  const boardDetail = /^\/board\/([^/]+)\/posts\/(\d+)$/.exec(path);
  if (boardDetail && BOARD_TYPES.has(boardDetail[1])) {
    return {
      category: 'board',
      boardType: boardDetail[1] as BoardType,
      screen: { mode: 'detail', postId: Number(boardDetail[2]) },
    };
  }

  const boardList = /^\/board\/([^/]+)$/.exec(path);
  if (boardList && BOARD_TYPES.has(boardList[1])) {
    return {
      category: 'board',
      boardType: boardList[1] as BoardType,
      screen: { mode: 'list' },
    };
  }

  const modulePage = /^\/(system|sales|production|purchase|inventory|quality|outsource)\/([^/]+)$/.exec(path);
  if (modulePage) {
    const [, category, page] = modulePage;
    if (category === 'system' && SYSTEM_PAGES.has(page)) {
      return { category: 'system', page: page as SystemPage };
    }
    if (category === 'sales' && SALES_PAGES.has(page)) {
      return { category: 'sales', page: page as SalesPageId };
    }
    if (category === 'production' && PRODUCTION_PAGES.has(page)) {
      return { category: 'production', page: page as ProductionPageId };
    }
    if (category === 'purchase' && PURCHASE_PAGES.has(page)) {
      return { category: 'purchase', page: page as PurchasePageId };
    }
    if (category === 'inventory' && INVENTORY_PAGES.has(page)) {
      return { category: 'inventory', page: page as InventoryPageId };
    }
    if (category === 'quality' && QUALITY_PAGES.has(page)) {
      return { category: 'quality', page: page as QualityPageId };
    }
    if (category === 'outsource' && OUTSOURCE_PAGES.has(page)) {
      return { category: 'outsource', page: page as OutsourcePageId };
    }
  }

  return null;
}

export function pathForCategory(category: MenuCategory, childId?: string): string {
  if (category === 'home') return '/';
  if (category === 'basis') return '/basis';
  const page = childId ?? defaultChildId(category);
  if (category === 'system') return `/system/${page}`;
  if (category === 'sales') return `/sales/${page}`;
  if (category === 'production') return `/production/${page}`;
  if (category === 'purchase') return `/purchase/${page}`;
  if (category === 'inventory') return `/inventory/${page}`;
  if (category === 'quality') return `/quality/${page}`;
  if (category === 'outsource') return `/outsource/${page}`;
  return '/';
}

export function expandedCategoryFromSelection(selection: AppSelection): MenuCategory | null {
  if (selection.category === 'board' || selection.category === 'workdiary') {
    return 'home';
  }
  return selection.category;
}

export function readSavedNavPath(): string | null {
  try {
    const raw = sessionStorage.getItem(NAV_PATH_STORAGE_KEY);
    return raw && raw.startsWith('/') ? raw : null;
  } catch {
    return null;
  }
}

export function writeSavedNavPath(path: string): void {
  try {
    sessionStorage.setItem(NAV_PATH_STORAGE_KEY, path);
  } catch {
    // ignore
  }
}

export function clearSavedNavPath(): void {
  try {
    sessionStorage.removeItem(NAV_PATH_STORAGE_KEY);
  } catch {
    // ignore
  }
}
