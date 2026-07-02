import { useState } from 'react';
import CompanyPage from './pages/CompanyPage';
import ItemPage from './pages/ItemPage';
import ProcessPage from './pages/ProcessPage';
import './App.css';

type Page = 'company' | 'item' | 'process';

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
          className={page === 'process' ? 'nav-active' : undefined}
          onClick={() => setPage('process')}
        >
          공정
        </button>
      </nav>
      {page === 'company' && <CompanyPage />}
      {page === 'item' && <ItemPage />}
      {page === 'process' && <ProcessPage />}
    </div>
  );
}
