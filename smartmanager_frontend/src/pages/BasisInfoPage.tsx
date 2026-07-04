import { useState } from 'react';
import {
  BASIS_TABS,
  type BasisTab,
  renderBasisPage,
} from '../layout/menuConfig';

export default function BasisInfoPage() {
  const [tab, setTab] = useState<BasisTab>('company');

  return (
    <div className="basis-info-page">
      <header className="basis-info-header">
        <h1>기준정보</h1>
        <p>거래처 · 품목 · 공정 · 달력 등 기준정보를 탭으로 관리합니다.</p>
      </header>
      <nav className="tab-row basis-tab-row" aria-label="기준정보 탭">
        {BASIS_TABS.map((item) => (
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
      <div className="basis-tab-content">{renderBasisPage(tab)}</div>
    </div>
  );
}
