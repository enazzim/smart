import { useEffect, useState } from 'react';
import type {
  CheckDistinction,
  CreateItemRequest,
  Item,
  PropertyClassification,
  UpdateItemRequest,
} from '../api/item';
import { createItem, deleteItem, fetchItems, updateItem } from '../api/item';

const PROPERTY_OPTIONS: PropertyClassification[] = ['원자재', '제품', '상품', '공정품'];

const CHECK_OPTIONS: { value: CheckDistinction; label: string }[] = [
  { value: 'NONE', label: '무검사' },
  { value: 'INSPECTION', label: '검사' },
];

const emptyForm: CreateItemRequest = {
  itemNo: '',
  itemName: '',
  propertyClassification: '제품',
  unit: 'EA',
};

function toUpdatePayload(item: Item): UpdateItemRequest {
  return {
    itemName: item.itemName,
    propertyClassification: item.propertyClassification,
    unit: item.unit,
    standard: item.standard ?? undefined,
    standardUnitCost: item.standardUnitCost ?? undefined,
    checkDistinction: item.checkDistinction ?? undefined,
    leadTime: item.leadTime ?? undefined,
    safetyStockQuantity: item.safetyStockQuantity ?? undefined,
    orderIntervalQuantity: item.orderIntervalQuantity ?? undefined,
    minOrderQuantity: item.minOrderQuantity ?? undefined,
  };
}

function parseOptionalNumber(value: string): number | undefined {
  if (value.trim() === '') return undefined;
  const n = Number(value);
  return Number.isNaN(n) ? undefined : n;
}

