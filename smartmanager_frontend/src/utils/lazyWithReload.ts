import { lazy, type ComponentType, type LazyExoticComponent } from 'react';

const RELOAD_KEY = 'smartmanager:chunk-reload';

/**
 * Vite lazy chunk 로드 실패(배포 직후 옛 해시 요청 등) 시
 * sessionStorage로 1회만 강제 새로고침해 최신 index/assets를 받습니다.
 * (React.lazy와 동일하게 props는 any로 두어 DrawingPage 등 구체 props를 보존)
 */
export function lazyWithReload<T extends ComponentType<any>>(
  factory: () => Promise<{ default: T }>,
): LazyExoticComponent<T> {
  return lazy(async () => {
    try {
      const mod = await factory();
      sessionStorage.removeItem(RELOAD_KEY);
      return mod;
    } catch (err) {
      if (!sessionStorage.getItem(RELOAD_KEY)) {
        sessionStorage.setItem(RELOAD_KEY, '1');
        window.location.reload();
      }
      throw err;
    }
  });
}
