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
  modelType?: string | null;
  lotTracked?: boolean;
};

export type ItemSearchDisplayMode = 'full' | 'itemNo' | 'itemName';

function formatItemLabel(item: ItemSearchSelection, displayMode: ItemSearchDisplayMode = 'full') {
  if (displayMode === 'itemNo') {
    return item.itemNo;
  }
  if (displayMode === 'itemName') {
    return item.itemName;
  }
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
  // 전체 바인딩 후 입력값으로 클라이언트 필터 (포커스·타이핑 시)
  const results = await fetchItems();
  const trimmed = query.trim();

  return results
    .filter((item) => allowedClasses.has(item.propertyClassification))
    .filter((item) => matchesQuery(item, trimmed))
    .map((item) => ({
      id: item.id,
      itemNo: item.itemNo,
      itemName: item.itemName,
      propertyClassification: item.propertyClassification,
      modelType: item.modelType,
      lotTracked: item.lotTracked,
    }));
}

export interface ItemSearchFieldProps {
  label: string;
  selectedItem: ItemSearchSelection | null;
  onSelect: (item: ItemSearchSelection | null) => void;
  /** 입력 중 텍스트(선택 전 포함 검색용). 선택/입력 변경 시 호출 */
  onQueryTextChange?: (query: string) => void;
  /** 값이 바뀌면 입력란·내부 상태를 비움 (검색 초기화용) */
  clearToken?: number;
  placeholder?: string;
  allowedClassifications?: PropertyClassification[];
  /** 지정 시 API 대신 목록에서만 검색 (판매단가 품목 등) */
  items?: ItemSearchSelection[];
  disabled?: boolean;
  emptyMessage?: string;
  /** 선택값·입력란 표시 형식 (기본: 품번 — 품명) */
  displayMode?: ItemSearchDisplayMode;
}

export default function ItemSearchField({
  label,
  selectedItem,
  onSelect,
  onQueryTextChange,
  clearToken,
  placeholder = '품목번호 또는 품목명 입력',
  allowedClassifications,
  items,
  disabled = false,
  emptyMessage,
  displayMode = 'full',
}: ItemSearchFieldProps) {
  const isLocalMode = items !== undefined;
  const allowedClassesKey = toAllowedClassesKey(allowedClassifications);
  const allowedClasses = useMemo(
    () => new Set<PropertyClassification>(allowedClassifications ?? DEFAULT_ALLOWED_CLASSES),
    [allowedClassesKey],
  );
  const listId = useId();
  const blurTimer = useRef<ReturnType<typeof setTimeout> | null>(null);
  const prevSelectedRef = useRef<ItemSearchSelection | null>(null);
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
    if (clearToken === undefined || clearToken === 0) {
      return;
    }
    setQuery('');
    setOptions([]);
    setOpen(false);
    setSearchError(null);
    prevSelectedRef.current = null;
    onQueryTextChange?.('');
  }, [clearToken]);

  useEffect(() => {
    if (selectedItem) {
      const labelText = formatItemLabel(selectedItem, displayMode);
      setQuery(labelText);
      onQueryTextChange?.(labelText);
    } else if (prevSelectedRef.current) {
      // 입력으로 선택만 해제한 경우는 유지. 선택 라벨이 그대로면 외부 초기화로 비움.
      const prevLabel = formatItemLabel(prevSelectedRef.current, displayMode);
      setQuery((current) => {
        if (current === prevLabel) {
          onQueryTextChange?.('');
          return '';
        }
        return current;
      });
    }
    prevSelectedRef.current = selectedItem;
  }, [selectedItem?.id, selectedItem?.itemNo, selectedItem?.itemName, displayMode]);

  useEffect(() => {
    if (!open || disabled) {
      return;
    }

    const searchQuery =
      query.trim() && (!selectedItem || query !== formatItemLabel(selectedItem, displayMode))
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
  }, [query, open, selectedItem, fetchOptions, isLocalMode, loadLocalOptions, disabled, displayMode]);

  useEffect(() => {
    if (disabled) {
      setOpen(false);
    }
  }, [disabled]);

  const onQueryChange = (value: string) => {
    setQuery(value);
    onQueryTextChange?.(value);
    setOpen(true);
    if (selectedItem && value !== formatItemLabel(selectedItem, displayMode)) {
      onSelect(null);
    }
  };

  const pickItem = (item: ItemSearchSelection) => {
    onSelect(item);
    setQuery(formatItemLabel(item, displayMode));
    setOpen(false);
  };

  const onBlur = () => {
    blurTimer.current = setTimeout(() => {
      setOpen(false);
      const match = options.find(
        (item) =>
          formatItemLabel(item, displayMode) === query.trim() ||
          formatItemLabel(item, 'full') === query.trim(),
      );
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
