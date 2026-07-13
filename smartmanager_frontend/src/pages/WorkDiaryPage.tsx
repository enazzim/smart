import { useCallback, useEffect, useState } from 'react';
import type { AuthenticatedUser } from '../api/auth';
import { useAuth } from '../context/AuthContext';
import {
  WORK_DIARY_STATUS_LABELS,
  approveWorkDiary,
  cancelWorkDiaryApproval,
  createWorkDiary,
  deleteWorkDiary,
  emptyFieldValues,
  fetchMyWorkDiaryTemplate,
  fetchWorkDiaries,
  fetchWorkDiary,
  fetchWorkDiaryTemplates,
  formatWorkDiaryDate,
  parseSchemaFields,
  type WorkDiaryFieldValues,
  submitWorkDiary,
  updateWorkDiary,
  type WorkDiaryDetail,
  type WorkDiaryListItem,
  type WorkDiaryStatus,
  type WorkDiaryTemplate,
} from '../api/workDiary';
import { WorkDiaryTemplatesCatalog } from '../components/workdiary/WorkDiaryTemplatePanel';
import NewPostBadge from '../components/board/NewPostBadge';
import WorkDiaryChecklistField from '../components/workdiary/WorkDiaryChecklistField';
import { mergeFieldValues, checklistValueOrEmpty, textareaValue } from '../components/workdiary/workDiaryFieldUtils';
import { useConfirm } from '../context/ConfirmContext';

export type WorkDiaryScreen =
  | { mode: 'list' }
  | { mode: 'detail'; id: number }
  | { mode: 'compose'; workDate?: string; editId?: number };

interface WorkDiaryPageProps {
  screen: WorkDiaryScreen;
  currentUser: AuthenticatedUser | null;
  onNavigateHome: () => void;
  onNavigateList: () => void;
  onNavigateDetail: (id: number) => void;
  onNavigateCompose: (compose: { workDate?: string; editId?: number }) => void;
}

const PAGE_SIZE = 20;

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

export default function WorkDiaryPage({
  screen,
  currentUser,
  onNavigateHome,
  onNavigateList,
  onNavigateDetail,
  onNavigateCompose,
}: WorkDiaryPageProps) {
  const { canWriteDashboard } = useAuth();
  const canWrite = canWriteDashboard();
  const canApprove = currentUser?.roleCodes.includes('SYSTEM_ADMIN') ?? false;

  if (screen.mode === 'list') {
    return (
      <WorkDiaryListView
        canWrite={canWrite}
        canApprove={canApprove}
        canManageTemplates={currentUser?.roleCodes.includes('SYSTEM_ADMIN') ?? false}
        onNavigateHome={onNavigateHome}
        onNavigateDetail={onNavigateDetail}
        onNavigateCompose={onNavigateCompose}
      />
    );
  }

  if (screen.mode === 'detail') {
    return (
      <WorkDiaryDetailView
        id={screen.id}
        onNavigateHome={onNavigateHome}
        onNavigateList={onNavigateList}
        onNavigateCompose={onNavigateCompose}
      />
    );
  }

  return (
    <WorkDiaryComposeView
      workDate={screen.workDate ?? todayIso()}
      editId={screen.editId}
      canWrite={canWrite}
      onNavigateList={onNavigateList}
      onNavigateDetail={onNavigateDetail}
    />
  );
}

