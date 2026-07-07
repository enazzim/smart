import { useEffect, useRef, useState } from 'react';
import { fetchCurrentUser, isAuthenticated, logout, type AuthenticatedUser } from './api/auth';
import type { BoardType } from './api/board';
import { fetchSystemSettings, SETTING_KEY_INVENTORY_ALLOW_NEGATIVE_STOCK, SETTING_KEY_MATERIAL_ISSUE_ENABLED } from './api/systemSettings';
import { setSessionExpiredHandler } from './api/http';
import LoginPage from './pages/LoginPage';
import AppShell, { type AppSelection } from './layout/AppShell';
import type { BoardScreen } from './pages/BoardPage';
import { MaterialIssueSettingProvider } from './context/MaterialIssueSettingContext';
import { isSelectionVisible } from './layout/menuAccess';
import {
  defaultChildId,
  type MenuCategory,
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

function readSavedNavigation(): { selection: AppSelection; expandedCategory: MenuCategory | null } | null {
  try {
    const raw = sessionStorage.getItem(NAV_STORAGE_KEY);
    if (!raw) {
      return null;
    }
    return JSON.parse(raw) as { selection: AppSelection; expandedCategory: MenuCategory | null };
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
  const [currentUser, setCurrentUser] = useState<AuthenticatedUser | null>(null);
  const [selection, setSelection] = useState<AppSelection>(() => readSavedNavigation()?.selection ?? DEFAULT_SELECTION);
  const [expandedCategory, setExpandedCategory] = useState<MenuCategory | null>(
    () => readSavedNavigation()?.expandedCategory ?? 'home',
  );
  const [materialIssueEnabled, setMaterialIssueEnabled] = useState(false);
  const [negativeStockAllowed, setNegativeStockAllowed] = useState(true);
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
      setCurrentUser(null);
      setMaterialIssueEnabled(false);
      setNegativeStockAllowed(true);
      return;
    }
    void fetchCurrentUser()
      .then(setCurrentUser)
      .catch(() => {
        logout();
        setAuthed(false);
      });
    void fetchSystemSettings()
      .then((rows) => {
        const materialIssueRow = rows.find((item) => item.settingKey === SETTING_KEY_MATERIAL_ISSUE_ENABLED);
        const negativeStockRow = rows.find((item) => item.settingKey === SETTING_KEY_INVENTORY_ALLOW_NEGATIVE_STOCK);
        setMaterialIssueEnabled(materialIssueRow?.value === 'YES');
        setNegativeStockAllowed(negativeStockRow?.value !== 'NO');
      })
      .catch(() => {
        setMaterialIssueEnabled(false);
        setNegativeStockAllowed(true);
      });
  }, [authed]);

  useEffect(() => {
    if (!currentUser) {
      return;
    }
    if (!isSelectionVisible(selection, currentUser.roleCodes)) {
      setSelection({ category: 'home' });
      setExpandedCategory('home');
    }
  }, [currentUser, selection]);

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
          setAuthed(true);
        }}
      />
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
    >
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
    </MaterialIssueSettingProvider>
  );
}
