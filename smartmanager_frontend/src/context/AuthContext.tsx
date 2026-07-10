import { createContext, useContext, useMemo, type ReactNode } from 'react';
import type { AuthenticatedUser } from '../api/auth';
import { isTransactionReadOnly } from '../layout/menuAccess';

interface AuthContextValue {
  currentUser: AuthenticatedUser | null;
  readOnly: boolean;
  canWrite: (permission: string) => boolean;
  /** 대시보드(게시판·업무일지) — VIEWER 여부와 무관하게 CRUD 허용 */
  canWriteDashboard: () => boolean;
}

const AuthContext = createContext<AuthContextValue>({
  currentUser: null,
  readOnly: true,
  canWrite: () => false,
  canWriteDashboard: () => false,
});

interface AuthProviderProps {
  currentUser: AuthenticatedUser | null;
  children: ReactNode;
}

export function AuthProvider({ currentUser, children }: AuthProviderProps) {
  const value = useMemo<AuthContextValue>(() => {
    const readOnly = currentUser ? isTransactionReadOnly(currentUser.roleCodes) : true;
    return {
      currentUser,
      readOnly,
      canWrite: (permission: string) =>
        !readOnly && (currentUser?.authorities.includes(permission) ?? false),
      canWriteDashboard: () => currentUser != null,
    };
  }, [currentUser]);

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth(): AuthContextValue {
  return useContext(AuthContext);
}
