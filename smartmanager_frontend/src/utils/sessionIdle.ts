/** 무동작 세션 만료(밀리초). 브라우저 탭 재시작은 sessionStorage로 별도 종료. */
export const SESSION_IDLE_TIMEOUT_MS = 30 * 60 * 1000;

const LAST_ACTIVITY_KEY = 'smartmanager.lastActivityAt';
const SESSION_NOTICE_KEY = 'smartmanager.sessionNotice';

export type SessionNotice = 'idle' | 'expired';

export function readLastActivityAt(): number | null {
  const raw = sessionStorage.getItem(LAST_ACTIVITY_KEY);
  if (!raw) {
    return null;
  }
  const value = Number(raw);
  return Number.isFinite(value) ? value : null;
}

export function touchSessionActivity(now = Date.now()): void {
  sessionStorage.setItem(LAST_ACTIVITY_KEY, String(now));
}

export function clearSessionActivity(): void {
  sessionStorage.removeItem(LAST_ACTIVITY_KEY);
}

export function isSessionIdleExpired(now = Date.now()): boolean {
  const last = readLastActivityAt();
  if (last == null) {
    return false;
  }
  return now - last >= SESSION_IDLE_TIMEOUT_MS;
}

export function setSessionNotice(notice: SessionNotice): void {
  sessionStorage.setItem(SESSION_NOTICE_KEY, notice);
}

export function consumeSessionNotice(): SessionNotice | null {
  const value = sessionStorage.getItem(SESSION_NOTICE_KEY);
  sessionStorage.removeItem(SESSION_NOTICE_KEY);
  if (value === 'idle' || value === 'expired') {
    return value;
  }
  return null;
}

export function sessionNoticeMessage(notice: SessionNotice | null): string | null {
  if (notice === 'idle') {
    return '일정 시간(30분) 동안 사용이 없어 세션이 종료되었습니다. 다시 로그인해 주세요.';
  }
  if (notice === 'expired') {
    return '인증이 만료되었습니다. 다시 로그인해 주세요.';
  }
  return null;
}
