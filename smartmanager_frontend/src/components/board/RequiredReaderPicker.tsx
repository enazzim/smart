import { useCallback, useEffect, useId, useRef, useState } from 'react';
import { fetchUsers, type User } from '../../api/user';

const SEARCH_DEBOUNCE_MS = 250;

export interface RequiredReaderOption {
  userId: number;
  loginId: string;
  name: string;
}

interface RequiredReaderPickerProps {
  selected: RequiredReaderOption[];
  onChange: (next: RequiredReaderOption[]) => void;
  excludeUserId?: number;
  disabled?: boolean;
}

function matchesQuery(user: User, query: string): boolean {
  const q = query.trim().toLowerCase();
  if (!q) {
    return true;
  }
  return user.name.toLowerCase().includes(q) || user.loginId.toLowerCase().includes(q);
}

export default function RequiredReaderPicker({
  selected,
  onChange,
  excludeUserId,
  disabled = false,
}: RequiredReaderPickerProps) {
  const listId = useId();
  const inputRef = useRef<HTMLInputElement>(null);
  const blurTimer = useRef<ReturnType<typeof setTimeout> | null>(null);
  const [query, setQuery] = useState('');
  const [open, setOpen] = useState(false);
  const [allUsers, setAllUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const loadUsers = useCallback(async (searchQuery: string) => {
    setLoading(true);
    setError(null);
    try {
      const users = await fetchUsers(searchQuery.trim() || undefined);
      setAllUsers(users.filter((user) => user.id !== excludeUserId));
    } catch (e) {
      setError(e instanceof Error ? e.message : '사용자 조회 실패');
      setAllUsers([]);
    } finally {
      setLoading(false);
    }
  }, [excludeUserId]);

  useEffect(() => {
    if (!open) {
      return;
    }
    const timer = setTimeout(() => {
      void loadUsers(query);
    }, query.trim() ? SEARCH_DEBOUNCE_MS : 0);
    return () => clearTimeout(timer);
  }, [query, open, loadUsers]);

  useEffect(() => {
    setAllUsers((prev) => prev.filter((user) => user.id !== excludeUserId));
  }, [excludeUserId]);

  const selectedIds = new Set(selected.map((item) => item.userId));
  const filteredUsers = allUsers.filter((user) => matchesQuery(user, query));

  const toggle = (user: User) => {
    if (selectedIds.has(user.id)) {
      onChange(selected.filter((item) => item.userId !== user.id));
      return;
    }
    onChange([
      ...selected,
      { userId: user.id, loginId: user.loginId, name: user.name },
    ]);
  };

  const remove = (userId: number) => {
    onChange(selected.filter((item) => item.userId !== userId));
  };

  const onFocus = () => {
    if (blurTimer.current) {
      clearTimeout(blurTimer.current);
    }
    if (!disabled) {
      setOpen(true);
    }
  };

  const onBlur = () => {
    blurTimer.current = setTimeout(() => {
      setOpen(false);
    }, 150);
  };

  return (
    <label className="board-required-picker">
      필수 열람 대상
      <span className="board-required-hint">미지정 시 확인 추적 없음 · 클릭 후 선택</span>
      <div className={`board-required-combobox${open ? ' board-required-combobox--open' : ''}`}>
        <div
          className="board-required-input-wrap"
          onMouseDown={(e) => {
            if (disabled) {
              return;
            }
            if ((e.target as HTMLElement).closest('.board-required-chip-remove')) {
              return;
            }
            e.preventDefault();
            inputRef.current?.focus();
          }}
        >
          {selected.map((item) => (
            <span key={item.userId} className="board-required-chip">
              {item.name || '이름 없음'}
              {!disabled && (
                <button
                  type="button"
                  className="board-required-chip-remove"
                  aria-label={`${item.name} 제거`}
                  onMouseDown={(e) => e.preventDefault()}
                  onClick={() => remove(item.userId)}
                >
                  ×
                </button>
              )}
            </span>
          ))}
          <input
            ref={inputRef}
            type="text"
            role="combobox"
            aria-expanded={open}
            aria-controls={listId}
            aria-autocomplete="list"
            className="board-required-search-input"
            value={query}
            placeholder={selected.length === 0 ? '이름 또는 로그인 ID 검색' : '추가 검색…'}
            disabled={disabled}
            onChange={(e) => setQuery(e.target.value)}
            onFocus={onFocus}
            onBlur={onBlur}
            onKeyDown={(e) => {
              if (e.key === 'Escape') {
                setOpen(false);
                inputRef.current?.blur();
              }
            }}
          />
        </div>
        {open && (
          <ul id={listId} className="board-required-dropdown" role="listbox">
            {loading && <li className="board-required-dropdown-empty">불러오는 중…</li>}
            {!loading && filteredUsers.length === 0 && (
              <li className="board-required-dropdown-empty">
                {query.trim() ? '일치하는 사용자가 없습니다.' : '등록된 사용자가 없습니다.'}
              </li>
            )}
            {!loading &&
              filteredUsers.map((user) => {
                const isSelected = selectedIds.has(user.id);
                return (
                  <li key={user.id}>
                    <button
                      type="button"
                      role="option"
                      aria-selected={isSelected}
                      className={`board-required-dropdown-option${isSelected ? ' board-required-dropdown-option--selected' : ''}`}
                      onMouseDown={(e) => e.preventDefault()}
                      onClick={() => toggle(user)}
                    >
                      <span className="board-required-dropdown-label">
                        {user.name || '이름 없음'} ({user.loginId || '—'})
                      </span>
                      {isSelected && <span className="board-required-dropdown-check">✓</span>}
                    </button>
                  </li>
                );
              })}
          </ul>
        )}
      </div>
      {error && <span className="board-required-error">{error}</span>}
    </label>
  );
}
