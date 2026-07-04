import type { ComponentType } from 'react';
import CompanyPage from '../pages/CompanyPage';
import ItemPage from '../pages/ItemPage';
import ItemCompositionPage from '../pages/ItemCompositionPage';
import ProcessPage from '../pages/ProcessPage';
import UnitPricePage from '../pages/UnitPricePage';
import WorkCenterPage from '../pages/WorkCenterPage';
import WorkStandardPage from '../pages/WorkStandardPage';
import EquipmentPage from '../pages/EquipmentPage';
import ProductionCalendarPage from '../pages/ProductionCalendarPage';
import WorkCenterCalendarPage from '../pages/WorkCenterCalendarPage';
import UserPage from '../pages/UserPage';
import PublicCodePage from '../pages/PublicCodePage';
import MonthClosingPage from '../pages/MonthClosingPage';
import SalesOrderPage from '../pages/SalesOrderPage';
import ProductionPlanPage from '../pages/ProductionPlanPage';
import PlaceholderPage from '../pages/PlaceholderPage';

/** TO-BE 업무 흐름 기준 7개 대메뉴 */
export type MenuCategory =
  | 'sales'
  | 'production'
  | 'purchase'
  | 'outsource'
  | 'quality'
  | 'basis'
  | 'system';

export type BasisTab =
  | 'company'
  | 'item'
  | 'bom'
  | 'process'
  | 'unitPrice'
  | 'workCenter'
  | 'workStandard'
  | 'equipment'
  | 'productionCalendar'
  | 'workCenterCalendar'
  | 'user';

export type SystemPage = 'publicCode' | 'role' | 'monthClosing';

export type SalesPageId = 'sales-order' | 'sales-shipment' | 'sales-revenue' | 'sales-collection';

export type ProductionPageId = 'prod-plan' | 'prod-mrp' | 'prod-work-plan' | 'prod-work-order' | 'prod-work-diary';

export type PlaceholderPageId =
  | 'sales-shipment'
  | 'sales-revenue'
  | 'sales-collection'
  | 'prod-mrp'
  | 'prod-work-plan'
  | 'prod-work-order'
  | 'prod-work-diary'
  | 'purchase-order'
  | 'purchase-delivery'
  | 'purchase-receipt'
  | 'outsource-order'
  | 'outsource-shipment'
  | 'outsource-receipt'
  | 'quality-inspection';

export interface MenuChild {
  id: string;
  label: string;
}

export interface MenuGroup {
  id: MenuCategory;
  label: string;
  children?: MenuChild[];
  direct?: boolean;
}

export const MENU_GROUPS: MenuGroup[] = [
  {
    id: 'sales',
    label: '영업',
    children: [
      { id: 'sales-order', label: '수주' },
      { id: 'sales-shipment', label: '출고·납품' },
      { id: 'sales-revenue', label: '매출' },
      { id: 'sales-collection', label: '수금' },
    ],
  },
  {
    id: 'production',
    label: '생산',
    children: [
      { id: 'prod-plan', label: '생산계획' },
      { id: 'prod-mrp', label: '자재소요' },
      { id: 'prod-work-plan', label: '작업계획' },
      { id: 'prod-work-order', label: '작업지시' },
      { id: 'prod-work-diary', label: '작업일보' },
    ],
  },
  {
    id: 'purchase',
    label: '구매',
    children: [
      { id: 'purchase-order', label: '구매발주' },
      { id: 'purchase-delivery', label: '납품' },
      { id: 'purchase-receipt', label: '입고' },
    ],
  },
  {
    id: 'outsource',
    label: '외주',
    children: [
      { id: 'outsource-order', label: '외주발주' },
      { id: 'outsource-shipment', label: '출고' },
      { id: 'outsource-receipt', label: '입고' },
    ],
  },
  {
    id: 'quality',
    label: '품질',
    children: [{ id: 'quality-inspection', label: '품질검사' }],
  },
  {
    id: 'basis',
    label: '기준정보',
    direct: true,
  },
  {
    id: 'system',
    label: '시스템정보',
    children: [
      { id: 'publicCode', label: '공용코드' },
      { id: 'role', label: '권한' },
      { id: 'monthClosing', label: '월마감' },
    ],
  },
];

export const BASIS_TABS: { id: BasisTab; label: string }[] = [
  { id: 'company', label: '거래처' },
  { id: 'item', label: '품목' },
  { id: 'bom', label: '품목구성' },
  { id: 'process', label: '공정' },
  { id: 'unitPrice', label: '단가' },
  { id: 'workCenter', label: '작업장' },
  { id: 'workStandard', label: '작업표준' },
  { id: 'equipment', label: '설비' },
  { id: 'productionCalendar', label: '기본달력' },
  { id: 'workCenterCalendar', label: 'WC달력' },
  { id: 'user', label: '사용자' },
];

const BASIS_PAGE_MAP: Record<BasisTab, ComponentType> = {
  company: CompanyPage,
  item: ItemPage,
  bom: ItemCompositionPage,
  process: ProcessPage,
  unitPrice: UnitPricePage,
  workCenter: WorkCenterPage,
  workStandard: WorkStandardPage,
  equipment: EquipmentPage,
  productionCalendar: ProductionCalendarPage,
  workCenterCalendar: WorkCenterCalendarPage,
  user: UserPage,
};

const PLACEHOLDER_LABELS: Record<PlaceholderPageId | 'sales-order', string> = {
  'sales-order': '수주',
  'sales-shipment': '출고·납품',
  'sales-revenue': '매출',
  'sales-collection': '수금',
  'prod-mrp': '자재소요',
  'prod-work-plan': '작업계획',
  'prod-work-order': '작업지시',
  'prod-work-diary': '작업일보',
  'purchase-order': '구매발주',
  'purchase-delivery': '납품',
  'purchase-receipt': '입고',
  'outsource-order': '외주발주',
  'outsource-shipment': '출고',
  'outsource-receipt': '입고',
  'quality-inspection': '품질검사',
};

const SYSTEM_PLACEHOLDER_LABELS: Record<Exclude<SystemPage, 'publicCode'>, string> = {
  role: '권한',
  monthClosing: '월마감',
};

export function renderBasisPage(tab: BasisTab) {
  const Page = BASIS_PAGE_MAP[tab];
  return <Page />;
}

export function renderSystemPage(page: SystemPage) {
  if (page === 'publicCode') {
    return <PublicCodePage />;
  }
  if (page === 'monthClosing') {
    return <MonthClosingPage />;
  }
  return <PlaceholderPage title={SYSTEM_PLACEHOLDER_LABELS[page]} />;
}

export function renderProductionPage(page: ProductionPageId) {
  if (page === 'prod-plan') {
    return <ProductionPlanPage />;
  }
  return <PlaceholderPage title={PLACEHOLDER_LABELS[page]} />;
}

export function renderSalesPage(page: SalesPageId) {
  if (page === 'sales-order') {
    return <SalesOrderPage />;
  }
  return <PlaceholderPage title={PLACEHOLDER_LABELS[page]} />;
}

export function renderPlaceholderPage(id: PlaceholderPageId) {
  return <PlaceholderPage title={PLACEHOLDER_LABELS[id]} />;
}

/** 카테고리별 기본 하위 메뉴 ID */
export function defaultChildId(category: MenuCategory): string {
  switch (category) {
    case 'sales':
      return 'sales-order';
    case 'production':
      return 'prod-plan';
    case 'purchase':
      return 'purchase-order';
    case 'outsource':
      return 'outsource-order';
    case 'quality':
      return 'quality-inspection';
    case 'system':
      return 'publicCode';
    default:
      return '';
  }
}
