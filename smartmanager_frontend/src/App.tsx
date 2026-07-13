import { useEffect, useRef, useState } from 'react';
import { BrowserRouter, Navigate, useLocation, useNavigate } from 'react-router-dom';
import { fetchCurrentUser, isAuthenticated, logout, type AuthenticatedUser } from './api/auth';
import type { BoardType } from './api/board';
import {
  fetchSystemSettings,
  SETTING_KEY_CLOSING_FISCAL_CUTOVER_DAY,
  SETTING_KEY_INVENTORY_ALLOW_NEGATIVE_STOCK,
  SETTING_KEY_MATERIAL_ISSUE_ENABLED,
} from './api/systemSettings';
import { normalizeFiscalCutoverSetting, DEFAULT_FISCAL_CUTOVER_SETTING } from './utils/fiscalCalendar';
import { setSessionExpiredHandler } from './api/http';
import LoginPage from './pages/LoginPage';
import AppShell from './layout/AppShell';
import type { AppSelection } from './layout/selection';
import type { BoardScreen } from './pages/BoardPage';
import { AuthProvider } from './context/AuthContext';
import { ConfirmProvider } from './context/ConfirmContext';
import { MaterialIssueSettingProvider } from './context/MaterialIssueSettingContext';
import { isSelectionVisible } from './layout/menuAccess';
import { defaultChildId, type MenuCategory } from './layout/menuConfig';
import {
  clearSavedNavPath,
  expandedCategoryFromSelection,
  pathForCategory,
  pathFromSelection,
  readSavedNavPath,
  selectionFromLocation,
  writeSavedNavPath,
} from './routing/appPaths';
import './App.css';

function locationKey(pathname: string, search: string): string {
  return `${pathname}${search}`;
}

function AuthenticatedApp({
  currentUser,
  materialIssueEnabled,
  setMaterialIssueEnabled,
  negativeStockAllowed,
  setNegativeStockAllowed,
  fiscalCutoverSetting,
  setFiscalCutoverSetting,
  onLogout,
  onSessionExpired,
}: {
  currentUser: AuthenticatedUser;
  materialIssueEnabled: boolean;
  setMaterialIssueEnabled: (value: boolean) => void;
  negativeStockAllowed: boolean;
  setNegativeStockAllowed: (value: boolean) => void;
  fiscalCutoverSetting: ReturnType<typeof normalizeFiscalCutoverSetting>;
  setFiscalCutoverSetting: (value: ReturnType<typeof normalizeFiscalCutoverSetting>) => void;
  onLogout: () => void;
  onSessionExpired: () => void;
}) {
  const location = useLocation();
  const navigate = useNavigate();
  const locationRef = useRef(locationKey(location.pathname, location.search));

  const parsed = selectionFromLocation(location.pathname, location.search);
  const selection: AppSelection = parsed ?? { category: 'home' };

  const [expandedCategory, setExpandedCategory] = useState<MenuCategory | null>(() =>
    expandedCategoryFromSelection(selection),
  );

  useEffect(() => {
    locationRef.current = locationKey(location.pathname, location.search);
  }, [location.pathname, location.search]);

  useEffect(() => {
    setSessionExpiredHandler(() => {
      writeSavedNavPath(locationRef.current);
      onSessionExpired();
    });
    return () => setSessionExpiredHandler(null);
  }, [onSessionExpired]);

  useEffect(() => {
    if (parsed == null) {
      navigate('/', { replace: true });
      return;
    }
    if (!isSelectionVisible(selection, currentUser.roleCodes, currentUser.authorities)) {
      navigate('/', { replace: true });
      setExpandedCategory('home');
    }
  }, [parsed, selection, currentUser, navigate]);

  useEffect(() => {
    if (
      materialIssueEnabled ||
      selection.category !== 'production' ||
      !('page' in selection) ||
      selection.page !== 'prod-material-issue'
    ) {
      return;
    }
    navigate('/production/prod-work-diary', { replace: true });
  }, [materialIssueEnabled, selection, navigate]);

  const menuCategory = expandedCategoryFromSelection(selection);
  useEffect(() => {
    setExpandedCategory(menuCategory);
  }, [menuCategory]);

  const go = (next: AppSelection, options?: { replace?: boolean }) => {
    navigate(pathFromSelection(next), options);
  };

  const onSelectCategory = (category: MenuCategory) => {
    if (category === 'home' || category === 'basis') {
      setExpandedCategory(category);
      navigate(pathForCategory(category));
      return;
    }
    setExpandedCategory((prev) => (prev === category ? null : category));
    navigate(pathForCategory(category, defaultChildId(category)));
  };

  const onSelectChild = (category: MenuCategory, childId: string) => {
    setExpandedCategory(category);
    navigate(pathForCategory(category, childId));
  };

  const onNavigateHome = () => {
    setExpandedCategory('home');
    navigate('/');
  };

  const onOpenBoardList = (boardType: BoardType) => {
    setExpandedCategory('home');
    go({ category: 'board', boardType, screen: { mode: 'list' } });
  };

  const onOpenBoardPost = (boardType: BoardType, postId: number) => {
    setExpandedCategory('home');
    go({ category: 'board', boardType, screen: { mode: 'detail', postId } });
  };

  const onBoardNavigateList = () => {
    if (selection.category !== 'board') return;
    go({ ...selection, screen: { mode: 'list' } });
  };

  const onBoardNavigateDetail = (postId: number) => {
    if (selection.category !== 'board') return;
    go({ ...selection, screen: { mode: 'detail', postId } });
  };

  const onBoardNavigateCompose = (compose: {
    composeMode: 'create' | 'edit' | 'reply';
    postId?: number;
    parentPostId?: number;
  }) => {
    if (selection.category !== 'board') return;
    const screen: BoardScreen =
      compose.composeMode === 'create'
        ? { mode: 'compose', composeMode: 'create' }
        : compose.composeMode === 'edit'
          ? { mode: 'compose', composeMode: 'edit', postId: compose.postId }
          : { mode: 'compose', composeMode: 'reply', parentPostId: compose.parentPostId };
    go({ ...selection, screen });
  };

  const onOpenWorkDiaryList = () => {
    setExpandedCategory('home');
    go({ category: 'workdiary', screen: { mode: 'list' } });
  };

  const onOpenWorkDiaryDetail = (id: number) => {
    setExpandedCategory('home');
    go({ category: 'workdiary', screen: { mode: 'detail', id } });
  };

  const onWorkDiaryNavigateList = () => {
    go({ category: 'workdiary', screen: { mode: 'list' } });
  };

  const onWorkDiaryNavigateDetail = (id: number) => {
    go({ category: 'workdiary', screen: { mode: 'detail', id } });
  };

  const onWorkDiaryNavigateCompose = (compose: { workDate?: string; editId?: number }) => {
    go({
      category: 'workdiary',
      screen:
        compose.editId != null
          ? { mode: 'compose', editId: compose.editId }
          : { mode: 'compose', workDate: compose.workDate },
    });
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
        <ConfirmProvider>
          {parsed == null ? (
            <Navigate to="/" replace />
          ) : (
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
          )}
        </ConfirmProvider>
      </AuthProvider>
    </MaterialIssueSettingProvider>
  );
}

