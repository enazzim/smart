import { useCallback, useEffect, useMemo, useState, type FormEvent } from 'react';
import type { PropertyClassification } from '../api/item';
import { updateItemLotTracked } from '../api/item';
import type {
  BomTreeNode,
  CreateItemCompositionRequest,
  ItemComposition,
  LotTrackedEnablePreviewItem,
} from '../api/itemComposition';
import {
  copyBom,
  createItemComposition,
  deleteItemComposition,
  enableLotTrackedForExplosion,
  fetchBomExplosion,
  fetchBomReverse,
  fetchItemCompositions,
  previewEnableLotTracked,
  updateItemComposition,
} from '../api/itemComposition';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import { useAuth } from '../context/AuthContext';
import { downloadExplosionExcel, downloadReverseExcel } from '../utils/bomExcelExport';
import { formatQty } from '../utils/numberFormat';
import { useConfirm } from '../context/ConfirmContext';

const PARENT_CLASSES: PropertyClassification[] = ['제품', '상품', '공정품'];
const CHILD_CLASSES: PropertyClassification[] = ['원자재', '공정품'];
/** 목록 필터: 상품·원자재 제외 (제품·공정품) */
const PARENT_FILTER_CLASSES: PropertyClassification[] = ['제품', '공정품'];
/** 목록 필터: 제품·상품 제외 (원자재·공정품) */
const CHILD_FILTER_CLASSES: PropertyClassification[] = ['원자재', '공정품'];

type ModalKind = 'explosion' | 'reverse' | 'copy' | null;

function formatUnitPrice(value: number): string {
  return value.toLocaleString(undefined, { maximumFractionDigits: 2 });
}

function formatVendorPrices(prices: BomTreeNode['outsourcePrices']): string {
  if (!prices || prices.length === 0) return '—';
  return prices
    .map((price) => {
      const detail = price.detail ? ` (${price.detail})` : '';
      return `${price.partnerName}${detail} ${formatUnitPrice(price.unitPrice)}`;
    })
    .join('\n');
}

function BomTreeRows({
  node,
  path = 'root',
  canEditLot,
  busyItemId,
  onToggleLot,
}: {
  node: BomTreeNode;
  path?: string;
  canEditLot: boolean;
  busyItemId: number | null;
  onToggleLot: (node: BomTreeNode) => void;
}) {
  const busy = busyItemId === node.itemId;
  return (
    <>
      <tr>
        <td style={{ paddingLeft: `${node.level * 1.25 + 0.5}rem` }}>
          L{node.level}
        </td>
        <td>{node.itemNum}</td>
        <td>{node.itemName}</td>
        <td>{node.propertyClassification}</td>
        <td>{node.lotTracked ? '예' : '아니오'}</td>
        <td className="actions">
          {canEditLot ? (
            <button
              type="button"
              className="btn-action"
              disabled={busy || busyItemId != null}
              onClick={() => onToggleLot(node)}
              title="품목 마스터 Lot 추적을 변경합니다 (다른 BOM 공유 품목에도 적용)"
            >
              {busy ? '저장 중…' : node.lotTracked ? '해제' : '설정'}
            </button>
          ) : (
            '—'
          )}
        </td>
        <td>{node.quantity}</td>
        <td className="bom-vendor-cell" style={{ whiteSpace: 'pre-line' }}>
          {formatVendorPrices(node.outsourcePrices ?? [])}
        </td>
        <td className="bom-vendor-cell" style={{ whiteSpace: 'pre-line' }}>
          {formatVendorPrices(node.purchasePrices ?? [])}
        </td>
      </tr>
      {node.children.map((child, index) => (
        <BomTreeRows
          key={`${path}/${child.itemNum}-${index}`}
          node={child}
          path={`${path}/${child.itemNum}-${index}`}
          canEditLot={canEditLot}
          busyItemId={busyItemId}
          onToggleLot={onToggleLot}
        />
      ))}
    </>
  );
}

