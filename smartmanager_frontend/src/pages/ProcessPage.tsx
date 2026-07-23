import { useCallback, useEffect, useMemo, useState } from 'react';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import type {
  CreateProcessRequest,
  ProcessPlan,
  WorkDistinction,
  CodeOption,
  WorkCenter,
} from '../api/process';
import {
  createProcessPlan,
  deleteProcessPlan,
  fetchProcessCodeOptions,
  fetchProcessPlans,
  fetchWorkCenters,
  updateProcessPlan,
} from '../api/process';
import { formatInteger } from '../utils/numberFormat';
import { useConfirm } from '../context/ConfirmContext';

const WORK_DISTINCTION_OPTIONS: { value: WorkDistinction; label: string }[] = [
  { value: 'INHOUSE', label: '자가' },
  { value: 'OUTSOURCE', label: '외주' },
  { value: 'SPLIT', label: '자가/외주' },
];

const emptyForm: CreateProcessRequest = {
  itemId: 0,
  processSequenceNum: 10,
  processCodeId: 0,
  workDistinction: 'INHOUSE',
  workCenterId: undefined,
  outsideOrderRate: 0,
  progressRate: 100,
};

function toForm(process: ProcessPlan): CreateProcessRequest {
  return {
    itemId: process.itemId,
    processSequenceNum: process.processSequenceNum,
    processCodeId: process.processCodeId,
    workDistinction: process.workDistinction,
    workCenterId: process.workCenterId ?? undefined,
    outsideOrderRate: process.outsideOrderRate,
    progressRate: process.progressRate,
  };
}

function toItemFromProcess(process: ProcessPlan): ItemSearchSelection {
  return {
    id: process.itemId,
    itemNo: process.itemNo,
    itemName: process.itemName,
  };
}

