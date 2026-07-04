import type { AuthenticatedUser } from '../api/auth';
import {
  MENU_GROUPS,
  type MenuCategory,
  type PlaceholderPageId,
  type ProductionPageId,
  type SalesPageId,
  type SystemPage,
  renderProductionPage,
  renderPlaceholderPage,
  renderSalesPage,
  renderSystemPage,
} from './menuConfig';
import BasisInfoPage from '../pages/BasisInfoPage';

export type AppSelection =
  | { category: 'basis' }
  | { category: 'system'; page: SystemPage }
  | { category: 'sales'; page: SalesPageId }
  | { category: 'production'; page: ProductionPageId }
  | {
      category: 'purchase' | 'outsource' | 'quality';
      page: PlaceholderPageId;
    };

interface AppShellProps {
  currentUser: AuthenticatedUser | null;
  selection: AppSelection;
  expandedCategory: MenuCategory | null;
  onSelectCategory: (category: MenuCategory) => void;
  onSelectChild: (category: MenuCategory, childId: string) => void;
  onLogout: () => void;
}

export default function AppShell({
  currentUser,
  selection,
  expandedCategory,
  onSelectCategory,
  onSelectChild,
  onLogout,
}: AppShellProps) {
  return (
    <div className="app-shell">
      <header className="app-header">
        <div className="app-brand">
          <strong>SmartManager</strong>
          <span>ERP</span>
        </div>
        <div className="app-header-user">
          <span>{currentUser ? `${currentUser.name} (${currentUser.loginId})` : '…'}</span>
          <button type="button" className="secondary" onClick={onLogout}>
            로그아웃
          </button>
        </div>
      </header>

      <div className="app-body">
        <aside className="app-sidebar" aria-label="메인 메뉴">
          {MENU_GROUPS.map((group) => {
            const expanded = expandedCategory === group.id;
            const isDirect = group.direct === true;
            const isActive =
              selection.category === group.id ||
              (group.id === 'basis' && selection.category === 'basis');

            return (
              <div key={group.id} className="menu-group">
                <button
                  type="button"
                  className={`menu-group-title${isActive ? ' menu-active' : ''}`}
                  onClick={() => onSelectCategory(group.id)}
                >
                  {group.label}
                </button>
                {!isDirect && expanded && group.children && (
                  <ul className="menu-children">
                    {group.children.map((child) => {
                      const childActive =
                        selection.category === group.id &&
                        'page' in selection &&
                        selection.page === child.id;
                      return (
                        <li key={child.id}>
                          <button
                            type="button"
                            className={childActive ? 'menu-child-active' : undefined}
                            onClick={() => onSelectChild(group.id, child.id)}
                          >
                            {child.label}
                          </button>
                        </li>
                      );
                    })}
                  </ul>
                )}
              </div>
            );
          })}
        </aside>

        <main className="app-main">{renderContent(selection)}</main>
      </div>
    </div>
  );
}

function renderContent(selection: AppSelection) {
  if (selection.category === 'basis') {
    return <BasisInfoPage />;
  }
  if (selection.category === 'system') {
    return renderSystemPage(selection.page);
  }
  if (selection.category === 'sales') {
    return renderSalesPage(selection.page);
  }
  if (selection.category === 'production') {
    return renderProductionPage(selection.page);
  }
  return renderPlaceholderPage(selection.page);
}
