import { useEffect, useState } from 'react';
import type { Company, CompanyRoleType, CreateCompanyRequest } from './api/company';
import { createCompany, fetchCompanies } from './api/company';
import './App.css';

const ROLE_OPTIONS: { value: CompanyRoleType; label: string }[] = [
  { value: 'SALES', label: '판매' },
  { value: 'PURCHASE', label: '구매' },
  { value: 'OUTSOURCE', label: '외주' },
  { value: 'COST', label: '비용' },
];

const emptyForm: CreateCompanyRequest = {
  companyName: '',
  presidentName: '',
  businessRegNo: '',
  businessAddress: '',
  roles: ['SALES'],
};

export default function App() {
  const [companies, setCompanies] = useState<Company[]>([]);
  const [form, setForm] = useState<CreateCompanyRequest>(emptyForm);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const load = async () => {
    setLoading(true);
    setError(null);
    try {
      setCompanies(await fetchCompanies());
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    void load();
  }, []);

  const toggleRole = (role: CompanyRoleType) => {
    setForm((prev) => {
      const exists = prev.roles.includes(role);
      const roles = exists ? prev.roles.filter((r) => r !== role) : [...prev.roles, role];
      return { ...prev, roles };
    });
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSubmitting(true);
    setError(null);
    try {
      await createCompany(form);
      setForm(emptyForm);
      await load();
    } catch (err) {
      setError(err instanceof Error ? err.message : '등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="layout">
      <header>
        <h1>거래처 (Company)</h1>
        <p>SmartManager Part 02 — 등록 시 PartnerLedger 원장 account 자동 생성</p>
      </header>

      {error && <div className="error">{error}</div>}

      <section className="panel">
        <h2>거래처 등록</h2>
        <form onSubmit={onSubmit} className="form-grid">
          <label>
            상호 *
            <input
              required
              value={form.companyName}
              onChange={(e) => setForm({ ...form, companyName: e.target.value })}
            />
          </label>
          <label>
            대표자 *
            <input
              required
              value={form.presidentName}
              onChange={(e) => setForm({ ...form, presidentName: e.target.value })}
            />
          </label>
          <label>
            사업자번호 *
            <input
              required
              value={form.businessRegNo}
              onChange={(e) => setForm({ ...form, businessRegNo: e.target.value })}
            />
          </label>
          <label>
            사업장주소 *
            <input
              required
              value={form.businessAddress}
              onChange={(e) => setForm({ ...form, businessAddress: e.target.value })}
            />
          </label>
          <label>
            전화
            <input
              value={form.telephone ?? ''}
              onChange={(e) => setForm({ ...form, telephone: e.target.value })}
            />
          </label>
          <label>
            담당자
            <input
              value={form.contactName ?? ''}
              onChange={(e) => setForm({ ...form, contactName: e.target.value })}
            />
          </label>
          <fieldset className="roles">
            <legend>역할 * (최소 1개)</legend>
            {ROLE_OPTIONS.map((opt) => (
              <label key={opt.value} className="checkbox">
                <input
                  type="checkbox"
                  checked={form.roles.includes(opt.value)}
                  onChange={() => toggleRole(opt.value)}
                />
                {opt.label}
              </label>
            ))}
          </fieldset>
          <button type="submit" disabled={submitting || form.roles.length === 0}>
            {submitting ? '등록 중…' : '등록'}
          </button>
        </form>
      </section>

      <section className="panel">
        <h2>거래처 목록</h2>
        {loading ? (
          <p>불러오는 중…</p>
        ) : companies.length === 0 ? (
          <p>등록된 거래처가 없습니다.</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>ID</th>
                <th>상호</th>
                <th>사업자번호</th>
                <th>대표자</th>
                <th>역할</th>
              </tr>
            </thead>
            <tbody>
              {companies.map((c) => (
                <tr key={c.id}>
                  <td>{c.id}</td>
                  <td>{c.companyName}</td>
                  <td>{c.businessRegNo}</td>
                  <td>{c.presidentName}</td>
                  <td>{c.roles.join(', ')}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </section>
    </div>
  );
}
