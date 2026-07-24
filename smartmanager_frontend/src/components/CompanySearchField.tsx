import { useCallback, useEffect, useId, useMemo, useRef, useState } from 'react';
import { fetchCompanies, type CompanyRoleType } from '../api/company';
import { formatBusinessRegNo } from '../utils/businessRegNo';
import { sortByKoreanField } from '../utils/koreanSort';

const SEARCH_DEBOUNCE_MS = 300;

/** 구매·외주 거래처 필터용 — 배열 참조 안정화 */
export const PURCHASE_OUTSOURCE_PARTNER_ROLES: readonly CompanyRoleType[] = ['PURCHASE', 'OUTSOURCE'];

export type CompanySearchSelection = {
  id: number;
  companyName: string;
  businessRegNo: string;
};

function formatCompanyLabel(company: CompanySearchSelection) {
  return `${company.companyName} (${formatBusinessRegNo(company.businessRegNo)})`;
}

function matchesQuery(company: CompanySearchSelection, query: string) {
  const q = query.trim().toLowerCase();
  if (!q) {
    return true;
  }
  return (
    company.companyName.toLowerCase().includes(q) ||
    company.businessRegNo.toLowerCase().includes(q)
  );
}

async function loadCompanies(
  roles: CompanyRoleType | readonly CompanyRoleType[] | undefined,
  query: string,
): Promise<CompanySearchSelection[]> {
  const companies = await fetchCompanies();
  const filteredByRole = (() => {
    if (!roles) {
      return companies;
    }
    const roleList = Array.isArray(roles) ? roles : [roles];
    return companies.filter((company) => roleList.some((role) => company.roles.includes(role)));
  })();

  const trimmed = query.trim();
  return sortByKoreanField(
    filteredByRole
      .map((company) => ({
        id: company.id,
        companyName: company.companyName,
        businessRegNo: company.businessRegNo,
      }))
      .filter((company) => matchesQuery(company, trimmed)),
    (company) => company.companyName,
  );
}

export interface CompanySearchFieldProps {
  label: string;
  partnerType?: CompanyRoleType;
  /** partnerType보다 우선 — 여러 역할 중 하나라도 있으면 표시 */
  partnerTypes?: readonly CompanyRoleType[];
  selectedCompany: CompanySearchSelection | null;
  onSelect: (company: CompanySearchSelection | null) => void;
  /** 값이 바뀌면 입력란·내부 상태를 비움 (검색 초기화용) */
  clearToken?: number;
  placeholder?: string;
}

export default function CompanySearchField({
  label,
  partnerType,
  partnerTypes,
  selectedCompany,
  onSelect,
  clearToken,
  placeholder = '상호 또는 사업자번호 입력',
}: CompanySearchFieldProps) {
  const rolesFilterKey = useMemo(
    () => partnerTypes?.join('|') ?? partnerType ?? '',
    [partnerTypes, partnerType],
  );
  const listId = useId();
  const blurTimer = useRef<ReturnType<typeof setTimeout> | null>(null);
  const [query, setQuery] = useState('');
  const [options, setOptions] = useState<CompanySearchSelection[]>([]);
  const [open, setOpen] = useState(false);
  const [searching, setSearching] = useState(false);
  const [searchError, setSearchError] = useState<string | null>(null);

  const rolesFilter = useMemo(
    () => partnerTypes ?? partnerType,
    [rolesFilterKey, partnerTypes, partnerType],
  );

  const fetchOptions = useCallback(
    async (searchQuery: string) => {
      setSearching(true);
      setSearchError(null);
      try {
        setOptions(await loadCompanies(rolesFilter, searchQuery));
      } catch (e) {
        setSearchError(e instanceof Error ? e.message : '거래처 검색 실패');
        setOptions([]);
      } finally {
        setSearching(false);
      }
    },
    [rolesFilter],
  );

  useEffect(() => {
    if (clearToken === undefined || clearToken === 0) {
      return;
    }
    setQuery('');
    setOptions([]);
    setOpen(false);
    setSearchError(null);
  }, [clearToken]);

  useEffect(() => {
    if (selectedCompany) {
      setQuery(formatCompanyLabel(selectedCompany));
    } else {
      setQuery('');
    }
  }, [selectedCompany?.id]);

  useEffect(() => {
    if (!open) {
      return;
    }

    const searchQuery =
      query.trim() && (!selectedCompany || query !== formatCompanyLabel(selectedCompany))
        ? query
        : '';

    const timer = setTimeout(() => {
      void fetchOptions(searchQuery);
    }, searchQuery ? SEARCH_DEBOUNCE_MS : 0);

    return () => clearTimeout(timer);
  }, [query, open, selectedCompany, fetchOptions]);

  const onQueryChange = (value: string) => {
    setQuery(value);
    setOpen(true);
    if (selectedCompany && value !== formatCompanyLabel(selectedCompany)) {
      onSelect(null);
    }
  };

  const pickCompany = (company: CompanySearchSelection) => {
    onSelect(company);
    setQuery(formatCompanyLabel(company));
    setOpen(false);
  };

  const onBlur = () => {
    blurTimer.current = setTimeout(() => {
      setOpen(false);
      const match = options.find((company) => formatCompanyLabel(company) === query.trim());
      if (match) {
        pickCompany(match);
      }
    }, 150);
  };

  const onFocus = () => {
    if (blurTimer.current) {
      clearTimeout(blurTimer.current);
    }
    setOpen(true);
  };

  return (
    <label className="item-combobox">
      {label}
      <input
        role="combobox"
        aria-expanded={open}
        aria-controls={listId}
        aria-autocomplete="list"
        value={query}
        placeholder={placeholder}
        onChange={(e) => onQueryChange(e.target.value)}
        onFocus={onFocus}
        onBlur={onBlur}
        onKeyDown={(e) => {
          if (e.key === 'Escape') {
            setOpen(false);
          }
        }}
      />
      {open && (
        <ul id={listId} className="item-combobox-list" role="listbox">
          {searching && <li className="item-combobox-empty">검색 중…</li>}
          {!searching && options.length === 0 && (
            <li className="item-combobox-empty">
              {query.trim() ? '일치하는 거래처가 없습니다.' : '등록된 거래처가 없습니다.'}
            </li>
          )}
          {!searching &&
            options.map((company) => (
              <li key={company.id}>
                <button
                  type="button"
                  role="option"
                  className="item-combobox-option"
                  onMouseDown={(e) => e.preventDefault()}
                  onClick={() => pickCompany(company)}
                >
                  {formatCompanyLabel(company)}
                </button>
              </li>
            ))}
        </ul>
      )}
      {searchError && <span className="item-search-hint error-text">{searchError}</span>}
    </label>
  );
}
