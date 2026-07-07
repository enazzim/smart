import { useCallback, useEffect, useMemo, useState } from 'react';
import BoardWidget from '../components/board/BoardWidget';
import WorkDiaryWidget from '../components/workdiary/WorkDiaryWidget';
import {
  BOARD_TYPE_LABELS,
  type BoardType,
  type DashboardWidget,
  fetchDashboardWidgets,
} from '../api/board';
import { fetchWorkDiaries, type WorkDiaryListItem } from '../api/workDiary';

const DASHBOARD_SLOTS: Array<{ key: BoardType | 'WORK_DIARY'; title: string }> = [
  { key: 'NOTICE', title: BOARD_TYPE_LABELS.NOTICE },
  { key: 'PRESIDENT_NOTICE', title: BOARD_TYPE_LABELS.PRESIDENT_NOTICE },
  { key: 'WORK_DIARY', title: '업무일지' },
  { key: 'PRODUCT', title: BOARD_TYPE_LABELS.PRODUCT },
  { key: 'LASER', title: BOARD_TYPE_LABELS.LASER },
  { key: 'INSTITUTE', title: BOARD_TYPE_LABELS.INSTITUTE },
  { key: 'SALES_QC', title: BOARD_TYPE_LABELS.SALES_QC },
];

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

interface DashboardPageProps {
  onOpenBoardList: (boardType: BoardType) => void;
  onOpenBoardPost: (boardType: BoardType, postId: number) => void;
  onOpenWorkDiaryList: () => void;
  onOpenWorkDiaryDetail: (id: number) => void;
}

export default function DashboardPage({
  onOpenBoardList,
  onOpenBoardPost,
  onOpenWorkDiaryList,
  onOpenWorkDiaryDetail,
}: DashboardPageProps) {
  const [widgets, setWidgets] = useState<DashboardWidget[]>([]);
  const [workDiaryItems, setWorkDiaryItems] = useState<WorkDiaryListItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [workDiaryLoading, setWorkDiaryLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [workDiaryError, setWorkDiaryError] = useState<string | null>(null);

  const load = useCallback(async () => {
    setLoading(true);
    setWorkDiaryLoading(true);
    setError(null);
    setWorkDiaryError(null);
    try {
      const rows = await fetchDashboardWidgets(5);
      setWidgets(rows);
    } catch (e) {
      setError(e instanceof Error ? e.message : '대시보드 조회 실패');
    } finally {
      setLoading(false);
    }
    try {
      const page = await fetchWorkDiaries({
        fromDate: addDaysIso(todayIso(), -30),
        toDate: todayIso(),
        page: 0,
        size: 5,
      });
      setWorkDiaryItems(page.items);
    } catch (e) {
      setWorkDiaryError(e instanceof Error ? e.message : '업무일지 조회 실패');
    } finally {
      setWorkDiaryLoading(false);
    }
  }, []);

  useEffect(() => {
    void load();
  }, [load]);

  const widgetMap = useMemo(() => {
    const map = new Map<BoardType, DashboardWidget>();
    widgets.forEach((widget) => map.set(widget.boardType, widget));
    return map;
  }, [widgets]);

  return (
    <div className="page dashboard-page">
      <header className="page-header">
        <div>
          <h1>대시보드</h1>
          <p>공지·게시판·업무일지 최근 글을 확인합니다.</p>
        </div>
        <button type="button" className="secondary" onClick={() => void load()} disabled={loading || workDiaryLoading}>
          새로고침
        </button>
      </header>

      {error && <p className="error-banner">{error}</p>}
      {workDiaryError && <p className="error-banner">{workDiaryError}</p>}

      <div className="dashboard-grid">
        {DASHBOARD_SLOTS.map((slot) => {
          if (slot.key === 'WORK_DIARY') {
            return (
              <WorkDiaryWidget
                key={slot.key}
                title={slot.title}
                items={workDiaryItems}
                loading={workDiaryLoading}
                onMore={onOpenWorkDiaryList}
                onOpenItem={onOpenWorkDiaryDetail}
              />
            );
          }
          const boardType = slot.key;
          const widget = widgetMap.get(boardType);
          return (
            <BoardWidget
              key={slot.key}
              title={slot.title}
              items={widget?.items ?? []}
              onMore={() => onOpenBoardList(boardType)}
              onOpenPost={(postId) => onOpenBoardPost(boardType, postId)}
            />
          );
        })}
      </div>
    </div>
  );
}
