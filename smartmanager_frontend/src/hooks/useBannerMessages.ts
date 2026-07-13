import { useCallback, useEffect, useState } from 'react';

/**
 * 화면 배너용 성공/오류 메시지.
 * - 성공·오류 피드백은 alert 대신 배너(또는 toast)로 표시한다.
 * - 삭제·취소 확인은 useConfirm(Confirm 모달)을 사용한다.
 */
export function useBannerMessages(autoDismissMs = 5000) {
  const [message, setMessage] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!message && !error) {
      return;
    }
    const timer = window.setTimeout(() => {
      setMessage(null);
      setError(null);
    }, autoDismissMs);
    return () => window.clearTimeout(timer);
  }, [message, error, autoDismissMs]);

  const showSuccess = useCallback((text: string) => {
    setError(null);
    setMessage(text);
  }, []);

  const showError = useCallback((text: string) => {
    setMessage(null);
    setError(text);
  }, []);

  const clear = useCallback(() => {
    setMessage(null);
    setError(null);
  }, []);

  return { message, error, showSuccess, showError, clear };
}
