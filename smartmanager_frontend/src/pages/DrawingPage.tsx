import { useEffect, useMemo, useState } from 'react';
import { QueryClient, QueryClientProvider, useQuery, useQueryClient } from '@tanstack/react-query';
import { Plus, Edit, Eye, Trash2, RotateCcw, FileUp, Download } from 'lucide-react';
import {
  deleteDrawing,
  fetchDeletedDrawings,
  fetchDrawingReferenceIntegrity,
  fetchDrawings,
  hardDeleteDrawing,
  restoreDrawing,
  type DrawingListItem,
  type DrawingReferenceIntegrityIssue,
  type DrawingType,
} from '../api/drawing';
import DrawingUploadModal from '../components/drawing/DrawingUploadModal';
import DrawingReviseModal from '../components/drawing/DrawingReviseModal';
import DrawingViewerModal from '../components/drawing/DrawingViewerModal';
import DrawingInfoEditModal from '../components/drawing/DrawingInfoEditModal';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import { listCachedDrawingPartNos, syncDailyDrawings } from '../services/drawingOfflineCacheService';
import { useBannerMessages } from '../hooks/useBannerMessages';

const drawingQueryClient = new QueryClient({
  defaultOptions: {
    queries: { retry: 1, refetchOnWindowFocus: false },
  },
});

type DrawingTab = 'dev' | 'prod' | 'deleted';

interface DrawingPageProps {
  readOnly?: boolean;
  canHardDelete?: boolean;
  actorUserId?: string;
}

