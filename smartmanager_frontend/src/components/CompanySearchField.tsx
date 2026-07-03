import { useCallback, useEffect, useId, useRef, useState } from 'react';
import { fetchCompanies, type CompanyRoleType } from '../api/company';

const SEARCH_DEBOUNCE_MS = 300;

export type CompanySearchSelection = {
  id: number;
  companyName: string;
  businessRegNo: string;
};

function formatCompanyLabel(company: CompanySearchSelection) {
  return `${company.companyName} (${company.businessRegNo})`;
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

async function loadCompanies(role: CompanyRoleType | undefined, query: string): Promise<CompanySearchSelection[]> {
  const companies = await fetchCompanies();
  const filteredByRole = role
    ? companies.filter((company) => company.roles.includes(role))
    : companies;

  const trimmed = query.trim();
  return filteredByRole
    .map((company) => ({
      id: company.id,
      companyName: company.companyName,
      businessRegNo: company.businessRegNo,
    }))
    .filter((company) => matchesQuery(company, trimmed));
}

export interface CompanySearchFieldProps {
  label: string;
  partnerType?: CompanyRoleType;
  selectedCompany: CompanySearchSelection | null;
  onSelect: (company: CompanySearchSelection | null) => void;
  placeholder?: string;
}

export default function CompanySearchField({
  label,
  partnerType,
  selectedCompany,
  onSelect,
  placeholder = '상호 또는 사업자번호 입력',
}: CompanySearchFieldProps) {
  const listId = useId();
  const blurTimer = useRef<ReturnType<typeof setTimeout> | null>(null);
  const [query, setQuery] = useState('');
  const [options, setOptions] = useState<CompanySearchSelection[]>([]);
  const [open, setOpen] = useState(false);
  const [searching, setSearching] = useState(false);
  const [searchError, setSearchError] = useState<string | null>(null);

  const fetchOptions = useCallback(
    async (searchQuery: string) => {
      setSearching(true);
      setSearchError(null);
      try {
        setOptions(await loadCompanies(partnerType, searchQuery));
      } catch (e) {
        setSearchError(e instanceof Error ? e.message : '거래처 검색 실패');
        setOptions([]);
      } finally {
        setSearching(false);
      }
    },
    [partnerType],
  );

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
