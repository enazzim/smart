import type { BoardPostSummary } from '../../api/board';
import { formatBoardDate } from '../../api/board';
import NewPostBadge from './NewPostBadge';

interface BoardWidgetProps {
  title: string;
  items: BoardPostSummary[];
  placeholder?: boolean;
  onMore?: () => void;
  onOpenPost?: (postId: number) => void;
}

export default function BoardWidget({
  title,
  items,
  placeholder = false,
  onMore,
  onOpenPost,
}: BoardWidgetProps) {
  return (
    <section className="board-widget">
      <header className="board-widget-header">
        <h2>{title}</h2>
        {onMore && !placeholder && (
          <button type="button" className="board-widget-more" onClick={onMore}>
            MORE
          </button>
        )}
      </header>
      {placeholder ? (
        <p className="board-widget-empty">업무일지는 추후 연동 예정입니다.</p>
      ) : items.length === 0 ? (
        <p className="board-widget-empty">등록된 글이 없습니다.</p>
      ) : (
        <ul className="board-widget-list">
          {items.map((item) => (
            <li key={item.id}>
              <button
                type="button"
                className="board-widget-item"
                onClick={() => onOpenPost?.(item.id)}
              >
                <span className="board-widget-title">
                  <span className="board-widget-title-text">
                    {item.hasAttachment ? '📎 ' : ''}
                    {item.title || '(제목 없음)'}
                  </span>
                  <NewPostBadge createdAt={item.createdAt} />
                </span>
                <span className="board-widget-date">{formatBoardDate(item.createdAt)}</span>
              </button>
            </li>
          ))}
        </ul>
      )}
    </section>
  );
}
