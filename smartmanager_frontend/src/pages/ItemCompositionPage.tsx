import { useCallback, useEffect, useState } from 'react';
import type { PropertyClassification } from '../api/item';
import type {
  BomTreeNode,
  CreateItemCompositionRequest,
  ItemComposition,
} from '../api/itemComposition';
import {
  copyBom,
  createItemComposition,
  deleteItemComposition,
  fetchBomExplosion,
  fetchBomReverse,
  fetchItemCompositions,
  updateItemComposition,
} from '../api/itemComposition';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import { downloadExplosionExcel, downloadReverseExcel } from '../utils/bomExcelExport';

const PARENT_CLASSES: PropertyClassification[] = ['제품', '상품', '공정품'];
const CHILD_CLASSES: PropertyClassification[] = ['원자재', '공정품'];

type ModalKind = 'explosion' | 'reverse' | 'copy' | null;

function BomTreeRows({ node }: { node: BomTreeNode }) {
  return (
    <>
      <tr>
        <td style={{ paddingLeft: `${node.level * 1.25 + 0.5}rem` }}>
          L{node.level}
        </td>
        <td>{node.itemNum}</td>
        <td>{node.itemName}</td>
        <td>{node.propertyClassification}</td>
        <td>{node.quantity}</td>
      </tr>
      {node.children.map((child) => (
        <BomTreeRows key={`${child.itemNum}-${child.level}`} node={child} />
      ))}
    </>
  );
}

export default function ItemCompositionPage() {
  const [rows, setRows] = useState<ItemComposition[]>([]);
  const [filterParent, setFilterParent] = useState<ItemSearchSelection | null>(null);
  const [filterChild, setFilterChild] = useState<ItemSearchSelection | null>(null);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [toast, setToast] = useState<string | null>(null);

  const [modal, setModal] = useState<ModalKind>(null);
  const [editingId, setEditingId] = useState<number | null>(null);

  const [formParent, setFormParent] = useState<ItemSearchSelection | null>(null);
  const [formChild, setFormChild] = useState<ItemSearchSelection | null>(null);
  const [parentQty, setParentQty] = useState('1');
  const [childQty, setChildQty] = useState('1');

  const [explosionTree, setExplosionTree] = useState<BomTreeNode | null>(null);
  const [reverseRows, setReverseRows] = useState<ItemComposition[]>([]);
  const [reverseItemNum, setReverseItemNum] = useState('');

  const [copySource, setCopySource] = useState<ItemSearchSelection | null>(null);
  const [copyTarget, setCopyTarget] = useState<ItemSearchSelection | null>(null);

  const isEditing = editingId !== null;

  const load = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      setRows(
        await fetchItemCompositions(
          filterParent?.itemNo,
          filterChild?.itemNo,
        ),
      );
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
    } finally {
      setLoading(false);
    }
  }, [filterParent?.itemNo, filterChild?.itemNo]);

  useEffect(() => {
    void load();
  }, []);

  const closeModal = () => {
    setModal(null);
    setExplosionTree(null);
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

  const onSubmitForm = async (e: React.FormEvent) => {
    e.preventDefault();
    const pQty = Number(parentQty);
    const cQty = Number(childQty);
    if (!Number.isFinite(pQty) || pQty <= 0 || !Number.isFinite(cQty) || cQty <= 0) {
      setError('모품수량·자품수량은 0보다 커야 합니다.');
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
    if (!window.confirm(`「${row.parentItemNo} → ${row.childItemNo}」 BOM을 삭제하시겠습니까?`)) {
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
      setModal('explosion');
    } catch (err) {
      setError(err instanceof Error ? err.message : '정전개 실패');
    } finally {
      setSubmitting(false);
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

  const onCopy = async (e: React.FormEvent) => {
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
    void (async () => {
      setLoading(true);
      try {
        setRows(await fetchItemCompositions(row.parentItemNo, undefined));
      } catch (e) {
        setError(e instanceof Error ? e.message : '목록 조회 실패');
      } finally {
        setLoading(false);
      }
    })();
  };

  useEffect(() => {
    if (!toast) return;
    const timer = setTimeout(() => setToast(null), 3000);
    return () => clearTimeout(timer);
  }, [toast]);

  return (
    <>
      <header>
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
              <label>
                모품목 *
                <input
                  readOnly
                  className="readonly"
                  value={formParent ? `${formParent.itemNo} — ${formParent.itemName}` : ''}
                />
              </label>
              <label>
                자품목 *
                <input
                  readOnly
                  className="readonly"
                  value={formChild ? `${formChild.itemNo} — ${formChild.itemName}` : ''}
                />
              </label>
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
              min="0.0001"
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
        <h2>품목구성 목록</h2>
        <div className="bom-toolbar">
          <div className="bom-filter-bar">
            <ItemSearchField
              label="모품목"
              selectedItem={filterParent}
              onSelect={setFilterParent}
              allowedClassifications={PARENT_CLASSES}
              placeholder="품목번호 또는 품목명"
            />
            <ItemSearchField
              label="자품목"
              selectedItem={filterChild}
              onSelect={setFilterChild}
              allowedClassifications={CHILD_CLASSES}
              placeholder="품목번호 또는 품목명"
            />
            <button type="button" className="bom-search-btn" onClick={() => void load()}>
              검색
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
          <table>
            <thead>
              <tr>
                <th>모품목</th>
                <th>모품목명</th>
                <th>자품목</th>
                <th>자품목명</th>
                <th>모품수량</th>
                <th>자품수량</th>
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
                  <td>{row.parentQuantity}</td>
                  <td>{row.childQuantity}</td>
                  <td className="actions">
                    <button type="button" className="link" onClick={() => startEdit(row)}>
                      수정
                    </button>
                    <button type="button" className="link danger" onClick={() => void onDelete(row)}>
                      삭제
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
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
                <h2>BOM 정전개 — {explosionTree.itemNum}</h2>
                <table>
                  <thead>
                    <tr>
                      <th>레벨</th>
                      <th>품목번호</th>
                      <th>품목명</th>
                      <th>자산분류</th>
                      <th>누적수량</th>
                    </tr>
                  </thead>
                  <tbody>
                    <BomTreeRows node={explosionTree} />
                  </tbody>
                </table>
                <div className="form-actions">
                  <button
                    type="button"
                    onClick={() => downloadExplosionExcel(explosionTree)}
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
                <h2>BOM 역전개 (1레벨)</h2>
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
                              className="link"
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
                      onClick={() => downloadReverseExcel(reverseItemNum, reverseRows)}
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
    </>
  );
}
