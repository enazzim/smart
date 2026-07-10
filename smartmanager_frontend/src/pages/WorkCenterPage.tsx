import { useEffect, useMemo, useState } from 'react';
import type { CodeOption } from '../api/process';
import { fetchProcessCodeOptions } from '../api/process';
import type { CreateWorkCenterRequest, WorkCenter } from '../api/workCenter';
import {
  createWorkCenter,
  deleteWorkCenter,
  fetchWorkCenters,
  updateWorkCenter,
} from '../api/workCenter';
import GridExcelExportButton from '../components/GridExcelExportButton';
import { formatInteger } from '../utils/numberFormat';

const emptyForm: CreateWorkCenterRequest = {
  wcName: '',
  mainProcessCodeId: 0,
  operationTime: 480,
};

function formatProcessLabel(code: string, name: string): string {
  return name ? `${code} · ${name}` : code;
}

export default function WorkCenterPage() {
  const [workCenters, setWorkCenters] = useState<WorkCenter[]>([]);
  const [processCodes, setProcessCodes] = useState<CodeOption[]>([]);
  const [form, setForm] = useState<CreateWorkCenterRequest>(emptyForm);
  const [searchQuery, setSearchQuery] = useState('');
  const [editingId, setEditingId] = useState<number | null>(null);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const isEditing = editingId !== null;

  const workCenterExportRows = useMemo(
    () =>
      workCenters.map((wc) => ({
        ID: wc.id,
        작업장명: wc.wcName,
        대표공정: formatProcessLabel(wc.mainProcessCode, wc.mainProcessName),
        '가동시간(분)': wc.operationTime,
      })),
    [workCenters],
  );

  const load = async (query = searchQuery) => {
    setLoading(true);
    setError(null);
    try {
      setWorkCenters(await fetchWorkCenters(query || undefined));
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    void (async () => {
      try {
        const options = await fetchProcessCodeOptions();
        setProcessCodes(options);
        if (options.length > 0) {
          setForm((prev) =>
            prev.mainProcessCodeId === 0 ? { ...prev, mainProcessCodeId: options[0].id } : prev,
          );
        }
      } catch (e) {
        setError(e instanceof Error ? e.message : '공정코드 조회 실패');
      }
    })();
    void load();
  }, []);

  const resetForm = () => {
    setForm({
      ...emptyForm,
      mainProcessCodeId: processCodes[0]?.id ?? 0,
    });
    setEditingId(null);
  };

  const startEdit = (wc: WorkCenter) => {
    setEditingId(wc.id);
    setForm({
      wcName: wc.wcName,
      mainProcessCodeId: wc.mainProcessCodeId,
      operationTime: wc.operationTime,
    });
    setError(null);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (form.mainProcessCodeId <= 0) {
      setError('대표공정을 선택해 주세요.');
      return;
    }
    setSubmitting(true);
    setError(null);
    try {
      if (isEditing && editingId !== null) {
        await updateWorkCenter(editingId, form);
      } else {
        await createWorkCenter(form);
      }
      resetForm();
      await load();
    } catch (err) {
      setError(err instanceof Error ? err.message : isEditing ? '수정 실패' : '등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (wc: WorkCenter) => {
    if (!window.confirm(`「${wc.wcName}」 작업장을 삭제하시겠습니까?`)) {
      return;
    }
    setError(null);
    try {
      await deleteWorkCenter(wc.id);
      if (editingId === wc.id) {
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
        <h1>작업장 (Work Center)</h1>
        <p>3필드 CRUD — 대표공정·일일 가동시간(분)</p>
      </header>

      {error && <div className="error">{error}</div>}

      <section className="panel">
        <h2>{isEditing ? `작업장 수정 (ID ${editingId})` : '작업장 등록'}</h2>
        <form onSubmit={onSubmit} className="form-grid form-grid-wide">
          <label>
            작업장명 *
            <input
              required
              value={form.wcName}
              onChange={(e) => setForm({ ...form, wcName: e.target.value })}
            />
          </label>
          <label>
            대표공정 *
            <select
              required
              value={form.mainProcessCodeId || ''}
              onChange={(e) => setForm({ ...form, mainProcessCodeId: Number(e.target.value) })}
            >
              {processCodes.length === 0 ? (
                <option value="">공정코드 없음</option>
              ) : (
                processCodes.map((opt) => (
                  <option key={opt.id} value={opt.id}>
                    {formatProcessLabel(opt.code, opt.name)}
                  </option>
                ))
              )}
            </select>
          </label>
          <label>
            일일 가동시간 (분) *
            <input
              required
              type="number"
              min={1}
              max={1440}
              value={form.operationTime}
              onChange={(e) => setForm({ ...form, operationTime: Number(e.target.value) })}
            />
          </label>
          <div className="form-actions">
            <button type="submit" disabled={submitting || processCodes.length === 0}>
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
          <h2>작업장 목록</h2>
          <GridExcelExportButton fileBaseName="작업장목록" disabled={loading} rows={workCenterExportRows} />
        </div>
        <form onSubmit={onSearch} className="search-row">
          <label>
            작업장명 검색
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
        ) : workCenters.length === 0 ? (
          <p>등록된 작업장이 없습니다.</p>
        ) : (
          <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th className="num">ID</th>
                <th>작업장명</th>
                <th>대표공정</th>
                <th className="num">가동시간(분)</th>
                <th>작업</th>
              </tr>
            </thead>
            <tbody>
              {workCenters.map((wc) => (
                <tr key={wc.id} className={editingId === wc.id ? 'row-editing' : undefined}>
                  <td className="num">{formatInteger(wc.id)}</td>
                  <td>{wc.wcName}</td>
                  <td>{formatProcessLabel(wc.mainProcessCode, wc.mainProcessName)}</td>
                  <td className="num">{formatInteger(wc.operationTime)}</td>
                  <td className="actions">
                    <button type="button" className="btn-action" onClick={() => startEdit(wc)}>
                      수정
                    </button>
                    <button type="button" className="btn-action danger" onClick={() => void onDelete(wc)}>
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
