import { useEffect, useState } from 'react';
import { fetchCurrentUser, isAuthenticated, logout, type AuthenticatedUser } from './api/auth';
import LoginPage from './pages/LoginPage';
import AppShell, { type AppSelection } from './layout/AppShell';
import type { MenuCategory, PlaceholderPageId, SystemPage } from './layout/menuConfig';
import './App.css';

const DEFAULT_SELECTION: AppSelection = { category: 'basis' };

function defaultChildId(category: MenuCategory): string {
  if (category === 'system') return 'user';
  if (category === 'sales') return 'sales-order';
  if (category === 'production') return 'prod-plan';
  if (category === 'purchase') return 'purchase-order';
  return '';
}

function toSelection(category: MenuCategory, childId?: string): AppSelection {
  if (category === 'basis') {
    return { category: 'basis' };
  }
  if (category === 'system') {
    return { category: 'system', page: (childId ?? 'user') as SystemPage };
  }
  return {
    category,
    page: (childId ?? defaultChildId(category)) as PlaceholderPageId,
  };
}

export default function App() {
  const [authed, setAuthed] = useState(isAuthenticated());
  const [currentUser, setCurrentUser] = useState<AuthenticatedUser | null>(null);
  const [selection, setSelection] = useState<AppSelection>(DEFAULT_SELECTION);
  const [expandedCategory, setExpandedCategory] = useState<MenuCategory | null>('basis');

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
    return <LoginPage onSuccess={() => setAuthed(true)} />;
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
