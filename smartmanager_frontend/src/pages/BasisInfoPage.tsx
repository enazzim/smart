import { useEffect, useMemo, useState } from 'react';
import type { AuthenticatedUser } from '../api/auth';
import {
  BASIS_TABS,
  type BasisTab,
  renderBasisPage,
} from '../layout/menuConfig';
import {
  canHardDeleteDrawings,
  canManageBasisData,
  canManageDrawings,
  canManageUsers,
  canReadDrawings,
} from '../layout/menuAccess';

interface BasisInfoPageProps {
  currentUser: AuthenticatedUser | null;
}

export default function BasisInfoPage({ currentUser }: BasisInfoPageProps) {
  const manageBasis = currentUser ? canManageBasisData(currentUser.roleCodes) : false;
  const manageUsers = currentUser ? canManageUsers(currentUser.authorities) : false;
  const readDrawings = currentUser ? canReadDrawings(currentUser.authorities) : false;
  const manageDrawings = currentUser ? canManageDrawings(currentUser.authorities) : false;
  const hardDeleteDrawings = currentUser ? canHardDeleteDrawings(currentUser.authorities) : false;
  const visibleTabs = useMemo(
    () =>
      BASIS_TABS.filter((item) => {
        if (item.id === 'user') {
          return true;
        }
        if (item.id === 'drawing') {
          return manageBasis || readDrawings;
        }
        return manageBasis;
      }),
    [manageBasis, readDrawings],
  );
  const [tab, setTab] = useState<BasisTab>(manageBasis ? 'company' : readDrawings ? 'drawing' : 'user');

  useEffect(() => {
    if (!visibleTabs.some((item) => item.id === tab)) {
      setTab(visibleTabs[0]?.id ?? 'user');
    }
  }, [visibleTabs, tab]);

  return (
    <div className="basis-info-page">
      <header className="basis-info-header">
        <h1>기준정보</h1>
        <p>
          {manageBasis
            ? '거래처 · 품목 · 공정 · 도면 · 달력 · 사용자 등 기준정보를 탭으로 관리합니다.'
            : readDrawings
              ? '도면 조회 및 내 계정 정보를 관리할 수 있습니다.'
              : '내 계정 정보를 확인하고 비밀번호를 변경할 수 있습니다.'}
        </p>
      </header>
      <nav className="tab-row basis-tab-row" aria-label="기준정보 탭">
        {visibleTabs.map((item) => (
          <button
            key={item.id}
            type="button"
            className={tab === item.id ? 'tab-active' : undefined}
            onClick={() => setTab(item.id)}
          >
            {item.label}
          </button>
        ))}
      </nav>
      <div className="basis-tab-content">
        {renderBasisPage(tab, {
          currentUser,
          canManageUsers: manageUsers,
          canManageDrawings: manageDrawings,
          canHardDeleteDrawings: hardDeleteDrawings,
          canReadDrawings: readDrawings,
        })}
      </div>
    </div>
  );
}
