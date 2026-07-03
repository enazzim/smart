import { useState } from 'react';
import CompanyPage from './pages/CompanyPage';
import ItemCompositionPage from './pages/ItemCompositionPage';
import ItemPage from './pages/ItemPage';
import ProcessPage from './pages/ProcessPage';
import UnitPricePage from './pages/UnitPricePage';
import './App.css';

type Page = 'company' | 'item' | 'bom' | 'process' | 'unitPrice';

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
      </nav>
      {page === 'company' && <CompanyPage />}
      {page === 'item' && <ItemPage />}
      {page === 'bom' && <ItemCompositionPage />}
      {page === 'process' && <ProcessPage />}
      {page === 'unitPrice' && <UnitPricePage />}
    </div>
  );
}
