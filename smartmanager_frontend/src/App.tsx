import { useState } from 'react';
import CompanyPage from './pages/CompanyPage';
import ItemCompositionPage from './pages/ItemCompositionPage';
import ItemPage from './pages/ItemPage';
import ProcessPage from './pages/ProcessPage';
import UnitPricePage from './pages/UnitPricePage';
import WorkCenterPage from './pages/WorkCenterPage';
import WorkStandardPage from './pages/WorkStandardPage';
import './App.css';

type Page = 'company' | 'item' | 'bom' | 'process' | 'unitPrice' | 'workCenter' | 'workStandard';

export default function App() {
  const [page, setPage] = useState<Page>('company');

  return (
    <div className="layout">
      <nav className="nav">
        <button
          type="button"
          className={page === 'company' ? 'nav-active' : undefined}
          onClick={() => setPage('company')}
        >
          거래처
        </button>
        <button
          type="button"
          className={page === 'item' ? 'nav-active' : undefined}
          onClick={() => setPage('item')}
        >
          품목
        </button>
        <button
          type="button"
          className={page === 'bom' ? 'nav-active' : undefined}
          onClick={() => setPage('bom')}
        >
          품목구성
        </button>
        <button
          type="button"
          className={page === 'process' ? 'nav-active' : undefined}
          onClick={() => setPage('process')}
        >
          공정
        </button>
        <button
          type="button"
          className={page === 'unitPrice' ? 'nav-active' : undefined}
          onClick={() => setPage('unitPrice')}
        >
          단가
        </button>
        <button
          type="button"
          className={page === 'workCenter' ? 'nav-active' : undefined}
          onClick={() => setPage('workCenter')}
        >
          작업장
        </button>
        <button
          type="button"
          className={page === 'workStandard' ? 'nav-active' : undefined}
          onClick={() => setPage('workStandard')}
        >
          작업표준
        </button>
      </nav>
      {page === 'company' && <CompanyPage />}
      {page === 'item' && <ItemPage />}
      {page === 'bom' && <ItemCompositionPage />}
      {page === 'process' && <ProcessPage />}
      {page === 'unitPrice' && <UnitPricePage />}
      {page === 'workCenter' && <WorkCenterPage />}
      {page === 'workStandard' && <WorkStandardPage />}
    </div>
  );
}