export default function ItemCompositionPage() {
  const confirm = useConfirm();
  const { currentUser } = useAuth();
  /** 백엔드 @BasisAuthorize.ItemWrite 와 동일 기준 (VIEWER 역할과 무관) */
  const canEditLot = Boolean(currentUser?.authorities.includes('basis:item:write'));

  const [rows, setRows] = useState<ItemComposition[]>([]);
  const [filterParent, setFilterParent] = useState<ItemSearchSelection | null>(null);
  const [filterChild, setFilterChild] = useState<ItemSearchSelection | null>(null);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [lotBusyItemId, setLotBusyItemId] = useState<number | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [toast, setToast] = useState<string | null>(null);

  const [modal, setModal] = useState<ModalKind>(null);
  const [editingId, setEditingId] = useState<number | null>(null);

  const [formParent, setFormParent] = useState<ItemSearchSelection | null>(null);
  const [formChild, setFormChild] = useState<ItemSearchSelection | null>(null);
  const [parentQty, setParentQty] = useState('1');
  const [childQty, setChildQty] = useState('1');

  const [explosionTree, setExplosionTree] = useState<BomTreeNode | null>(null);
  const [lotEnablePreview, setLotEnablePreview] = useState<LotTrackedEnablePreviewItem[] | null>(null);
  const [lotEnableSelectedIds, setLotEnableSelectedIds] = useState<Set<number>>(new Set());
  const [lotEnableBusy, setLotEnableBusy] = useState(false);
  const [reverseRows, setReverseRows] = useState<ItemComposition[]>([]);
  const [reverseItemNum, setReverseItemNum] = useState('');

  const [copySource, setCopySource] = useState<ItemSearchSelection | null>(null);
  const [copyTarget, setCopyTarget] = useState<ItemSearchSelection | null>(null);

  const isEditing = editingId !== null;

  const bomExportRows = useMemo(
    () =>
      rows.map((row) => ({
        모품목: row.parentItemNo,
        모품목명: row.parentItemName,
        자품목: row.childItemNo,
        자품목명: row.childItemName,
        모품수량: row.parentQuantity,
        자품수량: row.childQuantity,
      })),
    [rows],
  );

  const load = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      setRows(
        await fetchItemCompositions(
          filterParent?.itemNo,
          filterChild?.itemNo,
          filterParent?.id,
          filterChild?.id,
        ),
      );
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
    } finally {
      setLoading(false);
    }
  }, [filterParent?.id, filterParent?.itemNo, filterChild?.id, filterChild?.itemNo]);

  useEffect(() => {
    void load();
  }, [load]);

  const closeModal = () => {
    setModal(null);
    setExplosionTree(null);
    setLotEnablePreview(null);
    setLotEnableSelectedIds(new Set());
    setLotEnableBusy(false);
    setReverseRows([]);
    setReverseItemNum('');
    setCopySource(null);
    setCopyTarget(null);
  };

  const resetForm = () => {
    setEditingId(null);
    setFormParent(null);
    setFormChild(null);
    setParentQty('1');
    setChildQty('1');
  };

  const startEdit = (row: ItemComposition) => {
    setEditingId(row.id);
    setFormParent({
      id: row.parentItemId,
      itemNo: row.parentItemNo,
      itemName: row.parentItemName,
    });
    setFormChild({
      id: row.childItemId,
      itemNo: row.childItemNo,
      itemName: row.childItemName,
    });
    setParentQty(String(row.parentQuantity));
    setChildQty(String(row.childQuantity));
    setError(null);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const onSubmitForm = async (e: FormEvent) => {
    e.preventDefault();
    const pQty = Number(parentQty);
    const cQty = Number(childQty);
    if (!Number.isFinite(pQty) || pQty <= 0 || !Number.isFinite(cQty) || cQty < 0) {
      setError('모품수량은 0보다 커야 하고, 자품수량은 0 이상이어야 합니다.');
      return;
    }

    setSubmitting(true);
    setError(null);
    try {
      if (isEditing && editingId !== null) {
        await updateItemComposition(editingId, {
          parentQuantity: pQty,
          childQuantity: cQty,
        });
      } else {
        if (!formParent || !formChild) {
          setError('모품목·자품목을 선택해 주세요.');
          return;
        }
        const payload: CreateItemCompositionRequest = {
          parentItemNum: formParent.itemNo,
          childItemNum: formChild.itemNo,
          parentQuantity: pQty,
          childQuantity: cQty,
        };
        await createItemComposition(payload);
      }
      resetForm();
      await load();
    } catch (err) {
      setError(err instanceof Error ? err.message : '저장 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (row: ItemComposition) => {
    if (!(await confirm(`「${row.parentItemNo} → ${row.childItemNo}」 BOM을 삭제하시겠습니까?`, { title: '삭제 확인', confirmLabel: '삭제', cancelLabel: '닫기', danger: true }))) {
      return;
    }
    setError(null);
    try {
      await deleteItemComposition(row.id);
      if (editingId === row.id) {
        resetForm();
      }
      await load();
    } catch (err) {
      setError(err instanceof Error ? err.message : '삭제 실패');
    }
  };

  const onExplosion = async () => {
    const itemNum = filterParent?.itemNo;
    if (!itemNum) {
      setError('정전개는 모품목을 선택해 주세요.');
      return;
    }
    setSubmitting(true);
    setError(null);
    try {
      setExplosionTree(await fetchBomExplosion(itemNum));
      setLotEnablePreview(null);
      setLotEnableSelectedIds(new Set());
      setModal('explosion');
    } catch (err) {
      setError(err instanceof Error ? err.message : '정전개 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const refreshExplosionTree = useCallback(async () => {
    const itemNum = filterParent?.itemNo ?? explosionTree?.itemNum;
    if (!itemNum) {
      return;
    }
    setExplosionTree(await fetchBomExplosion(itemNum));
  }, [filterParent?.itemNo, explosionTree?.itemNum]);

  const onToggleLot = async (node: BomTreeNode) => {
    if (!canEditLot) {
      return;
    }
    const next = !node.lotTracked;
    if (!next) {
      const ok = await confirm(
        `품목 「${node.itemNum} ${node.itemName}」의 Lot 추적을 해제하시겠습니까?\n\n` +
          '품목 마스터 값이 변경되며, 다른 BOM에서 쓰는 동일 품목에도 적용됩니다.',
        { confirmLabel: '해제', cancelLabel: '닫기', danger: true },
      );
      if (!ok) {
        return;
      }
    }
    setLotBusyItemId(node.itemId);
    setError(null);
    try {
      await updateItemLotTracked(node.itemId, next);
      await refreshExplosionTree();
      setToast(
        next
          ? `「${node.itemNum}」 Lot 추적을 설정했습니다.`
          : `「${node.itemNum}」 Lot 추적을 해제했습니다.`,
      );
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Lot 추적 변경 실패');
    } finally {
      setLotBusyItemId(null);
    }
  };

  const openLotEnablePreview = async () => {
    const itemNum = explosionTree?.itemNum ?? filterParent?.itemNo;
    if (!itemNum || !canEditLot) {
      return;
    }
    setLotEnableBusy(true);
    setError(null);
    try {
      const preview = await previewEnableLotTracked(itemNum);
      setLotEnablePreview(preview);
      setLotEnableSelectedIds(
        new Set(preview.filter((row) => !row.alreadyLotTracked).map((row) => row.itemId)),
      );
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Lot 일괄 설정 미리보기 실패');
    } finally {
      setLotEnableBusy(false);
    }
  };

  const toggleLotEnableSelection = (itemId: number) => {
    setLotEnableSelectedIds((prev) => {
      const next = new Set(prev);
      if (next.has(itemId)) {
        next.delete(itemId);
      } else {
        next.add(itemId);
      }
      return next;
    });
  };

  const applyLotEnableBulk = async () => {
    const itemNum = explosionTree?.itemNum ?? filterParent?.itemNo;
    if (!itemNum || !canEditLot) {
      return;
    }
    const ids = Array.from(lotEnableSelectedIds);
    if (ids.length === 0) {
      setError('Lot 추적 설정 대상을 선택해 주세요.');
      return;
    }
    const sharedCount =
      lotEnablePreview?.filter(
        (row) => lotEnableSelectedIds.has(row.itemId) && row.otherParentItemNos.length > 0,
      ).length ?? 0;
    const ok = await confirm(
      `선택한 ${ids.length}개 품목의 Lot 추적을 설정하시겠습니까?` +
        (sharedCount > 0
          ? `\n\n이 중 ${sharedCount}개 품목은 현재 정전개 밖의 다른 모품목 BOM에도 쓰입니다.`
          : ''),
      { cancelLabel: '닫기' },
    );
    if (!ok) {
      return;
    }
    setLotEnableBusy(true);
    setError(null);
    try {
      const result = await enableLotTrackedForExplosion(itemNum, ids);
      setLotEnablePreview(null);
      setLotEnableSelectedIds(new Set());
      await refreshExplosionTree();
      setToast(`Lot 추적 ${result.updatedCount}건을 설정했습니다.`);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Lot 일괄 설정 실패');
    } finally {
      setLotEnableBusy(false);
    }
  };

  const onReverse = async () => {
    const itemNum = filterChild?.itemNo;
    if (!itemNum) {
      setError('역전개는 자품목을 선택해 주세요.');
      return;
    }
    setSubmitting(true);
    setError(null);
    try {
      setReverseRows(await fetchBomReverse(itemNum));
      setReverseItemNum(itemNum);
      setModal('reverse');
    } catch (err) {
      setError(err instanceof Error ? err.message : '역전개 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const openCopy = () => {
    setCopySource(filterParent);
    setCopyTarget(null);
    setModal('copy');
    setError(null);
  };

  const onCopy = async (e: FormEvent) => {
    e.preventDefault();
    if (!copySource?.itemNo || !copyTarget?.itemNo) {
      setError('원본·대상 모품목을 선택해 주세요.');
      return;
    }
    setSubmitting(true);
    setError(null);
    try {
      const result = await copyBom({
        sourceItemNum: copySource.itemNo,
        targetItemNum: copyTarget.itemNo,
      });
      setToast(`${result.copiedCount}건 복사되었습니다.`);
      closeModal();
      await load();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'BOM 복사 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const filterByParent = (row: ItemComposition) => {
    setFilterParent({
      id: row.parentItemId,
      itemNo: row.parentItemNo,
      itemName: row.parentItemName,
    });
    setFilterChild(null);
    closeModal();
  };

  useEffect(() => {
    if (!toast) return;
    const timer = setTimeout(() => setToast(null), 3000);
    return () => clearTimeout(timer);
  }, [toast]);

  return (
    <div className="page">
      <header className="page-header">
        <h1>품목구성 (BOM plan)</h1>
        <p>모·자품목 4필드 CRUD · 정전개 · 역전개 · BOM 복사</p>
      </header>

      {error && <div className="error">{error}</div>}
      {toast && <div className="toast">{toast}</div>}

      <section className="panel">
        <h2>{isEditing ? `품목구성 수정 (ID ${editingId})` : '품목구성 등록'}</h2>
        <form onSubmit={onSubmitForm} className="form-grid form-grid-wide">
          {isEditing ? (
            <>
              <ItemSearchField
                label="모품목 *"
                selectedItem={formParent}
                onSelect={() => {}}
                allowedClassifications={PARENT_CLASSES}
                disabled
              />
              <ItemSearchField
                label="자품목 *"
                selectedItem={formChild}
                onSelect={() => {}}
                allowedClassifications={CHILD_CLASSES}
                disabled
              />
            </>
          ) : (
            <>
              <ItemSearchField
                label="모품목 *"
                selectedItem={formParent}
                onSelect={setFormParent}
                allowedClassifications={PARENT_CLASSES}
              />
              <ItemSearchField
                label="자품목 *"
                selectedItem={formChild}
                onSelect={setFormChild}
                allowedClassifications={CHILD_CLASSES}
              />
            </>
          )}
          <label>
            모품수량 *
            <input
              required
              type="number"
              min="0.0001"
              step="any"
              value={parentQty}
              onChange={(e) => setParentQty(e.target.value)}
            />
          </label>
          <label>
            자품수량 *
            <input
              required
              type="number"
              min="0"
              step="any"
              value={childQty}
              onChange={(e) => setChildQty(e.target.value)}
            />
          </label>
          <div className="form-actions">
            <button type="submit" disabled={submitting}>
              {submitting ? '저장 중…' : isEditing ? '수정 저장' : '등록'}
            </button>
            {isEditing && (
              <button type="button" className="secondary" onClick={resetForm} disabled={submitting}>
                취소
              </button>
            )}
          </div>
        </form>
        {isEditing && (
          <p className="item-search-hint">수정 시 모품목·자품목은 변경할 수 없습니다.</p>
        )}
      </section>

      <section className="panel">
        <div className="panel-header-row">
          <h2>품목구성 목록</h2>
          <GridExcelExportButton fileBaseName="품목구성목록" disabled={loading} rows={bomExportRows} />
        </div>
        <div className="bom-toolbar">
          <div className="search-row">
            <ItemSearchField
              label="모품목 필터 (선택)"
              selectedItem={filterParent}
              onSelect={(item) => {
                setFilterParent(item);
              }}
              allowedClassifications={PARENT_FILTER_CLASSES}
              placeholder="전체 조회 — 품목번호 또는 품목명 입력"
            />
            <ItemSearchField
              label="자품목 필터 (선택)"
              selectedItem={filterChild}
              onSelect={(item) => {
                setFilterChild(item);
              }}
              allowedClassifications={CHILD_FILTER_CLASSES}
              placeholder="전체 조회 — 품목번호 또는 품목명 입력"
            />
            <button
              type="button"
              className="secondary"
              onClick={() => {
                setFilterParent(null);
                setFilterChild(null);
              }}
            >
              전체
            </button>
          </div>
          <div className="toolbar-actions">
            <button type="button" className="secondary" onClick={() => void onExplosion()} disabled={submitting}>
              정전개
            </button>
            <button type="button" className="secondary" onClick={() => void onReverse()} disabled={submitting}>
              역전개
            </button>
            <button type="button" className="secondary" onClick={openCopy}>
              BOM복사
            </button>
          </div>
        </div>

        {loading ? (
          <p>불러오는 중…</p>
        ) : rows.length === 0 ? (
          <p>등록된 BOM이 없습니다.</p>
        ) : (
          <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>모품목</th>
                <th>모품목명</th>
                <th>자품목</th>
                <th>자품목명</th>
                <th className="num">모품수량</th>
                <th className="num">자품수량</th>
                <th>작업</th>
              </tr>
            </thead>
            <tbody>
              {rows.map((row) => (
                <tr key={row.id} className={editingId === row.id ? 'row-editing' : undefined}>
                  <td>{row.parentItemNo}</td>
                  <td>{row.parentItemName}</td>
                  <td>{row.childItemNo}</td>
                  <td>{row.childItemName}</td>
                  <td className="num">{formatQty(row.parentQuantity)}</td>
                  <td className="num">{formatQty(row.childQuantity)}</td>
                  <td className="actions">
                    <button type="button" className="btn-action" onClick={() => startEdit(row)}>
                      수정
                    </button>
                    <button type="button" className="btn-action danger" onClick={() => void onDelete(row)}>
                      삭제
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          </div>
        )}
      </section>

      {modal && (
        <div className="modal-backdrop" onClick={closeModal}>
          <div
            className="modal-panel"
            role="dialog"
            aria-modal="true"
            onClick={(e) => e.stopPropagation()}
          >
            {modal === 'explosion' && explosionTree && (
              <>
                <div className="panel-header-row">
                  <h2>BOM 정전개 — {explosionTree.itemNum}</h2>
                  {canEditLot && (
                    <button
                      type="button"
                      className="secondary"
                      disabled={lotEnableBusy || lotBusyItemId != null}
                      onClick={() => void openLotEnablePreview()}
                    >
                      {lotEnableBusy && !lotEnablePreview ? '미리보기…' : '트리 Lot 일괄 설정'}
                    </button>
                  )}
                </div>
                <p className="hint-text">
                  Lot추적 설정/해제는 품목 마스터에 저장됩니다. 동일 품목이 다른 BOM에 있으면 함께 반영됩니다.
                </p>

                {lotEnablePreview && (
                  <section className="detail-panel" style={{ marginBottom: '1rem' }}>
                    <div className="panel-header-row">
                      <h3>Lot 일괄 설정 미리보기</h3>
                      <button
                        type="button"
                        className="secondary"
                        disabled={lotEnableBusy}
                        onClick={() => {
                          setLotEnablePreview(null);
                          setLotEnableSelectedIds(new Set());
                        }}
                      >
                        닫기
                      </button>
                    </div>
                    <p className="hint-text">
                      이미 Lot 추적 중인 품목은 제외됩니다. 「공유」는 현재 정전개 트리 밖의 모품목입니다.
                    </p>
                    <div className="table-wrap">
                      <table>
                        <thead>
                          <tr>
                            <th>선택</th>
                            <th>품목번호</th>
                            <th>품목명</th>
                            <th>현재</th>
                            <th>공유 모품목</th>
                          </tr>
                        </thead>
                        <tbody>
                          {lotEnablePreview.length === 0 ? (
                            <tr>
                              <td colSpan={5}>대상 품목이 없습니다.</td>
                            </tr>
                          ) : (
                            lotEnablePreview.map((row) => {
                              const selectable = !row.alreadyLotTracked;
                              const shared = row.otherParentItemNos.length > 0;
                              return (
                                <tr key={row.itemId} className={shared && selectable ? 'is-selected' : undefined}>
                                  <td>
                                    {selectable ? (
                                      <input
                                        type="checkbox"
                                        checked={lotEnableSelectedIds.has(row.itemId)}
                                        disabled={lotEnableBusy}
                                        onChange={() => toggleLotEnableSelection(row.itemId)}
                                      />
                                    ) : (
                                      '—'
                                    )}
                                  </td>
                                  <td>{row.itemNo}</td>
                                  <td>{row.itemName}</td>
                                  <td>{row.alreadyLotTracked ? '예' : '아니오'}</td>
                                  <td>
                                    {shared ? row.otherParentItemNos.join(', ') : '—'}
                                  </td>
                                </tr>
                              );
                            })
                          )}
                        </tbody>
                      </table>
                    </div>
                    <div className="form-actions">
                      <button
                        type="button"
                        disabled={lotEnableBusy || lotEnableSelectedIds.size === 0}
                        onClick={() => void applyLotEnableBulk()}
                      >
                        {lotEnableBusy ? '적용 중…' : `선택 ${lotEnableSelectedIds.size}건 설정`}
                      </button>
                      <button
                        type="button"
                        className="secondary"
                        disabled={lotEnableBusy}
                        onClick={() =>
                          setLotEnableSelectedIds(
                            new Set(
                              lotEnablePreview
                                .filter((row) => !row.alreadyLotTracked)
                                .map((row) => row.itemId),
                            ),
                          )
                        }
                      >
                        미추적 전체 선택
                      </button>
                    </div>
                  </section>
                )}

                <table>
                  <thead>
                    <tr>
                      <th>레벨</th>
                      <th>품목번호</th>
                      <th>품목명</th>
                      <th>자산분류</th>
                      <th>Lot추적</th>
                      <th>변경</th>
                      <th>누적수량</th>
                      <th>외주거래처·단가</th>
                      <th>구매거래처·단가</th>
                    </tr>
                  </thead>
                  <tbody>
                    <BomTreeRows
                      node={explosionTree}
                      canEditLot={canEditLot}
                      busyItemId={lotBusyItemId}
                      onToggleLot={(node) => void onToggleLot(node)}
                    />
                  </tbody>
                </table>
                <div className="form-actions">
                  <button
                    type="button"
                    onClick={() => void downloadExplosionExcel(explosionTree)}
                  >
                    엑셀 저장
                  </button>
                  <button type="button" className="secondary" onClick={closeModal}>
                    닫기
                  </button>
                </div>
              </>
            )}

            {modal === 'reverse' && (
              <>
                <h2>BOM 역전개 (직계 상위 1레벨)</h2>
                <p className="hint-text">자품목 「{reverseItemNum}」을(를) 사용하는 모품목 목록입니다.</p>
                {reverseRows.length === 0 ? (
                  <p>상위 모품목이 없습니다.</p>
                ) : (
                  <table>
                    <thead>
                      <tr>
                        <th>모품목</th>
                        <th>자품목</th>
                        <th>모품수량</th>
                        <th>자품수량</th>
                        <th>필터</th>
                      </tr>
                    </thead>
                    <tbody>
                      {reverseRows.map((row) => (
                        <tr key={row.id}>
                          <td>{row.parentItemNo}</td>
                          <td>{row.childItemNo}</td>
                          <td>{row.parentQuantity}</td>
                          <td>{row.childQuantity}</td>
                          <td>
                            <button
                              type="button"
                              className="btn-action"
                              onClick={() => filterByParent(row)}
                            >
                              모품목 필터
                            </button>
                          </td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                )}
                <div className="form-actions">
                  {reverseRows.length > 0 && (
                    <button
                      type="button"
                      onClick={() => void downloadReverseExcel(reverseItemNum, reverseRows)}
                    >
                      엑셀 저장
                    </button>
                  )}
                  <button type="button" className="secondary" onClick={closeModal}>
                    닫기
                  </button>
                </div>
              </>
            )}

            {modal === 'copy' && (
              <>
                <h2>BOM 복사 (직계 1레벨)</h2>
                <form onSubmit={onCopy} className="form-grid form-grid-wide">
                  <ItemSearchField
                    label="원본 모품목 *"
                    selectedItem={copySource}
                    onSelect={setCopySource}
                    allowedClassifications={PARENT_CLASSES}
                  />
                  <ItemSearchField
                    label="대상 모품목 *"
                    selectedItem={copyTarget}
                    onSelect={setCopyTarget}
                    allowedClassifications={PARENT_CLASSES}
                  />
                  <p className="hint-text full-width">대상에 동일 자품목이 있으면 건너뜁니다.</p>
                  <div className="form-actions">
                    <button type="submit" disabled={submitting}>
                      {submitting ? '복사 중…' : '복사'}
                    </button>
                    <button type="button" className="secondary" onClick={closeModal}>
                      취소
                    </button>
                  </div>
                </form>
              </>
            )}
          </div>
        </div>
      )}
    </div>
  );
}
