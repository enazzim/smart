import { useEffect, useMemo, useState } from 'react';
import type { AuthenticatedUser } from '../api/auth';
import {
  BASIS_TABS,
  type BasisTab,
  renderBasisPage,
} from '../layout/menuConfig';
import { canManageBasisData, canManageUsers } from '../layout/menuAccess';

interface BasisInfoPageProps {
  currentUser: AuthenticatedUser | null;
}

export default function BasisInfoPage({ currentUser }: BasisInfoPageProps) {
  const manageBasis = currentUser ? canManageBasisData(currentUser.roleCodes) : false;
  const manageUsers = currentUser ? canManageUsers(currentUser.authorities) : false;
  const visibleTabs = useMemo(
    () => (manageBasis ? BASIS_TABS : BASIS_TABS.filter((item) => item.id === 'user')),
    [manageBasis],
  );
  const [tab, setTab] = useState<BasisTab>(manageBasis ? 'company' : 'user');

  useEffect(() => {
    if (!manageBasis && tab !== 'user') {
      setTab('user');
    }
  }, [manageBasis, tab]);

  return (
    <div className="basis-info-page">
      <header className="basis-info-header">
        <h1>기준정보</h1>
        <p>
          {manageBasis
            ? '거래처 · 품목 · 공정 · 달력 · 사용자 등 기준정보를 탭으로 관리합니다.'
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
        {renderBasisPage(tab, { currentUser, canManageUsers: manageUsers })}
      </div>
    </div>
  );
}
