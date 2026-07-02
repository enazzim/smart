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

async function handleResponse<T>(response: Response): Promise<T> {
  if (!response.ok) {
    const body = await response.json().catch(() => ({ message: response.statusText }));
    throw new Error(body.message ?? '요청에 실패했습니다.');
  }
  if (response.status === 204) {
    return undefined as T;
  }
  return response.json() as Promise<T>;
}

export async function fetchCompanies(): Promise<Company[]> {
  return handleResponse<Company[]>(await fetch(API_BASE));
}

export async function fetchCompany(id: number): Promise<Company> {
  return handleResponse<Company>(await fetch(`${API_BASE}/${id}`));
}

export async function createCompany(payload: CreateCompanyRequest): Promise<Company> {
  return handleResponse<Company>(
    await fetch(API_BASE, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function updateCompany(id: number, payload: UpdateCompanyRequest): Promise<Company> {
  return handleResponse<Company>(
    await fetch(`${API_BASE}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    }),
  );
}

export async function deleteCompany(id: number): Promise<void> {
  await handleResponse<void>(
    await fetch(`${API_BASE}/${id}`, {
      method: 'DELETE',
    }),
  );
}
