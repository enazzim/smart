import {
  createContext,
  useCallback,
  useContext,
  useMemo,
  useRef,
  useState,
  type ReactNode,
} from 'react';
import ConfirmDialog, { type ConfirmDialogOptions } from '../components/ConfirmDialog';

type ConfirmRequest = ConfirmDialogOptions & {
  message: string;
  resolve: (value: boolean) => void;
};

type ConfirmFn = (message: string, options?: ConfirmDialogOptions) => Promise<boolean>;

const ConfirmContext = createContext<ConfirmFn | null>(null);

export function ConfirmProvider({ children }: { children: ReactNode }) {
  const [request, setRequest] = useState<ConfirmRequest | null>(null);
  const requestRef = useRef<ConfirmRequest | null>(null);

  const closeWith = useCallback((value: boolean) => {
    const current = requestRef.current;
    requestRef.current = null;
    setRequest(null);
    current?.resolve(value);
  }, []);

  const confirm = useCallback<ConfirmFn>((message, options) => {
    return new Promise<boolean>((resolve) => {
      if (requestRef.current) {
        requestRef.current.resolve(false);
      }
      const next: ConfirmRequest = {
        message,
        title: options?.title,
        confirmLabel: options?.confirmLabel,
        cancelLabel: options?.cancelLabel,
        danger: options?.danger,
        resolve,
      };
      requestRef.current = next;
      setRequest(next);
    });
  }, []);

  const value = useMemo(() => confirm, [confirm]);

  return (
    <ConfirmContext.Provider value={value}>
      {children}
      <ConfirmDialog
        open={request != null}
        message={request?.message ?? ''}
        title={request?.title}
        confirmLabel={request?.confirmLabel}
        cancelLabel={request?.cancelLabel}
        danger={request?.danger}
        onConfirm={() => closeWith(true)}
        onCancel={() => closeWith(false)}
      />
    </ConfirmContext.Provider>
  );
}

export function useConfirm(): ConfirmFn {
  const confirm = useContext(ConfirmContext);
  if (!confirm) {
    throw new Error('useConfirm는 ConfirmProvider 안에서만 사용할 수 있습니다.');
  }
  return confirm;
}
