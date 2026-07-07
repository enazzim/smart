import type { AuthenticatedUser } from '../api/auth';
import type { BoardType } from '../api/board';
import {
  MENU_GROUPS,
  type MenuCategory,
  type OutsourcePageId,
  type ProductionPageId,
  type PurchasePageId,
  type QualityPageId,
  type SalesPageId,
  type SystemPage,
  renderProductionPage,
  renderOutsourcePage,
  renderPurchasePage,
  renderQualityPage,
  renderSalesPage,
  renderSystemPage,
} from './menuConfig';
import { getVisibleMenuCategories } from './menuAccess';
import BasisInfoPage from '../pages/BasisInfoPage';
import BoardPage, { type BoardScreen } from '../pages/BoardPage';
import DashboardPage from '../pages/DashboardPage';
import WorkDiaryPage, { type WorkDiaryScreen } from '../pages/WorkDiaryPage';

export type AppSelection =
  | { category: 'home' }
  | { category: 'board'; boardType: BoardType; screen: BoardScreen }
  | { category: 'workdiary'; screen: WorkDiaryScreen }
  | { category: 'basis' }
  | { category: 'system'; page: SystemPage }
  | { category: 'sales'; page: SalesPageId }
  | { category: 'production'; page: ProductionPageId }
  | { category: 'purchase'; page: PurchasePageId }
  | { category: 'quality'; page: QualityPageId }
  | { category: 'outsource'; page: OutsourcePageId };

interface AppShellProps {
  currentUser: AuthenticatedUser | null;
  selection: AppSelection;
  expandedCategory: MenuCategory | null;
  materialIssueEnabled: boolean;
  onSelectCategory: (category: MenuCategory) => void;
  onSelectChild: (category: MenuCategory, childId: string) => void;
  onLogout: () => void;
  onNavigateHome: () => void;
  onOpenBoardList: (boardType: BoardType) => void;
  onOpenBoardPost: (boardType: BoardType, postId: number) => void;
  onBoardNavigateList: () => void;
  onBoardNavigateDetail: (postId: number) => void;
  onBoardNavigateCompose: (compose: {
    composeMode: 'create' | 'edit' | 'reply';
    postId?: number;
    parentPostId?: number;
  }) => void;
  onOpenWorkDiaryList: () => void;
  onOpenWorkDiaryDetail: (id: number) => void;
  onWorkDiaryNavigateList: () => void;
  onWorkDiaryNavigateDetail: (id: number) => void;
  onWorkDiaryNavigateCompose: (compose: { workDate?: string; editId?: number }) => void;
}

export default function AppShell({
  currentUser,
  selection,
  expandedCategory,
  materialIssueEnabled,
  onSelectCategory,
  onSelectChild,
  onLogout,
  onNavigateHome,
  onOpenBoardList,
  onOpenBoardPost,
  onBoardNavigateList,
  onBoardNavigateDetail,
  onBoardNavigateCompose,
  onOpenWorkDiaryList,
  onOpenWorkDiaryDetail,
  onWorkDiaryNavigateList,
  onWorkDiaryNavigateDetail,
  onWorkDiaryNavigateCompose,
}: AppShellProps) {
  const visibleCategories = new Set(
    currentUser ? getVisibleMenuCategories(currentUser.roleCodes) : ['home'],
  );

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
          {MENU_GROUPS.filter((group) => visibleCategories.has(group.id)).map((group) => {
            const expanded = expandedCategory === group.id;
            const isDirect = group.direct === true;
            const isActive =
              selection.category === group.id ||
              (group.id === 'home' && (selection.category === 'home' || selection.category === 'board' || selection.category === 'workdiary')) ||
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
                    {group.children
                      .filter(
                        (child) =>
                          child.id !== 'prod-material-issue' || materialIssueEnabled,
                      )
                      .map((child) => {
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

        <main className="app-main">
          {renderContent(selection, {
            currentUser,
            onNavigateHome,
            onOpenBoardList,
            onOpenBoardPost,
            onBoardNavigateList,
            onBoardNavigateDetail,
            onBoardNavigateCompose,
            onOpenWorkDiaryList,
            onOpenWorkDiaryDetail,
            onWorkDiaryNavigateList,
            onWorkDiaryNavigateDetail,
            onWorkDiaryNavigateCompose,
          })}
        </main>
      </div>
    </div>
  );
}

interface RenderContext {
  currentUser: AuthenticatedUser | null;
  onNavigateHome: () => void;
  onOpenBoardList: (boardType: BoardType) => void;
  onOpenBoardPost: (boardType: BoardType, postId: number) => void;
  onBoardNavigateList: () => void;
  onBoardNavigateDetail: (postId: number) => void;
  onBoardNavigateCompose: (compose: {
    composeMode: 'create' | 'edit' | 'reply';
    postId?: number;
    parentPostId?: number;
  }) => void;
  onOpenWorkDiaryList: () => void;
  onOpenWorkDiaryDetail: (id: number) => void;
  onWorkDiaryNavigateList: () => void;
  onWorkDiaryNavigateDetail: (id: number) => void;
  onWorkDiaryNavigateCompose: (compose: { workDate?: string; editId?: number }) => void;
}

function renderContent(selection: AppSelection, ctx: RenderContext) {
  if (selection.category === 'home') {
    return (
      <DashboardPage
        onOpenBoardList={ctx.onOpenBoardList}
        onOpenBoardPost={ctx.onOpenBoardPost}
        onOpenWorkDiaryList={ctx.onOpenWorkDiaryList}
        onOpenWorkDiaryDetail={ctx.onOpenWorkDiaryDetail}
      />
    );
  }
  if (selection.category === 'workdiary') {
    return (
      <WorkDiaryPage
        screen={selection.screen}
        currentUser={ctx.currentUser}
        onNavigateHome={ctx.onNavigateHome}
        onNavigateList={ctx.onWorkDiaryNavigateList}
        onNavigateDetail={ctx.onWorkDiaryNavigateDetail}
        onNavigateCompose={ctx.onWorkDiaryNavigateCompose}
      />
    );
  }
  if (selection.category === 'board') {
    return (
      <BoardPage
        boardType={selection.boardType}
        screen={selection.screen}
        currentUser={ctx.currentUser}
        onNavigateHome={ctx.onNavigateHome}
        onNavigateList={ctx.onBoardNavigateList}
        onNavigateDetail={ctx.onBoardNavigateDetail}
        onNavigateCompose={ctx.onBoardNavigateCompose}
      />
    );
  }
  if (selection.category === 'basis') {
    return <BasisInfoPage currentUser={ctx.currentUser} />;
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
  if (selection.category === 'purchase') {
    return renderPurchasePage(selection.page);
  }
  if (selection.category === 'quality') {
    return renderQualityPage(selection.page);
  }
  if (selection.category === 'outsource') {
    return renderOutsourcePage(selection.page);
  }
  return null;
}
