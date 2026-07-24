import { useCallback, useEffect, useMemo, useState } from 'react';
import ItemSearchField, { type ItemSearchSelection } from '../components/ItemSearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import VirtualMasterTable from '../components/VirtualMasterTable';
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
import { formatInteger } from '../utils/numberFormat';
import { NON_RAW_ITEM_CLASSES } from '../utils/itemClassFilters';
import { useConfirm } from '../context/ConfirmContext';

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
  const confirm = useConfirm();
  const [workCenters, setWorkCenters] = useState<WorkCenter[]>([]);
  const [equipmentList, setEquipmentList] = useState<Equipment[]>([]);
  const [userList, setUserList] = useState<User[]>([]);
  const [processOptions, setProcessOptions] = useState<ProcessPlan[]>([]);
  const [standards, setStandards] = useState<WorkStandard[]>([]);
  const [draftItem, setDraftItem] = useState<ItemSearchSelection | null>(null);
  const [appliedItem, setAppliedItem] = useState<ItemSearchSelection | null>(null);
  const [hasSearched, setHasSearched] = useState(false);
  const [clearToken, setClearToken] = useState(0);
  const [formItem, setFormItem] = useState<ItemSearchSelection | null>(null);
  const [form, setForm] = useState<CreateWorkStandardRequest>(emptyForm);
  const [copySource, setCopySource] = useState<ItemSearchSelection | null>(null);
  const [copyTarget, setCopyTarget] = useState<ItemSearchSelection | null>(null);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [editingStandard, setEditingStandard] = useState<WorkStandard | null>(null);
  const [loading, setLoading] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [copying, setCopying] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [copyMessage, setCopyMessage] = useState<string | null>(null);

  const isEditing = editingId !== null;
  const showItemColumn = appliedItem === null;

  const workStandardExportRows = useMemo(
    () =>
      standards.map((ws) => {
        const row: Record<string, string | number> = {};
        if (showItemColumn) {
          row['품목번호'] = ws.itemNum;
          row['품목명'] = ws.itemName;
        }
        Object.assign(row, {
          순번: ws.processSequenceNum,
          공정: `${ws.processCode} ${ws.processName}`,
          작업장: ws.wcName,
          설비: ws.equipmentName ?? '',
          주작업자: ws.mainWorkerName ?? '',
          우선순위: ws.priorityOrder,
          '셋업(분)': ws.setupTime,
          '표준(초)': ws.standardTime,
        });
        return row;
      }),
    [standards, showItemColumn],
  );

  const refreshListIfSearched = useCallback(async () => {
    if (!hasSearched) {
      return;
    }
    setLoading(true);
    setError(null);
    try {
      setStandards(await fetchWorkStandards(appliedItem?.itemNo));
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
    } finally {
      setLoading(false);
    }
  }, [hasSearched, appliedItem?.itemNo]);

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
      setError(null);
      try {
        const [centers, equipment, users] = await Promise.all([
          fetchWorkCenters(),
          fetchEquipment(),
          fetchUsers(),
        ]);
        setWorkCenters(centers);
        setEquipmentList(equipment);
        setUserList(users);
        if (centers.length > 0) {
          setForm((prev) => (prev.workCenterId === 0 ? { ...prev, workCenterId: centers[0].id } : prev));
        }
      } catch (e) {
        setError(e instanceof Error ? e.message : '초기 로드 실패');
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

  const onSearch = async () => {
    setLoading(true);
    setError(null);
    try {
      setStandards(await fetchWorkStandards(draftItem?.itemNo));
      setAppliedItem(draftItem);
      setHasSearched(true);
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
      setStandards([]);
    } finally {
      setLoading(false);
    }
  };

  const onResetSearch = () => {
    setDraftItem(null);
    setAppliedItem(null);
    setStandards([]);
    setHasSearched(false);
    setClearToken((token) => token + 1);
    setError(null);
  };

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
      await refreshListIfSearched();
    } catch (err) {
      setError(err instanceof Error ? err.message : isEditing ? '수정 실패' : '등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (standard: WorkStandard) => {
    if (!(await confirm(`「${standard.itemNum} · ${standard.processName}」 작업표준을 삭제하시겠습니까?`, { title: '삭제 확인', confirmLabel: '삭제', cancelLabel: '닫기', danger: true }))) {
      return;
    }
    setError(null);
    try {
      await deleteWorkStandard(standard.id);
      if (editingId === standard.id) {
        resetForm();
      }
      await refreshListIfSearched();
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
      await refreshListIfSearched();
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
        <div className="panel-header-row">
          <h2>작업표준 목록</h2>
          <GridExcelExportButton fileBaseName="작업표준목록" disabled={loading} rows={workStandardExportRows} />
        </div>
        <div className="search-row">
          <ItemSearchField
            label="품목 (선택)"
            selectedItem={draftItem}
            onSelect={setDraftItem}
            allowedClassifications={NON_RAW_ITEM_CLASSES}
            clearToken={clearToken}
            placeholder="품목번호 또는 품목명 입력"
          />
          <button type="button" disabled={loading} onClick={() => void onSearch()}>
            조회
          </button>
          <button type="button" className="secondary" disabled={loading} onClick={onResetSearch}>
            초기화
          </button>
        </div>
        {!hasSearched ? (
          <p className="hint-text">조회 버튼을 누르면 목록이 표시됩니다.</p>
        ) : loading ? (
          <p>불러오는 중…</p>
        ) : standards.length === 0 ? (
          <p>등록된 작업표준이 없습니다.</p>
        ) : (
          <VirtualMasterTable
            rows={standards}
            columnCount={showItemColumn ? 11 : 9}
            getRowKey={(ws) => ws.id}
            renderHeader={() => (
              <tr>
                {showItemColumn && (
                  <>
                    <th>품목번호</th>
                    <th>품목명</th>
                  </>
                )}
                <th className="num">순번</th>
                <th>공정</th>
                <th>작업장</th>
                <th>설비</th>
                <th>주작업자</th>
                <th className="num">우선순위</th>
                <th className="num">셋업(분)</th>
                <th className="num">표준(초)</th>
                <th>작업</th>
              </tr>
            )}
            renderRow={(ws) => (
              <tr className={editingId === ws.id ? 'row-editing' : undefined}>
                {showItemColumn && (
                  <>
                    <td>{ws.itemNum}</td>
                    <td>{ws.itemName}</td>
                  </>
                )}
                <td className="num">{formatInteger(ws.processSequenceNum)}</td>
                <td>
                  {ws.processCode} {ws.processName}
                </td>
                <td>{ws.wcName}</td>
                <td>{ws.equipmentName ?? '—'}</td>
                <td>{ws.mainWorkerName ?? '—'}</td>
                <td className="num">{formatInteger(ws.priorityOrder)}</td>
                <td className="num">{formatInteger(ws.setupTime)}</td>
                <td className="num">{formatInteger(ws.standardTime)}</td>
                <td className="actions">
                  <button type="button" className="btn-action" onClick={() => startEdit(ws)}>
                    수정
                  </button>
                  <button type="button" className="btn-action danger" onClick={() => void onDelete(ws)}>
                    삭제
                  </button>
                </td>
              </tr>
            )}
          />
        )}
      </section>
    </div>
  );
}