export default function ProcessPage() {
  const confirm = useConfirm();
  const [processCodes, setProcessCodes] = useState<CodeOption[]>([]);
  const [workCenters, setWorkCenters] = useState<WorkCenter[]>([]);
  const [processes, setProcesses] = useState<ProcessPlan[]>([]);
  const [formItem, setFormItem] = useState<ItemSearchSelection | null>(null);
  const [filterItem, setFilterItem] = useState<ItemSearchSelection | null>(null);
  const [form, setForm] = useState<CreateProcessRequest>(emptyForm);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const isEditing = editingId !== null;
  const showWorkCenter = form.workDistinction === 'INHOUSE' || form.workDistinction === 'SPLIT';
  const showOutsideOrderRate = form.workDistinction === 'SPLIT';
  const showItemColumn = filterItem === null;

  const processExportRows = useMemo(
    () =>
      processes.map((process) => {
        const row: Record<string, string | number> = {
          ID: process.id,
        };
        if (showItemColumn) {
          row['품목'] = `${process.itemNo} — ${process.itemName}`;
        }
        Object.assign(row, {
          순번: process.processSequenceNum,
          공정: `${process.processCode} — ${process.processName}`,
          작업구분:
            WORK_DISTINCTION_OPTIONS.find((option) => option.value === process.workDistinction)?.label ??
            process.workDistinction,
          작업장: process.wcName ?? '',
          '발주%': process.outsideOrderRate,
          '진척%': process.progressRate,
        });
        return row;
      }),
    [processes, showItemColumn],
  );

  const refreshProcessList = useCallback(async (item?: ItemSearchSelection | null) => {
    setLoading(true);
    setError(null);
    try {
      const target = item !== undefined ? item : filterItem;
      setProcesses(await fetchProcessPlans(target?.id));
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
    } finally {
      setLoading(false);
    }
  }, [filterItem]);

  useEffect(() => {
    void (async () => {
      try {
        const [codes, centers] = await Promise.all([
          fetchProcessCodeOptions(),
          fetchWorkCenters(),
        ]);
        setProcessCodes(codes);
        setWorkCenters(centers);
      } catch (e) {
        setError(e instanceof Error ? e.message : '공정 기준정보 조회 실패');
      }
    })();
  }, []);

  useEffect(() => {
    let cancelled = false;
    void (async () => {
      setLoading(true);
      setError(null);
      try {
        const data = await fetchProcessPlans(filterItem?.id);
        if (!cancelled) {
          setProcesses(data);
        }
      } catch (e) {
        if (!cancelled) {
          setError(e instanceof Error ? e.message : '공정 목록 조회 실패');
          setProcesses([]);
        }
      } finally {
        if (!cancelled) {
          setLoading(false);
        }
      }
    })();
    return () => {
      cancelled = true;
    };
  }, [filterItem?.id]);

  const resetForm = () => {
    setForm({
      ...emptyForm,
      processCodeId: processCodes[0]?.id ?? 0,
      workCenterId: workCenters[0]?.id,
    });
    setFormItem(null);
    setEditingId(null);
  };

  const startEdit = (process: ProcessPlan) => {
    setEditingId(process.id);
    setForm(toForm(process));
    setFormItem(toItemFromProcess(process));
    setError(null);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!formItem || !form.itemId) {
      setError('품목을 검색 후 선택해 주세요.');
      return;
    }
    if (!form.processCodeId) {
      setError('공정을 선택해 주세요.');
      return;
    }

    const payload: CreateProcessRequest = {
      ...form,
      itemId: formItem.id,
      workCenterId: showWorkCenter ? form.workCenterId : null,
      outsideOrderRate: showOutsideOrderRate ? form.outsideOrderRate ?? 0 : 0,
    };

    setSubmitting(true);
    setError(null);
    try {
      if (isEditing && editingId !== null) {
        await updateProcessPlan(editingId, payload);
      } else {
        await createProcessPlan(payload);
      }
      resetForm();
      await refreshProcessList();
    } catch (err) {
      setError(err instanceof Error ? err.message : isEditing ? '수정 실패' : '등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (process: ProcessPlan) => {
    if (!(await confirm(`순번 ${process.processSequenceNum} 공정을 삭제하시겠습니까?`, { title: '삭제 확인', confirmLabel: '삭제', cancelLabel: '닫기', danger: true }))) {
      return;
    }
    setError(null);
    try {
      await deleteProcessPlan(process.id);
      if (editingId === process.id) {
        resetForm();
      }
      await refreshProcessList();
    } catch (err) {
      setError(err instanceof Error ? err.message : '삭제 실패');
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>공정 (Process Plan)</h1>
        <p>제품·공정품 7필드 CRUD — 등록 시 공정창고 잔고 Lazy 생성</p>
      </header>

      {error && <div className="error">{error}</div>}

      <section className="panel">
        <h2>{isEditing ? `공정 수정 (ID ${editingId})` : '공정 등록'}</h2>
        <form onSubmit={onSubmit} className="form-grid form-grid-wide">
          <ItemSearchField
            label="품목 *"
            selectedItem={formItem}
            onSelect={(item) => {
              setFormItem(item);
              setForm({ ...form, itemId: item?.id ?? 0 });
            }}
          />
          <label>
            순서번호 *
            <input
              required
              type="number"
              min={1}
              max={98}
              step={1}
              value={form.processSequenceNum}
              onChange={(e) =>
                setForm({ ...form, processSequenceNum: Number(e.target.value) })
              }
            />
          </label>
          <label>
            공정 *
            <select
              required
              value={form.processCodeId || ''}
              onChange={(e) => setForm({ ...form, processCodeId: Number(e.target.value) })}
            >
              <option value="">선택</option>
              {processCodes.map((code) => (
                <option key={code.id} value={code.id}>
                  {code.code} — {code.name}
                </option>
              ))}
            </select>
          </label>
          <label>
            작업구분 *
            <select
              required
              value={form.workDistinction}
              onChange={(e) => {
                const workDistinction = e.target.value as WorkDistinction;
                setForm({
                  ...form,
                  workDistinction,
                  workCenterId:
                    workDistinction === 'OUTSOURCE' ? undefined : workCenters[0]?.id,
                  outsideOrderRate: workDistinction === 'SPLIT' ? 50 : 0,
                });
              }}
            >
              {WORK_DISTINCTION_OPTIONS.map((opt) => (
                <option key={opt.value} value={opt.value}>
                  {opt.label}
                </option>
              ))}
            </select>
          </label>
          {showWorkCenter && (
            <label>
              작업장 *
              <select
                required
                value={form.workCenterId ?? ''}
                onChange={(e) => setForm({ ...form, workCenterId: Number(e.target.value) })}
              >
                <option value="">선택</option>
                {workCenters.map((wc) => (
                  <option key={wc.id} value={wc.id}>
                    {wc.wcName}
                  </option>
                ))}
              </select>
            </label>
          )}
          {showOutsideOrderRate && (
            <label>
              발주비율(%) *
              <input
                required
                type="number"
                min={0}
                max={100}
                value={form.outsideOrderRate ?? 0}
                onChange={(e) =>
                  setForm({ ...form, outsideOrderRate: Number(e.target.value) })
                }
              />
              <span className="field-hint">0=전량 자가, 100=전량 외주 (운영 중 수정 가능)</span>
            </label>
          )}
          <label>
            진척비율(%) *
            <input
              required
              type="number"
              min={0}
              max={100}
              value={form.progressRate}
              onChange={(e) => setForm({ ...form, progressRate: Number(e.target.value) })}
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
        <div className="panel-header-row">
          <h2>공정 목록</h2>
          <GridExcelExportButton fileBaseName="공정목록" disabled={loading} rows={processExportRows} />
        </div>
        <div className="search-row">
          <ItemSearchField
            label="품목 필터 (선택)"
            selectedItem={filterItem}
            onSelect={(item) => {
              setFilterItem(item);
            }}
            placeholder="전체 조회 — 품목번호 또는 품목명 입력"
          />
          <button
            type="button"
            className="secondary"
            onClick={() => setFilterItem(null)}
          >
            전체
          </button>
        </div>
        {loading ? (
          <p>불러오는 중…</p>
        ) : processes.length === 0 ? (
          <p>등록된 공정이 없습니다.</p>
        ) : (
          <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th className="num">ID</th>
                {showItemColumn && <th>품목</th>}
                <th className="num">순번</th>
                <th>공정</th>
                <th>작업구분</th>
                <th>작업장</th>
                <th className="num">발주%</th>
                <th className="num">진척%</th>
                <th>작업</th>
              </tr>
            </thead>
            <tbody>
              {processes.map((process) => (
                <tr key={process.id} className={editingId === process.id ? 'row-editing' : undefined}>
                  <td className="num">{formatInteger(process.id)}</td>
                  {showItemColumn && (
                    <td>
                      {process.itemNo} — {process.itemName}
                    </td>
                  )}
                  <td className="num">{formatInteger(process.processSequenceNum)}</td>
                  <td>
                    {process.processCode} — {process.processName}
                  </td>
                  <td>
                    {WORK_DISTINCTION_OPTIONS.find((o) => o.value === process.workDistinction)?.label ??
                      process.workDistinction}
                  </td>
                  <td>{process.wcName ?? '—'}</td>
                  <td className="num">{formatInteger(process.outsideOrderRate)}</td>
                  <td className="num">{formatInteger(process.progressRate)}</td>
                  <td className="actions">
                    <button type="button" className="btn-action" onClick={() => startEdit(process)}>
                      수정
                    </button>
                    <button
                      type="button"
                      className="btn-action danger"
                      onClick={() => void onDelete(process)}
                    >
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
