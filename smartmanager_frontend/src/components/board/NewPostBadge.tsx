import { isRegisteredToday } from '../../api/board';

interface NewPostBadgeProps {
  createdAt?: string | null;
  /** YYYY-MM-DD 또는 ISO datetime */
  dateValue?: string | null;
}

/** 당일 등록 글에 표시하는 새글(N) 아이콘 */
export default function NewPostBadge({ createdAt, dateValue }: NewPostBadgeProps) {
  const value = createdAt ?? dateValue;
  if (!isRegisteredToday(value)) {
    return null;
  }
  return (
    <span className="board-new-badge" title="오늘 등록" aria-label="새글">
      N
    </span>
  );
}
