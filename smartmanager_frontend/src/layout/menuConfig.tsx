import { lazy, Suspense, type ComponentType } from 'react';
import type { AuthenticatedUser } from '../api/auth';
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
import SystemSettingsPage from '../pages/SystemSettingsPage';
import MasterImportPage from '../pages/MasterImportPage';
import SalesOrderPage from '../pages/SalesOrderPage';
import ProductionPlanPage from '../pages/ProductionPlanPage';
import MrpPage from '../pages/MrpPage';
import PurchaseOrderPage from '../pages/PurchaseOrderPage';
import PurchaseReceiptPage from '../pages/PurchaseReceiptPage';
import EtcPurchaseOrderPage from '../pages/EtcPurchaseOrderPage';
import EtcPurchaseReceiptPage from '../pages/EtcPurchaseReceiptPage';
import PartnerPaymentPage from '../pages/PartnerPaymentPage';
import PayableApprovalPage from '../pages/PayableApprovalPage';
import InventoryLedgerPage from '../pages/InventoryLedgerPage';
import LotMasterPage from '../pages/LotMasterPage';
import MiscStockMovementPage from '../pages/MiscStockMovementPage';
import WorkPlanPage from '../pages/WorkPlanPage';
import WorkCenterLoadPage from '../pages/WorkCenterLoadPage';
import WorkOrderPage from '../pages/WorkOrderPage';
import MaterialIssuePage from '../pages/MaterialIssuePage';
import WorkReportPage from '../pages/WorkReportPage';
import OutsourcingOrderPage from '../pages/OutsourcingOrderPage';
import OutsourcingShipmentPage from '../pages/OutsourcingShipmentPage';
import OutsourcingReceiptPage from '../pages/OutsourcingReceiptPage';
import QualityInspectionPage from '../pages/QualityInspectionPage';
import SalesShipmentPage from '../pages/SalesShipmentPage';
import SalesRevenuePage from '../pages/SalesRevenuePage';
import SalesCollectionPage from '../pages/SalesCollectionPage';
import VendorPurchaseTotalPage from '../pages/VendorPurchaseTotalPage';
import WarehouseIoPage from '../pages/WarehouseIoPage';
import ItemIoPage from '../pages/ItemIoPage';
import PlaceholderPage from '../pages/PlaceholderPage';

const DrawingPage = lazy(() => import('../pages/DrawingPage'));

/** TO-BE 업무 흐름 기준 대메뉴 */
export type MenuCategory =
  | 'home'
  | 'sales'
  | 'production'
  | 'purchase'
  | 'inventory'
  | 'outsource'
  | 'quality'
  | 'stats'
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
  | 'drawing'
  | 'user';

export type SystemPage = 'publicCode' | 'masterImport' | 'role' | 'monthClosing' | 'systemSettings';

export type SalesPageId = 'sales-order' | 'sales-shipment' | 'sales-revenue' | 'sales-collection';

export type ProductionPageId =
  | 'prod-plan'
  | 'prod-mrp'
  | 'prod-work-plan'
  | 'prod-schedule'
  | 'prod-work-order'
  | 'prod-material-issue'
  | 'prod-work-diary';

export type PurchasePageId =
  | 'purchase-order'
  | 'purchase-receipt'
  | 'purchase-etc-order'
  | 'purchase-etc-receipt'
  | 'purchase-payable-approval'
  | 'purchase-payment';

export type InventoryPageId = 'inventory-misc-movement' | 'inventory-ledger' | 'inventory-lot';

export type OutsourcePageId = 'outsource-order' | 'outsource-shipment' | 'outsource-receipt';

export type QualityPageId = 'quality-inspection';

export type StatsPageId =
  | 'stats-vendor-purchase'
  | 'stats-warehouse-io'
  | 'stats-item-io';

