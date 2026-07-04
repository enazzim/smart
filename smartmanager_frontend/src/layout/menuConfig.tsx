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
import PlaceholderPage from '../pages/PlaceholderPage';

export type MenuCategory = 'sales' | 'production' | 'purchase' | 'basis' | 'system';

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
  | 'workCenterCalendar';

export type SystemPage = 'user' | 'publicCode';

export type PlaceholderPageId =
  | 'sales-order'
  | 'sales-shipment'
  | 'prod-plan'
  | 'prod-order'
  | 'purchase-order'
  | 'purchase-receipt';

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
      { id: 'sales-order', label: '수주관리' },
      { id: 'sales-shipment', label: '출하관리' },
    ],
  },
  {
    id: 'production',
    label: '생산',
    children: [
      { id: 'prod-plan', label: '생산계획' },
      { id: 'prod-order', label: '작업지시' },
    ],
  },
  {
    id: 'purchase',
    label: '구매',
    children: [
      { id: 'purchase-order', label: '구매발주' },
      { id: 'purchase-receipt', label: '입고관리' },
    ],
  },
  {
    id: 'basis',
    label: '기준정보',
    direct: true,
  },
  {
    id: 'system',
    label: '시스템설정',
    children: [
      { id: 'user', label: '사용자' },
      { id: 'publicCode', label: '공용코드' },
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
};

const PLACEHOLDER_LABELS: Record<PlaceholderPageId, string> = {
  'sales-order': '수주관리',
  'sales-shipment': '출하관리',
  'prod-plan': '생산계획',
  'prod-order': '작업지시',
  'purchase-order': '구매발주',
  'purchase-receipt': '입고관리',
};

export function renderBasisPage(tab: BasisTab) {
  const Page = BASIS_PAGE_MAP[tab];
  return <Page />;
}

export function renderSystemPage(page: SystemPage) {
  if (page === 'user') {
    return <UserPage />;
  }
  return <PublicCodePage />;
}

export function renderPlaceholderPage(id: PlaceholderPageId) {
  return <PlaceholderPage title={PLACEHOLDER_LABELS[id]} />;
}
