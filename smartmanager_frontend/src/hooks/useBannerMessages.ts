import { useCallback, useEffect, useState } from 'react';

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
