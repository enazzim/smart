import { useState } from 'react';
import { login } from '../api/auth';

interface LoginPageProps {
  onSuccess: () => void;
  notice?: string | null;
}

export default function LoginPage({ onSuccess, notice = null }: LoginPageProps) {
  const [loginId, setLoginId] = useState('admin');
  const [password, setPassword] = useState('');
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSubmitting(true);
    setError(null);
    try {
      await login(loginId.trim(), password);
      onSuccess();
    } catch (err) {
      setError(err instanceof Error ? err.message : '로그인에 실패했습니다.');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="login-page">
      <section className="panel login-panel">
        <header>
          <h1>SmartManager 로그인</h1>
          <p>아이디와 비밀번호를 입력해 주세요.</p>
        </header>

        {notice && <div className="error">{notice}</div>}
        {error && <div className="error">{error}</div>}

        <form onSubmit={onSubmit} className="form-grid">
          <label>
            아이디
            <input
              required
              autoComplete="username"
              value={loginId}
              onChange={(e) => setLoginId(e.target.value)}
            />
          </label>
          <label>
            비밀번호
            <input
              required
              type="password"
              autoComplete="current-password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </label>
          <div className="form-actions">
            <button type="submit" disabled={submitting}>
              {submitting ? '로그인 중…' : '로그인'}
            </button>
          </div>
        </form>

        <p className="hint-text">초기 관리자: admin / Admin123! (최초 기동 시 자동 생성)</p>
      </section>
    </div>
  );
}
