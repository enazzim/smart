import { useEffect, useRef, useState } from 'react';
import { fetchCurrentUser, isAuthenticated, logout, type AuthenticatedUser } from './api/auth';
import { setSessionExpiredHandler } from './api/http';
import LoginPage from './pages/LoginPage';
import AppShell, { type AppSelection } from './layout/AppShell';
import {
  defaultChildId,
  type MenuCategory,
  type PlaceholderPageId,
  type ProductionPageId,
  type SalesPageId,
  type SystemPage,
} from './layout/menuConfig';
import './App.css';

const DEFAULT_SELECTION: AppSelection = { category: 'basis' };
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
  return {
    category,
    page: (childId ?? defaultChildId(category)) as PlaceholderPageId,
  };
}

export default function App() {
  const [authed, setAuthed] = useState(isAuthenticated());
  const [currentUser, setCurrentUser] = useState<AuthenticatedUser | null>(null);
  const [selection, setSelection] = useState<AppSelection>(() => readSavedNavigation()?.selection ?? DEFAULT_SELECTION);
  const [expandedCategory, setExpandedCategory] = useState<MenuCategory | null>(
    () => readSavedNavigation()?.expandedCategory ?? 'basis',
  );
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
      return;
    }
    void fetchCurrentUser()
      .then(setCurrentUser)
      .catch(() => {
        logout();
        setAuthed(false);
      });
  }, [authed]);

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
    if (category === 'basis') {
      setSelection({ category: 'basis' });
      setExpandedCategory('basis');
      return;
    }
    setExpandedCategory((prev) => (prev === category ? null : category));
    setSelection(toSelection(category, defaultChildId(category)));
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
    <AppShell
      currentUser={currentUser}
      selection={selection}
      expandedCategory={expandedCategory}
      onSelectCategory={onSelectCategory}
      onSelectChild={onSelectChild}
      onLogout={onLogout}
    />
  );
}
