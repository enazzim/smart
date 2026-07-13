import { useEffect, useRef, useState } from 'react';
import { fetchCurrentUser, isAuthenticated, logout, type AuthenticatedUser } from './api/auth';
import type { BoardType } from './api/board';
import { fetchSystemSettings, SETTING_KEY_INVENTORY_ALLOW_NEGATIVE_STOCK, SETTING_KEY_MATERIAL_ISSUE_ENABLED, SETTING_KEY_CLOSING_FISCAL_CUTOVER_DAY } from './api/systemSettings';
import { normalizeFiscalCutoverSetting, DEFAULT_FISCAL_CUTOVER_SETTING } from './utils/fiscalCalendar';
import { setSessionExpiredHandler } from './api/http';
import LoginPage from './pages/LoginPage';
import AppShell, { type AppSelection } from './layout/AppShell';
import type { BoardScreen } from './pages/BoardPage';
import { AuthProvider } from './context/AuthContext';
import { MaterialIssueSettingProvider } from './context/MaterialIssueSettingContext';
import { isSelectionVisible } from './layout/menuAccess';
import {
  defaultChildId,
  type MenuCategory,
  type InventoryPageId,
  type OutsourcePageId,
  type ProductionPageId,
  type PurchasePageId,
  type QualityPageId,
  type SalesPageId,
  type SystemPage,
} from './layout/menuConfig';
import './App.css';

const DEFAULT_SELECTION: AppSelection = { category: 'home' };
const NAV_STORAGE_KEY = 'smartmanager.nav';

function getLegacyInventoryPageFromPurchase(selection: AppSelection): InventoryPageId | null {
  if (selection.category !== 'purchase' || !('page' in selection)) {
    return null;
  }
  const page = selection.page as string;
  if (page === 'inventory-misc-movement' || page === 'inventory-ledger' || page === 'inventory-lot') {
    return page as InventoryPageId;
  }
  return null;
}

function readSavedNavigation(): { selection: AppSelection; expandedCategory: MenuCategory | null } | null {
  try {
    const raw = sessionStorage.getItem(NAV_STORAGE_KEY);
    if (!raw) {
      return null;
    }
    const parsed = JSON.parse(raw) as { selection: AppSelection; expandedCategory: MenuCategory | null };
    const legacyPage = getLegacyInventoryPageFromPurchase(parsed.selection);
    const selection = legacyPage
      ? { category: 'inventory' as const, page: legacyPage }
      : parsed.selection;
    const expandedCategory =
      parsed.expandedCategory === 'purchase' &&
      selection.category === 'inventory'
        ? 'inventory'
        : parsed.expandedCategory;
    return { selection, expandedCategory };
  } catch {
    return null;
  }
}

function toSelection(category: MenuCategory, childId?: string): AppSelection {
  if (category === 'home') {
    return { category: 'home' };
  }
  if (category === 'basis') {
    return { category: 'basis' };
  }
  if (category === 'system') {
    return { category: 'system', page: (childId ?? defaultChildId(category)) as SystemPage };
  }
  if (category === 'sales') {
    return { category: 'sales', page: (childId ?? defaultChildId(category)) as SalesPageId };
  }
  if (category === 'production') {
    return { category: 'production', page: (childId ?? defaultChildId(category)) as ProductionPageId };
  }
  if (category === 'purchase') {
    return { category: 'purchase', page: (childId ?? defaultChildId(category)) as PurchasePageId };
  }
  if (category === 'inventory') {
    return { category: 'inventory', page: (childId ?? defaultChildId(category)) as InventoryPageId };
  }
  if (category === 'quality') {
    return { category: 'quality', page: (childId ?? defaultChildId(category)) as QualityPageId };
  }
  if (category === 'outsource') {
    return { category: 'outsource', page: (childId ?? defaultChildId(category)) as OutsourcePageId };
  }
  return {
    category: 'sales',
    page: (childId ?? defaultChildId(category)) as SalesPageId,
  };
}

