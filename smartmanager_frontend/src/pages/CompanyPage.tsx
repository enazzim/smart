import { useCallback, useMemo, useState } from 'react';
import type { Company, CompanyRoleType, CreateCompanyRequest, UpdateCompanyRequest } from '../api/company';
import { createCompany, deleteCompany, fetchCompanies, updateCompany } from '../api/company';
import CompanySearchField, { type CompanySearchSelection } from '../components/CompanySearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import VirtualMasterTable from '../components/VirtualMasterTable';
import { formatInteger } from '../utils/numberFormat';
import {
  canonicalizeBusinessRegNo,
  formatBusinessRegNo,
  isStandardBusinessRegNo,
} from '../utils/businessRegNo';
import { useConfirm } from '../context/ConfirmContext';

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
  const confirm = useConfirm();
  const [companies, setCompanies] = useState<Company[]>([]);
  const [draftCompany, setDraftCompany] = useState<CompanySearchSelection | null>(null);
  const [appliedCompany, setAppliedCompany] = useState<CompanySearchSelection | null>(null);
  const [hasSearched, setHasSearched] = useState(false);
  const [clearToken, setClearToken] = useState(0);
  const [form, setForm] = useState<CreateCompanyRequest>(emptyForm);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [editingBusinessRegNo, setEditingBusinessRegNo] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const isEditing = editingId !== null;

  const displayedCompanies = useMemo(() => {
    if (!hasSearched) {
      return [];
    }
    if (!appliedCompany) {
      return companies;
    }
    return companies.filter((company) => company.id === appliedCompany.id);
  }, [companies, appliedCompany, hasSearched]);

  const companyExportRows = useMemo(
    () =>
      displayedCompanies.map((company) => ({
        ID: company.id,
        상호: company.companyName,
        사업자번호: formatBusinessRegNo(company.businessRegNo),
        대표자: company.presidentName,
        역할: company.roles.join(', '),
      })),
    [displayedCompanies],
  );

  const loadCompanies = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      return await fetchCompanies();
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
      return null;
    } finally {
      setLoading(false);
    }
  }, []);

  const onSearch = async () => {
    const rows = await loadCompanies();
    if (rows == null) {
      return;
    }
    setCompanies(rows);
    setAppliedCompany(draftCompany);
    setHasSearched(true);
  };

  const onResetSearch = () => {
    setDraftCompany(null);
    setAppliedCompany(null);
    setCompanies([]);
    setHasSearched(false);
    setClearToken((token) => token + 1);
    setError(null);
  };

  const refreshListIfSearched = async () => {
    if (!hasSearched) {
      return;
    }
    const rows = await loadCompanies();
    if (rows != null) {
      setCompanies(rows);
    }
  };

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
    const displayRegNo = formatBusinessRegNo(company.businessRegNo);
    setEditingBusinessRegNo(displayRegNo);
    setForm({
      ...toUpdatePayload(company),
      businessRegNo: displayRegNo,
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
        const rawRegNo = form.businessRegNo.trim();
        if (!rawRegNo) {
          setError('사업자번호는 필수입니다.');
          return;
        }
        let businessRegNo = canonicalizeBusinessRegNo(rawRegNo);
        if (!isStandardBusinessRegNo(rawRegNo)) {
          const ok = await confirm(
            `사업자등록번호 형식이 표준(XXX-XX-XXXXX, 숫자 10자리)과 다릅니다.\n입력값: ${rawRegNo}\n\n이대로 등록하시겠습니까?`,
            {
              title: '사업자등록번호 형식 확인',
              confirmLabel: '그대로 등록',
              cancelLabel: '수정',
            },
          );
          if (!ok) {
            return;
          }
          businessRegNo = rawRegNo;
        }
        setForm((prev) => ({ ...prev, businessRegNo }));
        await createCompany({ ...form, businessRegNo });
      }
      resetForm();
      await refreshListIfSearched();
    } catch (err) {
      setError(err instanceof Error ? err.message : isEditing ? '수정 실패' : '등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (company: Company) => {
    if (!(await confirm(`「${company.companyName}」 거래처를 삭제하시겠습니까?`, { title: '삭제 확인', confirmLabel: '삭제', cancelLabel: '닫기', danger: true }))) {
      return;
    }
    setError(null);
    try {
      await deleteCompany(company.id);
      if (editingId === company.id) {
        resetForm();
      }
      await refreshListIfSearched();
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
              placeholder="000-00-00000 또는 숫자 10자리"
              value={isEditing ? (editingBusinessRegNo ?? form.businessRegNo) : form.businessRegNo}
              onChange={(e) => setForm({ ...form, businessRegNo: e.target.value })}
              onBlur={() => {
                if (isEditing) return;
                const raw = form.businessRegNo.trim();
                if (isStandardBusinessRegNo(raw)) {
                  setForm((prev) => ({ ...prev, businessRegNo: formatBusinessRegNo(raw) }));
                }
              }}
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
        <div className="panel-header-row">
          <h2>거래처 목록</h2>
          <GridExcelExportButton
            fileBaseName="거래처목록"
            disabled={loading}
            rows={companyExportRows}
          />
        </div>
        <div className="search-row">
          <CompanySearchField
            label="거래처"
            selectedCompany={draftCompany}
            onSelect={setDraftCompany}
            clearToken={clearToken}
            placeholder="상호 또는 사업자번호 입력"
          />
          <button type="button" disabled={loading} onClick={() => void onSearch()}>
            조회
          </button>
          <button type="button" className="secondary" disabled={loading} onClick={onResetSearch}>
            초기화
          </button>
        </div>
        {!hasSearched ? (
          <p className="hint-text">조회 버튼을 누르면 목록이 표시됩니다.</p>
        ) : loading ? (
          <p>불러오는 중…</p>
        ) : companies.length === 0 ? (
          <p>등록된 거래처가 없습니다.</p>
        ) : displayedCompanies.length === 0 ? (
          <p>검색 조건에 맞는 거래처가 없습니다.</p>
        ) : (
          <VirtualMasterTable
            rows={displayedCompanies}
            columnCount={6}
            getRowKey={(c) => c.id}
            renderHeader={() => (
              <tr>
                <th className="num">ID</th>
                <th>상호</th>
                <th>사업자번호</th>
                <th>대표자</th>
                <th>역할</th>
                <th>작업</th>
              </tr>
            )}
            renderRow={(c) => (
              <tr className={editingId === c.id ? 'row-editing' : undefined}>
                <td className="num">{formatInteger(c.id)}</td>
                <td>{c.companyName}</td>
                <td>{formatBusinessRegNo(c.businessRegNo)}</td>
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
            )}
          />
        )}
      </section>
    </div>
  );
}