export type PlaceholderPageId =
  | 'sales-revenue';

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
    id: 'home',
    label: '대시보드',
    direct: true,
  },
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
      { id: 'prod-schedule', label: '작업장 부하' },
      { id: 'prod-work-order', label: '작업지시' },
      { id: 'prod-material-issue', label: '자재투입' },
      { id: 'prod-work-diary', label: '작업일보' },
    ],
  },
  {
    id: 'purchase',
    label: '구매',
    children: [
      { id: 'purchase-order', label: '구매발주' },
      { id: 'purchase-receipt', label: '구매입고' },
      { id: 'purchase-etc-order', label: '기타구매발주' },
      { id: 'purchase-etc-receipt', label: '기타구매입고' },
      { id: 'purchase-payable-approval', label: '승인처리' },
      { id: 'purchase-payment', label: '지급' },
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
    id: 'inventory',
    label: '재고',
    children: [
      { id: 'inventory-misc-movement', label: '기타 입출고' },
      { id: 'inventory-ledger', label: '재고·원장' },
      { id: 'inventory-lot', label: 'Lot 마스터' },
    ],
  },
  {
    id: 'quality',
    label: '품질',
    children: [{ id: 'quality-inspection', label: '품질검사' }],
  },
  {
    id: 'stats',
    label: '통계및 지표',
    children: [
      { id: 'stats-vendor-purchase', label: '매입처별 집계' },
      { id: 'stats-warehouse-io', label: '창고별 수불현황' },
      { id: 'stats-item-io', label: '품목별 수불현황' },
    ],
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
      { id: 'masterImport', label: '초기정보 일괄입력' },
      { id: 'role', label: '권한' },
      { id: 'systemSettings', label: '시스템 설정' },
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
  { id: 'drawing', label: '도면관리' },
  { id: 'user', label: '사용자' },
];

const BASIS_PAGE_MAP: Record<Exclude<BasisTab, 'user' | 'drawing'>, ComponentType> = {
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

const PLACEHOLDER_LABELS: Record<PlaceholderPageId | 'sales-order', string> = {
  'sales-order': '수주',
  'sales-revenue': '매출',
};

const SYSTEM_PLACEHOLDER_LABELS: Record<Exclude<SystemPage, 'publicCode' | 'masterImport' | 'monthClosing' | 'systemSettings'>, string> = {
  role: '권한',
};

export interface BasisPageContext {
  currentUser: AuthenticatedUser | null;
  canManageUsers: boolean;
  canManageDrawings: boolean;
  canHardDeleteDrawings: boolean;
  canReadDrawings: boolean;
}

export function renderBasisPage(tab: BasisTab, ctx?: BasisPageContext) {
  if (tab === 'drawing') {
    return (
      <Suspense fallback={<p>도면관리 화면을 불러오는 중…</p>}>
        <DrawingPage
          readOnly={!(ctx?.canManageDrawings ?? false)}
          canHardDelete={ctx?.canHardDeleteDrawings ?? false}
          actorUserId={ctx?.currentUser?.loginId}
        />
      </Suspense>
    );
  }
  if (tab === 'user') {
    return (
      <UserPage
        currentUser={ctx?.currentUser ?? null}
        canManageUsers={ctx?.canManageUsers ?? false}
      />
    );
  }
  const Page = BASIS_PAGE_MAP[tab];
  return <Page />;
}

export function renderSystemPage(page: SystemPage) {
  if (page === 'publicCode') {
    return <PublicCodePage />;
  }
  if (page === 'masterImport') {
    return <MasterImportPage />;
  }
  if (page === 'monthClosing') {
    return <MonthClosingPage />;
  }
  if (page === 'systemSettings') {
    return <SystemSettingsPage />;
  }
  return <PlaceholderPage title={SYSTEM_PLACEHOLDER_LABELS[page]} />;
}

export function renderProductionPage(page: ProductionPageId) {
  if (page === 'prod-plan') {
    return <ProductionPlanPage />;
  }
  if (page === 'prod-mrp') {
    return <MrpPage />;
  }
  if (page === 'prod-work-plan') {
    return <WorkPlanPage />;
  }
  if (page === 'prod-schedule') {
    return <WorkCenterLoadPage />;
  }
  if (page === 'prod-work-order') {
    return <WorkOrderPage />;
  }
  if (page === 'prod-material-issue') {
    return <MaterialIssuePage />;
  }
  if (page === 'prod-work-diary') {
    return <WorkReportPage />;
  }
  return <PlaceholderPage title={PLACEHOLDER_LABELS[page]} />;
}

export function renderSalesPage(page: SalesPageId) {
  if (page === 'sales-order') {
    return <SalesOrderPage />;
  }
  if (page === 'sales-shipment') {
    return <SalesShipmentPage />;
  }
  if (page === 'sales-revenue') {
    return <SalesRevenuePage />;
  }
  if (page === 'sales-collection') {
    return <SalesCollectionPage />;
  }
  return <PlaceholderPage title={PLACEHOLDER_LABELS[page]} />;
}

export function renderPurchasePage(page: PurchasePageId) {
  if (page === 'purchase-order') {
    return <PurchaseOrderPage />;
  }
  if (page === 'purchase-etc-order') {
    return <EtcPurchaseOrderPage />;
  }
  if (page === 'purchase-etc-receipt') {
    return <EtcPurchaseReceiptPage />;
  }
  if (page === 'purchase-payable-approval') {
    return <PayableApprovalPage />;
  }
  if (page === 'purchase-payment') {
    return <PartnerPaymentPage />;
  }
  return <PurchaseReceiptPage />;
}

export function renderInventoryPage(page: InventoryPageId) {
  if (page === 'inventory-misc-movement') {
    return <MiscStockMovementPage />;
  }
  if (page === 'inventory-lot') {
    return <LotMasterPage />;
  }
  return <InventoryLedgerPage />;
}

export function renderOutsourcePage(page: OutsourcePageId) {
  if (page === 'outsource-order') {
    return <OutsourcingOrderPage />;
  }
  if (page === 'outsource-shipment') {
    return <OutsourcingShipmentPage />;
  }
  return <OutsourcingReceiptPage />;
}

export function renderQualityPage(_page: QualityPageId) {
  return <QualityInspectionPage />;
}

export function renderStatsPage(page: StatsPageId) {
  if (page === 'stats-vendor-purchase') {
    return <VendorPurchaseTotalPage />;
  }
  if (page === 'stats-warehouse-io') {
    return <WarehouseIoPage />;
  }
  return <ItemIoPage />;
}

export function renderPlaceholderPage(id: PlaceholderPageId) {
  return <PlaceholderPage title={PLACEHOLDER_LABELS[id]} />;
}

/** 카테고리별 기본 하위 메뉴 ID */
export function defaultChildId(category: MenuCategory): string {
  switch (category) {
    case 'home':
      return '';
    case 'sales':
      return 'sales-order';
    case 'production':
      return 'prod-plan';
    case 'purchase':
      return 'purchase-order';
    case 'inventory':
      return 'inventory-misc-movement';
    case 'outsource':
      return 'outsource-order';
    case 'quality':
      return 'quality-inspection';
    case 'stats':
      return 'stats-vendor-purchase';
    case 'system':
      return 'publicCode';
    default:
      return '';
  }
}