function AppRouter() {
  const navigate = useNavigate();
  const [authed, setAuthed] = useState(isAuthenticated());
  const [sessionChecking, setSessionChecking] = useState(isAuthenticated());
  const [currentUser, setCurrentUser] = useState<AuthenticatedUser | null>(null);
  const [materialIssueEnabled, setMaterialIssueEnabled] = useState(false);
  const [negativeStockAllowed, setNegativeStockAllowed] = useState(true);
  const [fiscalCutoverSetting, setFiscalCutoverSetting] = useState(DEFAULT_FISCAL_CUTOVER_SETTING);

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

  const handleLogout = () => {
    clearSavedNavPath();
    logout();
    setCurrentUser(null);
    setAuthed(false);
  };

  const handleSessionExpired = () => {
    logout();
    setCurrentUser(null);
    setAuthed(false);
  };

  if (!authed) {
    return (
      <LoginPage
        onSuccess={() => {
          const saved = readSavedNavPath();
          clearSavedNavPath();
          setSessionChecking(true);
          setAuthed(true);
          if (saved && saved !== '/') {
            navigate(saved, { replace: true });
          } else {
            navigate('/', { replace: true });
          }
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

  return (
    <AuthenticatedApp
      currentUser={currentUser}
      materialIssueEnabled={materialIssueEnabled}
      setMaterialIssueEnabled={setMaterialIssueEnabled}
      negativeStockAllowed={negativeStockAllowed}
      setNegativeStockAllowed={setNegativeStockAllowed}
      fiscalCutoverSetting={fiscalCutoverSetting}
      setFiscalCutoverSetting={setFiscalCutoverSetting}
      onLogout={handleLogout}
      onSessionExpired={handleSessionExpired}
    />
  );
}

export default function App() {
  return (
    <BrowserRouter>
      <AppRouter />
    </BrowserRouter>
  );
}
