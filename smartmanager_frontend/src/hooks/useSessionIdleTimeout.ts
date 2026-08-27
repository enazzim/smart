import { useEffect, useRef } from 'react';
import {
  isSessionIdleExpired,
  readLastActivityAt,
  SESSION_IDLE_TIMEOUT_MS,
  setSessionNotice,
  touchSessionActivity,
} from '../utils/sessionIdle';

const ACTIVITY_EVENTS: (keyof WindowEventMap)[] = [
  'mousedown',
  'mousemove',
  'keydown',
  'scroll',
  'touchstart',
  'click',
  'wheel',
];

/**
 * 인증된 화면에서 사용자 입력이 없으면 idleTimeoutMs 후 onIdle 호출.
 * 활동은 1초 throttle로 lastActivity를 갱신합니다.
 */
export function useSessionIdleTimeout(
  enabled: boolean,
  onIdle: () => void,
  idleTimeoutMs: number = SESSION_IDLE_TIMEOUT_MS,
): void {
  const onIdleRef = useRef(onIdle);
  onIdleRef.current = onIdle;

  useEffect(() => {
    if (!enabled) {
      return;
    }

    touchSessionActivity();
    let lastTouchWrite = Date.now();
    let idleTimer: ReturnType<typeof setTimeout> | null = null;

    const clearIdleTimer = () => {
      if (idleTimer != null) {
        clearTimeout(idleTimer);
        idleTimer = null;
      }
    };

    const expireIfIdle = () => {
      if (!isSessionIdleExpired()) {
        return false;
      }
      clearIdleTimer();
      setSessionNotice('idle');
      onIdleRef.current();
      return true;
    };

    const scheduleIdleTimer = () => {
      clearIdleTimer();
      const last = readLastActivityAt() ?? Date.now();
      const remaining = Math.max(0, idleTimeoutMs - (Date.now() - last));
      idleTimer = setTimeout(() => {
        expireIfIdle();
      }, remaining);
    };

    const onActivity = () => {
      const now = Date.now();
      if (now - lastTouchWrite >= 1000) {
        touchSessionActivity(now);
        lastTouchWrite = now;
      }
      scheduleIdleTimer();
    };

    const onVisibility = () => {
      if (document.visibilityState === 'visible') {
        if (expireIfIdle()) {
          return;
        }
        scheduleIdleTimer();
      }
    };

    if (expireIfIdle()) {
      return;
    }

    scheduleIdleTimer();
    for (const eventName of ACTIVITY_EVENTS) {
      window.addEventListener(eventName, onActivity, { passive: true });
    }
    document.addEventListener('visibilitychange', onVisibility);

    return () => {
      clearIdleTimer();
      for (const eventName of ACTIVITY_EVENTS) {
        window.removeEventListener(eventName, onActivity);
      }
      document.removeEventListener('visibilitychange', onVisibility);
    };
  }, [enabled, idleTimeoutMs]);
}