function WorkDiaryListView({
  canWrite,
  canApprove,
  canManageTemplates,
  onNavigateHome,
  onNavigateDetail,
  onNavigateCompose,
}: {
  canWrite: boolean;
  canApprove: boolean;
  canManageTemplates: boolean;
  onNavigateHome: () => void;
  onNavigateDetail: (id: number) => void;
  onNavigateCompose: (compose: { workDate?: string; editId?: number }) => void;
}) {
  const [items, setItems] = useState<WorkDiaryListItem[]>([]);
  const [total, setTotal] = useState(0);
  const [page, setPage] = useState(0);
  const [fromDate, setFromDate] = useState(addDaysIso(todayIso(), -30));
  const [toDate, setToDate] = useState(todayIso());
  const [status, setStatus] = useState<WorkDiaryStatus | ''>('');
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [composeDate, setComposeDate] = useState(todayIso());
  const [showTemplates, setShowTemplates] = useState(false);
  const [templates, setTemplates] = useState<WorkDiaryTemplate[]>([]);
  const [templatesLoading, setTemplatesLoading] = useState(false);

  const openTemplates = async () => {
    setTemplatesLoading(true);
    try {
      setTemplates(await fetchWorkDiaryTemplates());
      setShowTemplates(true);
    } catch (e) {
      setError(e instanceof Error ? e.message : '그룹 양식을 불러오지 못했습니다.');
    } finally {
      setTemplatesLoading(false);
    }
  };

  const load = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const result = await fetchWorkDiaries({
        fromDate,
        toDate,
        status: status || undefined,
        page,
        size: PAGE_SIZE,
      });
      setItems(result.items);
      setTotal(result.totalElements);
    } catch (e) {
      setError(e instanceof Error ? e.message : '업무일지 목록을 불러오지 못했습니다.');
    } finally {
      setLoading(false);
    }
  }, [fromDate, toDate, status, page]);

  useEffect(() => {
    void load();
  }, [load]);

  const totalPages = Math.max(1, Math.ceil(total / PAGE_SIZE));

  return (
    <div className="page">
      <header className="page-header">
        <div>
          <button type="button" className="link-button" onClick={onNavigateHome}>
            ← 대시보드
          </button>
          <h1>업무일지</h1>
          <p>{canApprove ? '전체 작성자의 업무일지를 조회합니다.' : '내 업무일지를 조회·작성합니다.'}</p>
        </div>
        {canWrite && (
          <div className="inline-actions">
            <button type="button" className="secondary" disabled={templatesLoading} onClick={() => void openTemplates()}>
              그룹 양식 보기
            </button>
            <input type="date" value={composeDate} onChange={(e) => setComposeDate(e.target.value)} />
            <button type="button" onClick={() => onNavigateCompose({ workDate: composeDate })}>
              작성
            </button>
          </div>
        )}
        {!canWrite && canApprove && (
          <button type="button" className="secondary" disabled={templatesLoading} onClick={() => void openTemplates()}>
            그룹 양식 보기
          </button>
        )}
      </header>

      <section className="filter-panel">
        <label>
          기간(부터)
          <input type="date" value={fromDate} onChange={(e) => { setPage(0); setFromDate(e.target.value); }} />
        </label>
        <label>
          기간(까지)
          <input type="date" value={toDate} onChange={(e) => { setPage(0); setToDate(e.target.value); }} />
        </label>
        <label>
          상태
          <select
            value={status}
            onChange={(e) => {
              setPage(0);
              setStatus((e.target.value || '') as WorkDiaryStatus | '');
            }}
          >
            <option value="">전체</option>
            {(Object.keys(WORK_DIARY_STATUS_LABELS) as WorkDiaryStatus[]).map((value) => (
              <option key={value} value={value}>
                {WORK_DIARY_STATUS_LABELS[value]}
              </option>
            ))}
          </select>
        </label>
        <button type="button" onClick={() => void load()}>
          조회
        </button>
      </section>

      {error && <p className="error-banner">{error}</p>}

      {loading ? (
        <p>불러오는 중…</p>
      ) : (
        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>업무일</th>
                <th>제목</th>
                {canApprove && <th>작성자</th>}
                <th>그룹</th>
                <th>상태</th>
              </tr>
            </thead>
            <tbody>
              {items.length === 0 ? (
                <tr>
                  <td colSpan={canApprove ? 5 : 4}>업무일지가 없습니다.</td>
                </tr>
              ) : (
                items.map((row) => (
                  <tr key={row.id} className="clickable-row" onClick={() => onNavigateDetail(row.id)}>
                    <td>{formatWorkDiaryDate(row.workDate)}</td>
                    <td>
                      <span className="board-title-link">
                        <span className="board-title-text">{row.workDateTitle || '—'}</span>
                        <NewPostBadge dateValue={row.workDate} />
                      </span>
                    </td>
                    {canApprove && <td>{row.authorName}</td>}
                    <td>{row.workDiaryGroupName || '—'}</td>
                    <td>{WORK_DIARY_STATUS_LABELS[row.status]}</td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      )}

      {totalPages > 1 && (
        <div className="pagination-row">
          <button type="button" className="secondary" disabled={page <= 0} onClick={() => setPage((p) => p - 1)}>
            이전
          </button>
          <span>
            {page + 1} / {totalPages}
          </span>
          <button
            type="button"
            className="secondary"
            disabled={page + 1 >= totalPages}
            onClick={() => setPage((p) => p + 1)}
          >
            다음
          </button>
        </div>
      )}
      {showTemplates && (
        <WorkDiaryTemplatesCatalog
          templates={templates}
          canManageTemplates={canManageTemplates}
          onClose={() => setShowTemplates(false)}
          onTemplatesChange={setTemplates}
        />
      )}
    </div>
  );
}

function WorkDiaryDetailView({
  id,
  onNavigateHome,
  onNavigateList,
  onNavigateCompose,
}: {
  id: number;
  onNavigateHome: () => void;
  onNavigateList: () => void;
  onNavigateCompose: (compose: { workDate?: string; editId?: number }) => void;
}) {
  const confirm = useConfirm();
  const [detail, setDetail] = useState<WorkDiaryDetail | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [directiveNote, setDirectiveNote] = useState('');
  const [submitting, setSubmitting] = useState(false);

  const load = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await fetchWorkDiary(id);
      setDetail(data);
      setDirectiveNote(data.directiveNote ?? '');
    } catch (e) {
      setError(e instanceof Error ? e.message : '업무일지를 불러오지 못했습니다.');
    } finally {
      setLoading(false);
    }
  }, [id]);

  useEffect(() => {
    void load();
  }, [load]);

  const fields = parseSchemaFields(detail?.fieldSchema);

  const onSubmitDiary = async () => {
    if (!detail) return;
    setSubmitting(true);
    setError(null);
    setSuccess(null);
    try {
      await submitWorkDiary(detail.id);
      setSuccess('제출되었습니다.');
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '제출에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const onApprove = async () => {
    if (!detail) return;
    setSubmitting(true);
    setError(null);
    setSuccess(null);
    try {
      await approveWorkDiary(detail.id, directiveNote);
      setSuccess('결재되었습니다.');
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '결재에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const onCancelApproval = async () => {
    if (!detail || !(await confirm('결재를 취소하시겠습니까?', { title: '취소 확인', confirmLabel: '예, 취소', cancelLabel: '닫기', danger: true }))) return;
    setSubmitting(true);
    setError(null);
    setSuccess(null);
    try {
      await cancelWorkDiaryApproval(detail.id);
      setSuccess('결재가 취소되었습니다.');
      await load();
    } catch (e) {
      setError(e instanceof Error ? e.message : '결재 취소에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async () => {
    if (!detail || !(await confirm('업무일지를 삭제하시겠습니까?', { title: '삭제 확인', confirmLabel: '삭제', cancelLabel: '닫기', danger: true }))) return;
    setSubmitting(true);
    setError(null);
    try {
      await deleteWorkDiary(detail.id);
      onNavigateList();
    } catch (e) {
      setError(e instanceof Error ? e.message : '삭제에 실패했습니다.');
      setSubmitting(false);
    }
  };

  if (loading) return <p>불러오는 중…</p>;
  if (!detail) return <p className="error-banner">{error ?? '업무일지를 찾을 수 없습니다.'}</p>;

  return (
    <div className="page">
      <header className="page-header">
        <div>
          <button type="button" className="link-button" onClick={onNavigateList}>
            ← 목록
          </button>
          <h1>{detail.workDateTitle || '업무일지'}</h1>
          <p>
            {formatWorkDiaryDate(detail.workDate)} · {detail.authorName} · {WORK_DIARY_STATUS_LABELS[detail.status]}
          </p>
        </div>
        <div className="inline-actions">
          {detail.canEdit && (
            <button type="button" onClick={() => onNavigateCompose({ editId: detail.id })}>
              수정
            </button>
          )}
          {detail.canDelete && (
            <button type="button" className="secondary" disabled={submitting} onClick={() => void onDelete()}>
              삭제
            </button>
          )}
        </div>
      </header>

      {error && <p className="error-banner">{error}</p>}
      {success && <p className="success-banner">{success}</p>}

      <section className="detail-panel">
        <h2>업무 내용</h2>
        {fields.map((field) => (
          <div key={field.key} className="work-diary-field-readonly">
            <h3>{field.label}</h3>
            {field.type === 'checklist' ? (
              <WorkDiaryChecklistField
                field={field}
                value={checklistValueOrEmpty(detail.fieldValues[field.key])}
                readOnly
              />
            ) : (
              <pre>{textareaValue(detail.fieldValues[field.key]) || '—'}</pre>
            )}
          </div>
        ))}
        {detail.closingNote && (
          <div className="work-diary-field-readonly">
            <h3>마감 메모</h3>
            <pre>{detail.closingNote}</pre>
          </div>
        )}
      </section>

      {(detail.directiveNote || detail.canApprove) && (
        <section className="detail-panel">
          <h2>지시사항 (결재)</h2>
          {detail.canApprove ? (
            <label className="work-diary-field">
              지시사항
              <textarea
                rows={4}
                value={directiveNote}
                onChange={(e) => setDirectiveNote(e.target.value)}
                placeholder="결재 시 지시사항을 입력합니다. (선택)"
              />
            </label>
          ) : (
            <div className="work-diary-field-readonly">
              <h3>지시사항</h3>
              <pre>{detail.directiveNote || '—'}</pre>
            </div>
          )}
          {detail.approvedAt && (
            <p className="meta-text">
              결재: {detail.approvedByName ?? '—'} ({detail.approvedAt.slice(0, 16).replace('T', ' ')})
            </p>
          )}
        </section>
      )}

      <div className="form-actions">
        {detail.canEdit && (detail.status === 'DRAFT' || detail.approvalCanceledAt) && (
          <button type="button" disabled={submitting} onClick={() => void onSubmitDiary()}>
            {detail.approvalCanceledAt ? '재제출' : '제출'}
          </button>
        )}
        {detail.canApprove && (
          <button type="button" disabled={submitting} onClick={() => void onApprove()}>
            결재
          </button>
        )}
        {detail.canCancelApproval && (
          <button type="button" className="secondary" disabled={submitting} onClick={() => void onCancelApproval()}>
            결재 취소
          </button>
        )}
        <button type="button" className="secondary" onClick={onNavigateHome}>
          대시보드
        </button>
      </div>
    </div>
  );
}

function WorkDiaryComposeView({
  workDate,
  editId,
  canWrite,
  onNavigateList,
  onNavigateDetail,
}: {
  workDate: string;
  editId?: number;
  canWrite: boolean;
  onNavigateList: () => void;
  onNavigateDetail: (id: number) => void;
}) {
  const [template, setTemplate] = useState<WorkDiaryTemplate | null>(null);
  const [fieldValues, setFieldValues] = useState<WorkDiaryFieldValues>({});
  const [selectedDate, setSelectedDate] = useState(workDate);
  const [closingNote, setClosingNote] = useState('');
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const isEdit = editId != null;

  useEffect(() => {
    const load = async () => {
      setLoading(true);
      setError(null);
      try {
        if (isEdit) {
          const detail = await fetchWorkDiary(editId);
          setTemplate({
            templateCode: detail.templateCode,
            workDiaryGroupId: detail.workDiaryGroupId,
            workDiaryGroupName: detail.workDiaryGroupName,
            templateName: detail.templateName,
            fieldSchema: detail.fieldSchema,
          });
          setFieldValues(mergeFieldValues(parseSchemaFields(detail.fieldSchema), detail.fieldValues));
          setSelectedDate(detail.workDate);
          setClosingNote(detail.closingNote ?? '');
        } else {
          const tpl = await fetchMyWorkDiaryTemplate();
          setTemplate(tpl);
          setFieldValues(emptyFieldValues(tpl.fieldSchema));
          setSelectedDate(workDate);
        }
      } catch (e) {
        setError(e instanceof Error ? e.message : '양식을 불러오지 못했습니다.');
      } finally {
        setLoading(false);
      }
    };
    void load();
  }, [editId, isEdit, workDate]);

  const fields = parseSchemaFields(template?.fieldSchema);

  const save = async (status: 'DRAFT' | 'SUBMITTED') => {
    if (!canWrite) return;
    setSubmitting(true);
    setError(null);
    try {
      if (isEdit && editId != null) {
        const updated = await updateWorkDiary(editId, {
          fieldValues,
          closingNote: closingNote || undefined,
        });
        if (status === 'SUBMITTED' && (updated.status === 'DRAFT' || updated.approvalCanceledAt)) {
          await submitWorkDiary(updated.id);
        }
        onNavigateDetail(updated.id);
        return;
      }
      const created = await createWorkDiary({
        workDate: selectedDate,
        fieldValues,
        closingNote: closingNote || undefined,
        status,
      });
      onNavigateDetail(created.id);
    } catch (e) {
      setError(e instanceof Error ? e.message : '저장에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  if (!canWrite) {
    return <p className="error-banner">업무일지 작성 권한이 없습니다.</p>;
  }

  if (loading) return <p>불러오는 중…</p>;

  return (
    <div className="page">
      <header className="page-header">
        <div>
          <button type="button" className="link-button" onClick={onNavigateList}>
            ← 목록
          </button>
          <h1>{isEdit ? '업무일지 수정' : '업무일지 작성'}</h1>
          <p>
            {template?.templateName ?? '—'} · {template?.workDiaryGroupName ?? '—'}
          </p>
        </div>
      </header>

      {error && <p className="error-banner">{error}</p>}

      <section className="filter-panel">
        <label>
          업무일
          <input
            type="date"
            value={selectedDate}
            disabled={isEdit}
            onChange={(e) => setSelectedDate(e.target.value)}
          />
        </label>
      </section>

      <section className="detail-panel">
        <h2>업무 내용</h2>
        {fields.map((field) => (
          <div key={field.key} className="work-diary-field">
            <h3>{field.label}</h3>
            {field.type === 'checklist' ? (
              <WorkDiaryChecklistField
                field={field}
                value={checklistValueOrEmpty(fieldValues[field.key])}
                onChange={(next) => setFieldValues((prev) => ({ ...prev, [field.key]: next }))}
              />
            ) : (
              <textarea
                rows={4}
                value={textareaValue(fieldValues[field.key])}
                onChange={(e) => setFieldValues((prev) => ({ ...prev, [field.key]: e.target.value }))}
              />
            )}
          </div>
        ))}
        <label className="work-diary-field">
          마감 메모
          <input
            value={closingNote}
            onChange={(e) => setClosingNote(e.target.value)}
            maxLength={500}
          />
        </label>
      </section>

      <div className="form-actions">
        <button type="button" disabled={submitting} onClick={() => void save('DRAFT')}>
          {submitting ? '저장 중…' : '임시저장'}
        </button>
        <button type="button" disabled={submitting} onClick={() => void save('SUBMITTED')}>
          {submitting ? '저장 중…' : '저장 후 제출'}
        </button>
        <button type="button" className="secondary" onClick={onNavigateList}>
          취소
        </button>
      </div>
    </div>
  );
}
