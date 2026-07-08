import { useCallback, useEffect, useId, useMemo, useRef, useState } from 'react';
import { fetchItems } from '../api/item';
import type { PropertyClassification } from '../api/item';

const DEFAULT_ALLOWED_CLASSES: PropertyClassification[] = ['제품', '공정품'];
const SEARCH_DEBOUNCE_MS = 300;

function toAllowedClassesKey(classes?: PropertyClassification[]): string {
  return (classes ?? DEFAULT_ALLOWED_CLASSES).join('\0');
}

export type ItemSearchSelection = {
  id: number;
  itemNo: string;
  itemName: string;
  propertyClassification?: PropertyClassification;
};

function formatItemLabel(item: ItemSearchSelection) {
  const base = `${item.itemNo} — ${item.itemName}`;
  return item.propertyClassification ? `${base} (${item.propertyClassification})` : base;
}

function matchesQuery(item: ItemSearchSelection, query: string) {
  const q = query.trim().toLowerCase();
  if (!q) {
    return true;
  }
  const label = formatItemLabel(item).toLowerCase();
  return (
    item.itemNo.toLowerCase().includes(q) ||
    item.itemName.toLowerCase().includes(q) ||
    label.includes(q)
  );
}

async function loadProductItems(
  query: string,
  allowedClasses: Set<PropertyClassification>,
): Promise<ItemSearchSelection[]> {
  const trimmed = query.trim();

  let results = trimmed
    ? await fetchItems(trimmed, undefined)
    : await fetchItems();

  if (trimmed && results.length === 0) {
    results = await fetchItems(undefined, trimmed);
  }

  return results
    .filter((item) => allowedClasses.has(item.propertyClassification))
    .filter((item) => matchesQuery(item, trimmed))
    .map((item) => ({
      id: item.id,
      itemNo: item.itemNo,
      itemName: item.itemName,
      propertyClassification: item.propertyClassification,
    }));
}

export interface ItemSearchFieldProps {
  label: string;
  selectedItem: ItemSearchSelection | null;
  onSelect: (item: ItemSearchSelection | null) => void;
  placeholder?: string;
  allowedClassifications?: PropertyClassification[];
  /** 지정 시 API 대신 목록에서만 검색 (판매단가 품목 등) */
  items?: ItemSearchSelection[];
  disabled?: boolean;
  emptyMessage?: string;
}

export default function ItemSearchField({
  label,
  selectedItem,
  onSelect,
  placeholder = '품목번호 또는 품목명 입력',
  allowedClassifications,
  items,
  disabled = false,
  emptyMessage,
}: ItemSearchFieldProps) {
  const isLocalMode = items !== undefined;
  const allowedClassesKey = toAllowedClassesKey(allowedClassifications);
  const allowedClasses = useMemo(
    () => new Set<PropertyClassification>(allowedClassifications ?? DEFAULT_ALLOWED_CLASSES),
    [allowedClassesKey],
  );
  const listId = useId();
  const blurTimer = useRef<ReturnType<typeof setTimeout> | null>(null);
  const [query, setQuery] = useState('');
  const [options, setOptions] = useState<ItemSearchSelection[]>([]);
  const [open, setOpen] = useState(false);
  const [searching, setSearching] = useState(false);
  const [searchError, setSearchError] = useState<string | null>(null);

  const loadLocalOptions = useCallback(
    (searchQuery: string) => items!.filter((item) => matchesQuery(item, searchQuery)),
    [items],
  );

  const fetchOptions = useCallback(async (searchQuery: string) => {
    setSearching(true);
    setSearchError(null);
    try {
      setOptions(await loadProductItems(searchQuery, allowedClasses));
    } catch (e) {
      setSearchError(e instanceof Error ? e.message : '품목 검색 실패');
      setOptions([]);
    } finally {
      setSearching(false);
    }
  }, [allowedClassesKey]);

  useEffect(() => {
    if (selectedItem) {
      setQuery(formatItemLabel(selectedItem));
    } else {
      setQuery('');
    }
  }, [selectedItem?.id, selectedItem?.itemNo, selectedItem?.itemName]);

  useEffect(() => {
    if (!open || disabled) {
      return;
    }

    const searchQuery =
      query.trim() && (!selectedItem || query !== formatItemLabel(selectedItem))
        ? query
        : '';

    if (isLocalMode) {
      setOptions(loadLocalOptions(searchQuery));
      return;
    }

    const timer = setTimeout(() => {
      void fetchOptions(searchQuery);
    }, searchQuery ? SEARCH_DEBOUNCE_MS : 0);

    return () => clearTimeout(timer);
  }, [query, open, selectedItem, fetchOptions, isLocalMode, loadLocalOptions, disabled]);

  useEffect(() => {
    if (disabled) {
      setOpen(false);
    }
  }, [disabled]);

  const onQueryChange = (value: string) => {
    setQuery(value);
    setOpen(true);
    if (selectedItem && value !== formatItemLabel(selectedItem)) {
      onSelect(null);
    }
  };

  const pickItem = (item: ItemSearchSelection) => {
    onSelect(item);
    setQuery(formatItemLabel(item));
    setOpen(false);
  };

  const onBlur = () => {
    blurTimer.current = setTimeout(() => {
      setOpen(false);
      const match = options.find((item) => formatItemLabel(item) === query.trim());
      if (match) {
        pickItem(match);
      }
    }, 150);
  };

  const onFocus = () => {
    if (blurTimer.current) {
      clearTimeout(blurTimer.current);
    }
    setOpen(true);
  };

  const resolvedPlaceholder =
    isLocalMode && items!.length === 0
      ? '단가 품목 없음'
      : placeholder;

  const resolvedEmptyMessage =
    emptyMessage ??
    (isLocalMode
      ? query.trim()
        ? '일치하는 품목이 없습니다.'
        : '단가 품목이 없습니다.'
      : query.trim()
        ? '일치하는 품목이 없습니다.'
        : '등록된 품목이 없습니다.');

  return (
    <label className="item-combobox">
      {label}
      <input
        role="combobox"
        aria-expanded={open}
        aria-controls={listId}
        aria-autocomplete="list"
        value={query}
        placeholder={resolvedPlaceholder}
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
          {!isLocalMode && searching && <li className="item-combobox-empty">검색 중…</li>}
          {(!searching || isLocalMode) && options.length === 0 && (
            <li className="item-combobox-empty">{resolvedEmptyMessage}</li>
          )}
          {(!searching || isLocalMode) &&
            options.map((item) => (
              <li key={item.id}>
                <button
                  type="button"
                  role="option"
                  className="item-combobox-option"
                  onMouseDown={(e) => e.preventDefault()}
                  onClick={() => pickItem(item)}
                >
                  {formatItemLabel(item)}
                </button>
              </li>
            ))}
        </ul>
      )}
      {searchError && <span className="item-search-hint error-text">{searchError}</span>}
    </label>
  );
}