export default function ItemPage() {
  const [items, setItems] = useState<Item[]>([]);
  const [form, setForm] = useState<CreateItemRequest>(emptyForm);
  const [searchItemNo, setSearchItemNo] = useState('');
  const [searchItemName, setSearchItemName] = useState('');
  const [editingId, setEditingId] = useState<number | null>(null);
  const [editingItemNo, setEditingItemNo] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const isEditing = editingId !== null;

  const load = async (itemNo = searchItemNo, itemName = searchItemName) => {
    setLoading(true);
    setError(null);
    try {
      setItems(await fetchItems(itemNo || undefined, itemName || undefined));
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    void load();
  }, []);

  const resetForm = () => {
    setForm(emptyForm);
    setEditingId(null);
    setEditingItemNo(null);
  };

  const startEdit = (item: Item) => {
    setEditingId(item.id);
    setEditingItemNo(item.itemNo);
    setForm({ ...toUpdatePayload(item), itemNo: item.itemNo });
    setError(null);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSubmitting(true);
    setError(null);
    try {
      if (isEditing && editingId !== null) {
        const { itemNo: _ignored, ...updatePayload } = form;
        await updateItem(editingId, updatePayload);
      } else {
        await createItem(form);
      }
      resetForm();
      await load();
    } catch (err) {
      setError(err instanceof Error ? err.message : isEditing ? '수정 실패' : '등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (item: Item) => {
    if (!window.confirm(`「${item.itemName}」 품목을 삭제하시겠습니까?`)) {
      return;
    }
    setError(null);
    try {
      await deleteItem(item.id);
      if (editingId === item.id) {
        resetForm();
      }
      await load();
    } catch (err) {
      setError(err instanceof Error ? err.message : '삭제 실패');
    }
  };

  return (
    <>
      <header>
        <h1>품목 (Item)</h1>
        <p>11필드 CRUD — 등록 시 재고 행 미생성 (Lazy)</p>
      </header>

      {error && <div className="error">{error}</div>}

      <section className="panel">
        <h2>{isEditing ? `품목 수정 (ID ${editingId})` : '품목 등록'}</h2>
        <form onSubmit={onSubmit} className="form-grid form-grid-wide">
          <label>
            품목번호 *
            <input
              required
              readOnly={isEditing}
              value={isEditing ? (editingItemNo ?? form.itemNo) : form.itemNo}
              onChange={(e) => setForm({ ...form, itemNo: e.target.value })}
              className={isEditing ? 'readonly' : undefined}
            />
          </label>
          <label>
            품목명 *
            <input
              required
              value={form.itemName}
              onChange={(e) => setForm({ ...form, itemName: e.target.value })}
            />
          </label>
          <label>
            자산분류 *
            <select
              required
              value={form.propertyClassification}
              onChange={(e) =>
                setForm({ ...form, propertyClassification: e.target.value as PropertyClassification })
              }
            >
              {PROPERTY_OPTIONS.map((opt) => (
                <option key={opt} value={opt}>
                  {opt}
                </option>
              ))}
            </select>
          </label>
          <label>
            단위 *
            <input
              required
              value={form.unit}
              onChange={(e) => setForm({ ...form, unit: e.target.value })}
            />
          </label>
          <label>
            규격
            <input
              value={form.standard ?? ''}
              onChange={(e) => setForm({ ...form, standard: e.target.value })}
            />
          </label>
          <label>
            기준단가
            <input
              type="number"
              min="0"
              step="0.01"
              value={form.standardUnitCost ?? ''}
              onChange={(e) =>
                setForm({ ...form, standardUnitCost: parseOptionalNumber(e.target.value) })
              }
            />
          </label>
          <label>
            검사구분
            <select
              value={form.checkDistinction ?? ''}
              onChange={(e) =>
                setForm({
                  ...form,
                  checkDistinction: (e.target.value || undefined) as CheckDistinction | undefined,
                })
              }
            >
              <option value="">(없음)</option>
              {CHECK_OPTIONS.map((opt) => (
                <option key={opt.value} value={opt.value}>
                  {opt.label}
                </option>
              ))}
            </select>
          </label>
          <label>
            리드타임(일)
            <input
              type="number"
              min="0"
              value={form.leadTime ?? ''}
              onChange={(e) => setForm({ ...form, leadTime: parseOptionalNumber(e.target.value) })}
            />
          </label>
          <label>
            안전재고량
            <input
              type="number"
              min="0"
              step="0.0001"
              value={form.safetyStockQuantity ?? ''}
              onChange={(e) =>
                setForm({ ...form, safetyStockQuantity: parseOptionalNumber(e.target.value) })
              }
            />
          </label>
          <label>
            발주간격수량
            <input
              type="number"
              min="0"
              step="0.0001"
              value={form.orderIntervalQuantity ?? ''}
              onChange={(e) =>
                setForm({ ...form, orderIntervalQuantity: parseOptionalNumber(e.target.value) })
              }
            />
          </label>
          <label>
            최소발주량
            <input
              type="number"
              min="0"
              step="0.0001"
              value={form.minOrderQuantity ?? ''}
              onChange={(e) =>
                setForm({ ...form, minOrderQuantity: parseOptionalNumber(e.target.value) })
              }
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
      </section>

      <section className="panel">
        <h2>품목 목록</h2>
        <div className="search-row">
          <label>
            품목번호
            <input value={searchItemNo} onChange={(e) => setSearchItemNo(e.target.value)} />
          </label>
          <label>
            품목명
            <input value={searchItemName} onChange={(e) => setSearchItemName(e.target.value)} />
          </label>
          <button type="button" onClick={() => void load()}>
            검색
          </button>
        </div>
        {loading ? (
          <p>불러오는 중…</p>
        ) : items.length === 0 ? (
          <p>등록된 품목이 없습니다.</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>ID</th>
                <th>품목번호</th>
                <th>품목명</th>
                <th>자산분류</th>
                <th>단위</th>
                <th>규격</th>
                <th>작업</th>
              </tr>
            </thead>
            <tbody>
              {items.map((item) => (
                <tr key={item.id} className={editingId === item.id ? 'row-editing' : undefined}>
                  <td>{item.id}</td>
                  <td>{item.itemNo}</td>
                  <td>{item.itemName}</td>
                  <td>{item.propertyClassification}</td>
                  <td>{item.unit}</td>
                  <td>{item.standard ?? ''}</td>
                  <td className="actions">
                    <button type="button" className="btn-action" onClick={() => startEdit(item)}>
                      수정
                    </button>
                    <button type="button" className="btn-action danger" onClick={() => void onDelete(item)}>
                      삭제
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </section>
    </>
  );
}
