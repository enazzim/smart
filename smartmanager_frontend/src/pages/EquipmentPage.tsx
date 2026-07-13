import { useEffect, useMemo, useState } from 'react';
import type { CodeOption } from '../api/equipment';
import { fetchEquipmentCategories } from '../api/equipment';
import type { CreateEquipmentRequest, Equipment } from '../api/equipment';
import {
  createEquipment,
  deleteEquipment,
  fetchEquipment,
  updateEquipment,
} from '../api/equipment';
import type { WorkCenter } from '../api/process';
import { fetchWorkCenters } from '../api/process';
import GridExcelExportButton from '../components/GridExcelExportButton';
import { formatInteger } from '../utils/numberFormat';
import { useConfirm } from '../context/ConfirmContext';

const emptyForm: CreateEquipmentRequest = {
  equipmentNum: '',
  equipmentName: '',
  equipmentCategoryId: 0,
  designShot: 0,
  initialShot: 0,
};

function formatCategoryLabel(code: string, name: string): string {
  return name ? `${code} · ${name}` : code;
}

export default function EquipmentPage() {
  const confirm = useConfirm();
  const [categories, setCategories] = useState<CodeOption[]>([]);
  const [workCenters, setWorkCenters] = useState<WorkCenter[]>([]);
  const [equipmentList, setEquipmentList] = useState<Equipment[]>([]);
  const [form, setForm] = useState<CreateEquipmentRequest>(emptyForm);
  const [searchQuery, setSearchQuery] = useState('');
  const [editingId, setEditingId] = useState<number | null>(null);
  const [editingNum, setEditingNum] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const isEditing = editingId !== null;

  const equipmentExportRows = useMemo(
    () =>
      equipmentList.map((eq) => ({
        번호: eq.equipmentNum,
        설비명: eq.equipmentName,
        분류: eq.equipmentCategoryName,
        작업장: eq.wcName ?? '',
        설계샷: eq.designShot,
        누계샷: eq.accumulatedShot,
        작업샷: eq.workShot,
        교체: eq.replacementDue ? '필요' : '',
      })),
    [equipmentList],
  );

  const load = async (query = searchQuery) => {
    setLoading(true);
    setError(null);
    try {
      setEquipmentList(await fetchEquipment(query || undefined));
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    void (async () => {
      try {
        const [cats, centers] = await Promise.all([fetchEquipmentCategories(), fetchWorkCenters()]);
        setCategories(cats);
        setWorkCenters(centers);
        if (cats.length > 0) {
          setForm((prev) =>
            prev.equipmentCategoryId === 0 ? { ...prev, equipmentCategoryId: cats[0].id } : prev,
          );
        }
      } catch (e) {
        setError(e instanceof Error ? e.message : '초기 로드 실패');
      }
    })();
    void load();
  }, []);

  const resetForm = () => {
    setForm({
      ...emptyForm,
      equipmentCategoryId: categories[0]?.id ?? 0,
    });
    setEditingId(null);
    setEditingNum(null);
  };

  const startEdit = (eq: Equipment) => {
    setEditingId(eq.id);
    setEditingNum(eq.equipmentNum);
    setForm({
      equipmentNum: eq.equipmentNum,
      equipmentName: eq.equipmentName,
      equipmentCategoryId: eq.equipmentCategoryId,
      workCenterId: eq.workCenterId ?? undefined,
      designShot: eq.designShot,
      initialShot: eq.initialShot,
    });
    setError(null);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (form.equipmentCategoryId <= 0) {
      setError('설비분류를 선택해 주세요.');
      return;
    }
    setSubmitting(true);
    setError(null);
    try {
      const payload = {
        ...form,
        workCenterId: form.workCenterId || null,
      };
      if (isEditing && editingId !== null) {
        const { equipmentNum: _n, ...updatePayload } = payload;
        await updateEquipment(editingId, updatePayload);
      } else {
        await createEquipment(payload);
      }
      resetForm();
      await load();
    } catch (err) {
      setError(err instanceof Error ? err.message : isEditing ? '수정 실패' : '등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (eq: Equipment) => {
    if (!(await confirm(`「${eq.equipmentName}」 설비를 삭제하시겠습니까?`, { title: '삭제 확인', confirmLabel: '삭제', cancelLabel: '닫기', danger: true }))) {
      return;
    }
    setError(null);
    try {
      await deleteEquipment(eq.id);
      if (editingId === eq.id) {
        resetForm();
      }
      await load();
    } catch (err) {
      setError(err instanceof Error ? err.message : '삭제 실패');
    }
  };

  const onSearch = (e: React.FormEvent) => {
    e.preventDefault();
    void load(searchQuery);
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>설비 (Equipment)</h1>
        <p>6필드 CRUD · 샷 수명·교체 필요 표시</p>
      </header>

      {error && <div className="error">{error}</div>}

      <section className="panel">
        <h2>{isEditing ? `설비 수정 (ID ${editingId})` : '설비 등록'}</h2>
        <form onSubmit={onSubmit} className="form-grid form-grid-wide">
          <label>
            설비번호 *
            <input
              required
              readOnly={isEditing}
              value={isEditing ? (editingNum ?? form.equipmentNum) : form.equipmentNum}
              onChange={(e) => setForm({ ...form, equipmentNum: e.target.value })}
              className={isEditing ? 'readonly' : undefined}
            />
          </label>
          <label>
            설비명 *
            <input
              required
              value={form.equipmentName}
              onChange={(e) => setForm({ ...form, equipmentName: e.target.value })}
            />
          </label>
          <label>
            설비분류 *
            <select
              required
              value={form.equipmentCategoryId || ''}
              onChange={(e) => setForm({ ...form, equipmentCategoryId: Number(e.target.value) })}
            >
              {categories.length === 0 ? (
                <option value="">설비분류 없음</option>
              ) : (
                categories.map((cat) => (
                  <option key={cat.id} value={cat.id}>
                    {formatCategoryLabel(cat.code, cat.name)}
                  </option>
                ))
              )}
            </select>
          </label>
          <label>
            작업장
            <select
              value={form.workCenterId ?? ''}
              onChange={(e) =>
                setForm({
                  ...form,
                  workCenterId: e.target.value ? Number(e.target.value) : undefined,
                })
              }
            >
              <option value="">(미지정)</option>
              {workCenters.map((wc) => (
                <option key={wc.id} value={wc.id}>
                  {wc.wcName}
                </option>
              ))}
            </select>
          </label>
          <label>
            설계샷 *
            <input
              required
              type="number"
              min={0}
              value={form.designShot}
              onChange={(e) => setForm({ ...form, designShot: Number(e.target.value) })}
            />
          </label>
          <label>
            초기샷 *
            <input
              required
              type="number"
              min={0}
              value={form.initialShot}
              onChange={(e) => setForm({ ...form, initialShot: Number(e.target.value) })}
            />
          </label>
          {isEditing && editingId !== null && (
            <>
              <label>
                작업샷 (읽기 전용)
                <input
                  readOnly
                  className="readonly"
                  value={equipmentList.find((eq) => eq.id === editingId)?.workShot ?? 0}
                />
              </label>
              <label>
                누계샷 (읽기 전용)
                <input
                  readOnly
                  className="readonly"
                  value={equipmentList.find((eq) => eq.id === editingId)?.accumulatedShot ?? 0}
                />
              </label>
            </>
          )}
          <div className="form-actions">
            <button type="submit" disabled={submitting || categories.length === 0}>
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
        <div className="panel-header-row">
          <h2>설비 목록</h2>
          <GridExcelExportButton fileBaseName="설비목록" disabled={loading} rows={equipmentExportRows} />
        </div>
        <form onSubmit={onSearch} className="search-row">
          <label>
            설비번호·설비명 검색
            <input
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
              placeholder="부분 일치"
            />
          </label>
          <button type="submit" disabled={loading}>
            검색
          </button>
          <button
            type="button"
            className="secondary"
            disabled={loading}
            onClick={() => {
              setSearchQuery('');
              void load('');
            }}
          >
            초기화
          </button>
        </form>
        {loading ? (
          <p>불러오는 중…</p>
        ) : equipmentList.length === 0 ? (
          <p>등록된 설비가 없습니다.</p>
        ) : (
          <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>번호</th>
                <th>설비명</th>
                <th>분류</th>
                <th>작업장</th>
                <th className="num">설계샷</th>
                <th className="num">누계샷</th>
                <th className="num">작업샷</th>
                <th>교체</th>
                <th>작업</th>
              </tr>
            </thead>
            <tbody>
              {equipmentList.map((eq) => (
                <tr key={eq.id} className={editingId === eq.id ? 'row-editing' : undefined}>
                  <td>{eq.equipmentNum}</td>
                  <td>{eq.equipmentName}</td>
                  <td>{eq.equipmentCategoryName}</td>
                  <td>{eq.wcName ?? '—'}</td>
                  <td className="num">{formatInteger(eq.designShot)}</td>
                  <td className="num">{formatInteger(eq.accumulatedShot)}</td>
                  <td className="num">{formatInteger(eq.workShot)}</td>
                  <td>{eq.replacementDue ? '필요' : '—'}</td>
                  <td className="actions">
                    <button type="button" className="btn-action" onClick={() => startEdit(eq)}>
                      수정
                    </button>
                    <button type="button" className="btn-action danger" onClick={() => void onDelete(eq)}>
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
    </div>
  );
}
