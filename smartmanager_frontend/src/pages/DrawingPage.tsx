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
} from '../api/drawing';
import DrawingUploadModal from '../components/drawing/DrawingUploadModal';
import DrawingReviseModal from '../components/drawing/DrawingReviseModal';
import DrawingViewerModal from '../components/drawing/DrawingViewerModal';
import DrawingInfoEditModal from '../components/drawing/DrawingInfoEditModal';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import VirtualMasterTable from '../components/VirtualMasterTable';
import {
  canReviseDrawing,
  isArchivedLifecycle,
  lifecycleBadgeClass,
  lifecycleStageLabel,
  type DrawingLifecycleStage,
} from '../utils/drawingLifecycle';
import {
  getDrawingDefaultTab,
  isProductionDrawingViewer,
} from '../layout/menuAccess';
import { listCachedDrawingPartNos, syncDailyDrawings } from '../services/drawingOfflineCacheService';
import { useBannerMessages } from '../hooks/useBannerMessages';
import { useConfirm } from '../context/ConfirmContext';

const drawingQueryClient = new QueryClient({
  defaultOptions: {
    queries: { retry: 1, refetchOnWindowFocus: false },
  },
});

const EMPTY_DRAWINGS: DrawingListItem[] = [];
const EMPTY_PART_NOS: string[] = [];

type DrawingTab = 'dev' | 'prod' | 'deleted';

interface DrawingPageProps {
  readOnly?: boolean;
  canHardDelete?: boolean;
  actorUserId?: string;
  roleCodes?: string[];
}

const LIFECYCLE_FILTER_OPTIONS: { value: '' | DrawingLifecycleStage; label: string }[] = [
  { value: '', label: '전체 단계' },
  { value: 'RECEIVED', label: '선수신' },
  { value: 'SAMPLE', label: '샘플' },
  { value: 'PARTNER_REVIEW', label: '거래처 검토' },
  { value: 'MASS_PROD_READY', label: '양산 준비' },
  { value: 'ITEM_LINKED', label: '품목 연결' },
  { value: 'ARCHIVED', label: '보관 (사용 종료)' },
];

