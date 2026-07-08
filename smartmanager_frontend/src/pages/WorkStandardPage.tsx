import { useCallback, useEffect, useState } from 'react';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import type { ProcessPlan, WorkCenter } from '../api/process';
import { fetchProcessPlans, fetchWorkCenters } from '../api/process';
import type { Equipment } from '../api/equipment';
import { fetchEquipment } from '../api/equipment';
import type { User } from '../api/user';
import { fetchUsers } from '../api/user';
import type { CreateWorkStandardRequest, WorkStandard } from '../api/workStandard';
import {
  copyWorkStandards,
  createWorkStandard,
  deleteWorkStandard,
  fetchWorkStandards,
  updateWorkStandard,
} from '../api/workStandard';

const INHOUSE_PROCESS = new Set(['INHOUSE', 'SPLIT']);

const emptyForm: CreateWorkStandardRequest = {
  itemNum: '',
  processSequenceId: 0,
  workCenterId: 0,
  priorityOrder: 1,
  setupTime: 0,
  standardTime: 0,
};

function formatProcessLabel(process: ProcessPlan): string {
  const wd =
    process.workDistinction === 'INHOUSE'
      ? '자가'
      : process.workDistinction === 'SPLIT'
        ? '자가/외주'
        : '외주';
  return `${process.processSequenceNum} · ${process.processCode} ${process.processName} (${wd})`;
}

function toUpdatePayload(standard: WorkStandard): Omit<CreateWorkStandardRequest, 'itemNum' | 'processSequenceId'> {
  return {
    workCenterId: standard.workCenterId,
    equipmentId: standard.equipmentId ?? undefined,
    priorityOrder: standard.priorityOrder,
    mainWorkerId: standard.mainWorkerId ?? undefined,
    toolName: standard.toolName ?? undefined,
    setupTime: standard.setupTime,
    standardTime: standard.standardTime,
  };
}