export default function App() {
  const [authed, setAuthed] = useState(isAuthenticated());
  const [sessionChecking, setSessionChecking] = useState(isAuthenticated());
  const [currentUser, setCurrentUser] = useState<AuthenticatedUser | null>(null);
  const [selection, setSelection] = useState<AppSelection>(() => readSavedNavigation()?.selection ?? DEFAULT_SELECTION);
  const [expandedCategory, setExpandedCategory] = useState<MenuCategory | null>(
    () => readSavedNavigation()?.expandedCategory ?? 'home',
  );
  const [materialIssueEnabled, setMaterialIssueEnabled] = useState(false);
  const [negativeStockAllowed, setNegativeStockAllowed] = useState(true);
  const [fiscalCutoverSetting, setFiscalCutoverSetting] = useState(DEFAULT_FISCAL_CUTOVER_SETTING);
  const selectionRef = useRef(selection);
  const expandedCategoryRef = useRef(expandedCategory);

  useEffect(() => {
    selectionRef.current = selection;
  }, [selection]);

  useEffect(() => {
    expandedCategoryRef.current = expandedCategory;
  }, [expandedCategory]);

  useEffect(() => {
    setSessionExpiredHandler(() => {
      try {
        sessionStorage.setItem(
          NAV_STORAGE_KEY,
          JSON.stringify({
            selection: selectionRef.current,
            expandedCategory: expandedCategoryRef.current,
          }),
        );
      } catch {
        // ignore storage errors
      }
      setCurrentUser(null);
      setAuthed(false);
    });
    return () => setSessionExpiredHandler(null);
  }, []);

  useEffect(() => {
    if (!authed) {
      setSessionChecking(false);
      setCurrentUser(null);
      setMaterialIssueEnabled(false);
      setNegativeStockAllowed(true);
      setFiscalCutoverSetting(DEFAULT_FISCAL_CUTOVER_SETTING);
      return;
    }
    setSessionChecking(true);
    void fetchCurrentUser()
      .then((user) => {
        setCurrentUser(user);
        setSessionChecking(false);
      })
      .catch((error: unknown) => {
        // /me 실패 = 세션 없음 → 로그인 화면
        console.error('현재 사용자 조회 실패', error);
        logout();
        setCurrentUser(null);
        setAuthed(false);
        setSessionChecking(false);
      });
    void fetchSystemSettings()
      .then((rows) => {
        const materialIssueRow = rows.find((item) => item.settingKey === SETTING_KEY_MATERIAL_ISSUE_ENABLED);
        const negativeStockRow = rows.find((item) => item.settingKey === SETTING_KEY_INVENTORY_ALLOW_NEGATIVE_STOCK);
        const fiscalCutoverRow = rows.find((item) => item.settingKey === SETTING_KEY_CLOSING_FISCAL_CUTOVER_DAY);
        setMaterialIssueEnabled(materialIssueRow?.value === 'YES');
        setNegativeStockAllowed(negativeStockRow?.value !== 'NO');
        setFiscalCutoverSetting(normalizeFiscalCutoverSetting(fiscalCutoverRow?.value));
      })
      .catch(() => {
        setMaterialIssueEnabled(false);
        setNegativeStockAllowed(true);
        setFiscalCutoverSetting(DEFAULT_FISCAL_CUTOVER_SETTING);
      });
  }, [authed]);

  useEffect(() => {
    if (!currentUser) {
      return;
    }
    const legacyPage = getLegacyInventoryPageFromPurchase(selection);
    if (legacyPage) {
      setSelection({ category: 'inventory', page: legacyPage });
      if (expandedCategory === 'purchase') {
        setExpandedCategory('inventory');
      }
      return;
    }
    if (!isSelectionVisible(selection, currentUser.roleCodes, currentUser.authorities)) {
      setSelection({ category: 'home' });
      setExpandedCategory('home');
    }
  }, [currentUser, selection, expandedCategory]);

  useEffect(() => {
    if (
      materialIssueEnabled ||
      selection.category !== 'production' ||
      !('page' in selection) ||
      selection.page !== 'prod-material-issue'
    ) {
      return;
    }
    setSelection({ category: 'production', page: 'prod-work-diary' });
  }, [materialIssueEnabled, selection]);

  if (!authed) {
    return (
      <LoginPage
        onSuccess={() => {
          const saved = readSavedNavigation();
          if (saved) {
            setSelection(saved.selection);
            setExpandedCategory(saved.expandedCategory);
            sessionStorage.removeItem(NAV_STORAGE_KEY);
          }
          setSessionChecking(true);
          setAuthed(true);
        }}
      />
    );
  }

  if (sessionChecking || !currentUser) {
    return (
      <div className="login-page">
        <section className="panel login-panel">
          <header>
            <h1>SmartManager</h1>
            <p>세션을 확인하는 중입니다…</p>
          </header>
        </section>
      </div>
    );
  }

  const onSelectCategory = (category: MenuCategory) => {
    if (category === 'home') {
      setSelection({ category: 'home' });
      setExpandedCategory('home');
      return;
    }
    if (category === 'basis') {
      setSelection({ category: 'basis' });
      setExpandedCategory('basis');
      return;
    }
    setExpandedCategory((prev) => (prev === category ? null : category));
    setSelection(toSelection(category, defaultChildId(category)));
  };

  const onNavigateHome = () => {
    setSelection({ category: 'home' });
    setExpandedCategory('home');
  };

  const onOpenBoardList = (boardType: BoardType) => {
    setSelection({ category: 'board', boardType, screen: { mode: 'list' } });
    setExpandedCategory('home');
  };

  const onOpenBoardPost = (boardType: BoardType, postId: number) => {
    setSelection({ category: 'board', boardType, screen: { mode: 'detail', postId } });
    setExpandedCategory('home');
  };

  const onBoardNavigateList = () => {
    setSelection((prev) => {
      if (prev.category !== 'board') return prev;
      return { ...prev, screen: { mode: 'list' } };
    });
  };

  const onBoardNavigateDetail = (postId: number) => {
    setSelection((prev) => {
      if (prev.category !== 'board') return prev;
      return { ...prev, screen: { mode: 'detail', postId } };
    });
  };

  const onBoardNavigateCompose = (compose: {
    composeMode: 'create' | 'edit' | 'reply';
    postId?: number;
    parentPostId?: number;
  }) => {
    setSelection((prev) => {
      if (prev.category !== 'board') return prev;
      const screen: BoardScreen =
        compose.composeMode === 'create'
          ? { mode: 'compose', composeMode: 'create' }
          : compose.composeMode === 'edit'
            ? { mode: 'compose', composeMode: 'edit', postId: compose.postId }
            : { mode: 'compose', composeMode: 'reply', parentPostId: compose.parentPostId };
      return { ...prev, screen };
    });
  };

  const onOpenWorkDiaryList = () => {
    setSelection({ category: 'workdiary', screen: { mode: 'list' } });
    setExpandedCategory('home');
  };

  const onOpenWorkDiaryDetail = (id: number) => {
    setSelection({ category: 'workdiary', screen: { mode: 'detail', id } });
    setExpandedCategory('home');
  };

  const onWorkDiaryNavigateList = () => {
    setSelection({ category: 'workdiary', screen: { mode: 'list' } });
  };

  const onWorkDiaryNavigateDetail = (id: number) => {
    setSelection({ category: 'workdiary', screen: { mode: 'detail', id } });
  };

  const onWorkDiaryNavigateCompose = (compose: { workDate?: string; editId?: number }) => {
    setSelection({
      category: 'workdiary',
      screen: compose.editId != null
        ? { mode: 'compose', editId: compose.editId }
        : { mode: 'compose', workDate: compose.workDate },
    });
  };

  const onSelectChild = (category: MenuCategory, childId: string) => {
    setExpandedCategory(category);
    setSelection(toSelection(category, childId));
  };

  const onLogout = () => {
    sessionStorage.removeItem(NAV_STORAGE_KEY);
    logout();
    setAuthed(false);
  };

  return (
    <MaterialIssueSettingProvider
      materialIssueEnabled={materialIssueEnabled}
      setMaterialIssueEnabled={setMaterialIssueEnabled}
      negativeStockAllowed={negativeStockAllowed}
      setNegativeStockAllowed={setNegativeStockAllowed}
      fiscalCutoverSetting={fiscalCutoverSetting}
      setFiscalCutoverSetting={setFiscalCutoverSetting}
    >
      <AuthProvider currentUser={currentUser}>
        <AppShell
          currentUser={currentUser}
          selection={selection}
          expandedCategory={expandedCategory}
          materialIssueEnabled={materialIssueEnabled}
          onSelectCategory={onSelectCategory}
          onSelectChild={onSelectChild}
          onLogout={onLogout}
          onNavigateHome={onNavigateHome}
          onOpenBoardList={onOpenBoardList}
          onOpenBoardPost={onOpenBoardPost}
          onBoardNavigateList={onBoardNavigateList}
          onBoardNavigateDetail={onBoardNavigateDetail}
          onBoardNavigateCompose={onBoardNavigateCompose}
          onOpenWorkDiaryList={onOpenWorkDiaryList}
          onOpenWorkDiaryDetail={onOpenWorkDiaryDetail}
          onWorkDiaryNavigateList={onWorkDiaryNavigateList}
          onWorkDiaryNavigateDetail={onWorkDiaryNavigateDetail}
          onWorkDiaryNavigateCompose={onWorkDiaryNavigateCompose}
        />
      </AuthProvider>
    </MaterialIssueSettingProvider>
  );
}