function DrawingDashboard({
  readOnly = false,
  canHardDelete = false,
  actorUserId,
  roleCodes = [],
}: DrawingPageProps) {
  const confirm = useConfirm();
  const queryClient = useQueryClient();
  const { message, error, showSuccess, showError } = useBannerMessages();
  const productionViewer = isProductionDrawingViewer(roleCodes, !readOnly);

  const [tab, setTab] = useState<DrawingTab>(() => getDrawingDefaultTab(roleCodes));
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [viewingDrawing, setViewingDrawing] = useState<DrawingListItem | null>(null);
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
  } | null>(null);

  const [searchPartNo, setSearchPartNo] = useState('');
  const [searchModelType, setSearchModelType] = useState('');
  const [searchDate, setSearchDate] = useState('');
  const [searchItem, setSearchItem] = useState<ItemSearchSelection | null>(null);
  const [searchLifecycleStage, setSearchLifecycleStage] = useState<'' | DrawingLifecycleStage>('');
  const [searchHistoryQuery, setSearchHistoryQuery] = useState('');
  const [includeHistorySearch, setIncludeHistorySearch] = useState(false);
  const [appliedPartNo, setAppliedPartNo] = useState('');
  const [appliedModelType, setAppliedModelType] = useState('');
  const [appliedDate, setAppliedDate] = useState('');
  const [appliedItem, setAppliedItem] = useState<ItemSearchSelection | null>(null);
  const [appliedLifecycleStage, setAppliedLifecycleStage] = useState<'' | DrawingLifecycleStage>('');
  const [appliedHistoryQuery, setAppliedHistoryQuery] = useState('');
  const [hasSearched, setHasSearched] = useState(false);
  const [itemClearToken, setItemClearToken] = useState(0);
  const [isSyncingOffline, setIsSyncingOffline] = useState(false);
  const [cachedPartNos, setCachedPartNos] = useState<string[]>([]);
  const [offlineMessage, setOfflineMessage] = useState<string | null>(null);
  const [integrityOpen, setIntegrityOpen] = useState(false);
  const [integrityLoading, setIntegrityLoading] = useState(false);
  const [integrityIssues, setIntegrityIssues] = useState<DrawingReferenceIntegrityIssue[]>([]);

  const listQuery = useMemo(
    () => ({
      lifecycleStage: appliedLifecycleStage || null,
      historyQuery: appliedHistoryQuery || null,
    }),
    [appliedLifecycleStage, appliedHistoryQuery],
  );

  const { data: drawings = EMPTY_DRAWINGS, isLoading, isError, isFetching } = useQuery({
    queryKey: ['drawings', listQuery],
    queryFn: () => fetchDrawings(listQuery),
    enabled: hasSearched,
  });

  const { data: deletedDrawings = EMPTY_DRAWINGS, isFetching: isFetchingDeleted } = useQuery({
    queryKey: ['deletedDrawings'],
    queryFn: fetchDeletedDrawings,
    enabled: hasSearched,
  });

  useEffect(() => {
    if (!hasSearched) {
      setCachedPartNos((prev) => (prev.length === 0 ? prev : EMPTY_PART_NOS));
      return;
    }
    let cancelled = false;
    void listCachedDrawingPartNos()
      .then((partNos) => {
        if (!cancelled) setCachedPartNos(partNos);
      })
      .catch(() => {
        if (!cancelled) setCachedPartNos(EMPTY_PART_NOS);
      });
    return () => {
      cancelled = true;
    };
  }, [hasSearched, drawings, deletedDrawings]);

  const refreshLists = async () => {
    await Promise.all([
      queryClient.refetchQueries({ queryKey: ['drawings'] }),
      queryClient.refetchQueries({ queryKey: ['deletedDrawings'] }),
    ]);
  };

  const patchDrawingInLists = (rowId: string, patch: Partial<DrawingListItem>) => {
    queryClient.setQueriesData<DrawingListItem[]>(
      { queryKey: ['drawings'] },
      (prev) => {
        if (!Array.isArray(prev)) {
          return prev;
        }
        return prev.map((item) => (item.id === rowId ? { ...item, ...patch } : item));
      },
    );
    queryClient.setQueriesData<DrawingListItem[]>(
      { queryKey: ['deletedDrawings'] },
      (prev) => {
        if (!Array.isArray(prev)) {
          return prev;
        }
        return prev.map((item) => (item.id === rowId ? { ...item, ...patch } : item));
      },
    );
  };

  const moveDrawingToActive = (row: DrawingListItem) => {
    queryClient.setQueriesData<DrawingListItem[]>(
      { queryKey: ['drawings'] },
      (prev) => {
        const base = Array.isArray(prev) ? prev : [];
        return base.some((item) => item.id === row.id) ? base : [...base, row];
      },
    );
    queryClient.setQueriesData<DrawingListItem[]>(
      { queryKey: ['deletedDrawings'] },
      (prev) => (Array.isArray(prev) ? prev.filter((item) => item.id !== row.id) : prev),
    );
  };

  const moveDrawingToDeleted = (row: DrawingListItem) => {
    queryClient.setQueriesData<DrawingListItem[]>(
      { queryKey: ['drawings'] },
      (prev) => (Array.isArray(prev) ? prev.filter((item) => item.partNo !== row.partNo) : prev),
    );
    queryClient.setQueriesData<DrawingListItem[]>(
      { queryKey: ['deletedDrawings'] },
      (prev) => {
        const base = Array.isArray(prev) ? prev : [];
        return base.some((item) => item.id === row.id) ? base : [...base, row];
      },
    );
  };

  const removeDeletedDrawing = (id: string) => {
    queryClient.setQueriesData<DrawingListItem[]>(
      { queryKey: ['deletedDrawings'] },
      (prev) => (Array.isArray(prev) ? prev.filter((item) => item.id !== id) : prev),
    );
  };

  const handleDelete = async (partNo: string) => {
    if (!(await confirm(`[${partNo}] 도면을 정말 삭제하시겠습니까?\n(논리 삭제 처리되며 삭제 내역에서 확인 가능합니다)`, { title: '삭제 확인', confirmLabel: '삭제', cancelLabel: '닫기', danger: true }))) {
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
    if (!(await confirm(`[${partNo}] 도면을 다시 복구하시겠습니까?`, { cancelLabel: '닫기' }))) {
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
      !(await confirm(
        `[${partNo}] 도면을 정말 영구 삭제하시겠습니까?\n관련된 모든 이력과 PDF 파일이 물리적으로 삭제되며 절대 복구할 수 없습니다.`,
        { title: '영구 삭제 확인', confirmLabel: '영구 삭제', cancelLabel: '닫기', danger: true },
      ))
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
  const filteredDrawings = useMemo(() => {
    if (!hasSearched) {
      return [];
    }
    return currentData.filter((row: DrawingListItem) => {
      const matchType =
        tab === 'dev' ? row.drawingType === 'DEV' : tab === 'prod' ? row.drawingType === 'PROD' : true;
      const matchPartNo = row.partNo.toLowerCase().includes(appliedPartNo.toLowerCase());
      const matchModelType = row.modelType.toLowerCase().includes(appliedModelType.toLowerCase());
      const matchDate = appliedDate ? row.updatedAt.startsWith(appliedDate) : true;
      const matchItem = appliedItem
        ? row.itemNo?.toLowerCase() === appliedItem.itemNo.toLowerCase() || row.itemId === appliedItem.id
        : true;
      return matchType && matchPartNo && matchModelType && matchDate && matchItem;
    });
  }, [hasSearched, currentData, tab, appliedPartNo, appliedModelType, appliedDate, appliedItem]);

  const applySearch = (next: {
    partNo?: string;
    modelType?: string;
    date?: string;
    item?: ItemSearchSelection | null;
    lifecycleStage?: '' | DrawingLifecycleStage;
    historyQuery?: string;
    includeHistory?: boolean;
  }) => {
    setAppliedPartNo(next.partNo ?? searchPartNo);
    setAppliedModelType(next.modelType ?? searchModelType);
    setAppliedDate(next.date ?? searchDate);
    setAppliedItem(next.item !== undefined ? next.item : searchItem);
    const stage = next.lifecycleStage !== undefined ? next.lifecycleStage : searchLifecycleStage;
    const includeHistory = next.includeHistory !== undefined ? next.includeHistory : includeHistorySearch;
    const historyQ = next.historyQuery !== undefined ? next.historyQuery : searchHistoryQuery;
    setAppliedLifecycleStage(stage);
    setAppliedHistoryQuery(includeHistory && historyQ.trim() ? historyQ.trim() : '');
    setHasSearched(true);
  };

  const handleSearch = () => {
    applySearch({});
  };

  const handleReset = () => {
    setSearchPartNo('');
    setSearchModelType('');
    setSearchDate('');
    setSearchItem(null);
    setSearchLifecycleStage('');
    setSearchHistoryQuery('');
    setIncludeHistorySearch(false);
    setAppliedPartNo('');
    setAppliedModelType('');
    setAppliedDate('');
    setAppliedItem(null);
    setAppliedLifecycleStage('');
    setAppliedHistoryQuery('');
    setHasSearched(false);
    setItemClearToken((token) => token + 1);
    setOfflineMessage(null);
  };

  const handleItemSelect = (item: ItemSearchSelection | null) => {
    setSearchItem(item);
    if (item) {
      applySearch({ item });
    }
  };

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

  return (
    <div className="page drawing-page">
      <header className="page-header">
        <div>
          <h1>도면 관리 현황</h1>
          <p>거래처 선수신 → 개발 개정 → 양산 이관 → 품목 연결 업무 흐름을 관리합니다.</p>
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
      <p className="hint drawing-workflow-hint">
        {productionViewer
          ? '생산 열람 모드: 양산품(PROD) 탭에서 최신 도면을 확인합니다. 과거 이력은 상세 화면 사이드바에서 열람할 수 있습니다.'
          : '개발품(DEV)은 품질·관리 담당, 양산품(PROD)은 생산 열람용입니다. 버전 V1.1은 메이저 1·마이너 1(구 표기 V1-1)을 의미합니다.'}
      </p>
      {hasSearched && isError && <p className="error-banner">데이터를 불러오지 못했습니다.</p>}
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
              label="품목"
              selectedItem={searchItem}
              onSelect={handleItemSelect}
              clearToken={itemClearToken}
              placeholder="연결 품목"
            />
          </div>
          <label>
            <span>품번</span>
            <input
              placeholder="예: A-1"
              value={searchPartNo}
              onChange={(e) => setSearchPartNo(e.target.value)}
              onKeyDown={(e) => {
                if (e.key === 'Enter') {
                  e.preventDefault();
                  handleSearch();
                }
              }}
            />
          </label>
          <label>
            <span>기종</span>
            <input
              placeholder="예: 로더"
              value={searchModelType}
              onChange={(e) => setSearchModelType(e.target.value)}
              onKeyDown={(e) => {
                if (e.key === 'Enter') {
                  e.preventDefault();
                  handleSearch();
                }
              }}
            />
          </label>
          <label>
            <span>등록일</span>
            <input
              type="date"
              value={searchDate}
              onChange={(e) => setSearchDate(e.target.value)}
              onKeyDown={(e) => {
                if (e.key === 'Enter') {
                  e.preventDefault();
                  handleSearch();
                }
              }}
            />
          </label>
          <label>
            <span>업무 단계</span>
            <select
              value={searchLifecycleStage}
              onChange={(e) => setSearchLifecycleStage(e.target.value as '' | DrawingLifecycleStage)}
            >
              {LIFECYCLE_FILTER_OPTIONS.map((option) => (
                <option key={option.value || 'all'} value={option.value}>
                  {option.label}
                </option>
              ))}
            </select>
          </label>
          <div className="drawing-filter-history">
            <span>AS 이력 검색</span>
            <div className="drawing-filter-history__row">
              <input
                value={searchHistoryQuery}
                placeholder="개정 사유·버전 (예: 1.1)"
                disabled={!includeHistorySearch}
                onChange={(e) => setSearchHistoryQuery(e.target.value)}
                onKeyDown={(e) => {
                  if (e.key === 'Enter') {
                    e.preventDefault();
                    handleSearch();
                  }
                }}
              />
              <label className="drawing-filter-checkbox">
                <input
                  type="checkbox"
                  checked={includeHistorySearch}
                  onChange={(e) => setIncludeHistorySearch(e.target.checked)}
                />
                전체 이력 포함
              </label>
            </div>
          </div>
          <div className="drawing-filter-actions">
            <button type="button" onClick={handleSearch}>
              검색
            </button>
            <button type="button" className="secondary" onClick={handleReset}>
              초기화
            </button>
          </div>
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
          drawingType={viewingDrawing.drawingType}
          lifecycleStage={viewingDrawing.lifecycleStage}
          sourcePartnerName={viewingDrawing.sourcePartnerName}
          itemId={viewingDrawing.itemId}
          itemNo={viewingDrawing.itemNo}
          itemLinkedAt={viewingDrawing.itemLinkedAt}
          historyHighlightQuery={appliedHistoryQuery || undefined}
          isDeleted={tab === 'deleted'}
          readOnly={readOnly}
          canManage={!readOnly}
          actorUserId={actorUserId}
          onSuccess={showSuccess}
          onError={showError}
          onDrawingMetaChange={(patch) => {
            const rowId = patch.id ?? viewingDrawing.id;
            patchDrawingInLists(rowId, patch);
            setViewingDrawing((prev) => (prev && prev.id === rowId ? { ...prev, ...patch } : prev));
          }}
        />
      )}

      <section className="panel">
        {!hasSearched ? (
          <p className="hint">검색 조건을 입력한 뒤 검색하거나, 품목을 선택해 주세요.</p>
        ) : isLoading || isFetching || isFetchingDeleted ? (
          <p className="hint">불러오는 중…</p>
        ) : filteredDrawings.length === 0 ? (
          <p className="hint">표시할 도면이 없습니다.</p>
        ) : (
          <>
            <div className="drawing-card-list">
              {filteredDrawings.map((row) => (
                <article key={row.id} className="drawing-card">
                  <div className="drawing-card__header">
                    <h3 className="drawing-card__title">{row.partNo}</h3>
                    <div className="drawing-card__badges">
                      <span className={lifecycleBadgeClass(row.lifecycleStage)}>
                        {lifecycleStageLabel(row.lifecycleStage)}
                      </span>
                      <span className="drawing-version-badge">
                        V{row.majorVersion}.{row.minorVersion}
                      </span>
                    </div>
                  </div>
                  <p className="drawing-card__meta">
                    {row.partName} | {row.modelType}
                    {row.sourcePartnerName ? ` | 선수신 ${row.sourcePartnerName}` : ''}
                    {row.itemNo ? ` | 품목 ${row.itemNo}` : ''}
                  </p>
                  <p className="hint">최근 등록일: {row.updatedAt}</p>
                  <div className="drawing-card__actions">{renderRowActions(row)}</div>
                </article>
              ))}
            </div>

            <VirtualMasterTable
              className="drawing-table-desktop"
              rows={filteredDrawings}
              columnCount={9}
              getRowKey={(row) => row.id}
              renderHeader={() => (
                <tr>
                  <th>품번</th>
                  <th>업무 단계</th>
                  <th>선수신 거래처</th>
                  <th>연결 품목</th>
                  <th>품명</th>
                  <th>기종</th>
                  <th>버전</th>
                  <th>최근 등록일</th>
                  <th>관리</th>
                </tr>
              )}
              renderRow={(row) => (
                <tr>
                  <td>
                    <strong>{row.partNo}</strong>
                  </td>
                  <td>
                    <span className={lifecycleBadgeClass(row.lifecycleStage)}>
                      {lifecycleStageLabel(row.lifecycleStage)}
                    </span>
                  </td>
                  <td>{row.sourcePartnerName ?? '—'}</td>
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
              )}
            />
          </>
        )}
      </section>
    </div>
  );

  function renderRowActions(row: DrawingListItem) {
    const archived = isArchivedLifecycle(row.lifecycleStage);
    return (
      <div className="drawing-row-actions">
        {!readOnly && tab !== 'deleted' && !archived && (
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
              })
            }
          >
            <Edit size={16} />
          </button>
        )}
        {!readOnly && tab !== 'deleted' && canReviseDrawing(row.drawingType, row.lifecycleStage) && (
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
          onClick={() => setViewingDrawing(row)}
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

export default function DrawingPage({
  readOnly = false,
  canHardDelete = false,
  actorUserId,
  roleCodes = [],
}: DrawingPageProps) {
  return (
    <QueryClientProvider client={drawingQueryClient}>
      <DrawingDashboard
        readOnly={readOnly}
        canHardDelete={canHardDelete}
        actorUserId={actorUserId}
        roleCodes={roleCodes}
      />
    </QueryClientProvider>
  );
}
