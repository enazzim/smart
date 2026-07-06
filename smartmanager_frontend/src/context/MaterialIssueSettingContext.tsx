import { createContext, useContext, type ReactNode } from 'react';

interface MaterialIssueSettingContextValue {
  materialIssueEnabled: boolean;
  setMaterialIssueEnabled: (enabled: boolean) => void;
  negativeStockAllowed: boolean;
  setNegativeStockAllowed: (allowed: boolean) => void;
}

const MaterialIssueSettingContext = createContext<MaterialIssueSettingContextValue | null>(null);

export function MaterialIssueSettingProvider({
  materialIssueEnabled,
  setMaterialIssueEnabled,
  negativeStockAllowed,
  setNegativeStockAllowed,
  children,
}: MaterialIssueSettingContextValue & { children: ReactNode }) {
  return (
    <MaterialIssueSettingContext.Provider
      value={{ materialIssueEnabled, setMaterialIssueEnabled, negativeStockAllowed, setNegativeStockAllowed }}
    >
      {children}
    </MaterialIssueSettingContext.Provider>
  );
}

export function useMaterialIssueSetting(): MaterialIssueSettingContextValue {
  const ctx = useContext(MaterialIssueSettingContext);
  if (!ctx) {
    return {
      materialIssueEnabled: false,
      setMaterialIssueEnabled: () => {},
      negativeStockAllowed: true,
      setNegativeStockAllowed: () => {},
    };
  }
  return ctx;
}
