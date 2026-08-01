import { Fragment, useRef, type ReactNode } from 'react';
import { useVirtualizer } from '@tanstack/react-virtual';

/** 기준정보 목록 — 약 10행 보이는 뷰포트 기준 행 높이 */
export const MASTER_LIST_ROW_HEIGHT = 42;
export const MASTER_LIST_HEADER_HEIGHT = 40;
export const MASTER_LIST_VISIBLE_ROWS = 10;

type VirtualMasterTableProps<T> = {
  rows: T[];
  columnCount: number;
  getRowKey: (row: T, index: number) => string | number;
  renderHeader: () => ReactNode;
  renderRow: (row: T, index: number) => ReactNode;
  className?: string;
  visibleRowCount?: number;
  rowHeight?: number;
  headerHeight?: number;
};

/**
 * 기준정보 하단 목록용 가상 스크롤 테이블.
 * 뷰포트 높이는 약 {@link MASTER_LIST_VISIBLE_ROWS}행 + 헤더.
 */
export default function VirtualMasterTable<T>({
  rows,
  columnCount,
  getRowKey,
  renderHeader,
  renderRow,
  className,
  visibleRowCount = MASTER_LIST_VISIBLE_ROWS,
  rowHeight = MASTER_LIST_ROW_HEIGHT,
  headerHeight = MASTER_LIST_HEADER_HEIGHT,
}: VirtualMasterTableProps<T>) {
  const parentRef = useRef<HTMLDivElement>(null);
  const virtualizer = useVirtualizer({
    count: rows.length,
    getScrollElement: () => parentRef.current,
    estimateSize: () => rowHeight,
    overscan: 8,
  });

  const maxHeight = headerHeight + rowHeight * visibleRowCount;
  const items = virtualizer.getVirtualItems();
  const paddingTop = items.length > 0 ? items[0]!.start : 0;
  const paddingBottom =
    items.length > 0 ? virtualizer.getTotalSize() - items[items.length - 1]!.end : 0;

  return (
    <div
      ref={parentRef}
      className={['table-wrap', 'table-wrap--virtual', className].filter(Boolean).join(' ')}
      style={{ maxHeight }}
    >
      <table>
        <thead>{renderHeader()}</thead>
        <tbody>
          {paddingTop > 0 && (
            <tr aria-hidden="true" className="virtual-spacer-row">
              <td colSpan={columnCount} style={{ height: paddingTop }} />
            </tr>
          )}
          {items.map((item) => {
            const row = rows[item.index]!;
            return (
              <Fragment key={getRowKey(row, item.index)}>{renderRow(row, item.index)}</Fragment>
            );
          })}
          {paddingBottom > 0 && (
            <tr aria-hidden="true" className="virtual-spacer-row">
              <td colSpan={columnCount} style={{ height: paddingBottom }} />
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
}
