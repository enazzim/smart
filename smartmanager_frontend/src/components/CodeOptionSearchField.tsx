import { useCallback, useEffect, useId, useRef, useState } from 'react';

const SEARCH_DEBOUNCE_MS = 300;

export type CodeOptionSelection = {
  id: number;
  code: string;
  name: string;
};

function formatLabel(option: CodeOptionSelection) {
  return `${option.code} — ${option.name}`;
}

function matchesQuery(option: CodeOptionSelection, query: string) {
  const q = query.trim().toLowerCase();
  if (!q) {
    return true;
  }
  return (
    option.code.toLowerCase().includes(q) ||
    option.name.toLowerCase().includes(q) ||
    formatLabel(option).toLowerCase().includes(q)
  );
}

export interface CodeOptionSearchFieldProps {
  label: string;
  options: CodeOptionSelection[];
  selected: CodeOptionSelection | null;
  onSelect: (option: CodeOptionSelection | null) => void;
  clearToken?: number;
  placeholder?: string;
  disabled?: boolean;
  emptyMessage?: string;
}

/**
 * 미리 바인딩된 코드 옵션을 포커스·입력 시 클라이언트 필터하는 콤보.
 */
export default function CodeOptionSearchField({
  label,
  options,
  selected,
  onSelect,
  clearToken,
  placeholder = '코드 또는 명칭 입력',
  disabled = false,
  emptyMessage,
}: CodeOptionSearchFieldProps) {
  const listId = useId();
  const blurTimer = useRef<ReturnType<typeof setTimeout> | null>(null);
  const [query, setQuery] = useState('');
  const [filtered, setFiltered] = useState<CodeOptionSelection[]>([]);
  const [open, setOpen] = useState(false);

  useEffect(() => {
    if (clearToken === undefined || clearToken === 0) {
      return;
    }
    setQuery('');
    setFiltered([]);
    setOpen(false);
  }, [clearToken]);

  useEffect(() => {
    if (selected) {
      setQuery(formatLabel(selected));
    } else {
      setQuery('');
    }
  }, [selected?.id]);

  const applyFilter = useCallback(
    (searchQuery: string) => {
      setFiltered(options.filter((opt) => matchesQuery(opt, searchQuery)));
    },
    [options],
  );

  useEffect(() => {
    if (!open || disabled) {
      return;
    }
    const searchQuery =
      query.trim() && (!selected || query !== formatLabel(selected)) ? query : '';
    const timer = setTimeout(() => {
      applyFilter(searchQuery);
    }, searchQuery ? SEARCH_DEBOUNCE_MS : 0);
    return () => clearTimeout(timer);
  }, [query, open, selected, applyFilter, disabled]);

  const onQueryChange = (value: string) => {
    setQuery(value);
    setOpen(true);
    if (selected && value !== formatLabel(selected)) {
      onSelect(null);
    }
  };

  const pick = (option: CodeOptionSelection) => {
    onSelect(option);
    setQuery(formatLabel(option));
    setOpen(false);
  };

  const onBlur = () => {
    blurTimer.current = setTimeout(() => {
      setOpen(false);
      const match = filtered.find((opt) => formatLabel(opt) === query.trim());
      if (match) {
        pick(match);
      }
    }, 150);
  };

  const onFocus = () => {
    if (blurTimer.current) {
      clearTimeout(blurTimer.current);
    }
    setOpen(true);
  };

  const resolvedEmpty =
    emptyMessage ??
    (query.trim() ? '일치하는 항목이 없습니다.' : '등록된 항목이 없습니다.');

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
      {open && !disabled && (
        <ul id={listId} className="item-combobox-list" role="listbox">
          {filtered.length === 0 && <li className="item-combobox-empty">{resolvedEmpty}</li>}
          {filtered.map((opt) => (
            <li key={opt.id}>
              <button
                type="button"
                role="option"
                className="item-combobox-option"
                onMouseDown={(e) => e.preventDefault()}
                onClick={() => pick(opt)}
              >
                {formatLabel(opt)}
              </button>
            </li>
          ))}
        </ul>
      )}
    </label>
  );
}
