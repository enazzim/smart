import { useCallback, useEffect, useMemo, useState } from 'react';
import CompanySearchField, {
  PURCHASE_OUTSOURCE_PARTNER_ROLES,
  type CompanySearchSelection,
} from '../components/CompanySearchField';
import GridExcelExportButton from '../components/GridExcelExportButton';
import {
  createEtcClaim,
  deleteEtcClaim,
  fetchEtcClaims,
  updateEtcClaim,
  type EtcClaim,
  type EtcClaimListParams,
} from '../api/etcClaim';
import { formatAmount } from '../utils/numberFormat';
import { useConfirm } from '../context/ConfirmContext';

function todayIso(): string {
  return new Date().toISOString().slice(0, 10);
}

function addDaysIso(iso: string, days: number): string {
  const date = new Date(`${iso}T00:00:00`);
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

function formatDateTime(value: string) {
  return new Date(value).toLocaleString('ko-KR');
}

function recognitionLabel(value: EtcClaim['recognition']) {
  return value === 'APPROVED' ? '승인' : '미승인';
}

function emptyForm() {
  return {
    receiptDate: todayIso(),
    reason: '',
    amount: '0',
  };
}

function defaultStatusFilters(): EtcClaimListParams {
  const today = todayIso();
  return {
    receiptDateFrom: addDaysIso(today, -30),
    receiptDateTo: today,
    reason: '',
  };
}

export default function EtcClaimPage() {
  const confirm = useConfirm();
  const [tab, setTab] = useState<'register' | 'status'>('register');

  const [form, setForm] = useState(emptyForm);
  const [partner, setPartner] = useState<CompanySearchSelection | null>(null);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [submitting, setSubmitting] = useState(false);
  const [formError, setFormError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const [todayRows, setTodayRows] = useState<EtcClaim[]>([]);
  const [loadingToday, setLoadingToday] = useState(true);
  const [todayError, setTodayError] = useState<string | null>(null);

  const [statusFilters, setStatusFilters] = useState<EtcClaimListParams>(() => defaultStatusFilters());
  const [appliedStatusFilters, setAppliedStatusFilters] = useState<EtcClaimListParams>(() =>
    defaultStatusFilters(),
  );
  const [statusPartner, setStatusPartner] = useState<CompanySearchSelection | null>(null);
  const [appliedStatusPartner, setAppliedStatusPartner] = useState<CompanySearchSelection | null>(null);
  const [statusRows, setStatusRows] = useState<EtcClaim[]>([]);
  const [loadingStatus, setLoadingStatus] = useState(false);
  const [statusError, setStatusError] = useState<string | null>(null);
  const [statusClearToken, setStatusClearToken] = useState(0);

  const isEditing = editingId != null;

  const todayExportRows = useMemo(
    () =>
      todayRows.map((row) => ({
        거래처명: row.partnerName,
        접수일: row.receiptDate,
        금액: Number(row.amount),
        공제사유: row.reason,
        등록자: row.createdBy,
        등록일시: formatDateTime(row.createdAt),
      })),
    [todayRows],
  );

  const statusExportRows = useMemo(
    () =>
      statusRows.map((row) => ({
        거래처명: row.partnerName,
        사업자번호: row.partnerBusinessRegNo,
        접수일: row.receiptDate,
        금액: Number(row.amount),
        공제사유: row.reason,
        승인상태: recognitionLabel(row.recognition),
        등록자: row.createdBy,
        등록일시: formatDateTime(row.createdAt),
      })),
    [statusRows],
  );

  const loadTodayRows = useCallback(async () => {
    setLoadingToday(true);
    setTodayError(null);
    try {
      setTodayRows(await fetchEtcClaims({ registeredOn: todayIso() }));
    } catch (e) {
      setTodayError(e instanceof Error ? e.message : '오늘 등록 목록 조회 실패');
      setTodayRows([]);
    } finally {
      setLoadingToday(false);
    }
  }, []);

  const loadStatusRows = useCallback(async () => {
    setLoadingStatus(true);
    setStatusError(null);
    try {
      setStatusRows(
        await fetchEtcClaims({
          ...appliedStatusFilters,
          partnerId: appliedStatusPartner?.id,
          partnerName: appliedStatusPartner?.companyName,
        }),
      );
    } catch (e) {
      setStatusError(e instanceof Error ? e.message : '등록현황 조회 실패');
      setStatusRows([]);
    } finally {
      setLoadingStatus(false);
    }
  }, [appliedStatusFilters, appliedStatusPartner]);

  useEffect(() => {
    void loadTodayRows();
  }, [loadTodayRows]);

  useEffect(() => {
    if (tab === 'status') {
      void loadStatusRows();
    }
  }, [tab, loadStatusRows]);

  const resetForm = () => {
    setForm(emptyForm());
    setPartner(null);
    setEditingId(null);
    setFormError(null);
  };

  const startEdit = (row: EtcClaim) => {
    if (!row.editable) {
      setFormError('승인된 기타공제는 수정할 수 없습니다.');
      return;
    }
    setEditingId(row.id);
    setPartner({
      id: row.partnerId,
      companyName: row.partnerName,
      businessRegNo: row.partnerBusinessRegNo,
    });
    setForm({
      receiptDate: row.receiptDate,
      reason: row.reason,
      amount: String(row.amount),
    });
    setFormError(null);
    setMessage(null);
    setTab('register');
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!partner) {
      setFormError('거래처를 선택해 주세요.');
      return;
    }
    const amount = Number(form.amount);
    if (!form.reason.trim()) {
      setFormError('공제사유를 입력해 주세요.');
      return;
    }
    if (!Number.isFinite(amount) || amount <= 0) {
      setFormError('금액은 0보다 커야 합니다.');
      return;
    }

    setSubmitting(true);
    setFormError(null);
    setMessage(null);
    try {
      const payload = {
        partnerId: partner.id,
        receiptDate: form.receiptDate,
        reason: form.reason.trim(),
        amount,
      };
      if (editingId != null) {
        await updateEtcClaim(editingId, payload);
        setMessage('수정되었습니다.');
      } else {
        await createEtcClaim(payload);
        setMessage('등록되었습니다.');
      }
      resetForm();
      await loadTodayRows();
      await loadStatusRows();
    } catch (err) {
      setFormError(err instanceof Error ? err.message : '저장 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (id: number) => {
    if (
      !(await confirm('이 기타공제를 삭제하시겠습니까?', {
        title: '삭제 확인',
        confirmLabel: '삭제',
        cancelLabel: '닫기',
        danger: true,
      }))
    ) {
      return;
    }
    setFormError(null);
    setMessage(null);
    try {
      await deleteEtcClaim(id);
      if (editingId === id) {
        resetForm();
      }
      setMessage('삭제되었습니다.');
      await loadTodayRows();
      if (tab === 'status') {
        await loadStatusRows();
      }
    } catch (err) {
      setFormError(err instanceof Error ? err.message : '삭제 실패');
    }
  };

  const onStatusSearch = (e: React.FormEvent) => {
    e.preventDefault();
    setAppliedStatusFilters({ ...statusFilters });
    setAppliedStatusPartner(statusPartner);
  };

  const onStatusReset = () => {
    const defaults = defaultStatusFilters();
    setStatusFilters(defaults);
    setAppliedStatusFilters(defaults);
    setStatusPartner(null);
    setAppliedStatusPartner(null);
    setStatusClearToken((token) => token + 1);
  };

  return (
    <div className="page">
      <header className="page-header">
        <h1>기타공제등록</h1>
        <p className="page-desc">거래처별 기타공제를 등록하고 등록현황을 조회합니다.</p>
      </header>

      <div className="tab-row">
        <button
          type="button"
          className={tab === 'register' ? 'tab-active' : undefined}
          onClick={() => setTab('register')}
        >
          기타공제등록
        </button>
        <button
          type="button"
          className={tab === 'status' ? 'tab-active' : undefined}
          onClick={() => setTab('status')}
        >
          등록현황
        </button>
      </div>

      {(formError || message) && (
        <p className={formError ? 'error-banner' : 'success-banner'}>{formError ?? message}</p>
      )}

      {tab === 'register' && (
        <>
          <section className="panel">
            <h2>입력</h2>
            <form onSubmit={(e) => void onSubmit(e)} className="form-grid">
              <CompanySearchField
                label="거래처명"
                partnerTypes={PURCHASE_OUTSOURCE_PARTNER_ROLES}
                selectedCompany={partner}
                onSelect={setPartner}
                placeholder="거래처명 또는 사업자번호"
              />
              <label>
                접수일
                <input
                  type="date"
                  required
                  value={form.receiptDate}
                  onChange={(e) => setForm((prev) => ({ ...prev, receiptDate: e.target.value }))}
                />
              </label>
              <label className="full-width">
                공제사유
                <input
                  type="text"
                  required
                  value={form.reason}
                  onChange={(e) => setForm((prev) => ({ ...prev, reason: e.target.value }))}
                  placeholder="공제사유를 입력하세요"
                />
              </label>
              <label>
                금액
                <input
                  type="number"
                  required
                  min={0}
                  step="0.01"
                  value={form.amount}
                  onChange={(e) => setForm((prev) => ({ ...prev, amount: e.target.value }))}
                />
              </label>
              <div className="form-actions full-width">
                <button type="button" className="secondary" onClick={resetForm}>
                  초기화
                </button>
                <button
                  type="button"
                  className="danger"
                  disabled={!isEditing || submitting}
                  onClick={() => (editingId != null ? void onDelete(editingId) : undefined)}
                >
                  삭제
                </button>
                <button type="submit" disabled={submitting || (isEditing && !editingId)}>
                  {submitting ? '저장 중…' : isEditing ? '수정' : '등록'}
                </button>
              </div>
            </form>
          </section>

          <section className="panel">
            <div className="panel-header-row">
              <h2>입력결과 (오늘 등록)</h2>
              <GridExcelExportButton
                fileBaseName="기타공제_오늘등록"
                sheetName="입력결과"
                disabled={loadingToday || todayRows.length === 0}
                rows={todayExportRows}
              />
            </div>
            {todayError && <p className="error-banner">{todayError}</p>}
            {loadingToday ? (
              <p>불러오는 중…</p>
            ) : todayRows.length === 0 ? (
              <p className="hint">오늘 등록된 기타공제가 없습니다.</p>
            ) : (
              <div className="table-wrap">
                <table>
                  <thead>
                    <tr>
                      <th>거래처명</th>
                      <th>접수일</th>
                      <th className="num">금액</th>
                      <th>공제사유</th>
                      <th>등록자</th>
                      <th>작업</th>
                    </tr>
                  </thead>
                  <tbody>
                    {todayRows.map((row) => (
                      <tr key={row.id}>
                        <td>{row.partnerName}</td>
                        <td>{row.receiptDate}</td>
                        <td className="num">{formatAmount(row.amount)}</td>
                        <td>{row.reason}</td>
                        <td>{row.createdBy || '—'}</td>
                        <td className="actions">
                          <button type="button" className="btn-action" disabled={!row.editable} onClick={() => startEdit(row)}>
                            수정
                          </button>
                          <button
                            type="button"
                            className="btn-action danger"
                            disabled={!row.editable}
                            onClick={() => void onDelete(row.id)}
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
        </>
      )}

      {tab === 'status' && (
        <section className="panel">
          <h2>검색조건</h2>
          <form onSubmit={onStatusSearch} className="search-row">
            <CompanySearchField
              label="거래처"
              partnerTypes={PURCHASE_OUTSOURCE_PARTNER_ROLES}
              selectedCompany={statusPartner}
              onSelect={setStatusPartner}
              clearToken={statusClearToken}
              placeholder="전체"
            />
            <label>
              접수일(시작)
              <input
                type="date"
                value={statusFilters.receiptDateFrom ?? ''}
                onChange={(e) => setStatusFilters((prev) => ({ ...prev, receiptDateFrom: e.target.value }))}
              />
            </label>
            <label>
              접수일(종료)
              <input
                type="date"
                value={statusFilters.receiptDateTo ?? ''}
                onChange={(e) => setStatusFilters((prev) => ({ ...prev, receiptDateTo: e.target.value }))}
              />
            </label>
            <label>
              공제사유
              <input
                type="text"
                value={statusFilters.reason ?? ''}
                onChange={(e) => setStatusFilters((prev) => ({ ...prev, reason: e.target.value }))}
                placeholder="사유 검색"
              />
            </label>
            <div className="form-actions">
              <button type="submit" disabled={loadingStatus}>
                검색
              </button>
              <button type="button" className="secondary" disabled={loadingStatus} onClick={onStatusReset}>
                초기화
              </button>
            </div>
          </form>

          <div className="panel-header-row" style={{ marginTop: '1rem' }}>
            <h2>등록현황</h2>
            <GridExcelExportButton
              fileBaseName="기타공제등록현황"
              sheetName="등록현황"
              disabled={loadingStatus || statusRows.length === 0}
              rows={statusExportRows}
            />
          </div>
          {statusError && <p className="error-banner">{statusError}</p>}
          {loadingStatus ? (
            <p>불러오는 중…</p>
          ) : statusRows.length === 0 ? (
            <p className="hint">조회 결과가 없습니다.</p>
          ) : (
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>거래처명</th>
                    <th>접수일</th>
                    <th className="num">금액</th>
                    <th>공제사유</th>
                    <th>승인상태</th>
                    <th>등록자</th>
                    <th>등록일시</th>
                    <th>작업</th>
                  </tr>
                </thead>
                <tbody>
                  {statusRows.map((row) => (
                    <tr key={row.id}>
                      <td>
                        {row.partnerName}
                        <br />
                        <small>{row.partnerBusinessRegNo}</small>
                      </td>
                      <td>{row.receiptDate}</td>
                      <td className="num">{formatAmount(row.amount)}</td>
                      <td>{row.reason}</td>
                      <td>{recognitionLabel(row.recognition)}</td>
                      <td>{row.createdBy || '—'}</td>
                      <td>{formatDateTime(row.createdAt)}</td>
                      <td className="actions">
                        <button type="button" className="btn-action" disabled={!row.editable} onClick={() => startEdit(row)}>
                          수정
                        </button>
                        <button
                          type="button"
                          className="btn-action danger"
                          disabled={!row.editable}
                          onClick={() => void onDelete(row.id)}
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
      )}
    </div>
  );
}