function DrawingDashboard({ readOnly = false, canHardDelete = false, actorUserId }: DrawingPageProps) {
  const queryClient = useQueryClient();
  const { message, error, showSuccess, showError } = useBannerMessages();

  const [tab, setTab] = useState<DrawingTab>('dev');
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [viewingDrawing, setViewingDrawing] = useState<{
    id: string;
    partNo: string;
    type: DrawingType;
  } | null>(null);
  const [revisingDrawing, setRevisingDrawing] = useState<{
    partNo: string;
    majorVersion: number;
    minorVersion: number;
  } | null>(null);
  const [editingDrawing, setEditingDrawing] = useState<{
    id: string;
    partNo: string;
    partName: string;
    modelType: string;
    itemId?: number | null;
    itemNo?: string | null;
  } | null>(null);

  const [searchPartNo, setSearchPartNo] = useState('');
  const [searchModelType, setSearchModelType] = useState('');
  const [searchDate, setSearchDate] = useState('');
  const [searchItem, setSearchItem] = useState<ItemSearchSelection | null>(null);
  const [isSyncingOffline, setIsSyncingOffline] = useState(false);
  const [cachedPartNos, setCachedPartNos] = useState<string[]>([]);
  const [offlineMessage, setOfflineMessage] = useState<string | null>(null);
  const [integrityOpen, setIntegrityOpen] = useState(false);
  const [integrityLoading, setIntegrityLoading] = useState(false);
  const [integrityIssues, setIntegrityIssues] = useState<DrawingReferenceIntegrityIssue[]>([]);

  const { data: drawings = [], isLoading, isError } = useQuery({
    queryKey: ['drawings'],
    queryFn: fetchDrawings,
  });

  const { data: deletedDrawings = [] } = useQuery({
    queryKey: ['deletedDrawings'],
    queryFn: fetchDeletedDrawings,
  });

  useEffect(() => {
    void listCachedDrawingPartNos().then(setCachedPartNos).catch(() => setCachedPartNos([]));
  }, [drawings, deletedDrawings]);

  const refreshLists = async () => {
    await Promise.all([
      queryClient.refetchQueries({ queryKey: ['drawings'] }),
      queryClient.refetchQueries({ queryKey: ['deletedDrawings'] }),
    ]);
  };

  const moveDrawingToActive = (row: DrawingListItem) => {
    queryClient.setQueryData<DrawingListItem[]>(['drawings'], (prev) => {
      const base = prev ?? [];
      return base.some((item) => item.id === row.id) ? base : [...base, row];
    });
    queryClient.setQueryData<DrawingListItem[]>(['deletedDrawings'], (prev) =>
      (prev ?? []).filter((item) => item.id !== row.id),
    );
  };

  const moveDrawingToDeleted = (row: DrawingListItem) => {
    queryClient.setQueryData<DrawingListItem[]>(['drawings'], (prev) =>
      (prev ?? []).filter((item) => item.partNo !== row.partNo),
    );
    queryClient.setQueryData<DrawingListItem[]>(['deletedDrawings'], (prev) => {
      const base = prev ?? [];
      return base.some((item) => item.id === row.id) ? base : [...base, row];
    });
  };

  const removeDeletedDrawing = (id: string) => {
    queryClient.setQueryData<DrawingListItem[]>(['deletedDrawings'], (prev) =>
      (prev ?? []).filter((item) => item.id !== id),
    );
  };

  const handleDelete = async (partNo: string) => {
    if (!window.confirm(`[${partNo}] 도면을 정말 삭제하시겠습니까?\n(논리 삭제 처리되며 삭제 내역에서 확인 가능합니다)`)) {
      return;
    }
    try {
      await deleteDrawing(partNo, actorUserId);
      const deleted = drawings.find((row) => row.partNo === partNo);
      if (deleted) {
        moveDrawingToDeleted(deleted);
      }
      showSuccess('삭제되었습니다.');
      await refreshLists();
    } catch (err) {
      showError(err instanceof Error ? err.message : '삭제에 실패했습니다.');
    }
  };

  const handleRestore = async (id: string, partNo: string) => {
    if (!window.confirm(`[${partNo}] 도면을 다시 복구하시겠습니까?`)) {
      return;
    }
    try {
      await restoreDrawing(id, actorUserId);
      const restored = deletedDrawings.find((row) => row.id === id);
      if (restored) {
        moveDrawingToActive(restored);
      }
      showSuccess('도면이 복구되었습니다.');
      await refreshLists();
    } catch (err) {
      showError(err instanceof Error ? err.message : '복구에 실패했습니다.');
    }
  };

  const handleHardDelete = async (id: string, partNo: string) => {
    if (
      !window.confirm(
        `[${partNo}] 도면을 정말 영구 삭제하시겠습니까?\n관련된 모든 이력과 PDF 파일이 물리적으로 삭제되며 절대 복구할 수 없습니다.`,
      )
    ) {
      return;
    }
    try {
      await hardDeleteDrawing(id, actorUserId);
      removeDeletedDrawing(id);
      showSuccess('도면이 영구 삭제되었습니다.');
      await refreshLists();
    } catch (err) {
      showError(err instanceof Error ? err.message : '영구 삭제에 실패했습니다.');
    }
  };

  const currentData = tab === 'deleted' ? deletedDrawings : drawings;
  const filteredDrawings = useMemo(
    () =>
      currentData.filter((row: DrawingListItem) => {
        const matchType =
          tab === 'dev' ? row.drawingType === 'DEV' : tab === 'prod' ? row.drawingType === 'PROD' : true;
        const matchPartNo = row.partNo.toLowerCase().includes(searchPartNo.toLowerCase());
        const matchModelType = row.modelType.toLowerCase().includes(searchModelType.toLowerCase());
        const matchDate = searchDate ? row.updatedAt.startsWith(searchDate) : true;
        const matchItem = searchItem
          ? row.itemNo?.toLowerCase() === searchItem.itemNo.toLowerCase() || row.itemId === searchItem.id
          : true;
        return matchType && matchPartNo && matchModelType && matchDate && matchItem;
      }),
    [currentData, tab, searchPartNo, searchModelType, searchDate, searchItem],
  );

  const handleIntegrityScan = async () => {
    setIntegrityLoading(true);
    setIntegrityOpen(true);
    try {
      const issues = await fetchDrawingReferenceIntegrity();
      setIntegrityIssues(issues);
      if (issues.length === 0) {
        showSuccess('구성 참조 정합 검사: 문제 없음');
      }
    } catch (err) {
      setIntegrityIssues([]);
      showError(err instanceof Error ? err.message : '정합 검사에 실패했습니다.');
      setIntegrityOpen(false);
    } finally {
      setIntegrityLoading(false);
    }
  };

  const handleOfflineSync = async () => {
    if (tab === 'deleted') {
      return;
    }
    const partNos = filteredDrawings.map((row) => row.partNo);
    if (partNos.length === 0) {
      showError('동기화할 도면이 없습니다.');
      return;
    }
    setIsSyncingOffline(true);
    setOfflineMessage(null);
    try {
      const result = await syncDailyDrawings(partNos);
      const cached = await listCachedDrawingPartNos();
      setCachedPartNos(cached);
      setOfflineMessage(`오프라인 캐시 완료: ${result.success}건 성공, ${result.failed}건 실패`);
    } catch {
      setOfflineMessage('오프라인 동기화 중 오류가 발생했습니다.');
    } finally {
      setIsSyncingOffline(false);
    }
  };

  if (isLoading) {
    return (
      <div className="page drawing-page">
        <p>불러오는 중…</p>
      </div>
    );
  }

  if (isError) {
    return (
      <div className="page drawing-page">
        <p className="error-banner">데이터를 불러오지 못했습니다.</p>
      </div>
    );
  }

  return (
    <div className="page drawing-page">
      <header className="page-header">
        <div>
          <h1>도면 관리 현황</h1>
          <p>개발·양산 도면 등록, 개정, PDF 열람 및 품목 연동을 관리합니다.</p>
        </div>
        <div className="inline-actions">
          <button
            type="button"
            className="secondary"
            disabled={integrityLoading}
            onClick={() => void handleIntegrityScan()}
          >
            {integrityLoading ? '정합 검사 중…' : '구성 정합 검사'}
          </button>
          {!readOnly && (
            <>
              <button
                type="button"
                className="secondary"
                disabled={isSyncingOffline || tab === 'deleted' || filteredDrawings.length === 0}
                title={tab === 'deleted' ? '삭제된 도면은 오프라인 동기화 대상이 아닙니다.' : undefined}
                onClick={() => void handleOfflineSync()}
              >
                <Download size={16} style={{ marginRight: 6, verticalAlign: 'middle' }} />
                {isSyncingOffline ? '동기화 중…' : '오늘 작업 도면 동기화'}
              </button>
              <button type="button" onClick={() => setIsModalOpen(true)}>
                <Plus size={16} style={{ marginRight: 6, verticalAlign: 'middle' }} />
                도면 등록
              </button>
            </>
          )}
        </div>
      </header>

      {message && <p className="success-banner">{message}</p>}
      {error && <p className="error-banner">{error}</p>}
      {offlineMessage && (
        <p className="hint">
          {offlineMessage}
          {cachedPartNos.length > 0 ? ` (캐시 ${cachedPartNos.length}건)` : ''}
        </p>
      )}

      <section className="panel drawing-filter-panel">
        <div className="drawing-filter-grid">
          <div>
            <ItemSearchField
              label="품목 검색"
              selectedItem={searchItem}
              onSelect={setSearchItem}
              placeholder="연결 품목으로 필터"
            />
          </div>
          <label>
            <span>품번 검색</span>
            <input placeholder="예: A-1" value={searchPartNo} onChange={(e) => setSearchPartNo(e.target.value)} />
          </label>
          <label>
            <span>기종 검색</span>
            <input placeholder="예: 로더" value={searchModelType} onChange={(e) => setSearchModelType(e.target.value)} />
          </label>
          <label>
            <span>등록일 검색</span>
            <input type="date" value={searchDate} onChange={(e) => setSearchDate(e.target.value)} />
          </label>
        </div>
      </section>

      <div className="tab-row">
        <button type="button" className={tab === 'dev' ? 'tab-active' : undefined} onClick={() => setTab('dev')}>
          개발품 도면
        </button>
        <button type="button" className={tab === 'prod' ? 'tab-active' : undefined} onClick={() => setTab('prod')}>
          양산품 도면
        </button>
        <button
          type="button"
          className={tab === 'deleted' ? 'tab-active drawing-tab-danger' : 'drawing-tab-danger'}
          onClick={() => setTab('deleted')}
        >
          삭제 내역 (휴지통)
        </button>
      </div>

      {integrityOpen && (
        <div className="modal-backdrop" role="presentation" onClick={() => setIntegrityOpen(false)}>
          <div className="modal" role="dialog" onClick={(e) => e.stopPropagation()} style={{ maxWidth: 720 }}>
            <h2>구성 참조 정합 검사</h2>
            {integrityLoading ? (
              <p>검사 중…</p>
            ) : integrityIssues.length === 0 ? (
              <p className="hint">문제가 발견되지 않았습니다.</p>
            ) : (
              <ul className="drawing-integrity-list">
                {integrityIssues.map((issue, index) => (
                  <li key={`${issue.code}-${issue.parentHistoryId}-${issue.childHistoryId ?? index}`}>
                    <strong>[{issue.code}]</strong> {issue.message}
                    <br />
                    <span className="hint">
                      부모 {issue.parentPartNo}
                      {issue.childPartNo ? ` → 자식 ${issue.childPartNo}` : ''}
                    </span>
                  </li>
                ))}
              </ul>
            )}
            <div className="form-actions">
              <button type="button" className="secondary" onClick={() => setIntegrityOpen(false)}>
                닫기
              </button>
            </div>
          </div>
        </div>
      )}

      {!readOnly && (
        <DrawingUploadModal
          open={isModalOpen}
          onClose={() => setIsModalOpen(false)}
          actorUserId={actorUserId}
          onSuccess={showSuccess}
          onError={showError}
        />
      )}

      {revisingDrawing && !readOnly && (
        <DrawingReviseModal
          open={!!revisingDrawing}
          onClose={() => setRevisingDrawing(null)}
          partNo={revisingDrawing.partNo}
          currentMajor={revisingDrawing.majorVersion}
          currentMinor={revisingDrawing.minorVersion}
          actorUserId={actorUserId}
          onSuccess={showSuccess}
          onError={showError}
        />
      )}

      {editingDrawing && !readOnly && (
        <DrawingInfoEditModal
          open={!!editingDrawing}
          onClose={() => setEditingDrawing(null)}
          drawingId={editingDrawing.id}
          initialPartNo={editingDrawing.partNo}
          initialPartName={editingDrawing.partName}
          initialModelType={editingDrawing.modelType}
          initialItemId={editingDrawing.itemId}
          initialItemNo={editingDrawing.itemNo}
          actorUserId={actorUserId}
          onSuccess={showSuccess}
          onError={showError}
        />
      )}

      {viewingDrawing && (
        <DrawingViewerModal
          open={!!viewingDrawing}
          onClose={() => setViewingDrawing(null)}
          masterId={viewingDrawing.id}
          partNo={viewingDrawing.partNo}
          drawingType={viewingDrawing.type}
          isDeleted={tab === 'deleted'}
          readOnly={readOnly}
          canManage={!readOnly}
          actorUserId={actorUserId}
          onSuccess={showSuccess}
          onError={showError}
        />
      )}

      <section className="panel">
        {filteredDrawings.length === 0 ? (
          <p className="hint">표시할 도면이 없습니다.</p>
        ) : (
          <>
            <div className="drawing-card-list">
              {filteredDrawings.map((row) => (
                <article key={row.id} className="drawing-card">
                  <div className="drawing-card__header">
                    <h3 className="drawing-card__title">{row.partNo}</h3>
                    <span className="drawing-version-badge">
                      V{row.majorVersion}.{row.minorVersion}
                    </span>
                  </div>
                  <p className="drawing-card__meta">
                    {row.partName} | {row.modelType}
                    {row.itemNo ? ` | 품목 ${row.itemNo}` : ''}
                  </p>
                  <p className="hint">최근 등록일: {row.updatedAt}</p>
                  <div className="drawing-card__actions">{renderRowActions(row)}</div>
                </article>
              ))}
            </div>

            <div className="table-wrap drawing-table-desktop">
              <table>
                <thead>
                  <tr>
                    <th>품번</th>
                    <th>연결 품목</th>
                    <th>품명</th>
                    <th>기종</th>
                    <th>버전</th>
                    <th>최근 등록일</th>
                    <th>관리</th>
                  </tr>
                </thead>
                <tbody>
                  {filteredDrawings.map((row) => (
                    <tr key={row.id}>
                      <td>
                        <strong>{row.partNo}</strong>
                      </td>
                      <td>{row.itemNo ?? '—'}</td>
                      <td>{row.partName}</td>
                      <td>{row.modelType}</td>
                      <td>
                        <span className="drawing-version-badge">
                          V{row.majorVersion}.{row.minorVersion}
                        </span>
                      </td>
                      <td>{row.updatedAt}</td>
                      <td className="row-actions">{renderRowActions(row)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </>
        )}
      </section>
    </div>
  );

  function renderRowActions(row: DrawingListItem) {
    return (
      <div className="drawing-row-actions">
        {!readOnly && tab !== 'deleted' && (
          <button
            type="button"
            className="drawing-icon-btn drawing-icon-btn--warn"
            title="정보 수정"
            onClick={() =>
              setEditingDrawing({
                id: row.id,
                partNo: row.partNo,
                partName: row.partName,
                modelType: row.modelType,
                itemId: row.itemId,
                itemNo: row.itemNo,
              })
            }
          >
            <Edit size={16} />
          </button>
        )}
        {!readOnly && tab !== 'deleted' && (
          <button
            type="button"
            className="drawing-icon-btn"
            title="도면 개정"
            onClick={() =>
              setRevisingDrawing({
                partNo: row.partNo,
                majorVersion: row.majorVersion,
                minorVersion: row.minorVersion,
              })
            }
          >
            <FileUp size={16} />
          </button>
        )}
        <button
          type="button"
          className="drawing-icon-btn drawing-icon-btn--info"
          title="도면 열람"
          onClick={() => setViewingDrawing({ id: row.id, partNo: row.partNo, type: row.drawingType })}
        >
          <Eye size={16} />
        </button>
        {!readOnly && tab === 'deleted' && (
          <button
            type="button"
            className="drawing-icon-btn drawing-icon-btn--success"
            title="도면 복구"
            onClick={() => void handleRestore(row.id, row.partNo)}
          >
            <RotateCcw size={16} />
          </button>
        )}
        {!readOnly && tab === 'deleted' && canHardDelete && (
          <button
            type="button"
            className="drawing-icon-btn drawing-icon-btn--danger"
            title="완전 삭제"
            onClick={() => void handleHardDelete(row.id, row.partNo)}
          >
            <Trash2 size={16} />
          </button>
        )}
        {!readOnly && tab !== 'deleted' && (
          <button
            type="button"
            className="drawing-icon-btn drawing-icon-btn--danger"
            title="도면 삭제"
            onClick={() => void handleDelete(row.partNo)}
          >
            <Trash2 size={16} />
          </button>
        )}
      </div>
    );
  }
}

export default function DrawingPage({ readOnly = false, canHardDelete = false, actorUserId }: DrawingPageProps) {
  return (
    <QueryClientProvider client={drawingQueryClient}>
      <DrawingDashboard readOnly={readOnly} canHardDelete={canHardDelete} actorUserId={actorUserId} />
    </QueryClientProvider>
  );
}
