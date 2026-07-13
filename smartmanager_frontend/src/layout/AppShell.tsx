import { useEffect, useState } from 'react';
import type { AuthenticatedUser } from '../api/auth';
import type { BoardType } from '../api/board';
import {
  MENU_GROUPS,
  type MenuCategory,
  renderInventoryPage,
  renderProductionPage,
  renderOutsourcePage,
  renderPurchasePage,
  renderQualityPage,
  renderSalesPage,
  renderSystemPage,
} from './menuConfig';
import { useAuth } from '../context/AuthContext';
import { canAccessSelfAccount, getVisibleMenuCategories, isDashboardSelection } from './menuAccess';
import BasisInfoPage from '../pages/BasisInfoPage';
import BoardPage from '../pages/BoardPage';
import DashboardPage from '../pages/DashboardPage';
import WorkDiaryPage from '../pages/WorkDiaryPage';
import type { AppSelection } from './selection';

export type { AppSelection } from './selection';

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
  const [mobileNavOpen, setMobileNavOpen] = useState(false);
  const { readOnly } = useAuth();
  const visibleCategories = new Set(
    currentUser ? getVisibleMenuCategories(currentUser.roleCodes) : ['home'],
  );
  const showAccountLink =
    currentUser != null && canAccessSelfAccount(currentUser.roleCodes, currentUser.authorities);
  const transactionReadOnly = readOnly && !isDashboardSelection(selection.category);

  useEffect(() => {
    if (!mobileNavOpen) {
      return;
    }
    const previousOverflow = document.body.style.overflow;
    document.body.style.overflow = 'hidden';
    const onKeyDown = (event: KeyboardEvent) => {
      if (event.key === 'Escape') {
        setMobileNavOpen(false);
      }
    };
    window.addEventListener('keydown', onKeyDown);
    return () => {
      document.body.style.overflow = previousOverflow;
      window.removeEventListener('keydown', onKeyDown);
    };
  }, [mobileNavOpen]);

  useEffect(() => {
    setMobileNavOpen(false);
  }, [selection]);

  const handleSelectCategory = (category: MenuCategory) => {
    onSelectCategory(category);
    const group = MENU_GROUPS.find((item) => item.id === category);
    if (group?.direct) {
      setMobileNavOpen(false);
    }
  };

  const handleSelectChild = (category: MenuCategory, childId: string) => {
    onSelectChild(category, childId);
    setMobileNavOpen(false);
  };

  return (
    <div className={`app-shell${mobileNavOpen ? ' mobile-nav-open' : ''}${transactionReadOnly ? ' app-read-only' : ''}`}>
      <header className="app-header">
        <div className="app-header-left">
          <button
            type="button"
            className="mobile-nav-toggle"
            aria-label={mobileNavOpen ? '메뉴 닫기' : '메뉴 열기'}
            aria-expanded={mobileNavOpen}
            aria-controls="app-sidebar"
            onClick={() => setMobileNavOpen((open) => !open)}
          >
            <span className="mobile-nav-toggle-bar" aria-hidden="true" />
            <span className="mobile-nav-toggle-bar" aria-hidden="true" />
            <span className="mobile-nav-toggle-bar" aria-hidden="true" />
          </button>
          <div className="app-brand">
            <strong>SmartManager</strong>
            <span>ERP</span>
          </div>
        </div>
        <div className="app-header-user">
          <span>{currentUser ? `${currentUser.name} (${currentUser.loginId})` : '…'}</span>
          {showAccountLink && (
            <button type="button" className="secondary" onClick={() => onSelectCategory('basis')}>
              내 계정
            </button>
          )}
          <button type="button" className="secondary" onClick={onLogout}>
            로그아웃
          </button>
        </div>
      </header>

      <div className="app-body">
        {mobileNavOpen && (
          <button
            type="button"
            className="mobile-nav-backdrop"
            aria-label="메뉴 닫기"
            onClick={() => setMobileNavOpen(false)}
          />
        )}
        <aside id="app-sidebar" className="app-sidebar" aria-label="메인 메뉴">
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
                  onClick={() => handleSelectCategory(group.id)}
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
                            onClick={() => handleSelectChild(group.id, child.id)}
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
          {transactionReadOnly && (
            <div className="read-only-banner" role="status">
              조회 전용 권한입니다. 등록·수정·삭제는 할 수 없습니다.
            </div>
          )}
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
  if (selection.category === 'inventory') {
    return renderInventoryPage(selection.page);
  }
  if (selection.category === 'quality') {
    return renderQualityPage(selection.page);
  }
  if (selection.category === 'outsource') {
    return renderOutsourcePage(selection.page);
  }
  return null;
}
