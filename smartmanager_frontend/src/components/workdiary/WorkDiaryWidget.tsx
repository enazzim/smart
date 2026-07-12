import type { WorkDiaryListItem } from '../../api/workDiary';
import { formatWorkDiaryDate } from '../../api/workDiary';
import NewPostBadge from '../board/NewPostBadge';

interface WorkDiaryWidgetProps {
  title: string;
  items: WorkDiaryListItem[];
  loading?: boolean;
  onMore?: () => void;
  onOpenItem?: (id: number) => void;
}

export default function WorkDiaryWidget({
  title,
  items,
  loading = false,
  onMore,
  onOpenItem,
}: WorkDiaryWidgetProps) {
  return (
    <section className="board-widget">
      <header className="board-widget-header">
        <h2>{title}</h2>
        {onMore && (
          <button type="button" className="board-widget-more" onClick={onMore}>
            MORE
          </button>
        )}
      </header>
      {loading ? (
        <p className="board-widget-empty">불러오는 중…</p>
      ) : items.length === 0 ? (
        <p className="board-widget-empty">등록된 업무일지가 없습니다.</p>
      ) : (
        <ul className="board-widget-list">
          {items.map((item) => (
            <li key={item.id}>
              <button
                type="button"
                className="board-widget-item"
                onClick={() => onOpenItem?.(item.id)}
              >
                <span className="board-widget-title">
                  <span className="board-widget-title-text">
                    {item.workDateTitle || `${item.authorName} 업무일지`}
                  </span>
                  <NewPostBadge dateValue={item.workDate} />
                </span>
                <span className="board-widget-date">{formatWorkDiaryDate(item.workDate)}</span>
              </button>
            </li>
          ))}
        </ul>
      )}
    </section>
  );
}
