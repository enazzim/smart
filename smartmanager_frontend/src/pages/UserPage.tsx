import { useEffect, useMemo, useState } from 'react';
import type { AuthenticatedUser } from '../api/auth';
import { changeMyPassword } from '../api/auth';
import type { CodeOption, CreateUserRequest, Role, User } from '../api/user';
import {
  checkLoginId,
  createUser,
  deleteUser,
  fetchRoles,
  fetchUsers,
  fetchWorkDiaryGroups,
  updateUser,
} from '../api/user';
import GridExcelExportButton from '../components/GridExcelExportButton';
import { useConfirm } from '../context/ConfirmContext';

const emptyForm: CreateUserRequest & { passwordConfirm: string } = {
  loginId: '',
  password: '',
  passwordConfirm: '',
  name: '',
  contact: '',
  email: '',
  roleIds: [],
};

const emptyPasswordForm = {
  currentPassword: '',
  password: '',
  passwordConfirm: '',
};

function toggleRole(roleIds: number[], roleId: number): number[] {
  return roleIds.includes(roleId) ? roleIds.filter((id) => id !== roleId) : [...roleIds, roleId];
}

interface UserPageProps {
  currentUser: AuthenticatedUser | null;
  canManageUsers: boolean;
}

export default function UserPage({ currentUser, canManageUsers }: UserPageProps) {
  const confirm = useConfirm();
  const [roles, setRoles] = useState<Role[]>([]);
  const [workDiaryGroups, setWorkDiaryGroups] = useState<CodeOption[]>([]);
  const [users, setUsers] = useState<User[]>([]);
  const [selfUser, setSelfUser] = useState<User | null>(null);
  const [form, setForm] = useState(emptyForm);
  const [passwordForm, setPasswordForm] = useState(emptyPasswordForm);
  const [searchQuery, setSearchQuery] = useState('');
  const [appliedQuery, setAppliedQuery] = useState('');
  const [hasSearched, setHasSearched] = useState(false);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [editingLoginId, setEditingLoginId] = useState<string | null>(null);
  const [loginIdAvailable, setLoginIdAvailable] = useState<boolean | null>(null);
  const [checkingLoginId, setCheckingLoginId] = useState(false);
  const [loading, setLoading] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  const isEditing = editingId !== null;
  const isSelfService = !canManageUsers;

  const userExportRows = useMemo(
    () =>
      users.map((user) => ({
        아이디: user.loginId,
        이름: user.name,
        연락처: user.contact ?? '',
        이메일: user.email ?? '',
        역할: user.roleCodes.join(', '),
        업무일지그룹: user.workDiaryGroupName ?? '',
      })),
    [users],
  );

  const load = async (query = searchQuery) => {
    setLoading(true);
    setError(null);
    try {
      const rows = await fetchUsers(query || undefined);
      if (isSelfService) {
        setSelfUser(rows[0] ?? null);
      } else {
        setUsers(rows);
      }
      return true;
    } catch (e) {
      setError(e instanceof Error ? e.message : '목록 조회 실패');
      return false;
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    void (async () => {
      try {
        if (canManageUsers) {
          const [roleList, diaryGroups] = await Promise.all([fetchRoles(), fetchWorkDiaryGroups()]);
          setRoles(roleList);
          setWorkDiaryGroups(diaryGroups);
        } else {
          setLoading(true);
          await load('');
        }
      } catch (e) {
        setError(e instanceof Error ? e.message : '초기 로드 실패');
        setLoading(false);
      }
    })();
  }, [canManageUsers]);

  const refreshListIfSearched = async () => {
    if (!hasSearched) {
      return;
    }
    await load(appliedQuery);
  };

  const resetForm = () => {
    setForm(emptyForm);
    setEditingId(null);
    setEditingLoginId(null);
    setLoginIdAvailable(null);
  };

  const startEdit = (user: User) => {
    setEditingId(user.id);
    setEditingLoginId(user.loginId);
    setForm({
      loginId: user.loginId,
      password: '',
      passwordConfirm: '',
      name: user.name,
      contact: user.contact ?? '',
      email: user.email ?? '',
      roleIds: [...user.roleIds],
      workDiaryGroupId: user.workDiaryGroupId ?? undefined,
    });
    setLoginIdAvailable(null);
    setError(null);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const onCheckLoginId = async () => {
    const loginId = form.loginId.trim();
    if (!loginId || isEditing) {
      return;
    }
    setCheckingLoginId(true);
    try {
      setLoginIdAvailable(await checkLoginId(loginId));
    } catch {
      setLoginIdAvailable(null);
    } finally {
      setCheckingLoginId(false);
    }
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (form.roleIds.length === 0) {
      setError('사용권한을 1개 이상 선택해 주세요.');
      return;
    }
    if (!isEditing) {
      if (!form.password) {
        setError('비밀번호를 입력해 주세요.');
        return;
      }
      if (form.password !== form.passwordConfirm) {
        setError('비밀번호 확인이 일치하지 않습니다.');
        return;
      }
      if (loginIdAvailable === false) {
        setError('이미 사용 중인 로그인 아이디입니다.');
        return;
      }
    } else if (form.password && form.password !== form.passwordConfirm) {
      setError('비밀번호 확인이 일치하지 않습니다.');
      return;
    }

    setSubmitting(true);
    setError(null);
    try {
      if (isEditing && editingId !== null) {
        await updateUser(editingId, {
          name: form.name,
          password: form.password || undefined,
          contact: form.contact || undefined,
          email: form.email || undefined,
          roleIds: form.roleIds,
          workDiaryGroupId: form.workDiaryGroupId ?? null,
        });
      } else {
        await createUser({
          loginId: form.loginId.trim(),
          password: form.password,
          name: form.name,
          contact: form.contact || undefined,
          email: form.email || undefined,
          roleIds: form.roleIds,
          workDiaryGroupId: form.workDiaryGroupId ?? null,
        });
      }
      resetForm();
      await refreshListIfSearched();
    } catch (err) {
      setError(err instanceof Error ? err.message : isEditing ? '수정 실패' : '등록 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onPasswordSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSuccess(null);
    if (passwordForm.password.length < 8) {
      setError('새 비밀번호는 8자 이상이어야 합니다.');
      return;
    }
    if (passwordForm.password !== passwordForm.passwordConfirm) {
      setError('새 비밀번호 확인이 일치하지 않습니다.');
      return;
    }

    setSubmitting(true);
    setError(null);
    try {
      await changeMyPassword(passwordForm.currentPassword, passwordForm.password);
      setPasswordForm(emptyPasswordForm);
      setSuccess('비밀번호가 변경되었습니다.');
    } catch (err) {
      setError(err instanceof Error ? err.message : '비밀번호 변경 실패');
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async (user: User) => {
    if (!(await confirm(`「${user.name} (${user.loginId})」 사용자를 삭제하시겠습니까?`, { title: '삭제 확인', confirmLabel: '삭제', cancelLabel: '닫기', danger: true }))) {
      return;
    }
    setError(null);
    try {
      await deleteUser(user.id);
      if (editingId === user.id) {
        resetForm();
      }
      await refreshListIfSearched();
    } catch (err) {
      setError(err instanceof Error ? err.message : '삭제 실패');
    }
  };

  const onSearch = async (e: React.FormEvent) => {
    e.preventDefault();
    const ok = await load(searchQuery);
    if (ok) {
      setAppliedQuery(searchQuery);
      setHasSearched(true);
    }
  };

  const onResetSearch = () => {
    setSearchQuery('');
    setAppliedQuery('');
    setUsers([]);
    setHasSearched(false);
    setError(null);
  };

  if (isSelfService) {
    const profile = selfUser;
    const roleLabel =
      profile?.roleCodes.join(', ') || currentUser?.roleCodes.join(', ') || '—';

    return (
      <div className="page">
        <header className="page-header">
          <h1>내 계정</h1>
          <p>본인 정보 확인 및 비밀번호 변경</p>
        </header>

        {error && <div className="error">{error}</div>}
        {success && <div className="success-banner">{success}</div>}

        <section className="panel">
          <h2>계정 정보</h2>
          {loading ? (
            <p>로딩 중…</p>
          ) : (
            <dl className="profile-dl">
              <div>
                <dt>로그인 아이디</dt>
                <dd>{profile?.loginId ?? currentUser?.loginId ?? '—'}</dd>
              </div>
              <div>
                <dt>사원명</dt>
                <dd>{profile?.name ?? currentUser?.name ?? '—'}</dd>
              </div>
              <div>
                <dt>연락처</dt>
                <dd>{profile?.contact ?? '—'}</dd>
              </div>
              <div>
                <dt>이메일</dt>
                <dd>{profile?.email ?? '—'}</dd>
              </div>
              <div>
                <dt>사용권한</dt>
                <dd>{roleLabel}</dd>
              </div>
              <div>
                <dt>업무일지그룹</dt>
                <dd>{profile?.workDiaryGroupName ?? '—'}</dd>
              </div>
            </dl>
          )}
        </section>

        <section className="panel">
          <h2>비밀번호 변경</h2>
          <form onSubmit={onPasswordSubmit} className="form-grid form-grid-wide" data-allow-write>
            <label>
              현재 비밀번호 *
              <input
                type="password"
                required
                value={passwordForm.currentPassword}
                onChange={(e) => setPasswordForm({ ...passwordForm, currentPassword: e.target.value })}
                autoComplete="current-password"
              />
            </label>
            <label>
              새 비밀번호 *
              <input
                type="password"
                required
                minLength={8}
                value={passwordForm.password}
                onChange={(e) => setPasswordForm({ ...passwordForm, password: e.target.value })}
                autoComplete="new-password"
              />
            </label>
            <label>
              새 비밀번호 확인 *
              <input
                type="password"
                required
                value={passwordForm.passwordConfirm}
                onChange={(e) => setPasswordForm({ ...passwordForm, passwordConfirm: e.target.value })}
                autoComplete="new-password"
              />
            </label>
            <div className="form-actions">
              <button type="submit" disabled={submitting}>
                {submitting ? '변경 중…' : '비밀번호 변경'}
              </button>
            </div>
          </form>
        </section>
      </div>
    );
  }

  return (
    <div className="page">
      <header className="page-header">
        <h1>사용자 (User)</h1>
        <p>7필드 CRUD — RBAC 역할·업무일지그룹</p>
      </header>

      {error && <div className="error">{error}</div>}

      <section className="panel">
        <h2>{isEditing ? `사용자 수정 (${editingLoginId})` : '사용자 등록'}</h2>
        <form onSubmit={onSubmit} className="form-grid form-grid-wide">
          <label>
            로그인 아이디 *
            <input
              required
              readOnly={isEditing}
              value={form.loginId}
              onChange={(e) => {
                setForm({ ...form, loginId: e.target.value });
                setLoginIdAvailable(null);
              }}
              onBlur={() => void onCheckLoginId()}
            />
            {!isEditing && form.loginId.trim() && (
              <span className="hint">
                {checkingLoginId
                  ? '중복 확인 중…'
                  : loginIdAvailable === true
                    ? '사용 가능'
                    : loginIdAvailable === false
                      ? '이미 사용 중'
                      : ''}
              </span>
            )}
          </label>
          <label>
            {isEditing ? '새 비밀번호 (변경 시)' : '비밀번호 *'}
            <input
              type="password"
              required={!isEditing}
              value={form.password}
              onChange={(e) => setForm({ ...form, password: e.target.value })}
              autoComplete="new-password"
            />
          </label>
          <label>
            비밀번호 확인 {isEditing ? '' : '*'}
            <input
              type="password"
              required={!isEditing}
              value={form.passwordConfirm}
              onChange={(e) => setForm({ ...form, passwordConfirm: e.target.value })}
              autoComplete="new-password"
            />
          </label>
          <label>
            사원명 *
            <input
              required
              value={form.name}
              onChange={(e) => setForm({ ...form, name: e.target.value })}
            />
          </label>
          <label>
            연락처
            <input value={form.contact} onChange={(e) => setForm({ ...form, contact: e.target.value })} />
          </label>
          <label>
            이메일
            <input
              type="email"
              value={form.email}
              onChange={(e) => setForm({ ...form, email: e.target.value })}
            />
          </label>
          <label>
            업무일지그룹
            <select
              value={form.workDiaryGroupId ?? ''}
              onChange={(e) =>
                setForm({
                  ...form,
                  workDiaryGroupId: e.target.value ? Number(e.target.value) : undefined,
                })
              }
            >
              <option value="">(없음)</option>
              {workDiaryGroups.map((opt) => (
                <option key={opt.id} value={opt.id}>
                  {opt.code} · {opt.name}
                </option>
              ))}
            </select>
          </label>
          <fieldset className="roles">
            <legend>사용권한 *</legend>
            {roles.map((role) => (
              <label key={role.id} className="checkbox">
                <input
                  type="checkbox"
                  checked={form.roleIds.includes(role.id)}
                  onChange={() => setForm({ ...form, roleIds: toggleRole(form.roleIds, role.id) })}
                />
                {role.roleName} ({role.roleCode})
              </label>
            ))}
          </fieldset>
          <div className="form-actions">
            <button type="submit" disabled={submitting || roles.length === 0}>
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
          <h2>사용자 목록</h2>
          <GridExcelExportButton fileBaseName="사용자목록" disabled={loading} rows={userExportRows} />
        </div>
        <form onSubmit={onSearch} className="search-row">
          <label>
            아이디·이름 검색
            <input
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
              placeholder="부분 일치"
            />
          </label>
          <button type="submit" disabled={loading}>
            조회
          </button>
          <button type="button" className="secondary" disabled={loading} onClick={onResetSearch}>
            초기화
          </button>
        </form>
        {!hasSearched ? (
          <p className="hint-text">조회 버튼을 누르면 목록이 표시됩니다.</p>
        ) : loading ? (
          <p>로딩 중…</p>
        ) : (
          <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>아이디</th>
                <th>이름</th>
                <th>연락처</th>
                <th>이메일</th>
                <th>역할</th>
                <th>업무일지그룹</th>
                <th />
              </tr>
            </thead>
            <tbody>
              {users.length === 0 ? (
                <tr>
                  <td colSpan={7}>검색 조건에 맞는 사용자가 없습니다.</td>
                </tr>
              ) : (
                users.map((user) => (
                  <tr key={user.id}>
                    <td>{user.loginId}</td>
                    <td>{user.name}</td>
                    <td>{user.contact ?? '—'}</td>
                    <td>{user.email ?? '—'}</td>
                    <td>{user.roleCodes.join(', ') || '—'}</td>
                    <td>{user.workDiaryGroupName ?? '—'}</td>
                    <td className="row-actions">
                      <button type="button" className="btn-action" onClick={() => startEdit(user)}>
                        수정
                      </button>
                      <button type="button" className="btn-action danger" onClick={() => void onDelete(user)}>
                        삭제
                      </button>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
          </div>
        )}
      </section>
    </div>
  );
}