export default function WorkStandardPage() {
  const [workCenters, setWorkCenters] = useState<WorkCenter[]>([]);
  const [equipmentList, setEquipmentList] = useState<Equipment[]>([]);
  const [userList, setUserList] = useState<User[]>([]);
  const [processOptions, setProcessOptions] = useState<ProcessPlan[]>([]);
  const [standards, setStandards] = useState<WorkStandard[]>([]);
  const [formItem, setFormItem] = useState<ItemSearchSelection | null>(null);
  const [filterItem, setFilterItem] = useState<ItemSearchSelection | null>(null);
  const [form, setForm] = useState<CreateWorkStandardRequest>(emptyForm);
  const [copySource, setCopySource] = useState<ItemSearchSelection | null>(null);
  const [copyTarget, setCopyTarget] = useState<ItemSearchSelection | null>(null);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [editingStandard, setEditingStandard] = useState<WorkStandard | null>(null);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [copying, setCopying] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [copyMessage, setCopyMessage] = useState<string | null>(null);

  const isEditing = editingId !== null;
  const showItemColumn = filterItem === null;

  const refreshList = useCallback(async (item?: ItemSearchSelection | null) => {
    setLoading(true);
    setError(null);
    try {
      const target = item === undefined ? filterItem : item;
      setStandards(await fetchWorkStandards(target?.itemNo));
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
    } finally {
      setLoading(false);
    }
  }, [filterItem]);

  const loadProcessOptions = useCallback(async (item: ItemSearchSelection | null) => {
    if (!item) {
      setProcessOptions([]);
      return;
    }
    const processes = await fetchProcessPlans(item.id);
    setProcessOptions(processes.filter((p) => INHOUSE_PROCESS.has(p.workDistinction)));
  }, []);

  useEffect(() => {
    void (async () => {
      setLoading(true);
      setError(null);
      try {
        const [centers, allStandards, equipment, users] = await Promise.all([
          fetchWorkCenters(),
          fetchWorkStandards(),
          fetchEquipment(),
          fetchUsers(),
        ]);
        setWorkCenters(centers);
        setEquipmentList(equipment);
        setUserList(users);
        setStandards(allStandards);
        if (centers.length > 0) {
          setForm((prev) => (prev.workCenterId === 0 ? { ...prev, workCenterId: centers[0].id } : prev));
        }
      } catch (e) {
        setError(e instanceof Error ? e.message : '초기 로드 실패');
      } finally {
        setLoading(false);
      }
    })();
  }, []);

  useEffect(() => {
    if (formItem) {
      void loadProcessOptions(formItem).catch((e) => {
        setError(e instanceof Error ? e.message : '공정 목록 조회 실패');
      });
    } else {
      setProcessOptions([]);
    }
  }, [formItem, loadProcessOptions]);

  const resetForm = () => {
    setForm({
      ...emptyForm,
      workCenterId: workCenters[0]?.id ?? 0,
    });
    setFormItem(null);
    setEditingId(null);
    setEditingStandard(null);
  };

  const startEdit = (standard: WorkStandard) => {
    setEditingId(standard.id);
    setEditingStandard(standard);
    setFormItem({
      id: standard.itemId,
      itemNo: standard.itemNum,
      itemName: standard.itemName,
    });
    setForm({
      itemNum: standard.itemNum,
      processSequenceId: standard.processSequenceId,
      ...toUpdatePayload(standard),
    });
    setError(null);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!formItem) {
      setError('품목을 선택해 주세요.');
      return;
    }
    if (form.processSequenceId <= 0) {
      setError('공정을 선택해 주세요.');
      return;
    }
    if (form.workCenterId <= 0) {
      setError('작업장을 선택해 주세요.');
      return;
    }

    setSubmitting(true);
    setError(null);
    try {
      const payload = { ...form, itemNum: formItem.itemNo };
      if (isEditing && editingId !== null) {
        const { itemNum: _i, processSequenceId: _p, ...updatePayload } = payload;
        await updateWorkStandard(editingId, updatePayload);
      } else {
        await createWorkStandard(payload);
      }
      resetForm();
      await refreshList();
    } catch (err) {
      setError(err instanceof Error ? err.message : isEditing ? '수정 실패' : '등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (standard: WorkStandard) => {
    if (!window.confirm(`「${standard.itemNum} · ${standard.processName}」 작업표준을 삭제하시겠습니까?`)) {
      return;
    }
    setError(null);
    try {
      await deleteWorkStandard(standard.id);
      if (editingId === standard.id) {
        resetForm();
      }
      await refreshList();
    } catch (err) {
      setError(err instanceof Error ? err.message : '삭제 실패');
    }
  };

  const onCopy = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!copySource || !copyTarget) {
      setError('표준복사 원본·대상 품목을 모두 선택해 주세요.');
      return;
    }
    setCopying(true);
    setError(null);
    setCopyMessage(null);
    try {
      const copied = await copyWorkStandards({
        sourceItemNum: copySource.itemNo,
        targetItemNum: copyTarget.itemNo,
      });
      setCopyMessage(`${copied}건 복사되었습니다. (UK 충돌·공정 미매칭 행은 건너뜀)`);
      setCopySource(null);
      setCopyTarget(null);
      await refreshList();
    } catch (err) {
      setError(err instanceof Error ? err.message : '표준복사 실패');
    } finally {
      setCopying(false);
    }
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>작업표준 (Work Standard)</h1>
        <p>plan 9필드 CRUD · 자가·혼합 공정 · 표준복사</p>
      </header>

      {error && <div className="error">{error}</div>}
      {copyMessage && <p className="hint">{copyMessage}</p>}

      <section className="panel">
        <h2>표준복사</h2>
        <form onSubmit={onCopy} className="form-grid form-grid-wide">
          <ItemSearchField
            label="원본 품목 *"
            selectedItem={copySource}
            onSelect={setCopySource}
          />
          <ItemSearchField
            label="대상 품목 *"
            selectedItem={copyTarget}
            onSelect={setCopyTarget}
          />
          <div className="form-actions">
            <button type="submit" disabled={copying || !copySource || !copyTarget}>
              {copying ? '복사 중…' : '표준복사'}
            </button>
          </div>
        </form>
      </section>

      <section className="panel">
        <h2>{isEditing ? `작업표준 수정 (ID ${editingId})` : '작업표준 등록'}</h2>
        <form onSubmit={onSubmit} className="form-grid form-grid-wide">
          {isEditing && editingStandard ? (
            <>
              <ItemSearchField
                label="품목 *"
                selectedItem={formItem}
                onSelect={() => {}}
                disabled
              />
              <label>
                공정 *
                <input
                  readOnly
                  className="readonly"
                  value={`${editingStandard.processSequenceNum} · ${editingStandard.processCode} ${editingStandard.processName}`}
                />
              </label>
            </>
          ) : (
            <>
              <ItemSearchField
                label="품목 *"
                selectedItem={formItem}
                onSelect={(item) => {
                  setFormItem(item);
                  setForm((prev) => ({
                    ...prev,
                    itemNum: item?.itemNo ?? '',
                    processSequenceId: 0,
                  }));
                }}
              />
              <label>
                공정 * (자가·혼합)
                <select
                  required
                  disabled={!formItem}
                  value={form.processSequenceId || ''}
                  onChange={(e) => setForm({ ...form, processSequenceId: Number(e.target.value) })}
                >
                  <option value="">{formItem ? '공정 선택' : '품목을 먼저 선택'}</option>
                  {processOptions.map((p) => (
                    <option key={p.id} value={p.id}>
                      {formatProcessLabel(p)}
                    </option>
                  ))}
                </select>
              </label>
            </>
          )}
          <label>
            작업장 *
            <select
              required
              value={form.workCenterId || ''}
              onChange={(e) => setForm({ ...form, workCenterId: Number(e.target.value) })}
            >
              {workCenters.length === 0 ? (
                <option value="">작업장 없음</option>
              ) : (
                workCenters.map((wc) => (
                  <option key={wc.id} value={wc.id}>
                    {wc.wcName}
                  </option>
                ))
              )}
            </select>
          </label>
          <label>
            사용설비
            <select
              value={form.equipmentId ?? ''}
              onChange={(e) =>
                setForm({
                  ...form,
                  equipmentId: e.target.value ? Number(e.target.value) : undefined,
                })
              }
            >
              <option value="">(미지정)</option>
              {equipmentList.map((eq) => (
                <option key={eq.id} value={eq.id}>
                  {eq.equipmentNum} — {eq.equipmentName}
                </option>
              ))}
            </select>
          </label>
          <label>
            주작업자
            <select
              value={form.mainWorkerId ?? ''}
              onChange={(e) =>
                setForm({
                  ...form,
                  mainWorkerId: e.target.value ? Number(e.target.value) : undefined,
                })
              }
            >
              <option value="">(미지정)</option>
              {userList.map((user) => (
                <option key={user.id} value={user.id}>
                  {user.loginId} — {user.name}
                </option>
              ))}
            </select>
          </label>
          <label>
            우선순위 *
            <input
              required
              type="number"
              min={1}
              value={form.priorityOrder}
              onChange={(e) => setForm({ ...form, priorityOrder: Number(e.target.value) })}
            />
          </label>
          <label>
            사용공구
            <input
              value={form.toolName ?? ''}
              onChange={(e) => setForm({ ...form, toolName: e.target.value })}
            />
          </label>
          <label>
            셋업시간 (분) *
            <input
              required
              type="number"
              min={0}
              value={form.setupTime}
              onChange={(e) => setForm({ ...form, setupTime: Number(e.target.value) })}
            />
          </label>
          <label>
            표준시간 (초) *
            <input
              required
              type="number"
              min={0}
              value={form.standardTime}
              onChange={(e) => setForm({ ...form, standardTime: Number(e.target.value) })}
            />
          </label>
          <div className="form-actions">
            <button
              type="submit"
              disabled={submitting || workCenters.length === 0 || (!isEditing && !formItem)}
            >
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
        <h2>작업표준 목록</h2>
        <div className="search-row">
          <ItemSearchField
            label="품목 필터 (선택)"
            selectedItem={filterItem}
            onSelect={(item) => {
              setFilterItem(item);
              void refreshList(item);
            }}
            placeholder="전체 조회 — 품목번호 또는 품목명 입력"
          />
          <button
            type="button"
            className="secondary"
            disabled={loading}
            onClick={() => {
              setFilterItem(null);
              void refreshList(null);
            }}
          >
            전체
          </button>
        </div>
        {loading ? (
          <p>불러오는 중…</p>
        ) : standards.length === 0 ? (
          <p>등록된 작업표준이 없습니다.</p>
        ) : (
          <table>
            <thead>
              <tr>
                {showItemColumn && (
                  <>
                    <th>품목번호</th>
                    <th>품목명</th>
                  </>
                )}
                <th>순번</th>
                <th>공정</th>
                <th>작업장</th>
                <th>설비</th>
                <th>주작업자</th>
                <th>우선순위</th>
                <th>셋업(분)</th>
                <th>표준(초)</th>
                <th>작업</th>
              </tr>
            </thead>
            <tbody>
              {standards.map((ws) => (
                <tr key={ws.id} className={editingId === ws.id ? 'row-editing' : undefined}>
                  {showItemColumn && (
                    <>
                      <td>{ws.itemNum}</td>
                      <td>{ws.itemName}</td>
                    </>
                  )}
                  <td>{ws.processSequenceNum}</td>
                  <td>
                    {ws.processCode} {ws.processName}
                  </td>
                  <td>{ws.wcName}</td>
                  <td>{ws.equipmentName ?? '—'}</td>
                  <td>{ws.mainWorkerName ?? '—'}</td>
                  <td>{ws.priorityOrder}</td>
                  <td>{ws.setupTime}</td>
                  <td>{ws.standardTime}</td>
                  <td className="actions">
                    <button type="button" className="btn-action" onClick={() => startEdit(ws)}>
                      수정
                    </button>
                    <button type="button" className="btn-action danger" onClick={() => void onDelete(ws)}>
                      삭제
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </section>
    </div>
  );
}
