import { useCallback, useEffect, useId, useRef, useState } from 'react';
import { fetchUsers, type User } from '../api/user';

const SEARCH_DEBOUNCE_MS = 250;

export interface WorkerSearchFieldProps {
  label: string;
  value: string;
  onChange: (name: string) => void;
  disabled?: boolean;
  placeholder?: string;
}

function matchesQuery(user: User, query: string): boolean {
  const q = query.trim().toLowerCase();
  if (!q) {
    return true;
  }
  return user.name.toLowerCase().includes(q) || user.loginId.toLowerCase().includes(q);
}

export default function WorkerSearchField({
  label,
  value,
  onChange,
  disabled = false,
  placeholder = '이름 또는 로그인 ID 입력',
}: WorkerSearchFieldProps) {
  const listId = useId();
  const blurTimer = useRef<ReturnType<typeof setTimeout> | null>(null);
  const [query, setQuery] = useState(value);
  const [options, setOptions] = useState<User[]>([]);
  const [open, setOpen] = useState(false);
  const [searching, setSearching] = useState(false);
  const [searchError, setSearchError] = useState<string | null>(null);

  useEffect(() => {
    setQuery(value);
  }, [value]);

  const loadOptions = useCallback(async (searchQuery: string) => {
    setSearching(true);
    setSearchError(null);
    try {
      const users = await fetchUsers(searchQuery);
      setOptions(users.filter((user) => matchesQuery(user, searchQuery)));
    } catch (e) {
      setSearchError(e instanceof Error ? e.message : '작업자 검색 실패');
      setOptions([]);
    } finally {
      setSearching(false);
    }
  }, []);

  useEffect(() => {
    if (!open) {
      return;
    }
    const timer = setTimeout(() => {
      void loadOptions(query);
    }, query.trim() ? SEARCH_DEBOUNCE_MS : 0);
    return () => clearTimeout(timer);
  }, [query, open, loadOptions]);

  const onQueryChange = (next: string) => {
    setQuery(next);
    onChange(next);
    setOpen(true);
  };

  const pickUser = (user: User) => {
    onChange(user.name);
    setQuery(user.name);
    setOpen(false);
  };

  const onBlur = () => {
    blurTimer.current = setTimeout(() => {
      setOpen(false);
      const match = options.find((user) => user.name === query.trim());
      if (match) {
        pickUser(match);
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
        disabled={disabled}
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
              {query.trim() ? '일치하는 작업자가 없습니다.' : '등록된 사용자가 없습니다.'}
            </li>
          )}
          {!searching &&
            options.map((user) => (
              <li key={user.id}>
                <button
                  type="button"
                  role="option"
                  className="item-combobox-option"
                  onMouseDown={(e) => e.preventDefault()}
                  onClick={() => pickUser(user)}
                >
                  {user.name} ({user.loginId})
                </button>
              </li>
            ))}
        </ul>
      )}
      {searchError && <span className="item-search-hint error-text">{searchError}</span>}
    </label>
  );
}
