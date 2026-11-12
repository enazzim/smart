import { apiFetch, handleResponse } from './http';

export type CompanyRoleType = 'SALES' | 'OUTSOURCE' | 'PURCHASE' | 'COST';

export interface Company {
  id: number;
  companyName: string;
  presidentName: string;
  businessRegNo: string;
  corporationRegNo?: string | null;
  businessAddress: string;
  homepageUrl?: string | null;
  businessType?: string | null;
  businessItem?: string | null;
  telephone?: string | null;
  fax?: string | null;
  saleStandardDay?: number | null;
  billApprovalStandard?: number | null;
  fixCollectDay1?: number | null;
  contactName?: string | null;
  contactEmail?: string | null;
  roles: CompanyRoleType[];
  createdAt: string;
}

export interface CreateCompanyRequest {
  companyName: string;
  presidentName: string;
  businessRegNo: string;
  corporationRegNo?: string;
  businessAddress: string;
  homepageUrl?: string;
  businessType?: string;
  businessItem?: string;
  telephone?: string;
  fax?: string;
  saleStandardDay?: number;
  billApprovalStandard?: number;
  fixCollectDay1?: number;
  contactName?: string;
  contactEmail?: string;
  roles: CompanyRoleType[];
}

export type UpdateCompanyRequest = Omit<CreateCompanyRequest, 'businessRegNo'>;

const API_BASE = '/api/v1/basis/companies';

export async function fetchCompanies(): Promise<Company[]> {
  return handleResponse<Company[]>(await apiFetch(API_BASE));
}

export interface CompanyLookup {
  id: number;
  companyName: string;
  businessRegNo: string;
}

export async function fetchCompanyLookup(params: {
  q: string;
  roles?: CompanyRoleType | readonly CompanyRoleType[];
  limit?: number;
}): Promise<CompanyLookup[]> {
  const search = new URLSearchParams();
  search.set('q', params.q);
  if (params.roles) {
    const roles = Array.isArray(params.roles) ? params.roles : [params.roles];
    if (roles.length > 0) {
      search.set('roles', roles.join(','));
    }
  }
  if (params.limit != null) {
    search.set('limit', String(params.limit));
  }
  return handleResponse<CompanyLookup[]>(await apiFetch(`${API_BASE}/lookup?${search.toString()}`));
}

export async function fetchCompany(id: number): Promise<Company> {
  return handleResponse<Company>(await apiFetch(`${API_BASE}/${id}`));
}

export async function createCompany(payload: CreateCompanyRequest): Promise<Company> {
  return handleResponse<Company>(
    await apiFetch(API_BASE, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateCompany(id: number, payload: UpdateCompanyRequest): Promise<Company> {
  return handleResponse<Company>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteCompany(id: number): Promise<void> {
  await handleResponse<void>(
    await apiFetch(`${API_BASE}/${id}`, {
      method: 'DELETE',
    }),
  );
}
