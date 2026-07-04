import { useEffect, useState } from 'react';
import type {
  CreateLargePublicCodeRequest,
  CreateSmallPublicCodeRequest,
  PublicCodeLarge,
  PublicCodeSmall,
  PublicCodeUsageType,
  UpdateLargePublicCodeRequest,
  UpdateSmallPublicCodeRequest,
} from '../api/publicCode';
import {
  USAGE_TYPE_OPTIONS,
  createLargePublicCode,
  createSmallPublicCode,
  deleteLargePublicCode,
  deleteSmallPublicCode,
  fetchLargePublicCodes,
  fetchSmallPublicCodes,
  updateLargePublicCode,
  updateSmallPublicCode,
} from '../api/publicCode';

const emptyLargeForm: CreateLargePublicCodeRequest = {
  largeCode: '',
  largeName: '',
  usageType: 'GENERIC',
};

const emptySmallForm: Omit<CreateSmallPublicCodeRequest, 'largeCode'> = {
  smallCode: '',
  smallName: '',
};

function formatLargeLabel(row: PublicCodeLarge): string {
  return `${row.largeCode} ${row.largeName}`;
}

export default function PublicCodePage() {
  const [largeRows, setLargeRows] = useState<PublicCodeLarge[]>([]);
  const [smallRows, setSmallRows] = useState<PublicCodeSmall[]>([]);
  const [selectedLargeCode, setSelectedLargeCode] = useState<string | null>(null);
  const [largeForm, setLargeForm] = useState<CreateLargePublicCodeRequest>(emptyLargeForm);
  const [smallForm, setSmallForm] = useState(emptySmallForm);
  const [editingLargeCode, setEditingLargeCode] = useState<string | null>(null);
  const [editingSmallId, setEditingSmallId] = useState<number | null>(null);
  const [showLargeForm, setShowLargeForm] = useState(false);
  const [showSmallForm, setShowSmallForm] = useState(false);
  const [loadingLarge, setLoadingLarge] = useState(true);
  const [loadingSmall, setLoadingSmall] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const selectedLarge = largeRows.find((row) => row.largeCode === selectedLargeCode) ?? null;
  const isEditingLarge = editingLargeCode !== null;
  const isEditingSmall = editingSmallId !== null;

  const loadLarge = async () => {
    setLoadingLarge(true);
    setError(null);
    try {
      const rows = await fetchLargePublicCodes();
      setLargeRows(rows);
      if (rows.length === 0) {
        setSelectedLargeCode(null);
        setSmallRows([]);
        return;
      }
      const nextSelected =
        selectedLargeCode && rows.some((row) => row.largeCode === selectedLargeCode)
          ? selectedLargeCode
          : rows[0].largeCode;
      setSelectedLargeCode(nextSelected);
    } catch (e) {
      setError(e instanceof Error ? e.message : '대분류 조회 실패');
    } finally {
      setLoadingLarge(false);
    }
  };

  const loadSmall = async (largeCode: string) => {
    setLoadingSmall(true);
    setError(null);
    try {
      setSmallRows(await fetchSmallPublicCodes(largeCode));
    } catch (e) {
      setError(e instanceof Error ? e.message : '소분류 조회 실패');
    } finally {
      setLoadingSmall(false);
    }
  };

  useEffect(() => {
    void loadLarge();
  }, []);

  useEffect(() => {
    if (selectedLargeCode) {
      void loadSmall(selectedLargeCode);
    } else {
      setSmallRows([]);
    }
  }, [selectedLargeCode]);

  const resetLargeForm = () => {
    setLargeForm(emptyLargeForm);
    setEditingLargeCode(null);
    setShowLargeForm(false);
  };

  const resetSmallForm = () => {
    setSmallForm(emptySmallForm);
    setEditingSmallId(null);
    setShowSmallForm(false);
  };

  const startEditLarge = (row: PublicCodeLarge) => {
    setEditingLargeCode(row.largeCode);
    setLargeForm({
      largeCode: row.largeCode,
      largeName: row.largeName,
      usageType: row.usageType,
    });
    setShowLargeForm(true);
    setError(null);
  };

  const startEditSmall = (row: PublicCodeSmall) => {
    setEditingSmallId(row.id);
    setSmallForm({
      smallCode: row.smallCode,
      smallName: row.smallName,
    });
    setShowSmallForm(true);
    setError(null);
  };

  const onSubmitLarge = async (e: React.FormEvent) => {
    e.preventDefault();
    setSubmitting(true);
    setError(null);
    try {
      if (isEditingLarge && editingLargeCode) {
        const payload: UpdateLargePublicCodeRequest = {
          largeName: largeForm.largeName,
          usageType: largeForm.usageType,
        };
        await updateLargePublicCode(editingLargeCode, payload);
      } else {
        await createLargePublicCode(largeForm);
      }
      resetLargeForm();
      await loadLarge();
    } catch (err) {
      setError(err instanceof Error ? err.message : isEditingLarge ? '대분류 수정 실패' : '대분류 등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onSubmitSmall = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!selectedLargeCode) {
      setError('대분류를 먼저 선택해 주세요.');
      return;
    }
    setSubmitting(true);
    setError(null);
    try {
      if (isEditingSmall && editingSmallId !== null) {
        const payload: UpdateSmallPublicCodeRequest = { smallName: smallForm.smallName };
        await updateSmallPublicCode(editingSmallId, payload);
      } else {
        await createSmallPublicCode({
          largeCode: selectedLargeCode,
          smallCode: smallForm.smallCode,
          smallName: smallForm.smallName,
        });
      }
      resetSmallForm();
      await loadSmall(selectedLargeCode);
    } catch (err) {
      setError(err instanceof Error ? err.message : isEditingSmall ? '소분류 수정 실패' : '소분류 등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDeleteLarge = async (row: PublicCodeLarge) => {
    if (!window.confirm(`「${formatLargeLabel(row)}」 대분류와 하위 소분류를 삭제하시겠습니까?`)) {
      return;
    }
    setError(null);
    try {
      await deleteLargePublicCode(row.largeCode);
      if (selectedLargeCode === row.largeCode) {
        setSelectedLargeCode(null);
      }
      resetLargeForm();
      await loadLarge();
    } catch (err) {
      setError(err instanceof Error ? err.message : '대분류 삭제 실패');
    }
  };

  const onDeleteSmall = async (row: PublicCodeSmall) => {
    if (!window.confirm(`「${row.smallName}(${row.smallCode})」 소분류를 삭제하시겠습니까?`)) {
      return;
    }
    setError(null);
    try {
      await deleteSmallPublicCode(row.id);
      if (editingSmallId === row.id) {
        resetSmallForm();
      }
      if (selectedLargeCode) {
        await loadSmall(selectedLargeCode);
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : '소분류 삭제 실패');
    }
  };

  return (
    <>
      <header>
        <h1>공용코드 관리</h1>
        <p>시스템정보 — 대분류·소분류 마스터-디테일 CRUD</p>
      </header>

      {error && <div className="error">{error}</div>}

      <div className="master-detail">
        <section className="panel master-panel">
          <div className="panel-header-row">
            <h2>대분류</h2>
            <button
              type="button"
              onClick={() => {
                resetLargeForm();
                setShowLargeForm(true);
              }}
            >
              등록
            </button>
          </div>

          {showLargeForm && (
            <form onSubmit={onSubmitLarge} className="form-grid form-grid-wide inline-form">
              <label>
                대분류 코드 *
                <input
                  required
                  disabled={isEditingLarge}
                  value={largeForm.largeCode}
                  onChange={(e) => setLargeForm({ ...largeForm, largeCode: e.target.value })}
                />
              </label>
              <label>
                대분류명 *
                <input
                  required
                  value={largeForm.largeName}
                  onChange={(e) => setLargeForm({ ...largeForm, largeName: e.target.value })}
                />
              </label>
              <label>
                용도 *
                <select
                  required
                  value={largeForm.usageType}
                  onChange={(e) =>
                    setLargeForm({ ...largeForm, usageType: e.target.value as PublicCodeUsageType })
                  }
                >
                  {USAGE_TYPE_OPTIONS.map((opt) => (
                    <option key={opt.value} value={opt.value}>
                      {opt.label}
                    </option>
                  ))}
                </select>
              </label>
              <div className="form-actions">
                <button type="submit" disabled={submitting}>
                  {submitting ? '저장 중…' : isEditingLarge ? '수정 저장' : '등록'}
                </button>
                <button type="button" className="secondary" onClick={resetLargeForm} disabled={submitting}>
                  취소
                </button>
              </div>
            </form>
          )}

          {loadingLarge ? (
            <p>불러오는 중…</p>
          ) : largeRows.length === 0 ? (
            <p>등록된 대분류가 없습니다.</p>
          ) : (
            <table>
              <thead>
                <tr>
                  <th>코드</th>
                  <th>명칭</th>
                  <th>용도</th>
                  <th>작업</th>
                </tr>
              </thead>
              <tbody>
                {largeRows.map((row) => (
                  <tr
                    key={row.largeCode}
                    className={
                      selectedLargeCode === row.largeCode
                        ? 'row-selected'
                        : editingLargeCode === row.largeCode
                          ? 'row-editing'
                          : undefined
                    }
                    onClick={() => setSelectedLargeCode(row.largeCode)}
                  >
                    <td>{row.largeCode}</td>
                    <td>{row.largeName}</td>
                    <td>{row.usageType}</td>
                    <td className="actions" onClick={(e) => e.stopPropagation()}>
                      <button type="button" className="btn-action" onClick={() => startEditLarge(row)}>
                        수정
                      </button>
                      <button type="button" className="btn-action danger" onClick={() => void onDeleteLarge(row)}>
                        삭제
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}
        </section>

        <section className="panel detail-panel">
          <div className="panel-header-row">
            <h2>
              {selectedLarge
                ? `${selectedLarge.largeName} — 소분류`
                : '소분류'}
            </h2>
            <button
              type="button"
              disabled={!selectedLargeCode}
              onClick={() => {
                resetSmallForm();
                setShowSmallForm(true);
              }}
            >
              등록
            </button>
          </div>

          {showSmallForm && selectedLargeCode && (
            <form onSubmit={onSubmitSmall} className="form-grid form-grid-wide inline-form">
              <label>
                대분류
                <input value={selectedLargeCode} disabled />
              </label>
              <label>
                소분류 코드 *
                <input
                  required
                  disabled={isEditingSmall}
                  value={smallForm.smallCode}
                  onChange={(e) => setSmallForm({ ...smallForm, smallCode: e.target.value })}
                />
              </label>
              <label>
                소분류명 *
                <input
                  required
                  value={smallForm.smallName}
                  onChange={(e) => setSmallForm({ ...smallForm, smallName: e.target.value })}
                />
              </label>
              <div className="form-actions">
                <button type="submit" disabled={submitting}>
                  {submitting ? '저장 중…' : isEditingSmall ? '수정 저장' : '등록'}
                </button>
                <button type="button" className="secondary" onClick={resetSmallForm} disabled={submitting}>
                  취소
                </button>
              </div>
            </form>
          )}

          {!selectedLargeCode ? (
            <p>좌측에서 대분류를 선택해 주세요.</p>
          ) : loadingSmall ? (
            <p>불러오는 중…</p>
          ) : smallRows.length === 0 ? (
            <p>등록된 소분류가 없습니다.</p>
          ) : (
            <table>
              <thead>
                <tr>
                  <th>코드</th>
                  <th>명칭</th>
                  <th>작업</th>
                </tr>
              </thead>
              <tbody>
                {smallRows.map((row) => (
                  <tr key={row.id} className={editingSmallId === row.id ? 'row-editing' : undefined}>
                    <td>{row.smallCode}</td>
                    <td>{row.smallName}</td>
                    <td className="actions">
                      <button type="button" className="btn-action" onClick={() => startEditSmall(row)}>
                        수정
                      </button>
                      <button type="button" className="btn-action danger" onClick={() => void onDeleteSmall(row)}>
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
    </>
  );
}
