import { useEffect, useState } from 'react';
import type { Company, CompanyRoleType, CreateCompanyRequest, UpdateCompanyRequest } from '../api/company';
import { createCompany, deleteCompany, fetchCompanies, updateCompany } from '../api/company';

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

function toUpdatePayload(company: Company): UpdateCompanyRequest {
  return {
    companyName: company.companyName,
    presidentName: company.presidentName,
    corporationRegNo: company.corporationRegNo ?? undefined,
    businessAddress: company.businessAddress,
    homepageUrl: company.homepageUrl ?? undefined,
    businessType: company.businessType ?? undefined,
    businessItem: company.businessItem ?? undefined,
    telephone: company.telephone ?? undefined,
    fax: company.fax ?? undefined,
    saleStandardDay: company.saleStandardDay ?? undefined,
    billApprovalStandard: company.billApprovalStandard ?? undefined,
    fixCollectDay1: company.fixCollectDay1 ?? undefined,
    contactName: company.contactName ?? undefined,
    contactEmail: company.contactEmail ?? undefined,
    roles: [...company.roles],
  };
}

export default function CompanyPage() {
  const [companies, setCompanies] = useState<Company[]>([]);
  const [form, setForm] = useState<CreateCompanyRequest>(emptyForm);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [editingBusinessRegNo, setEditingBusinessRegNo] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const isEditing = editingId !== null;

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

  const resetForm = () => {
    setForm(emptyForm);
    setEditingId(null);
    setEditingBusinessRegNo(null);
  };

  const toggleRole = (role: CompanyRoleType) => {
    setForm((prev) => {
      const exists = prev.roles.includes(role);
      const roles = exists ? prev.roles.filter((r) => r !== role) : [...prev.roles, role];
      return { ...prev, roles };
    });
  };

  const startEdit = (company: Company) => {
    setEditingId(company.id);
    setEditingBusinessRegNo(company.businessRegNo);
    setForm({
      ...toUpdatePayload(company),
      businessRegNo: company.businessRegNo,
    });
    setError(null);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSubmitting(true);
    setError(null);
    try {
      if (isEditing && editingId !== null) {
        const { businessRegNo: _ignored, ...updatePayload } = form;
        await updateCompany(editingId, updatePayload);
      } else {
        await createCompany(form);
      }
      resetForm();
      await load();
    } catch (err) {
      setError(err instanceof Error ? err.message : isEditing ? '수정 실패' : '등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (company: Company) => {
    if (!window.confirm(`「${company.companyName}」 거래처를 삭제하시겠습니까?`)) {
      return;
    }
    setError(null);
    try {
      await deleteCompany(company.id);
      if (editingId === company.id) {
        resetForm();
      }
      await load();
    } catch (err) {
      setError(err instanceof Error ? err.message : '삭제 실패');
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>거래처 (Company)</h1>
        <p>등록·수정·삭제 — PartnerLedger 원장 연동</p>
      </header>

      {error && <div className="error">{error}</div>}

      <section className="panel">
        <h2>{isEditing ? `거래처 수정 (ID ${editingId})` : '거래처 등록'}</h2>
        <form onSubmit={onSubmit} className="form-grid form-grid-wide">
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
              readOnly={isEditing}
              value={isEditing ? (editingBusinessRegNo ?? form.businessRegNo) : form.businessRegNo}
              onChange={(e) => setForm({ ...form, businessRegNo: e.target.value })}
              className={isEditing ? 'readonly' : undefined}
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
          <div className="form-actions">
            <button type="submit" disabled={submitting || form.roles.length === 0}>
              {submitting ? '저장 중…' : isEditing ? '수정 저장' : '등록'}
            </button>
            {isEditing && (
              <button type="button" className="secondary" onClick={resetForm} disabled={submitting}>
                취소
              </button>
            )}
          </div>
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
                <th>작업</th>
              </tr>
            </thead>
            <tbody>
              {companies.map((c) => (
                <tr key={c.id} className={editingId === c.id ? 'row-editing' : undefined}>
                  <td>{c.id}</td>
                  <td>{c.companyName}</td>
                  <td>{c.businessRegNo}</td>
                  <td>{c.presidentName}</td>
                  <td>{c.roles.join(', ')}</td>
                  <td className="actions">
                    <button type="button" className="btn-action" onClick={() => startEdit(c)}>
                      수정
                    </button>
                    <button type="button" className="btn-action danger" onClick={() => void onDelete(c)}>
                      삭제
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </section>
    </div>
  );
}
